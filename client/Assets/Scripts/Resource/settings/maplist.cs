
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace game.resource.settings
{
    public class MapList
    {
        public struct MapInfo
        {
            public struct FilePath
            {
                public string wor;
                public string miniMapImage;
            }

            public struct WorFile
            {
                public struct Rectangle
                {
                    public int left;
                    public int top;
                    public int right;
                    public int bottom;
                };

                public MapInfo.WorFile.Rectangle rect;
            }

            //////////////////////////////////////////////////

            public int id;
            public string rootPath;
            public string name;

            //////////////////////////////////////////////////

            public MapInfo.FilePath filePath;
            public MapInfo.WorFile worFile;

            //////////////////////////////////////////////////
        }

        public static MapList.MapInfo LoadMapInfo(int _mapId)
        {
            MapList.MapInfo result = new() { id = 0 };
            resource.Ini mapListIni = Game.Resource(mapping.settings.MapList.filePath).Get<resource.Ini>();

            if (mapListIni.IsEmpty())
            {
                return result;
            }

            result.rootPath = mapListIni.Get<string>(mapping.settings.MapList.Section.list, string.Empty + _mapId);

            if (result.rootPath == string.Empty)
            {
                return result;
            }

            result.rootPath = mapping.settings.MapList.resourceFolder + result.rootPath;
            result.name = formater.TCVN3.UTF8(mapListIni.Get<string>(mapping.settings.MapList.Section.list, string.Empty + _mapId + mapping.settings.MapList.Key.Suffix.name));
            result.filePath.wor = result.rootPath + mapping.settings.MapList.WorFile.extension;
            result.filePath.miniMapImage = result.rootPath + mapping.settings.MapList.MiniMap.imageSuffix;

            { // .wor file

                resource.Ini worIni = Game.Resource(result.filePath.wor).Get<resource.Ini>();
                string rectangleLiteral = worIni.Get<string>(mapping.settings.MapList.WorFile.Section.main, mapping.settings.MapList.WorFile.Key.rect);

                if (rectangleLiteral == string.Empty)
                {
                    return new();
                }

                string[] rectangleSplited = rectangleLiteral.Split(',');

                result.worFile.rect.left = (rectangleSplited.Length > mapping.settings.MapList.WorFile.Key.Rect.left) ? int.Parse(Regex.Replace(rectangleSplited[mapping.settings.MapList.WorFile.Key.Rect.left], "[^0-9-]", string.Empty)) : 0;
                result.worFile.rect.top = (rectangleSplited.Length > mapping.settings.MapList.WorFile.Key.Rect.top) ? int.Parse(Regex.Replace(rectangleSplited[mapping.settings.MapList.WorFile.Key.Rect.top], "[^0-9-]", string.Empty)) : 0;
                result.worFile.rect.right = (rectangleSplited.Length > mapping.settings.MapList.WorFile.Key.Rect.right) ? int.Parse(Regex.Replace(rectangleSplited[mapping.settings.MapList.WorFile.Key.Rect.right], "[^0-9-]", string.Empty)) : 0;
                result.worFile.rect.bottom = (rectangleSplited.Length > mapping.settings.MapList.WorFile.Key.Rect.bottom) ? int.Parse(Regex.Replace(rectangleSplited[mapping.settings.MapList.WorFile.Key.Rect.bottom], "[^0-9-]", string.Empty)) : 0;
            }

            result.id = _mapId;
            return result;
        }

        private class UnitTest
        {
            public static void PrintMapInfo(MapList.MapInfo _mapInfo)
            {
                UnityEngine.Debug.Log("id: " + _mapInfo.id);
                UnityEngine.Debug.Log("rootPath: " + _mapInfo.rootPath);
                UnityEngine.Debug.Log("name: " + _mapInfo.name);

                UnityEngine.Debug.Log("filePath.wor: " + _mapInfo.filePath.wor);
                UnityEngine.Debug.Log("filePath.miniMapImage: " + _mapInfo.filePath.miniMapImage);

                UnityEngine.Debug.Log("worfile.rect.left: " + _mapInfo.worFile.rect.left);
                UnityEngine.Debug.Log("worfile.rect.top: " + _mapInfo.worFile.rect.top);
                UnityEngine.Debug.Log("worfile.rect.right: " + _mapInfo.worFile.rect.right);
                UnityEngine.Debug.Log("worfile.rect.bottom: " + _mapInfo.worFile.rect.bottom);
            }

            public static void LoadMapInfo()
            {
                int mapId = 1;
                MapList.MapInfo mapinfo;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Restart();
                stopwatch.Start();
                mapinfo = MapList.LoadMapInfo(mapId);
                stopwatch.Stop();
                UnitTest.PrintMapInfo(mapinfo);
                UnityEngine.Debug.Log("settings.MapList.UnitTest.LoadMapInfo >> mapId: " + mapId + ", performance: " + stopwatch.ElapsedMilliseconds + " milliseconds");

                mapId = 2;
                stopwatch.Restart();
                stopwatch.Start();
                mapinfo = MapList.LoadMapInfo(mapId);
                stopwatch.Stop();
                UnitTest.PrintMapInfo(mapinfo);
                UnityEngine.Debug.Log("settings.MapList.UnitTest.LoadMapInfo >> mapId: " + mapId + ", performance: " + stopwatch.ElapsedMilliseconds + " milliseconds");
            }
        }
    }
}
