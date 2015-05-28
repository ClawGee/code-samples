using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Media
    {
        public int Id { get; set; }
        public Guid? Uid { get; set; }
        public string FileName { get; set; }
        public DateTime CreateDate { get; set; }
        public string ContentType { get; set; }
        public bool Deleted { get; set; }
        public int EntityType { get; set; }
        public int EntityId { get; set; }
        public int MediaType { get; set; }
        public string FullUrl { get; set; }
        
    }
}