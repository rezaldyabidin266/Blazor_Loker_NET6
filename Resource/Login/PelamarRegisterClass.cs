using System.ComponentModel.DataAnnotations;

namespace BlazorLoker2022.Resource.Login
{
    public class PelamarRegisterClass
    {
        [Required(ErrorMessage = "Invalid")]
        public string nama { get; set; }
        [Required(ErrorMessage = "Invalid")]
        public string alamat { get; set; }  
        [Required(ErrorMessage = "Invalid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Invalid")]
        public string password { get; set; }
        [Required(ErrorMessage = "Invalid")]
        public string noTlp { get; set; }
        [Required(ErrorMessage = "Invalid")]
        public string tempatLahir { get; set; }
        [Required(ErrorMessage = "Invalid")]
        public DateTime tglLahir { get; set; }
        [Required(ErrorMessage = "Invalid")]
        public string note { get; set; }

    }
}
