
using XLua;

namespace game.resource
{
    public class Script
    {
        [LuaCallCSharp]
        private static byte[] CustomLoaderFunction(ref string moduleName)
        {
            return Game.Resource(moduleName).Get<resource.Buffer>();
        }

        ////////////////////////////////////////////////////////////////////////////////

        private LuaEnv luaenv;

        ////////////////////////////////////////////////////////////////////////////////

        public Script(string scriptPath)
        {
            this.Initialize();
            this.Load(scriptPath);
        }

        ////////////////////////////////////////////////////////////////////////////////

        private void Initialize()
        {
            this.luaenv = new LuaEnv();
            this.luaenv.AddLoader(Script.CustomLoaderFunction);
            this.luaenv.DoString(
                Game.Resource(resource.mapping.Script.Library.onCreate).Get<resource.Buffer>(),
                resource.mapping.Script.Library.onCreate
            );
        }

        private void Load(string scriptPath)
        {
            this.luaenv.DoString(
                Game.Resource(scriptPath).Get<resource.Buffer>(),
                scriptPath
            );
        }

        public void Release()
        {
            this.luaenv.Dispose();
            this.luaenv = null;
        }

        ////////////////////////////////////////////////////////////////////////////////

        public Typename CallFunction<Typename>(string functionName, params object[] args)
        {
            object[] result = this.luaenv.Global.Get<LuaFunction>(functionName).Call(args);

            if(result != null && result.Length > 0)
            {
                return (Typename)result[0];
            }

            return default;
        }

        public void CallFunction(string functionName, params object[] args)
        {
            this.luaenv.Global.Get<LuaFunction>(functionName).Call(args);
        }
    }
}
