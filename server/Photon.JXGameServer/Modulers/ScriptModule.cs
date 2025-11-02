using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Photon.JXGameServer.Modulers
{
    public class ScriptModule
    {
        public static ScriptModule Me;

        Dictionary<uint,LuaObj> l_scripts;
        Dictionary<string,LuaObj> m_scripts;

        public ScriptModule() 
        {
            Me = this;

            l_scripts = new Dictionary<uint, LuaObj>();
            m_scripts = new Dictionary<string, LuaObj>();
        }
        public void InitWorld() => RunScript("/script/system/autoexec.lua", "main");
        public void RunScript(string file,string func)
        {
            file = file.Replace('/', '\\');

            var script = GetScript(file);
            if (script != null)
            {
                script.CallFunction(null, func);
            }
            else
            {
                PhotonApp.log.Error($"Unable to load {file}");
            }
        }
        public void LoadConfig(string root)
        {
            var lua_Funcs = LuaEntity.Init();

            PhotonApp.log.InfoFormat("LoadScripts: {0} api", LuaEntity.Funcs.Count);

            var c = 0;
            var files = Directory.GetFiles(root, "*.lua",SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var key = file.Substring(root.Length);
                var id = JXHelper.FileNameToId(System.Text.Encoding.GetEncoding(936).GetBytes(key));

                try
                {
                    var script = new LuaObj();

                    m_scripts.Add(key, script);
                    l_scripts.Add(id, script);

                    if (script.Load(id, file, lua_Funcs))
                        c++;
                    else
                    {
                        m_scripts.Remove(key);
                        l_scripts.Remove(id);

                        script = null;
                    }
                }
                catch (Exception e)
                {
                    PhotonApp.log.Error(e.ToString());
                }
            }
            PhotonApp.log.InfoFormat("LoadScripts: {0} scripts loaded", c);
        }
        public uint GetId(LuaObj script) => l_scripts.FirstOrDefault(x => x.Value == script).Key;

        public LuaObj GetScript(uint id)
        {
            if (l_scripts.ContainsKey(id))
                return l_scripts[id];
            return null;
        }
        public LuaObj GetScript(string path)
        {
            if (string.IsNullOrEmpty(path)) 
                return null;
            if (m_scripts.ContainsKey(path))
                return m_scripts[path];
            return null;
        }
        public string GetPath(uint id)
        {
            if (l_scripts.ContainsKey(id))
                return m_scripts.FirstOrDefault(x => x.Value == l_scripts[id]).Key;
            return null;
        }
    }
}
