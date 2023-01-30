namespace WEBSITE_FILM_002.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USERS()
        {
            COMMENTS = new HashSet<COMMENTS>();
            FILMS = new HashSet<FILMS>();
            IMAGES = new HashSet<IMAGES>();
            VIDEOS = new HashSet<VIDEOS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal USERID { get; set; }

        [Required]
        [StringLength(255), DisplayName("Họ")]
        public string FIRSTNAME { get; set; }

        [Required]
        [StringLength(255), DisplayName("Tên")]
        public string LASTNAME { get; set; }

        [Column(TypeName = "date"), DisplayName("Ngày sinh")]
        public DateTime BORN { get; set; }

        public int GENDER { get; set; }

        [Column(TypeName = "ntext"), DisplayName("Ảnh đại diện")]
        public string IMAGENAME { get; set; }

        [DisplayName("Trạng thái tài khoản")]
        public int USERSTATUS { get; set; }

        [DisplayName("Tài khoản liên kết")]
        public decimal ACCOUNTID { get; set; }

        public virtual ACCOUNTS ACCOUNTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTS> COMMENTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FILMS> FILMS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMAGES> IMAGES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIDEOS> VIDEOS { get; set; }
    }
}
