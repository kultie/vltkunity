using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using Photon.JXGameServer.Items;
using Photon.JXGameServer.Scripts;
using JX.Database;

namespace Photon.JXGameServer.Entitys
{
    public class PlayerObj : CharacterObj
    {
        public ClientPeer ClientPeer { get; set; }
        public SendParameters sendParameters { get; set; }

        public PlayerState state;
        PlayerLoad load;
        ushort step;

        public uint uid,cid;

        public CharacterData character;
        public List<ItemObj> playerItems;
        public List<PlayerTask> playerTasks;
        public List<PlayerFriend> playerFriends;

        public override NPCKIND Kind
        {
            get { return NPCKIND.kind_player; }
        }
        public override NPCSERIES Series
        {
            get { return (NPCSERIES)character.Fiveprop; }
            set { character.Fiveprop = (byte)value; }
        }
        public override NPCCAMP CurrentCamp
        {
            get { return (NPCCAMP)character.Camp; }
            set { character.Camp = (byte)value; }
        }
        public override bool FightMode
        {
            get { return character.FightMode; }
            set { character.FightMode = value; }
        }
        public override int HPMax
        {
            get { return character.MaxLife; }
            set { character.MaxLife = value; }
        }
        public override int HPCur
        {
            get { return character.CurLife; }
            set { character.CurLife = value; }
        }
        public override int MPMax
        {
            get { return character.MaxInner; }
            set { character.MaxInner = value; }
        }
        public override int MPCur
        {
            get { return character.CurInner; }
            set { character.CurInner = value; }
        }
        public override int SPMax
        {
            get { return character.MaxStamina; }
            set { character.MaxStamina = value; }
        }
        public override int SPCur
        {
            get { return character.CurStamina; }
            set { character.CurStamina = value; }
        }
        public override byte Level
        {
            get { return (byte)character.FightLevel; }
            set { character.FightLevel = value; }
        }
        public override string Name
        {
            get { return character.Name; }
            set { character.Name = value; }
        }
        public override byte WalkSpeed
        {
            get { return (byte)PlayerBase.BASE_WALK_SPEED; }
            set { }
        }
        public override byte RunSpeed
        {
            get { return (byte)PlayerBase.BASE_RUN_SPEED; }
            set { }
        }
        public override int AttackSpeed
        {
            get { return (byte)PlayerBase.BASE_ATTACK_SPEED; }
            set { }
        }
        public override int CastSpeed
        {
            get { return (byte)PlayerBase.BASE_CAST_SPEED; }
            set { }
        }
        public override int HitRecover
        {
            get { return (byte)PlayerBase.BASE_HIT_RECOVER; }
            set { }
        }

        public short Faction
        {
            get { return (short)(character.Sect - 1); }
            set { character.Sect = (byte)(value + 1); }
        }
        public uint Tong
        {
            get { return character.TongID; }
            set { character.TongID = value; }
        }

        public int m_nObjectIdx;
        public List<string> NpcStr;

        public int TeamId = 0;
        public bool m_RunStatus = false;

        public byte m_ProtectTime = 0;
        public List<short> m_PlayerStationList = new List<short>();
        public List<short> m_PlayerWayPointList = new List<short>();

        public PLAYER_REVIVAL_POS_DATA m_sPortalPos, m_sDeathRevivalPos;

