using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Helpers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using System;

namespace Photon.JXGameServer.Skills
{
    public class MissleObj : MapObj
    {
        public int m_nLauncherSrcPX, m_nLauncherSrcPY;
        public int nLauncherIdx;

        public byte m_nLevel;

        public int m_nDir, m_nDirIndex;
        public bool m_bCollideEvent, m_bVanishedEvent, m_bStartEvent, m_bFlyEvent, m_bHeelAtParent;
        public int m_nFlyEventTime, m_nParentMissleIndex;

        public MissleTemplate template;

        public eMissleStatus m_eMissleStatus;
        public short m_nCurrentLife, m_nStartLifeTime;
        public eMissleInteruptType m_nInteruptTypeWhenMove;

        public int m_nRefPX, m_nRefPY;
        public int m_nXFactor, m_nYFactor, m_nTempParam1, m_nTempParam2;

        public NPCRELATION m_eRelation;
        public int m_ulNextCalDamageTime;

        public MissleObj(MissleTemplate missle, CharacterObj obj) : base (obj.scene, obj.region)
        {
            nLauncherIdx = obj.id;
            scene.AddMissleObj(this);

            m_MapX = obj.m_MapX; m_MapY = obj.m_MapY;
            m_OffX = obj.m_OffX; m_OffY = obj.m_OffY;

            int x = 0, y = 0;
            obj.GetMpsPos(ref x, ref y);

            m_nLauncherSrcPX = x; m_nLauncherSrcPY = y;

            m_eMissleStatus = eMissleStatus.MS_DoWait;
            template = missle.Clone();
            m_nCurrentLife = 0;

            m_nTempParam1 = m_nTempParam2 = 0;
            m_ulNextCalDamageTime = 0;
        }
        public void HeartBeat()
        {
            var m_nLauncher = scene.FindObj(nLauncherIdx);
            if (m_nLauncher == null)
            {
                DoVanish();
                return;
            }

            if (m_nCurrentLife >= template.m_nLifeTime && m_eMissleStatus != eMissleStatus.MS_DoVanish && m_eMissleStatus != eMissleStatus.MS_DoCollision)
            {
                if (template.m_bAutoExplode)
                {
                    ProcessCollision();
                }
                DoVanish();
            }
            if (m_nCurrentLife == m_nStartLifeTime && m_eMissleStatus != eMissleStatus.MS_DoVanish)
            {
                if (PrePareFly(m_nLauncher))
                    DoFly();
                else
                    DoVanish();
            }
            switch (m_eMissleStatus)
            {
                case eMissleStatus.MS_DoWait:
                    break;
                case eMissleStatus.MS_DoFly:
                    OnFly(m_nLauncher);
                    if (m_bFlyEvent)
                    {
                        if (m_nFlyEventTime == 0)
                        {
                            DoVanish();
                            break;
                        }
                        if ((m_nCurrentLife - m_nStartLifeTime) % m_nFlyEventTime == 0)
                        {

                        }
                    }
                    break;
                case eMissleStatus.MS_DoCollision:
                    OnCollision();
                    break;
                case eMissleStatus.MS_DoVanish:
                    OnVanish();
                    break;
            }
            m_nCurrentLife++;
        }
        CharacterObj CheckNearestCollision(CharacterObj m_nLauncher)
        {
            bool bCollision;
            int nSearchRegion = 0, nRMx = 0, nRMy = 0, nDX, nDY;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!GetOffsetAxis(region.RegionIndex, m_MapX, m_MapY, i, j, ref nSearchRegion, ref nRMx, ref nRMy))
                        continue;

