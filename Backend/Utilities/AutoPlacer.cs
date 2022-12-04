using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using NPOI;

namespace Backend.Utilities
{
    struct SchoolInfo
    {
        public string Name;
        public int Capacity;
        public int CurrentCount;
        public int RemainingCapacity => Capacity - CurrentCount;
    }

    public static class AutoPlacer
    {
        public static void autoPlacer()
        {

        }

    }
}