        public PlayerObj(ClientPeer client, SendParameters sendParameters, uint uid, uint cid) : base(null, null)
        {
            this.ClientPeer = client;
            this.sendParameters = sendParameters;

            this.uid = uid;
            this.cid = cid;

            m_nObjectIdx = 0;
            NpcStr = new List<string>();

            state = PlayerState.init;

            m_sPortalPos = new PLAYER_REVIVAL_POS_DATA();
            m_sDeathRevivalPos = new PLAYER_REVIVAL_POS_DATA();
        }
        public void SyncNormal(PlayerObj me, bool isPlay = true)
        {
            var param = new Dictionary<byte, object>
            {
                { (byte)ParamterCode.Id, id },
                                    
                { (byte)ParamterCode.Name, character.Name },

                { (byte)ParamterCode.Series, character.Fiveprop },
                { (byte)ParamterCode.Sex, character.Sex },

                { (byte)ParamterCode.FactionId, character.Sect },

                { (byte)ParamterCode.Level, character.FightLevel },

                { (byte)ParamterCode.Cam, character.Camp },
                { (byte)ParamterCode.Fight, character.FightMode },

                { (byte)ParamterCode.HPMax, HPMax },
                { (byte)ParamterCode.HPCur, HPCur },
                { (byte)ParamterCode.MPMax, MPMax },
                { (byte)ParamterCode.MPCur, MPCur },
                { (byte)ParamterCode.SPMax, SPMax },
                { (byte)ParamterCode.SPCur, SPCur },
            };

            if (isPlay)
            {
                int X = 0, Y = 0;
                GetMpsPos(ref X, ref Y);

                param.Add((byte)ParamterCode.MapX, X);
                param.Add((byte)ParamterCode.MapY, Y);
                param.Add((byte)ParamterCode.Dir, m_Dir);
            }
            else
            {
                param.Add((byte)ParamterCode.Exp, character.FightExp);
            }

            me.ClientPeer.SendEvent(new EventData
            {
                Code = (byte)OperationCode.SendPlayer,
                Parameters = param,
            }, me.sendParameters);
        }
        public Dictionary<byte, object> SyncUpdate()
        {
            var param = new Dictionary<byte, object>();

            int X = 0, Y = 0;
            GetMpsPos(ref X, ref Y);

            if ((byte)m_Doing != (byte)backups[(byte)ParamterCode.ActionId])
            {
                param.Add((byte)ParamterCode.ActionId, (byte)m_Doing);
                backups[(byte)ParamterCode.ActionId] = (byte)m_Doing;
            }
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

            if (character.FightMode != (bool)backups[(byte)ParamterCode.Fight])
            {
                param.Add((byte)ParamterCode.Fight, character.FightMode);
                backups[(byte)ParamterCode.Fight] = character.FightMode;
            }
            if (character.FightLevel != (byte)backups[(byte)ParamterCode.Level])
            {
                param.Add((byte)ParamterCode.Level, character.FightLevel);
                backups[(byte)ParamterCode.Level] = character.FightLevel;
            }
            if (character.FightExp != (int)backups[(byte)ParamterCode.Exp])
            {
                param.Add((byte)ParamterCode.Exp, character.FightExp);
                backups[(byte)ParamterCode.Exp] = character.FightExp;
            }

            if (HPMax != (int)backups[(byte)ParamterCode.HPMax])
            {
                param.Add((byte)ParamterCode.HPMax, HPMax);
                backups[(byte)ParamterCode.HPMax] = HPMax;
            }
            if (HPCur != (int)backups[(byte)ParamterCode.HPCur])
            {
                param.Add((byte)ParamterCode.HPCur, HPCur);
                backups[(byte)ParamterCode.HPCur] = HPCur;
            }
            if (MPMax != (int)backups[(byte)ParamterCode.MPMax])
            {
                param.Add((byte)ParamterCode.MPMax, MPMax);
                backups[(byte)ParamterCode.MPMax] = MPMax;
            }
            if (MPCur != (int)backups[(byte)ParamterCode.MPCur])
            {
                param.Add((byte)ParamterCode.MPCur, MPCur);
                backups[(byte)ParamterCode.MPCur] = MPCur;
            }
            if (SPMax != (int)backups[(byte)ParamterCode.SPMax])
            {
                param.Add((byte)ParamterCode.SPMax, SPMax);
                backups[(byte)ParamterCode.SPMax] = SPMax;
            }
            if (SPCur != (int)backups[(byte)ParamterCode.SPCur])
            {
                param.Add((byte)ParamterCode.SPCur, SPCur);
                backups[(byte)ParamterCode.SPCur] = SPCur;
            }

            if (param.Count > 0)
            {
                param.Add((byte)ParamterCode.Id, id);
            }

            return param;
        }
        public bool isLoading
        {
            get
            {
                return state == PlayerState.init || state == PlayerState.load;
            }
        }
        public bool isPlaying
        {
            get
            {
                return state == PlayerState.play;
            }
        }
        public bool isExit
        {
            get
            {
                return state == PlayerState.exit;
            }
        }
        uint m_TrapScriptID = 0;
        protected bool TriggerMapTrap()
        {
            uint dwTrap = region.CheckTrap(m_MapX, m_MapY);
            if (m_TrapScriptID == dwTrap)
                return (dwTrap != 0);

            m_TrapScriptID = dwTrap;
            if (m_TrapScriptID == 0)
                return false;

            var script = ScriptModule.Me.GetScript(m_TrapScriptID);
            if (script != null)
            {
                script.SetPlayer(id);
                script.CallFunction(scene, "main", id);
            }
            else
            {
                PhotonApp.log.InfoFormat("TriggerMapTrap {0} not found ", m_TrapScriptID);
            }

            return true;
        }
        protected bool m_BaiTan = false;
        void GotoIt(MapObj obj)
        {
            int nDesX = 0, nDesY = 0;
            obj.GetMpsPos(ref nDesX, ref nDesY);

            SendSerCommand(m_RunStatus ? NPCCMD.do_run : NPCCMD.do_walk, nDesX, nDesY);
        }
        void FollowPeople()
        {
            if (Doing == NPCCMD.do_death || Doing == NPCCMD.do_revive)
            {
                m_nPeopleIdx = 0;
                return;
            }
            var obj = scene.FindObj(m_nPeopleIdx);
            if (obj == null || obj.Doing == NPCCMD.do_death || obj.Doing == NPCCMD.do_revive)
            {
                m_nPeopleIdx = 0;
                return;
            }
            if (obj.Kind == NPCKIND.kind_dialoger)
            {

            }
            else
            if (obj.Kind == NPCKIND.kind_player && ((PlayerObj)obj).m_BaiTan)
            {

            }
            else
            {
                var distance = GetDistance(obj);
                if (Utils.GetRelation(this, obj) == NPCRELATION.relation_enemy)
                {
                    if (obj.Kind == NPCKIND.kind_player && !obj.FightMode)
                        return;

                    if (RandMove.nTime > 0 || obj.Doing == NPCCMD.do_jump)
                    {
                        m_nPeopleIdx = 0;
                        return;
                    }

                    if (distance > this.m_CurrentAttackRadius)
                    {
                        GotoIt(obj);
                    }
                    else
                    {
                        SendSerCommand(NPCCMD.do_skill, m_ActiveSkillID, -1, m_nPeopleIdx);
                    }
                }
            }
        }
        void FollowObject()
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
            {
                m_nObjectIdx = 0;
                return;
            }

