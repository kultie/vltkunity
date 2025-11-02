using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.JXGameServer.Skills
{
    public class MissleTemplate
    {
        public short m_nLifeTime;
        public int m_nSpeed;
        public int m_nSkillId;
        public int m_nCollideRange;
        public bool m_bCollideVanish;
        public bool m_bCollideFriend;
        public bool m_bCanSlow;
        public bool m_bRangeDamage;
        public int m_nDamageRange;

        public eMissleMoveKind m_eMoveKind;
        public eMissleFollowKind m_eFollowKind;

        public int m_nZAcceleration;
        public int m_nHeightSpeed;

        public int m_nParam1;
        public int m_nParam2;
        public int m_nParam3;

        public bool m_bAutoExplode;
        public int m_ulDamageInterval;

        public MissleTemplate(JX_Table missles, int index) 
        {
            m_nLifeTime = (short)missles.Get<int>((int)Missle_Index.LifeTime, index);
            m_nSpeed = missles.Get<int>((int)Missle_Index.Speed, index);
            m_nSkillId = missles.Get<int>((int)Missle_Index.ResponseSkill, index);

            m_nCollideRange = missles.Get<int>((int)Missle_Index.CollidRange, index);
            m_bCollideVanish = missles.Get<bool>((int)Missle_Index.ColVanish, index);
            m_bCollideFriend = missles.Get<bool>((int)Missle_Index.CanColFriend, index);
            m_bCanSlow = missles.Get<bool>((int)Missle_Index.CanSlow, index);
            m_bRangeDamage = missles.Get<bool>((int)Missle_Index.IsRangeDmg, index);
            m_nDamageRange = missles.Get<int>((int)Missle_Index.DmgRange, index);

            m_eMoveKind = (eMissleMoveKind)(byte)missles.Get<int>((int)Missle_Index.MoveKind, index);
            m_eFollowKind = (eMissleFollowKind)(byte)missles.Get<int>((int)Missle_Index.FollowKind, index);

            m_nZAcceleration = missles.Get<int>((int)Missle_Index.Zacc, index);
            m_nHeightSpeed = missles.Get<int>((int)Missle_Index.Zspeed, index);

            m_nParam1 = missles.Get<int>((int)Missle_Index.Param1, index);
            m_nParam2 = missles.Get<int>((int)Missle_Index.Param2, index);
            m_nParam3 = missles.Get<int>((int)Missle_Index.Param3, index);

            m_bAutoExplode = missles.Get<bool>((int)Missle_Index.AutoExplode, index);
            m_ulDamageInterval = missles.Get<int>((int)Missle_Index.DmgInterval, index);
        }
        public MissleTemplate Clone()
        {
            return (MissleTemplate)this.MemberwiseClone();
        }
    }
}
