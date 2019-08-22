using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryLib;
using System.Numerics;

namespace codvar
{
    public partial class Form1 : Form
    {

        public bool formIsLoaded = false;
        public string selectedGame = "";


        //Dump an individual DVAR data given the memory location of the DVAR
        //Because the structures of DVARs in each game change a little bit from game to game
        //This function only dumps information that is common in each game such as variable name, memory location, and their respected values
        public void Dvarmgr_DvarDumped(object sender, DvarDumpEventArgs e)
        {
            statusLabel.Text = "Status: " + Manager.obj.dvarmgr.dvars_found + " / " + Manager.obj.g_dvar_info[selectedGame].count;
            Application.DoEvents();


            DataGridViewRow row = (DataGridViewRow)dvarDataGrid.Rows[0].Clone();
            row.Cells[0].Value = e.dvar.name;
            row.Cells[1].Value = e.dvar.address.ToString("X"); //I like to display the memory location of each DVAR.

            switch ((int)e.dvar.type)
            {
                //BOOL
                case 0:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 1:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 2:
                    row.Cells[3].Value = ((Vector2)e.dvar.value).X;
                    row.Cells[4].Value = ((Vector2)e.dvar.value).Y;
                    break;
                case 3:
                    row.Cells[3].Value = ((Vector3)e.dvar.value).X;
                    row.Cells[4].Value = ((Vector3)e.dvar.value).Y;
                    row.Cells[5].Value = ((Vector3)e.dvar.value).Z;
                    break;
                case 4:
                    row.Cells[3].Value = ((Vector4)e.dvar.value).W;
                    row.Cells[4].Value = ((Vector4)e.dvar.value).X;
                    row.Cells[5].Value = ((Vector4)e.dvar.value).Y;
                    row.Cells[6].Value = ((Vector4)e.dvar.value).Z;
                    break;
                case 5:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 6:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 7:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 8:
                    row.Cells[3].Value = ((Vector4)e.dvar.value).W;
                    row.Cells[4].Value = ((Vector4)e.dvar.value).X;
                    row.Cells[5].Value = ((Vector4)e.dvar.value).Y;
                    row.Cells[6].Value = ((Vector4)e.dvar.value).Z;
                    break;
                case 9:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 10:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 11:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;
                case 12:
                    row.Cells[3].Value = e.dvar.value.ToString();
                    break;

            }

            row.Cells[2].Value = e.dvar.type_str;

            dvarDataGrid.Rows.Add(row);
        }


        public Form1()
        {
            InitializeComponent();
            formIsLoaded = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //btnDump_Click
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                selectedGame = gameBox.Text;

                Manager.obj.memory.ProcessName = selectedGame;

                Manager.obj.dvarmgr = new DvarManager(Manager.obj.memory.ProcessName);

                Manager.obj.dvarmgr.DvarDumped += Dvarmgr_DvarDumped;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dvarDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(formIsLoaded)
            {
                int rPos = dvarDataGrid.CurrentCell.ColumnIndex; //Gets the column
                int i = dvarDataGrid.CurrentRow.Index; //Gets the row

                if(rPos >= 3)
                {
                    // int dvarLocation = (int)dvarDataGrid.Rows[i].Cells[1].Value; //Get the memory location of the dvar from the dataGrid
                    int dvarLocation = int.Parse(dvarDataGrid.Rows[i].Cells[1].Value.ToString(), System.Globalization.NumberStyles.HexNumber);

                    Dvar dvar = new Dvar(dvarLocation, Manager.obj.g_dvar_info[selectedGame].typeOffset, Manager.obj.g_dvar_info[selectedGame].valueOffset);

                    switch ((int)dvar.value)
                    {
                        case 0:
                            //dvarTypeStr = "BOOL";
                            dvar.value = Convert.ToBoolean(dvarDataGrid.Rows[i].Cells[rPos].Value);
                            break;
                        case 1:
                            //dvarTypeStr = "FLOAT";
                            dvar.value = Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos].Value);
                            break;
                        case 2:
                            //dvarTypeStr = "VEC2";
                            Vector2 vec2 = new Vector2(Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos].Value), Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 1].Value));
                            dvar.value = vec2;
                            break;
                        case 3:
                            //dvarTypeStr = "VEC3";

                            Vector3 vec3 = new Vector3(Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos].Value), Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 1].Value), Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 2].Value));
                            dvar.value = vec3;
                            break;
                        case 4:
                            //dvarTypeStr = "VEC4";
                            Vector4 vec4 = new Vector4(Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos].Value),
                                                    Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 1].Value),
                                                    Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 2].Value),
                                                    Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 3].Value));

                            dvar.value = vec4;

                            break;
                        case 5:
                            //dvarTypeStr = "INT";
                            dvar.value = Convert.ToInt32(dvarDataGrid.Rows[i].Cells[rPos].Value);

                            break;
                        case 7:
                           // dvarTypeStr = "STRING";
                            string tmp = Convert.ToString(dvarDataGrid.Rows[i].Cells[rPos].Value) + "\0";
                            dvar.value = tmp; //We have to write the string to the pointer address
                            break;
                        case 8:
                            //dvarTypeStr = "COLOR";
                            Vector4 rgba = new Vector4(Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos].Value),
                                                    Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 1].Value),
                                                    Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 2].Value),
                                                    Convert.ToSingle(dvarDataGrid.Rows[i].Cells[rPos + 3].Value));

                            dvar.value = rgba;
                            break;
                        case 9:
                            //dvarTypeStr = "INT64";
                            dvar.value = Convert.ToInt64(dvarDataGrid.Rows[i].Cells[rPos].Value);
                            break;
                    }
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
