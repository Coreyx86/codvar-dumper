using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryLib;

namespace codvar
{
    public partial class Form1 : Form
    {
        public int dvarCount = 0;
        public int dvarsDumped = 0;

        //Array of the process names that are currently supported by this program
        public string[] processes =
        {
            "iw3sp",
            "iw3mp",
            "CoDWaW",
            "CoDWaWmp",
            "iw4sp",
            "iw4mp",
            "BlackOps",
            "BlackOpsMP",
            "iw5sp",
            "iw5mp"
        };

        //Array of the memory locations that store the current number of registered developer variables in the respected processes.
        public int[] ADDR_DVARCOUNT =
        {
            0x1330C94,
            0xCBA73F8,
            0x21ACF34,
            0xF3EFB58,
            0x1965A68,
            0x637C3C8,
            0x261CBD4,
            0x385BE74, //BOMP
            0x1C42398,
            0x59C8DD8
        };

        //Array of the memory locations that store the pointers to all of the registered developer variables.
        public int[] ADDR_DVARARRAY =
        {
            0x1330CA0,
            0xCBA7408,
            0x21ACF48,
            0xF3EFB68,
            0x1965A90,
            0x637C3F0,
            0x261CBE8,
            0x385BE88,
            0x1C423C0,
            0x59C8E00
        };

        public bool dvarsAreDumped = false;
        public string selectedGame = "";


        public int getIndexOfProcArray(string process)
        {
            for (int i = 0; i < processes.Length; i++)
            {
                if (process == processes[i])
                    return i;
                else continue;
            }
            return 0;
        }


