using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;

namespace GoogleBooksClient
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

        private async void LoadFromGoogleAPI(object sender, RoutedEventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={suchTb.Text}&maxResults=40";

            var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            BooksResult booksResult = System.Text.Json.JsonSerializer.Deserialize<BooksResult>(json);

            myGrid.ItemsSource = booksResult.items.Select(x => x.volumeInfo).ToList();

        }

        private void SaveAsJson(object sender, RoutedEventArgs e)
        {
            if (myGrid.Items.Count == 0)
            {
                MessageBox.Show("Nix da zu speichern");
                return;
            }

            if (myGrid.ItemsSource is IEnumerable<Volumeinfo> items)
            {
                var sett = new JsonSerializerOptions() { WriteIndented = true };
                var json = JsonSerializer.Serialize(items, sett);
                using var sw = new StreamWriter("books.json");
                sw.Write(json);
            }

        }

        private void LoadFromJSONFile(object sender, RoutedEventArgs e)
        {
            var fileName = "books.json";

            using var sr = new StreamReader(fileName);
            var json = sr.ReadToEnd();
            myGrid.ItemsSource = JsonSerializer.Deserialize<IEnumerable<Volumeinfo>>(json);

        }

        private void SumAllPages(object sender, RoutedEventArgs e)
        {
            if (myGrid.ItemsSource is IEnumerable<Volumeinfo> items)
            {
                var pageSum = items.Sum(x => x.pageCount);

                MessageBox.Show($"Summe aller Pages: {pageSum}");
            }
        }

        private void BuchMit1000SeitenAusDiesmJahr(object sender, RoutedEventArgs e)
        {
            if (myGrid.ItemsSource is IEnumerable<Volumeinfo> items)
            {
                var result = items.FirstOrDefault(x => x.publishedDate != null && x.publishedDate.Contains("2024") && x.pageCount >= 1000);

                if (result != null)
                    MessageBox.Show($"{result.title} {result.pageCount} {result.publishedDate}");
                else
                    MessageBox.Show("Nix");
            }
        }

        private void Filter1000(object sender, RoutedEventArgs e)
        {
            if (myGrid.ItemsSource is IEnumerable<Volumeinfo> items)
            {
                var query = items.Where(x => x.pageCount >= 1000);
                DateTime.Now.GetKW();
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    query = query.Where(x => x.description.Length > 100);

                query = query.OrderByDescending(x => x.publisher).ThenByDescending(x => x.publishedDate); 

                myGrid.ItemsSource = query.ToList();
            }
        }

        private void Filter1000Query(object sender, RoutedEventArgs e)
        {
            if (myGrid.ItemsSource is IEnumerable<Volumeinfo> items)
            {
                var query = from b in items
                            orderby b.publisher descending, b.publishedDate descending
                            orderby b.publisher descending, b.publishedDate descending
                            select b;

                myGrid.ItemsSource = query.ToList();
            }
        }

        private void SelectAnno(object sender, RoutedEventArgs e)
        {
            if (myGrid.ItemsSource is IEnumerable<Volumeinfo> items)
            {
                myGrid.ItemsSource = items.Select(x => new { Title = x.title, Seite = x.pageCount, SeiteMalZwei = x.pageCount * 2 });
            }
        }
    }

}