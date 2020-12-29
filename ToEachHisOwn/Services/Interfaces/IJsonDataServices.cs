using System.Collections.Generic;

namespace ToEachHisOwn.Services.Interfaces
{
    public interface IJsonDataServices
    {
        ///<summary>Достаёт ведрённые ресурсы из приложения, сортирует их по ключу.</summary>
        IDictionary<string, string[]> GetDbFromExe();

        ///<summary>Метод получает названия статических баз данных, хранящихся в exe приложения.</summary>
        //ICollection<string> GetNameOfDbFromExe();

        ///<summary>Открывает указанный файл десериализуя его в json</summary>
        IDictionary<string, string[]> Open(string filename, string filenameWithoutExstension);

        ///<summary>Удаляет выбранную базу из списка.</summary>
        IDictionary<string, string[]> Delete(string key);
    }
}
