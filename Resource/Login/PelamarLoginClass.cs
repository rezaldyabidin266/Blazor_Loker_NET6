using System.ComponentModel.DataAnnotations;

namespace BlazorLoker2022.Resource.Login
{
    public class PelamarLoginClass
    {
        [Required(ErrorMessage = "Email Tidak Boleh Kosong")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Password Tidak Boleh Kosong")]
        public string? password { get; set; }
        public string ipAddress { get; set; }
        public string browser { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string remarks { get; set; }
    }
}
