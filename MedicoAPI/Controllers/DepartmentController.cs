using MedicoAPI.DataAccess.Repository.IRepository;
using MedicoAPI.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedicoAPI.Models;

namespace MedicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet("GetDepartmentWithDoctors")]
        
        public IActionResult GetDepartmentWithDoctors()
        {
            try
            {
                var getdept = _unitOfWork.departmentService.GetDepartmentWithDoctors();

                if (getdept.Count() != 0)
                {
                    var responseData = new
                    {
                        status = 200,
                        data = getdept
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

        [HttpGet("GetDoctorsInfo")]

        public IActionResult GetDoctorsInfo()
        {
            try
            {
                var getdept = _unitOfWork.departmentService.GetDoctorsInfo();

                if (getdept.Count() != 0)
                {
                    var responseData = new
                    {
                        status = 200,
                        data = getdept
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

        [HttpGet("GetDoctorsInfoAgain")]

        public IActionResult GetDoctorsInfoAgain()
        {
            try
            {
                var getdept = _unitOfWork.departmentService.GetDoctorsInfo();

                if (getdept.Count() != 0)
                {
                    var responseData = new
                    {
                        status = 200,
                        data = getdept
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

        [HttpGet("GetDoctorsInfoAgainAgain")]

        public IActionResult GetDoctorsInfoAgainAgain()
        {
            try
            {
                var getdept = _unitOfWork.departmentService.GetDoctorsInfo();

                if (getdept.Count() != 0)
                {
                    var responseData = new
                    {
                        status = 200,
                        data = getdept
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

        [HttpGet("GetDoctorsById")]
        public IActionResult GetDoctorsById(int doctorId)
        {
            try
            {
                if (doctorId != null || doctorId != 0)
                {
                    var getDoctor = _unitOfWork.departmentService.GetDoctorDetailsById(doctorId).FirstOrDefault();

                    if (getDoctor != null)
                    {
                            //var responseData = new
                            //{
                            //    //status = 200,
                            //    data = getDoctor
                            //};
                            return Ok(getDoctor);
                      
                    }
                    else
                    {
                        //var responseData = new
                        //{
                        //   // status = 400,
                        //    data = getDoctor
                        //};
                        return Ok(getDoctor);
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
    }
}
