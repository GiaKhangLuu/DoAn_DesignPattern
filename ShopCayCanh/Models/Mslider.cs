namespace ShopCayCanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ShopCayCanh.Library;

    [Table("slider")]
    public partial class Mslider : IPrototype
    {
        public int ID { get; set; }

      
        [StringLength(255)]
        public string name { get; set; }

      
        [StringLength(255)]
        public string url { get; set; }

      
        [StringLength(100)]
        public string position { get; set; }

      
        [StringLength(100)]
        public string img { get; set; }

        public int? orders { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }

        public IPrototype Clone()
        {
            Mslider mslider = new Mslider();
            mslider.name = this.name;
            mslider.url = this.url;
            mslider.position = this.position;
            mslider.img = this.img;
            mslider.orders = this.orders;
            mslider.created_at = this.created_at;
            mslider.created_by = this.created_by;
            mslider.updated_at = this.updated_at;
            mslider.updated_by = this.updated_by;
            mslider.status = this.status;
            return mslider;
        }
    }
}
