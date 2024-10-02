using System.ComponentModel.DataAnnotations;

namespace WAMS.Backend.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "No Uid")]
        public string? UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "No Pw")]
        public string? Password { get; set; }


    }
}
