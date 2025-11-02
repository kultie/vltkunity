using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Maps;
using Photon.JXGameServer.Modulers;
using Photon.JXGameServer.Skills;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using System.Collections.Generic;

namespace Photon.JXGameServer.Entitys
{
    public class CharacterObj : MapObj, ICharacterObj
    {
        public int Id { get { return id; } }
        public bool IsPlayer
        {
            get { return Kind == NPCKIND.kind_player; }
        }
        public virtual NPCKIND Kind
        {
            get { return NPCKIND.kind_num; }
        }
        public virtual NPCSERIES Series
        {
            get { return NPCSERIES.series_num; }
            set { }
        }
        public virtual NPCCAMP Cam
        {
            get { return NPCCAMP.camp_num; }
            set { }
        }
        public virtual NPCCAMP CurrentCamp
        {
            get { return NPCCAMP.camp_num; }
            set { }
        }
        public virtual bool FightMode
        {
            get { return false; }
            set { }
        }
        public virtual int HPMax
        {
            get { return 0; }
            set { }
        }
        public virtual int HPCur
        {
            get { return 0; }
            set { }
        }
        public virtual int MPMax
        {
            get { return 100; }
            set { }
        }
        public virtual int MPCur
        {
            get { return 100; }
            set { }
        }
        public virtual int SPMax
        {
            get { return 0; }
            set { }
        }
        public virtual int SPCur
        {
            get { return 0; }
            set { }
        }
        public virtual byte Level
        {
            get { return 0; }
            set { }
        }
        public virtual string Name
        {
            get { return string.Empty; }
            set { }
        }
        public virtual string Title
        {
            get { return string.Empty; }
            set { }
        }
        public virtual byte WalkSpeed
        {
            get { return 0; }
            set { }
        }
        public virtual byte RunSpeed
        {
            get { return 0; }
            set { }
        }
        public virtual int AttackSpeed
        {
            get { return 0; }
            set { }
        }
        public virtual int CastSpeed
        {
            get { return 0; }
            set { }
        }
        public virtual int HitRecover
        {
            get { return 0; }
            set { }
        }
        public virtual EnumPK GetNormalPKState() 
        {
            return EnumPK.ENMITY_STATE_CLOSE;
        }
        public virtual EnumPK GetEnmityPKState()
        {
            return EnumPK.ENMITY_STATE_CLOSE;
        }
        public virtual int GetEnmityPKAim() { return 0; }
        public virtual int GetExercisePKAim() { return 0; }

        public virtual string MasterName
        {
            get { return string.Empty; } 
            set { } 
        }
        public virtual ICharacterObj MasterObj 
        {
            get { return null; }
            set { } 
        }
        protected NPCCMD m_Doing = NPCCMD.do_stand;
        public NPCCMD Doing
        {
            get { return m_Doing; }
            set { m_Doing = value; }
        }

        KState m_Hide;
        public KState Hide
        {
            get { return m_Hide; }
        }
        KState m_RandMove;
        public KState RandMove
        {
            get { return m_RandMove; }
        }
        KState m_PhysicsArmor;
        KState m_ColdArmor;
        KState m_LightArmor;
        KState m_PoisonArmor;
        KState m_FireArmor;

        protected bool m_ProcessAI;

        public int m_ActiveAuraID,m_ActiveSkillID;
        public byte m_ActiveSkillLevel;
        public short m_CurrentAttackRadius;

        public int m_CurrentAllResist;
        public int m_CurrentFireResist;
        public int m_CurrentColdResist;
        public int m_CurrentPoisonResist;
        public int m_CurrentLightResist;
        public int m_CurrentPhysicsResist;

        public int m_CurrentExperience;
        public ushort m_CurrentLifeReplenish = 0, m_CurrentManaReplenish = 0, m_CurrentStaminaGain = 0;
        public ushort m_CurrentLifeReplenish_p = 0, m_CurrentManaReplenish_p = 0;
        
        public ushort m_CurrentAttackRating, m_CurrentDefend;
        public JX_KMagicAttrib m_PhysicsDamage;

        public byte m_CurrentWalkSpeed, m_CurrentRunSpeed, m_CurrentAttackSpeed, m_CurrentCastSpeed;

