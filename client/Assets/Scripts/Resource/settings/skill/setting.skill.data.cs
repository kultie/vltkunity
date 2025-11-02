
using System.Collections.Generic;

namespace game.resource.settings.skill
{
    public class SkillSettingData
    {
        public class KMagicAttrib
        {
            public int nAttribType;
            public int[] nValue;

            public KMagicAttrib() 
            {
                this.nValue = new int[3];
                nValue[0] = nValue[1] = nValue[2] = nAttribType = 0; 
            }

            public KMagicAttrib(int nAttribType)
            {
                this.nAttribType = nAttribType;
                this.nValue = new int[3];
                nValue[0] = nValue[1] = nValue[2] = nAttribType = 0;
            }

            public KMagicAttrib(int nAttribType, int nValue0)
            {
                this.nAttribType = nAttribType;
                this.nValue = new int[3];
                this.nValue[0] = nValue0;
                this.nValue[1] = this.nValue[2] = 0;
            }

            public KMagicAttrib(int nAttribType, int nValue0, int nValue1, int nValue2)
            {
                this.nAttribType = nAttribType;
                this.nValue = new int[3];
                this.nValue[0] = nValue0;
                this.nValue[1] = nValue1;
                this.nValue[2] = nValue2;
            }
        };

        ////////////////////////////////////////////////////////////////////////////////

        public int skillLevel;
        public int m_nId;
        public string m_szName;
        public string m_property;
        public int m_nAttrib;
        public int m_usReqLevel;
        public int m_maxLevel;
        public int m_nEquiptLimited;
        public int m_nHorseLimited;
        public int m_bDoHurt;
        public int m_nChildSkillNum;
        public skill.Defination.MisslesForm m_eMisslesForm;
        public int m_nCharClass;
        public skill.Defination.SKillStyle m_eSkillStyle;
        public skill.Defination.CLIENTACTION m_nCharActionId;
        public int m_bIsAura;
        public int m_bUseAttackRate;
        public int m_bTargetOnly;
        public int m_bTargetEnemy;
        public int m_bTargetAlly;
        public int m_bTargetObj;
        public int m_bTargetOther;
        public int m_bTargetNoNpc;
        public int m_eRelation;
        public int m_bBaseSkill;
        public int m_bByMissle;
        public int m_nChildSkillId;
        public int m_bFlyingEvent;
        public int m_bStartEvent;
        public int m_bCollideEvent;
        public int m_bVanishedEvent;
        public int m_nFlySkillId;
        public int m_nStartSkillId;
        public int m_nVanishedSkillId;
        public int m_nCollideSkillId;
        public skill.Defination.NPCATTRIB m_nSkillCostType;
        public int m_nCost;
        public int m_nMinTimePerCast;
        public int m_nMinTimePerCastOnHorse;
        public int m_nValue1;
        public int m_nValue2;
        public int m_nChildSkillLevel;
        public int m_nEventSkillLevel;
        public int m_bIsMelee;
        public int m_nFlyEventTime;
        public int m_nShowEvent;
        public skill.Defination.MisslesGenerateStyle m_eMisslesGenerateStyle;
        public int m_nMisslesGenerateData;
        public int m_nMaxShadowNum;
        public int m_nAttackRadius;
        public int m_nWaitTime;
        public int m_bClientSend;
        public int m_bTargetSelf;
        public int m_nInteruptTypeWhenMove;
        public int m_bHeelAtParent;
        public int m_nIsExpSkill;
        public int m_nSeries;
        public int m_nShowAddition;
        public int m_bIsPhysical;
        public int m_nStateSpecialId;
        public string m_szSkillDesc;
        public int m_bNeedShadow;
        public string m_szSkillIcon;
        public skill.Defination.SkillLRInfo m_eLRSkillInfo;
        public string m_szPreCastEffectFile;
        public string m_szManPreCastSoundFile;
        public string m_szFMPreCastSoundFile;

        ////////////////////////////////////////////////////////////////////////////////
        
        public List<KMagicAttrib> m_MissleAttribs;
        public List<KMagicAttrib> m_DamageAttribs; 
        public List<KMagicAttrib> m_StateAttribs;
        public List<KMagicAttrib> m_ImmediateAttribs;

        ////////////////////////////////////////////////////////////////////////////////

        public string description;

        ////////////////////////////////////////////////////////////////////////////////
        
        protected void InitSettingData()
        {
            this.m_MissleAttribs = new List<KMagicAttrib>();
            this.m_DamageAttribs = new List<KMagicAttrib>();
            this.m_StateAttribs = new List<KMagicAttrib>();
            this.m_ImmediateAttribs = new List<KMagicAttrib>();
        }
    }
}
