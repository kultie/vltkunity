
namespace game.resource.settings.skill.missile
{
    public class Data
    {
        public class VanishEffect
        {
            public int m_nBeginTime;
            public int m_nEndTime;
            public int m_nCurDir;
        }

        ////////////////////////////////////////////////////////////////////////////////

        protected skill.Missile self;
        protected resource.Map map;
        protected skill.SkillSetting skillSetting;
        protected skill.MissileSetting missileSetting;
        protected skill.Params.TOrdinSkillParam skillParam;

        ////////////////////////////////////////////////////////////////////////////////

        protected skill.Texture texture;

        ////////////////////////////////////////////////////////////////////////////////

        public int m_nChildSkillId;
        public int m_nDir;
        public int m_nDirIndex;
        public int m_nSkillId;
        public int m_nStartLifeTime;
        public int m_nLifeTime;
        public int m_nRefPX;
        public int m_nRefPY;
        public int m_nXFactor;
        public int m_nYFactor;
        public int m_nCurrentLife;
        public int m_nXOffset;
        public int m_nYOffset;
        public skill.Defination.MissleStatus m_eMissleStatus;
        public skill.Missile m_nParentMissleIndex;

        ////////////////////////////////////////////////////////////////////////////////

        public int m_nHeight;
        public int m_nHeightSpeed;
        public int m_nCurrentMapZ;
        public int m_nLauncherSrcPX;
        public int m_nLauncherSrcPY;

        ////////////////////////////////////////////////////////////////////////////////

        public int m_nTempParam1;
        public int m_nTempParam2;
        public int m_nAngle;

        public missile.Data.VanishEffect vanishEffect;

        ////////////////////////////////////////////////////////////////////////////////

        public bool isPainting;
        public bool isActive;

        ////////////////////////////////////////////////////////////////////////////////

        public skill.Params.Owner GetLauncher() => this.skillParam.launcher;
    }
}
