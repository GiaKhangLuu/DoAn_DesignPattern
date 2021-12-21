namespace ShopCayCanh.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using ShopCayCanh.Library;

    [Table("user")]
    public partial class Muser : IPrototype
    {
        public int ID { get; set; }

        
        [StringLength(255)]
        public string fullname { get; set; }

        
        [StringLength(225)]
        public string username { get; set; }

        
        [StringLength(64)]
        public string password { get; set; }

        
        [StringLength(255)]
        public string email { get; set; }

        [StringLength(5)]
        public string gender { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(100)]
        public string img { get; set; }

        public int access { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime updated_at { get; set; }

        public int updated_by { get; set; }

        public int status { get; set; }

        public IPrototype Clone()
        {
            Muser muser = new Muser();
            muser.fullname = this.fullname;
            muser.username = this.username;
            muser.password = this.password;
            muser.email = this.email;
            muser.gender = this.gender;
            muser.phone = this.phone;
            muser.img = this.img;
            muser.access = this.access;
            muser.created_at = this.created_at;
            muser.created_by = this.created_by;
            muser.updated_at = this.updated_at;
            muser.updated_by = this.updated_by;
            muser.status = this.status;
            return muser;
        }
    }
}
