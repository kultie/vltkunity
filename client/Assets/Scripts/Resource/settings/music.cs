
using System.Linq;

namespace game.resource.settings
{
    public class Music
    {
        public static void Initialize()
        {
            Cache.Settings.Music.musicset = new System.Collections.Generic.Dictionary<int, string>();

            resource.Table table = Game.Resource(resource.mapping.Settings.Music.musicset).Get<resource.Table>();

            if (table.IsEmpty())
            {
                UnityEngine.Debug.LogError(resource.mapping.Settings.Music.musicset);
                return;
            }

            for (int i = 1; i < table.RowCount; i++)
            {
                int mapId = table.Get<int>((int)resource.mapping.Settings.Music.HeaderIndexer.mapId, i);
                string musicFilePath = table.Get<string>((int)resource.mapping.Settings.Music.HeaderIndexer.musicFile1, i);

                Cache.Settings.Music.musicset[mapId] = musicFilePath;
            }
        }

        public static string GetRandomMusicFile()
        {
            if (Cache.Settings.Music.musicset == null)
            {
                return null;
            }

            int randomIndex = settings.skill.Static.GetRandomNumber(1, Cache.Settings.Music.musicset.Count);
            return Cache.Settings.Music.musicset.ElementAt(randomIndex - 1).Value;
        }

        public static string GetMapMusicFile(int mapId)
        {
            if (Cache.Settings.Music.musicset.ContainsKey(mapId))
            {
                return Cache.Settings.Music.musicset[mapId];
            }

            return GetRandomMusicFile();
        }

        //////////////////////////////////////////////////////////////////////////////////

        public static string GetMapMusicResourceFile(int mapId)
        {
            string musicFile = settings.Music.GetMapMusicFile(mapId);

            if (musicFile == null)
            {
                return null;
            }

            musicFile = "audio/WorldMapMusic/" + musicFile;
            musicFile = musicFile.Replace('\\', '/');
            musicFile = musicFile.Replace("//", "/");
            musicFile = musicFile.Replace(".mp3", "");
            musicFile = musicFile.ToLower();

            return musicFile;
        }
    }
}
