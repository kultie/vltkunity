using System.Collections.Generic;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Utils;
using System.Reflection;
using Photon.JXGameServer.Scripts;
using Photon.ShareLibrary.Entities;

namespace Photon.JXGameServer.Helpers
{
    public class NpcTemplate
    {
        public byte m_WalkSpeed;
        public byte m_RunSpeed;
        public byte m_AttackSpeed;
        public byte m_CastSpeed;

        public byte m_Stature;

        public byte m_Treasure;

        public int m_ActiveRadius;
        public byte m_VisionRadius;

        public byte m_HitRecover;
        public byte m_ReviveFrame;

        public int m_FireResist;
        public int m_FireResistMax;
        public int m_ColdResist;
        public int m_ColdResistMax;
        public int m_LightResist;
        public int m_LightResistMax;
        public int m_PoisonResist;
        public int m_PoisonResistMax;
        public int m_PhysicsResist;
        public int m_PhysicsResistMax;

        public int m_RedLum;
        public int m_GreenLum;
        public int m_BlueLum;

        public int m_Experience;
        public int m_LifeMax;
        public ushort m_LifeReplenish;
        public ushort m_AttackRating;
        public ushort m_Defend;

        public JX_KMagicAttrib m_PhysicsDamage = new JX_KMagicAttrib();

        public List<PlayerSkill> m_SkillList = new List<PlayerSkill>();

