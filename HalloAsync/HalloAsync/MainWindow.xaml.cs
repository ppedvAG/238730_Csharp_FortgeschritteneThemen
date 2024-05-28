using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhne(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(200);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;

            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => { pb1.Value = i; Thread.Sleep(100); });
                    Thread.Sleep(200);
                }

                this.Dispatcher.Invoke(() => { ((Button)sender).IsEnabled = false; });
            });
        }

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            cts = new CancellationTokenSource();
            var ts = TaskScheduler.FromCurrentSynchronizationContext();
            var t1 = Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(20);
                    Task.Factory.StartNew(() => pb1.Value = i, CancellationToken.None, TaskCreationOptions.None, ts);

                    if (cts.IsCancellationRequested)
                    {
                        //clean up
                        //break;
                        cts.Token.ThrowIfCancellationRequested();
                    }

                    if (i == 77)
                        throw new ExecutionEngineException();
                }
            }, cts.Token);
            t1.ContinueWith(t => ((Button)sender).IsEnabled = true, ts);
            t1.ContinueWith(t => MessageBox.Show("OK"), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, ts);
            t1.ContinueWith(t => MessageBox.Show("Canceled"), CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, ts);
            t1.ContinueWith(t => MessageBox.Show($"ERROR: {t.Exception.InnerException.Message}"), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, ts);

        }

        CancellationTokenSource cts = null!;

        private void Abort(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        [Experimental("Pass")]
        private async void StartAA(object sender, RoutedEventArgs e)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(sender));

            ((Button)sender).IsEnabled = false;
            cts = new CancellationTokenSource();
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Value = i;
                    await Task.Delay(200, cts.Token);
                    var lines = File.ReadAllLines("test.txt");
                    //var lines = await File.ReadAllLinesAsync("test.txt");
                }
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show($"Abbruch");
            }
            catch(IOException ex) when (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine(ex.ToString()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Schade: {ex.Message}");
            }
            ((Button)sender).IsEnabled = true;

        }


        public long AlteFunction(int wert)
        {
            Thread.Sleep(5000);
            return wert * 2;
        }

        public Task<long> AlteFunctionAsync(int wert, CancellationToken token = default)
        {
            return Task.Run(() => AlteFunction(wert));
        }

        private async void StartAlteFunction(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Alte Function: {AlteFunction(12)}");
            MessageBox.Show($"Alte Function: {await AlteFunctionAsync(12)}");
        }
    }
}