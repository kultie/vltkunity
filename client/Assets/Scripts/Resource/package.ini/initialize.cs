
using System.Collections.Generic;

namespace game.resource
{
    class PackageIni
    {
        public static bool Initialize()
        {
            string packageRootDirectoryPath = game.resource.dataController.Config.GetLocalStogareFullPath();

            UnityEngine.Debug.Log(packageRootDirectoryPath);

            if (game.resource.Cache.resourcePackageHandler.ToInt64() == 0)
            {
                game.resource.Cache.resourcePackageHandler = game.resource.packageIni.Handler.CreateHandler(packageRootDirectoryPath);
            }

            List<string> packageStatusList = game.resource.packageIni.Handler.GetElementStatusList(game.resource.Cache.resourcePackageHandler);

            string report = "game.resource.Cache --> package element count: " + game.resource.packageIni.Handler.GetPackageElementCount(game.resource.Cache.resourcePackageHandler);

            for (int index = 0; index < packageStatusList.Count; index++)
            {
                report += "\n" + packageStatusList[index];
            }

            UnityEngine.Debug.Log(report);

            return game.resource.Cache.resourcePackageHandler.ToInt64() != 0;
        }

        public static bool InitializeService()
        {
            string packageRootDirectoryPath = game.resource.dataController.Config.GetAssetFullPath();

            UnityEngine.Debug.Log(packageRootDirectoryPath);

            if (game.resource.Cache.resourcePackageHandler.ToInt64() == 0)
            {
                game.resource.Cache.resourcePackageHandler = game.resource.packageIni.Handler.CreateHandler(packageRootDirectoryPath);
            }


            return game.resource.Cache.resourcePackageHandler.ToInt64() != 0;
        }
    }
}
