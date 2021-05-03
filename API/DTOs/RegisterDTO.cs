using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage="Please enter a UserName")]
        public string userName { get; set; }

        [Required(ErrorMessage="Please Enter a PassWord")]
        [StringLength(8, MinimumLength=1)]
        public string passWord { get; set; }
    }
}