namespace ShopCayCanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ShopCayCanh.Library;

    [Table("topic")]
    public partial class Mtopic : IPrototype
    {
        public int ID { get; set; }
   
        [StringLength(255)]
        public string name { get; set; }
        
        [StringLength(255)]
        public string slug { get; set; }

        public int parentid { get; set; }

        public int orders { get; set; }

        [StringLength(150)]
        public string metakey { get; set; }
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
            Mtopic mtopic = new Mtopic();
            mtopic.name = this.name;
            mtopic.slug = this.slug;
            mtopic.parentid = this.parentid;
            mtopic.orders = this.orders;
            mtopic.metakey = this.metakey;
            mtopic.metadesc = this.metadesc;
            mtopic.created_at = this.created_at;
            mtopic.created_by = this.created_by;
            mtopic.updated_at = this.updated_at;
            mtopic.updated_by = this.updated_by;
            mtopic.status = this.status;
            return mtopic;
        }
    }
}