        //Dump an individual DVAR data given the memory location of the DVAR
        //Because the structures of DVARs in each game change a little bit from game to game
        //This function only dumps information that is common in each game such as variable name, memory location, and their respected values
        public void LogDvar(int dvarLocation)
        {
            dvarsDumped++;
            statusLabel.Text = "Status: " + dvarsDumped.ToString() + " / " + dvarCount.ToString();
            Application.DoEvents();


            string dvarName = "";       //The name of the variable;
            string dvarTypeStr = "";    //String representation of the datatype of the DVAR being dumped

            byte dvarType = 0x00;       //DVARs types are represented with a single byte.

            int dvarValue = 0;          //The memory location that stores the value(s) of the DVAR

            dvarName = MemoryLib.Extension.ReadString(MemoryLib.Extension.ReadInt(dvarLocation)); //For every game the name of each dvar is stored as a pointer at the first four bytes of the dvar_s structure

            DataGridViewRow row = (DataGridViewRow)dvarDataGrid.Rows[0].Clone();
            row.Cells[0].Value = dvarName;
            row.Cells[1].Value = dvarLocation.ToString("X"); //I like to display the memory location of each DVAR.

            //Since the DVAR structures change from game to game I must adjust the locations where the dvarType is located and where the values are located.
            if (selectedGame == "iw3sp" || selectedGame == "iw3mp")
            {
                dvarType = MemoryLib.Extension.ReadByte(dvarLocation + 0xA);
                dvarValue = dvarLocation + 0xC;
            }
            else if (selectedGame == "CoDWaW" || selectedGame == "CoDWaWmp")
            {
                dvarType = MemoryLib.Extension.ReadByte(dvarLocation + 0xA);
                dvarValue = dvarLocation + 0x10;
            }
            else if (selectedGame == "iw4sp" || selectedGame == "iw4mp")
            {
                dvarType = (selectedGame == "iw4sp" ? MemoryLib.Extension.ReadByte(dvarLocation + 0xC) : MemoryLib.Extension.ReadByte(dvarLocation + 0x8));
                dvarValue = (selectedGame == "iw4sp" ? dvarLocation + 0x10 : dvarLocation + 0xC);
            }
            else if(selectedGame == "BlackOps" || selectedGame == "BlackOpsMP")
            {
                dvarType = (selectedGame == "BlackOps" ? MemoryLib.Extension.ReadByte(dvarLocation + 0xD) : MemoryLib.Extension.ReadByte(dvarLocation + 0x10));
                dvarValue = (selectedGame == "BlackOps" ? dvarLocation + 0x10 : dvarLocation + 0x18);
            }
            else if (selectedGame == "iw5sp" || selectedGame == "iw5mp")
            {
                dvarType = MemoryLib.Extension.ReadByte(dvarLocation + 0x8);
                dvarValue = dvarLocation + 0xC;
            }

            switch (dvarType)
            {
                //BOOL
                case 0:
                    dvarTypeStr = "BOOL";
                    row.Cells[3].Value = MemoryLib.Extension.ReadByte(dvarValue);
                    break;
                case 1:
                    dvarTypeStr = "FLOAT";
                    row.Cells[3].Value = MemoryLib.Extension.ReadFloat(dvarValue);
                    break;
                case 2:
                    dvarTypeStr = "VEC2";

                    row.Cells[3].Value = MemoryLib.Extension.ReadFloat(dvarValue);
                    row.Cells[4].Value = MemoryLib.Extension.ReadFloat(dvarValue + 4);
                    break;
                case 3:
                    dvarTypeStr = "VEC3";
                    row.Cells[3].Value = MemoryLib.Extension.ReadFloat(dvarValue);
                    row.Cells[4].Value = MemoryLib.Extension.ReadFloat(dvarValue + 4);
                    row.Cells[5].Value = MemoryLib.Extension.ReadFloat(dvarValue + 8);
                    break;
                case 4:
                    dvarTypeStr = "VEC4";
                    row.Cells[3].Value = MemoryLib.Extension.ReadFloat(dvarValue);
                    row.Cells[4].Value = MemoryLib.Extension.ReadFloat(dvarValue + 4);
                    row.Cells[5].Value = MemoryLib.Extension.ReadFloat(dvarValue + 8);
                    row.Cells[6].Value = MemoryLib.Extension.ReadFloat(dvarValue + 12);
                    break;
                case 5:
                    dvarTypeStr = "INT";
                    row.Cells[3].Value = MemoryLib.Extension.ReadInt(dvarValue);
                    break;
                case 6:
                    dvarTypeStr = "ENUM";
                    break;
                case 7:
                    dvarTypeStr = "STRING";
                    row.Cells[3].Value = MemoryLib.Extension.ReadString(MemoryLib.Extension.ReadInt(dvarValue));
                    break;
                case 8:
                    dvarTypeStr = "COLOR";
                    row.Cells[3].Value = MemoryLib.Extension.ReadFloat(dvarValue);
                    row.Cells[4].Value = MemoryLib.Extension.ReadFloat(dvarValue + 4);
                    row.Cells[5].Value = MemoryLib.Extension.ReadFloat(dvarValue + 8);
                    row.Cells[6].Value = MemoryLib.Extension.ReadFloat(dvarValue + 12);
                    break;
                case 9:
                    dvarTypeStr = "INT64";
                    break;
                case 10:
                    dvarTypeStr = "LINEAR_COLOR_RGB";
                    break;
                case 11:
                    dvarTypeStr = "COLOR XYZ";
                    break;
                case 12:
                    dvarTypeStr = "COUNT";
                    break;

            }

            row.Cells[2].Value = dvarTypeStr;

            dvarDataGrid.Rows.Add(row);
        }

        public void DvarDumper()
        {
            int selectedGameIndex = getIndexOfProcArray(selectedGame);

            dvarCount = MemoryLib.Extension.ReadInt(ADDR_DVARCOUNT[selectedGameIndex]);

            statusLabel.Text = "Status: 0 / " + dvarCount.ToString();
            Application.DoEvents();

            for (int i = 0; i < dvarCount; i++)
                LogDvar(MemoryLib.Extension.ReadInt(ADDR_DVARARRAY[selectedGameIndex] + (0x4 * i))); //Go through the dvar array which holds a 32bit pointer to each dvar and dump them
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //btnDump_Click
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                statusLabel.Text = "Status: " + dvarsDumped.ToString() + " / "+dvarCount.ToString();

                selectedGame = gameBox.Text;
                MemoryLib.MemoryLib.processName = selectedGame;

                DvarDumper();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
