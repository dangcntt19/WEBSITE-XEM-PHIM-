namespace WEBSITE_FILM_002.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class COMMENTS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal COMMENTID { get; set; }

        [Column(TypeName = "ntext")]
        [Required, DisplayName("Nội dung")]
        public string COMMENT_CONTENT { get; set; }

        [Column(TypeName = "date"), DisplayName("Ngày bình luận")]
        public DateTime DATECREATE { get; set; }

        [DisplayName("Trạng thái")]
        public int COMMENT_STATUS { get; set; }

        [DisplayName("Người viết")]
        public decimal USERID { get; set; }

        [DisplayName("Phim")]
        public decimal FILMID { get; set; }

        public virtual FILMS FILMS { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
