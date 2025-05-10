using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedicoAPI.Models
{
    [Keyless]
    public class DeptDocInfoViewModel
    {
        public int deptId {  get; set; }
        public string deptName { get; set; }
        public int doctorId { get; set; }
        public string doctorName { get; set; }
    }

   
    public class Doc_Info
    {
        [Key]
        public int doctorId { get; set; }
        public string doctorName { get; set; }
        public byte[]? doctorImg { get; set; }
     
    }

    public class Doc_InfoByIdViewModel
    {
        [Key]
        public int doctorId { get; set; }
        public string doctorName { get; set; }
        public byte[] doctorImg { get; set; }
        public string aboutDoctor { get; set; }
        public string education { get; set; }
        public string speciality { get; set; }



    }
}
