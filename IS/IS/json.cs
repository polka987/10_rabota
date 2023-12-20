using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS
{
    public class json
    {
        public static void Serialize<T>(string path, T obj) => File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{path}", JsonConvert.SerializeObject(obj));

        public static T Deserialize<T>(string path)
        {
            if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{path}"))
                File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{path}","");
            return JsonConvert.DeserializeObject<T>(File.ReadAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{path}"));
        }
    }
}
