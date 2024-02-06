using BillPayments_LookUp_Validation.Models.CST;
using BillPayments_LookUp_Validation.Services;
using BillPayments_LookUp_Validation.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BillPayments_LookUp_Validation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IWalletAccountService _walletAccountService;
        private readonly IConfiguration _config;

        public WalletController(IAuthenticationService authenticationService, IWalletAccountService walletAccountService)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _walletAccountService = walletAccountService ?? throw new ArgumentNullException(nameof(walletAccountService));
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest model)
        {
            try
            {
                string token = await _authenticationService.GetAccessTokenAsync(model.Email, model.Password);
                return Ok(new { AccessToken = token });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

        [HttpGet("account/{identifier}/details")]
        public async Task<IActionResult> GetAccountDetails(string identifier)
        {
            try
            {
                // Retrieve the token from the request headers
                string token = HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrWhiteSpace(token))
                {
                    // Token not found in headers, return unauthorized
                    return Unauthorized(new { Message = "Unauthorized", Error = "Token not provided" });
                }

                // Token is typically provided in the format "Bearer {actual_token}"
                token = token.Replace("Bearer ", "");

                var accountDetails = await _walletAccountService.GetAccountDetailsAsync(token, identifier);
                return Ok(accountDetails);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }

    }

}
