
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace game.resource.settings.npcres.special
{
    class Getters
    {
        public static List<string> PartGroup(Dictionary<string, resource.Table> _tableHeaderMapping, string _partMember)
        {
            if (_tableHeaderMapping.ContainsKey(mapping.settings.NpcRes.Kind.Header.partFileName) == false)
            {
                UnityEngine.Debug.LogError(mapping.settings.NpcRes.Kind.Header.partFileName);
                return new();
            }

            resource.Table partTable = _tableHeaderMapping[mapping.settings.NpcRes.Kind.Header.partFileName];
            List<string> rowPartList;
            bool hasFound;

            for (int indexRow = 0 + 1; indexRow < partTable.RowCount; indexRow++)
            {
                rowPartList = new();
                hasFound = false;

                for (int indexColumn = 0 + 2; indexColumn < partTable.HeaderCount; indexColumn++)
                {
                    string crossData = partTable.Get<string>(indexColumn, indexRow);

                    if (crossData.Length <= 0)
                    {
                        continue;
                    }

                    rowPartList.Add(crossData);

                    if (crossData.CompareTo(_partMember) == 0)
                    {
                        hasFound = true;
                    }
                }

                if (hasFound)
                {
                    return rowPartList;
                }
            }

            return new();
        }

        public static List<string> PartGroup(string _specialType, string _partMember)
        {
            if (special.Validation.IsMainMan(_specialType))
            {
                return special.Getters.PartGroup(resource.Cache.Settings.NpcRes.mainManTableMapping, _partMember);
            }

            if (special.Validation.IsMainLady(_specialType))
            {
                return special.Getters.PartGroup(resource.Cache.Settings.NpcRes.mainLadyTableMapping, _partMember);
            }

            return new();
        }

        public static string AnimationName(Dictionary<string, resource.Table> _tableMapping, bool _riding, string _weaponAction, int _rowIndex)
        {
            resource.Table weaponActionTable;

            if (_riding)
            {
                if (_tableMapping.ContainsKey(mapping.settings.NpcRes.Kind.Header.weaponActionTab2))
                {
                    weaponActionTable = _tableMapping[mapping.settings.NpcRes.Kind.Header.weaponActionTab2];
                }
                else
                {
                    UnityEngine.Debug.LogError(mapping.settings.NpcRes.Kind.Header.weaponActionTab2);
                    return string.Empty;
                }
            }
            else
            {
                if (_tableMapping.ContainsKey(mapping.settings.NpcRes.Kind.Header.weaponActionTab1))
                {
                    weaponActionTable = _tableMapping[mapping.settings.NpcRes.Kind.Header.weaponActionTab1];
                }
                else
                {
                    UnityEngine.Debug.LogError(mapping.settings.NpcRes.Kind.Header.weaponActionTab1);
                    return string.Empty;
                }
            }

            return weaponActionTable.Get<string>(_weaponAction, _rowIndex);
        }

        public static string AnimationName(string _specialType, bool _riding, string _weaponAction, int _rowIndex)
        {
            if (special.Validation.IsMainMan(_specialType))
            {
                return special.Getters.AnimationName(resource.Cache.Settings.NpcRes.mainManTableMapping, _riding, _weaponAction, _rowIndex);
            }
            else if (special.Validation.IsMainLady(_specialType))
            {
                return special.Getters.AnimationName(resource.Cache.Settings.NpcRes.mainLadyTableMapping, _riding, _weaponAction, _rowIndex);
            }

            return string.Empty;
        }

        public static int PartOrder(Dictionary<string, resource.Ini> _iniMapping, string _partName, string _animationName, int _direction)
        {
            if (_iniMapping.ContainsKey(mapping.settings.NpcRes.Kind.Header.actionRenderOrderTab) == false)
            {
                UnityEngine.Debug.LogError(mapping.settings.NpcRes.Kind.Header.actionRenderOrderTab);
                return 0;
            }

            resource.Ini orderIni = _iniMapping[mapping.settings.NpcRes.Kind.Header.actionRenderOrderTab];
            string orderString = orderIni.Get<string>(_animationName, mapping.settings.NpcRes.ActionRenderOrderTab.Key.prefix + "" + _direction);

            if (orderString == string.Empty)
            {
                orderString = orderIni.Get<string>(mapping.settings.NpcRes.ActionRenderOrderTab.Section.Default, mapping.settings.NpcRes.ActionRenderOrderTab.Key.prefix + "" + _direction);
            }

            string[] orderVectorString = orderString.Split(',');

            if (orderVectorString.Length <= 0)
            {
                return 0;
            }

            int result = 0;
            int nextOrderNumber = 0;
            Dictionary<int, string> allPartId = settings.npcres.special.Part.AllPartId();

            foreach (string indexPartId in orderVectorString)
            {
                nextOrderNumber++;

                string partIdString = Regex.Replace(indexPartId, "[^0-9-]", string.Empty);
                int partId = int.Parse(partIdString);
                if (allPartId.ContainsKey(partId) == false)
                {
                    continue;
                }

                if (allPartId[partId].CompareTo(_partName) != 0)
                {
                    continue;
                }

                result = nextOrderNumber;
                break;
            }

            return result;
        }

        public static int PartOrder(string _specialType, string _partName, string _animationName, int _direction)
        {
            if (special.Validation.IsMainMan(_specialType))
            {
                return special.Getters.PartOrder(resource.Cache.Settings.NpcRes.mainManIniMapping, _partName, _animationName, _direction);
            }
            else if (special.Validation.IsMainLady(_specialType))
            {
                return special.Getters.PartOrder(resource.Cache.Settings.NpcRes.mainLadyIniMapping, _partName, _animationName, _direction);
            }

            return 0;
        }

        public static settings.npcres.Structures.PartSprInfo PartSprInfo(
            Dictionary<string, resource.Table> _tableMapping,
            Dictionary<string, string> _headerMapping,
            Dictionary<string, resource.Table> _propertiesMapping,
            string _partName,
            string _animationName,
            int _rowIndex
        )
        {
            if (_tableMapping.ContainsKey(_partName) == false)
            {
                return new();
            }

            resource.Table sprTable = _tableMapping[_partName];
            string sprFile = sprTable.Get<string>(_animationName, _rowIndex);

            if (sprFile.Length <= 0)
            {
                return new();
            }

            while (true)
            {
                if (sprFile.Length >= 9)
                {
                    if (sprFile[..9].CompareTo("..\\woman\\") == 0)
                    {
                        sprFile = sprFile.Remove(0, 3);
                        sprFile = sprFile.Insert(0, mapping.settings.NpcRes.Properties.sprFolderPrefix);
                        break;
                    }
                }

                if (sprFile.Length >= 7)
                {
                    if (sprFile[..7].CompareTo("..\\man\\") == 0)
                    {
                        sprFile = sprFile.Remove(0, 3);
                        sprFile = sprFile.Insert(0, mapping.settings.NpcRes.Properties.sprFolderPrefix);
                        break;
                    }
                }

                if (_headerMapping.ContainsKey(mapping.settings.NpcRes.Kind.Header.resFilePath) == true)
                {
                    sprFile = sprFile.Insert(0, "\\");
                    sprFile = sprFile.Insert(0, _headerMapping[mapping.settings.NpcRes.Kind.Header.resFilePath]);
                    sprFile = sprFile.Insert(0, "\\");
                }

                break;
            }

            if (_propertiesMapping.ContainsKey(_partName) == false)
            {
                return new()
                {
                    sprFullPath = sprFile,
                };
            }

            resource.Table sprPropertiesTable = _propertiesMapping[_partName];
            string sprPropertiesString = sprPropertiesTable.Get<string>(_animationName, _rowIndex);
            settings.npcres.Structures.PartSprInfo result = new()
            {
                sprFullPath = sprFile,
            };

            string[] sprPropertiesVector = sprPropertiesString.Split(',');

            if (sprPropertiesVector.Length >= mapping.settings.NpcRes.SprPropertiesIndexer.frameCount + 1)
            {
                result.frameCount = ushort.Parse("0" + Regex.Replace(sprPropertiesVector[mapping.settings.NpcRes.SprPropertiesIndexer.frameCount], "[^0-9-]", string.Empty));
            }

            if (sprPropertiesVector.Length >= mapping.settings.NpcRes.SprPropertiesIndexer.directionCount + 1)
            {
                result.directionCount = int.Parse(Regex.Replace(sprPropertiesVector[mapping.settings.NpcRes.SprPropertiesIndexer.directionCount], "[^0-9-]", string.Empty));
            }

            if (sprPropertiesVector.Length >= mapping.settings.NpcRes.SprPropertiesIndexer.intervalRatio + 1)
            {
                result.intervalRatio = int.Parse(Regex.Replace(sprPropertiesVector[mapping.settings.NpcRes.SprPropertiesIndexer.intervalRatio], "[^0-9-]", string.Empty));
            }

            return result;
        }

        public static settings.npcres.Structures.PartSprInfo PartSprInfo(
            string _specialType,
            string _partName,
            string _animationName,
            int _rowIndex
        )
        {
            if (npcres.special.Validation.IsMainMan(_specialType))
            {
                return special.Getters.PartSprInfo(
                    resource.Cache.Settings.NpcRes.mainManTableMapping,
                    resource.Cache.Settings.NpcRes.Kind.mainManHeaderValueMapping,
                    resource.Cache.Settings.NpcRes.mainManPartPropertiesTableMapping,
                    _partName,
                    _animationName,
                    _rowIndex
                );
            }

            if (npcres.special.Validation.IsMainLady(_specialType))
            {
                return special.Getters.PartSprInfo(
                    resource.Cache.Settings.NpcRes.mainLadyTableMapping,
                    resource.Cache.Settings.NpcRes.Kind.mainLadyHeaderValueMapping,
                    resource.Cache.Settings.NpcRes.mainLadyPartPropertiesTableMapping,
                    _partName,
                    _animationName,
                    _rowIndex
                );
            }

            return new();
        }

        public static npcres.Structures.PartAnimation PartAnimation(string _specialType, string _animationName, string _partName, int _direction, int _rowIndex, int _speed)
        {
            if (_rowIndex < 0)
            {
                return new();
            }

            settings.npcres.Structures.PartSprInfo partSprInfo = special.Getters.PartSprInfo(_specialType, _partName, _animationName, _rowIndex);

            if (partSprInfo.frameCount <= 0)
            {
                return new();
            }

            npcres.Structures.PartAnimation result = new();
            result.sprPath = partSprInfo.sprFullPath;
            result.framePerDirection = partSprInfo.frameCount / partSprInfo.directionCount;
            result.frameBegin = (ushort)(result.framePerDirection * (_direction - 1));
            result.frameEnd = (ushort)(result.frameBegin + result.framePerDirection - 1);
            result.framePerSeconds = _speed * partSprInfo.intervalRatio;
            result.layerOrder = special.Getters.PartOrder(_specialType, _partName, _animationName, _direction);

            return result;
        }

        public static npcres.Structures.PartAnimation ShadowAnimation(string _specialType, string _animationName, int _direction, int _speed)
        {
            settings.npcres.Structures.PartSprInfo partSprInfo;

            if (npcres.special.Validation.IsMainMan(_specialType))
            {
                if (resource.Cache.Settings.NpcRes.Shadow.mainManAnimationMapping.ContainsKey(_animationName))
                {
                    partSprInfo = resource.Cache.Settings.NpcRes.Shadow.mainManAnimationMapping[_animationName];
                }
                else
                {
                    return new();
                }
            }
            else if (npcres.special.Validation.IsMainLady(_specialType))
            {
                if (resource.Cache.Settings.NpcRes.Shadow.mainLadyAnimationMapping.ContainsKey(_animationName))
                {
                    partSprInfo = resource.Cache.Settings.NpcRes.Shadow.mainLadyAnimationMapping[_animationName];
                }
                else
                {
                    return new();
                }
            }
            else
            {
                return new();
            }

            if (partSprInfo.frameCount <= 0)
            {
                return new();
            }

            npcres.Structures.PartAnimation result = new();
            result.sprPath = partSprInfo.sprFullPath;
            result.framePerDirection = partSprInfo.frameCount / partSprInfo.directionCount;
            result.frameBegin = (ushort)(result.framePerDirection * (_direction - 1));
            result.frameEnd = (ushort)(result.frameBegin + result.framePerDirection - 1);
            result.framePerSeconds = _speed * partSprInfo.intervalRatio;
            result.layerOrder = special.Getters.PartOrder(_specialType, mapping.settings.NpcRes.Shadow.partName, _animationName, _direction);

            return result;
        }

        public static Dictionary<string, npcres.Structures.PartAnimation> PartGroupAnimation(string _specialType, string _animationName, string _partName, int _direction, int _rowIndex, int _speed)
        {
            List<string> partGrouping = special.Getters.PartGroup(_specialType, _partName);
            Dictionary<string, npcres.Structures.PartAnimation> result = new();

            foreach (string part in partGrouping)
            {
                result[part] = special.Getters.PartAnimation(_specialType, _animationName, part, _direction, _rowIndex, _speed);
            }

            return result;
        }
    }
}
