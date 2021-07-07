namespace ToEachHisOwn.Services.Interfaces
{
    public interface IDialogServices
    {
        void ShowMessage(string message);
        string FilePath { get; set; }
        string FileNameWithoutExstension { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog();
    }
}
