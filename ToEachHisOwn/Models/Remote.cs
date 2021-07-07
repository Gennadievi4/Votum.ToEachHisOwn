using RLib;

namespace ToEachHisOwn.Models
{
    /// <summary>Пульт/// </summary>
    internal class Remote
    {
        internal int Id { get; set; }

        /// <summary>Номер комплекта/// </summary>
        internal int ReceiverId { get; set; }

        /// <summary>Уровень заряда батареи/// </summary>
        internal int Battary { get; set; }

        /// <summary>Уровень сигнала/// </summary>
        internal int RSSI { get; set; }

        /// <summary>Доступность использования команды/// </summary>
        internal bool IsSendbackCommandAvailable { get; set; }

        /// <summary>Исполняется ли команда пультом/// </summary>
        internal bool IsSendbackCommandPresent { get; set; }

        /// <summary>Фамилия ученика/// </summary>
        internal SendbackCommand SendbackCommand { get; set; }

        /// <summary>Используемая кнопка пультом/// </summary>
        internal Button Button { get; set; }
    }
}
