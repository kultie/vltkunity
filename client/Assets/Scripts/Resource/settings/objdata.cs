using Photon.ShareLibrary.Constant;
using System.Collections.Generic;

namespace game.resource.settings
{
    class ObjData
    {
        public static void Initialize()
        {
            resource.Table declareTable = Game.Resource(mapping.settings.ObjData.fileFullPath).Get<resource.Table>();

            if (declareTable.IsEmpty())
            {
                UnityEngine.Debug.LogError(mapping.settings.ObjData.fileFullPath);
                return;
            }

            Cache.Settings.ObjData.declareRowIndexToResTypeMapping = ObjData.DeclareRowIndexToResTypeMapping(declareTable);
        }
        private static Dictionary<int, string> DeclareRowIndexToResTypeMapping(resource.Table _declareTable)
        {
            var result = new Dictionary<int, string>();

            var sprs = new Dictionary<string, ObjSpr>();

            for (int rowIndex = 1; rowIndex < _declareTable.RowCount; rowIndex++)
            {
                var npcResType = _declareTable.Get<string>((int)mapping.settings.ObjData.HeaderIndexer.ImageName, rowIndex);
                result.Add(rowIndex, npcResType);

                if (!sprs.ContainsKey(npcResType))
                {
                    var spr = new ObjSpr();
                    spr.Layer = (byte)_declareTable.Get<int>((int)mapping.settings.ObjData.HeaderIndexer.Layer, rowIndex);
                    spr.TotalFrame = _declareTable.Get<int>((int)mapping.settings.ObjData.HeaderIndexer.ImageTotalFrame, rowIndex);
                    spr.CurFrame = _declareTable.Get<int>((int)mapping.settings.ObjData.HeaderIndexer.ImageCurFrame, rowIndex);
                    spr.TotalDir = _declareTable.Get<int>((int)mapping.settings.ObjData.HeaderIndexer.ImageTotalDir, rowIndex);
                    spr.CurDir = _declareTable.Get<int>((int)mapping.settings.ObjData.HeaderIndexer.ImageCurDir, rowIndex);

                    var kind = _declareTable.Get<string>((int)mapping.settings.ObjData.HeaderIndexer.Kind, rowIndex);
                    if (kind == "Item" || kind == "Money" || kind == "Prop")
                    {
                        spr.Name = _declareTable.Get<string>((int)mapping.settings.ObjData.HeaderIndexer.Name, rowIndex);
                    }
                    else
                    {
                        spr.Name = string.Empty;
                    }

                    sprs.Add(npcResType, spr);
                }
            }

            Cache.Settings.ObjData.declareRowIndexToStatureMapping = sprs;

            return result;
        }
    }
}