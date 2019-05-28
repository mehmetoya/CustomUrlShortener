using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomUrlShortener.Model.DbModel
{
    [Table("UrlMap", Schema = "dbo")]
    public class UrlMap
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public int VisitorCount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
