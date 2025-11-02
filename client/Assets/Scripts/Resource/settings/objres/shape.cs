using static game.resource.settings.npcres.Shape;

namespace game.resource.settings.objres
{
    public class Shape
    {
        public class Appearance
        {
            public readonly UnityEngine.GameObject parent;
            public readonly UnityEngine.Transform transform;
            public readonly UnityEngine.Rendering.SortingGroup sortingGroup;

            public Appearance(string _objectName)
            {
                this.parent = new UnityEngine.GameObject(_objectName);
                this.transform = parent.transform;
                this.sortingGroup = this.parent.AddComponent<UnityEngine.Rendering.SortingGroup>();
            }

            public static implicit operator UnityEngine.GameObject(Shape.Appearance _appearance)
            {
                return _appearance.parent;
            }
        }

        private readonly Shape.Appearance appearance;
        public Shape(string _objectName)
        {
            this.appearance = new Shape.Appearance(_objectName);
        }
        public Shape.Appearance GetAppearance() => this.appearance;

        npcres.Shape.PartFields newPartField = null;
        public npcres.Shape.PartFields GetPartFields()
        {
            if (newPartField == null)
            {
                newPartField = new PartFields();

                newPartField.gameObject = new UnityEngine.GameObject("shape");
                newPartField.rectTransform = newPartField.gameObject.AddComponent<UnityEngine.RectTransform>();
                newPartField.spriteRenderer = newPartField.gameObject.AddComponent<UnityEngine.SpriteRenderer>();
                newPartField.isValid = false;

                newPartField.gameObject.transform.SetParent(this.appearance.transform, false);
                newPartField.rectTransform.sizeDelta = new UnityEngine.Vector2(0, 0);
            }

            if (newPartField.isValid)
            {
                return newPartField;
            }

            newPartField.gameObject.SetActive(true);
            //updatePartData.sprInfo = npcres.Cache.GetSprInfo(_partAnimation.sprPath);
            newPartField.isValid = true;

            return newPartField;
        }
        byte direction = 0xff;
        npcres.Shape.PartFrame updatePartFrame = null;
        public npcres.Shape.PartFrame GetPartFrame(byte dir, string _partAnimation)
        {
            if (updatePartFrame == null)
            {
                updatePartFrame = new PartFrame();
            }

            if ( direction != dir)
            {
                direction = dir;
                settings.skill.texture.SprCache.Data.SprFrame sprFrame = npcres.Cache.GetSprFrame(_partAnimation, dir);

                updatePartFrame.sprite = sprFrame.sprite;
                updatePartFrame.sizeDelta = sprFrame.sizeDelta;
                updatePartFrame.anchoredPosition = sprFrame.anchoredPosition;
                updatePartFrame.isValid = true;
            }

            return updatePartFrame;
        }
        public void SetDirection(byte dir) => direction = dir;
    }
}