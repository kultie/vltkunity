using ExitGames.Logging;
using JX.Database;
using Photon.JXGameServer.Common;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Entities;
using Photon.ShareLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Photon.JXGameServer.Items
{
    public struct ItemGenerate
    {
        int Genre;
        int DetailType;
        int ParticularType;
        int SeriesReq;
        int Level;
        int Price;
        int PriceXu;

        public static explicit operator ItemGenerate(int v)
        {
            throw new NotImplementedException();
        }
    }

    public struct ItemFilePath
    {
        public static readonly string TABFILE_PATH = "settings\\item\\004";
        public static readonly string TABFILE_ME = "mine.txt";
        public static readonly string TABFILE_TASK = "questkey.txt";
        public static readonly string TABFILE_FUSION = "fusion.txt";   //纹纲
        public static readonly string TABFILE_MEDICE = "potion.txt";
        public static readonly string TABFILE_MEDMATERIAL = "medmaterialbase.txt";
        public static readonly string TABFILE_MELEEWEAPON = "meleeweapon.txt";
        public static readonly string TABFILE_RANGEWEAPON = "rangeweapon.txt";
        public static readonly string TABFILE_ARMOR = "armor.txt";   //衣服
        public static readonly string TABFILE_HELM = "helm.txt";    //帽子
        public static readonly string TABFILE_BOOT = "boot.txt";
        public static readonly string TABFILE_BELT = "belt.txt";
        public static readonly string TABFILE_AMULET = "amulet.txt";
        public static readonly string TABFILE_RG = "ring.txt";
        public static readonly string TABFILE_CUFF = "cuff.txt";
        public static readonly string TABFILE_PENDANT = "pendant.txt";
        public static readonly string TABFILE_HORSE = "horse.txt";
        public static readonly string TABFILE_TOWNPORTAL = "townportal.txt";
        //public static readonly string TABFILE_EQUIPMENT_UNIQUE = "unique.txt";      //蓝装？
        public static readonly string TABFILE_MAGICATTRIB = "magicattrib.txt"; //魔法属性
        public static readonly string TABFILE_GOLDITEM = "golditem.txt";
        public static readonly string TABFILE_MASK = "mask.txt";	    // mat na
        //public static readonly string TABFILE_PIFENG = "Pifeng.txt";	// 披风
        //public static readonly string TABFILE_YJIAAN = "Yinjian.txt";	// 印鉴
        public static readonly string TABFILE_SHIP = "shipin.txt";	// 饰品
        public static readonly string TABFILE_PLATA = "platinaequip.txt"; // 白金
        public static readonly string TABFILE_MAGICAGOLD = "magicattrib_ge.txt"; // 白金

        // Magic
        public static readonly string TABFILE_MAGICATTRIB_PATH = "settings\\item\\004\\magicattrib.txt";
        public static readonly string TABFILE_MAGICAGOLD_PATH = "settings\\item\\004\\magicattrib_ge.txt";
        public static readonly string TABFILE_MAGICALEVEL_PATH = "settings\\item\\004\\magicattriblevel_index.txt";
    }

    public class LibOfBaseProp
    {
        public KBPT_Equipment rangeWeapon;
        public KBPT_Equipment meleeWeapon;
        public KBPT_Equipment armor;
        public KBPT_Equipment helm;
        public KBPT_Equipment boot;
        public KBPT_Equipment belt;
        public KBPT_Equipment amulet;
        public KBPT_Equipment ring;
        public KBPT_Equipment cuff;
        public KBPT_Equipment pendant;
        public KBPT_Equipment horse;
        public KBPT_Equipment mask;
        // KBPT_Equipment pifeng = new KBPT_Equipment(root, ItemFilePath.TABFILE_PIFENG);
        // KBPT_Equipment yinjiaan = new KBPT_Equipment(root, ItemFilePath.TABFILE_YJIAAN);
        public KBPT_Equipment shipin;
        public KBPT_Equipment_Gold goldItem;   // 15 锟狡斤拷装
        public KBPT_Medicine medicine;
        public KBPT_Quest quest;
        public KBPT_TownPortal townPortal;
        public KBPT_MagicAttribute_TF magicAttrib;
        public KBPT_MagicGoldAttribute_TF magicGoldAttrib;

        // KBPT_Fusion fusion = new KBPT_Fusion(root, ItemFilePath.TABFILE_FUSION);
        public KBPT_Equipment_Platina platina;
        // public KBPT_Mine mine;

        public static readonly int MATF_CBDR = 15;
        public static readonly int MATF_PREFIXPOSFIX = 2;
        public static readonly int MATF_SERIES = 5;
        public static readonly int MATF_LEVEL = 10;
        public KBPT_ClassMAIT[,,,] m_CMAIT = new KBPT_ClassMAIT[MATF_PREFIXPOSFIX, MATF_CBDR, MATF_SERIES, MATF_LEVEL];

        public LibOfBaseProp(string root, ILogger log)
        {
            rangeWeapon = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_RANGEWEAPON);
            meleeWeapon = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_MELEEWEAPON);
            armor = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_ARMOR);
            helm = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_HELM);
            boot = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_BOOT);
            belt = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_BELT);
            amulet = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_AMULET);
            ring = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_RG);
            cuff = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_CUFF);
            pendant = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_PENDANT);
            horse = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_HORSE);
            mask = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_MASK);
            shipin = new KBPT_Equipment(log, root, ItemFilePath.TABFILE_SHIP);
            goldItem = new KBPT_Equipment_Gold(log, root, ItemFilePath.TABFILE_GOLDITEM);   // 15 锟狡斤拷装
            medicine = new KBPT_Medicine(log, root, ItemFilePath.TABFILE_MEDICE);
            quest = new KBPT_Quest(log, root, ItemFilePath.TABFILE_TASK);
            townPortal = new KBPT_TownPortal(log, root, ItemFilePath.TABFILE_TOWNPORTAL);
            magicAttrib = new KBPT_MagicAttribute_TF(log, root, ItemFilePath.TABFILE_MAGICATTRIB);
            platina = new KBPT_Equipment_Platina(log, root, ItemFilePath.TABFILE_PLATA);
            magicGoldAttrib = new KBPT_MagicGoldAttribute_TF(log, root, ItemFilePath.TABFILE_MAGICAGOLD);
            //mine = new KBPT_Mine(root, ItemFilePath.TABFILE_ME);
            InitMagicAttrIndexTable();
        }

        public void InitMagicAttrIndexTable()
        {
            bool nResult = false;
            int i = 0;
            int nPrefixPostfix = 0;
            int nType = 0;
            int nSeries = 0;
            int nSeriesMin = 0;
            int nSeriesMax = 0;

            int nLevel = 0;
            int nLevelMin = 0;
            int nLevelMax = 0;

            int nTotalCount = 0;
            for (i = 0; i < magicAttrib.props.Length; ++i)
            {
                MagicAttributeTabFile item = magicAttrib.props[i];
                // pItem->m_nUseFlag = false;
                item.m_nUseFlag = false;
                nPrefixPostfix = item.Pos;
                for (nType = 0; nType < MATF_CBDR; ++nType)
                {
                    int dropRate = (int)item.GetType().GetField(string.Format("DropRate{0}", nType)).GetValue(item);
                    if (dropRate <= 1)
                        continue;
                    nSeriesMin = nSeriesMax = item.Class;
                    if (item.Class == -1)
                    {
                        nSeriesMin = 0;
                        nSeriesMax = MATF_SERIES - 1;
                    }
                    for (nSeries = nSeriesMin; nSeries <= nSeriesMax; ++nSeries)
                    {
                        nLevelMin = item.Level;
                        nLevelMax = MATF_LEVEL;
                        for (nLevel = nLevelMin; nLevel <= nLevelMax; ++nLevel)
                        {
                            if (m_CMAIT[nPrefixPostfix, nType, nSeries, nLevel - 1] == null)
                            {
                                m_CMAIT[nPrefixPostfix, nType, nSeries, nLevel - 1] = new KBPT_ClassMAIT();
                            }
                            m_CMAIT[nPrefixPostfix, nType, nSeries, nLevel - 1].Insert(i);
                            nTotalCount++;
                        }
                    }
                }
            }
            Debug.WriteLine(m_CMAIT.Length.ToString());
        }

        public KBPT_ClassMAIT GetCMIT(int nPrefixPostfix, int nType, int nSeries, int nLevel)
        {
            if (!((nPrefixPostfix >= 0) && (nPrefixPostfix < MATF_PREFIXPOSFIX)))
                return null;
            if (!((nType >= 0) && (nType < MATF_CBDR)))
                return null;
            if (!((nSeries >= 0) && (nSeries < MATF_SERIES)))
                return null;
            if (!(((nLevel - 1) >= 0) && ((nLevel - 1) < MATF_LEVEL)))
                return null;
            return m_CMAIT[nPrefixPostfix, nType, nSeries, nLevel - 1];
        }

        public BasicPropEquiment GetMeleeWeaponRecord(string key)
        {
            int index = meleeWeapon.FindItemRecord(key);
            return meleeWeapon.props[index];
        }

        internal BasicPropEquiment GetAmuletRecord(string key)
        {
            int index = amulet.FindItemRecord(key);
            return amulet.props[index];
        }

        internal BasicPropEquiment GetArmorRecord(string key)
        {
            int i = armor.FindItemRecord(key);
            return armor.props[i];
        }

        internal BasicPropEquiment GetBeltRecord(string key)
        {
            int i = belt.FindItemRecord(key);
            return belt.props[i];
        }

        internal BasicPropEquiment GetBootRecord(string key)
        {
            int i = boot.FindItemRecord(key);
            return boot.props[i];
        }

        internal BasicPropEquiment GetCuffRecord(string key)
        {
            int i = cuff.FindItemRecord(key);
            return cuff.props[i];
        }

        internal BasicPropEquiment GetHelmRecord(string key)
        {
            int i = helm.FindItemRecord(key);
            return helm.props[i];
        }

        internal BasicPropEquiment GetHorseRecord(string key)
        {
            int i = horse.FindItemRecord(key);
            return horse.props[i];
        }

        internal BasicPropEquiment GetMaskRecord(string key)
        {
            int i = horse.FindItemRecord(key);
            return horse.props[i];
        }

        internal BasicPropEquiment GetPendantRecord(string key)
        {
            int i = pendant.FindItemRecord(key);
            return pendant.props[i];
        }

        internal BasicPropEquiment GetPifengRecord(string key)
        {
            throw new NotImplementedException();
        }

        internal BasicPropEquiment GetRangeWeaponRecord(string key)
        {
            int i = rangeWeapon.FindItemRecord(key);
            return rangeWeapon.props[i];
        }

        internal BasicPropEquiment GetRingRecord(string key)
        {
            int i = ring.FindItemRecord(key);
            return ring.props[i];
        }

        internal BasicPropEquiment GetShipinRecord(string key)
        {
            int i = shipin.FindItemRecord(key);
            return shipin.props[i];
        }

        internal BasicPropEquiment GetYinjianRecord(string key)
        {
            throw new NotImplementedException();
        }
        internal MagicAttributeTabFile GetMARecord(int i)
        {
            return magicAttrib.props[i];
        }

        internal BasicPropMedicine GetMedicineRecord(string key)
        {
            int i = medicine.FindItemRecord(key);
            return medicine.props[i];
        }

        internal BasicPropEquimentGold GetGoldItemRecord(int idx)
        {
            return goldItem.props[idx - 1];
        }
    }
    public class ItemGeneratorParam
    {
        public byte[] GeneratorLevel = new byte[6];  // 魔法
        public byte[] RGeneratorLevel = new byte[6]; // 熔炼
        public byte[] JGeneratorLevel = new byte[7]; // 基础属性
        public byte Luck;               // 幸运值
    }
    public class ItemModule
    {
        public static ItemModule Me;
        public string root;
        LibOfBaseProp LibOfBaseProp;
        public int ItemIndex = 0;
        public Dictionary<int, ItemObj> ItemSet;

        public ItemModule()
        {
            Me = this;
            ItemSet = new Dictionary<int, ItemObj>();
        }
        public void LoadConfig(string root)
        {
            this.root = root;
            LibOfBaseProp = new LibOfBaseProp(root, PhotonApp.log);
        }

        public ItemObj AddOther(
                  byte nItemGenre,
                  ushort nDetailType/*=-1*/,
                  byte nParticularType/*=-1*/,
                  byte nSeries,
                  byte nLevel,
                  byte[] pnMagicLevel,
                  byte nLuck,
                  byte nStackNum = 1,
                  byte nEnChance = 0,
                  byte nPoint = 0,
                  byte nRongpiont = 0,
                  byte nYear = 0,
                  byte nMonth = 0,
                  byte nDay = 0,
                  byte nHour = 0,
                  byte nMin = 0,
                  int[] pnRMagicLevel = null,
                  byte[] pnJbLevel = null,
                  int[] pnBsLevel = null,
                  int IsWhere = 0,
                  int nIsLogin = 0)
        {
            ItemObj item = new ItemObj();

            if (nLevel <= 0)
            {
                nLevel = 1;
            }
            switch ((ItemGenre)nItemGenre)
            {
                case ItemGenre.item_equip:
                    Gen_Equipment(nDetailType, nParticularType, nSeries, nLevel, pnMagicLevel, nLuck, item, nEnChance, nPoint, nRongpiont, 0, 0, 0, 0, 0, 0);
                    break;
                case ItemGenre.item_medicine:
                    Gen_Medicine(nDetailType, nLevel, item, nStackNum);
                    break;
                case ItemGenre.item_mine:
                    //Gen_Mine(nParticularType, item, nStackNum, nLevel, nSeries, nLuck, pnJbLevel);
                    break;
                case ItemGenre.item_materials:
                    break;
                case ItemGenre.item_task:
                    Gen_Quest(nDetailType, item, nStackNum, nLevel);
                    break;
                case ItemGenre.item_townportal:
                    Gen_TownPortal(item);
                    break;
                case ItemGenre.item_fusion:
                    Gen_Fusion(nParticularType, item, nStackNum, nLevel, nSeries, nLuck, pnJbLevel);
                    break;
                case ItemGenre.item_number:
                    break;
                default:
                    break;
            }
            if (item.m_CommonAttrib == null) return null;
            return AddToSet(item.ItemData);
        }

        public ItemObj LoadFromExist(ItemData itemData)
        {
            return null;
        }

        internal bool RemoveItemFromDB(string itemId)
        {
            try
            {
                var itemObjs = ItemSet.Values.ToList();
                int indx = itemObjs.FindIndex((el) => el.m_CommonAttrib.ItemDataId == itemId);
                RemoveItem(indx);
                //var reply = DatabaseRepository.Me.RemoveItem(itemId);
                //return reply.Code == ReturnCode.Ok;
            }
            catch (Exception ex)
            {
                PhotonApp.log.Error(ex);
            }
            return false;
        }
        internal bool RemoveItem(int index)
        {
            return ItemSet.Remove(index);
        }

        public bool GenMagicAttribute(int nType,
                                   byte[] pnaryMALevel,
                                   int nSeriesReq,
                                   int nLucky,
                                   KMagicAttrib[] srcArrMA)
        {
            var pnaryMA = new List<KMagicAttrib>();
            KBPT_ClassMAIT SelectedMagicTable = new KBPT_ClassMAIT();
            MagicAttributeTabFile[] pMagicAttrTable = new MagicAttributeTabFile[6];
            Utils.InitArr(pMagicAttrTable);
            bool isShowMagic = true;
            for (int i = 0; i < srcArrMA.Length; i++)
            {
                if (pnaryMALevel[i] == 0) break;
                KBPT_ClassMAIT pCMITItem = null;
                for (int level = pnaryMALevel[i]; level > 0; level--)
                {
                    pCMITItem = LibOfBaseProp.GetCMIT(
                                1 - (i & 1),
                                nType,
                                nSeriesReq,
                                level
                                );
                    if (pCMITItem != null)
                    {
                        break;
                    }
                }
                if (pCMITItem == null) continue;
                int nCMITItemCount = pCMITItem.GetCount();
                MagicAttributeTabFile pMAItem = null;
                for (int j = 0; j < nCMITItemCount; ++j)
                {
                    int nMAIndex = pCMITItem.Get(j);
                    MagicAttributeTabFile pMAItem1 = LibOfBaseProp.GetMARecord(nMAIndex);
                    if (pMAItem1 == null) continue;
                    if (pMAItem1.m_nUseFlag) continue;
                    if ((int)pMAItem1.GetType().GetField(string.Format("DropRate{0}", nType)).GetValue(pMAItem1) <= 1) // 25/1250||3000/160000  10--2000
                        continue;
                    if (pMagicAttrTable.ToList().FindIndex((item) => item.PropKind == pMAItem1.PropKind) > -1) continue;
                    pMAItem = pMAItem1;
                    break;
                }
                if (pMAItem == null)
                {
                    continue;
                }
                int magicCheckNum = 1 - (i & 1);
                if (isShowMagic != (magicCheckNum == 1))
                {
                    continue;
                }
                pMagicAttrTable[i] = pMAItem;
                pMAItem.m_nUseFlag = true;
                var magicAttrib = new KMagicAttrib();
                //magicAttrib.AttributeIndex = magicIndex;
                magicAttrib.nAttribType = (short)pMAItem.PropKind;
                magicAttrib.nValue[0] = (short)KRandom.GetRandomNumber(pMAItem.MagicAttriRangeMin0, pMAItem.MagicAttriRangeMax0);
                magicAttrib.nValue[1] = (short)KRandom.GetRandomNumber(pMAItem.MagicAttriRangeMin1, pMAItem.MagicAttriRangeMax1);
                magicAttrib.nValue[2] = (short)KRandom.GetRandomNumber(pMAItem.MagicAttriRangeMin2, pMAItem.MagicAttriRangeMax2);
                pnaryMA.Add(magicAttrib);
                isShowMagic = !isShowMagic;
                /*  if (pnaryMA[i].Value2 > 1)
                  {
                      TabFile nJnLevel = new TabFile(Path.Combine(Global.Root, ItemFilePath.TABFILE_MAGICAGOLD_PATH));
                      int nMinVal, nMaxVal;
                      nMinVal = nJnLevel.GetInteger(6, pnaryMA[i].Value2, 0);
                      nMaxVal = nJnLevel.GetInteger(7, pnaryMA[i].Value2, 0);
                      pnaryMA[i].Value0 = KRandom.GetRandomNumber(nMinVal, nMaxVal);
                      nMinVal = nJnLevel.GetInteger(10, pnaryMA[i].Value2, 0);
                      nMaxVal = nJnLevel.GetInteger(11, pnaryMA[i].Value2, 0);
                      pnaryMA[i].Value2 = KRandom.GetRandomNumber(nMinVal, nMaxVal);
                  }*/
            }
            for (int i = 0; i < 6; ++i)
            {
                if (pMagicAttrTable[i] == null)
                    break;
                pMagicAttrTable[i].m_nUseFlag = false;
            }
            for (int i = 0; i < pnaryMA.Count; i++)
            {
                srcArrMA[i].nAttribType = pnaryMA[i].nAttribType;
                srcArrMA[i].nValue[0] = pnaryMA[i].nValue[0];
                srcArrMA[i].nValue[1] = pnaryMA[i].nValue[1];
                srcArrMA[i].nValue[2] = pnaryMA[i].nValue[2];
            }

            return true;
        }

        public bool Gen_Equipment(ushort nDetailType,
                                   byte nParticularType,
                                   byte nSeriesReq,
                                   byte nLevel,
                                   byte[] pnaryMALevel,
                                   byte nLucky,
                                   ItemObj item,
                                   byte nEnChance,
                                   byte nPoint,
                                   int nRongpoint,
                                   int IsWhere,
                                   int nIsShop,
                                   int nPrice,
                                   int nFsxu,
                                   int nIsBang,
                                   int nPriceType)
        {
            bool bEC = false;
            if (pnaryMALevel.Length > 0)
            {
                item.m_GeneratorParam.GeneratorLevel = pnaryMALevel;
            }
            item.m_GeneratorParam.Luck = nLucky;

            string indexCacheKey = string.Format("{0},{1},{2},{3}", (int)ItemGenre.item_equip, nDetailType, nParticularType, nLevel);
            BasicPropEquiment basicProp = null;
            switch ((EquipDetailType)nDetailType)
            {
                case EquipDetailType.equip_meleeweapon:
                    basicProp = LibOfBaseProp.GetMeleeWeaponRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_rangeweapon:
                    basicProp = LibOfBaseProp.GetRangeWeaponRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_armor:
                    basicProp = LibOfBaseProp.GetArmorRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_ring:
                    basicProp = LibOfBaseProp.GetRingRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_amulet:
                    basicProp = LibOfBaseProp.GetAmuletRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_boots:
                    basicProp = LibOfBaseProp.GetBootRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_belt:
                    basicProp = LibOfBaseProp.GetBeltRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_helm:
                    basicProp = LibOfBaseProp.GetHelmRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_cuff:
                    basicProp = LibOfBaseProp.GetCuffRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_pendant:
                    basicProp = LibOfBaseProp.GetPendantRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_horse:
                    basicProp = LibOfBaseProp.GetHorseRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_mask:
                    basicProp = LibOfBaseProp.GetMaskRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_pifeng:
                    basicProp = LibOfBaseProp.GetPifengRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_yinjian:
                    basicProp = LibOfBaseProp.GetYinjianRecord(indexCacheKey);
                    break;
                case EquipDetailType.equip_shiping:
                    basicProp = LibOfBaseProp.GetShipinRecord(indexCacheKey);
                    break;
                default:
                    break;
            }
            if (basicProp == null)
            {
                return false;
            }
            item.SetAttrib_CBR(basicProp);
            item.SetSeries(nSeriesReq);
            for (int k = 0; k < pnaryMALevel.Length; k++)
            {
                if (basicProp.Level > 0 && pnaryMALevel[k] > basicProp.Level)
                {
                    pnaryMALevel[k] = (byte)basicProp.Level;
                }
            }
            if (nPoint == 0)
            {
                if (pnaryMALevel.Length == 0)
                    return false;
                KMagicAttrib[] sMA = new KMagicAttrib[6]; // 道具的魔法属性
                Utils.InitArr(sMA);
                bEC = GenMagicAttribute(nDetailType, pnaryMALevel, nSeriesReq, nLucky, sMA);
                if (bEC)
                    item.SetAttrib_MA(sMA); // 设置魔法属性
                                            // item.SetBackUpMagicAttribute();//备份原始魔法属性(用于强化)
                item.EnChance(nEnChance);
                if (nIsShop > 0)
                {
                    if (nFsxu > 0)
                        item.SetPerXu(nFsxu); // 修改金币
                    if (nPrice > 0)
                        item.SetPerPrice(nPrice); // 修改价格
                    item.SetBang(nIsBang);
                }
                return bEC;
            }
            else
            if (nPoint == 7)
            {
                KMagicAttrib[] sMA = new KMagicAttrib[6];
                Utils.InitArr(sMA);
                TabFile tabMagicAttributee = new TabFile(Path.Combine(Global.Root, ItemFilePath.TABFILE_MAGICATTRIB_PATH));
                if (nLucky > 10) // 0--10之间
                {
                    nLucky = 10;
                }
                else if (nLucky < 0)
                {
                    nLucky = 0;
                }
                int nType, nMax, nMin;
                for (int j = 0; j < 6; ++j)
                {
                    if (item.m_GeneratorParam.GeneratorLevel[j] > 0)
                    {
                        nType = tabMagicAttributee.GetInteger(5, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMax = tabMagicAttributee.GetInteger(6, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMin = tabMagicAttributee.GetInteger(7, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        sMA[j].nAttribType = (short)nType;

                        sMA[j].nValue[0] = (short)(nMax + ((nMin - nMax) * nLucky / 10));

                        nMax = tabMagicAttributee.GetInteger(8, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMin = tabMagicAttributee.GetInteger(9, item.m_GeneratorParam.GeneratorLevel[j], 0);

                        sMA[j].nValue[1] = (short)(nMax + ((nMin - nMax) * nLucky / 10));

                        nMax = tabMagicAttributee.GetInteger(10, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMin = tabMagicAttributee.GetInteger(11, item.m_GeneratorParam.GeneratorLevel[j], 0);

                        sMA[j].nValue[2] = (short)(nMax + ((nMin - nMax) * nLucky / 10));

                        if (sMA[j].nValue[2] > 1)
                        {
                            TabFile nJnLevel = new TabFile(Path.Combine(Global.Root, ItemFilePath.TABFILE_MAGICAGOLD_PATH));
                            int nMinVal, nMaxVal, nRows;
                            nRows = nMax + IsWhere;
                            if (nRows > nMin)
                                nRows = nMin;
                            nMinVal = nJnLevel.GetInteger(6, nRows, 0);
                            nMaxVal = nJnLevel.GetInteger(7, nRows, 0);
                            sMA[j].nValue[0] = (short)KRandom.GetRandomNumber(nMinVal, nMaxVal);
                            nMinVal = nJnLevel.GetInteger(10, nRows, 0);
                            nMaxVal = nJnLevel.GetInteger(11, nRows, 0);
                            sMA[j].nValue[2] = (short)KRandom.GetRandomNumber(nMinVal, nMaxVal);
                        }
                        continue;
                    }

                    sMA[j].nAttribType = 0;
                    sMA[j].nValue[0] = sMA[j].nValue[1] = sMA[j].nValue[2] = 0;
                }
                item.SetAttrib_MA(sMA);
                item.SetPoint(nPoint);
                item.SetIsWhere(IsWhere);
                item.EnChance(nEnChance);
                if (nIsShop > 0)
                {
                    if (nFsxu > 0)
                        item.SetPerXu(nFsxu);
                    if (nPrice > 0)
                        item.SetPerPrice(nPrice);
                    item.SetBang(nIsBang);
                }
                return bEC;
            }
            else
            {
                KMagicAttrib[] sMA = new KMagicAttrib[6];
                Utils.InitArr(sMA);
                TabFile tabMagicAttributee = new TabFile(Path.Combine(Global.Root, ItemFilePath.TABFILE_MAGICATTRIB_PATH));
                if (nLucky > 10) // 0--10之间
                {
                    nLucky = 10;
                }
                else if (nLucky < 0)
                {
                    nLucky = 0;
                }
                int nType, nMax, nMin;
                for (int j = 0; j < 6; ++j)
                {
                    if (item.m_GeneratorParam.GeneratorLevel[j] > 0 && j < nPoint)
                    {
                        nType = tabMagicAttributee.GetInteger(5, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMax = tabMagicAttributee.GetInteger(6, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMin = tabMagicAttributee.GetInteger(7, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        sMA[j].nAttribType = (short)nType;
                        sMA[j].nValue[0] = (short)(nMax + ((nMin - nMax) * nLucky / 10));
                        nMax = tabMagicAttributee.GetInteger(8, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMin = tabMagicAttributee.GetInteger(9, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        sMA[j].nValue[1] = (short)(nMax + ((nMin - nMax) * nLucky / 10));
                        nMax = tabMagicAttributee.GetInteger(10, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        nMin = tabMagicAttributee.GetInteger(11, item.m_GeneratorParam.GeneratorLevel[j], 0);
                        sMA[j].nValue[2] = (short)(nMax + ((nMin - nMax) * nLucky / 10));
                        continue;
                    }
                    // 剩下的设定为空孔
                    sMA[j].nAttribType = 0;
                    sMA[j].nValue[0] = sMA[j].nValue[1] = sMA[j].nValue[2] = 0;
                }
                item.SetAttrib_MA(sMA); // 设置魔法属性
                item.SetPoint(nPoint);
                item.EnChance(nEnChance);
                if (nIsShop > 0)
                {
                    if (nFsxu > 0)
                        item.SetPerXu(nFsxu);
                    if (nPrice > 0)
                        item.SetPerPrice(nPrice);

                    item.SetBang(nIsBang);
                }
                return bEC;
            }
        }

        public bool Gen_Medicine(ushort nDetailType,
                                   int nLevel,
                                    ItemObj pItem,
                                   byte nStackNum)
        {
            BasicPropMedicine pMed;
            bool bEC = false;
            string indexCacheKey = string.Format("{0},{1},{2},{3}", (int)ItemGenre.item_medicine, nDetailType, 0, nLevel);
            pMed = LibOfBaseProp.GetMedicineRecord(indexCacheKey);
            pItem = new ItemObj(pMed);
            pItem.m_GeneratorParam.Luck = 0;
            if (null == pMed)
            {
                return bEC;
            }
            pItem.SetStackNum(nStackNum);
            return true;
        }

        public void Gen_Quest(ushort nDetailType, ItemObj pItem, int nStackNum, int nLevel)
        {
        }

        public void Gen_Fusion(int nParticularType,
                                 ItemObj pItem,
                                 int nStackNum,
                                 int nLevel,
                                 int nSeries,
                                 int nLuck,
                                  byte[] npnaryJBLevel)
        {
        }
        public bool Gen_TownPortal(ItemObj pItem)
        {
            BasicPropTownPortal prop = LibOfBaseProp.townPortal.props[0];
            if (null == prop) return false;
            pItem.SetBasicProp(prop);
            return true;
        }

        public List<ItemObj> AddToSet(List<ItemData> items)
        {
            return items.Select(el => AddToSet(el)).ToList();
        }
        public ItemObj AddToSet(ItemData item)
        {
            ItemObj itemObj = LoadFromExist(item);
            ItemIndex += 1;
            itemObj.m_CommonAttrib.SetIdx = ItemIndex;
            ItemSet[ItemIndex] = itemObj;
            return itemObj;
        }
    }
}
