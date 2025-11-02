using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public static class KRandom
    {
        public static readonly int IA = 3877;
        public static readonly int IC = 29573;
        public static readonly int IM = 139968;

        static System.Random random = new System.Random();

        private static int nRandomSeed = -1;
        public static int g_GetRandomSeed()
        {
            if (nRandomSeed < 0)
            {
                nRandomSeed = random.Next(100) + 42;
            }

            return nRandomSeed;
        }
        public static int g_Random(int nMax)
        {
            if (nMax <= 0)
            {
                return 0;
            }
            if (nRandomSeed < 0)
            {
                nRandomSeed = g_GetRandomSeed();
            }
            nRandomSeed = nRandomSeed * IA + IC;
            return random.Next(nMax);
        }
        public static int GetRandomNumber(int nMin, int nMax)
        {
            bool nIsThis = false;

            if (nMin < 0 && nMax < 0)
            {
                nMin = -nMin;
                nMax = -nMax;
                nIsThis = true;
            }

            if (nMax - nMin + 1 < 0)
                return 0;

            var value = g_Random(nMax - nMin + 1) + nMin;
            return nIsThis ? -value : value;
        }
        public static bool g_RandPercent(int nPercent) => g_Random(100) < nPercent;
    }
}
