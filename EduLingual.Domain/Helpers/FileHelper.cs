using Newtonsoft.Json;
using System.IO;

namespace EduLingual.Api.Utils
{
    public static class FileHelper
    {
        public static dynamic LoadJson<T>(string path, string f)
        {
            using (StreamReader r = new StreamReader(path + f))
            {
                string json = r.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(json)!;
                return result;
            }
        }

        public static dynamic LoadFileToString(string path, string f)
        {
            using (StreamReader r = new StreamReader(path + f))
            {
                string contents = r.ReadToEnd();
                return contents;
            }
        }
    }
}
