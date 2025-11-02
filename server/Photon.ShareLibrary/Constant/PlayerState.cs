namespace Photon.ShareLibrary.Constant
{
    public enum PlayerChat : byte
    {
        near = 0,
        team,
        tong,
        menpai,
        city,
        world,

        hiden,
        system,
    };
    public enum PlayerState : byte
    {
        init = 0, 
        load,
        wait,
        play, 
        exit
    };
    public enum PlayerLoad : byte
    {
        info = 0,
        skill,
        item,
        task,
        friend,
    }
    public enum  MoveSpeedMultiply
    {
        Walk = 2,
        Run = 2,
    }
    public enum PlayerBase : byte
    {
        BASE_WALK_SPEED = 5 * MoveSpeedMultiply.Walk,
        BASE_RUN_SPEED = 10 * MoveSpeedMultiply.Run,

        BASE_ATTACK_SPEED = 18,
        BASE_CAST_SPEED = 18,
        BASE_HIT_RECOVER = 6,

        BASE_VISION_RADIUS = 120,

        BASE_FIRE_RESIST_MAX = 150,
        BASE_COLD_RESIST_MAX = 150,
        BASE_POISON_RESIST_MAX = 150,
        BASE_LIGHT_RESIST_MAX = 150,
        BASE_PHYSICS_RESIST_MAX = 150,
    }
}