        int GetNpcLevelDataFromScript(LuaObj script,byte nSeries,byte nLevel,string szDataName,string szParam)
        {
            if (script.CallFunction(null, "GetNpcLevelData", 1, "ddss", nSeries, nLevel, szDataName, szParam))
                return script.GetTopValue;
            PhotonApp.log.InfoFormat("GetNpcLevelData '{0}' : '{1}' => {2} {3}", szDataName, szParam, nSeries, nLevel);
            script.SafeCall();
            return 0;
        }
        int GetNpcKeyDataFromScript(LuaObj script, byte nSeries, byte nLevel, string szDataName, double nParam1, double nParam2, double nParam3)
        {
            if (script.CallFunction(null, "GetNpcKeyData", 1, "ddsnnn", nSeries, nLevel, szDataName, nParam1, nParam2, nParam3))
                return script.GetTopValue;
            PhotonApp.log.InfoFormat("GetNpcKeyData '{0}' : '{1}' : {2} : {3} => {4} {5}", szDataName, nParam1, nParam2, nParam3, nSeries, nLevel);
            script.SafeCall();
            return 0;
        }
        public NpcTemplate(int nTemplateID, byte nLevel, byte nSeries) 
        {
            var rowIndex = nTemplateID + 2;

            m_Treasure = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.Treasure, rowIndex);
            m_WalkSpeed = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.WalkSpeed, rowIndex);
            m_RunSpeed = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.RunSpeed, rowIndex);
            m_AttackSpeed = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AttackSpeed, rowIndex);
            m_CastSpeed = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.CastSpeed, rowIndex);

            m_Stature = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.Stature, rowIndex);

            m_FireResistMax = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.FireResistMax, rowIndex);
            m_ColdResistMax = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.ColdResistMax, rowIndex);
            m_LightResistMax = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.LightResistMax, rowIndex);
            m_PoisonResistMax = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.PoisonResistMax, rowIndex);
            m_PhysicsResistMax = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.PhysicsResistMax, rowIndex);

            m_ActiveRadius = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.ActiveRadius, rowIndex);
            m_VisionRadius = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.VisionRadius, rowIndex);

            m_HitRecover = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.HitRecover, rowIndex);
            m_ReviveFrame = (byte)SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.ReviveFrame, rowIndex);

            m_Experience = 10;
            m_LifeMax = nLevel * 100;
            m_LifeReplenish = 1;
            m_AttackRating = 100;
            m_Defend = 10;

            m_PhysicsDamage.Init();
            m_PhysicsDamage.nValue[0] = 1;
            m_PhysicsDamage.nValue[2] = 5;

            var path = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.LevelScript, rowIndex);
            if (!string.IsNullOrEmpty(path))
            {
                var script = ScriptModule.Me.GetScript(path);
                if (script != null)
                {
                    string SkillValue;
                    int SkillId,SkillLevel;

                    SkillId = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.Skill1, rowIndex);
                    SkillValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.Level1, rowIndex);

                    if ((SkillId > 0) && !string.IsNullOrEmpty(SkillValue))
                    {
                        SkillLevel = GetNpcLevelDataFromScript(script, nSeries, nLevel, "Level1", SkillValue);

                        if (SkillLevel > 63)
                            SkillLevel = 63;
                        if (SkillLevel <= 0)
                            SkillLevel = 1;

                        m_SkillList.Add(new PlayerSkill { id = (ushort)SkillId, level = (byte)SkillLevel, exp = 0 });
                    }
                    else
                    {
                        m_SkillList.Add(null);
                    }

                    SkillId = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.Skill2, rowIndex);
                    SkillValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.Level2, rowIndex);

                    if ((SkillId > 0) && !string.IsNullOrEmpty(SkillValue))
                    {
                        SkillLevel = GetNpcLevelDataFromScript(script, nSeries, nLevel, "Level2", SkillValue);

                        if (SkillLevel > 63)
                            SkillLevel = 63;
                        if (SkillLevel <= 0)
                            SkillLevel = 1;

                        m_SkillList.Add(new PlayerSkill { id = (ushort)SkillId, level = (byte)SkillLevel, exp = 0 });
                    }
                    else
                    {
                        m_SkillList.Add(null);
                    }

                    SkillId = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.Skill3, rowIndex);
                    SkillValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.Level3, rowIndex);

                    if ((SkillId > 0) && !string.IsNullOrEmpty(SkillValue))
                    {
                        SkillLevel = GetNpcLevelDataFromScript(script, nSeries, nLevel, "Level3", SkillValue);

                        if (SkillLevel > 63)
                            SkillLevel = 63;
                        if (SkillLevel <= 0)
                            SkillLevel = 1;

                        m_SkillList.Add(new PlayerSkill { id = (ushort)SkillId, level = (byte)SkillLevel, exp = 0 });
                    }
                    else
                    {
                        m_SkillList.Add(null);
                    }

                    SkillId = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.Skill4, rowIndex);
                    SkillValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.Level4, rowIndex);

                    if ((SkillId > 0) && !string.IsNullOrEmpty(SkillValue))
                    {
                        SkillLevel = GetNpcLevelDataFromScript(script, nSeries, nLevel, "Level4", SkillValue);

                        if (SkillLevel > 63)
                            SkillLevel = 63;
                        if (SkillLevel <= 0)
                            SkillLevel = 1;

                        m_SkillList.Add(new PlayerSkill { id = (ushort)SkillId, level = (byte)SkillLevel, exp = 0 });
                    }
                    else
                    {
                        m_SkillList.Add(null);
                    }

                    SkillId = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.AuraSkillId, rowIndex);
                    SkillValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.AuraSkillLevel, rowIndex);

                    if ((SkillId > 0) && !string.IsNullOrEmpty(SkillValue))
                    {
                        SkillLevel = GetNpcLevelDataFromScript(script, nSeries, nLevel, "AuraSkillLevel", SkillValue);

                        if (SkillLevel > 63)
                            SkillLevel = 63;
                        if (SkillLevel <= 0)
                            SkillLevel = 1;

                        m_SkillList.Add(new PlayerSkill { id = (ushort)SkillId, level = (byte)SkillLevel, exp = 0 });
                    }
                    else
                    {
                        m_SkillList.Add(null);
                    }

                    SkillId = SceneModule.Me.NpcTable.Get<int>((int)Npcs_Index.PasstSkillId, rowIndex);
                    SkillValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.PasstSkillLevel, rowIndex);

                    if ((SkillId > 0) && !string.IsNullOrEmpty(SkillValue))
                    {
                        SkillLevel = GetNpcLevelDataFromScript(script, nSeries, nLevel, "PasstSkillLevel", SkillValue);

                        if (SkillLevel > 63)
                            SkillLevel = 63;
                        if (SkillLevel <= 0)
                            SkillLevel = 1;

                        m_SkillList.Add(new PlayerSkill { id = (ushort)SkillId, level = (byte)SkillLevel, exp = 0 });
                    }
                    else
                    {
                        m_SkillList.Add(null);
                    }

                    //---------------------------------------------------------------------------------------------

                    float nParam, nParam1, nParam2, nParam3;
                    nParam = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ExpParam, rowIndex, 1f);
                    nParam1 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ExpParam1, rowIndex);
                    nParam2 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ExpParam2, rowIndex);
                    nParam3 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ExpParam3, rowIndex);

                    m_Experience = (int)(nParam * GetNpcKeyDataFromScript(script, nSeries, nLevel, "Exp", nParam1, nParam2, nParam3) / 100);
                    if (m_Experience <= 0) m_Experience = 10;

                    nParam1 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.LifeParam1, rowIndex);
                    nParam2 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.LifeParam2, rowIndex);
                    nParam3 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.LifeParam3, rowIndex);

                    m_LifeMax = (int)GetNpcKeyDataFromScript(script, nSeries, nLevel, "Life", nParam1, nParam2, nParam3);
                    if (m_LifeMax <= 0) m_LifeMax = 100 * nLevel;

                    var szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.LifeReplenish, rowIndex);

                    m_LifeReplenish = (ushort)GetNpcLevelDataFromScript(script, nSeries, nLevel, "LifeReplenish", szValue);
                    if (m_LifeReplenish <= 0) m_LifeReplenish = 1;

                    nParam = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ARParam, rowIndex, 1f);
                    nParam1 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ARParam1, rowIndex);
                    nParam2 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ARParam2, rowIndex);
                    nParam3 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.ARParam3, rowIndex);

                    m_AttackRating = (ushort)(nParam * GetNpcKeyDataFromScript(script, nSeries, nLevel, "AttackRating", nParam1, nParam2, nParam3) / 100);
                    if (m_AttackRating <= 0) m_AttackRating = 100;

                    nParam = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.DefenseParam, rowIndex, 1f);
                    nParam1 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.DefenseParam1, rowIndex);
                    nParam2 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.DefenseParam2, rowIndex);
                    nParam3 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.DefenseParam3, rowIndex);

                    m_Defend = (ushort)(nParam * GetNpcKeyDataFromScript(script, nSeries, nLevel, "Defense", nParam1, nParam2, nParam3) / 100);
                    if (m_Defend <= 0) m_Defend = 10;

                    nParam = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MinDamageParam, rowIndex, 1f);
                    nParam1 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MinDamageParam1, rowIndex);
                    nParam2 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MinDamageParam2, rowIndex);
                    nParam3 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MinDamageParam3, rowIndex);

                    m_PhysicsDamage.nValue[0] = (int)(nParam * GetNpcKeyDataFromScript(script, nSeries, nLevel, "MinDamage", nParam1, nParam2, nParam3) / 100);
                    if (m_PhysicsDamage.nValue[0] <= 0) m_PhysicsDamage.nValue[0] = 1;

                    nParam = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MaxDamageParam, rowIndex, 1f);
                    nParam1 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MaxDamageParam1, rowIndex);
                    nParam2 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MaxDamageParam2, rowIndex);
                    nParam3 = SceneModule.Me.NpcTable.Get<float>((int)Npcs_Index.MaxDamageParam3, rowIndex);

                    m_PhysicsDamage.nValue[2] = (int)(nParam * GetNpcKeyDataFromScript(script, nSeries, nLevel, "MaxDamage", nParam1, nParam2, nParam3) / 100);
                    if (m_PhysicsDamage.nValue[2] <= 0) m_PhysicsDamage.nValue[2] = 5;

                    //---------------------------------------------------------------------------------------------

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.RedLum, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_RedLum = 0;
                    else
                        m_RedLum = GetNpcLevelDataFromScript(script, nSeries, nLevel, "RedLum", szValue);

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.GreenLum, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_RedLum = 0;
                    else
                        m_GreenLum = GetNpcLevelDataFromScript(script, nSeries, nLevel, "GreenLum", szValue);

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.BlueLum, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_RedLum = 0;
                    else
                        m_BlueLum = GetNpcLevelDataFromScript(script, nSeries, nLevel, "BlueLum", szValue);

                    //---------------------------------------------------------------------------------------------

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.FireResist, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_FireResist = 0;
                    else
                        m_FireResist = GetNpcLevelDataFromScript(script, nSeries, nLevel, "FireResist", szValue);

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.ColdResist, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_FireResist = 0;
                    else
                        m_ColdResist = GetNpcLevelDataFromScript(script, nSeries, nLevel, "ColdResist", szValue);

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.LightResist, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_LightResist = 0;
                    else
                        m_LightResist = GetNpcLevelDataFromScript(script, nSeries, nLevel, "LightResist", szValue);

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.PoisonResist, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_PoisonResist = 0;
                    else
                        m_PoisonResist = GetNpcLevelDataFromScript(script, nSeries, nLevel, "PoisonResist", szValue);

                    szValue = SceneModule.Me.NpcTable.Get<string>((int)Npcs_Index.PhysicsResist, rowIndex);
                    if (string.IsNullOrEmpty(szValue))
                        m_PhysicsResist = 0;
                    else
                        m_PhysicsResist = GetNpcLevelDataFromScript(script, nSeries, nLevel, "PhysicsResist", szValue);
                }
            }
        }
    }

    public class NpcTableData
    {
        public string Name;
        public string Kind;
        public string Camp;
        public string Series;
        public string Treasure;
        public string HeadImage;
        public string ClientOnly;
        public string CorpseIdx;
        public string RedLum;
        public string GreenLum;
        public string BlueLum;
        public string NpcResType;
        public string ArmorType;
        public string HelmType;
        public string WeaponType;
        public string HorseType;
        public string RideHorse;
        public string StandFrame;
        public string StandFrame1;
        public string DeathFrame;
        public string WalkFrame;
        public string RunFrame;
        public string HurtFrame;
        public string Skill1;
        public string Level1;
        public string Skill2;
        public string Level2;
        public string Skill3;
        public string Level3;
        public string Skill4;
        public string Level4;
        public string ActionScript;
        public string LevelScript;
        public string ExpParam;
        public string ExpParam1;
        public string ExpParam2;
        public string ExpParam3;
        public string LifeParam;
        public string LifeParam1;
        public string LifeParam2;
        public string LifeParam3;
        public string LifeReplenish;
        public string ARParam;
        public string ARParam1;
        public string ARParam2;
        public string ARParam3;
        public string DefenseParam;
        public string DefenseParam1;
        public string DefenseParam2;
        public string DefenseParam3;
        public string MinDamageParam;
        public string MinDamageParam1;
        public string MinDamageParam2;
        public string MinDamageParam3;
        public string MaxDamageParam;
        public string MaxDamageParam1;
        public string MaxDamageParam2;
        public string MaxDamageParam3;
        public string WalkSpeed;
        public string RunSpeed;
        public string AttackSpeed;
        public string CastSpeed;
        public string VisionRadius;
        public string HitRecover;
        public string ActiveRadius;
        public string AIMode;
        public string AIParam1;
        public string AIParam2;
        public string AIParam3;
        public string AIParam4;
        public string AIParam5;
        public string AIParam6;
        public string AIParam7;
        public string AIParam8;
        public string AIParam9;
        public string FireResist;
        public string ColdResist;
        public string LightResist;
        public string PoisonResist;
        public string PhysicsResist;
        public string FireResistMax;
        public string ColdResistMax;
        public string LightResistMax;
        public string PoisonResistMax;
        public string PhysicsResistMax;
        public string ReviveFrame;
        public string Stature;
        public string DropRateFile;
        public string AIMaxTime;
        public string PhysicalDamageBase;
        public string PhysicalMagicBase;
        public string PoisonDamageBase;
        public string PoisonMagicBase;
        public string ColdDamageBase;
        public string ColdMagicBase;
        public string FireDamageBase;
        public string FireMagicBase;
        public string LightingDamageBase;
        public string LightingMagicBase;
        public string AuraSkillId;
        public string AuraSkillLevel;
        public string PasstSkillId;
        public string PasstSkillLevel;

        private int rowIndex = 1;
        public int TemplateId { get => (rowIndex - 2); }
        public NpcTableData(int rowIndex)
        {
            this.rowIndex = rowIndex;
            Load(rowIndex);
        }
        public void Load(int rowIndex)
        {
            FieldInfo[] fields = GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];
                string val = SceneModule.Me.NpcTable.GetString(i, rowIndex - 1);
                field.SetValue(this, val);
            }
        }
    }
}
