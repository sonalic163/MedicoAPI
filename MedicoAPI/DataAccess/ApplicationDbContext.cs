using MedicoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicoAPI.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public ApplicationDbContext()
        {

        }

        public virtual DbSet<FeedbackDetails> FeedbackDetails { get; set; }
        public virtual DbSet<DeptDocInfoViewModel> DeptDocInfoViewModel { get; set; }
        public virtual DbSet<AppointmentDetails> AppointmentDetails { get; set; }
        public virtual DbSet<GetAppointmentDetailsViewModel> GetAppointmentDetailsViewModel { get; set; }
        public virtual DbSet<Doc_Info> Doc_Info { get; set; }
        public virtual DbSet<Doc_InfoByIdViewModel> Doc_InfoByIdViewModel { get; set; }

    }
}
