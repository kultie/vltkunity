
namespace game.resource.mapping.settings
{
    public struct Missile
    {
        public const string filePath = "\\settings\\missles.txt";

        public enum HeaderIndexer
        {
            MissleId = 0,
            MissleName,
            MoveKind,
            FollowKind,
            ColFollowTarget,
            MissleHeight,
            CollidRange,
            IsRangeDmg,
            DmgRange,
            DmgInterval,
            LifeTime,
            Speed,
            Zspeed,
            Zacc,
            LoopPlay,
            SubLoop,
            SubStart,
            SubStop,
            ResponseSkill,
            CanDestroy,
            ColVanish,
            CanSlow,
            CanColFriend,
            AutoExplode,
            MissRate,
            Param1,
            Param2,
            Param3,
            MultiShow,
            AnimFile1,
            AnimFileInfo1,
            SndFile1,
            AnimFile2,
            AnimFileInfo2,
            SndFile2,
            AnimFile3,
            AnimFileInfo3,
            SndFile3,
            AnimFile4,
            AnimFileInfo4,
            SndFile4,
            AnimFileB1,
            AnimFileInfoB1,
            SndFileB1,
            AnimFileB2,
            AnimFileInfoB2,
            SndFileB2,
            AnimFileB3,
            AnimFileInfoB3,
            SndFileB3,
            AnimFileB4,
            AnimFileInfoB4,
            SndFileB4,
            RedLum,
            GreenLum,
            BlueLum,
            LightRadius
        }
    }
}
