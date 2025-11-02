using UnityEngine;
using UnityEngine.UI;

namespace game.scene.world.userInterface
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField]
        Image mapMask;
        [SerializeField]
        Text mapName;
        [SerializeField]
        Text mapPos;

        private game.resource.map.MiniMap miniMapHandle;

        public void SetHandle(game.resource.map.MiniMap miniMapHandle)
        {
            miniMapHandle.SetUI(this);

            this.miniMapHandle = miniMapHandle;
            this.miniMapHandle.go.transform.SetParent(this.mapMask.transform, false);
        }

        public void SetMapPosition(game.resource.map.Position position)
        {
            mapPos.text = $"{position.top}:{position.left}";

            float xx = (position.left / 16f) + this.miniMapHandle.xRatio;
            float yy = this.miniMapHandle.yRatio - (position.top / 16f);

            this.miniMapHandle.compRect.anchoredPosition = new UnityEngine.Vector2(-xx, -yy);
        }

        public void SetMapName(string name) => this.mapName.text = name;
    }
}
