
using System.Collections.Generic;
using System.Text;
using System;

namespace game.resource.packageIni
{
    class Handler
    {
        public static IntPtr CreateHandler(string _rootDirectoryPath)
        {
            return packageIni.PluginApi.z(_rootDirectoryPath);
        }

        public static int GetPackageElementCount(IntPtr _handler)
        {
            return packageIni.PluginApi.x(_handler);
        }

        public static List<string> GetElementStatusList(IntPtr _handler)
        {
            List<string> result = new();
            StringBuilder elementPath = new(512);
            StringBuilder elementStatus = new(512);
            int elementCount = packageIni.PluginApi.x(_handler);

            for (int index = 0; index < elementCount; index++)
            {
                packageIni.PluginApi.c(_handler, index, elementPath, elementPath.Capacity, elementStatus, elementStatus.Capacity);
                result.Add(elementPath.ToString() + " --> " + elementStatus.ToString());
            }

            return result;
        }
    }
}
