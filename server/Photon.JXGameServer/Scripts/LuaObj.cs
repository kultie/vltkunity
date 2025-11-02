using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Photon.JXGameServer.Maps;
using Photon.JXGameServer.Modulers;

namespace Photon.JXGameServer.Scripts
{
    public class LuaObj
    {
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr KLuaScript_Init(int size);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern void KLuaScript_Close(IntPtr L);

        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern void KLuaScript_Register(IntPtr L, int size, IntPtr funcs);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern void KLuaScript_Load(IntPtr L, IntPtr path, bool include);

        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool KLuaScript_Call(IntPtr L, IntPtr func);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool KLuaScript_CallMe(IntPtr L, IntPtr func, int id);
        [DllImport("JX.LuaLib.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern bool KLuaScript_CallFunctionS(IntPtr L, IntPtr cFuncName, int nResults, IntPtr cFormat, IntPtr vlist);

        System.IntPtr state;
        public LuaObj()
        {
            state = IntPtr.Zero;
        }

        public int GetTopValue
        {
            get
            {
                var ret = LuaEntity.GetTopValue(state);
                SafeCall();
                return ret;
            }
        }
        public string GetTopString
        {
            get
            {
                var ret = LuaEntity.GetTopString(state);
                SafeCall();
                return ret;
            }
        }
        public void SafeCall()
        {
            int idx = LuaEntity.lua_gettop(state);
            LuaEntity.lua_settop(state, -idx);
        }
        void SetGlobalName(IntPtr name,int value)
        {
            LuaEntity.lua_pushnumber(state, value);
            LuaEntity.lua_setglobal(state, name);
        }
        public bool Load(uint id, string path, IntPtr lua_Funcs)
        {
            state = KLuaScript_Init(128);
            if (state == System.IntPtr.Zero)
                return false;

            try
            {
                KLuaScript_Register(state, LuaEntity.Funcs.Count, lua_Funcs);

                SetGlobalName(LuaEntity.ScriptId, (int)id);

                var p = Marshal.StringToHGlobalUni(path);
                KLuaScript_Load(state, p, false);
                Marshal.FreeHGlobal(p);

                return true;
            }
            catch (Exception e)
            {
                PhotonApp.log.ErrorFormat("LuaObj {0}", e);
                KLuaScript_Close(state);

                return false;
            }
        }
        void SetMap(SceneObj scene, string func)
        {
            SetGlobalName(LuaEntity.ScriptId, (int)ScriptModule.Me.GetId(this));

            if (scene != null)
            {
                SetGlobalName(LuaEntity.MapId, scene.MapId);
                SetGlobalName(LuaEntity.WorldIndex, scene.WorldIndex);
            }
        }
        public void SetObject(int id) => SetGlobalName(LuaEntity.ObjectIndex, id);
        public void SetPlayer(int id) => SetGlobalName(LuaEntity.PlayerIndex, id);

        public void CallFunction(SceneObj scene, string func)
        {
            SetMap(scene, func);
            
            var f = Marshal.StringToHGlobalAnsi(func);
            KLuaScript_Call(state, f);
            Marshal.FreeHGlobal(f);

            SafeCall();
        }
        public void CallFunction(SceneObj scene, string func, int id)
        {
            SetMap(scene, func);

            var f = Marshal.StringToHGlobalAnsi(func);
            KLuaScript_CallMe(state, f, id);
            Marshal.FreeHGlobal(f);

            SafeCall();
        }
        int IntSizeOf(Type t)
        {
            return (Marshal.SizeOf(t) + IntPtr.Size - 1) & ~(IntPtr.Size - 1);
        }
        public bool CallFunction(SceneObj scene, string func,int rets,string format,params object[] args)
        {
            SetMap(scene, func);

            var allocs = new List<IntPtr>();
            var offset = 0;

            var f = Marshal.StringToHGlobalAnsi(func);
            allocs.Add(f);

            var p = Marshal.StringToHGlobalAnsi(format);
            allocs.Add(p);

            var sizes = new int[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                sizes[i] = args[i] is string ? IntPtr.Size : IntSizeOf(args[i].GetType());
            }

            var result = Marshal.AllocHGlobal(sizes.Sum());
            allocs.Add(result);

            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] is string)
                {
                    var data = Marshal.StringToHGlobalAnsi((string)args[i]);
                    allocs.Add(data);

                    Marshal.WriteIntPtr(result, offset, data);
                    offset += sizes[i];
                }
                else
                {
                    Marshal.StructureToPtr(args[i], result + offset, false);
                    offset += sizes[i];
                }
            }

            var r = KLuaScript_CallFunctionS(state, f, rets, p, result);

            foreach (var ptr in allocs)
            {
                Marshal.FreeHGlobal(ptr);
            }

            return r;
        }
    }
}
