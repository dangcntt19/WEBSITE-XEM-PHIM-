namespace WEBSITE_FILM_002.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FILMS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FILMS()
        {
            COMMENTS = new HashSet<COMMENTS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal FILMID { get; set; }

        [Column(TypeName = "ntext")]
        [Required, DisplayName("Tên phim")]
        public string FILMNAME { get; set; }

        [Column(TypeName = "nvarchar"), DisplayName("Trạng thái")]
        public string STATUS { get; set; }

        [Column(TypeName = "ntext"), DisplayName("Đạo diễn")]
        public string FILMDIRECTOR { get; set; }

        [Column(TypeName = "date"), DisplayName("Năm sản xuất")]
        public DateTime? PRODUCTIONYEAR { get; set; }

        [DisplayName("Thời lượng")]
        public int? TIME { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Chất lượng")]
        public string QUALITY { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Độ phân giải")]
        public string RESOLUTION { get; set; }

        [Column(TypeName = "nvarchar")]
        [DisplayName("Ngôn ngữ")]
        public string LANGUAGE { get; set; }

        [Column(TypeName = "nvarchar")]
        [DisplayName("Thể Loại")]
        public string CATEGORY { get; set; }

        [DisplayName("Lượt xem")]                
        public int? VIEWS { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Hãng phim")]
        public string MANUFACTURING_COMPANY { get; set; }

        [DisplayName("Điểm đánh giá")]
        public int? MOVIEREVIEW { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung")]
        public string CONTENT_FILM { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày tạo")]
        public DateTime DATECREATE { get; set; }

        [DisplayName("Trạng thái (Hiện/ẩn)")]
        public int FILM_STATUS { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Ảnh bìa")]
        public string IMAGEID { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Phim tải lên")]
        public string VIDEOID { get; set; }

        [DisplayName("Người đăng")]
        public decimal USERID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTS> COMMENTS { get; set; }

        public virtual USERS USERS { get; set; }
    }
}
