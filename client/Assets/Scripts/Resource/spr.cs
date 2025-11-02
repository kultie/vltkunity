
namespace game.resource
{
    public class SPR
    {
        public const ushort firstFrame = 0;
        public const ushort FPS = 18;

        public class Info
        {
            public ushort width = 0;
            public ushort height = 0;
            public ushort centerX = 0;
            public ushort centerY = 0;
            public ushort frameCount = 0;
            public ushort colorCount = 0;
            public ushort directionCount = 0;
            public ushort interval = 0;
        }

        public class FrameInfo
        {
            public ushort frameIndex = 0;
            public ushort width = 0;
            public ushort height = 0;
            public ushort offsetX = 0;
            public ushort offsetY = 0;
        }

        public class FrameCount
        {
            private ushort value = 0;
            public static implicit operator ushort(FrameCount _sprFrameCount) => _sprFrameCount.value;
            public static implicit operator FrameCount(ushort _value) => new() { value = _value };
        }

        public class TextureBuffer
        {
            private byte[] buffer;

            public TextureBuffer() { }

            public TextureBuffer(int _length)
            {
                if (_length <= 0)
                {
                    return;
                }

                this.buffer = new byte[_length];
            }

            public static implicit operator byte[](TextureBuffer _textureBuffer)
            {
                return _textureBuffer.buffer;
            }

            public static implicit operator TextureBuffer(byte[] _buffer)
            {
                return new() { buffer = _buffer };
            }
        }
    }
}
