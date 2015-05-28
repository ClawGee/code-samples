using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Gallery
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsPublic { get; set; }

        public int EntityType { get; set; }

        public int EntityId { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }

        public Guid Uid { get; set; }

        public List<Media> GalleryPhotos { get; set; }

        public FullUser GalleryUser { get; set; }
    }
}