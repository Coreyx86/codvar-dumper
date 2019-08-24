using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace codvar
{
    public class DvarInfo
    {
        public int count;
        public int arrayStart;
        public int typeOffset;
        public int valueOffset;

        public DvarInfo()
        {

        }

        public DvarInfo(int count, int arrayStart, int typeOffset, int valueOffset)
        {
            this.count = count;
            this.arrayStart = arrayStart;
            this.typeOffset = typeOffset;
            this.valueOffset = valueOffset;
        }
    }
    public class Dvar
    {
        /// <summary>
        /// The offset from the base address that holds the dvar_type value.
        /// </summary>
        private int typeOffset = 0;
        /// <summary>
        /// The offset from the base address that holds the dvar's value(s)
        /// </summary>
        private int valueOffset = 0;

        /// <summary>
        /// The base address, in memory, of the DVAR.
        /// </summary>
        public int address { get; set; }

        private int valueLocation
        {
            get { return address + valueOffset; }
        }

        /// <summary>
        /// The name of the dvar. Can be found by following the pointer found at address + 0. Standard across all of the games.
        /// </summary>
        public string name
        {
            get
            {
                int ptrValue = Manager.obj.memory.Extension.ReadInt(address);
                return Manager.obj.memory.Extension.ReadString(ptrValue);
            }
        }
        public DvarType type
        {
            get
            {
                return (DvarType)Manager.obj.memory.Extension.ReadByte(address + typeOffset);
            }
        }

        public string type_str
        {
            get
            {
                return ((DvarType)Manager.obj.memory.Extension.ReadByte(address + typeOffset)).ToString();
            }
        }

        private object _value = null;
        public object value
        {
            get
            {
                if(typeOffset > 0)
                {
                    switch (type)
                    {
                        case DvarType.BOOL:
                            _value = Manager.obj.memory.Extension.ReadBool(valueLocation);
                            break;
                        case DvarType.FLOAT:
                            _value = Manager.obj.memory.Extension.ReadFloat(valueLocation);
                            break;
                        case DvarType.VEC2:
                            _value = Manager.obj.memory.Extension.ReadVec2(valueLocation);
                            break;
                        case DvarType.VEC3:
                        case DvarType.LINEAR_COLOR_RGB:
                            _value = Manager.obj.memory.Extension.ReadVec3(valueLocation);
                            break;
                        case DvarType.VEC4:
                        case DvarType.COLOR:
                            _value = Manager.obj.memory.Extension.ReadVec4(valueLocation);
                            break;
                        case DvarType.INT:
                            _value = Manager.obj.memory.Extension.ReadInt(valueLocation);
                            break;
                        case DvarType.ENUM:
                            _value = Manager.obj.memory.Extension.ReadByte(valueLocation);
                            break;
                        case DvarType.STRING:
                            int strPtr = Manager.obj.memory.Extension.ReadInt(valueLocation);
                            _value = Manager.obj.memory.Extension.ReadString(strPtr);
                            break;
                        case DvarType.INT64:
                            _value = Manager.obj.memory.Extension.ReadInt64(valueLocation);
                            break;
                        case DvarType.COLOR_XYZ:
                        case DvarType.COUNT:
                        default:
                            _value = "N/A";
                            break;
                    }
                }
                return _value;
            }
            set
            {
                _value = value;
                if (typeOffset > 0)
                {
                    switch (type)
                    {
                        case DvarType.BOOL:
                            Manager.obj.memory.Extension.WriteBool(valueLocation, (bool)value);
                            break;
                        case DvarType.FLOAT:
                            Manager.obj.memory.Extension.WriteFloat(valueLocation, (float)value);
                            break;
                        case DvarType.VEC2:
                            Manager.obj.memory.Extension.WriteVec2(valueLocation, (Vector2)value);
                            break;
                        case DvarType.VEC3:
                        case DvarType.LINEAR_COLOR_RGB:
                            Manager.obj.memory.Extension.WriteVec3(valueLocation, (Vector3)value);
                            break;
                        case DvarType.VEC4:
                        case DvarType.COLOR:
                            Manager.obj.memory.Extension.WriteVec4(valueLocation, (Vector4)value);
                            break;
                        case DvarType.INT:
                            Manager.obj.memory.Extension.WriteInt32(valueLocation, (int)value);
                            break;
                        case DvarType.ENUM:
                            Manager.obj.memory.Extension.WriteBytes(valueLocation, new byte[] { (byte)value });
                            break;
                        case DvarType.STRING:
                            int strPtr = Manager.obj.memory.Extension.ReadInt(valueLocation);
                            Manager.obj.memory.Extension.WriteString(strPtr, Convert.ToString(value));
                            break;
                        case DvarType.INT64:
                            Manager.obj.memory.Extension.WriteInt64(valueLocation, (long)value);
                            break;
                        case DvarType.COLOR_XYZ:
                        case DvarType.COUNT:
                        default:
                            break;
                    }
                }
            }
        }

        public enum DvarType
        {
            BOOL,
            FLOAT,
            VEC2,
            VEC3,
            VEC4,
            INT,
            ENUM,
            STRING,
            COLOR,
            INT64,
            LINEAR_COLOR_RGB,
            COLOR_XYZ,
            COUNT
        };

        public Dvar()
        {
            
        }

        public Dvar(int address, int typeOffset, int valueOffset)
        {
            this.address = address;
            this.typeOffset = typeOffset;
            this.valueOffset = valueOffset;
        }
    }

    public class DvarDumpEventArgs : EventArgs
    {
        public Dvar dvar { get; set; }

        public DvarDumpEventArgs(Dvar dvar)
        {
            this.dvar = dvar;
        }
    }
    public class DvarManager
    {
        private string game = string.Empty;

        private int dvar_count = 0;

        private DvarInfo info = null;

        public int dvars_found = 0;

        public delegate void _DvarDumped(object sender, DvarDumpEventArgs e);
        private _DvarDumped dvarDumped;
        public event _DvarDumped DvarDumped
        {
            add
            {
                if (dvarDumped != null)
                {
                    foreach (Delegate d in dvarDumped.GetInvocationList())
                    {
                        if (d.Target == value.Target)
                        {
                            dvarDumped -= (d as _DvarDumped);
                        }
                    }
                }
                dvarDumped += value;
                findDvars();
            }
            remove
            {
                dvarDumped -= value;
            }
        }

        public DvarManager()
        {

        }

        public DvarManager(string game)
        {
            this.game = game;

            info = Manager.obj.g_dvar_info[game];

            dvar_count = Manager.obj.memory.Extension.ReadInt(info.count);
        }

        private void findDvars()
        {
            Dictionary<string, Dvar> found = new Dictionary<string, Dvar>();

            int ptr = 0;
            for(int i = 0; i < dvar_count; i++)
            {
                ptr = Manager.obj.memory.Extension.ReadInt(info.arrayStart + (i * 0x4));

                Dvar dvar = new Dvar(ptr, info.typeOffset, info.valueOffset);

                if (!found.ContainsKey(dvar.name))
                {
                    found.Add(dvar.name, dvar);

                    dvars_found = found.Count;
                    dvarDumped?.Invoke(this, new DvarDumpEventArgs(dvar));
                }
            }
        }
    }
}
