using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace TestConcole
{
    class Program
    {


        public static string[] ConvertTxtToJson(string s)
        {
            var db_directory = Directory
                .GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory))
                .FirstOrDefault(x => x.Contains("Базы1"));

            var db_files = Directory.GetFiles(db_directory);

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
            {
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
                //TypeNameHandling = TypeNameHandling.Auto,
                //ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //Formatting = Formatting.Indented,
                //MissingMemberHandling = MissingMemberHandling.Error,
                //PreserveReferencesHandling = PreserveReferencesHandling.All
            };

            foreach (var item in db_files)
            {
                if (item is string str && File.ReadAllText(str).Contains('['))
                    continue;

                if (db_files.ToList().LastIndexOf(db_files.Last()) == db_files.ToArray().Count() - 1 && !File.ReadAllText(item).Contains('['))
                    File.WriteAllText(db_files.Last(), JsonConvert.SerializeObject(File.ReadAllText(db_files.Last()).Split(new string[] { "\r\n" }, StringSplitOptions.None), jsonSettings));
            }

            return db_files;
        }

        public static void DeserializeObjectJson(Func<string, string[]> func)
        {
            Console.WriteLine(JsonConvert.DeserializeObject(File.ReadAllText(func("хуй").First())));
        }

        static void Main(string[] args)
        {
            DeserializeObjectJson(ConvertTxtToJson);

            Console.WriteLine("That was all!");

            Console.ReadKey();
        }
    }
}
