using System.Collections.Generic;
using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Utils;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;
using Photon.ShareLibrary.Constant;
using Photon.JXGameServer.Skills;
using Photon.JXGameServer.Maps;
using Photon.JXGameServer.Entitys;

namespace Photon.JXGameServer.Entitys
{
    public class NpcObj : CharacterObj
    {
        public int nTemplateID;

        NPCKIND m_nKind;
        public override NPCKIND Kind
        {
            get { return m_nKind; }
        }
        public void SetNPCKIND(NPCKIND kind)
        {
            m_nKind = kind;
        }
        NPCSERIES m_nSeries;
        public override NPCSERIES Series
        {
            get { return m_nSeries; }
            set { m_nSeries = value; }
        }
        NPCCAMP m_nCamp;
        public override NPCCAMP Cam
        {
            get { return m_nCamp; }
            set { m_nCamp = value; }
        }
        NPCCAMP m_nCurrentCamp;
        public override NPCCAMP CurrentCamp
        {
            get { return m_nCurrentCamp; }
            set { m_nCurrentCamp = value; }
        }
        bool m_FightMode = true;
        public override bool FightMode
        {
            get { return m_FightMode; }
            set { m_FightMode = value; }
        }
        int m_CurrentLifeMax;
        public override int HPMax
        {
            get { return m_CurrentLifeMax; }
            set { m_CurrentLifeMax = value; }
        }
        int m_CurrentLife;
        public override int HPCur
        {
            get { return m_CurrentLife; }
            set { m_CurrentLife = value; }
        }
        byte m_nLevel;
        public override byte Level
        {
            get { return m_nLevel; }
            set { m_nLevel = value; }
        }
        public string m_Name = string.Empty;
        public override string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public string m_Title = string.Empty;
        public override string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }
        public override byte WalkSpeed
        {
            get { return npcTemplate.m_WalkSpeed; }
            set { }
        }
        public override byte RunSpeed
        {
            get { return npcTemplate.m_RunSpeed; }
            set { }
        }
        public override int AttackSpeed
        {
            get { return m_CurrentAttackSpeed; }
            set { }
        }
        public override int CastSpeed
        {
            get { return m_CurrentCastSpeed; }
            set { }
        }
        public override int HitRecover
        {
            get { return npcTemplate.m_HitRecover; }
            set { }
        }

        public uint szDropFile;
        public NpcObj(SceneObj scene, RegionObj region) : base(scene, region)
        {

        }
        public int m_OriginX, m_OriginY;
        public byte m_CurrentTreasure;
        public bool LoadNPC(KSPNpc kSPNpc)
        {
            m_OriginX = kSPNpc.nPositionX;
            m_OriginY = kSPNpc.nPositionY;

            if (AddMap(m_OriginX, m_OriginY) >= 0)
            {
                this.nTemplateID = kSPNpc.nTemplateID + 1;
                this.m_nLevel = kSPNpc.Level;

                this.m_nSeries = (NPCSERIES)(byte)KRandom.GetRandomNumber(0,4);
                this.m_nKind = (NPCKIND)kSPNpc.Kind;
                this.m_nCamp = (NPCCAMP)kSPNpc.Camp;
                this.m_nCurrentCamp = this.m_nCamp;

                this.m_Script = kSPNpc.Scripts;

                LoadData();
                return true;
            }
            return false;
        }
        public NpcDeathCalcExp m_cDeathCalcExp;
        public NpcTemplate npcTemplate;
        public NpcGold npcGold;
        public NpcAI npcAI;

        public void LoadData()
        {
            m_Name = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.Name, nTemplateID);

            var keys = $"idx_{nTemplateID}_level_{m_nLevel}";
            if (!SceneModule.Me.NpcTemplates.ContainsKey(keys))
            {
                SceneModule.Me.NpcTemplates.Add(keys, new NpcTemplate(nTemplateID,m_nLevel,(byte)m_nSeries));
            }
            //base value from template (script)
            npcTemplate = SceneModule.Me.NpcTemplates[keys];

