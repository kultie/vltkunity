
namespace game.resource.settings.skill.missile
{
    public class Active : skill.missile.Painting
    {
        public bool Activate()
        {
            //System.Diagnostics.Stopwatch performance = System.Diagnostics.Stopwatch.StartNew();

            //UnityEngine.Debug.Log(this.skillSetting.m_nId + ", " + this.m_nCurrentLife + " == " + this.m_nStartLifeTime + ", status: " + this.m_eMissleStatus);

            if (this.isActive == false
                && this.m_nCurrentLife <= this.m_nStartLifeTime)
            {   // đợi map.textures.thread kích hoạt missile
                return true;
            }
            else if(this.m_nCurrentLife < this.m_nStartLifeTime)
            {   // skill waitting time...
                this.m_nCurrentLife++;
                return true;
            }
            else if (this.m_nCurrentLife >= this.m_nLifeTime
                && this.m_eMissleStatus != skill.Defination.MissleStatus.MS_DoVanish
                && this.m_eMissleStatus != skill.Defination.MissleStatus.MS_DoCollision)
            {
                if (this.missileSetting.m_bAutoExplode != 0)
                {
                    this.ProcessCollision();
                }

                //UnityEngine.Debug.Log("start vanish status at: " + this.m_nCurrentLife);
                //UnityEngine.Debug.Log("vanish 1");
                this.DoVanish();
            }
            else if (this.m_nCurrentLife >= this.m_nLifeTime)
            {
                if(this.vanishEffect.m_nEndTime <= this.m_nCurrentLife)
                {
                    this.isActive = false;
                    this.isPainting = false;
                    return false;
                }
            }

            if (this.m_nCurrentLife == this.m_nStartLifeTime && this.m_eMissleStatus != skill.Defination.MissleStatus.MS_DoVanish)
            {
                if (this.PrePareFly())
                {
                    this.DoFly();
                }
                else
                {
                    //UnityEngine.Debug.Log("vanish 2");
                    this.DoVanish();
                    //UnityEngine.Debug.Log(this.skillSetting.m_nId + ", " + this.m_nCurrentLife + " == " + this.m_nStartLifeTime);
                }
            }

            switch (this.m_eMissleStatus)
            {
                case Defination.MissleStatus.MS_DoWait:
                    {
                        this.OnWait();
                    }
                    break;
                case Defination.MissleStatus.MS_DoFly:
                    {
                        this.OnFly();
                        if (this.skillSetting.m_bFlyingEvent != 0)
                        {
                            if ((this.m_nCurrentLife - this.m_nStartLifeTime) % this.skillSetting.m_nFlyEventTime == 0)
                            {
                                //UnityEngine.Debug.Log("cast skill flying event!!!");
                                this.FlyEvent();
                            }
                        }
                    }
                    break;
                case Defination.MissleStatus.MS_DoCollision:
                    {
                        this.OnCollision();
                    }
                    break;
                case Defination.MissleStatus.MS_DoVanish:
                    {
                        //UnityEngine.Debug.Log("on Vanish");
                        this.OnVanish();
                    }
                    break;
            }

            this.PreParePaint();
            this.isPainting = true;
            this.m_nCurrentLife++;

            //UnityEngine.Debug.Log(this.skillSetting.m_nId + " --> activated!!!");
            //UnityEngine.Debug.Log(this.skillSetting.m_nId + " --> m_eMissleStatus: " + m_eMissleStatus + ", " + this.m_nCurrentLife + " == " + this.m_nStartLifeTime);

            //performance.Stop();
            //UnityEngine.Debug.Log("active Perfomance: " + performance.ElapsedMilliseconds + " milliseconds");

            return true;
        }
    }
}
