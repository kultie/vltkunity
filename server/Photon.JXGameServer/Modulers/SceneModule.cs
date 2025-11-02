using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Items;
using Photon.JXGameServer.Maps;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Photon.JXGameServer.Modulers
{
    public class SceneModule
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public static SceneModule Me;

        JX_Table MoneyTable;

        JX_Table ObjTable;
        JX_Ini ObjTableColor;

        public JX_Table NpcTable;
        public JX_Table NpcTableGold;
        public JX_Ini g_NpcMapDropRate;

        public List<SceneObj> Scenes
        {
            get { return m_scenes; }
        }
        List<SceneObj> m_scenes;

        string[] g_szObjKind = new string[]
        {
            "MapObj",
            "Body",
            "Box",
            "Item",
            "Money",
            "LoopSound",
            "RandSound",
            "Light",
            "Door",
            "Trap",
            "Prop"
        };

        public Dictionary<string, NpcTemplate> NpcTemplates;
        Dictionary<uint, KItemDropRate> NpcDropItems;

        IniFile revivepos;
        public SceneModule() 
        {
            Me = this;

            m_scenes = new List<SceneObj>();

            NpcTemplates = new Dictionary<string, NpcTemplate>();
            NpcDropItems = new Dictionary<uint, KItemDropRate>();
        }

        public void LoadConfig(string root)
        {
            int c = JXHelper.LoadPackages(root);
            PhotonApp.log.InfoFormat("LoadPackages: {0}", c);

            if (c == 0)
                return;

            MoneyTable = new JX_Table(Path.Combine(root, "settings/obj/moneyobj.txt"));

            ObjTable = new JX_Table(Path.Combine(root, "settings/obj/objdata.txt"));
            ObjTableColor = new JX_Ini(Path.Combine(root, "settings/obj/ObjNameColor.ini"));

            NpcTable = new JX_Table(Path.Combine(root, "settings/npcs.txt"));
            NpcTableGold = new JX_Table(Path.Combine(root, "settings/npc/npcgoldtemplate.txt"));

            var pathWorld = new IniFile(Path.Combine(root, "WorldSet.ini"));
            var pathMap = Path.Combine(root, "settings/maplist.ini");

            g_NpcMapDropRate = new JX_Ini(pathMap);
            revivepos = new IniFile(Path.Combine(root, "settings/revivepos.ini")); 

            try
            {
                var n = pathWorld.ReadInteger("Init", "Count", 0);
                var str = new StringBuilder();
                var map = new Dictionary<int,string>();

                c = 0;
                while (c < n)
                {
                    var temp = pathWorld.ReadString("World", "World" + c.ToString(c < 100 ? "00" : "000"));
                    if (!string.IsNullOrEmpty(temp))
                    {
                        int id = temp.IndexOf(';');
                        if (id > 0) temp = temp.Substring(0, id).Trim();
                        id = temp.IndexOf('-');
                        if (id > 0) temp = temp.Substring(0, id).Trim();

                        if (!string.IsNullOrEmpty(temp))
                        {
                            id = int.Parse(temp);

                            if (!map.ContainsKey(id))
                            {
                                str.Clear();
                                if (GetPrivateProfileString("List", id.ToString(), string.Empty, str, 256, pathMap) > 0)
                                {
                                    map.Add(id, str.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    ++c;
                }
                PhotonApp.log.InfoFormat("LoadMaps: found {0} maps", map.Count);

                foreach (var kvp in map)
                {
                    var scene = new SceneObj((ushort)kvp.Key, (ushort)m_scenes.Count);
                    m_scenes.Add(scene);

                    scene.nNormalDropRate = LoadItemDrop(g_NpcMapDropRate.Get<string>("List", $"{kvp.Key}_NormalDropRate"));
                    scene.nGoldenDropRate = LoadItemDrop(g_NpcMapDropRate.Get<string>("List", $"{kvp.Key}_GoldenDropRate"));

                    if (scene.LoadMap(kvp.Value) == false)
                    {
                        m_scenes.Remove(scene);
                    }
                }

                PhotonApp.log.InfoFormat("LoadMaps: total {0} maps loaded", m_scenes.Count);
            }
            catch (Exception e)
            {
                PhotonApp.log.Error(e.InnerException ?? e);
            }
        }
        public void HeartBeat()
        {
            foreach (var obj in m_scenes)
            {
                try
                {
                    obj.HeartBeat();
                } catch (Exception e)
                {
                    PhotonApp.log.Error(e);
                }
            }
        }
        public bool IsCity(int id)
        {
            var str = g_NpcMapDropRate.Get<string>("List", $"{id}_MapType");
            return str == "City" || str == "Country" || str == "Capital";
        }
        public SceneObj FindMapId(int mapid)
        {
            foreach (var obj in m_scenes)
            {
                if (obj.MapId == mapid)
                    return obj;
            }
            return null;
        }
        public int FindMapIndex(int mapid)
        {
            for (int i = 0 ; i < m_scenes.Count; i++)
            {
                if (m_scenes[i].MapId == mapid)
                {
                    return i;
                }
            }
            return -1;
        }
        public byte GetLifeTime(int nDataID) => (byte)ObjTable.Get<int>(((int)ObjData_Index.ObjDataField_LifeTime), nDataID);
        public byte GetObjectDir(int nDataID) => (byte)ObjTable.Get<int>(((int)ObjData_Index.ObjDataField_ImageCurDir), nDataID);
        public ObjKind GetObjectKind(int nDataID)
        {
            if (nDataID <= 0 || nDataID >= ObjTable.RowCount)
                return ObjKind.Obj_Kind_Num;

            var szBuffer = ObjTable.Get<string>(((int)ObjData_Index.ObjDataField_Kind),nDataID);

            for (var i = 0; i < (byte)ObjKind.Obj_Kind_Num; ++i)
            {
                if (szBuffer == g_szObjKind[i])
                    return (ObjKind)i;
            }
            return ObjKind.Obj_Kind_Num;
        }
        public int GetMoneyDataId(int nMoney)
        {
            for (int i = 1; i <= MoneyTable.RowCount; ++i)
            {
                int nMoneyLevel = MoneyTable.Get<int>(0, i);
                if (nMoney < nMoneyLevel)
                {
                    return MoneyTable.Get<int>(1, i);
                }
            }
            return 0;
        }
        public uint LoadItemDrop(string path)
        {
            var id = JXHelper.FileNameToId(path);

            if (!NpcDropItems.ContainsKey(id))
            {
                var list = new KItemDropRate();
                NpcDropItems.Add(id, list);

                list.Load(PhotonApp.Instance.ApplicationPath + path);
            }

            return id;
        }
        public int ObjNameColor(byte i)
        {
            var r = ObjTableColor.Get<int>(i.ToString(), "R");
            var g = ObjTableColor.Get<int>(i.ToString(), "G");
            var b = ObjTableColor.Get<int>(i.ToString(), "B");

            return (r << 16) | (g << 8) | b;
        }
        public KItemDropRate GetItemDrop(uint id)
        {
            if (NpcDropItems.ContainsKey(id))
                return NpcDropItems[id];
            else
                return null;
        }

        public void GetRevivalPosFromId(int dwSubWorldId, int nRevivalId, ref int x, ref int y) => revivepos.GetRevivalPosFromId(dwSubWorldId, nRevivalId, ref x, ref y);
        public void GetRevivalPosRandIdx(int dwSubWorldId, ref int r, ref int x, ref int y) => revivepos.GetRevivalPosRandIdx(dwSubWorldId, ref r, ref x, ref y);
    }
}
