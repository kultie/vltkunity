
using System.Runtime.InteropServices;
using System.Text;
using System;

namespace game.resource.packageIni
{
    class PluginApi
    {
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        private const string libraryName = "library.package.ini.macos";
#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        private const string libraryName = "library.package.ini.windows";
#elif UNITY_ANDROID
        private const string libraryName = "library.package.ini.android";
#elif UNITY_IOS && !UNITY_EDITOR
        private const string libraryName = "__Internal";
#else
        private const string libraryName = "library.package.ini";
#endif

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(libraryName)]
        public static extern IntPtr z(string _z);

        [DllImport(libraryName)]
        public static extern int x(IntPtr _x);

        [DllImport(libraryName)]
        public static extern void c(IntPtr _x, int _c, StringBuilder _v, int _b, StringBuilder _n, int _m);

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(libraryName)]
        public static extern void v(IntPtr _x, string _v, ref uint _b, ref int _n, ref int _m, ref int _l, ref int _k, ref int _j);

        [DllImport(libraryName)]
        public static extern void b(IntPtr _x, uint _b, int _n, int _m, int _l, int _k, int _j, IntPtr _h);

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(libraryName)]
        public static extern ushort n(IntPtr _x, string _n);

        [DllImport(libraryName)]
        public static extern void m(IntPtr _x, string _m, ref ushort _l, ref ushort _k, ref ushort _j, ref ushort _h, ref ushort _g, ref ushort _f, ref ushort _d, ref ushort _s);

        [DllImport(libraryName)]
        public static extern void l(IntPtr _x, string _l, int _k, ref ushort _j, ref ushort _h, ref ushort _g, ref ushort _f);

        [DllImport(libraryName)]
        public static extern void k(IntPtr _x, string _k, int _j, IntPtr _h);

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(libraryName)]
        public static extern IntPtr j(IntPtr _x, string _j, IntPtr _h, int _g, ref map.Config.Textures _f, ref int _d, ref int _s);

        [DllImport(libraryName)]
        public static extern void h(IntPtr _h, ref map.Config.Textures _g, [Out] map.Element.Texture[] _f, [Out] map.Element.Obstacle[] _d);

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport(libraryName)]
        public static extern IntPtr g(IntPtr _x, string _g, int _f, int _d, int _s, int _a, ref int _p);

        [DllImport(libraryName)]
        public static extern void f(IntPtr _f, [Out] mapServer.Element.NpcData[] _d);
    }
}
