
namespace game.resource.settings.skill.missile
{
    public class Fly : skill.missile.Vanish
    {
        private void ZAxisMove()
        {
            if (this.missileSetting.m_nZAcceleration != 0)
            {
                m_nHeight += m_nHeightSpeed;
                if (m_nHeight < 0) m_nHeight = 0;
                m_nHeightSpeed -= this.missileSetting.m_nZAcceleration;
                m_nCurrentMapZ = m_nHeight >> 10;
            }
        }

        protected bool PrePareFly()
        {
            if (this.missileSetting.m_eMoveKind == Defination.MissleMoveKind.MISSLE_MMK_RollBack)
            {
                m_nTempParam2 = m_nStartLifeTime + (m_nLifeTime - m_nStartLifeTime) / 2;
            }

            //if (this.skillSetting.m_nInteruptTypeWhenMove != 0)
            //{
            //    resource.map.Position launcherPosition = this.skillParam.launcher.GetMapPosition();
            //    int nPX = launcherPosition.left;
            //    int nPY = launcherPosition.top;

            //    if (nPX != m_nLauncherSrcPX || nPY != m_nLauncherSrcPY)
            //    {
            //        return false;
            //    }
            //}

            //if (this.skillSetting.m_bHeelAtParent != 0)
            //{
            //    int nNewPX = 0;
            //    int nNewPY = 0;

            //    if (this.m_nParentMissleIndex != null)
            //    {
            //        resource.map.Position parentMapPosition = this.m_nParentMissleIndex.texture.GetMapPosition();
            //        resource.map.Position thisMapPosition = this.texture.GetMapPosition();

            //        int nParentPX = parentMapPosition.left, nParentPY = parentMapPosition.top;
            //        int nSrcPX = thisMapPosition.left, nSrcPY = thisMapPosition.top;

            //        nNewPX = nSrcPX + (nParentPX - m_nRefPX);
            //        nNewPY = nSrcPY + (nParentPY - m_nRefPY);
            //    }
            //    else
            //    {
            //        if(this.skillParam.HasLauncher() == false)
            //        {
            //            return false;
            //        }

            //        resource.map.Position parentMapPos = this.skillParam.launcher.GetMapPosition();
            //        resource.map.Position thisMapPosition = this.texture.GetMapPosition();

            //        int nParentPX = parentMapPos.left, nParentPY = parentMapPos.top;
            //        int nSrcPX = thisMapPosition.left, nSrcPY = thisMapPosition.top;

            //        nNewPX = nSrcPX + (nParentPX - m_nRefPX);
            //        nNewPY = nSrcPY + (nParentPY - m_nRefPY);
            //    }

            //    int refNull1 = 0;
            //    int refNull2 = 0;
            //    int refNull3 = 0;

            //    skill.Static.SubWorld_Mps2Map(nNewPX, nNewPY, ref refNull1, ref refNull2, ref refNull3, ref this.m_nXOffset, ref this.m_nYOffset);
            //}

            return true;
        }

        protected void DoFly()
        {
            if (this.m_eMissleStatus == Defination.MissleStatus.MS_DoFly) return;
            this.m_eMissleStatus = Defination.MissleStatus.MS_DoFly;
        }

