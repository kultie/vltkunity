
using System.IO;

namespace game.resource.dataController
{
    class Config
    {
        public static string GetHostingControlationAddress()
        {
            return "http://154.26.129.47/jx";
        }

        public static string GetLocalStorageDirectoryName()
        {
            return "data.controller";
        }

        public static string GetLocalStogareFullPath()
        {
            return Path.Combine(UnityEngine.Application.persistentDataPath, GetLocalStorageDirectoryName());
        }

        public static string GetAssetFullPath()
        {
            return Path.Combine("Assets/Resources", GetLocalStorageDirectoryName());
        }
    }
}