            var obj = scene.FindObjectObj(m_nObjectIdx);
            if (obj == null)
            {
                m_nObjectIdx = 0;
                return;
            }

            if (GetDistanceSquare(obj) < JXHelper.PLAYER_PICKUP_CLIENT_DISTANCE * JXHelper.PLAYER_PICKUP_CLIENT_DISTANCE)
            {
                obj.PickUpObj(this);
            }
            else
            {
                GotoIt(obj);
            }
        }
        public void HeartBeat()
        {
            if (isPlaying)
            {
                if (TriggerMapTrap())
                    return;

                if (m_nPeopleIdx > 0)
                {
                    FollowPeople();
                }

                if (m_nObjectIdx > 0)
                {
                    FollowObject();
                }

                ++m_LoopFrames;

                if (HPCur > 0)
                {
                    if (ProcessState())
                        return;
                }

                ProcCommand();

                ProcStatus();

                region.SyncPlayer(this);
            }
        }
        public void DoLoad()
        {
            if (state == PlayerState.init)
            {
                state = PlayerState.load;
                load = PlayerLoad.info;
                step = 0;
            }
            LoadData();
        }
        void InitBase()
        {
            //SetFirstDamage
            int nDamageBase = character.Power / 5 + 1;
            m_PhysicsDamage = new ShareLibrary.Utils.JX_KMagicAttrib();
            m_PhysicsDamage.Init();
            m_PhysicsDamage.nValue[0] = nDamageBase;
            m_PhysicsDamage.nValue[2] = nDamageBase;
            m_PhysicsDamage.nValue[1] = 0;

            //SetBaseAttackRating
            m_CurrentAttackRating = (ushort)(character.Agility * 4 - 28);

            //SetBaseDefence
            m_CurrentDefend = (ushort)(character.Agility >> 2);

            //SetBaseResistData
            m_CurrentFireResist = PlayerModule.Me.GetFireResist((NPCSERIES)character.Fiveprop, (byte)character.FightLevel);
            m_CurrentColdResist = PlayerModule.Me.GetFireResist((NPCSERIES)character.Fiveprop, (byte)character.FightLevel);
            m_CurrentPoisonResist = PlayerModule.Me.GetFireResist((NPCSERIES)character.Fiveprop, (byte)character.FightLevel);
            m_CurrentLightResist = PlayerModule.Me.GetFireResist((NPCSERIES)character.Fiveprop, (byte)character.FightLevel);
            m_CurrentPhysicsResist = PlayerModule.Me.GetFireResist((NPCSERIES)character.Fiveprop, (byte)character.FightLevel);

            m_CurrentLifeReplenish = m_CurrentManaReplenish = m_CurrentStaminaGain = 1;

            m_CurrentAttackSpeed = (byte)PlayerBase.BASE_ATTACK_SPEED;
            m_CurrentCastSpeed = (byte)PlayerBase.BASE_CAST_SPEED;

            backups = new ExitGames.Client.Photon.Hashtable
            {
                {(byte)ParamterCode.ActionId, (byte)m_Doing },
                {(byte)ParamterCode.Dir, (byte)1 },
                {(byte)ParamterCode.MapX, character.MapX },
                {(byte)ParamterCode.MapY, character.MapY },

                {(byte)ParamterCode.Fight, character.FightMode },
                {(byte)ParamterCode.Level, character.FightLevel },
                {(byte)ParamterCode.Exp, character.FightExp },

                {(byte)ParamterCode.HPMax , HPMax},
                {(byte)ParamterCode.HPCur , HPCur},
                {(byte)ParamterCode.MPMax , MPMax},
                {(byte)ParamterCode.MPCur , MPCur},
                {(byte)ParamterCode.SPMax , SPMax},
                {(byte)ParamterCode.SPCur , SPCur},
            };
        }
        public bool isSync = false;
        void LoadData()
        {
            switch (load)
            {
                case PlayerLoad.info:
                    {
                        var datas = DatabaseRepository.Me.getPlayerData(uid, cid);
                        character = datas.character;
                        skillList = new Skills.SkillList(datas.skills);
                        playerItems = ItemModule.Me.AddToSet(datas.items);
                        playerTasks = datas.tasks;
                        playerFriends = datas.friends;

                        if (isSync)
                        {
                            SyncNormal(this, false);

                            if (character.TongID > 0)
                            {
                                PhotonApp.SocialPeer.peer.SendOperation((byte)OperationCode.TongCommand,
                                    new Dictionary<byte, object>
                                    {
                                        { (byte)ParamterCode.ActionId, (byte)TongCommand.Query },
                                        { (byte)ParamterCode.CharacterId, cid },
                                        { (byte)ParamterCode.FactionId, character.TongID },
                                    }, ExitGames.Client.Photon.SendOptions.SendReliable);
                            }
                        }
                    }
                    load = PlayerLoad.skill;
                    step = 0;
                    break;

                case PlayerLoad.skill:
                    if (isSync && step < skillList.m_SkillList.Count)
                    {
                        ClientPeer.SendEvent(new EventData
                        {
                            Code = (byte)OperationCode.SyncCharSkill,
                            Parameters = new Dictionary<byte, object>
                            {
                                { (byte)ParamterCode.Data , Utils.SerializeBson(skillList.m_SkillList) },
                                //{ (byte)ParamterCode.Id , playerSkills[step].id },
                                //{ (byte)ParamterCode.Level , playerSkills[step].level },
                                //{ (byte)ParamterCode.Exp , playerSkills[step].exp },
                            }
                        }, sendParameters);
                    }
                    load = PlayerLoad.item;
                    step = 0;
                    break;

                case PlayerLoad.item:
                    if (isSync && (playerItems != null) && (step < playerItems.Count))
                    {
                        SendItem(playerItems[step].ItemData);
                        step++;
                    }
                    else
                    {
                        load = PlayerLoad.task;
                        step = 0;
                    }
                    break;

                case PlayerLoad.task:
                    //if (isSync && (playerTasks != null) && (step < playerTasks.Count))
                    //{
                    //    ClientPeer.SendEvent(new EventData
                    //    {
                    //        Code = (byte)OperationCode.SyncCharTask,
                    //        Parameters = new Dictionary<byte, object>
                    //        {
                    //            //[(byte)ParamterCode.Data] = playerTasks[step].ToByteArray(),
                    //        }
                    //    }, sendParameters);
                    //    step++;
                    //}
                    //else
                    //{
                        load = PlayerLoad.friend;
                        step = 0;
                    //}
                    break;

                case PlayerLoad.friend:
                    if (isSync && (playerFriends != null) && (step < playerFriends.Count))
                    {
                        ClientPeer.SendEvent(new EventData
                        {
                            Code = (byte)OperationCode.SyncCharFriend,
                            Parameters = new Dictionary<byte, object>
                            {
                                { (byte)ParamterCode.Data , Utils.SerializeBson(playerFriends) },
                            }
                        }, sendParameters);

                        step = (ushort)playerFriends.Count;
                    }
                    InitBase();

                    ChangeWorld(character.MapId, character.MapX, character.MapY);
                    SetRevivalPos(character.MapId);

                    m_PathFinder = new NpcFindPath(this);
                    break;
            }
        }
        public void EnterWorld()
        {
            region.SendMe(this);
            state = PlayerState.play;

            DoScript("\\script\\system\\player.lua", "online");
        }
        // for Lua
        public bool HaveItem(int nNo) => playerItems.FirstOrDefault(x => (ItemGenre)x.GetGenre() == ItemGenre.item_task && x.GetDetailType() == nNo) != null;
        public int GetSaveVal(int nNo)
        {
            var task = playerTasks.Find(x  => x.id == nNo);
            if (task == null)
                return 0;
            //return task.Value;
            return 1;
        }
        public void SetSaveVal(int nNo, int bFlag)
        {
            var task = playerTasks.Find(x => x.id == nNo);
            if (task == null)
            {
                playerTasks.Add(new PlayerTask
                {
                    id = (ushort)nNo,
                });
            }
            //task.Value = bFlag;
        }
        public void ChangeWorld(int MapId, int X, int Y)
        {
            if ((scene != null) && (scene.MapId == MapId))
            {
                SetPos(X, Y);
                DoStand();
            }
            else
            {
                var temp = SceneModule.Me.FindMapId(MapId);
                if (temp != null)
                {
                    if (scene != null)
                    {
                        scene.RemovePlayer(this);
                    }
                    scene = temp;
                    scene.AddPlayer(this, X, Y);
                }
                else
                {
                    PhotonApp.LoginPeer.peer.SendOperation(
                        (byte)OperationCode.ServerCharge,
                        new Dictionary<byte, object>
                        {
                            {(byte)ParamterCode.CharacterId, cid},
                            {(byte)ParamterCode.MapId, MapId},
                            {(byte)ParamterCode.MapX, X},
                            {(byte)ParamterCode.MapY, Y},
                        }, ExitGames.Client.Photon.SendOptions.SendReliable);
                }
            }
        }

