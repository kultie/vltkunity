using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Maps;
using Photon.ShareLibrary.Constant;

namespace Photon.JXGameServer.Entitys
{
    public class MapObj
    {
        public SceneObj scene;
        public RegionObj region;
        protected int LEFTREGIONIDX => region.m_nConnectRegion[2];
        protected int RIGHTREGIONIDX => region.m_nConnectRegion[6];
        protected int UPREGIONIDX => region.m_nConnectRegion[4];
        protected int DOWNREGIONIDX => region.m_nConnectRegion[0];

        public int id;

        public int m_MapX;
        public int m_MapY;
        public int m_OffX;
        public int m_OffY;

        public byte m_Dir = 1;

        public uint m_Script = 0;

        public int m_nPeopleIdx;

        public MapObj(SceneObj scene,RegionObj region)
        {
            this.id = 0;

            this.scene = scene;
            this.region = region;

            this.m_nPeopleIdx = 0;

        }
        public int GetDistance(MapObj obj)
        {
            int X = 0, Y = 0;
            GetMpsPos(ref X, ref Y);

            int x = 0, y = 0;
            obj.GetMpsPos(ref x, ref y);

            return JXHelper.g_GetDistance(x, y, X, Y);
        }
        public int GetPlayerSquare(int id)
        {
            var obj = scene.FindPlayer(id);
            if (obj == null) return -1;

            return GetDistanceSquare(obj);
        }
        public int GetDistanceSquare(MapObj obj)
        {
            int X = 0, Y = 0;
            GetMpsPos(ref X, ref Y);

            int x = 0, y = 0;
            obj.GetMpsPos(ref x, ref y);

            X -= x;
            Y -= y;

            return (X * X + Y * Y);
        }
        public bool GetOffset(ref int nSearchRegion, ref int nRMx, ref int nRMy)
        {
            if (nSearchRegion < 0 || nSearchRegion >= scene.m_Region.Count)
                return false;

            if (nRMx < 0)
            {
                nSearchRegion = scene.m_Region[nSearchRegion].m_nConnectRegion[2];
                nRMx += JXHelper.m_nRegionWidth;
            }
            else
            if (nRMx >= JXHelper.m_nRegionWidth)
            {
                nSearchRegion = scene.m_Region[nSearchRegion].m_nConnectRegion[6];
                nRMx -= JXHelper.m_nRegionWidth;
            }

            if (nSearchRegion < 0 || nSearchRegion >= scene.m_Region.Count)
                return false;

            if (nRMy < 0)
            {
                nSearchRegion = scene.m_Region[nSearchRegion].m_nConnectRegion[4];
                nRMy += JXHelper.m_nRegionHeight;
            }
            else
            if (nRMy >= JXHelper.m_nRegionHeight)
            {
                nSearchRegion = scene.m_Region[nSearchRegion].m_nConnectRegion[0];
                nRMy -= JXHelper.m_nRegionHeight;
            }

            if (nSearchRegion < 0 || nSearchRegion >= scene.m_Region.Count)
                return false;

            return true;
        }
        protected int MoveMe(int x, int y, int nOldRegionIndex)
        {
            m_OffX += x;
            m_OffY += y;

            if (m_OffX < 0)
            {
                m_MapX--;
                m_OffX += JXHelper.CELLWIDTH;
            }
            else
            if (m_OffX > JXHelper.CELLWIDTH)
            {
                m_MapX++;
                m_OffX -= JXHelper.CELLWIDTH;
            }

            if (m_OffY < 0)
            {
                m_MapY--;
                m_OffY += JXHelper.CELLHEIGHT;
            }
            else
            if (m_OffY > JXHelper.CELLHEIGHT)
            {
                m_MapY++;
                m_OffY -= JXHelper.CELLHEIGHT;
            }

            int m_RegionIndex = nOldRegionIndex;
            
            GetOffset(ref m_RegionIndex, ref m_MapX, ref m_MapY);

            return m_RegionIndex;
        }
        CharacterObj GetNearestNpcAt(int nRMx, int nRMy, NPCRELATION nRelation, CharacterObj obj)
        {
            int nSearchRegion = region.RegionIndex;

            if (GetOffset(ref nSearchRegion, ref nRMx, ref nRMy))
                return scene.m_Region[nSearchRegion].FindObj(nRMx, nRMy, obj, nRelation);
            else
                return null;
        }
        public CharacterObj GetNearestNpc(int nRangeX, int nRangeY, NPCRELATION nRelation)
        {
            for (int i = 0; i < nRangeX; ++i)
            {
                for (int j = 0; j < nRangeY; ++j)
                {
                    //if ((i * i + j * j) > nRangeX * nRangeX)
                        //continue;

                    var ret = GetNearestNpcAt(m_MapX + i, m_MapY + j, nRelation, (CharacterObj)this);
                    if (ret != null)
                        return ret;

                    if (i == 0 && j == 0)
                        continue;

                    ret = GetNearestNpcAt(m_MapX - i, m_MapY + j, nRelation, (CharacterObj)this);
                    if (ret != null)
                        return ret;

                    ret = GetNearestNpcAt(m_MapX - i, m_MapY - j, nRelation, (CharacterObj)this);
                    if (ret != null)
                        return ret;

                    ret = GetNearestNpcAt(m_MapX + i, m_MapY - j, nRelation, (CharacterObj)this);
                    if (ret != null)
                        return ret;
                }
            }
            return null;
        }
        public CharacterObj GetNearestNpc(int nMapX,int nMapY,int nRange, NPCRELATION nRelation, CharacterObj obj)
        {
            for (int i = -nRange; i <= nRange; ++i)
            {
                for (int j = -nRange; j <= nRange; ++j)
                {
                    //if ((i * i + j * j) > nRange * nRange)
                    //    continue;

                    var ret = GetNearestNpcAt(nMapX + i, nMapY + j, nRelation, obj);
                    if (ret != null)
                        return ret;
                }
            }
            return null;
        }
        public int AddMap(int X, int Y)
        {
            scene.GetFreeObjPos(ref X, ref Y);

            int nRegion = m_MapX = m_MapY = m_OffX = m_OffY = 0;
            scene.Mps2Map(X, Y, ref nRegion, ref m_MapX, ref m_MapY, ref m_OffX, ref m_OffY);

            return nRegion;
        }
        public void GetMpsPos(ref int X, ref int Y) => scene.NewMap2Mps(region, m_MapX, m_MapY, m_OffX, m_OffY, ref X, ref Y);
    }
}
