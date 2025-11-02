using Photon.ShareLibrary.Entities;
using System.Runtime.InteropServices;

namespace Photon.JXLoginServer.Entitys
{
    public class ItemTemplate
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileInt(string Section, string Key, int nDefault, string lpFileName);

        byte iequipclasscode;
        ushort idetailtype;
        byte iparticulartype;

        byte ilevel;
        byte iseries;

        byte ix;
        byte iy;
        byte ilocal;

        byte iparam1;
        byte iparam2;
        byte iparam3;
        byte iparam4;
        byte iparam5;
        byte iparam6;

        byte iparamr1;
        byte iparamr2;
        byte iparamr3;
        byte iparamr4;
        byte iparamr5;
        byte iparamr6;

        byte ilucky;

        public ItemTemplate(string path, byte id)
        {
            var section = $"ITEM{id}";

            iequipclasscode = (byte)GetPrivateProfileInt(section, "iequipclasscode", 0, path);
            idetailtype = (ushort)GetPrivateProfileInt(section, "idetailtype", 0, path);
            iparticulartype = (byte)GetPrivateProfileInt(section, "iparticulartype", 0, path);

            ilevel = (byte)GetPrivateProfileInt(section, "ilevel", 0, path);
            iseries = (byte)GetPrivateProfileInt(section, "iseries", 0, path);
            ix = (byte)GetPrivateProfileInt(section, "ix", 0, path);
            iy = (byte)GetPrivateProfileInt(section, "iy", 0, path);
            ilocal = (byte)GetPrivateProfileInt(section, "ilocal", 0, path);

            iparam1 = (byte)GetPrivateProfileInt(section, "iparam1", 0, path);
            iparam2 = (byte)GetPrivateProfileInt(section, "iparam2", 0, path);
            iparam3 = (byte)GetPrivateProfileInt(section, "iparam3", 0, path);
            iparam4 = (byte)GetPrivateProfileInt(section, "iparam4", 0, path);
            iparam5 = (byte)GetPrivateProfileInt(section, "iparam5", 0, path);
            iparam6 = (byte)GetPrivateProfileInt(section, "iparam6", 0, path);

            iparamr1 = (byte)GetPrivateProfileInt(section, "iparamr1", 0, path);
            iparamr2 = (byte)GetPrivateProfileInt(section, "iparamr2", 0, path);
            iparamr3 = (byte)GetPrivateProfileInt(section, "iparamr3", 0, path);
            iparamr4 = (byte)GetPrivateProfileInt(section, "iparamr4", 0, path);
            iparamr5 = (byte)GetPrivateProfileInt(section, "iparamr5", 0, path);
            iparamr6 = (byte)GetPrivateProfileInt(section, "iparamr6", 0, path);

            ilucky = (byte)GetPrivateProfileInt(section, "ilucky", 0, path);
        }
        public void Save(uint cid)
        {
            var itemData = new ItemData
            {
                Equipclasscode = iequipclasscode,
                Detailtype = idetailtype,
                Particulartype = iparticulartype,

                Level = ilevel,
                Series = iseries,

                X = ix,
                Y = iy,
                Local = ilocal,

                Param1 = iparam1,
                Param2 = iparam2,
                Param3 = iparam3,
                Param4 = iparam4,
                Param5 = iparam5,
                Param6 = iparam6,
                
                Paramr1 = iparamr1,
                Paramr2 = iparamr2,
                Paramr3 = iparamr3,
                Paramr4 = iparamr4,
                Paramr5 = iparamr5,
                Paramr6 = iparamr6,

                Lucky = ilucky,
                //Durability = 0,
                //Identify = 0,
                Stack = 1,
            };

            //itemData.Id = "0" + DateTime.Now.ToString("yyyyMMddhhmmss") + PhotonApp.counter.Next;
            //itemData.Cid = cid;

            //DatabaseRepository.Me.AddNewItem(cid,itemData);
        }
    }
}
