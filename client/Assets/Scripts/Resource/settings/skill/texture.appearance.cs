
namespace game.resource.settings.skill.texture
{
    public class Appearance
    {
        public UnityEngine.GameObject parent;
        public UnityEngine.GameObject textureObject;
        public UnityEngine.SpriteRenderer spriteRendererComponent;
        public UnityEngine.RectTransform rectTransformComponent;

        public void Initialize()
        {
            this.parent = new UnityEngine.GameObject(typeof(settings.skill.Texture).FullName);
            this.textureObject = new UnityEngine.GameObject("texture");
            this.spriteRendererComponent = this.textureObject.AddComponent<UnityEngine.SpriteRenderer>();
            this.rectTransformComponent = this.textureObject.AddComponent<UnityEngine.RectTransform>();

            this.textureObject.transform.SetParent(this.parent.transform, false);
        }

        public void Release()
        {
            UnityEngine.GameObject.Destroy(this.parent);
        }
    }
}
