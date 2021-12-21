namespace ShopCayCanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    using ShopCayCanh.Library;

    [Table("post")]
    public partial class Mpost : IPrototype
    {
        public int ID { get; set; }

        public int? topid { get; set; }
      
        public string title { get; set; }

        [StringLength(255)]
        public string slug { get; set; }
   
        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(50)]
        public string type { get; set; }
        
        [StringLength(150)]
        public string metakey { get; set; }
      
        [StringLength(150)]
        public string metadesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }

        public IPrototype Clone()
        {
            Mpost mpost = new Mpost();
            mpost.topid = this.topid;
            mpost.title = this.title;
            mpost.slug = this.slug;
            mpost.detail = this.detail;
            mpost.img = this.img;
            mpost.type = this.type;
            mpost.metakey = this.metakey;
            mpost.metadesc = this.metadesc;
            mpost.created_at = this.created_at;
            mpost.created_by = this.created_by;
            mpost.updated_at = this.updated_at;
            mpost.updated_by = this.updated_by;
            mpost.status = this.status;
            return mpost;
        }
    }
}
