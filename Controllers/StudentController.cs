using BillPayments_LookUp_Validation.Models.Requests;
using BillPayments_LookUp_Validation.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillPayments_LookUp_Validation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("AddNewStudent")]
        public async Task<IActionResult> AddNewStudent([FromBody] AddStudentRequest request)
        {
            var response = await _studentService.AddNewStudentAsync(request);

            return Ok(response);
        }

        [HttpGet("GetStudentById/{FielD_NAME}/{Lov}")]
        public async Task<IActionResult> GetStudentById(string FielD_NAME, string Lov)
        {
            var request = new GetStudentByIdRequest
            {
                FielD_NAME = FielD_NAME,
                Lov = Lov
            };

            var response = await _studentService.GetStudentByIdAsync(request);

            return Ok(response);
        }

    }
}
