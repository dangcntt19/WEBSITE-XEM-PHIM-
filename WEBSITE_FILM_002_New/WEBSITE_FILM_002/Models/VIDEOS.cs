namespace WEBSITE_FILM_002.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VIDEOS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal VIDEOID { get; set; }

        [Column(TypeName = "ntext")]
        [Required, DisplayName("Tên Video")]
        public string VIDEONAME { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày đăng tải")]
        public DateTime DATECREATE { get; set; }

        [DisplayName("Người đăng tải")]
        public decimal USERID { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
