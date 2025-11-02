using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Photon.JXLoginServer.Entitys
{
    public class ServerEntity
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public byte id;
        public string ip;
        public ClientPeer client;
        
        public List<int> maps = new List<int>();

        public void LoadMap()
        {
            var path = Path.Combine(PhotonApp.Instance.ApplicationPath, $"settings/maps/{id}.ini");

            IniFile ini = new IniFile(path);
            var n = ini.ReadInteger("Init", "Count");

            int c = 0;
            while (c < n)
            {
                var temp = ini.ReadString("World", "World" + c.ToString(c < 100 ? "00" : "000"));

                int x = temp.IndexOf(';');
                if (x > 0) temp = temp.Substring(0, x).Trim();
                x = temp.IndexOf('-');
                if (x > 0) temp = temp.Substring(0, x).Trim();

                if (!string.IsNullOrEmpty(temp))
                {
                    maps.Add(int.Parse(temp));
                }
                ++c;
            }
        }
    }
}
