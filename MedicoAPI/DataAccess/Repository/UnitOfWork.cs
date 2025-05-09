using MedicoAPI.DataAccess.Repository.IRepository;

namespace MedicoAPI.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentService departmentService { get; private set; }
        public IPatientDetailsService patientDetailsService { get; private set; }

        private ApplicationDbContext _db;

        IConfiguration _config;

        public UnitOfWork(ApplicationDbContext db, IConfiguration config)
        {
            _config = config;
            _db = db;
            departmentService = new DepartmentService(_db);
            patientDetailsService = new PatientDetailsService(_db);
        }
    }
}
