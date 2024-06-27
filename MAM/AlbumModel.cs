using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAM
{
    public class Album
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public List<string> Track { get; set; }
    }
    public class AlbumModel
    {
        private List<Album> albums = new List<Album>();

        public List<Album> GetAlbums()
        {
            return albums;
        }

        public void AddAlbum(Album album)
        {
            albums.Add(album);
        }

        public void RemoveAlbum(int id) 
        { 
            albums.RemoveAll(album => album.Id == id);
        }

        public void EditAlbum(Album album)
        {
            var CurrentAlbum = albums.Find(a => a.Id == album.Id);
            if(CurrentAlbum != null)
            {
                CurrentAlbum.AlbumName = album.AlbumName;
                CurrentAlbum.Author = album.Author;
                CurrentAlbum.Year = album.Year;
                CurrentAlbum.Track = album.Track;
            }
        }
    }
}
