using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using TestConcole.Properties;

namespace TestConcole
{
    class Program
    {

        #region Работа с ресурсами
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
        #endregion

        #region Эксперименты с потоками

        private static bool __CanClockWork = true;

        private static void StartConsoleHeaderClock()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine($"Запущен поток: id:{thread.ManagedThreadId}, name:{thread.Name}, priority:{thread.Priority}");

            while (__CanClockWork)
            {
                Console.Title = DateTime.Now.ToString("G");
                Thread.Sleep(100);
            }

            Console.WriteLine("Поток часов завершил свою работу!");
        }
        #endregion

        static void Main(string[] args)
        {
            #region Работа с ресурсами
            //DeserializeObjectJson(ConvertTxtToJson);

            //var assembly = Assembly.GetExecutingAssembly();

            //var res = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            //foreach (DictionaryEntry item in res)
            //{
            //    if (item.Value is string str)
            //        Console.WriteLine($"{item.Key}:{JsonConvert.DeserializeObject(str)}");
            //    Console.WriteLine($"{item.Key}");
            //}
            #endregion

            #region Эксперименты с потоками

            var clock_thread = new Thread(StartConsoleHeaderClock);
            clock_thread.IsBackground = true;
            clock_thread.Priority = ThreadPriority.AboveNormal;
            clock_thread.Name = "Поток часов";

            clock_thread.Start();
            Console.WriteLine($"Идентификатор потока часов: {clock_thread.ManagedThreadId}");

            Console.ReadLine();
            __CanClockWork = false;
            //Синхронизация потоков
            if (!clock_thread.Join(105))
                clock_thread.Interrupt();

            //StartConsoleHeaderClock();

            Console.WriteLine("Главный поток завершён!");
            Console.ReadLine();
            Console.WriteLine("Главный поток выгружен");
            #endregion


            Console.ReadKey();
        }
    }
}
