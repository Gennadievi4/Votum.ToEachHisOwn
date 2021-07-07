using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestWPFWithoutMD
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private CancellationTokenSource _OperationCancellation;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.IsEnabled = false;

            _OperationCancellation?.Cancel();
            var cancellation = new CancellationTokenSource();
            _OperationCancellation = cancellation;

            //DoProcess();
            //DoProcessInThread();
            IProgress<double> progress = new Progress<double>(p => ProgressB.Value = p);

            var cancel = cancellation.Token;

            try
            {
                var result = await Task.Run(() => DoWorkAsync(100, 100, progress, cancel), cancel);
                ResultViewer.Text = result;
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Операция вычисления отменена!");
                progress.Report(0);
            }
            button.IsEnabled = true;
        }

        private void DoProcessInThread()
        {
            var thread = new Thread(DoProcess)
            {
                IsBackground = true
            };

            thread.Start();
        }

        private void ProgressInformator(double Progress)
        {
            //ProgressB.Value = Progress;
            if (ProgressB.Dispatcher.CheckAccess())
                ProgressB.Value = Progress;
            else
                Dispatcher.Invoke(() => { ProgressB.Value = Progress; });
        }

        private void DoProcess()
        {
            var result = DoWork(100, 100, ProgressInformator);
            //ResultViewer.Text = result;
            if (ResultViewer.Dispatcher.CheckAccess())
            {
                ResultViewer.Text = result;
            }
            else
                ResultViewer.Dispatcher.Invoke(() =>
                {
                    ResultViewer.Text = result;
                });
        }

        private static string DoWork(int IterationCount, int Timeout, Action<double> ProgressInfo = null)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < IterationCount; i++)
            {
                Debug.WriteLine($"Итерации {i} - поток {thread_id}");
                ProgressInfo?.Invoke((double)i / IterationCount);
                Thread.Sleep(Timeout);
            }

            ProgressInfo?.Invoke(1);
            return "Result" + DateTime.Now.ToString("hh:mm:ss");
        }

        private static async Task<string> DoWorkAsync(int IterationCount, int Timeout, IProgress<double> Progress, CancellationToken Cancel = default, Action<double> ProgressInfo = null)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < IterationCount; i++)
            {
                Cancel.ThrowIfCancellationRequested();
                Debug.WriteLine($"Итерации {i} - поток {thread_id}");
                Progress?.Report((double)i / IterationCount);
                await Task.Delay(Timeout, Cancel);
            }

            return "Result" + DateTime.Now.ToString("hh:mm:ss");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _OperationCancellation?.Cancel();
        }
    }
}