        #region Team
        public void TeamCreate() => PlayerModule.Me.TeamCreate(this);

        public void TeamLeave() => PlayerModule.Me.TeamLeave(this);

        public void TeamInvite(int cid) => PlayerModule.Me.TeamInvite(this, cid);

        public void TeamJoin(int cid) => PlayerModule.Me.TeamJoin(this, cid);

        public void TeamAccept(int cid,bool act) => PlayerModule.Me.TeamAccept(this, cid, act);

        public void TeamCharge(int cid) => PlayerModule.Me.TeamCharge(this, cid);
        #endregion

        public void UseTownPortal()
        {
            m_sPortalPos.m_nSubWorldID = scene.MapId;
            GetMpsPos(ref m_sPortalPos.m_nMpsX, ref m_sPortalPos.m_nMpsY);

            ChangeWorld(m_sDeathRevivalPos.m_nSubWorldID, m_sDeathRevivalPos.m_nMpsX, m_sDeathRevivalPos.m_nMpsY);
            character.FightMode = false;
        }
        public void BackToTownPortal()
        {
            ChangeWorld(m_sPortalPos.m_nSubWorldID, m_sPortalPos.m_nMpsX, m_sPortalPos.m_nMpsY);
            character.FightMode = true;
        }
        public void SetRevivalPos(ushort nSubWorld)
        {
            var temp = 0;
            SceneModule.Me.GetRevivalPosRandIdx(nSubWorld, ref temp, ref m_sDeathRevivalPos.m_nMpsX, ref m_sDeathRevivalPos.m_nMpsY);

            m_sDeathRevivalPos.m_nSubWorldID = nSubWorld;
            m_sDeathRevivalPos.m_ReviveID = (ushort)temp;
        }
        public void SetRevivalPos(ushort nSubWorld, ushort nReviveId)
        {
            m_sDeathRevivalPos.m_nSubWorldID = nSubWorld;
            m_sDeathRevivalPos.m_ReviveID = nReviveId;
            SceneModule.Me.GetRevivalPosFromId(nSubWorld, nReviveId, ref m_sDeathRevivalPos.m_nMpsX, ref m_sDeathRevivalPos.m_nMpsY);
        }
        // for Exp ///////////////////////////////////////////////////////
        void LevelUp()
        {
            character.FightExp = 0;
            character.FightLevel++;

            DoScript("\\script\\system\\player.lua", "levelup");
        }
        void AddSelfExp(int nExp, int nTarLevel)
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
                return;

