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
        [Display(Name = "Email")]
        public string? EMAIL { get; set; }

        /// <summary>
        /// NICKNAME
        /// </summary>
        [Required]
        [MinLength(3)]
        [Display(Name = "Nickname")]
        public string? NICKNAME { get; set; }

        /// <summary>
        /// FULLNAME
        /// </summary>
        [Required]
        [MinLength(3)]
        [Display(Name = "Fullname")]
        public string? FULLNAME { get; set; }

        /// <summary>
        /// PHONENUMBER
        /// </summary>
        [Required]
        [MinLength(6)]
        [Display(Name = "Phone number")]
        public string? PHONENUMBER { get; set; }

        /// <summary>
        /// RESPONSIBILITY_ID
        /// </summary>
        public decimal? RESPONSIBILITY_ID { get; set; }

        /// <summary>
        /// RESPONSIBILITY_NAME
        /// </summary>
        [Required]
        [Display(Name = "Permissions")]
        public string? RESPONSIBILITY_NAME { get; set; }

        /// <summary>
        /// INACTIVE_DATE
        /// </summary>
        [Display(Name = "Inactive date")]
        public DateTime? INACTIVE_DATE { get; set; } = DateTime.Now.AddYears(100);
    }
}