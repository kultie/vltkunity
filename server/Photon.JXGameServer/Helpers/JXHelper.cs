using Photon.JXGameServer.Maps;
using System;
using System.Runtime.InteropServices;

namespace Photon.JXGameServer.Helpers
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KSPTrap
    {
        public byte cX;
        public byte cY;
        public byte cNumCell;
        public byte cReserved;
        public uint uTrapId;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct KSPNpc
    {
        public int nTemplateID;
        public int nPositionX;
        public int nPositionY;
        public byte Kind;
        public byte Camp;
        public byte Level;

        public uint Scripts;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct KSPObj
    {
        public int nTemplateID;
        public int nPositionX;
        public int nPositionY;
        public byte Dir;

        public uint Scripts;
    }
    public static class JXHelper
    {
        public static byte[] bytes = new byte[1024];

        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern uint FileNameToId(IntPtr path,int size);


        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int LoadPackages(IntPtr path);


        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int ExtractElem(IntPtr path, IntPtr buffer);

        
        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool ExtractFile(IntPtr path);


        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool LoadObstacle(IntPtr buffer);

        
        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int ReadTraps();
        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool LoadTrap(IntPtr buf);


        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int ReadNpcs();
        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool LoadNpc(IntPtr buf);


        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int ReadObjs();
        [DllImport("JX.LoadPak.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool LoadObj(IntPtr buf);

        public static uint FileNameToId(byte[] path,int s = 0)
        {
            if (s == 0) s = path.Length;

            var b = Marshal.AllocHGlobal(s);
            Marshal.Copy(path,0,b,s);
            var c = FileNameToId(b,s);
            Marshal.FreeHGlobal(b);
            return c;
        }

        public static int LoadPackages(string root)
        {
            var p = Marshal.StringToHGlobalAnsi(root);
            var c = LoadPackages(p);
            Marshal.FreeHGlobal(p);
            return c;
        }
        public static bool ExtractElem(string path)
        {
            var p = Marshal.StringToHGlobalAnsi(path);
            var b = Marshal.AllocHGlobal(1024);

            var c = ExtractElem(p,b);
            if (c > 0)
            {
                Array.Clear(bytes, 0, bytes.Length);
                Marshal.Copy(b, bytes, 0, c);
            }
            
            Marshal.FreeHGlobal(b);
            Marshal.FreeHGlobal(p);
            return c > 0;
        }
        public static bool ExtractFile(string path)
        {
            var p = Marshal.StringToHGlobalAnsi(path);
            var r = ExtractFile(p);
            Marshal.FreeHGlobal(p);
            return r;
        }
        public static void LoadObstacle(_KGridData m_cObstacle)
        {
            int ofs = 0;
            var b = Marshal.AllocHGlobal(m_nRegionWidth * m_nRegionHeight * sizeof(int));
            if (LoadObstacle(b))
            {
                for (var i = 0; i < m_nRegionWidth; ++i)
                {
                    for (var j = 0; j < m_nRegionHeight; ++j)
                    {                        
                        var v = (uint)Marshal.ReadInt32(b, ofs);
                        if (v > 0)
                            m_cObstacle.SetData(i, j, v);
                        ofs += sizeof(int);
                    }
                }
            }
            Marshal.FreeHGlobal(b);
        }
        public static void LoadTrap(_KGridData m_cTrap)
        {
            var n = ReadTraps();
            if (n > 0)
            {
                var b = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(KSPTrap)));
                for (var i = 0; i <n; ++i)
                {
                    if (!LoadTrap(b))
                        break;

                    var t = (KSPTrap)Marshal.PtrToStructure(b, typeof(KSPTrap));
                    var cX = t.cX;
                    var cY = t.cY;
                    var uTrapId = t.uTrapId;
                    var number = t.cNumCell;

                    if (cY >= m_nRegionHeight || cX + number - 1 >= m_nRegionWidth)
                        continue;

                    for (var j = 0; j < number; ++j)
                    {
                        m_cTrap.SetData(cX + j, cY, uTrapId);
                    }
                }
                Marshal.FreeHGlobal(b);
            }
        }
        public static int LoadNpc(RegionObj r)
        {
            var n = ReadNpcs();
            if (n > 0)
            {
                var b = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(KSPNpc)));
                for (var i = 0; i < n; ++i)
                {
                    if (!LoadNpc(b))
                        break;

                    var t = (KSPNpc)Marshal.PtrToStructure(b, typeof(KSPNpc));
                    r.LoadNpc(t);
                }
                Marshal.FreeHGlobal(b);
            }
            return n;
        }
        public static int LoadObj(RegionObj r)
        {
            var n = ReadObjs();
            if (n > 0)
            {
                var b = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(KSPObj)));
                for (var i = 0; i < n; ++i)
                {
                    if (!LoadObj(b))
                        break;

                    var t = (KSPObj)Marshal.PtrToStructure(b, typeof(KSPObj));
                    r.LoadObj(t);
                }
                Marshal.FreeHGlobal(b);
            }
            return n;
        }

        public static uint FileNameToId(string root)
        {
            var p = Marshal.StringToHGlobalAnsi(root);
            var r = FileNameToId(p,(root.Length + 1)* Marshal.SystemMaxDBCSCharSize);
            Marshal.FreeHGlobal(p);
            return r;
        }

        public const byte MAX_LEVEL = 150;

        public const byte m_nRegionWidth = 16;
        public const byte m_nRegionHeight = 32;

        public const byte m_nCellWidth = 32;
        public const byte m_nCellHeight = 32;

        public const int CELLWIDTH = ((int)m_nCellWidth) << 10;
        public const int CELLHEIGHT = ((int)m_nCellHeight) << 10;


        public const short PLAYER_SHARE_EXP_DISTANCE = 768;
        public const byte PLAYER_TEAM_EXP_ADD = 50;
        public const byte PLAYER_PICKUP_CLIENT_DISTANCE = 63;

        public const byte MAX_FIND_TIMER = 30;
        public const byte defFIND_PATH_STOP_DISTANCE = 64;
        public const byte MaxMissleDir = 64;

        static short[] g_nSin = new short[]
        {
            1024,   1019,   1004,   979,    946,    903,    851,    791,
            724,    649,    568,    482,    391,    297,    199,    100,
            0,     -100,    -199,   -297,   -391,   -482,   -568,   -649,
            -724,   -791,   -851,   -903,   -946,   -979,   -1004,  -1019,
            -1024,  -1019,  -1004,  -979,   -946,   -903,   -851,   -791,
            -724,   -649,   -568,   -482,   -391,   -297,   -199,   -100,
            0,       100,   199,    297,    391,    482,    568,    649,
            724,    791,    851,    903,    946,    979,    1004,   1019
        };
        static short[] g_nCos = new short[]
        {
            0,      -100,   -199,   -297,   -391,   -482,   -568,   -649,
            -724,   -791,   -851,   -903,   -946,   -979,   -1004,  -1019,
            -1024,  -1019,  -1004,  -979,   -946,   -903,   -851,   -791,
            -724,   -649,   -568,   -482,   -391,   -297,   -199,   -100,
            0,       100,   199,    297,    391,    482,    568,    649,
            724,    791,    851,    903,    946,    979,    1004,   1019,
            1024,   1019,   1004,   979,    946,    903,    851,    791,
            724,    649,    568,    482,    391,    297,    199,    100,
        };
        public static int g_DirSin(int nDir)
        {
            if (nDir < 0)
                nDir = MaxMissleDir + nDir;

            if (nDir >= MaxMissleDir)
                nDir -= MaxMissleDir;

            return g_nSin[nDir];
        }
        public static int g_DirCos(int nDir)
        {
            if (nDir < 0)
                nDir = MaxMissleDir + nDir;

            if (nDir >= MaxMissleDir)
                nDir -= MaxMissleDir;

            return g_nCos[nDir];
        }
        public static int g_Dir2DirIndex(int nDir) => (nDir << 6) / MaxMissleDir;
        public static int g_DirIndex2Dir(int nDir) => (MaxMissleDir * nDir) >> 6;
        public static int g_GetDirIndex(int nX1, int nY1, int nX2, int nY2) => g_GetNewDirIndex(nX2 - nX1, nY2 - nY1);
        public static int g_GetNewDirIndex(int nDx, int nDy)
        {
            int nRet = -1;

            if (nDx == 0 && nDy == 0)
                return -1;

            int nDistance = g_GetLength(nDx, nDy);

            if (nDistance == 0) return -1;

            int nSin = (nDy << 10) / nDistance;

            //find more than me as my dir
            for (int i = 0; i < 32; i++)
            {
                if (nSin > g_nSin[i])
                    break;
                nRet = i;
            }

            int nD1, nD2;
            if (g_nSin[nRet] != nSin)
            {
                nD1 = g_nSin[nRet] - nSin;
                nD2 = nSin - g_nSin[nRet + 1];
                if (nD1 > nD2)
                    nRet++;
            }

            if (nDx >= 0 && nRet != 0)
            {
                nRet = 64 - nRet;
            }
            return nRet;
        }
        public static int g_GetDistance(int nX1, int nY1, int nX2, int nY2)
        {
            return (int)Math.Sqrt((nX1 - nX2) * (nX1 - nX2) + (nY1 - nY2) * (nY1 - nY2));
        }
        public static int g_GetLength(int nDx, int Dy)
        {
            return (int)Math.Sqrt(nDx * nDx + Dy * Dy);
        }
        public static int MakeLong(int a, int b)
        {
            return (int)(((ushort)(a & 0xffff)) | ((int)((ushort)(b & 0xffff))) << 16);
        }
        public static int LoWord(int x)
        {
            return x & 0xffff;
        }
        public static int HiWord(int x)
        {
            return (x >> 16) & 0xffff;
        }
    }
}
