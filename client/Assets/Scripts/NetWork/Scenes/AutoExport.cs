using game.resource.mapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AutoExport : MonoBehaviour
{
    ushort framesIndex;
    int frameLength;
    int _series = 0, _gender = 0, _type = 0;
    string PathCharacter;
    // Start is called before the first frame update
    void Start()
    {
        PathCharacter = CreatePlayer.CharacterSeries.GetPath(_series, _gender, _type);
        frameLength = Game.Resource(PathCharacter).Get<game.resource.SPR.FrameCount>();
        framesIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (framesIndex < frameLength)
        {
            game.resource.SPR.FrameInfo frameInfo = Game.Resource(PathCharacter).Get<game.resource.SPR.FrameInfo>(framesIndex);
            UnityEngine.Texture2D sprite = Game.Resource(PathCharacter).Get<UnityEngine.Texture2D>(frameInfo);

            SaveTexture2DToFile(sprite,$"d:\\VLCM\\{_series}_{_gender}_{_type}_{framesIndex}", SaveTextureFileFormat.PNG);
            ++framesIndex;
        }
        else
        {
            if (_type < 1)
                _type = 1;
            else
            {
                _type = 0;
                if (_gender < 1)
                    _gender = 1;
                else
                {
                    _gender = 0;
                    if (_series < 4)
                    {
                        ++_series;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            PathCharacter = CreatePlayer.CharacterSeries.GetPath(_series, _gender, _type);
            frameLength = Game.Resource(PathCharacter).Get<game.resource.SPR.FrameCount>();
            framesIndex = 0;
        }
    }

    enum SaveTextureFileFormat
    {
        EXR, JPG, PNG, TGA
    };
    void SaveTexture2DToFile(Texture2D tex, string filePath, SaveTextureFileFormat fileFormat, int jpgQuality = 95)
    {
        switch (fileFormat)
        {
            case SaveTextureFileFormat.EXR:
                System.IO.File.WriteAllBytes(filePath + ".exr", tex.EncodeToEXR());
                break;
            case SaveTextureFileFormat.JPG:
                System.IO.File.WriteAllBytes(filePath + ".jpg", tex.EncodeToJPG(jpgQuality));
                break;
            case SaveTextureFileFormat.PNG:
                System.IO.File.WriteAllBytes(filePath + ".png", tex.EncodeToPNG());
                break;
            case SaveTextureFileFormat.TGA:
                System.IO.File.WriteAllBytes(filePath + ".tga", tex.EncodeToTGA());
                break;
        }
    }
}
