using MedicoAPI.Models;

namespace MedicoAPI.DataAccess.Repository.IRepository
{
    public interface IDepartmentService
    {
        public List<DeptDocInfoViewModel> GetDepartmentWithDoctors();
        public List<Doc_Info> GetDoctorsInfo();
        public List<Doc_InfoByIdViewModel> GetDoctorDetailsById(int dectorId);
    }
}
