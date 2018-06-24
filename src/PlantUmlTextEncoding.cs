using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantUml.Net
{
    /// <summary>
    /// Provides methods for encoding PlantUML text, as described at http://plantuml.com/text-encoding
    /// </summary>
    internal static class PlantUmlTextEncoding
    {
        public static string EncodeUrl(string text)
        {
            return Encode64(Deflate(Encoding.UTF8.GetString(Encoding.Default.GetBytes(text))));
        }

        private static byte[] Deflate(string text)
        {
            using (var ms = new MemoryStream())
            {
                using (var deflate = new DeflateStream(ms, CompressionMode.Compress))
                {
                    using (var writer = new StreamWriter(deflate, Encoding.UTF8))
                    {
                        writer.Write(text);
                    }
                }

                return ms.ToArray();
            }
        }

        private static string Encode64(byte[] data)
        {
            var r = new StringBuilder();

            for (var i = 0; i < data.Length; i += 3)
            {
                if (i + 2 == data.Length)
                {
                    r.Append(Append3Bytes(data[i], data[i + 1], 0));
                }
                else if (i + 1 == data.Length)
                {
                    r.Append(Append3Bytes(data[i], 0, 0));
                }
                else
                {
                    r.Append(Append3Bytes(data[i], data[i + 1], data[i + 2]));
                }
            }

            return r.ToString();
        }

        private static string Append3Bytes(byte b1, byte b2, int b3)
        {
            var c1 = b1 >> 2;
            var c2 = ((b1 & 0x3) << 4) | (b2 >> 4);
            var c3 = ((b2 & 0xF) << 2) | (b3 >> 6);
            var c4 = b3 & 0x3F;
            var r = new StringBuilder();
            r.Append(Encode6bit(c1 & 0x3F));
            r.Append(Encode6bit(c2 & 0x3F));
            r.Append(Encode6bit(c3 & 0x3F));
            r.Append(Encode6bit(c4 & 0x3F));
            return r.ToString();

        }

        private static char Encode6bit(int b)
        {
            if (b < 10)
            {
                return Convert.ToChar(48 + b);
            }
            b -= 10;
            if (b < 26)
            {
                return Convert.ToChar(65 + b);
            }
            b -= 26;
            if (b < 26)
            {
                return Convert.ToChar(97 + b);
            }
            b -= 26;
            if (b == 0)
            {
                return '-';
            }
            if (b == 1)
            {
                return '_';
            }
            return '?';
        }
    }
}
