
namespace game.resource.map
{
    public class Layer
    {
        public readonly UnityEngine.GameObject hiddenTextures;
        public readonly UnityEngine.GameObject groundNode;
        public readonly UnityEngine.GameObject groundObject;
        public readonly UnityEngine.GameObject groundMixture;
        public readonly UnityEngine.GameObject skillMissile;
        public readonly UnityEngine.GameObject buildingAbove;
        public readonly UnityEngine.GameObject identification;

        public Layer(UnityEngine.GameObject _parent)
        {
            this.hiddenTextures = new UnityEngine.GameObject("hidden-textures");
            this.groundNode = new UnityEngine.GameObject("ground-node");
            this.groundObject = new UnityEngine.GameObject("ground-object");
            this.groundMixture = new UnityEngine.GameObject("ground-mixture");
            this.skillMissile = new UnityEngine.GameObject("skill-missile");
            this.buildingAbove = new UnityEngine.GameObject("building-above");
            this.identification = new UnityEngine.GameObject("identification");

            this.hiddenTextures.transform.parent = _parent.transform;
            this.groundNode.transform.parent = _parent.transform;
            this.groundObject.transform.parent = _parent.transform;
            this.groundMixture.transform.parent = _parent.transform;
            this.skillMissile.transform.parent = _parent.transform;
            this.buildingAbove.transform.parent = _parent.transform;
            this.identification.transform.parent = _parent.transform;

            UnityEngine.Rendering.SortingGroup hiddenTexturesSorting = this.hiddenTextures.AddComponent<UnityEngine.Rendering.SortingGroup>();
            UnityEngine.Rendering.SortingGroup groundNodeSorting = this.groundNode.AddComponent<UnityEngine.Rendering.SortingGroup>();
            UnityEngine.Rendering.SortingGroup groundObjectSorting = this.groundObject.AddComponent<UnityEngine.Rendering.SortingGroup>();
            UnityEngine.Rendering.SortingGroup groundMixtureSorting = this.groundMixture.AddComponent<UnityEngine.Rendering.SortingGroup>();
            UnityEngine.Rendering.SortingGroup skillMissileSorting = this.skillMissile.AddComponent<UnityEngine.Rendering.SortingGroup>();
            UnityEngine.Rendering.SortingGroup buildingAboveSorting = this.buildingAbove.AddComponent<UnityEngine.Rendering.SortingGroup>();
            UnityEngine.Rendering.SortingGroup identificationSorting = this.identification.AddComponent<UnityEngine.Rendering.SortingGroup>();

            string sortingGroupName = "game.resource.map.Layer";

            hiddenTexturesSorting.sortingLayerName = sortingGroupName;
            hiddenTexturesSorting.sortingOrder = 1;

            groundNodeSorting.sortingLayerName = sortingGroupName;
            groundNodeSorting.sortingOrder = 2;

            groundObjectSorting.sortingLayerName = sortingGroupName;
            groundObjectSorting.sortingOrder = 3;

            groundMixtureSorting.sortingLayerName = sortingGroupName;
            groundMixtureSorting.sortingOrder = 4;

            skillMissileSorting.sortingLayerName = sortingGroupName;
            skillMissileSorting.sortingOrder = 5;

            buildingAboveSorting.sortingLayerName = sortingGroupName;
            buildingAboveSorting.sortingOrder = 6;

            identificationSorting.sortingLayerName = sortingGroupName;
            identificationSorting.sortingOrder = 7;
        }
    }
}
