using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Handlers;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;
using ExitGames.Logging;
using LogManager = ExitGames.Logging.LogManager;
using System.Reflection;
using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Items;
using Photon.ShareLibrary.Constant;

namespace Photon.JXGameServer.Scripts
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LuaFunc
    {
        public IntPtr name;
        public IntPtr func;
    }
    public static class LuaEntity
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static IntPtr MapId = IntPtr.Zero;
        public static IntPtr ScriptId = IntPtr.Zero;

        public static IntPtr WorldIndex = IntPtr.Zero;
        public static IntPtr ObjectIndex = IntPtr.Zero;
        public static IntPtr PlayerIndex = IntPtr.Zero;

        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern void KLuaScript_Load(IntPtr L, IntPtr path, bool include);


        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_rawget(IntPtr L, int idx);


        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_type(IntPtr L, int idx);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_isnumber(IntPtr L, int idx);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_isstring(IntPtr L, int idx);


        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_getglobal(IntPtr L, IntPtr idx);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_setglobal(IntPtr L, IntPtr idx);


        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_gettop(IntPtr L);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_settop(IntPtr L, int idx);


        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_tostring(IntPtr L, int idx);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_strlen(IntPtr L, int idx);

        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern double lua_tonumber(IntPtr L, int index);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushnumber(IntPtr L, double index);

        static int GetWorldIndex(IntPtr L) => GetGlobalIndex(L, WorldIndex);
        static int GetObjectIndex(IntPtr L) => GetGlobalIndex(L, ObjectIndex);
        static int GetPlayerIndex(IntPtr L) => GetGlobalIndex(L, PlayerIndex);

        static int GetGlobalIndex(IntPtr L, IntPtr name)
        {
            lua_getglobal(L, name);

            int idx = lua_gettop(L);
            if (lua_type(L, idx) == 1)
                return -1;

            return (int)lua_tonumber(L, idx);
        }
        public static int GetTopValue(IntPtr L)
        {
            int idx = lua_gettop(L);
            return (int)lua_tonumber(L, idx);
        }
        public static string GetTopString(IntPtr L)
        {
            int idx = lua_gettop(L);
            if (lua_isstring(L, idx) == 1)
            {
                return ToUtf8(L, idx);
            }

            if (lua_isnumber(L, idx) == 1)
            {
                return $"{(int)lua_tonumber(L, idx)}";
            }
            return string.Empty;
        }
        static string ToUtf8(IntPtr L, int idx)
        {
            var s = lua_strlen(L, idx);
            if (s > 0)
            {
                Marshal.Copy(lua_tostring(L, idx), JXHelper.bytes, 0, s);
                return Encoding.UTF8.GetString(JXHelper.bytes, 0, s);
            }
            return string.Empty;
        }
        static string TableToUtf8(IntPtr L, int basic, int index)
        {
            lua_pushnumber(L, index);
            lua_rawget(L, basic);

            return ToUtf8(L, lua_gettop(L));
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int LuaCallback(System.IntPtr L);

        public static List<string> Funcs = new List<string>()
        {
            "_ALERT",

            "print",

            "FileName2Id",

            "Include",
            "IncludeLib",

            "Sale",
            "Say",
            "Talk",

            "AddMagic",
            "AddMagicPoint",

            "AddLocalNews",
            "AddGlobalNews",

            "SetNpcName",
            "SetNpcTitle",
            "SetNpcScript",

            "SetPos",
            "GetLevel",
            "SetFightState",
            "GetFightState",

            "AddSkillState",
            "SetProtectTime",

            "OpenBox",
            "SetRevPos",

            "AddStation",
            "AddTermini",

            "UseTownPortal",
            "ReturnFromPortal",

            "NewWorld",
            "LeaveTeam",

            "GetTask",
            "SetTask",

            "Msg2Player",
            "SetPropState",

            "AddEventItem",
            "HaveItem",

            "SubWorldID2Idx",
            "AddNpc",

            "AddItem",
            "AddGoldItem"
        };

        static Dictionary<string, Delegate> callbacks = new Dictionary<string, Delegate>();
        public static IntPtr Init()
        {
            MapId = Marshal.StringToHGlobalAnsi("MapId");
            ScriptId = Marshal.StringToHGlobalAnsi("ScriptId");

            WorldIndex = Marshal.StringToHGlobalAnsi("WorldIndex");
            ObjectIndex = Marshal.StringToHGlobalAnsi("ObjectIndex");
            PlayerIndex = Marshal.StringToHGlobalAnsi("PlayerIndex");

            var type = typeof(LuaEntity);
            var size = Marshal.SizeOf(typeof(LuaFunc));

            var result = Marshal.AllocHGlobal(size * Funcs.Count);
            var offset = 0;

            LuaFunc temp = new LuaFunc();
            for (int i = 0; i < Funcs.Count; i++)
            {
                temp.name = Marshal.StringToHGlobalAnsi(Funcs[i]);

                MethodInfo methodInfo = type.GetMethod("Lua" + Funcs[i], BindingFlags.Static | BindingFlags.Public);
                if (methodInfo == null)
                {
                    log.InfoFormat("{0} not found", "Lua" + Funcs[i]);
                    temp.func = IntPtr.Zero;
                }
                else
                {
                    callbacks[Funcs[i]] = Delegate.CreateDelegate(typeof(LuaCallback), methodInfo);
                    temp.func = Marshal.GetFunctionPointerForDelegate(callbacks[Funcs[i]]);
                }

                Marshal.StructureToPtr(temp, result + offset, false);
                offset += size;
            }
            return result;
        }
        public static int Lua_ALERT(IntPtr L)
        {
            var path = ScriptModule.Me.GetPath((uint)GetGlobalIndex(L, ScriptId));
            log.Error(path + " " + ToUtf8(L, 1));
            return 0;
        }
        public static int Luaprint(IntPtr L)
        {
            var nParamNum = lua_gettop(L);

            List<string> strs = new List<string>();
            for (int i = 1; i <= nParamNum; i++)
            {
                strs.Add(ToUtf8(L, i));
            }
            log.Info(string.Join("\n", strs));
            return 0;
        }
        public static int LuaFileName2Id(IntPtr L)
        {
            uint v = 0;
            if (lua_gettop(L) >= 1)
            {
                var temp = ToUtf8(L, 1);
                v = JXHelper.FileNameToId(temp);
            }
            lua_pushnumber(L, (int)v);
            return 1;
        }

        public static int LuaInclude(IntPtr L)
        {
            var path = ToUtf8(L, 1);

            if (path.StartsWith("base64:"))
            {
                var bytes = Convert.FromBase64String(path.Substring(7));

                path = Encoding.UTF8.GetString(bytes);
            }

            if (!path.StartsWith("\\") && !path.StartsWith("/"))
                path = Path.Combine(PhotonApp.Instance.ApplicationPath, path);
            else
                path = PhotonApp.Instance.ApplicationPath + path;

            if (string.IsNullOrEmpty(Path.GetExtension(path)))
                path += ".lua";

            if (File.Exists(path))
            {
                var p = Marshal.StringToHGlobalUni(path);
                KLuaScript_Load(L, p, true);
                Marshal.FreeHGlobal(p);
            }
            else
            {
                log.ErrorFormat("Include {0} not found", path);
            }
            return 0;
        }
        public static int LuaIncludeLib(IntPtr L)
        {
            return 0;
        }

        static PlayerObj FindPlayer(IntPtr L)
        {
            var scene = GetWorldIndex(L);
            if ((scene >= 0) && (scene < SceneModule.Me.Scenes.Count))
            {
                return SceneModule.Me.Scenes[scene].FindPlayer(GetPlayerIndex(L));
            }
            return null;
        }
        static ObjectObj FindObject(IntPtr L)
        {
            var scene = GetWorldIndex(L);
            if ((scene >= 0) && (scene < SceneModule.Me.Scenes.Count))
            {
                return SceneModule.Me.Scenes[scene].FindObjectObj(GetObjectIndex(L));
            }
            return null;
        }
        static NpcObj FindNpc(IntPtr L)
        {
            var scene = GetWorldIndex(L);
            NpcObj npc = null;
            if ((scene >= 0) && (scene < SceneModule.Me.Scenes.Count))
            {
                npc = SceneModule.Me.Scenes[scene].FindNpc((int)lua_tonumber(L, 1));
            }
            return npc;
        }
        public static int LuaSale(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    var param = new Dictionary<byte, object>
                    {
                        { (byte)ParamterCode.Id, player.m_nPeopleIdx },
                        { (byte)ParamterCode.Data, (int)lua_tonumber(L, 1) }
                    };

                    player.ClientPeer.SendEvent(new EventData
                    {
                        Code = (byte)OperationCode.NpcSale,
                        Parameters = param
                    }, player.sendParameters);
                }
            }
            return 0;
        }
        public static int LuaSay(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.NpcStr.Clear();

                    var param = new Dictionary<byte, object>
                    {
                        { (byte)ParamterCode.Id, player.m_nPeopleIdx },
                        { (byte)ParamterCode.Name, ToUtf8(L, 1) }
                    };

                    if (nParamNum > 2)
                    {
                        nParamNum = (int)lua_tonumber(L, 2);
                        if (nParamNum > 0)
                        {
                            var bStringTab = (lua_type(L, 3) == 4);

                            string str = string.Empty, temp;
                            for (int i = 1; i <= nParamNum; i++)
                            {
                                if (bStringTab)
                                    temp = TableToUtf8(L, 3, i);
                                else
                                    temp = ToUtf8(L, 2 + i);

                                var idx = temp.IndexOf('/');
                                if (idx < 0)
                                {
                                    player.NpcStr.Add("");
                                    str += Utils.spstr + temp;
                                }
                                else
                                {
                                    str += Utils.spstr + temp.Substring(0, idx);
                                    player.NpcStr.Add(temp.Substring(idx + 1));
                                }
                            }
                            param.Add((byte)ParamterCode.Data, str.Substring(Utils.spstr.Length));
                        }
                        else
                        {
                            param.Add((byte)ParamterCode.Data, string.Empty);
                        }
                    }
                    else
                    {
                        param.Add((byte)ParamterCode.Data, string.Empty);
                    }

                    player.ClientPeer.SendEvent(new EventData
                    {
                        Code = (byte)OperationCode.NpcQuest,
                        Parameters = param
                    }, player.sendParameters);

                }
            }
            return 0;
        }
        public static int LuaTalk(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 2)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    nParamNum = (int)lua_tonumber(L, 1);

                    var param = new Dictionary<byte, object>
                    {
                        { (byte)ParamterCode.Id, player.m_nPeopleIdx },
                        { (byte)ParamterCode.Name, ToUtf8(L, 2) }
                    };

                    var str = "";
                    for (int i = 1; i <= nParamNum; i++)
                    {
                        var temp = ToUtf8(L, 2 + i);
                        str += Utils.spstr + temp;
                    }
                    param.Add((byte)ParamterCode.Data, str.Substring(Utils.spstr.Length));

                    player.ClientPeer.SendEvent(new EventData
                    {
                        Code = (byte)OperationCode.NpcTalk,
                        Parameters = param
                    }, player.sendParameters);
                }
            }
            return 0;
        }

        public static int LuaAddMagic(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 2)
            {
                var player = FindPlayer(L);
                if (player != null)
                {

                }
            }
            return 0;
        }
        public static int LuaAddMagicPoint(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {

                }
            }
            return 0;
        }

        public static int LuaAddLocalNews(IntPtr L)
        {
            PlayerModule.Me.Broadcast(ToUtf8(L, 1));
            return 0;
        }
        public static int LuaAddGlobalNews(IntPtr L)
        {
            var message = ToUtf8(L, 1);

            PlayerModule.Me.Broadcast(message);
            PhotonApp.SocialPeer.peer.SendOperation((byte)OperationCode.DoChat, new Dictionary<byte, object>
            {
                { (byte)ParamterCode.CharacterId, string.Empty },
                { (byte)ParamterCode.Message, message },
                { (byte)ParamterCode.ActionId, (byte)PlayerChat.system },
            }, ExitGames.Client.Photon.SendOptions.SendReliable);

            return 0;
        }
        public static int LuaSetNpcName(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 2)
            {
                var npc = FindNpc(L);
                if (npc != null)
                {
                    npc.SetName(ToUtf8(L, 2));
                }
            }
            return 0;
        }
        public static int LuaSetNpcTitle(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 2)
            {
                var npc = FindNpc(L);
                if (npc != null)
                {
                    npc.SetTitle(ToUtf8(L, 2));
                }
            }
            return 0;
        }
        public static int LuaSetNpcScript(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 2)
            {
                var npc = FindNpc(L);
                if (npc != null)
                {
                    var s = lua_strlen(L, 2);
                    Marshal.Copy(lua_tostring(L, 2), JXHelper.bytes, 0, s);
                    npc.SetScript(JXHelper.FileNameToId(JXHelper.bytes, s));
                }
            }
            return 0;
        }
        public static int LuaSetPos(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 2)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    var nX = (int)lua_tonumber(L, 1);
                    var nY = (int)lua_tonumber(L, 2);

                    player.SetPos(nX * 32, nY * 32);
                }
            }
            return 0;
        }
        public static int LuaGetLevel(IntPtr L)
        {
            int v = -1;
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    v = player.Level;
                }
            }
            lua_pushnumber(L, v);
            return 1;
        }
        public static int LuaSetFightState(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.FightMode = (int)lua_tonumber(L, 1) == 1;
                }
            }
            return 0;
        }
        public static int LuaGetFightState(IntPtr L)
        {
            int v = -1;
            var player = FindPlayer(L);
            if (player != null)
            {
                v = player.FightMode ? 1 : 0;
            }
            lua_pushnumber(L, v);
            return 1;
        }
        public static int LuaAddSkillState(IntPtr L)
        {
            if (lua_gettop(L) >= 4)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    //player.m_ProtectTime = (byte)(int)lua_tonumber(L, 1);
                }
            }
            return 0;
        }
        public static int LuaSetProtectTime(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.m_ProtectTime = (byte)(int)lua_tonumber(L, 1);
                }
            }
            return 0;
        }
        public static int LuaOpenBox(IntPtr L)
        {
            var player = FindPlayer(L);
            if (player != null)
            {
                player.ClientPeer.SendEvent(new EventData
                {
                    Code = (byte)OperationCode.OpenBox,
                    Parameters = new Dictionary<byte, object>(),
                }, player.sendParameters);
            }
            return 0;
        }
        public static int LuaSetRevPos(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.SetRevivalPos(player.scene.MapId, (ushort)lua_tonumber(L, 1));
                }
            }
            return 0;
        }
        public static int LuaAddStation(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.m_PlayerStationList.Add((short)(int)lua_tonumber(L, 1));
                }
            }
            return 0;
        }
        public static int LuaAddTermini(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.m_PlayerWayPointList.Add((short)(int)lua_tonumber(L, 1));
                }
            }
            return 0;
        }
        public static int LuaUseTownPortal(IntPtr L)
        {
            var player = FindPlayer(L);
            if (player != null)
            {
                player.UseTownPortal();
            }
            return 0;
        }
        public static int LuaReturnFromPortal(IntPtr L)
        {
            var player = FindPlayer(L);
            if (player != null)
            {
                player.BackToTownPortal();
            }
            return 0;
        }
        public static int LuaNewWorld(IntPtr L)
        {
            if (lua_gettop(L) >= 3)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.ChangeWorld((int)lua_tonumber(L, 1), (int)lua_tonumber(L, 2), (int)lua_tonumber(L, 3));
                }
            }
            return 0;
        }
        public static int LuaLeaveTeam(IntPtr L)
        {
            var player = FindPlayer(L);
            if (player != null)
            {
                player.TeamLeave();
            }
            return 0;
        }
        public static int LuaGetTask(IntPtr L)
        {
            int v = 0;
            if (lua_gettop(L) >= 1)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    v = player.GetSaveVal((int)lua_tonumber(L, 1));
                }
            }
            lua_pushnumber(L, v);
            return 1;
        }
        public static int LuaSetTask(IntPtr L)
        {
            if (lua_gettop(L) >= 2)
            {
                var player = FindPlayer(L);
                if (player != null)
                {
                    player.SetSaveVal((int)lua_tonumber(L, 1), (int)lua_tonumber(L, 2));
                }
            }
            return 0;
        }
        public static int LuaMsg2Player(IntPtr L)
        {
            if (lua_gettop(L) >= 1)
            {
                PlayerObj player = FindPlayer(L);
                if (player != null)
                {
                    player.SendMsgSelf(ToUtf8(L, 1));
                }
            }
            return 0;
        }
        public static int LuaSetPropState(IntPtr L)
        {
            int nState = 1;
            if (lua_gettop(L) >= 1)
            {
                nState = (int)lua_tonumber(L, 1);
                nState = (nState == 0) ? 0 : 1;
            }
            var obj = FindObject(L);
            if (obj != null)
            {
                obj.SetState((byte)nState);
            }
            return 0;
        }
        public static int LuaAddEventItem(IntPtr L)
        {
            var nParamNum = lua_gettop(L);
            if (nParamNum >= 1)
            {
                PlayerObj player = FindPlayer(L);
                if (player != null)
                {
                    var nEventId = (byte)lua_tonumber(L, 1);

                    byte nLevel = 1;
                    if (nParamNum >= 2)
                    {
                        nLevel = (byte)lua_tonumber(L, 2);
                    }

                    var item = ItemModule.Me.AddOther((byte)ItemGenre.item_task, nEventId, 0, 0, nLevel, null, 0);
                    if (item != null)
                    {
                        player.AddItem(item);
                    }
                }
            }
            return 0;
        }
        public static int LuaHaveItem(IntPtr L)
        {
            int v = 0;
            if (lua_gettop(L) >= 1)
            {
                PlayerObj player = FindPlayer(L);
                if (player != null)
                {
                    if (player.HaveItem((int)lua_tonumber(L, 1)))
                    {
                        v = 1;
                    }
                }
            }
            lua_pushnumber(L, v);
            return 1;
        }
    }
}
