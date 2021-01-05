using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
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

        #region Эксперименты с потоками №2
        private const string data_dir = "data";
        private static char[] __Separators = { ' ', '.', ',', '!', ';', '-', '(', ')', '[', ']', '{', '}' };
        private static readonly object __LockObject = new object();
        private static int GetWordsCount(FileInfo file)
        {
            if (!file.Exists) throw new FileNotFoundException(" Файл для анализа не найден!", file.FullName);
            if (file.Length == 0) return 0;

            Console.WriteLine($"Обработка файла {file.Name} - thread id:{Thread.CurrentThread.ManagedThreadId}");

            var count = 0;
            using (var reader = file.OpenText())
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var words = line.Split(__Separators, StringSplitOptions.RemoveEmptyEntries);
                    count += words.Length;
                }

            return count;
        }
        private static void PrintConsoleData(string Message, int Count, int Timeout = 10)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            lock (__LockObject)
                Console.WriteLine($"Поток {thread_id} запущен.");

            lock (__LockObject)
                for (int i = 0; i < Count; i++)
                {
                    Console.Write($"{i} ");
                    Console.Write(Message);
                    Console.WriteLine($"-thread id:{thread_id}");
                    Thread.Sleep(Timeout);
                }

            lock (__LockObject)
                Console.WriteLine($"Поток {thread_id} завершён.");
        }
        #endregion

        #region Синхронизация
        private static void ThreadMethod()
        { 
            Console.WriteLine("Вторичный поток стартовал!");

            Console.WriteLine("Вторичный поток завершился!");
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

            #region Эксперименты с потоками №2
            var data_direstory = new DirectoryInfo(data_dir);

            var words_count = 0;

            var timer = new Stopwatch();

            var files = data_direstory.GetFiles();

            var manual_reset_event = new ManualResetEvent(false);
            var auto_reset_event = new AutoResetEvent(false);

            //var thread = new Thread(() => 
            //{
            //    Console.WriteLine("Вторичный поток стартовал!");
            //    Thread.Sleep(500);

            //    Console.WriteLine("Вторичный поток ждёт разрешения на дальнейшую работу...");
            //    manual_reset_event.WaitOne();
            //    Console.WriteLine("Разрешение получено.");

            //    Console.WriteLine("Вторичный поток завершился!");
            //});
            //thread.Start();

            //Console.WriteLine("Главный поток готов возобновить работу вторичного...");
            //Console.ReadLine();
            //manual_reset_event.Set();

            var threads = new Thread[10];

            for (var i = 0; i < threads.Length; i++)
            {
                var index = i;
                var thread = new Thread(() => 
                {
                    Console.WriteLine($"Поток {index} запущен и ждёт разрешения.");
                    Thread.Sleep(500);

                    manual_reset_event.WaitOne();

                    Thread.Sleep(500);
                    Console.WriteLine($"Поток {index} выполнил свою работу.");
                });
                thread.Start();
                threads[i] = thread;
            }

            Console.WriteLine("Главный поток готов разрешить работу...");
            Console.ReadLine();

            manual_reset_event.Set();

            //var mutex = new Mutex(true, "Mutex name");
            //mutex.WaitOne();
            //mutex.ReleaseMutex();

            //var semaphor = new Semaphore(0, 10); //Семафор на 10 входов
            //semaphor.WaitOne();
            //semaphor.Release();
            
            Console.WriteLine("Главный поток завершился!");
            Console.ReadKey();
            #endregion
        }
    }
}
