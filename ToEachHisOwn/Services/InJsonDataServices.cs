using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ToEachHisOwn.Properties;
using ToEachHisOwn.Services.Interfaces;

namespace ToEachHisOwn.Services
{
    public class InJsonDataServices : IJsonData
    {
        private IDictionary<string, string[]> _DataDict = new Dictionary<string, string[]>();

        ///<summary>Достаёт внедрённые ресурсы из приложения, сортирует их по ключу.</summary>  
        public IDictionary<string, string[]> GetDbFromExe()
        {
            var res = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);

            foreach (DictionaryEntry item in res)
            {
                if (item.Value is string str)
                {
                    JArray desValue = (JArray)JsonConvert.DeserializeObject(str);
                    _DataDict.Add(item.Key.ToString(), desValue.ToObject<string[]>());
                }
            }

            //var sortedDict = new SortedDictionary<string, string[]>(_DataDict);

            //_DataDict.OrderBy(x => x.Key.Where(x1 => char.IsDigit(x1) && int.Parse(x1.ToString()) > 0)).ToDictionary(x => x.Key, x => x.Value);

            return _DataDict.OrderBy(x => int.TryParse(x.Key.Split(new char[] { '-' }, 2)[0], out int num)
                ? num.ToString().PadLeft(2, '0')
                : x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            //return _DataDict.OrderBy(x => int.TryParse(new string(x.Key.TakeWhile(x1 => char.IsDigit(x1)).ToArray()), out int num)
            //    ? num.ToString().PadLeft(2, '0')
            //    : x.Key)
            //    .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}