            var exps = PlayerModule.Me.NextLevelExp(Level);

            character.FightExp += nExp;
            if (character.FightExp > exps)
            {
                LevelUp();
            }
            else
            {
                var para = new EventData
                {
                    Code = (byte)OperationCode.NpcExp,
                    Parameters = new Dictionary<byte, object>()
                {
                    {(byte)ParamterCode.Data, nExp },
                }
                };
                ClientPeer.SendEvent(para, sendParameters);
            }
        }
        public void AddExp(int exp, byte level)
        {
            if (TeamId == 0)
            {
                AddSelfExp(exp, level);
            }
            else
            {

            }
        }
        // for Item ///////////////////////////////////////////////////////
        public bool FindPos(byte place, int w, int h, ref byte x, ref byte y)
        {
            x = 0; y = 0;
            return true;
        }
        void SendItem(ItemData item)
        {
/*
            ClientPeer.SendEvent(new EventData
            {
                Code = (byte)OperationCode.SyncCharItem,
                Parameters = new Dictionary<byte, object>
                {
                    [(byte)ParamterCode.Data] = item.ToByteArray(),
                }
            }, sendParameters);
*/
        }
        public bool AddItem(ItemObj item)
        {
            byte x = 0, y = 0;
            if (FindPos(0, 0, 0, ref x, ref y))
            {
                var data = item.ItemData;
                item.SaveTo(data);
                //data.Cid = this.cid;
                data.Local = (int)ItemPosition.pos_equiproom;
                data.X = x; data.Y = y;
                playerItems.Add(item);
                SendItem(data);
                return true;
            }
            return false;
        }
        public void PickItem(int id)
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
                return;

            m_nObjectIdx = id;
        }
        // for NPC ///////////////////////////////////////////////////////
        public void NpcQuery(int npc)
        {
            var obj = region.FindNpc(npc);
            if (obj != null)
            {
                m_nPeopleIdx = npc;
                DoScript(obj.m_Script, "main");
            }
            else
            {
                PhotonApp.log.ErrorFormat("NpcQuery {0} not found", npc);
            }
        }
        public void NpcSelect(int npc, int func)
        {
            if ((func < NpcStr.Count) && !string.IsNullOrEmpty(NpcStr[func]))
            {
                var obj = region.FindNpc(npc);
                if (obj != null)
                {
                    var script = ScriptModule.Me.GetScript(obj.m_Script);

                    if (NpcStr[func][0] == '#')
                    {
                        var start = NpcStr[func].IndexOf('(');
                        var stop = NpcStr[func].IndexOf(')');

                        if (stop > start && start > 1)
                        {
                            var funcs = NpcStr[func].Substring(1, start - 1);
                            var strs = NpcStr[func].Substring(start + 1, stop - start - 1).Split(',');

                            var format = string.Empty; 
                            var objs = new List<object>();
                            try
                            {
                                foreach (var str in strs)
                                {
                                    try {
                                        int x = int.Parse(str);
                                        format += 'd';
                                        objs.Add(x);
                                    }  catch {
                                        format += 's';
                                        objs.Add(str);
                                    }
                                }

                                script.SetPlayer(id);
                                script.CallFunction(scene, funcs, 0, format, objs.ToArray());
                                script.SafeCall();
                            }
                            catch (Exception ex)
                            {
                                PhotonApp.log.Error(ex);
                            }
                        }
                    }
                    else
                    {
                        DoScript(obj.m_Script, NpcStr[func]);
                    }
                }
                else
                {
                    PhotonApp.log.ErrorFormat("NpcSelect {0} not found", npc);
                }
            }
            else
            {
                PhotonApp.log.ErrorFormat("Function {0} NpcSelect {1} is over", func, npc);
            }
        }
        public void NpcCallback(int npc, string func)
        {
            var obj = region.FindNpc(npc);
            if (obj != null)
            {
                m_nPeopleIdx = npc;
                DoScript(obj.m_Script, func);
            }
            else
            {
                PhotonApp.log.ErrorFormat("NpcCallback {0} not found", npc);
            }
        }
        LuaObj GetScript(uint str)
        {
            if (str != 0)
            {
                var obj = ScriptModule.Me.GetScript(str);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    PhotonApp.log.ErrorFormat("PlayerObj:GetScript {0} not found", ScriptModule.Me.GetPath(str));
                }
            }
            return null;
        }
        public void DoScript(uint str, string func)
        {
            var script = GetScript(str);
            if (script != null)
            {
                try
                {
                    script.SetPlayer(id);
                    script.CallFunction(scene, func, m_nPeopleIdx);
                }
                catch (Exception ex)
                {
                    PhotonApp.log.Error(ex);
                }
            }
        }
        public void DoScript(string str, string func)
        {
            var script = ScriptModule.Me.GetScript(str);
            if (script != null)
            {
                try
                {
                    script.SetPlayer(id);
                    script.CallFunction(scene, func, m_nPeopleIdx);
                }
                catch (Exception ex)
                {
                    PhotonApp.log.Error(ex);
                }
            }
            else
            {
                PhotonApp.log.ErrorFormat("PlayerObj:DoScript {0} not found", str);
            }
        }
        // for Client ///////////////////////////////////////////////////////
        public void MeRevive(bool place = false)
        {
            if (place)// hoi sinh tai cho
            {
                FightMode = true;
            }
            else
            {
                ChangeWorld(m_sDeathRevivalPos.m_nSubWorldID, m_sDeathRevivalPos.m_nMpsX, m_sDeathRevivalPos.m_nMpsY);
                FightMode = !SceneModule.Me.IsCity(m_sDeathRevivalPos.m_nSubWorldID);
            }
            HPCur = HPMax;
            MPCur = MPMax;
            SPCur = SPMax;
        }
        public void MeMove(int i, int x, int y)
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
                return;

            m_Doing = m_RunStatus ? NPCCMD.do_run : NPCCMD.do_walk;
            m_DestX = x; m_DestY = y;
            m_nPeopleIdx = 0;
        }
        public void MeSkill(int id, int npc)
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
                return;

            var skill = skillList.GetSkillById(id);
            if (skill != null)
            {
                m_nPeopleIdx = npc;

                m_ActiveSkillID = id;
                m_ActiveSkillLevel = 1;
                m_CurrentAttackRadius = 50;//skill.skillTemplate.m_nAttackRadius;

                SendSerCommand(NPCCMD.do_skill, id, -1, npc);
            }
        }
        public void StopMove()
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
                return;

            m_Doing = NPCCMD.do_stand;
        }
        public void DoSit()
        {
            if (m_Doing == NPCCMD.do_death || m_Doing == NPCCMD.do_revive)
                return;

            m_Doing = m_Doing == NPCCMD.do_stand ? NPCCMD.do_sit : NPCCMD.do_stand;
        }
        public void DoRun()
        {
            m_RunStatus = !m_RunStatus;
        }
        public void SendMsgSelf(string message) => SendMessage(string.Empty, message, PlayerChat.system);
        public void SendMessage(string sender, string message, PlayerChat type)
        {
            ClientPeer.SendEvent(new EventData
            {
                Code = (byte)OperationCode.DoChat,
                Parameters = new Dictionary<byte, object>
                {
                    { (byte)ParamterCode.CharacterName, sender },
                    { (byte)ParamterCode.Message, message },
                    { (byte)ParamterCode.Data, (byte)type },
                },
            }, sendParameters);
        }
        public void DoChat(string message, bool team)
        {
            if (team)
            {
                if (TeamId <= 0)
                    return;

                var obj = PlayerModule.Me.GetTeamObj(TeamId);
                if (obj != null)
                {
                    var player = PlayerModule.Me.GetPlayerObj(obj.Captain);
                    if (player != null)
                        player.SendMessage(Name, message, PlayerChat.team);

                    foreach (var id in obj.Members)
                    {
                        player = PlayerModule.Me.GetPlayerObj(id);
                        if (player != null)
                            player.SendMessage(Name, message, PlayerChat.team);
                    }
                }
            }
            else
            {
                region.SendMessage(this, message);
            }
        }


        public void AutoEquip(string itemId, bool isEquip)
        {
            try
            {
                ItemData equipItem = playerItems.First((el) => el.m_CommonAttrib.ItemDataId == itemId)?.ItemData;
                if (equipItem == null) return;
/*
                ItemData item = equipItem.Clone();
                ItemPosition sPlace = item.GetItemPlace();
                ItemPart sEquipPos = item.GetItemPart();
                ItemPart dEquipPos = item.GetItemPart();
                EquipDetailType equipDetailType = (EquipDetailType)item.Detailtype;
                bool isEquipping = equipItemDict.ContainsKey(dEquipPos);
                if (isEquip)
                {
                    if (isEquipping)
                    {
                        // Check ring slots
                        if (dEquipPos == ItemPart.itempart_ring1 && !equipItemDict.ContainsKey(ItemPart.itempart_ring2))
                        {
                            dEquipPos = ItemPart.itempart_ring2;
                        }
                        else
                        {
                            // Remove equipping item
                            string equippingId = equipItemDict[sEquipPos];
                            AutoEquip(equippingId, false);
                        }
                    }
                    equipItemDict[dEquipPos] = item.Id;
                    item.Local = (int)ItemPosition.pos_equip;
                    // TODO: Check durability
                }
                else
                {
                    if (isEquipping)
                    {
                        ItemPart part = equipItemDict.FirstOrDefault(x => x.Value == item.Id).Key;
                        if (part != ItemPart.itempart_num)
                        {
                            item.Local = (int)ItemPosition.pos_equiproom;
                            equipItemDict.Remove(part);
                        }
                    }
                }
*/
                //if (equipItem.Local != item.Local)
                //{
                    /* ClientPeer.SendOperationResponse(new OperationResponse()
                     {
                         OperationCode = (byte)OperationCode.AutoEquip,
                         Parameters = new Dictionary<byte, object>()
                         {
                             [(byte)ParamterCode.Data] = item.ToByteArray()
                         }
                     }, sendParameters);
                     CommonCodeReply result = GameDataRepository.Me.UpdateItemList(new List<ItemData> { item });
                     if (result.Code != ReturnCode.Ok)
                     {
                         PhotonApp.log.Error("unbale to update item");
                         return;
                     }*/
                    //equipItem.Local = item.Local;
                    //SendItem(item);
                //}
            }
            catch
            {
                PhotonApp.log.Error("AutoEquip: item not found id: " + itemId);
            }
        }
        protected bool CheckRoomPlaceAvailable(ItemData item)
        {
            return true;
        }
        protected bool CheckCanPlaceInEquipment(ItemData item)
        {
            return true;
        }
        public void ClientDisconnected()
        {
            var play = isPlaying;
            if (play)
            {
                if (character != null && scene != null && region != null)
                {
                    int x = 0, y = 0;
                    GetMpsPos(ref x, ref y);

                    character.MapX = x;
                    character.MapY = y;

                    SavePlayerToDB();
                }
                if (scene != null)
                {
                    scene.RemovePlayer(this);
                }
            }
            if (!isExit)
            {
                PhotonApp.LoginPeer.peer.SendOperation((byte)OperationCode.NotifyPlayer,
                    new Dictionary<byte, object>
                    {
                        {(byte)ParamterCode.CharacterId, cid},
                        {(byte)ParamterCode.Data, false},
                    }, ExitGames.Client.Photon.SendOptions.SendReliable);
            }
        }
        public void SavePlayerToDB()
        {
            try
            {
                DatabaseRepository.Me.SaveCharacter(cid, character);

                // Save items
                List<ItemData> updateItemList = playerItems
                    .Select((item) =>
                {
                    ItemData data = item.ItemData;
                    //data.Cid = character.Cid;
                    return data;
                }).ToList();

                DatabaseRepository.Me.UpdateItemList(updateItemList);
                playerItems.ForEach(item => ItemModule.Me.RemoveItem(item.m_CommonAttrib.SetIdx));
            }
            catch (Exception ex)
            {
                PhotonApp.log.Error(ex.Message);
            }
        }
        public void DoTask(int taskId, int taskValue)
        {
            // player test script
            string playertest = "\\script\\global\\playertest.lua";
            LuaObj script = ScriptModule.Me.GetScript(playertest);
            if (script == null)
            {
                return;
            }
            script.SetPlayer(id);
            script.CallFunction(null, "doTask");
        }
    }
}
