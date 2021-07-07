using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ToEachHisOwn.Properties;
using ToEachHisOwn.Services.Interfaces;

namespace ToEachHisOwn.Services
{
    public class InJsonDataServices : IJsonDataServices
    {
        private IDictionary<string, string[]> _DataDict = new Dictionary<string, string[]>();
        private int _NewNumberKey = 0;

        /// <summary>Получает повторное имя базы</summary>
        public string MatchOfNameBase { get; private set; }

        ///<summary>Достаёт внедрённые ресурсы из приложения, сортирует их по ключу.</summary>  
        public IDictionary<string, string[]> GetDbFromExe()
        {
            var res = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry item in res)
            {
                if (item.Value is string str)
                {
                    var desValue = JsonConvert.DeserializeObject<string[]>(str);
                    _DataDict.Add(item.Key.ToString(), desValue);
                }
            }

            //var sortedDict = new SortedDictionary<string, string[]>(_DataDict);

            //_DataDict.OrderBy(x => x.Key.Where(x1 => char.IsDigit(x1) && int.Parse(x1.ToString()) > 0)).ToDictionary(x => x.Key, x => x.Value);

            return SortDic();

            //return _DataDict.OrderBy(x => int.TryParse(new string(x.Key.TakeWhile(x1 => char.IsDigit(x1)).ToArray()), out int num)
            //    ? num.ToString().PadLeft(2, '0')
            //    : x.Key)
            //    .ToDictionary(x => x.Key, x => x.Value);
        }

        ///<summary>Открывает указанный файл десериализуя его в json</summary>
        public IDictionary<string, string[]> Open(string FileName, string FileNameWithoutExstension)
        {
            var DesSer = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(FileName));

            if (_DataDict.ContainsKey(FileNameWithoutExstension))
            {
                var keys = new List<string>(_DataDict.Keys);

                keys.Add(FileNameWithoutExstension);

                _NewNumberKey = keys
                    .GroupBy(x => x)
                    .Where(x => x.Key.Contains(FileNameWithoutExstension))
                    .Count();

                _DataDict.Add($"{FileNameWithoutExstension}({_NewNumberKey})", DesSer);
                MatchOfNameBase = $"{FileNameWithoutExstension}({_NewNumberKey})";
                return SortDic();
            }

            _DataDict.Add(FileNameWithoutExstension, DesSer);
            MatchOfNameBase = $"{FileNameWithoutExstension}";
            return SortDic();
        }

        /// <summary>Удаляет базу из приложения по её ключу.</summary>
        /// <param name="key">Ключ базы в словаре баз</param>
        /// <returns>134</returns>
        public IDictionary<string, string[]> Delete(string key)
        {
            _DataDict.Remove(key);

            return SortDic();
        }

        private IDictionary<string, string[]> SortDic() => _DataDict
            .OrderBy(x => int.TryParse(x.Key.Split(new char[] { '-' }, 2)[0], out int num)
                ? num.ToString().PadLeft(2, '0')
                : x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
    }
}