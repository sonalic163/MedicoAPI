using MedicoAPI.DataAccess.Repository.IRepository;
using MedicoAPI.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicoAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using MedicoAPI.DataAccess.Repository;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace MedicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailReceiverService _emailReceiverService;
        private readonly IConfiguration _configuration;

        public PatientDetailsController(IUnitOfWork unitOfWork, ApplicationDbContext context, EmailReceiverService emailReceiverService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _emailReceiverService = emailReceiverService;
            _configuration = configuration;
        }

        [HttpPost("AddPatientDetails")]
        public IActionResult AddPatientDetails(AppointmentDetails appointmentDetails)
        {
            try
            {
                if (appointmentDetails != null)
                {
                    var getPatient = _unitOfWork.patientDetailsService.GetAppointmentDetails(appointmentDetails.dectorId, appointmentDetails.appointDate).ToList();

                    if(getPatient.Count == 0)
                    {
                        var addPatient = _unitOfWork.patientDetailsService.AddAppointmentDetails(appointmentDetails);
                        if (addPatient == 1)
                        {
                           

                            var responseData = new
                            {
                                status = 200,
                                data = "Patient appointment registered successfully"
                            };
                            return Ok(responseData);
                        }
                        else
                        {
                            var responseData = new
                            {
                                status = 400,
                                data = "Patient appointment failed to register"
                            };
                            return Ok(responseData);
                        }
                    }
                    else
                    {
                        var responseData = new
                        {
                            status = 401,
                            data = "Please select another date or time"
                        };
                        return Ok(responseData);
                    }

                }
                else
                {
                    return Ok("Failed to register patient appointment");
                }
            }
            catch (Exception ex)
            {
                var Response_Body = ex.Message;
                return Ok(Response_Body);
            }

        }

        [HttpPost("AddFeedbackDetails")]
        public IActionResult AddFeedbackDetails(FeedbackDetails feedbackDetails)
        {
            try
            {
                if (feedbackDetails != null)
                {
                        var addFeedback = _unitOfWork.patientDetailsService.AddFeedbackDetails(feedbackDetails);
                        if (addFeedback == 1)
                        {
                           
                            var responseData = new
                            {
                                status = 200,
                                data = "Your reviews submitted successfully"
                            };
                            return Ok(responseData);
                        }
                        else
                        {
                            var responseData = new
                            {
                                status = 400,
                                data = "Failed to review submit."
                            };
                            return Ok(responseData);
                        }
                    }
                    else
                    {
                        var responseData = new
                        {
                            status = 401,
                            data = "Something went wrong."
                        };
                        return Ok(responseData);
                    }

            }
            catch (Exception ex)
            {
                var Response_Body = ex.Message;
                return Ok(Response_Body);
            }

        }

        [HttpGet("GetFeedbackDetails")]
        public IActionResult GetFeedbackDetails()
        {
            try
            {
                var getfeedback = _unitOfWork.patientDetailsService.GetFeedbackDetails();

                if (getfeedback.Count() != 0)
                {
                    var responseData = new
                    {
                        status = 200,
                        data = getfeedback
                    };
                    return Ok(responseData);
                }
                else
                {
                    var responseData = new
                    {
                        status = 201,
                        data = new List<object>()
                    };
                    return Ok(responseData);
                }
            }
            catch (Exception ex)
            {
                var Response_Body = ex.Message;
                return BadRequest(Response_Body);
            }
        }

       
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(ReceivedEmail receivedEmail)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new { status = 400, data = "Email is not sent." });
            }

            try
            {
                await _emailReceiverService.SendEmailAsync(receivedEmail);
                return Ok(new { status = 200, data = "Email sent successfully." });
            }
            catch (Exception ex)
            {
                return Ok(new { status = 500, data = "Internal server error"});
            }
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(ReceivedEmail receivedEmail)
         {
            if (receivedEmail == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Create the email
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(receivedEmail.Name, receivedEmail.From)); // Sender's email
                email.To.Add(new MailboxAddress("Sonali Chandole", _configuration["EmailSettings:ReceiverEmail"])); // Fixed receiver email
                email.Subject = receivedEmail.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                    <p><strong>Name:</strong> {receivedEmail.Name}</p>
                    <p><strong>Email:</strong> {receivedEmail.From}</p>
                    <p><strong>Phone:</strong> {receivedEmail.PhoneNo}</p>
                    <p><strong>Message:</strong>{receivedEmail.Body}</p>" 
                };

                email.Body = bodyBuilder.ToMessageBody();

                // Send the email
                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"], _configuration["EmailSettings:SenderPassword"]);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }

                return Ok(new { status =200 ,data = "Email sent successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
    }