            if (m_nKind == NPCKIND.kind_normal)
            {
                m_PhysicsDamage = new JX_KMagicAttrib();
                m_PhysicsDamage.Init();

                skillList = new SkillList(npcTemplate.m_SkillList);

                m_cDeathCalcExp = new NpcDeathCalcExp();
                npcGold = new NpcGold(this);

                npcAI = new NpcAI(this, npcTemplate);
                npcAI.LoadAI(nTemplateID);

                m_PathFinder = new NpcFindPath(this);
            }
        }
        public void Init()
        {
            m_ProcessAI = m_nKind == NPCKIND.kind_normal;
            region.AddNpcRef(m_MapX, m_MapY);

            RestoreNpcInfo();

            //backup for change
            backups = new ExitGames.Client.Photon.Hashtable
            {
                { (byte)ParamterCode.Title, m_Title },
                { (byte)ParamterCode.Name, m_Name },
                { (byte)ParamterCode.Kind, (byte)m_nKind },
                { (byte)ParamterCode.Series, (byte)m_nSeries },
                { (byte)ParamterCode.Cam, (byte)m_nCurrentCamp },
                { (byte)ParamterCode.HPMax, m_CurrentLifeMax },
                { (byte)ParamterCode.HPCur, m_CurrentLife },
                { (byte)ParamterCode.MapX, m_OriginX },
                { (byte)ParamterCode.MapY, m_OriginY },
                { (byte)ParamterCode.Dir, m_Dir },

                { (byte)ParamterCode.ActionId, (byte)m_Doing }
            };
            if (npcGold != null)
                backups.Add((byte)ParamterCode.Data, npcGold.m_nGoldType);

            if (this.m_Script == 0)
                return;

            var handle = ScriptModule.Me.GetScript(this.m_Script);
            if (handle != null)
            {
                handle.CallFunction(scene, "init", id);
            }
            else
            if (m_nKind != NPCKIND.kind_normal)
            {
                PhotonApp.log.InfoFormat("NpcObj:Init map {0} script {1} not found", scene.MapId, ScriptModule.Me.GetPath(this.m_Script));
            }
        }
        public void RestoreNpcInfo()
        {
            m_CurrentFireResist = npcTemplate.m_FireResist;
            m_CurrentColdResist = npcTemplate.m_ColdResist;
            m_CurrentLightResist = npcTemplate.m_LightResist;
            m_CurrentPoisonResist = npcTemplate.m_PoisonResist;
            m_CurrentPhysicsResist = npcTemplate.m_PhysicsResist;

            m_CurrentExperience = npcTemplate.m_Experience;
            m_CurrentLifeMax = m_CurrentLife = npcTemplate.m_LifeMax;
            m_CurrentLifeReplenish = npcTemplate.m_LifeReplenish;
            m_CurrentAttackRating = npcTemplate.m_AttackRating;
            m_CurrentDefend = npcTemplate.m_Defend;

            m_CurrentTreasure = npcTemplate.m_Treasure;
            m_CurrentWalkSpeed = npcTemplate.m_WalkSpeed;
            m_CurrentRunSpeed = npcTemplate.m_RunSpeed;
            m_CurrentAttackSpeed = npcTemplate.m_AttackSpeed;
            m_CurrentCastSpeed = npcTemplate.m_CastSpeed;

            if (m_nKind != NPCKIND.kind_normal)
                return;

            m_PhysicsDamage.nValue[0] = npcTemplate.m_PhysicsDamage.nValue[0];
            m_PhysicsDamage.nValue[2] = npcTemplate.m_PhysicsDamage.nValue[2];

            szDropFile = 0;
            npcGold.ChangeGold(0);
        }
        public void SyncNormal(PlayerObj me)
        {
            int X = 0, Y = 0;
            GetMpsPos(ref X, ref Y);

            var param = new Dictionary<byte, object>
            {
                { (byte)ParamterCode.Id, id },
                { (byte)ParamterCode.Level, m_nLevel },
                { (byte)ParamterCode.NpcType, nTemplateID - 1},

                { (byte)ParamterCode.Title, m_Title },
                { (byte)ParamterCode.Name, m_Name },
                { (byte)ParamterCode.Kind, (byte)m_nKind },
                { (byte)ParamterCode.Series, (byte)m_nSeries },
                { (byte)ParamterCode.Cam, (byte)m_nCurrentCamp },
                { (byte)ParamterCode.HPMax, m_CurrentLifeMax },
                { (byte)ParamterCode.HPCur, m_CurrentLife },
                { (byte)ParamterCode.MapX, X },
                { (byte)ParamterCode.MapY, Y },
                { (byte)ParamterCode.Dir, m_Dir },

                { (byte)ParamterCode.ActionId, (byte)m_Doing }
            };
            if (npcGold != null)
                param.Add((byte)ParamterCode.Data, npcGold.m_nGoldType);

            me.ClientPeer.SendEvent(new EventData
            {
                Code = (byte)OperationCode.SendNpc,
                Parameters = param,
            }, me.sendParameters);
        }
        public Dictionary<byte, object> SyncUpdate()
        {
            var param = new Dictionary<byte, object>();

            int X = 0, Y = 0;
            GetMpsPos(ref X, ref Y);

            if (m_Dir != (byte)backups[(byte)ParamterCode.Dir])
            {
                param.Add((byte)ParamterCode.Dir, m_Dir);
                backups[(byte)ParamterCode.Dir] = m_Dir;
            }
            if ((X != (int)backups[(byte)ParamterCode.MapX]) || (Y != (int)backups[(byte)ParamterCode.MapY]))
            {
                param.Add((byte)ParamterCode.MapX, X);
                backups[(byte)ParamterCode.MapX] = X;

                param.Add((byte)ParamterCode.MapY, Y);
                backups[(byte)ParamterCode.MapY] = Y;
            }
            if (m_Title != (string)backups[(byte)ParamterCode.Title])
            {
                param.Add((byte)ParamterCode.Title, m_Title);
                backups[(byte)ParamterCode.Title] = m_Title;
            }
            if (m_Name != (string)backups[(byte)ParamterCode.Name])
            {
                param.Add((byte)ParamterCode.Name, m_Name);
                backups[(byte)ParamterCode.Name] = m_Name;
            }
            if ((byte)m_nKind != (byte)backups[(byte)ParamterCode.Kind])
            {
                param.Add((byte)ParamterCode.Kind, (byte)m_nKind);
                backups[(byte)ParamterCode.Kind] = (byte)m_nKind;
            }
            if ((byte)m_nSeries != (byte)backups[(byte)ParamterCode.Series])
            {
                param.Add((byte)ParamterCode.Series, (byte)m_nSeries);
                backups[(byte)ParamterCode.Series] = (byte)m_nSeries;
            }
            if ((byte)m_nCurrentCamp != (byte)backups[(byte)ParamterCode.Cam])
            {
                param.Add((byte)ParamterCode.Cam, (byte)m_nCurrentCamp);
                backups[(byte)ParamterCode.Cam] = (byte)m_nCurrentCamp;
            }
            if (m_CurrentLifeMax != (int)backups[(byte)ParamterCode.HPMax])
            {
                param.Add((byte)ParamterCode.HPMax, m_CurrentLifeMax);
                backups[(byte)ParamterCode.HPMax] = m_CurrentLifeMax;
            }
            if (m_CurrentLife != (int)backups[(byte)ParamterCode.HPCur])
            {
                param.Add((byte)ParamterCode.HPCur, m_CurrentLife);
                backups[(byte)ParamterCode.HPCur] = m_CurrentLife;
            }
            if (npcGold != null)
            {
                if (npcGold.m_nGoldType != (byte)backups[(byte)ParamterCode.Data])
                {
                    param.Add((byte)ParamterCode.Data, npcGold.m_nGoldType);
                    backups[(byte)ParamterCode.Data] = npcGold.m_nGoldType;
                }
            }
            if ((byte)m_Doing != (byte)backups[(byte)ParamterCode.ActionId])
            {
                param.Add((byte)ParamterCode.ActionId, (byte)m_Doing);
                backups[(byte)ParamterCode.ActionId] = (byte)m_Doing;
            }

            if (param.Count > 0)
            {
                param.Add((byte)ParamterCode.Id, id);
            }

            return param;
        }
        public void HeartBeat()
        {
            if (m_nKind == NPCKIND.kind_dialoger)
            {

            }
            else
            {
                ++m_LoopFrames;

                if (HPCur > 0)
                {
                    if (ProcessState())
                        return;
                }

                if (m_ProcessAI)
                {
                    npcAI.ProcessAI();

                    ProcCommand();
                }

                ProcStatus();

                region.SyncNpc(this);
            }
        }
        public SkillObj GetSkillLevel(byte idx) => skillList.GetSkillByIndex(idx);
        public void SetSkillLevel(byte idx,int id,byte level)
        {
            var skill = skillList.GetSkillByIndex(idx);
            if (skill != null)
            {
                skill.id = id;
                skill.level = level;
            }
            else
            {
                skillList.SetSkillAtIndex(idx, id, level);
            }
        }
        public void SetAuraSkill(byte idx)
        {
            var aura = skillList.GetSkillByIndex(idx);
            if (aura != null)
            {
                m_ActiveAuraID = aura.id;
                //aura.skillTemplate.st
            }
        }
        public bool SetActiveSkill(byte idx)
        {
            var skill = skillList.GetSkillByIndex(idx);
            if (skill != null)
            {
                m_ActiveSkillID = skill.id;
                m_ActiveSkillLevel = skill.level;

                m_CurrentAttackRadius = skill.skillTemplate.m_nAttackRadius;

                return true;
            }
            return false;
        }
        public void SetName(string str) => this.m_Name = str;
        public void SetTitle(string str) => this.m_Title = str;
        public void SetScript(uint str) => this.m_Script = str;
    }
}
