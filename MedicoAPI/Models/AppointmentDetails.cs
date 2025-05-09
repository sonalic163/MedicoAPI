using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedicoAPI.Models
{
    public class AppointmentDetails
    {
        [Key]
        public int appointId { get; set; }
        public string patientName { get; set; }
        public string? patientEmail { get; set; }
        public string patientPhoneNo { get; set; }
        public int deptId { get; set; }
        public int dectorId { get; set; }
        public DateTime appointDate { get; set; }
        public string? message { get; set; }
    }

    [Keyless]
    public class GetAppointmentDetailsViewModel
    {
        public int appointId { get; set; }
        public string patientName { get; set; }
    }

    public class FeedbackDetails
    {
        [Key]
        public int feedbackId { get; set; }
        public string? feedbackName { get; set; }
        public string? feedbackMessage { get; set; }
        public int starRating { get; set; }
        public DateTime? createdOn { get; set; }
    }

}
