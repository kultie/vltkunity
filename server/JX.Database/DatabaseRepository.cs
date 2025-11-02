using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Photon.ShareLibrary.Entities;
using JX.Database.Model;
using System.Threading.Tasks;
using NHibernate.Mapping;
using NHibernate.Criterion;
using System.Runtime.InteropServices;
using System.Linq;

namespace JX.Database
{
    public class PlayerDatas
    {
        public CharacterData character;
        public List<PlayerSkill> skills;
        public List<ItemData> items;
        public List<PlayerTask> tasks;
        public List<PlayerFriend> friends;
    };
    public class DatabaseRepository
    {
        public static DatabaseRepository Me;

        private ISessionFactory _sessionFactory;
        public DatabaseRepository(string path)
        {
            Me = this;

            var exeConfigPath = string.Format("{0}/{1}", path, Path.GetFileName(this.GetType().Assembly.Location));

            var config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);

            var myValue = GetAppSetting(config, "mysql");

            _sessionFactory = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(c => c.Is(myValue)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DatabaseRepository>())
                .BuildSessionFactory();
        }
        string GetAppSetting(Configuration config, string key)
        {
            KeyValueConfigurationElement element = config.AppSettings.Settings[key];
            if (element != null)
            {
                string value = element.Value;
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
            return string.Empty;
        }
        public bool RegisterAccount(string account, string password, ref int uid)
        {
            return true;
        }
        public uint CreateCharacter(uint uid, byte factionId, string name, bool sex)
        {
            return 0;
        }
        public List<CharacterLogin> Login(string account, string password, ref uint uid)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var user = session.CreateCriteria(typeof(AccountModel))
                    .Add(Expression.Sql("username= ?", account, NHibernateUtil.String))
                    .UniqueResult<AccountModel>();
                if (user == null)
                    return null;

                uid = user.Id;
                var chars = new List<CharacterLogin>();

                var temps = session.CreateCriteria(typeof(CharacterModel))
                    .Add(Expression.Sql("account_id= ?", uid, NHibernateUtil.UInt64))
                    .List<CharacterModel>();
                foreach (var t in temps)
                    chars.Add(new CharacterLogin
                    {
                        Id = t.Id,
                        Name = t.szName,
                        Series = t.ifiveprop,
                        Avatar = t.iavatar,
                        Rank = t.irank,
                        Sex = t.bSex,
                        Faction = (byte)(1 + t.nSect),
                        Level = t.ifightlevel
                    });
                return chars;
            }
        }
        int getPlayerMap(CharacterModel role)
        {
            if (role.iRoleParm6 != 0 && role.iRoleParm6 != role.isWaiGua)
                return role.iRoleParm6;
            else
            if (role.cUseRevive)
                return role.irevivalid;
            else
                return role.ientergameid;
        }
        public CharacterPosition getPlayerPosition(uint uid,uint cid)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var result = session.CreateMultiCriteria()
                    .Add(session.QueryOver<AccountModel>().Where(a => a.Id == uid))
                    .Add(session.QueryOver<CharacterModel>().Where(a => a.Id == cid))
                    .List();

                var users = result[0] as List<AccountModel>;
                var chars = result[1] as List<CharacterModel>;

                if ((chars == null) || chars.Count == 0)
                    return null;
                    
                return new CharacterPosition
                {
                    Account = users.FirstOrDefault().Account,
                    MapId = (ushort)getPlayerMap(chars.FirstOrDefault()),
                };
            }
        }
        public PlayerDatas getPlayerData(uint uid, uint cid)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var result = session.CreateMultiCriteria()
                    .Add(session.QueryOver<CharacterModel>().Where(a => a.Id == cid))
                    .Add(session.QueryOver<SkillModel>().Where(a => a.Character_Id == cid))
                    .Add(session.QueryOver<ItemModel>().Where(a => a.Character_Id == cid))
                    .Add(session.QueryOver<TaskModel>().Where(a => a.Character_Id == cid))
                    .Add(session.QueryOver<FriendModel>().Where(a => a.Character_Id == cid || a.Friend_Id == cid))
                    .List();

                var chars = (result[0] as List<CharacterModel>).FirstOrDefault();
                var skills = result[1] as List<SkillModel>;
                var items = result[2] as List<ItemModel>;
                var tasks = result[3] as List<TaskModel>;
                var friends = result[4] as List<FriendModel>;

                IList<CharacterModel> listfriend = null;
                if (friends.Count > 0)
                {
                    var ids = string.Join(",",friends.Select(x => cid == x.Character_Id ? x.Friend_Id : x.Character_Id).ToList());
                    listfriend = session.CreateCriteria(typeof(CharacterModel))
                        .Add(Expression.Sql("id in (?)", $"{ids}", NHibernateUtil.String))
                        .List<CharacterModel>();
                    //listfriend = session.CreateQuery($"select * from characters where id in ({ids})")
                    //    .List<CharacterModel>();
                }
                return new PlayerDatas
                {
                    character = new CharacterData
                    {
                        Avatar = chars.iavatar,
                        Name = chars.szName,

                        Fiveprop = chars.ifiveprop,
                        Sex = chars.bSex,

                        LeftProp = chars.ileftprop,
                        LeftFight = chars.ileftfight,

                        Power = chars.ipower,
                        Agility = chars.iagility,
                        Outer = chars.iouter,
                        Inside = chars.iinside,

                        Luck = chars.iluck,

                        FightExp = chars.ifightexp,
                        FightLevel = chars.ifightlevel,
                        LeadExp = chars.ileadexp,
                        LeadLevel = chars.ileadlevel,

                        Sect = (byte)(1 + chars.nSect),
                        FirstSect = (byte)(1 + chars.nFirstSect),
                        JoinCount = chars.ijoincount,

                        MaxLife = chars.imaxlife,
                        CurLife =chars.icurlife,
                        MaxInner = chars.imaxinner,
                        CurInner = chars.icurinner,
                        MaxStamina = chars.imaxstamina,
                        CurStamina = chars.icurstamina,

                        Money = chars.iclientx,
                        SaveMoney = chars.isavemoney,

                        CurNpcTitle = chars.iPltitle,
                        SectRole = chars.isectrole,
                        Rankrole = chars.irankrole,
                        Exboxrole = chars.iexboxrole,
                        Exitemrole = chars.iexitemrole,

                        FightMode = chars.cFightMode,
                        Camp = chars.iteam,

                        PkStatus = chars.cPkStatus,
                        Pkvalue = chars.ipkvalue,

                        Reputevalue = chars.ireputevalue,
                        Fuyuanvalue = chars.ifuyuanvalue,
                        Rebornvalue = chars.irebornvalue,

                        TongID = chars.dwTongID,

                        MapId = chars.cUseRevive ? chars.irevivalid : chars.ientergameid,
                        MapX = chars.cUseRevive ? chars.irevivalx : chars.ientergamex,
                        MapY = chars.cUseRevive ? chars.irevivaly : chars.ientergamey,
                    },
                    skills = skills.Select(x => new PlayerSkill { id = x.Skill_Id, level = x.Skill_Level, exp = x.Skill_Exp}).ToList(),
                    items = new List<ItemData>
                    {

                    },
                    tasks = tasks.Select(x => new PlayerTask { id = x.Task_Id, value = x.Task_Value}).ToList(),
                    friends = listfriend == null ? new List<PlayerFriend>() : listfriend.Select(x => new PlayerFriend { id = x.Id, name = x.szName}).ToList(),
                };
            }
        }
        public void SaveCharacter(uint cid,CharacterData data)
        {
        }
        public void UpdateItemList(List<ItemData> data)
        {

        }
    }
}
