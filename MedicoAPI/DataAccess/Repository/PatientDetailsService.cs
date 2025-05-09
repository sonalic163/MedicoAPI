using MedicoAPI.DataAccess.Repository.IRepository;
using MedicoAPI.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace MedicoAPI.DataAccess.Repository
{
    public class PatientDetailsService : IPatientDetailsService
    {
        private ApplicationDbContext db;

        public PatientDetailsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddAppointmentDetails(AppointmentDetails appointmentDetails)
        {
            try
            {
                var parameter = new List<MySqlParameter>
                {
                   new MySqlParameter("@p_patientName", appointmentDetails.patientName),
                   new MySqlParameter("@p_patientEmail", appointmentDetails.patientEmail),
                   new MySqlParameter("@p_patientPhoneNo", appointmentDetails.patientPhoneNo),
                   new MySqlParameter("@p_deptId", appointmentDetails.deptId),
                   new MySqlParameter("@p_dectorId", appointmentDetails.dectorId),
                    new MySqlParameter("@p_appointDate", appointmentDetails.appointDate),
                   new MySqlParameter("@p_message", appointmentDetails.message),
                };

                var query = $"CALL sp_InsertPatientDetails(@p_patientName,@p_patientEmail,@p_patientPhoneNo,@p_deptId,@p_dectorId,@p_appointDate,@p_message)";
                var data = db.Database.ExecuteSqlRaw(query, parameter.ToArray());
                return data;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<GetAppointmentDetailsViewModel> GetAppointmentDetails(int dectorId, DateTime appointDate)
        {
            try
            {
                var parameter = new List<MySqlParameter>
                {
                    new MySqlParameter("@p_dectorId", dectorId),
                    new MySqlParameter("@p_appointDate", appointDate)
                };

                var query = $"CALL sp_GetApponitmentDetails(@p_dectorId,@p_appointDate)";

                var status = db.GetAppointmentDetailsViewModel
                        .FromSqlRaw(query, parameter.ToArray()).ToList();
                return status;
            }
            catch (Exception ex)
            {
                return new List<GetAppointmentDetailsViewModel>();
            }
        }
        public int AddFeedbackDetails(FeedbackDetails feedbackDetails)
        {
            try
            {
                var parameter = new List<MySqlParameter>
                {
                   new MySqlParameter("@p_feedbackName", feedbackDetails.feedbackName),
                   new MySqlParameter("@p_feedbackMessage", feedbackDetails.feedbackMessage),
                   new MySqlParameter("@p_starRating", feedbackDetails.starRating)
                };

                var query = $"CALL sp_InsertFeedbackDetails(@p_feedbackName, @p_feedbackMessage, @p_starRating)";
                var data = db.Database.ExecuteSqlRaw(query, parameter.ToArray());
                return data;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public List<FeedbackDetails> GetFeedbackDetails()
        {
            try
            {            
                var query = $"CALL sp_GetFeedbackInfo()";

                var status = db.FeedbackDetails
                        .FromSqlRaw(query).ToList();
                return status;
            }
            catch (Exception ex)
            {
                return new List<FeedbackDetails>();
            }
        }

    }
}
