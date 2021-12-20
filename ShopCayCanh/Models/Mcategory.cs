namespace ShopCayCanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ShopCayCanh.Library;

    [Table("category")]
    public partial class Mcategory : IPrototype
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

        [StringLength(150)]
        public string metadesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? created_at { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }

        public IPrototype Clone()
        {
            Mcategory mcategory = new Mcategory();
            mcategory.name = this.name;
            mcategory.slug = this.slug;
            mcategory.parentid = this.parentid;
            mcategory.orders = this.orders;
            mcategory.metakey = this.metakey;
            mcategory.metadesc = this.metadesc;
            mcategory.created_at = this.created_at;
            mcategory.created_by = this.created_by;
            mcategory.updated_at = this.updated_at;
            mcategory.updated_by = this.updated_by;
            mcategory.status = this.status;
            return mcategory;
        }
    }
}
