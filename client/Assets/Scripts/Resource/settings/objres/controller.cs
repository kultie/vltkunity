using System.Collections.Generic;
using static game.resource.settings.npcres.Structures;

namespace game.resource.settings.objres
{
    public class Controller
    {
        public readonly npcres.Map map;

        protected readonly objres.Shape shape;
        protected readonly npcres.Identification identify;
        protected readonly npcres.Position position;
        protected readonly objres.Animate animate;

        public Controller() 
        {
            this.map = new npcres.Map();

            this.shape = new objres.Shape("game.resource.settings.objres.Controller");
            this.identify = new npcres.Identification();
            this.position = new npcres.Position(this.identify);
            this.animate = new Animate();
        }
        public npcres.Identification GetIdentify() => this.identify;
        public objres.Shape.Appearance GetAppearance() => this.shape.GetAppearance();

        public void SetMapPosition(resource.map.Position _position) => this.position.SetMapPosition(_position);

        public void SetMapPosition(int _top, int _left) => this.position.SetMapPosition(_top, _left);

        public resource.map.Position GetMapPosition() => this.position.GetMapPosition();

        public UnityEngine.Vector3 GetScenePosition() => this.position.GetScenePosition();

        public int GetOrderInMap() => this.position.GetOrderInMap();

        public void SetName(string name) => this.identify.SetName(name);

        public void SetNameColor(int gold)
        {
            UnityEngine.Color color = UnityEngine.Color.white;

            color.b = ((gold >> 0) & 0xff) / 255f;
            color.g = ((gold >> 8) & 0xff) / 255f;
            color.r = ((gold >> 16) & 0xff) / 255f;
            color.a = 1f;//((gold >> 24) & 0xff) / 255f;

            this.identify.SetTextColor(color);
        }

        byte direction = 0;
        public void SetObjDeclareLine(int _declareLine)
        {
            if (!Cache.Settings.ObjData.declareRowIndexToResTypeMapping.ContainsKey(_declareLine))
                return;

            var obj = this.animate.SetObjDeclareLine(_declareLine);

            this.SetName(obj.sprInfo.Name); 
        }
        public void Update()
        {
            var obj = this.animate.GetAnimation();

            npcres.Shape.PartFields part = this.shape.GetPartFields();

            npcres.Shape.PartFrame frameData = this.shape.GetPartFrame(direction, obj.sprPath);

            if (part.spriteRenderer.sprite == frameData.sprite)
            {
                return;
            }

            part.spriteRenderer.sortingOrder = obj.sprInfo.Layer;
            part.rectTransform.anchoredPosition = frameData.anchoredPosition;
            part.rectTransform.sizeDelta = frameData.sizeDelta;
            part.spriteRenderer.sprite = frameData.sprite;
        }
        public void SetDirection(int _direction)
        {
            var info = this.animate.GetAnimation().sprInfo;

            direction = (byte)((info.TotalFrame / info.TotalDir) * _direction);
        }
    }
}