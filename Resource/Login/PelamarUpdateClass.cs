using System.ComponentModel.DataAnnotations;

namespace BlazorLoker2022.Resource.Login
{
    public class PelamarUpdateClass
    {
        public string nama { get; set; }
        public string alamat { get; set; }
        [Required(ErrorMessage = "Email Tidak Boleh Kosong")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password Tidak Boleh Kosong")]
        public string password { get; set; }
        public string noTlp { get; set; }
        public string tempatLahir { get; set; }
        public DateTime tglLahir { get; set; }
        public string note { get; set; }
    }
}
