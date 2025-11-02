
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace game
{
    class Resource
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        private static string ReplaceStringPath(string path)
        {
            return path.Replace('/', '\\');
        }
#else
        private static string ReplaceStringPath(string path)
        {
            return path.Replace('\\', '/');
        }
#endif

        private readonly string path;

        public Resource(string _path)
        {
            this.path = _path;
        }

        public static implicit operator Resource(string _path)
        {
            return new(_path);
        }

        private resource.packageIni.ElementReference GetPackageElement()
        {
            resource.packageIni.ElementReference result = new();
            resource.packageIni.PluginApi.v(
                resource.Cache.resourcePackageHandler,
                this.path,
                ref result.id,
                ref result.packageIndex,
                ref result.index,
                ref result.cacheIndex,
                ref result.offset,
                ref result.size
            );

            return result;
        }

        private resource.Buffer GetBufferData()
        {
            string localStorageFullPath = resource.dataController.Config.GetLocalStogareFullPath() + this.path;

            localStorageFullPath = Resource.ReplaceStringPath(localStorageFullPath);

            if (System.IO.File.Exists(localStorageFullPath))
            {
                return System.IO.File.ReadAllBytes(localStorageFullPath);
            }

            resource.packageIni.ElementReference elementReference = this.GetPackageElement();

            if(elementReference.id <= 0
                || elementReference.size <= 0)
            {
                return new resource.Buffer();
            }

            resource.Buffer bufferResult = new(elementReference.size);
            IntPtr bufferPointer = Marshal.AllocHGlobal(elementReference.size * sizeof(char));

            resource.packageIni.PluginApi.b(
                resource.Cache.resourcePackageHandler,
                elementReference.id,
                elementReference.packageIndex,
                elementReference.index,
                elementReference.cacheIndex,
                elementReference.offset,
                elementReference.size,
                bufferPointer
            );

            Marshal.Copy(bufferPointer, bufferResult, 0, bufferResult.size);
            Marshal.FreeHGlobal(bufferPointer);

            return bufferResult;
        }

        private resource.Table GetTableFile()
        {
            return new resource.Table(this.GetBufferData());
        }

        private resource.Ini GetIniFile()
        {
            return new resource.Ini(this.GetBufferData());
        }

        private resource.SPR.FrameCount GetSprFrameCount()
        {
            return resource.packageIni.PluginApi.n(resource.Cache.resourcePackageHandler, this.path);
        }

        private resource.SPR.Info GetSprInfo()
        {
            resource.SPR.Info result = new();

            resource.packageIni.PluginApi.m(
                resource.Cache.resourcePackageHandler,
                this.path,
                ref result.width,
                ref result.height,
                ref result.centerX,
                ref result.centerY,
                ref result.frameCount,
                ref result.colorCount,
                ref result.directionCount,
                ref result.interval
            );

            return result;
        }

        private resource.SPR.FrameInfo GetSprFrameInfo(ushort _frameIndex)
        {
            resource.SPR.FrameInfo result = new()
            {
                frameIndex = _frameIndex
            };

            resource.packageIni.PluginApi.l(
                resource.Cache.resourcePackageHandler,
                this.path,
                _frameIndex,
                ref result.width,
                ref result.height,
                ref result.offsetX,
                ref result.offsetY
            );

            return result;
        }

        private resource.SPR.TextureBuffer GetSprFrameRawTextureData(resource.SPR.FrameInfo _frameInfo)
        {
            int bufferLength = _frameInfo.width * _frameInfo.height * 4;
            resource.SPR.TextureBuffer bufferData = new(bufferLength);
            IntPtr bufferPointer = Marshal.AllocHGlobal(bufferLength * sizeof(char));

            resource.packageIni.PluginApi.k(
                resource.Cache.resourcePackageHandler,
                this.path,
                _frameInfo.frameIndex,
                bufferPointer
            );

            Marshal.Copy(bufferPointer, bufferData, 0, bufferLength);
            Marshal.FreeHGlobal(bufferPointer);

            return bufferData;
        }

        private resource.SPR.TextureBuffer GetSprFrameRawTextureData(ushort _frameIndex)
        {
            return this.GetSprFrameRawTextureData(this.GetSprFrameInfo(_frameIndex));
        }

        private UnityEngine.Texture2D GetSprFrameTexture2D(resource.SPR.FrameInfo _frameInfo)
        {
            UnityEngine.Texture2D newTexture2D = new(_frameInfo.width, _frameInfo.height, UnityEngine.TextureFormat.RGBA32, false);
            newTexture2D.LoadRawTextureData(this.GetSprFrameRawTextureData(_frameInfo));
            newTexture2D.Apply();

            return newTexture2D;
        }

        private UnityEngine.Texture2D GetSprFrameTexture2D(ushort _frameIndex)
        {
            return this.GetSprFrameTexture2D(this.GetSprFrameInfo(_frameIndex));
        }

        private UnityEngine.Sprite GetSprFrameSprite(resource.SPR.FrameInfo _frameInfo)
        {
            if(_frameInfo.width == 0 || _frameInfo.height == 0)
            {
                return null;
            }

            return UnityEngine.Sprite.Create(
                this.GetSprFrameTexture2D(_frameInfo),
                new UnityEngine.Rect(0, 0, _frameInfo.width, _frameInfo.height),
                new UnityEngine.Vector2(0.5f, 0.5f)
            );
        }

        private UnityEngine.Sprite GetSprFrameSprite(ushort _frameIndex)
        {
            resource.SPR.FrameInfo frameInfo = this.GetSprFrameInfo(_frameIndex);

            if (frameInfo.width == 0 || frameInfo.height == 0)
            {
                return null;
            }


            return this.GetSprFrameSprite(frameInfo);
        }

        private UnityEngine.Sprite GetImageSprite()
        {
            resource.Buffer imageBuffer = this.GetBufferData();

            if (imageBuffer.size <= 0)
            {
                return null;
            }

            UnityEngine.Texture2D imageTexture2D = new UnityEngine.Texture2D(2, 2);
            imageTexture2D.LoadImage(imageBuffer);

            return UnityEngine.Sprite.Create(
                imageTexture2D,
                new UnityEngine.Rect(0, 0, imageTexture2D.width, imageTexture2D.height),
                new UnityEngine.Vector2(0.5f, 0.5f)
            );
        }

        /*  supporting
         *  
         *  resource.packageIni.ElementReference
         *  
         *  game.resource.Buffer
         *  game.resource.Table
         *  game.resource.Ini
         *  
         *  game.resource.SPR.FrameCount
         *  game.resource.SPR.Info
         *  game.resource.SPR.FrameInfo
         *  game.resource.SPR.TextureBuffer
         *  
         *  UnityEngine.Texture2D
         *  UnityEngine.Sprite
         */

        public Typename Get<Typename>()
        {
            Type requestType = typeof(Typename);

            if(requestType == typeof(resource.packageIni.ElementReference)) return (Typename)(object)this.GetPackageElement();
            if(requestType == typeof(resource.Buffer)) return (Typename)(object)this.GetBufferData();
            if(requestType == typeof(resource.Table)) return (Typename)(object)this.GetTableFile();
            if(requestType == typeof(resource.Ini)) return (Typename)(object)this.GetIniFile();
            if(requestType == typeof(resource.SPR.FrameCount)) return (Typename)(object)this.GetSprFrameCount();
            if(requestType == typeof(resource.SPR.Info)) return (Typename)(object)this.GetSprInfo();
            if(requestType == typeof(UnityEngine.Sprite)) return (Typename)(object)this.GetImageSprite();

            throw new Exception("Hiện chưa hỗ trợ định dạng này: " + typeof(Typename).FullName);
        }

        public Typename Get<Typename>(ushort _frameIndex)
        {
            Type requestType = typeof(Typename);

            if (requestType == typeof(resource.SPR.FrameInfo)) return (Typename)(object)this.GetSprFrameInfo(_frameIndex);
            if (requestType == typeof(resource.SPR.TextureBuffer)) return (Typename)(object)this.GetSprFrameRawTextureData(_frameIndex);
            if (requestType == typeof(UnityEngine.Texture2D)) return (Typename)(object)this.GetSprFrameTexture2D(_frameIndex);
            if (requestType == typeof(UnityEngine.Sprite)) return (Typename)(object)this.GetSprFrameSprite(_frameIndex);

            throw new Exception("Hiện chưa hỗ trợ định dạng này: " + typeof(Typename).FullName);
        }

        public Typename Get<Typename>(resource.SPR.FrameInfo _frameInfo)
        {
            Type requestType = typeof(Typename);

            if (requestType == typeof(resource.SPR.TextureBuffer)) return (Typename)(object)this.GetSprFrameRawTextureData(_frameInfo);
            if (requestType == typeof(UnityEngine.Texture2D)) return (Typename)(object)this.GetSprFrameTexture2D(_frameInfo);
            if (requestType == typeof(UnityEngine.Sprite)) return (Typename)(object)this.GetSprFrameSprite(_frameInfo);

            throw new Exception("Hiện chưa hỗ trợ định dạng này: " + typeof(Typename).FullName);
        }
    }
}
