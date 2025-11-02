using Photon.ShareLibrary.Constant;
using Photon.ShareLibrary.Utils;
using Photon.SocketServer;

namespace Photon.JXLoginServer
{
    public static class TypeSerializer
    {
        public static void RegisterTypes()
        {
            Protocol.TryRegisterCustomType(typeof(ushort), (byte)CustomTypeCode.UShort,
                                           SerializerMethods.SerializeUShort,
                                           SerializerMethods.DeserializeUShort);

            Protocol.TryRegisterCustomType(typeof(uint), (byte)CustomTypeCode.UInt,
                                           SerializerMethods.SerializeUInt,
                                           SerializerMethods.DeserializeUInt);

            Protocol.TryRegisterCustomType(typeof(ulong), (byte)CustomTypeCode.ULong,
                                           SerializerMethods.SerializeULong,
                                           SerializerMethods.DeserializeULong);
        }
    }
}
