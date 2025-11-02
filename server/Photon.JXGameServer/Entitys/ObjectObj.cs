using System.Collections.Generic;
using Photon.JXGameServer.Helpers;
using Photon.JXGameServer.Items;
using Photon.JXGameServer.Maps;
using Photon.JXGameServer.Modulers;
using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Handlers;
using Photon.SocketServer;

namespace Photon.JXGameServer.Entitys
{
    public class ObjectObj : MapObj
    {
        public ObjKind Kind;
        public int nTemplateID;

        public int iMoney;
        public ItemObj iItem;
        public short m_nBelongTime, m_nLifeTime;

        public ObjectObj(SceneObj scene, RegionObj region):base(scene, region) 
        {
            m_nBelongTime = m_nLifeTime = 0;
        }
        public bool LoadData(KSPObj kSPObj)
        {
            if (AddMap(kSPObj.nPositionX, kSPObj.nPositionY) >= 0)
            {
                nTemplateID = kSPObj.nTemplateID;

                var dir = kSPObj.Dir;
                if (dir >= 0)
                {
                    m_Dir = (byte)(dir & 0xf);
                }
                Kind = SceneModule.Me.GetObjectKind(nTemplateID);

                m_Script = kSPObj.Scripts;
                return true;
            }
            return false;
        }
        public void SyncNormal(PlayerObj me)
        {
            var param = new Dictionary<byte, object>();

            int X = 0, Y = 0;
            GetMpsPos(ref X, ref Y);

            param.Add((byte)ParamterCode.Id, id);
            param.Add((byte)ParamterCode.Kind, (byte)Kind);
            param.Add((byte)ParamterCode.NpcType, nTemplateID);

            param.Add((byte)ParamterCode.MapX, X);
            param.Add((byte)ParamterCode.MapY, Y);
            param.Add((byte)ParamterCode.Dir, m_Dir);

            if (Kind == ObjKind.Obj_Kind_Item)
            {
                param.Add((byte)ParamterCode.Name, iItem.m_CommonAttrib.szItemName);
                param.Add((byte)ParamterCode.Data, SceneModule.Me.ObjNameColor(iItem.ColorName()));
            }
            else
            if (Kind == ObjKind.Obj_Kind_Money)
            {
                param.Add((byte)ParamterCode.Name, string.Format("{0} lượng", iMoney));
                param.Add((byte)ParamterCode.Data, SceneModule.Me.ObjNameColor(3));
            }
            else
            if (Kind == ObjKind.Obj_Kind_Prop)
            {
                param.Add((byte)ParamterCode.Data, m_nBelongTime);
            }

            me.ClientPeer.SendEvent(new EventData
            {
                Code = (byte)OperationCode.SendObj,
                Parameters = param,
            }, me.sendParameters);
        }
        public void PickUpObj(PlayerObj obj)
        {
            switch (Kind)
            {
                case ObjKind.Obj_Kind_Money:
                    if (m_nBelongTime > 0)
                        if (m_nPeopleIdx != obj.id)
                            return;

                    obj.character.Money = obj.character.Money + iMoney;
                    break;

                case ObjKind.Obj_Kind_Item:
                    if (m_nBelongTime > 0)
                        if (m_nPeopleIdx != obj.id)
                            return;

                    if (!obj.AddItem(iItem))
                        return;
                    break;

                default:
                    var script = ScriptModule.Me.GetScript(m_Script);
                    if (script != null)
                    {
                        script.SetObject(this.id);
                        script.SetPlayer(obj.id);
                        script.CallFunction(scene, "main");
                    }
                    obj.m_nObjectIdx = 0;
                    return;
            }
            obj.m_nObjectIdx = 0;
            region.RemoveObjectObj(this);
        }
        public void SetState(byte nState)
        {
            m_nBelongTime = nState;

            if (nState == 1)//hide
                m_nLifeTime = SceneModule.Me.GetLifeTime(nTemplateID);
            else//show
                m_nLifeTime = 0;

            SyncMe();
        }
        public void SetItemBelong(int nPlayerIdx)
        {
            m_nPeopleIdx = nPlayerIdx;
            if (nPlayerIdx > 0)
                m_nBelongTime = 600;

            m_nLifeTime = SceneModule.Me.GetLifeTime(nTemplateID);
        }
        public void HeartBeat()
        {
            switch (Kind)
            {
                case ObjKind.Obj_Kind_Money:
                case ObjKind.Obj_Kind_Item:
                    if (m_nBelongTime > 0)
                        --m_nBelongTime;

                    if (--m_nLifeTime == 0)
                        region.RemoveObjectObj(this);
                    break;

                case ObjKind.Obj_Kind_Prop:
                    if (m_nBelongTime == 1)
                    {
                        if (--m_nLifeTime <= 0)
                        {
                            m_nBelongTime = 0;

                            SyncMe();
                        }
                    }
                    break;
            }
        }
        void SyncMe()
        {
            var param = new Dictionary<byte, object>();
            param.Add((byte)ParamterCode.Id, id);
            param.Add((byte)ParamterCode.Data, m_nBelongTime);

            region.BroadCast(new EventData
            {
                Code = (byte)OperationCode.SyncObj,
                Parameters = param,
            });
        }
    }
}
