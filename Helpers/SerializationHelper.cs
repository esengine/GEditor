using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GEditor.Helpers
{
    internal static class SerializationHelper
    {
        // 序列化对象到文件
        public static void SerializeObject<T>(T obj, string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, obj);
            }
        }

        // 从文件反序列化对象
        public static T DeserializeObject<T>(string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
