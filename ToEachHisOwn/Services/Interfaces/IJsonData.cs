using System.Collections.Generic;

namespace ToEachHisOwn.Services.Interfaces
{
    public interface IJsonData
    {
        ///<summary>Достаёт ведрённые ресурсы из приложения, сортирует их по ключу.</summary>
        IDictionary<string, string[]> GetDbFromExe();

        ///<summary>Метод получает названия статических баз данных, хранящихся в exe приложения.</summary>
        //ICollection<string> GetNameOfDbFromExe();


    }
}
