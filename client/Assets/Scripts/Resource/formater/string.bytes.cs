
using System.Text;

namespace game.resource.formater
{
    class Byte1252
    {
        public static string TCVN3(byte[] data)
        {
            return Encoding.GetEncoding(1252).GetString(data);
        }
    }
}
