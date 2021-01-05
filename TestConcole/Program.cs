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

        #region Эксперименты с потоками №1

        //private static bool __CanClockWork = true;

        //private static void StartConsoleHeaderClock()
        //{
        //    var thread = Thread.CurrentThread;
        //    Console.WriteLine($"Запущен поток: id:{thread.ManagedThreadId}, name:{thread.Name}, priority:{thread.Priority}");

        //    while (__CanClockWork)
        //    {
        //        Console.Title = DateTime.Now.ToString("G");
        //        Thread.Sleep(100);
        //    }

        //    Console.WriteLine("Поток часов завершил свою работу!");
        //}
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

            #region Эксперименты с потоками №1

            //var clock_thread = new Thread(StartConsoleHeaderClock);
            //clock_thread.IsBackground = true;
            //clock_thread.Priority = ThreadPriority.AboveNormal;
            //clock_thread.Name = "Поток часов";

            //clock_thread.Start();
            //Console.WriteLine($"Идентификатор потока часов: {clock_thread.ManagedThreadId}");

            //Console.ReadLine();
            //__CanClockWork = false;
            //Синхронизация потоков
            //if (!clock_thread.Join(105))
            //    clock_thread.Interrupt();

            ////StartConsoleHeaderClock();

            //Console.WriteLine("Главный поток завершён!");
            //Console.ReadLine();
            //Console.WriteLine("Главный поток выгружен");
            #endregion

            #region Эксперименты с потоками №2
            //var data_direstory = new DirectoryInfo(data_dir);

            //var words_count = 0;
            //foreach (var file in data_direstory.GetFiles())
            //{
            //    Console.WriteLine($"{file.Name} - {file.Length / 1024.0}");
            //    words_count += GetWordsCount(file);
            //}

            //Console.WriteLine($"Общее количество слов в романе \"Война и мир\" составляет {words_count} последовательно.");

            //var files = data_direstory.GetFiles();

            //var threads = new Thread[files.Length];
            //words_count = 0;
            //for (int i = 0; i < threads.Length; i++)
            //{
            //    var files_to_process = files[i];
            //    threads[i] = new Thread(
            //        () =>
            //        {
            //            var count = GetWordsCount(files_to_process);
            //            lock (threads)
            //                words_count += count;
            //        });
            //    threads[i].Start();
            //}

            //for (int i = 0; i < threads.Length; i++)
            //    threads[i].Join();

            //Console.WriteLine($"Число слов {words_count} параллельно.");

            //var print_threads = new Thread[10];
            //for (int i = 0; i < print_threads.Length; i++)
            //{
            //    var message = $"Thread {i + 1} message";
            //    var thread = new Thread(() => PrintConsoleData(message, 10, 0));
            //    thread.Start();
            //    print_threads[i] = thread;
            //}

            //var words_count = data_direstory.GetFiles("*.txt").Sum(GetWordsCount);
            //Console.WriteLine($"Число слов {words_count} linq.");

            //var words_count = data_direstory.GetFiles("*.txt")
            //    .AsParallel()
            //    .Sum(GetWordsCount);
            //Console.WriteLine($"Число слов {words_count} linq parallel.");

            //Parallel.ForEach(files, file =>
            //{
            //    var count = GetWordsCount(file);
            //    lock (files)
            //        words_count += count;
            //});
            //Console.WriteLine($"Число слов {words_count} параллельно.");

            //Parallel.For(0, files.Length, i =>
            //{
            //    var file = files[i];
            //    var count = GetWordsCount(file);
            //    lock (files)
            //        words_count += count;
            //});
            //Console.WriteLine($"Число слов {words_count} параллельно.");

            Console.ReadKey();
            #endregion
        }
    }
}
