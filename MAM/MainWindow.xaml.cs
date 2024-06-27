using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MAM
{
    public class AlbumController
    {
        private AlbumModel model;
        private MainWindow view;

        public AlbumController(AlbumModel model, MainWindow view)
        {
            this.model = model;
            this.view = view;
            LoadAlbums();
        }

        public void LoadAlbums()
        {
            view.AlbumList.Items.Clear();
            foreach (var album in model.GetAlbums())
            {
                view.AlbumList.Items.Add($"{album.AlbumName} - {album.Author} - {album.Year}");
            }
        }

        public void AddAlbum(string AlbumName, string Author, int Year, string Track)
        {
            var newAlbum = new Album
            {
                Id = model.GetAlbums().Count + 1,
                AlbumName = AlbumName,
                Author = Author,
                Year = Year,
                Track = Track.Split(',').Select(t => t.Trim()).ToList()
            };
            model.AddAlbum(newAlbum);
            LoadAlbums();
        }

        public void EditAlbum(int id, string AlbumName, string Author, int Year, string Track)
        {
            var album = new Album
            {
                Id = id,
                AlbumName = AlbumName,
                Author = Author,
                Year = Year,
                Track = Track.Split(',').Select(t => t.Trim()).ToList()
            };
            model.EditAlbum(album);
            LoadAlbums();
        }

        public void RemoveAlbum(int id)
        {
            model.RemoveAlbum(id);
            LoadAlbums();
        }
    }
    public partial class MainWindow : Window
    {
        private readonly AlbumModel model;
        private readonly AlbumController controller;

        public MainWindow()
        {
            InitializeComponent();
            model = new AlbumModel();
            controller = new AlbumController(model, this);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            controller.AddAlbum(AlbumNameTextBox.Text, AuthorTextBox.Text, int.Parse(YearTextBox.Text), TrackTextBox.Text);
            ClearForm();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(AlbumList.SelectedItem != null)
            {
                var selectedAlbum = model.GetAlbums().First(a => $"{a.AlbumName} - {a.Author} - {a.Year}" == AlbumList.SelectedItem.ToString());
                controller.EditAlbum(selectedAlbum.Id, AlbumNameTextBox.Text, AuthorTextBox.Text, int.Parse(YearTextBox.Text), TrackTextBox.Text);
                ClearForm();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AlbumList.SelectedItem != null)
            {
                var selectedAlbum = model.GetAlbums().First(a => $"{a.AlbumName} - {a.Author} - {a.Year}" == AlbumList.SelectedItem.ToString());
                controller.RemoveAlbum(selectedAlbum.Id);
                ClearForm();
            }
        }

        private void AlbumList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AlbumList.SelectedItem != null)
            {
                var selectedAlbum = model.GetAlbums().First(a => $"{a.AlbumName} - {a.Author} - {a.Year}" == AlbumList.SelectedItem.ToString());
                AlbumNameTextBox.Text = selectedAlbum.AlbumName;
                AuthorTextBox.Text = selectedAlbum.Author;
                YearTextBox.Text = selectedAlbum.Year.ToString();
                TrackTextBox.Text = string.Join(",", selectedAlbum.Track);
            }   
        }
        
        private void ClearForm()
        {
            AlbumNameTextBox.Clear();
            AuthorTextBox.Clear();
            YearTextBox.Clear();
            TrackTextBox.Clear();
        }
    }
}