        public SkillList skillList;

        public NpcFindPath m_PathFinder;

        public ExitGames.Client.Photon.Hashtable backups;

        public int m_DestX;
        public int m_DestY;
        public bool m_nIsOver = true;

        protected uint m_LoopFrames = 0;
        public NPCCOMMAND m_Command = new NPCCOMMAND();

        public CharacterObj(SceneObj scene, RegionObj region) : base(scene, region) 
        {
            m_Command.CmdKind = NPCCMD.do_none;

            m_Hide = new KState();
            m_Hide.Init();

            m_RandMove = new KState();
            m_RandMove.Init();

            m_PhysicsArmor = new KState();
            m_PhysicsArmor.Init();

            m_ColdArmor = new KState();
            m_ColdArmor.Init();

            m_LightArmor = new KState();
            m_LightArmor.Init();

            m_PoisonArmor = new KState();
            m_PoisonArmor.Init();

            m_FireArmor = new KState();
            m_FireArmor.Init();
        }
        void ClearNormalState()
        {
            m_Hide.Clear();
            m_RandMove.Clear();
            
            m_PhysicsArmor.Clear();
            m_ColdArmor.Clear();
            m_LightArmor.Clear();
            m_PoisonArmor.Clear();
            m_FireArmor.Clear();
        }
        List<KStateNode> m_StateSkillList = new List<KStateNode>();
        void ClearStateSkillEffect()
        {
            foreach (var s in m_StateSkillList)
            {
                if (s.m_LeftTime == -1)
                    continue;

                foreach (var t in s.m_State)
                {
                    if (t.nAttribType != 0)
                    {
                        KNpcAttribModify.Me.ModifyAttrib(this,t);
                    }
                }
            }
            m_StateSkillList.Clear();
        }
        byte nTotalFrame, nCurrentFrame;
        void DeathPunish(DEATH_MODE nMode, int nBelongPlayer, CharacterObj nLastDamageIdx)
        {
            if (IsPlayer)
            {
                ((PlayerObj)this).ClientPeer.SendEvent(new EventData
                {
                    Code = (byte)OperationCode.DoDie,
                    Parameters = new Dictionary<byte, object>(),
                }, ((PlayerObj)this).sendParameters);
            }
            else
            if (nBelongPlayer > 0)
            {
                var m_pDropRate = SceneModule.Me.GetItemDrop(((NpcObj)this).szDropFile);
                if (m_pDropRate != null)
                    m_pDropRate.CalcDrop((NpcObj)this, nBelongPlayer);
            }
        }
        void DoDeath(DEATH_MODE nMode, CharacterObj nLastDamageIdx)
        {
            if (m_Doing == NPCCMD.do_death)
                return;

            int nPlayer = 0;
            if (IsPlayer)
            {
                if (!FightMode)
                {
                    HPCur = 1;
                    return;
                }
                // remove mark

                ((PlayerObj)this).TeamLeave();
            }
            else
            {
                nPlayer = ((NpcObj)this).m_cDeathCalcExp.CalcExp((NpcObj)this);
            }

            DeathPunish(nMode, nPlayer , nLastDamageIdx);

            m_Doing = NPCCMD.do_death;
            m_ProcessAI = false;

            nTotalFrame = 15;
            nCurrentFrame = 0;
            m_nPeopleIdx = nLastDamageIdx.id;
        }
        DEATH_MODE DeathCalcPKValue(CharacterObj nAttacker)
        {

            return DEATH_MODE.DEATH_MODE_NPC_KILL;
        }
        int CalcDamage(CharacterObj nAttacker, short nMin, short nMax, DAMAGE_TYPE nType)
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
            {
                nAttacker.m_nPeopleIdx = 0;
                return 0;
            }

            if (nMin < 0)
                nMin = 0;

            if (nMax < 0)
                nMax = 0;

            if (nMin + nMax <= 0)
                return 0;

            short nDamageRange = (short)(nMax - nMin);
            if (nDamageRange <= 0)
                return 0;

            short nDamage = (short)KRandom.g_Random(nDamageRange);
            if (nDamage <= 0)
                return 0;

