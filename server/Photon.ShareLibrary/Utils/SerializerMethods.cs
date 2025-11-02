using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public static class SerializerMethods
    {
        /// <summary>
        /// <see cref="UShort"/> serialization
        /// </summary>
        public static byte[] SerializeUShort(object obj)
        {
            byte[] bytes = new byte[2];

            var strBytes = BitConverter.GetBytes((ushort)obj);
            Buffer.BlockCopy(strBytes, 0, bytes, 0, strBytes.Length);

            return bytes;
        }

        /// <summary>
        /// <see cref="UInt"/> deserialization
        /// </summary>
        public static object DeserializeUShort(byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// <see cref="UInt"/> serialization
        /// </summary>
        public static byte[] SerializeUInt(object obj)
        {
            byte[] bytes = new byte[4];

            var strBytes = BitConverter.GetBytes((uint)obj);
            Buffer.BlockCopy(strBytes, 0, bytes, 0, strBytes.Length);

            return bytes;
        }

        /// <summary>
        /// <see cref="UInt"/> deserialization
        /// </summary>
        public static object DeserializeUInt(byte[] bytes)
        {
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// <see cref="ULong"/> serialization
        /// </summary>
        public static byte[] SerializeULong(object obj)
        {
            byte[] bytes = new byte[8];

            var strBytes = BitConverter.GetBytes((ulong)obj);
            Buffer.BlockCopy(strBytes, 0, bytes, 0, strBytes.Length);

            return bytes;
        }

        /// <summary>
        /// <see cref="UInt"/> deserialization
        /// </summary>
        public static object DeserializeULong(byte[] bytes)
        {
            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
