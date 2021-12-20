namespace ShopCayCanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ShopCayCanh.Library;

    [Table("contact")]
    public partial class Mcontact : IPrototype
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string fullname { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(15)]
        public string phone { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? created_at { get; set; }

        public int? created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }

        public IPrototype Clone()
        {
            Mcontact mcontact = new Mcontact();
            mcontact.fullname = this.fullname;
            mcontact.email = this.email;
            mcontact.phone = this.phone;
            mcontact.title = this.title;
            mcontact.detail = this.detail;
            mcontact.created_at = this.created_at;
            mcontact.created_by = this.created_by;
            mcontact.updated_at = this.updated_at;
            mcontact.updated_by = this.updated_by;
            mcontact.status = this.status;
            return mcontact;
        }
    }
}
