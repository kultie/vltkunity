
namespace game.resource.settings.npcres
{
    public class Cache
    {
        public static resource.SPR.Info GetSprInfo(string sprPath)
        {
            return settings.skill.texture.SprCache.GetSprInfo(resource.Cache.Settings.NpcRes.textures, sprPath);
        }

        public static settings.skill.texture.SprCache.Data.SprFrame GetSprFrame(string sprPath, ushort frameIndex)
        {
            return settings.skill.texture.SprCache.GetSprFrame(resource.Cache.Settings.NpcRes.textures, sprPath, frameIndex);
        }
    }
}
