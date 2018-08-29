using System.ComponentModel.DataAnnotations;

namespace Drossey.Areas.admin.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string PersonalId { get; set; }
        public string PhotoUrl { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public long? PlaceId { get; set; }
        public int? CouffierNumber { get; set; }
        [Required]
        public long CityId { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsOnline { get; set; }
   

    }
}
