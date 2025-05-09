namespace MedicoAPI.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDepartmentService departmentService { get; }
        IPatientDetailsService patientDetailsService { get; }
    }
}
