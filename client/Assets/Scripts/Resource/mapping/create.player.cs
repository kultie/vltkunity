
namespace game.resource.mapping
{
    struct CreatePlayer
    {
        public struct CharacterSeries
        {
            public const string special = "\\maps\\特殊用地\\明月镇\\special.txt";

            // series: 0, 1, 2, 3, 4
            // gender: 0, 1
            // effect: 2: standby, 1: showoff, 0: active
            public static string GetPath(int _series, int _gender, int _effect)
            {
                string[] series = { "金", "木", "水", "火", "土" };
                string[] gender = { "女", "男" };

                return "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_" + series[_series] + "_" + gender[_gender] + "_" + _effect + ".spr";
            }

            public struct Metal
            {
                public const string manStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_金_男_2.spr";
                public const string manShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_金_男_1.spr";
                public const string manStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_金_男_0.spr";
            }

            public struct Wood
            {
                public const string manStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_木_男_2.spr";
                public const string manShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_木_男_1.spr";
                public const string manStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_木_男_0.spr";

                public const string girlStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_木_女_2.spr";
                public const string girlShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_木_女_1.spr";
                public const string girlStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_木_女_0.spr";
            }

            public struct Water
            {
                public const string girlStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_水_女_2.spr";
                public const string girlShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_水_女_1.spr";
                public const string girlStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_水_女_0.spr";
            }

            public struct Fire
            {
                public const string manStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_火_男_2.spr";
                public const string manShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_火_男_1.spr";
                public const string manStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_火_男_0.spr";

                public const string girlStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_火_女_2.spr";
                public const string girlShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_火_女_1.spr";
                public const string girlStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_火_女_0.spr";
            }

            public struct Earth
            {
                public const string manStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_土_男_2.spr";
                public const string manShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_土_男_1.spr";
                public const string manStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_土_男_0.spr";

                public const string girlStandBy = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_土_女_2.spr";
                public const string girlShowoff = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_土_女_1.spr";
                public const string girlStandByActive = "\\Spr\\Ui3\\登入界面\\选存档人物\\角色_土_女_0.spr";
            }
        }
    }
}
