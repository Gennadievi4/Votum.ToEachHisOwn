using RLib;

namespace ToEachHisOwn.Models
{
    internal class Remote
    {
        internal int Id { get; set; }
        internal int ReceiverId { get; set; }
        internal int Battary { get; set; }
        internal int RSSI { get; set; }
        internal bool IsSendbackCommandAvailable { get; set; }
        internal bool IsSendbackCommandPresent { get; set; }
        internal SendbackCommand SendbackCommand { get; set; }
        internal Button Button { get; set; }
    }
}
