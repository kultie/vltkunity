
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace game.resource.settings.npcres
{
    class Initialize
    {
        public Initialize()
        {
            npcres.AttribModify.Initialize();

            resource.Table kindTable = Game.Resource(mapping.settings.NpcRes.Kind.filePath).Get<resource.Table>();

            if(kindTable.IsEmpty())
            {
                UnityEngine.Debug.LogError(mapping.settings.NpcRes.Kind.filePath);
                return;
            }

            resource.Cache.Settings.NpcRes.Kind.mainManHeaderValueMapping = Initialize.GetSpecialCharacterMapping(kindTable, mapping.settings.NpcRes.Kind.CharacterName.mainMan);
            resource.Cache.Settings.NpcRes.Kind.mainLadyHeaderValueMapping = Initialize.GetSpecialCharacterMapping(kindTable, mapping.settings.NpcRes.Kind.CharacterName.mainLady);

            resource.Cache.Settings.NpcRes.mainManTableMapping = Initialize.GetSpecialCharacterTable(resource.Cache.Settings.NpcRes.Kind.mainManHeaderValueMapping);
            resource.Cache.Settings.NpcRes.mainLadyTableMapping = Initialize.GetSpecialCharacterTable(resource.Cache.Settings.NpcRes.Kind.mainLadyHeaderValueMapping);

            resource.Cache.Settings.NpcRes.mainManPartPropertiesTableMapping = Initialize.GetSpecialPartPropertiesTable(resource.Cache.Settings.NpcRes.Kind.mainManHeaderValueMapping);
            resource.Cache.Settings.NpcRes.mainLadyPartPropertiesTableMapping = Initialize.GetSpecialPartPropertiesTable(resource.Cache.Settings.NpcRes.Kind.mainLadyHeaderValueMapping);

            resource.Cache.Settings.NpcRes.mainManIniMapping = Initialize.GetSpecialCharacterIni(resource.Cache.Settings.NpcRes.Kind.mainManHeaderValueMapping);
            resource.Cache.Settings.NpcRes.mainLadyIniMapping = Initialize.GetSpecialCharacterIni(resource.Cache.Settings.NpcRes.Kind.mainLadyHeaderValueMapping);

            game.resource.Table shadowTable = Game.Resource(mapping.settings.NpcRes.Shadow.filePath).Get<resource.Table>();

            if(shadowTable.IsEmpty())
            {
                UnityEngine.Debug.LogError(mapping.settings.NpcRes.Shadow.filePath);
            }
            else
            {
                resource.Cache.Settings.NpcRes.Shadow.mainManAnimationMapping = Initialize.GetSpecialShadowAnimationMapping(kindTable, shadowTable, mapping.settings.NpcRes.Kind.CharacterName.mainMan);
                resource.Cache.Settings.NpcRes.Shadow.mainLadyAnimationMapping = Initialize.GetSpecialShadowAnimationMapping(kindTable, shadowTable, mapping.settings.NpcRes.Kind.CharacterName.mainLady);
            }

            resource.Cache.Settings.NpcRes.NormalNpc.animationMapping = GetNormalNpcMapping(kindTable);
            resource.Cache.Settings.NpcRes.textures = new Dictionary<string, skill.texture.SprCache.Data>();
        }

        private static Dictionary<string, string> GetSpecialCharacterMapping(resource.Table _kindTable, string _characterName)
        {
            Dictionary<string, string> result = new();

            List<string> headerKey = _kindTable.GetHeaderKeyList();
            int rowIndex = _kindTable.FindRowIndex(mapping.settings.NpcRes.Kind.Header.characterName, _characterName);

            foreach (string key in headerKey)
            {
                result.Add(key, _kindTable.Get<string>(key, rowIndex));
            }

            return result;
        }

        private static Dictionary<string, resource.Table> GetSpecialCharacterTable(Dictionary<string, string> _headerMapping)
        {
            Dictionary<string, resource.Table> result = new();
            List<string> allTabFileHeader = settings.npcres.special.Part.AllTabFileList();

            foreach(string indexTabFileHeader in allTabFileHeader)
            {
                if(_headerMapping.ContainsKey(indexTabFileHeader) == false)
                {
                    UnityEngine.Debug.LogError(indexTabFileHeader);
                    continue;
                }

                string tableFilePath = mapping.settings.NpcRes.directoryPath + _headerMapping[indexTabFileHeader];
                resource.Table table = Game.Resource(tableFilePath).Get<resource.Table>();

                if (table.IsEmpty())
                {
                    UnityEngine.Debug.LogError(tableFilePath);
                    continue;
                }

                result.Add(indexTabFileHeader, table);
            }

            return result;
        }

        private static Dictionary<string, resource.Ini> GetSpecialCharacterIni(Dictionary<string, string> _headerMapping)
        {
            Dictionary<string, resource.Ini> result = new();
            List<string> allIniFileHeader = settings.npcres.special.Part.AllIniFileList();

            foreach(string indexIniFileHeader in allIniFileHeader)
            {
                if (_headerMapping.ContainsKey(indexIniFileHeader) == false)
                {
                    UnityEngine.Debug.LogError(indexIniFileHeader);
                    continue;
                }

                string iniFilePath = mapping.settings.NpcRes.directoryPath + _headerMapping[indexIniFileHeader];
                resource.Ini ini = Game.Resource(iniFilePath).Get<resource.Ini>();

                if (ini.IsEmpty())
                {
                    UnityEngine.Debug.LogError(iniFilePath);
                    continue;
                }

                result.Add(indexIniFileHeader, ini);
            }

            return result;
        }

        private static Dictionary<string, resource.Table> GetSpecialPartPropertiesTable(Dictionary<string, string> _headerMapping)
        {
            Dictionary<string, resource.Table> result = new();
            List<string> partElements = resource.settings.npcres.special.Part.AllPartList();

            foreach(string partElement in partElements)
            {
                if(_headerMapping.ContainsKey(partElement) == false)
                {
                    continue;
                }

                string specialFilePath = mapping.settings.NpcRes.directoryPath + _headerMapping[partElement];
                string tabFileExtension = mapping.settings.NpcRes.Properties.tabFileExtension;

                if (specialFilePath[^tabFileExtension.Length..].CompareTo(tabFileExtension) != 0)
                {
                    UnityEngine.Debug.LogError(specialFilePath);
                    continue;
                }

                specialFilePath = specialFilePath.Insert(specialFilePath.Length - tabFileExtension.Length, mapping.settings.NpcRes.Properties.sprPropertiesSuffix);
                result[partElement] = Game.Resource(specialFilePath).Get<resource.Table>();
            }

            return result;
        }

        private static Dictionary<string, settings.npcres.Structures.PartSprInfo> GetSpecialShadowAnimationMapping(resource.Table _kindTable, game.resource.Table _shadowTable, string _specialName)
        {
            int kindRowIndex = _kindTable.FindRowIndex(mapping.settings.NpcRes.Kind.Header.characterName, _specialName);
            string resourceDirectory = _kindTable.Get<string>(mapping.settings.NpcRes.Kind.Header.resFilePath, kindRowIndex);

            if(resourceDirectory == null)
            {
                UnityEngine.Debug.LogError(_specialName);
                return new();
            }

            int specialRowIndex = _shadowTable.FindRowIndex(mapping.settings.NpcRes.Kind.Header.characterName, _specialName);

            if(specialRowIndex < 0)
            {
                UnityEngine.Debug.LogError(_specialName);
                return new();
            }

            Dictionary<string, settings.npcres.Structures.PartSprInfo> result = new();

            for (int headIndex = 1; headIndex < _shadowTable.HeaderCount;)
            {
                string ainmationName = _shadowTable.GetHeaderKey(headIndex);
                string sprFileName = _shadowTable.Get<string>(headIndex, specialRowIndex);
                headIndex++;
                string sprProperties = _shadowTable.Get<string>(headIndex, specialRowIndex);
                headIndex++;

                string[] sprPropertiesSplited = sprProperties.Split(',');
                ushort sprFrameCount = ushort.Parse(Regex.Replace(sprPropertiesSplited[0], "[^0-9-]", string.Empty));
                int sprDirections = int.Parse(Regex.Replace(sprPropertiesSplited[1], "[^0-9-]", string.Empty));
                int sprInterval = int.Parse(Regex.Replace(sprPropertiesSplited[2], "[^0-9-]", string.Empty));

                settings.npcres.Structures.PartSprInfo newSprPartInfo = new settings.npcres.Structures.PartSprInfo();
                newSprPartInfo.sprFullPath = "\\" + resourceDirectory + "\\" + sprFileName;
                newSprPartInfo.frameCount = sprFrameCount;
                newSprPartInfo.directionCount = sprDirections;
                newSprPartInfo.intervalRatio = sprInterval;

                result[ainmationName] = newSprPartInfo;
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, resource.Cache.Settings.NpcRes.NormalNpc.PartInfo>> GetNormalNpcMapping(resource.Table _kindTable)
        {
            Dictionary<string, Dictionary<string, resource.Cache.Settings.NpcRes.NormalNpc.PartInfo>> result = new();
            resource.Table npcActionTable = Game.Resource(mapping.settings.NpcRes.NormalNpc.sprActionPath).Get<resource.Table>();
            resource.Table npcPropertiesTable = Game.Resource(mapping.settings.NpcRes.NormalNpc.sprPropertiesPath).Get<resource.Table>();

            if(npcActionTable.IsEmpty())
            {
                UnityEngine.Debug.LogError(mapping.settings.NpcRes.NormalNpc.sprActionPath);
                return result;
            }

            if(npcPropertiesTable.IsEmpty())
            {
                UnityEngine.Debug.LogError(mapping.settings.NpcRes.NormalNpc.sprPropertiesPath);
                return result;
            }

            Dictionary<string, int> actionKeyIndexer = new Dictionary<string, int>();
            Dictionary<string, int> propertiesKeyIndexer = new Dictionary<string, int>();

            for(int rowIndex = 1; rowIndex < npcActionTable.RowCount; rowIndex++)
            {
                actionKeyIndexer[npcActionTable.Get<string>(mapping.settings.NpcRes.NormalNpc.Header.npcList, rowIndex)] = rowIndex;
            }

            for(int rowIndex = 1; rowIndex < npcPropertiesTable.RowCount; rowIndex++)
            {
                propertiesKeyIndexer[npcPropertiesTable.Get<string>(mapping.settings.NpcRes.NormalNpc.Header.npcList, rowIndex)] = rowIndex;
            }

            for (int rowIndex = 3; rowIndex < _kindTable.RowCount; rowIndex++)
            {
                string characterName = _kindTable.Get<string>(mapping.settings.NpcRes.Kind.Header.characterName, rowIndex);
                string sprDirectoryPath = _kindTable.Get<string>(mapping.settings.NpcRes.Kind.Header.resFilePath, rowIndex);

                if(actionKeyIndexer.ContainsKey(characterName) == false)
                {
                    continue;
                }

                if(propertiesKeyIndexer.ContainsKey(characterName) == false)
                {
                    continue;
                }

                int actionRowIndex = actionKeyIndexer[characterName];
                int propertiesIndex = propertiesKeyIndexer[characterName];

                if(sprDirectoryPath.StartsWith('\\') == false)
                {
                    sprDirectoryPath = "\\" + sprDirectoryPath;
                }

                if(sprDirectoryPath.EndsWith('\\') == false)
                {
                    sprDirectoryPath += "\\";
                }

                result[characterName] = new();

                foreach (KeyValuePair<string, int> actionHeaderPair in npcActionTable.GetHeaderKeyIndexPair())
                {
                    if(actionHeaderPair.Key.CompareTo(mapping.settings.NpcRes.NormalNpc.Header.npcList) == 0)
                    {
                        continue;
                    }

                    string sprName = npcActionTable.Get<string>(actionHeaderPair.Key, actionRowIndex);
                    string sprPropertiesLiteral = npcPropertiesTable.Get<string>(actionHeaderPair.Key, propertiesIndex);

                    if(sprName == string.Empty
                        || sprPropertiesLiteral == string.Empty)
                    {
                        continue;
                    }

                    string[] sprPropertiesSplited = sprPropertiesLiteral.Split(',');

                    resource.Cache.Settings.NpcRes.NormalNpc.PartInfo newPartInfo = new();
                    newPartInfo.fullBody.sprFullPath = sprDirectoryPath + sprName;
                    newPartInfo.fullBody.frameCount = sprPropertiesSplited.Length >= 1 ? ushort.Parse(Regex.Replace(sprPropertiesSplited[0], "[^0-9-]", string.Empty)) : (ushort)0;
                    newPartInfo.fullBody.directionCount = sprPropertiesSplited.Length >= 2 ? int.Parse(Regex.Replace(sprPropertiesSplited[1], "[^0-9-]", string.Empty)) : 0;
                    newPartInfo.fullBody.intervalRatio = sprPropertiesSplited.Length >= 3 ? int.Parse(Regex.Replace(sprPropertiesSplited[2], "[^0-9-]", string.Empty)) : 0;

                    newPartInfo.shadow.sprFullPath = sprDirectoryPath + sprName.Insert(sprName.Length - mapping.settings.NpcRes.Properties.sprFileExtension.Length, mapping.settings.NpcRes.NormalNpc.shadowSuffix);
                    newPartInfo.shadow.frameCount = newPartInfo.fullBody.frameCount;
                    newPartInfo.shadow.directionCount = newPartInfo.fullBody.directionCount;
                    newPartInfo.shadow.intervalRatio = newPartInfo.fullBody.intervalRatio;

                    result[characterName][actionHeaderPair.Key] = newPartInfo;
                }
            }

            return result;
        }
    }
}