        protected void OnFly()
        {
            if (this.skillSetting.m_nInteruptTypeWhenMove != 0)
            {
                if (this.skillSetting.m_nInteruptTypeWhenMove == (int)skill.Defination.Interupt.Interupt_EndOldMissleLifeWhenMove)
                {
                    resource.map.Position launcherPosition = this.skillParam.launcher.GetMapPosition();
                    int nPX = launcherPosition.left, nPY = launcherPosition.top;

                    if (nPX != m_nLauncherSrcPX || nPY != m_nLauncherSrcPY)
                    {

                        //resource.map.Position thisMapPos = this.texture.GetMapPosition();
                        //int nSrcX2 = thisMapPos.left;
                        //int nSrcY2 = thisMapPos.top;
                        //CreateSpecialEffect(MS_DoVanish, nSrcX2, nSrcY2, m_nCurrentMapZ);

                        //if(this.skillSetting.m_nId == 301)
                        //{
                        //    UnityEngine.Debug.Log("vanish 1");
                        //}

                        //UnityEngine.Debug.Log("vanish 3");
                        DoVanish();
                        return;
                    }
                }
            }

            //if (TestBarrier())
            //{
            //    resource.map.Position thisMapPos = this.texture.GetMapPosition();
            //    int nSrcX3 = thisMapPos.left;
            //    int nSrcY3 = thisMapPos.top;
            //    CreateSpecialEffect(MS_DoVanish, nSrcX3, nSrcY3, m_nCurrentMapZ);

            //    DoVanish();
            //    return;
            //}

            int nDOffsetX = 0;
            int nDOffsetY = 0;

            ZAxisMove();

            //UnityEngine.Debug.Log("this.missileSetting.m_eMoveKind: " + this.missileSetting.m_eMoveKind);

            switch (this.missileSetting.m_eMoveKind)
            {
                case Defination.MissleMoveKind.MISSLE_MMK_Stand:
                    {

                    }
                    break;
                case Defination.MissleMoveKind.MISSLE_MMK_Parabola:
                case Defination.MissleMoveKind.MISSLE_MMK_Line:
                    {
                        if(this.missileSetting.m_eFollowKind == Defination.MissleFollowKind.MISSLE_MFK_Missle 
                            && this.skillParam.target.HaveData())
                        {
                            if (m_nTempParam1 > 5)
                            {
                                resource.map.Position missilePosition = this.texture.GetMapPosition();
                                resource.map.Position targetPosition = this.skillParam.target.GetMapPosition();

                                int nDistance = 0;
                                int nSrcMpsX = missilePosition.left;
                                int nSrcMpsY = missilePosition.top;
                                int nDesMpsX = targetPosition.left;
                                int nDesMpsY = targetPosition.top;

                                nDistance = skill.Static.GetDistance(nSrcMpsX, nSrcMpsY, nDesMpsX, nDesMpsY);
                                if (nDistance > 20)
                                {
                                    int nXFactor = ((nDesMpsX - nSrcMpsX) << 10) / nDistance;
                                    int nYFactor = ((nDesMpsY - nSrcMpsY) << 10) / nDistance;
                                    int dx = (int)(nXFactor * (this.missileSetting.m_nSpeed / 1.3f));
                                    int dy = (int)(nYFactor * (this.missileSetting.m_nSpeed / 1.3f));
                                    nDOffsetX = dx;
                                    nDOffsetY = dy;
                                    m_nDir = skill.Static.g_GetDirIndex(nSrcMpsX, nSrcMpsY, nDesMpsX, nDesMpsY);
                                }
                                else
                                {
                                    //ProcessDamage(m_nFollowNpcIdx);
                                    //UnityEngine.Debug.Log("vanish 4");
                                    //UnityEngine.Debug.Log("ProcessDamage");
                                    DoVanish();
                                    return;
                                }
                            }
                            else
                            {
                                nDOffsetX = (this.missileSetting.m_nSpeed * m_nXFactor);
                                nDOffsetY = (this.missileSetting.m_nSpeed * m_nYFactor);
                                m_nTempParam1++;
                            }
                        }
                        else
                        {
                            nDOffsetX = (this.missileSetting.m_nSpeed * m_nXFactor);
                            nDOffsetY = (this.missileSetting.m_nSpeed * m_nYFactor);
                        }
                    }
                    break;
                case Defination.MissleMoveKind.MISSLE_MMK_RollBack:
                    {
                        if (this.m_nTempParam1 == 0)
                        {
                            if (m_nTempParam2 <= m_nCurrentLife)
                            {
                                m_nXFactor = -m_nXFactor;
                                m_nYFactor = -m_nYFactor;
                                m_nTempParam1 = 1;
                                m_nDir = m_nDir - skill.Defination.MaxMissleDir / 2;
                                if (m_nDir < 0) m_nDir += skill.Defination.MaxMissleDir;
                            }
                        }

                        nDOffsetX = (this.missileSetting.m_nSpeed * m_nXFactor);
                        nDOffsetY = (this.missileSetting.m_nSpeed * m_nYFactor);
                    }
                    break;
                case Defination.MissleMoveKind.MISSLE_MMK_Random:
                    {

                    }
                    break;
                case Defination.MissleMoveKind.MISSLE_MMK_Circle:
                    {
                        int nPreAngle = m_nAngle - 1;
                        if (nPreAngle < 0) nPreAngle = skill.Defination.MaxMissleDir - 1;
                        m_nDir = m_nAngle + (skill.Defination.MaxMissleDir / 4);
                        if (m_nDir >= skill.Defination.MaxMissleDir) m_nDir = m_nDir - skill.Defination.MaxMissleDir;
                        int dx = (this.missileSetting.m_nSpeed + 50) * (skill.Static.g_DirCos(m_nAngle, skill.Defination.MaxMissleDir) - skill.Static.g_DirCos(nPreAngle, skill.Defination.MaxMissleDir));
                        int dy = (this.missileSetting.m_nSpeed + 50) * (skill.Static.g_DirSin(m_nAngle, skill.Defination.MaxMissleDir) - skill.Static.g_DirSin(nPreAngle, skill.Defination.MaxMissleDir));

                        if (this.missileSetting.m_nParam2 != 0)
                        {
                            nDOffsetX = dx;
                            nDOffsetY = dy;
                        }
                        else
                        {
                            resource.map.Position launcherPosition = this.skillParam.launcher.GetMapPosition();

                            this.m_nRefPX = launcherPosition.left;
                            this.m_nRefPY = launcherPosition.top;

                            nDOffsetX = dx;
                            nDOffsetY = dy;
                        }

                        if (this.missileSetting.m_nParam1 != 0)
                        {
                            m_nAngle++;
                            if (m_nAngle >= skill.Defination.MaxMissleDir)
                                m_nAngle = 0;
                        }
                        else
                        {
                            m_nAngle--;
                            if (m_nAngle < 0)
                                m_nAngle = skill.Defination.MaxMissleDir - 1;
                        }

                    }
                    break;

                case Defination.MissleMoveKind.MISSLE_MMK_Helix:
                    {
                        int nPreAngle = m_nAngle - 1;
                        if (nPreAngle < 0)
                        {
                            nPreAngle = skill.Defination.MaxMissleDir - 1;
                        }
                        m_nDir = m_nAngle + (skill.Defination.MaxMissleDir >> 2);
                        if (m_nDir >= skill.Defination.MaxMissleDir) m_nDir = m_nDir - skill.Defination.MaxMissleDir;

                        int dx = (this.missileSetting.m_nSpeed + m_nCurrentLife + 50) * (skill.Static.g_DirCos(m_nAngle, skill.Defination.MaxMissleDir) - skill.Static.g_DirCos(nPreAngle, skill.Defination.MaxMissleDir));
                        int dy = (this.missileSetting.m_nSpeed + m_nCurrentLife + 50) * (skill.Static.g_DirSin(m_nAngle, skill.Defination.MaxMissleDir) - skill.Static.g_DirSin(nPreAngle, skill.Defination.MaxMissleDir));

                        if (this.missileSetting.m_nParam2 != 0) 
                        {
                            nDOffsetX = dx;
                            nDOffsetY = dy;
                        }
                        else         
                        {
                            resource.map.Position launcherPosition = this.skillParam.launcher.GetMapPosition();

                            this.m_nRefPX = launcherPosition.left + 50 * (skill.Static.g_DirCos(m_nAngle, skill.Defination.MaxMissleDir) + skill.Static.g_DirCos(nPreAngle, skill.Defination.MaxMissleDir));
                            this.m_nRefPY = launcherPosition.top + 50 * (skill.Static.g_DirSin(m_nAngle, skill.Defination.MaxMissleDir) + skill.Static.g_DirSin(nPreAngle, skill.Defination.MaxMissleDir));

                            nDOffsetX = dx;
                            nDOffsetY = dy;
                        }

                        if (this.missileSetting.m_nParam1 != 0)
                        {
                            m_nAngle++;
                            if (m_nAngle >= skill.Defination.MaxMissleDir)
                                m_nAngle = 0;
                        }
                        else
                        {
                            m_nAngle--;
                            if (m_nAngle < 0)
                                m_nAngle = skill.Defination.MaxMissleDir - 1;
                        }
                    }
                    break;
                case Defination.MissleMoveKind.MISSLE_MMK_Follow:
                    {
                        if (this.skillParam.target.HaveData())
                        {
                            if (m_nTempParam1 > 5)
                            {
                                resource.map.Position missilePosition = this.texture.GetMapPosition();
                                resource.map.Position targetPosition = this.skillParam.target.GetMapPosition();

                                int nDistance = 0;
                                int nSrcMpsX = missilePosition.left;
                                int nSrcMpsY = missilePosition.top;
                                int nDesMpsX = targetPosition.left;
                                int nDesMpsY = targetPosition.top;

                                //UnityEngine.Debug.Log("nDesMpsX: " + nDesMpsX + ", nDesMpsY: " + nDesMpsY);

                                nDistance = skill.Static.GetDistance(nSrcMpsX, nSrcMpsY, nDesMpsX, nDesMpsY);
                                if (nDistance > 20)
                                {
                                    int nXFactor = ((nDesMpsX - nSrcMpsX) << 10) / nDistance;
                                    int nYFactor = ((nDesMpsY - nSrcMpsY) << 10) / nDistance;
                                    int dx = (int)(nXFactor * (this.missileSetting.m_nSpeed / 1.3f));
                                    int dy = (int)(nYFactor * (this.missileSetting.m_nSpeed / 1.3f));
                                    nDOffsetX = dx;
                                    nDOffsetY = dy;
                                    m_nDirIndex = skill.Static.g_GetDirIndex(nSrcMpsX, nSrcMpsY, nDesMpsX, nDesMpsY);
                                    m_nDir = skill.Static.g_DirIndex2Dir(m_nDirIndex, skill.Defination.MaxMissleDir);
                                }
                                else
                                {
                                    //ProcessDamage(m_nFollowNpcIdx, m_nRate);  //¹¥»÷¼¼ÄÜÉËº¦¼ÆËã
                                    //UnityEngine.Debug.Log("vanish 5");
                                    //UnityEngine.Debug.Log("ProcessDamage");
                                    DoVanish();
                                    return;
                                }
                            }
                            else
                            {
                                nDOffsetX = (this.missileSetting.m_nSpeed * m_nXFactor);
                                nDOffsetY = (this.missileSetting.m_nSpeed * m_nYFactor);
                                m_nTempParam1++;
                            }
                        }
                        else
                        {
                            nDOffsetX = (this.missileSetting.m_nSpeed * m_nXFactor);
                            nDOffsetY = (this.missileSetting.m_nSpeed * m_nYFactor);
                        }

                        break;
                    }

                case Defination.MissleMoveKind.MISSLE_MMK_Motion:
                    {

                    }
                    break;

                case Defination.MissleMoveKind.MISSLE_MMK_SingleLine:
                    {
                        int x = m_nXOffset;
                        int y = m_nYOffset;
                        int dx = (this.missileSetting.m_nSpeed * m_nXFactor);
                        int dy = (this.missileSetting.m_nSpeed * m_nYFactor);
                        nDOffsetX = dx;
                        nDOffsetY = dy;
                    }
                    break;
                default:
                    break;
            }

            this.m_nXOffset += (nDOffsetX >> 10);
            this.m_nYOffset += (nDOffsetY >> 10);

            if (CheckBeyondRegion(nDOffsetX, nDOffsetY))
            {
                if (CheckCollision() == -1)
                {
                    //UnityEngine.Debug.Log("vanish");

                    if (this.missileSetting.m_bAutoExplode != 0)
                    {
                        ProcessCollision();
                    }

                    //resource.map.Position thisPosition = this.texture.GetMapPosition();
                    //int nSrcX4 = thisPosition.left;
                    //int nSrcY4 = thisPosition.top;
                    //CreateSpecialEffect(MS_DoVanish, nSrcX4, nSrcY4, m_nCurrentMapZ);

                    //if (this.skillSetting.m_nId == 301)
                    //{
                    //    UnityEngine.Debug.Log("vanish 2");
                    //}

                    //UnityEngine.Debug.Log("vanish 6, cur: " + this.m_nCurrentLife + ", end: " + this.m_nLifeTime);
                    DoVanish();
                    return;
                }
                //else
                //{
                //    UnityEngine.Debug.Log("mapZ: " + this.m_nCurrentMapZ);
                //}
            }
            else
            {
                //if (this.skillSetting.m_nId == 301)
                //{
                //    UnityEngine.Debug.Log("vanish 3 --> cur: " + this.m_nCurrentLife + " == " + this.m_nStartLifeTime);
                //}

                //UnityEngine.Debug.Log("vanish 7");
                DoVanish();
            }
        }

        private bool CheckBeyondRegion(int nDOffsetX, int nDOffsetY)
        {
            //this.m_nXOffset = m_nXOffset + (nDOffsetX >> 10);
            //this.m_nYOffset = m_nYOffset + (nDOffsetY >> 10);

            return true;
        }
    }
}
