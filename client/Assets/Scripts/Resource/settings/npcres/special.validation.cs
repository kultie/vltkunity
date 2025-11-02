
namespace game.resource.settings.npcres.special
{
    class Validation
    {
        public static bool IsMainMan(string _characterName)
        {
            return _characterName.CompareTo(mapping.settings.NpcRes.Kind.CharacterName.mainMan) == 0;
        }

        public static bool IsMainLady(string _characterName)
        {
            return _characterName.CompareTo(mapping.settings.NpcRes.Kind.CharacterName.mainLady) == 0;
        }
    }
}
