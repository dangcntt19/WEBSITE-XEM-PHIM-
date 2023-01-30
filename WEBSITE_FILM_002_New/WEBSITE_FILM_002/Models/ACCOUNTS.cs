namespace WEBSITE_FILM_002.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ACCOUNTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNTS()
        {
            USERS = new HashSet<USERS>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ACCOUNTID { get; set; }

        [Required]
        [StringLength(50), DisplayName("Tài khoản")]
        public string ACCOUNTNAME { get; set; }

        [Column(TypeName = "text")]
        [Required, DisplayName("Mật khẩu"), DataType(DataType.Password)]
        public string ACCOUNTPASS { get; set; }

        [Column(TypeName = "date"), DisplayName("Ngày tạo")]
        public DateTime DATECREATE { get; set; }

        [DisplayName("Quyền tài khoản")]
        public int PERMISSON { get; set; }

        [Column(TypeName = "text"), DisplayName("Liên hệ"), DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USERS> USERS { get; set; }
    }
}
