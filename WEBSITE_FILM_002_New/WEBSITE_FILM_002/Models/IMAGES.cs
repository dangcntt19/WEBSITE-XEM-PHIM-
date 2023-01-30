namespace WEBSITE_FILM_002.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMAGES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IMAGEID { get; set; }

        [Column(TypeName = "ntext")]
        [Required, DisplayName("Tên hình ảnh")]
        public string IMAGENAME { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày đăng tải")]
        public DateTime DATECREATE { get; set; }

        [DisplayName("Người tải lên")]
        public decimal USERID { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
