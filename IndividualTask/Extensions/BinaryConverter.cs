using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace IndividualTask.Extensions
{
    public static class BinaryConverter
    {
        public static byte[] ObjectToByteArray<T>(this T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T ByteArrayToObject<T>(this byte[] arrBytes)
        {
            using (var memStream = new MemoryStream(arrBytes))
            {
                var binForm = new BinaryFormatter();
                var obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static string BytesToString(this byte[] bytes, bool upperCase)
        {
            var result = new StringBuilder(bytes.Length * 2);

            foreach (var b in bytes)
                result.Append(b.ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }
    }
}
