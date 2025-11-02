using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Photon.JXLoginServer.Entitys
{
    public class RoleTemplate
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        byte ifiveprop;

        byte ileftprop;
        byte ileftfight;

        byte ipower;
        byte iagility;
        byte iouter;
        byte iinside;

        byte iluck;

        byte ifightexp;
        byte ifightlevel;
        byte ileadlevel;
        byte ileadexp;

        ushort imoney;
        ushort isavemoney;

        ushort imaxlife;
        ushort imaxinner;
        ushort imaxstamina;

        List<SkillTemplate> skills = new List<SkillTemplate>();
        List<ItemTemplate> items = new List<ItemTemplate>();

        public RoleTemplate(byte id)
        {
            var path = Path.Combine(PhotonApp.Instance.ApplicationPath, $"settings/npc/player/newplayerini{id.ToString("00")}.ini");

            ifiveprop = (byte)GetPrivateProfileInt("ROLE", "ifiveprop", 0, path);

            ileftprop = (byte)GetPrivateProfileInt("ROLE", "ileftprop", 0, path);
            ileftfight = (byte)GetPrivateProfileInt("ROLE", "ileftfight", 0, path);

            ipower = (byte)GetPrivateProfileInt("ROLE", "ipower", 0, path);
            iagility = (byte)GetPrivateProfileInt("ROLE", "iagility", 0, path);
            iouter = (byte)GetPrivateProfileInt("ROLE", "iouter", 0, path);
            iinside = (byte)GetPrivateProfileInt("ROLE", "iinside", 0, path);

            iluck = (byte)GetPrivateProfileInt("ROLE", "iluck", 0, path);

            ifightexp = (byte)GetPrivateProfileInt("ROLE", "ifightexp", 0, path);
            ifightlevel = (byte)GetPrivateProfileInt("ROLE", "ifightlevel", 0, path);
            ileadexp = (byte)GetPrivateProfileInt("ROLE", "ileadexp", 0, path);
            ileadlevel = (byte)GetPrivateProfileInt("ROLE", "ileadlevel", 0, path);

            imoney = (ushort)GetPrivateProfileInt("ROLE", "imoney", 0, path);
            isavemoney = (ushort)GetPrivateProfileInt("ROLE", "isavemoney", 0, path);

            imaxlife = (ushort)GetPrivateProfileInt("ROLE", "imaxlife", 0, path);
            imaxinner = (ushort)GetPrivateProfileInt("ROLE", "imaxinner", 0, path);
            imaxstamina = (ushort)GetPrivateProfileInt("ROLE", "imaxstamina", 0, path);

            byte i,count = (byte)GetPrivateProfileInt("FSKILLS", "COUNT", 0, path);
            for (i = 1; i <= count; ++i)
            {
                var skill = new SkillTemplate();
                skill.s = (ushort)GetPrivateProfileInt("FSKILLS", $"S{i}", 0, path);
                skill.l = (byte)GetPrivateProfileInt("FSKILLS", $"L{i}", 0, path);
                skills.Add(skill);
            }

            count = (byte)GetPrivateProfileInt("ITEMS", "COUNT", 0, path);
            for (i = 1; i <= count; ++i)
            {
                items.Add(new ItemTemplate(path, i));
            }
        }
        public void Load(CharacterData character)
        {
            character.Fiveprop = ifiveprop;//Series

            character.LeftProp = ileftprop; // diem tiem nang chua phan phoi
            character.LeftFight = ileftfight; // diem skill chua phan phoi

            character.Power = ipower;// suc manh
            character.Agility = iagility;// than phap
            character.Outer = iouter; // sinh khi
            character.Inside = iinside; // noi cong

            character.Luck = iluck;

            character.FightExp = ifightexp;
            character.FightLevel = ifightlevel;
            character.LeadExp = ileadexp;
            character.LeadLevel = ileadlevel;

            character.Sect = 0;// mon phai
            character.FirstSect = 0;
            character.JoinCount = 0;

            character.MaxLife = imaxlife;//HP
            character.CurLife = imaxlife;
            character.MaxInner = imaxinner;//MP
            character.CurInner = imaxinner;
            character.MaxStamina = imaxstamina;//SP
            character.CurStamina = imaxstamina;

            character.Money = imoney;
            character.SaveMoney = isavemoney;// tien trong guong

            character.CurNpcTitle = 0;// danh hieu
        
            character.SectRole = 0;// hang 
            character.Rankrole = 0;
            character.Exboxrole = 0; // ruong mo rong
            character.Exitemrole = 0;

            character.FightMode = false;
            character.Camp = (byte)NPCCAMP.camp_begin;
            character.PkStatus = 0;// tinh trang PK
            character.Pkvalue = 0;// diem PK
            character.Reputevalue = 0;// diem danh vong
            character.Fuyuanvalue = 0;// diem phuc duyen
            character.Rebornvalue = 0;// diem trung sinh
            character.TongID = 0;// bang hoi

            character.RoleParm1 = 0;// kinh nghiem x lan
            character.RoleParm2 = 0;// may man x lan
            character.RoleParm3 = 0;
            character.RoleParm4 = 0;// thoi gian roi game
            character.RoleParm5 = 0;// so lan doi ten

            character.MapId = 0; character.MapX = character.MapY = 0; //vi tri hien tai
        }
        public void SaveItemSkill(uint cid)
        {
            foreach (var skill in skills)
            {

            };
            foreach (var item in items)
            {
                item.Save(cid);
            }
        }
    }
}