            switch (nType)
            {
                case DAMAGE_TYPE.damage_physics:
                    m_PhysicsArmor.nValue[0] -= nDamage;

                    if (m_PhysicsArmor.nValue[0] < 0)
                    {
                        nDamage = (short)-m_PhysicsArmor.nValue[0];
                        m_PhysicsArmor.nValue[0] = 0;
                        m_PhysicsArmor.nTime = 0;
                    }
                    else
                    {
                        nDamage = 0;
                    }
                    break;
            }

            if (nDamage <= 0)
                return 0;

            if (nAttacker.Kind == NPCKIND.kind_player && Kind == NPCKIND.kind_normal)
            {
                ((NpcObj)this).m_cDeathCalcExp.AddDamage(nAttacker.id, nDamage);
            }

            HPCur -= nDamage;
            if (HPCur <= 0)
            {
                nAttacker.m_nPeopleIdx = 0;
                HPCur = 0;

                DoDeath(DeathCalcPKValue(nAttacker), nAttacker);
            }
            return nDamage;
        }
        public void ReceiveDamage(CharacterObj nLauncher, int nEnChance)
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
            {
                nLauncher.m_nPeopleIdx = 0;
                return;
            }

            if (Utils.GetRelation(nLauncher, this) != NPCRELATION.relation_enemy)
                return;

            int nCurLifeDamage = CalcDamage(nLauncher, 20, 50, DAMAGE_TYPE.damage_physics);

