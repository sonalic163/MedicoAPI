using MedicoAPI.Models;

namespace MedicoAPI.DataAccess.Repository.IRepository
{
    public interface IPatientDetailsService
    {
        public List<FeedbackDetails> GetFeedbackDetails();
        public int AddFeedbackDetails(FeedbackDetails feedbackDetails);
        public int AddAppointmentDetails(AppointmentDetails appointmentDetails);
        public List<GetAppointmentDetailsViewModel> GetAppointmentDetails(int dectorId, DateTime appointDate);
    }
}
