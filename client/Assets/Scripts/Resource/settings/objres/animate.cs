using System.Collections.Generic;
using Unity.VisualScripting;

namespace game.resource.settings.objres
{
    public struct AnimateStructure
    {
        public game.resource.ObjSpr sprInfo;
        public string sprPath;
    };
    public class Animate
    {
        private int declareLine;
        AnimateStructure declareAnimate;

        public Animate()
        {
            this.declareLine = 1;
            this.declareAnimate = new AnimateStructure();

            this.ResetAllPartAnimation();
        }

        private void ResetAllPartAnimation()
        {
            this.declareAnimate.sprPath = resource.Cache.Settings.ObjData.declareRowIndexToResTypeMapping[this.declareLine];
            this.declareAnimate.sprInfo = resource.Cache.Settings.ObjData.declareRowIndexToStatureMapping[this.declareAnimate.sprPath];
        }

        public AnimateStructure SetObjDeclareLine(int _declareLine)
        {
            this.declareLine = _declareLine;
            ResetAllPartAnimation();

            return this.declareAnimate;
        }

        public AnimateStructure GetAnimation() => this.declareAnimate;
    }
}
