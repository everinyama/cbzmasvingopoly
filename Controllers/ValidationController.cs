using BillPayments_LookUp_Validation.Models;
using BillPayments_LookUp_Validation.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace BillPayments_LookUp_Validation.Controllers
{
    [ApiController]
    [Route("[controller]")]


    ////
    ///
        public class ValidationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
            private readonly IValidate_MasvingoPolyCollege _Validate_MasvingoPolyCollegeService;

            public ValidationController(ILogger<WeatherForecastController> logger , IValidate_MasvingoPolyCollege MasvingoPolyCollegeService)
        {
            _logger = logger;
            _Validate_MasvingoPolyCollegeService = MasvingoPolyCollegeService;
                
        }

        [HttpGet(Name = "Validation")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[AllowAnonymous]
        [HttpPost("BillerValidationSwitch")]
        public string PaymentTinVerification(RequestObject model)
        {


            switch (model.Trantype!.Trim())
            {

                case "40_BVL_5":
                    {
                        string xmlString = model.XmlRequestObject!;
                        BillValidation billValidation = new BillValidation();
                        XmlSerializer serializer = new XmlSerializer(typeof(BillValidation));
                        using (StringReader reader = new StringReader(xmlString))
                        {
                            billValidation = (BillValidation)serializer.Deserialize(reader)!;
                        }

                        // Get all School Billers from Db
                        // Compare the incoming Biller with exixting billers
                        // Get the URL from the matching DB biller 

                        if(billValidation.FieldName == "MPC_STUDENT_NO")
                        {
                            var response = _Validate_MasvingoPolyCollegeService.validate_masvingo_poly(billValidation);
                            return response;

                        }
                        if (billValidation.FieldName == "GREATZIM_STUDENT_NO")
                        {
                            var response = _Validate_MasvingoPolyCollegeService.validate_masvingo_poly(billValidation);
                            return response;

                        }

                        //var response = _zimraService.PaymentTinVerification(model);
                        //return response;
                        return "Sample DataResponse";
                    }
                
                default:
                    {
                        return "Unknown transaction type";
                    }

            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginFormModel loginForm)
        {
            // Perform authentication logic here
            // You can access the form data using the loginForm object

            // Example authentication logic
            if (loginForm.Username == "john_doe" && loginForm.Password == "secretpassword")
            {
                // Authentication successful
                return Ok("Login successful");
            }
            else
            {
                // Authentication failed
                return Unauthorized("Invalid username or password");
            }
        }

        public class LoginFormModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }


    }
}