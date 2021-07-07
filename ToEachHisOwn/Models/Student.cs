namespace ToEachHisOwn.Models
{
    /// <summary>Ученик/// </summary>
    internal class Student
    {
        internal int Id { get; set; }

        /// <summary>Имя ученика/// </summary>
        internal string Name { get; set; }

        /// <summary>Фамилия ученика/// </summary>
        internal string Surname { get; set; }

        /// <summary>Отчество ученика/// </summary>
        internal string Patronymic { get; set; }

        /// <summary>Пульт ученика/// </summary>
        internal Remote Remote { get; set; }
    }
}