            HPCur -= nCurLifeDamage;
            if (HPCur > 0)
            {
                DoHurt();

                var para = new EventData
                {
                    Code = (byte)OperationCode.NpcDamage,
                    Parameters = new Dictionary<byte, object>()
                    {
                        {(byte)ParamterCode.Id, this.id },
                        {(byte)ParamterCode.Data, nCurLifeDamage },
                    }
                };

                region.BroadCast(para);
            }
        }
        protected bool ProcessState()
        {
            if (m_LoopFrames % 10 == 0)
            {
                if (m_Doing == NPCCMD.do_sit)
                {
                    int nAdd = (HPMax << 1) / 1000;
                    if (nAdd <= 0)
                        nAdd = 1;
                    HPCur += nAdd;
                    if (HPCur > HPMax)
                        HPCur = HPMax;

                    nAdd = (MPMax << 1) / 1000;
                    if (nAdd <= 0)
                        nAdd = 1;
                    MPCur += nAdd;
                    if (MPCur > MPMax)
                        MPCur = MPMax;
                }

                HPCur += m_CurrentLifeReplenish * (100 + m_CurrentLifeReplenish_p) / 100;
                if (HPCur > HPMax)
                    HPCur = HPMax;

                MPCur += m_CurrentManaReplenish * (100 + m_CurrentManaReplenish_p) / 100;
                if (MPCur > MPMax)
                    MPCur = MPMax;

                if (m_Doing != NPCCMD.do_run)
                    SPCur += m_CurrentStaminaGain;

                if (SPCur > SPMax)
                    SPCur = SPMax;
            }
            return false;
        }
        protected void ProcCommand()
        {
            switch (m_Command.CmdKind)
            {
                case NPCCMD.do_stand:
                    DoStand();
                    break;
                case NPCCMD.do_hurt:
                    DoHurt();
                    break;
                case NPCCMD.do_jump:
                    DoJump();
                    break;
                case NPCCMD.do_walk:
                case NPCCMD.do_run:
                    DoMove();
                    break;
                case NPCCMD.do_skill:
                    DoSkill(m_Command.Param_Y, m_Command.Param_Z);
                    break;
            }
            m_Command.CmdKind = NPCCMD.do_none;
        }
        protected void ProcStatus()
        {
            switch (m_Doing)
            {
                case NPCCMD.do_stand:
                    break;
                case NPCCMD.do_hurt:
                    OnHurt();
                    break;
                case NPCCMD.do_walk:
                case NPCCMD.do_run:
                    if (ServeMove(m_Doing == NPCCMD.do_walk ? WalkSpeed : RunSpeed))
                    {
                        DoStand();
                    }
                    break;
                case NPCCMD.do_attack:// vat ly
                case NPCCMD.do_magic:// ma thuat
                    OnSkill();
                    break;
                case NPCCMD.do_death:
                    OnDeath();
                    break;
                case NPCCMD.do_revive:
                    OnRevive();
                    break;
            }
        }
        protected void DoStand()
        {
            m_Doing = NPCCMD.do_stand;
            m_ProcessAI = true;

            nCurrentFrame = nTotalFrame = 0;
        }
        void DoHurt(int nHurtFrames = 12)
        {
            m_Doing = NPCCMD.do_hurt;
            m_ProcessAI = false;

            nCurrentFrame = 0;
            nTotalFrame = (byte)(nHurtFrames + nHurtFrames * (100 + HitRecover) / 100);
        }
        void DoJump()
        {

        }
        void DoMove()
        {
            m_Doing = m_Command.CmdKind;
            m_DestX = m_Command.Param_X;
            m_DestY = m_Command.Param_Y;
        }
        void DoSkill(int nX, int nY)
        {
            if (m_Doing == NPCCMD.do_skill || m_Doing == NPCCMD.do_attack || m_Doing == NPCCMD.do_magic || m_Doing == NPCCMD.do_hurt)
                return;

            m_Hide.nTime = 0;

            var obj = skillList.GetSkillById(m_ActiveSkillID);
            if (obj != null)
            {
                if (obj.NextCastTime > scene.m_dwCurrentTime)
                    return;

                if (obj.CanCast(this) && obj.skillTemplate.CanCastSkill(this,nX,nY))
                {
                    m_DestX = nX;
                    m_DestY = nY;

                    obj.SaveNextTime(this, scene.m_dwCurrentTime);
                    
                    var param = new EventData
                    {
                        Code = (byte)OperationCode.NpcSkill,
                        Parameters = new Dictionary<byte, object>
                        {
                            {(byte)ParamterCode.Id, id},
                            {(byte)ParamterCode.SkillId, m_ActiveSkillID},
                            {(byte)ParamterCode.SkillLevel, m_ActiveSkillLevel},
                            {(byte)ParamterCode.SkillEnChance, (byte)0},
                            {(byte)ParamterCode.NpcType, nY},
                        }
                    };

                    region.BroadCast(param);

                    DoOrdinSkill(obj, nX, nY);
                }
            }
        }
        void DoOrdinSkill(SkillObj pSkill, int nX, int nY)
        {
            if (pSkill.skillTemplate.m_eSkillStyle == eSKillStyle.SKILL_SS_Melee)
            {
                if (!CastMeleeSkill(pSkill))
                {
                    DoStand();
                    return;
                }
            }
            else
            if (pSkill.skillTemplate.m_bIsPhysical)
            {
                m_Doing = NPCCMD.do_attack;
                nTotalFrame = (byte)(AttackSpeed * 100/(m_CurrentAttackSpeed + 100));
            }
            else
            {
                m_Doing = NPCCMD.do_magic;
                nTotalFrame = (byte)(CastSpeed * 100 / (m_CurrentCastSpeed + 100));
            }
            nCurrentFrame = 0;
            m_ProcessAI = false;
        }
        bool CastMeleeSkill(SkillObj pSkill)
        {
            return false;
        }
        void DoRevive()
        {
            m_Doing = NPCCMD.do_revive;
            m_ProcessAI = false;

            ClearStateSkillEffect();
            ClearNormalState();

            if (IsPlayer)
            {

            }
            else
            {
                nTotalFrame = ((NpcObj)this).npcTemplate.m_ReviveFrame;
                nCurrentFrame = 0;
            }
        }
        void OnHurt()
        {
            ++nCurrentFrame;
            if (nCurrentFrame == nTotalFrame)
            {
                DoStand();
            }
        }
        void OnSkill()
        {
            PhotonApp.log.Info("OnSkill");

            ++nCurrentFrame;
            if (nCurrentFrame == nTotalFrame)
            {
                DoStand();
            }
            else
            if (nCurrentFrame == nTotalFrame * 60 / 100)
            {
                if (m_DestX == -1)
                {
                    if (m_DestY <= 0)
                        return;

                    if (scene.FindObj(m_DestY) == null)
                        return;
                }
                skillList.GetSkillById(m_ActiveSkillID).skillTemplate.Cast(this, m_DestX, m_DestY);
            }
        }
        void OnDeath()
        {
            ++nCurrentFrame;
            if (nCurrentFrame == nTotalFrame)
            {
                if (IsPlayer)
                {

                }
                else
                if (Kind == NPCKIND.kind_partner)
                {
                    // remove
                }
                else
                {
                    CharacterObj obj = scene.FindObj(m_nPeopleIdx);
                    if (obj.IsPlayer)
                    {

                    }
                    else
                    if (obj.Kind == NPCKIND.kind_partner)
                    {

                    }
                    else
                    {

                    }
                    DoRevive();
                }
            }
        }
        void OnRevive()
        {
            if (IsPlayer)
                return;

            ++nCurrentFrame;
            if (nCurrentFrame == nTotalFrame)
            {
                Revive();
            }
        }
        void Revive()
        {
            DoStand();

            ((NpcObj)this).RestoreNpcInfo();
            ((NpcObj)this).m_cDeathCalcExp.Clear();
        }
        public bool ServeMove(byte MoveSpeed)
        {
            if (m_Doing != NPCCMD.do_walk && m_Doing != NPCCMD.do_run && m_Doing != NPCCMD.do_hurt && m_Doing != NPCCMD.do_runattack)
                return false;

            if (MoveSpeed <= 0)
                return true;

            if (MoveSpeed >= JXHelper.m_nCellWidth)
                MoveSpeed = JXHelper.m_nCellWidth - 1;

            int x = 0, y = 0;
            scene.NewMap2Mps(region, m_MapX, m_MapY, 0, 0, ref x, ref y);

            x = (x << 10) + m_OffX;
            y = (y << 10) + m_OffY;

            int nRet = m_PathFinder.GetDir(x, y, m_DestX, m_DestY, MoveSpeed, ref m_Dir);
            if (nRet == 1)
            {
                if (m_Dir >= JXHelper.MaxMissleDir)
                    m_Dir = (byte)(m_Dir % JXHelper.MaxMissleDir);

                x = JXHelper.g_DirCos(m_Dir) * MoveSpeed;
                y = JXHelper.g_DirSin(m_Dir) * MoveSpeed;
            }
            else
            {
                return false;
            }

            int nOldRegionIndex = region.RegionIndex;
            int nOldMapX = m_MapX;
            int nOldMapY = m_MapY;
            int nOldOffX = m_OffX;
            int nOldOffY = m_OffY;

            int m_RegionIndex = MoveMe(x, y, nOldRegionIndex);

            if (m_RegionIndex <= -1)
            {
                m_MapX = nOldMapX;
                m_MapY = nOldMapY;
                m_OffX = nOldOffX;
                m_OffY = nOldOffY;
                return true;
            }

            if (nOldRegionIndex != m_RegionIndex)
            {
                scene.ChangeRegion(nOldRegionIndex, m_RegionIndex, this, nOldMapX, nOldMapY);
            }
            return false;
        }
        public void SendSerCommand(NPCCMD cmd, int x, int y, int z = 0)
        {
            m_Command.CmdKind = cmd;
            m_Command.Param_X = x;
            m_Command.Param_Y = y;
            m_Command.Param_Z = z;

            //m_ProcessAI = true;
        }
        public void SetPos(int nX, int nY)
        {
            int nMpsX = 0, nMpsY = 0;
            GetMpsPos(ref nMpsX, ref nMpsY);

            if (nMpsX == nX && nMpsY == nY)
                return;

            int m_RegionIndex = 0,nMapX = 0, nMapY = 0, nOffX = 0, nOffY = 0;
            scene.Mps2Map(nX, nY, ref m_RegionIndex, ref nMapX, ref nMapY, ref nOffX, ref nOffY);

            if (m_RegionIndex >= 0)
            {
                var tx = m_MapX;
                var ty = m_MapY;

                m_MapX = nMapX;
                m_MapY = nMapY;
                m_OffX = nOffX;
                m_OffY = nOffY;

                int nOldRegion = region.RegionIndex;

                if (m_RegionIndex != nOldRegion)
                {
                    scene.ChangeRegion(nOldRegion, m_RegionIndex, this, tx , ty);
                }
            }
        }
    }
}