                    var obj = scene.m_Region[nSearchRegion].FindObj(nRMx, nRMy, m_nLauncher, m_eRelation);
                    if (obj != null)
                    {
                        bCollision = true;
                        nDX = m_MapX - obj.m_MapX;
                        nDY = m_MapY - obj.m_MapY;

                        if (Math.Abs(nDX) > 0)
                        {
                            if (nDX < 0)
                            {
                                if (JXHelper.CELLWIDTH - m_OffX + obj.m_OffX > JXHelper.CELLWIDTH)
                                {
                                    bCollision = false;
                                }
                            }
                            else
                            if (nDX > 0)
                            {
                                if (JXHelper.CELLWIDTH + m_OffX - obj.m_OffX > JXHelper.CELLWIDTH)
                                {
                                    bCollision = false;
                                }
                            }
                        }
                        if (Math.Abs(nDY) > 0)
                        {
                            if (nDY < 0)
                            {
                                if (JXHelper.CELLHEIGHT - m_OffY + obj.m_OffY > JXHelper.CELLHEIGHT)
                                {
                                    bCollision = false;
                                }
                            }
                            else
                            if (nDY > 0)
                            {
                                if (JXHelper.CELLHEIGHT + m_OffY - obj.m_OffY > JXHelper.CELLHEIGHT)
                                {
                                    bCollision = false;
                                }
                            }
                        }
                        if (bCollision)
                            return obj;
                    }
                }
            }
            return null;
        }
        int FsCheckCollision(CharacterObj m_nLauncher)
        {
            bool nIsOut;
            if (template.m_nCollideRange == 1)
            {
                var obj = CheckNearestCollision(m_nLauncher);
                if (obj != null)
                {
                    if (template.m_nDamageRange == 1)
                        nIsOut = ProcessCollision(obj.m_MapX, obj.m_MapY, template.m_nDamageRange, m_eRelation, obj);
                    else
                        nIsOut = ProcessCollision();
                }
                else
                {
                    nIsOut = ProcessCollision();
                }
                if (nIsOut)
                {
                    DoCollision();
                    return 1;
                }
            }
            else
            {
                int nSearchRegion = 0, nRMx = 0, nRMy = 0;
                for (int i = -template.m_nCollideRange; i <= template.m_nCollideRange; ++i)
                {
                    for (int j = -template.m_nCollideRange; j <= template.m_nCollideRange; ++j)
                    {
                        if (!GetOffsetAxis(region.RegionIndex, m_MapX, m_MapY, i, j, ref nSearchRegion, ref nRMx, ref nRMy))
                            continue;

                        if (nSearchRegion < 0)
                            continue;

                        var obj = scene.m_Region[nSearchRegion].FindObj(nRMx, nRMy, m_nLauncher, m_eRelation);
                        if (obj != null)
                            nIsOut = ProcessCollision(obj.m_MapX, obj.m_MapY, template.m_nDamageRange, m_eRelation, obj);
                        else
                            nIsOut = ProcessCollision();

                        if (nIsOut)
                        {
                            DoCollision();
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }
        bool ProcessCollision() => ProcessCollision(m_MapX, m_MapY, template.m_nDamageRange, m_eRelation);
        bool ProcessCollision(int nMapX, int nMapY, int nRange, NPCRELATION eRelation, CharacterObj mNpcIdx = null)
        {
            var m_nLauncher = scene.FindObj(nLauncherIdx);
            if (m_nLauncher == null)
                return false;

            if (mNpcIdx != null)
            {
                if (Utils.GetRelation(m_nLauncher, mNpcIdx) == eRelation)
                {
                    ProcessDamage(mNpcIdx);
                }
                return true;
            }
            var obj = GetNearestNpc(nMapX, nMapY, nRange, eRelation, m_nLauncher);
            if (obj != null)
            {
                ProcessDamage(obj);
                return true;
            }
            return false;
        }
        bool CheckBeyondRegion(int nDOffsetX, int nDOffsetY)
        {
            if (nDOffsetX == 0 && nDOffsetY == 0) 
                return false;

            int nOldRegionIndex = region.RegionIndex;
            int nOldMapX = m_MapX;
            int nOldMapY = m_MapY;
            int nOldOffX = m_OffX;
            int nOldOffY = m_OffY;

            int m_RegionIndex = MoveMe(nDOffsetX, nDOffsetY, nOldRegionIndex);

            if (m_RegionIndex <= -1)
            {
                m_MapX = nOldMapX;
                m_MapY = nOldMapY;
                m_OffX = nOldOffX;
                m_OffY = nOldOffY;
                return false;
            }

            region = scene.m_Region[m_RegionIndex];

            return true;
        }
        bool GetOffsetAxis(int nSrcRegionId, int nSrcMapX, int nSrcMapY,
                            int nOffsetMapX, int nOffsetMapY,
                            ref int nDesRegionId, ref int nDesMapX, ref int nDesMapY)
        {
            nDesMapX = nSrcMapX + nOffsetMapX;
            nDesMapY = nSrcMapY + nOffsetMapY;

            nDesRegionId = nSrcRegionId;

            return GetOffset(ref nDesRegionId, ref nDesMapX, ref nDesMapY);
        }
        bool PrePareFly(CharacterObj m_nLauncher)
        {
            if (m_nInteruptTypeWhenMove != eMissleInteruptType.Interupt_None)
            {
                int nPX = 0, nPY = 0;
                m_nLauncher.GetMpsPos(ref nPX, ref nPY);

                if (nPX != m_nLauncherSrcPX || nPY != m_nLauncherSrcPY)
                {
                    return false;
                }
            }
            if (m_bHeelAtParent)
            {
                int nSrcPX = 0, nSrcPY = 0;
                GetMpsPos(ref nSrcPX, ref nSrcPY);

                int nNewPX, nNewPY;
                if (m_nParentMissleIndex != 0)
                {
                    var mis = scene.GetMissleObj(m_nParentMissleIndex);
                    if ((mis == null) || (scene.FindObj(mis.nLauncherIdx) != m_nLauncher))
                        return false;

                    int nParentPX = 0, nParentPY = 0;
                    mis.GetMpsPos(ref nParentPX,ref nParentPY);

                    nNewPX = nSrcPX + (nParentPX - m_nRefPX);
                    nNewPY = nSrcPY + (nParentPY - m_nRefPY);
                }
                else
                {
                    int nParentPX = 0, nParentPY = 0;
                    m_nLauncher.GetMpsPos(ref nParentPX, ref nParentPY);

                    nNewPX = nSrcPX + (nParentPX - m_nRefPX);
                    nNewPY = nSrcPY + (nParentPY - m_nRefPY);
                }

                int nRegion = m_MapX = m_MapY = m_OffX = m_OffY = 0;
                scene.Mps2Map(nNewPX, nNewPY, ref nRegion, ref m_MapX, ref m_MapY, ref m_OffX, ref m_OffY);

                if (nRegion < 0)
                    return false;
            }

            return true;
        }
        void DoFly()
        {
            m_eMissleStatus = eMissleStatus.MS_DoFly;
        }
        void ZAxisMove()
        {
/*
            if (template.m_nZAcceleration != 0)
            {
                m_nHeight += template.m_nHeightSpeed;

                if (m_nHeight < 0)
                    m_nHeight = 0;

                template.m_nHeightSpeed -= template.m_nZAcceleration;

                m_nCurrentMapZ = m_nHeight >> 10;
            }
*/
        }
        void OnFly(CharacterObj m_nLauncher)
        {
            if (m_nInteruptTypeWhenMove != eMissleInteruptType.Interupt_None)
            {
                if (m_nInteruptTypeWhenMove == eMissleInteruptType.Interupt_EndOldMissleLifeWhenMove)
                {
                    int nPX = 0, nPY = 0;
                    m_nLauncher.GetMpsPos(ref nPX, ref nPY);

                    if (nPX != m_nLauncherSrcPX || nPY != m_nLauncherSrcPY)
                    {
                        DoVanish();
                        return;
                    }
                }
            }

            if (template.m_nSpeed > 32)
                template.m_nSpeed = 32;

            ZAxisMove();

            if (FsCheckCollision(m_nLauncher) == -1)
            {
                if (template.m_bAutoExplode)
                {
                    ProcessCollision();
                }
                DoVanish();
                return;
            }

            int nDOffsetX = 0, nDOffsetY = 0;
            switch (template.m_eMoveKind)
            {
                case eMissleMoveKind.MISSLE_MMK_Stand:
                case eMissleMoveKind.MISSLE_MMK_Random:
                case eMissleMoveKind.MISSLE_MMK_Motion:
                case eMissleMoveKind.MISSLE_MMK_SingleLine:
                    break;

                case eMissleMoveKind.MISSLE_MMK_Parabola:
                case eMissleMoveKind.MISSLE_MMK_Line:
                    if (m_nLauncher.m_nPeopleIdx != 0 && template.m_eFollowKind == eMissleFollowKind.MISSLE_MFK_Missle)
                    {
                        if (m_nTempParam1 > 5)
                        {
                            var m_nFollowNpcIdx = scene.FindObj(m_nLauncher.m_nPeopleIdx);
                            if (m_nFollowNpcIdx == null)
                            {
                                DoVanish();
                                return;
                            }

                            int nSrcMpsX = 0, nSrcMpsY = 0, nDesMpsX = 0, nDesMpsY = 0;

                            GetMpsPos(ref nSrcMpsX, ref nSrcMpsY);
                            m_nFollowNpcIdx.GetMpsPos(ref nDesMpsX, ref nDesMpsY);

                            int nDistance = JXHelper.g_GetDistance(nSrcMpsX, nSrcMpsY, nDesMpsX, nDesMpsY);
                            if (nDistance > 20)
                            {
                                int nXFactor = ((nDesMpsX - nSrcMpsX) << 10) / nDistance;
                                int nYFactor = ((nDesMpsY - nSrcMpsY) << 10) / nDistance;
                                int dx = (int)(nXFactor * template.m_nSpeed / 1.3);
                                int dy = (int)(nYFactor * template.m_nSpeed / 1.3);
                                nDOffsetX = dx;
                                nDOffsetY = dy;
                                m_nDirIndex = JXHelper.g_GetDirIndex(nSrcMpsX, nSrcMpsY, nDesMpsX, nDesMpsY);   //�ı䷽�� ���ع���
                                m_nDir = JXHelper.g_DirIndex2Dir(m_nDirIndex);
                            }
                            else
                            {
                                ProcessDamage(m_nFollowNpcIdx);
                                DoVanish();
                                return;
                            }
                        }
                        else
                        {
                            nDOffsetX = template.m_nSpeed * m_nXFactor;
                            nDOffsetY = template.m_nSpeed * m_nYFactor;
                            m_nTempParam1++;
                        }
                    }
                    else
                    {
                        nDOffsetX = template.m_nSpeed * m_nXFactor;
                        nDOffsetY = template.m_nSpeed * m_nYFactor;
                    }
                    break;

                case eMissleMoveKind.MISSLE_MMK_RollBack:
                    if (m_nTempParam1 == 0)
                    {
                        if (m_nTempParam2 <= m_nCurrentLife)
                        {
                            m_nXFactor = -m_nXFactor;
                            m_nYFactor = -m_nYFactor;
                            m_nTempParam1 = 1;
                            m_nDir = m_nDir - JXHelper.MaxMissleDir >> 1;
                            if (m_nDir < 0) m_nDir += JXHelper.MaxMissleDir;
                        }
                    }

                    nDOffsetX = template.m_nSpeed * m_nXFactor;
                    nDOffsetY = template.m_nSpeed * m_nYFactor;
                    break;

                case eMissleMoveKind.MISSLE_MMK_Circle:
                    {

                    }
                    break;

                case eMissleMoveKind.MISSLE_MMK_Helix:
                    {

                    }
                    break;

                case eMissleMoveKind.MISSLE_MMK_Follow:
                    {

                    }
                    break;
            }
            if (CheckBeyondRegion(nDOffsetX, nDOffsetY))
            {

            }
            else
            {
                DoVanish();
            }
        }
        void DoCollision()
        {
            if (m_eMissleStatus == eMissleStatus.MS_DoCollision)
                return;

            if (m_bCollideEvent)
            {

            }
            if (template.m_bCollideVanish)
            {
                DoVanish();
            }
        }
        void OnCollision()
        {

        }
        void DoVanish()
        {
            if (m_eMissleStatus == eMissleStatus.MS_DoVanish) 
                return;

            if (m_bVanishedEvent)
            {

            }

            m_eMissleStatus = eMissleStatus.MS_DoVanish;
        }
        void OnVanish()
        {
            scene.DelMissleObj(id);
        }
        void ProcessDamage(CharacterObj obj)
        {
            obj.ReceiveDamage(scene.FindObj(this.nLauncherIdx), 30);
        }
    }
}
