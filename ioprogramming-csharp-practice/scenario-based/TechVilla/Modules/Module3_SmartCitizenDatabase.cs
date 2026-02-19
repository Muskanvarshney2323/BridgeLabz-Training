using System;
using System.Linq;

namespace TechVilla.Modules
{
    public static class Module3_SmartCitizenDatabase
    {
        // Example: store 1D array of IDs and 2D zone-sector counts
        public static int[] CreateCitizenIdArray(int size)
        {
            var ids = Enumerable.Range(1, size).ToArray();
            Array.Reverse(ids); // sample operation
            Array.Sort(ids);
            return ids;
        }

        public static int[,] CreateZoneSectorMatrix(int zones, int sectorsPerZone)
        {
            var matrix = new int[zones, sectorsPerZone];
            var rnd = new Random();
            for (int z = 0; z < zones; z++)
                for (int s = 0; s < sectorsPerZone; s++)
                    matrix[z, s] = rnd.Next(0, 500);
            return matrix;
        }

        public static int FindId(int[] ids, int id)
        {
            return Array.IndexOf(ids, id);
        }
    }
}
