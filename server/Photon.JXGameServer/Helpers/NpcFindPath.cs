using Photon.JXGameServer.Entitys;
using Photon.ShareLibrary.Constant;

namespace Photon.JXGameServer.Helpers
{
    public class NpcFindPath
    {
        int m_nDestX;
        int m_nDestY;

        byte m_nFindState;
        byte m_nPathSide;

        CharacterObj m_mapObj;
        public NpcFindPath(CharacterObj owner)
        {
            m_nDestX = m_nDestY = 0;
            m_mapObj = owner;
        }
        void GetDir(ref int nWantDir, ref byte nTempDir64, ref int x, ref int y, byte nMoveSpeed)
        {
            if (nTempDir64 < 0)
            {
                nWantDir = JXHelper.MaxMissleDir + nWantDir;
            }
            if (nTempDir64 >= JXHelper.MaxMissleDir)
            {
                nTempDir64 = (byte)(nTempDir64 % JXHelper.MaxMissleDir);
            }
            x = JXHelper.g_DirCos(nTempDir64) * nMoveSpeed;
            y = JXHelper.g_DirSin(nTempDir64) * nMoveSpeed;
        }
        public int GetDir(int nXpos, int nYpos, int nDestX, int nDestY, byte nMoveSpeed, ref byte pnGetDir)
        {
            if (m_mapObj.m_nIsOver)
            {
                m_nFindState = 0;
                m_mapObj.m_nIsOver = false;
                return 0;
            }
            if (!CheckDistance(nXpos >> 10, nYpos >> 10, nDestX, nDestY, nMoveSpeed))
            {
                m_nFindState = 0;
                return 0;
            }
            if ((m_nDestX != nDestX) || (m_nDestY != nDestY))
            {
                m_nFindState = 0;
                m_nDestX = nDestX;
                m_nDestY = nDestY;
            }
            int nWantDir = JXHelper.g_GetDirIndex(nXpos >> 10, nYpos >> 10, nDestX, nDestY);
            if (nWantDir < 0)
            {
                nWantDir = JXHelper.MaxMissleDir + nWantDir;
            }
            if (nWantDir >= JXHelper.MaxMissleDir)
            {
                nWantDir = nWantDir % JXHelper.MaxMissleDir;
            }
            int x = JXHelper.g_DirCos(nWantDir) * nMoveSpeed;
            int y = JXHelper.g_DirSin(nWantDir) * nMoveSpeed;
            
            var nCheckBarrier = CheckBarrierMin( x,y);
            if (nCheckBarrier == (byte)Obstacle_Kind.Obstacle_NULL)
            {
                m_nFindState = 0;

                pnGetDir = (byte)nWantDir;
                return 1;
            }
            if (nCheckBarrier == 0xff)
            {
                return -1;
            }

            byte i, nTempDir8, nTempDir64;

            if (m_nFindState == 0)
            {
                if ((m_mapObj.scene.TestBarrier(nDestX, nDestY) != 0) && !CheckDistance(nXpos >> 10, nYpos >> 10, nDestX, nDestY, JXHelper.defFIND_PATH_STOP_DISTANCE))
                    return 0;

                nTempDir8 = (byte)(Dir64To8(nWantDir) + 8);
                nTempDir64 = Dir8To64((byte)(nTempDir8 & 0x07));

                GetDir(ref nWantDir, ref nTempDir64, ref x, ref y, nMoveSpeed);

                if (CheckBarrierMin(x,y) == (byte)Obstacle_Kind.Obstacle_NULL)
                {
                    m_nFindState = 1;
                    if ((nTempDir64 < nWantDir && nWantDir - nTempDir64 <= 4) || (nTempDir64 > nWantDir && nTempDir64 - nWantDir >= 60))
                        m_nPathSide = 0;
                    else
                        m_nPathSide = 1;
                    
                    pnGetDir = nTempDir64;
                    return 1;
                }
                for (i = 1; i < 8; ++i)
                {
                    nTempDir64 = Dir8To64((byte)((nTempDir8 + i) & 0x07));

                    GetDir(ref nWantDir, ref nTempDir64, ref x, ref y, nMoveSpeed);

                    if (CheckBarrierMin(x,y) == (byte)Obstacle_Kind.Obstacle_NULL)
                    {
                        m_nFindState = 1;
                        m_nPathSide = 1;

                        pnGetDir = nTempDir64;
                        return 1;
                    }

                    nTempDir64 = Dir8To64((byte)((nTempDir8 - i) & 0x07));

                    GetDir(ref nWantDir, ref nTempDir64, ref x, ref y, nMoveSpeed);

                    if (CheckBarrierMin(x, y) == (byte)Obstacle_Kind.Obstacle_NULL)
                    {
                        m_nFindState = 1;
                        m_nPathSide = 0;
                        
                        pnGetDir = nTempDir64;
                        return 1;
                    }
                }
            }
            else
            {
                int nWantDir8 = Dir64To8(nWantDir) + 8;
                if (m_nPathSide == 1)
                {
                    nTempDir64 = Dir8To64((byte)(nWantDir8 & 0x07));
                    if ((nTempDir64 < nWantDir && nWantDir - nTempDir64 <= 4) || (nTempDir64 > nWantDir && nTempDir64 - nWantDir >= 60))
                        i = 1;
                    else
                        i = 0;

                    for (; i < 8; ++i)
                    {
                        nTempDir64 = Dir8To64((byte)((nWantDir8 + i) & 0x07));

                        GetDir(ref nWantDir, ref nTempDir64, ref x, ref y, nMoveSpeed);

                        if (CheckBarrierMin(x, y) == (byte)Obstacle_Kind.Obstacle_NULL)
                        {
                            pnGetDir = nTempDir64;
                            return 1;
                        }
                    }
                }
                else
                {
                    nTempDir64 = Dir8To64((byte)(nWantDir8 & 0x07));
                    if ((nTempDir64 < nWantDir && nWantDir - nTempDir64 <= 4) || (nTempDir64 > nWantDir && nTempDir64 - nWantDir >= 60))
                        i = 0;
                    else
                        i = 1;

                    for (; i < 8; ++i)
                    {
                        nTempDir64 = Dir8To64((byte)((nWantDir8 - i) & 0x07));

                        GetDir(ref nWantDir, ref nTempDir64, ref x, ref y, nMoveSpeed);

                        if (CheckBarrierMin(x, y) == (byte)Obstacle_Kind.Obstacle_NULL)
                        {
                            pnGetDir = nTempDir64;
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }
        byte Dir64To8(int nDir)
        {
            return (byte)(((nDir + 4) >> 3) & 0x07);
        }
        byte Dir8To64(byte nDir)
        {
            return (byte)(nDir << 3);  // *8
        }
        bool CheckDistance(int x1, int y1, int x2, int y2, byte nDistance)
        {
            return ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) >= nDistance * nDistance);
        }
        byte CheckBarrierMin(int nChangeX, int nChangeY) => m_mapObj.scene.TestBarrierMin(m_mapObj.region.RegionIndex, m_mapObj.m_MapX, m_mapObj.m_MapY, m_mapObj.m_OffX, m_mapObj.m_OffY, nChangeX, nChangeY);
    }
}
