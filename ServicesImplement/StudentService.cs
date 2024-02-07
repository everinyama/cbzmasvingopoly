using BillPayments_LookUp_Validation.Models.Responses;
using BillPayments_LookUp_Validation.Services;
using System.Text.Json;
using BillPayments_LookUp_Validation.Models.Requests;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class StudentService: IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public StudentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<AddStudentResponse> AddNewStudentAsync(AddStudentRequest request)
        {
            var studentInternalApiInsert = _configuration["CbzInternalStudentsApiInsert"];

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(studentInternalApiInsert, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<AddStudentResponse>(jsonResponse);
            }

            // Handle errors or return a default response
            return new AddStudentResponse
            {
                StatusCode = (int)response.StatusCode,
                StatusMessage = response.ReasonPhrase
            };
        }

        public async Task<GetStudentByIdResponse> GetStudentByIdAsync(GetStudentByIdRequest request)
        {
            var studentInternalApiRetrieve = _configuration["CbzInternalStudentsApiRetrieve"] + request.FielD_NAME + "/" + request.Lov;

            var response = await _httpClient.GetAsync(studentInternalApiRetrieve);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GetStudentByIdResponse>(jsonResponse);
            }

            // Handle errors or return a default response
            return new GetStudentByIdResponse
            {
                StatusCode = (int)response.StatusCode,
                StatusMessage = response.ReasonPhrase
            };
        }
    }
}
