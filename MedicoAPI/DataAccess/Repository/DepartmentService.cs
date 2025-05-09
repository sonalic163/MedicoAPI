using MedicoAPI.DataAccess.Repository.IRepository;
using MedicoAPI.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MedicoAPI.DataAccess.Repository
{
    public class DepartmentService : IDepartmentService
    {
        private ApplicationDbContext db;

        public DepartmentService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<DeptDocInfoViewModel> GetDepartmentWithDoctors()
        {
            try
            {  
               var query = $"CALL sp_GetDeptDocInfo()";
               var status = db.DeptDocInfoViewModel.FromSqlRaw(query).ToList();

                return status;
            }
            catch (Exception ex)
            {
                return new List<DeptDocInfoViewModel>();
            }
        }

        public List<Doc_Info> GetDoctorsInfo()
        {
            try
            {
                var query = $"CALL sp_GetDocInfo()";
                var status = db.Doc_Info.FromSqlRaw(query).ToList();

                return status.Select(d => new Doc_Info
                {
                    doctorId = d.doctorId,
                    doctorName = d.doctorName,
                    doctorImg = d.doctorImg != null ? (d.doctorImg) : null
                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<Doc_Info>();
            }
        }

        public List<Doc_InfoByIdViewModel> GetDoctorDetailsById(int dectorId)
        {
            try
            {
                var parameter = new List<MySqlParameter>
                {
                    new MySqlParameter("@p_dectorId", dectorId), 
                };

                var query = $"CALL sp_GetDocInfoById(@p_dectorId)";

                var status = db.Doc_InfoByIdViewModel
                        .FromSqlRaw(query, parameter.ToArray()).ToList();
                return status;
            }
            catch (Exception ex)
            {
                return new List<Doc_InfoByIdViewModel>();
            }
        }


    }
}
