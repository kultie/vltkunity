
using System.Collections.Generic;

namespace game.resource.settings
{
    class Npcs
    {
        public static void Initialize()
        {
            resource.Table declareTable = Game.Resource(mapping.settings.Npcs.fileFullPath).Get<resource.Table>();

            if(declareTable.IsEmpty())
            {
                UnityEngine.Debug.LogError(mapping.settings.Npcs.fileFullPath);
                return;
            }

            Cache.Settings.Npcs.declareRowIndexToResTypeMapping = Npcs.DeclareRowIndexToResTypeMapping(declareTable);
            Cache.Settings.Npcs.declareRowIndexToStatureMapping = Npcs.DeclareRowIndexToStatureMapping(declareTable);
        }

        public static string GetNpcResType(int _declareLine)
        {
            int declareRowIndex = _declareLine - 1;

            if(Cache.Settings.Npcs.declareRowIndexToResTypeMapping.ContainsKey(declareRowIndex) == false)
            {
                return string.Empty;
            }

            return Cache.Settings.Npcs.declareRowIndexToResTypeMapping[declareRowIndex];
        }

        public static int GetNpcStature(int _declareLine)
        {
            int declareRowIndex = _declareLine - 1;

            if (Cache.Settings.Npcs.declareRowIndexToStatureMapping.ContainsKey(declareRowIndex) == false)
            {
                return 0;
            }

            return Cache.Settings.Npcs.declareRowIndexToStatureMapping[declareRowIndex];
        }

        ///////////////////////////////////////////////////////////////////////////
        
        private static Dictionary<int, string> DeclareRowIndexToNpcName(resource.Table _declareTable)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            for (int rowIndex = 1; rowIndex < _declareTable.RowCount; rowIndex++)
            {
                string npcResType = _declareTable.Get<string>((int)mapping.settings.Npcs.HeaderIndexer.Name, rowIndex);
                result.Add(rowIndex, npcResType);
            }

            return result;
        }

        private static Dictionary<int, string> DeclareRowIndexToResTypeMapping(resource.Table _declareTable)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            for(int rowIndex = 1; rowIndex < _declareTable.RowCount; rowIndex++)
            {
                string npcResType = _declareTable.Get<string>((int)mapping.settings.Npcs.HeaderIndexer.NpcResType, rowIndex);
                result.Add(rowIndex, npcResType);
            }

            return result;
        }

        private static Dictionary<int, int> DeclareRowIndexToStatureMapping(resource.Table _declareTable)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            for (int rowIndex = 1; rowIndex < _declareTable.RowCount; rowIndex++)
            {
                int stature = _declareTable.Get<int>((int)mapping.settings.Npcs.HeaderIndexer.Stature, rowIndex);
                result.Add(rowIndex, stature);
            }

            return result;
        }
    }
}
