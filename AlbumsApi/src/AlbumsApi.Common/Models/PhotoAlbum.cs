using System;
using System.Collections.Generic;
using System.Text;

namespace AlbumsApi.Common.Models
{
    public class PhotoAlbum
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public IEnumerable<Photo> Photos { get; set; }
    }
}
