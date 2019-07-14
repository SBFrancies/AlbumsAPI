using System;
using System.Collections.Generic;
using System.Text;

namespace AlbumsApi.Client.Models
{
    public class AlbumEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }
    }
}
