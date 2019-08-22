using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryLib;

namespace codvar
{
    public sealed class Manager
    {
        private static readonly Lazy<Manager> mgr = new Lazy<Manager>(() => new Manager());

        public static Manager obj { get { return mgr.Value; } }

        public Memory memory { get; set; }

        public DvarManager dvarmgr { get; set; }

        /// <summary>
        /// Dictionary to store the memory locations that hold the dvar count and the start to the dvar[] { game, <dvarCount, dvarArray>}
        /// </summary>
        public Dictionary<string, DvarInfo> g_dvar_info = new Dictionary<string, DvarInfo>
        {
            { "iw3sp", new DvarInfo(0x1330C94, 0x1330CA0, 0xA, 0xC) },
            { "iw3mp", new DvarInfo(0xCBA73F8, 0xCBA7408, 0xA, 0xC) },
            { "CoDWaW", new DvarInfo(0x21ACF34, 0x21ACF48, 0xA, 0x10) },
            { "CoDWaWmp", new DvarInfo(0xF3EFB58, 0xF3EFB68, 0xA, 0x10) },
            { "iw4sp", new DvarInfo(0x1965A68, 0x1965A90, 0xC, 0x10) },
            { "iw4mp", new DvarInfo(0x637C3C8, 0x637C3F0, 0x8, 0xC) },
            { "BlackOps", new DvarInfo(0x261CBD4, 0x261CBE8, 0x10, 0x18) },
            { "BlackOpsMP", new DvarInfo(0x385BE74, 0x385BE88, 0x10, 0x18) },
            { "iw5sp", new DvarInfo(0x1C42398, 0x1C423C0, 0x8, 0xC) },
            { "iw5mp", new DvarInfo(0x59C8DD8, 0x59C8E00, 0x8, 0xC) },
        };

        private Manager()
        {
            memory = new Memory(string.Empty);
        }
    }
}
