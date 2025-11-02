using Photon.JXGameServer.Entitys;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using KRandom = Photon.ShareLibrary.Utils.KRandom;

namespace Photon.JXGameServer.Items
{
    public struct KItemParam
    {
        public byte Genre;
        public byte Detail;
        public byte Particular;
        public ushort RandRate;
        public byte Quality;
    }
    public class KItemDropRate
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        int RandRange;
        byte MagicRate;
        byte MoneyRate;
        byte MoneyScale;
        byte MinItemLevel;
        byte MinItemLevelScale;
        byte MaxItemLevel;
        byte MaxItemLevelScale;

        List<KItemParam> pItemParam;
        public KItemDropRate()
        {
            pItemParam = new List<KItemParam>();
        }
        public void Load(string path)
        {
            PhotonApp.log.InfoFormat("Load drop from {0}",path);

            RandRange = (int)GetPrivateProfileInt("Main", "RandRange", 0, path);
            MagicRate = (byte)GetPrivateProfileInt("Main", "MagicRate", 0, path);
            MoneyRate = (byte)GetPrivateProfileInt("Main", "MoneyRate", 0, path);
            MoneyScale = (byte)GetPrivateProfileInt("Main", "MoneyScale", 0, path);
            MinItemLevel = (byte)GetPrivateProfileInt("Main", "MinItemLevel", 0, path);
            MinItemLevelScale = (byte)GetPrivateProfileInt("Main", "MinItemLevelScale", 0, path);
            MaxItemLevel = (byte)GetPrivateProfileInt("Main", "MaxItemLevel", 0, path);
            MaxItemLevelScale = (byte)GetPrivateProfileInt("Main", "MaxItemLevelScale", 0, path);

            var count = GetPrivateProfileInt("Main", "Count", 0, path);
            for (int i = 1; i <= count; i++)
            {
                var s= i.ToString();

                var item = new KItemParam();
                item.Genre = (byte)GetPrivateProfileInt(s, "Genre", 0, path);
                item.Detail = (byte)GetPrivateProfileInt(s, "Detail", 0, path);
                item.Particular = (byte)GetPrivateProfileInt(s, "Particular", 0, path);
                item.RandRate = (ushort)GetPrivateProfileInt(s, "RandRate", 0, path);
                item.Quality = (byte)GetPrivateProfileInt(s, "Quality", 0, path);

                pItemParam.Add(item);
            }
        }
        byte CalcLevel(NpcObj npcObj)
        {
            byte nLevel, nMinLevel, nMaxLevel;

            if (MinItemLevel > 0 && MaxItemLevel > 0)
            {
                nLevel = (byte)KRandom.GetRandomNumber(MinItemLevel, MaxItemLevel);
            }
            else
            {
                nMinLevel = (byte)(npcObj.Level / MinItemLevelScale);
                nMaxLevel = (byte)(npcObj.Level / MaxItemLevelScale);

                if (nMinLevel < MinItemLevel)
                    nMinLevel = MinItemLevel;

                if (nMinLevel > MaxItemLevel)
                    nMinLevel = MaxItemLevel;

                if (nMaxLevel < MinItemLevel)
                    nMaxLevel = MinItemLevel;

                if (nMaxLevel > MaxItemLevel)
                    nMaxLevel = MaxItemLevel;

                if (nMaxLevel < nMinLevel)
                {
                    byte nTemp = nMinLevel;
                    nMinLevel = nMaxLevel;
                    nMaxLevel = nTemp;
                }

                nLevel = (byte)KRandom.GetRandomNumber(nMinLevel, nMaxLevel);
            }

            if (nLevel > 10)
                nLevel = 10;
            if (nLevel <= 0)
                nLevel = 1;

            return nLevel;
        }
        void CalcXY(ref int x,ref int y,byte idx)
        {
            switch (idx)
            {
                case 0:
                    return;

                case 1:
                    x -= 20;
                    return;

                case 2:
                    x += 20;
                    return;

                case 3:
                    y -= 20;
                    return;

                case 4:
                    y += 20;
                    return;

                case 5:
                    x -= 40;
                    return;

                case 6:
                    x += 40;
                    return;

                case 7:
                    y -= 40;
                    return;

                case 8:
                    y += 40;
                    return;

                case 9:
                    x -= 40; y -= 40;
                    return;

                case 10:
                    x += 40; y -= 40;
                    return;

                case 11:
                    x -= 40; y += 40;
                    return;

                case 12:
                    x += 40; y += 40;
                    return;
            }
        }
        void CalcItem(NpcObj npcObj,int nBelongPlayer, int X, int Y, byte idx)
        {
            int i = 0, nCheckRand = 0, nRand = KRandom.g_Random(RandRange);
            for (; i < pItemParam.Count; ++i)
            {
                if (nCheckRand <= nRand && nRand < nCheckRand + pItemParam[i].RandRate)
                    break;

                nCheckRand += pItemParam[i].RandRate;
            }
            if (i >= pItemParam.Count)
                return;

            CalcXY(ref X, ref Y, idx);

            byte nLevel = CalcLevel(npcObj);
            ushort nLuck = PhotonApp.LuckyRate;
            
            if (nBelongPlayer > 0)
            {
                int nGoldLuck = 0;// g_GlobalMissionArray.GetMissionValue(28);
                if (nGoldLuck <= 0)
                    nGoldLuck = 1;

                var player = npcObj.scene.FindPlayer(nBelongPlayer);
                if (player != null)
                {
                    nLuck += (ushort)(player.character.Luck * nGoldLuck);
                }
            }

            byte[] pnMagicLevel = new byte[6];
            Array.Clear(pnMagicLevel, 0, 6);

            for (var j = 0; j < 6; ++j)
            {
                if (KRandom.g_RandPercent(MagicRate + nLuck))
                {
                    pnMagicLevel[j] = nLevel;
                }
                else
                {
                    break;
                }
            }

            var item = ItemModule.Me.AddOther(pItemParam[i].Genre, pItemParam[i].Detail, pItemParam[i].Particular, (byte)KRandom.GetRandomNumber(0, 4), nLevel, pnMagicLevel, (byte)nLuck);
            if (item == null)
            {
                PhotonApp.log.Error($"CalcItem: failed to add item {pItemParam[i].Genre}");
                return;
            }
            npcObj.scene.DropItem(nBelongPlayer, item, X, Y);
        }
        public void CalcDrop(NpcObj npcObj,int nBelongPlayer)
        {
            if (pItemParam.Count <= 0)
                return;

            if (MaxItemLevelScale <= 0 || MinItemLevelScale <= 0)
                return;

            int X = 0, Y = 0;
            npcObj.GetMpsPos(ref X, ref Y);
            
            if ((npcObj.scene.DropItemKind & (byte)MAP_ITEMKIND_NODROP.NO_DROP_MONEY) == 0)
            {
                if (KRandom.g_RandPercent(MoneyRate))
                {
                    npcObj.scene.DropMoney(nBelongPlayer, npcObj.Level * MoneyScale * PhotonApp.MoneySale, X, Y);
                }
            }
            if ((npcObj.scene.DropItemKind & (byte)MAP_ITEMKIND_NODROP.NO_DROP_ITEM) == 0)
            {
                var nItemCountNum = npcObj.m_CurrentTreasure;
                if (nItemCountNum <= 0) nItemCountNum = 1;

                for (byte i = 0; i < nItemCountNum; i++)
                {
                    CalcItem(npcObj, nBelongPlayer, X, Y, i);
                }
            }
        }
    }
}
