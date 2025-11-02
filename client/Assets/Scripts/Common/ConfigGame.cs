using System;
using System.IO;
using Photon.ShareLibrary.Constant;
using UnityEngine;


namespace game.config
{

    public static class ConfigGame
    {
        public const string splashScreen = "Splash";
        public const string downloadScreen = "Download";
        public const string characterScreen = "Character";
        public const string worldScreen = "GameWorld";
        public static string tableLanguage = "LanguageTable";
        public static string example = "PlayerExample";

        public const string urlAnimationBundle = "http://s3.fingame.vn/jx1dev/animation-Android";

        public static string getPath()
        {
            String path = "";
            string dataFileName = "JXAnimation";
            path = Path.Combine(Application.persistentDataPath, "AssetData");
            path = Path.Combine(path, dataFileName + ".unity3d");
            return path;
        }


        public static NPCSERIES parserCategoryToAttribute(string category)
        {
            NPCSERIES attributeType = NPCSERIES.series_metal;

            switch (category)
            {
                case "earth":
                    attributeType = NPCSERIES.series_earth;
                    break;
                case "metal":
                    attributeType = NPCSERIES.series_metal;
                    break;
                case "fire":
                    attributeType = NPCSERIES.series_fire;
                    break;
                case "water":
                    attributeType = NPCSERIES.series_water;
                    break;
                case "wood":
                    attributeType = NPCSERIES.series_wood;
                    break;

            }
            return attributeType;
        }

        public static string parserAttributeTypeName(NPCSERIES attributeType)
        {
            string name = "metal";

            switch (attributeType)
            {
                case NPCSERIES.series_earth:
                    name = "earth";
                    break;
                case NPCSERIES.series_metal:
                    name = "metal";
                    break;
                case NPCSERIES.series_wood:
                    name = "wood";
                    break;
                case NPCSERIES.series_fire:
                    name = "fire";
                    break;
                case NPCSERIES.series_water:
                    name = "water";
                    break;

            }
            return name;
        }

        public static int GetRotateByJoyStick(int vertical, int horizontal)
        {
            if (vertical == 0 && horizontal == 1)
            {
                return 7;
            }
            if (vertical == 0 && horizontal == -1)
            {
                return 3;
            }
            if (vertical == 1 && horizontal == 1)
            {
                return 6;
            }
            if (vertical == 1 && horizontal == -1)
            {
                return 4;
            }
            if (vertical == -1 && horizontal == 1)
            {
                return 8;
            }
            if (vertical == -1 && horizontal == -1)
            {
                return 2;
            }
            if (vertical == 1 && horizontal == 0)
            {
                return 5;
            }
            if (vertical == -1 && horizontal == 0)
            {
                return 1;
            }
            return 0;
        }


        public static int GetCurrentDirection(int CurrentDirection)
        {
            switch (CurrentDirection)
            {
                case 1:
                    return 5;
                case 2:
                    return 6;
                case 3:
                    return 7;
                case 4:
                    return 8;
                case 5:
                    return 1;
                case 6:
                    return 2;
                case 7:
                    return 3;
                case 8:
                    return 4;
                default: return 0;
            }
        }


        public static bool Between(float A, float B, float Angle)
        {
            if (Angle < B && Angle > A)
            {
                return true;
            }
            return false;
        }
    }


    /// <summary>
    ///  Resources Manager
    /// </summary>
    public static class ResourcesManager
    {
        public static string genderManButtonSelected = "Login/Button-Nam-2-500x500";
        public static string genderManButtonDisabled = "Login/Button-Nam-1-500x500";
        public static string genderGirlButtonSelected = "Login/Button-Nu-2-500x500";
        public static string genderGirlButtonDisabled = "Login/Button-Nu-1-500x500";
    }
}