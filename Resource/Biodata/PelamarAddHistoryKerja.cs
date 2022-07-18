using System.ComponentModel.DataAnnotations;

namespace BlazorLoker2022.Resource.Biodata
{
    public class PelamarAddHistoryKerja
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Pertanyaan ini wajib diisi")]
        public string tempatKerja { get; set; }
        [Required(ErrorMessage = "Pertanyaan ini wajib diisi")]
        public string posisi { get; set; }
        [Required(ErrorMessage = "Pertanyaan ini wajib diisi")]
        public string tugas { get; set; }
        [Required(ErrorMessage = "Pertanyaan ini wajib diisi")]
        public double salaryTerakhir { get; set; }
        [Required(ErrorMessage = "Pertanyaan ini wajib diisi")]
        public DateTime tglAwal { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Pertanyaan ini wajib diisi")]
        public DateTime tglAkhir { get; set; } = DateTime.Now;
    }
}
