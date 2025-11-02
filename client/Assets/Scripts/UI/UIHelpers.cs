using UnityEngine;


namespace game.ui
{
    public class UIHelpers
    {
        public static GameObject BringPrefabToScene(string prefab)
        {
            return GameObject.Instantiate(RessourcesLoadPrefab(prefab));
        }
        public static GameObject RessourcesLoadPrefab(string prefab)
        {
            return Resources.Load("Prefabs/" + prefab) as GameObject;
        }
        public static GameObject BringPrefabToScene(string prefab, float x, float y)
        {
            var obj = BringPrefabToScene(prefab);
            obj.transform.position = new Vector3(x, y, obj.transform.position.z);
            return obj;
        }
        public static MessageBox BringMessageBox()
        {
            return BringPrefabToScene("MessageBox").GetComponent<MessageBox>();
        }
    }
}
