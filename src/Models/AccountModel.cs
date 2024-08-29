using System.ComponentModel.DataAnnotations;

namespace MetaFrm.Management.Razor.Models
{
    /// <summary>
    /// AccountModel
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// USER_ID
        /// </summary>
        public decimal? USER_ID { get; set; }

        /// <summary>
        /// EMAIL
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "이메일")]
        public string? EMAIL { get; set; }

        /// <summary>
        /// NICKNAME
        /// </summary>
        [Required]
        [MinLength(3)]
        [Display(Name = "별명")]
        public string? NICKNAME { get; set; }

        /// <summary>
        /// FULLNAME
        /// </summary>
        [Required]
        [MinLength(3)]
        [Display(Name = "성명")]
        public string? FULLNAME { get; set; }

        /// <summary>
        /// PHONENUMBER
        /// </summary>
        [Required]
        [MinLength(6)]
        [Display(Name = "전화번호")]
        public string? PHONENUMBER { get; set; }

        /// <summary>
        /// RESPONSIBILITY_ID
        /// </summary>
        public decimal? RESPONSIBILITY_ID { get; set; }

        /// <summary>
        /// RESPONSIBILITY_NAME
        /// </summary>
        [Required]
        [Display(Name = "권한")]
        public string? RESPONSIBILITY_NAME { get; set; }

        /// <summary>
        /// INACTIVE_DATE
        /// </summary>
        [Display(Name = "비활성")]
        public DateTime? INACTIVE_DATE { get; set; } = DateTime.Now.AddYears(100);
    }
}