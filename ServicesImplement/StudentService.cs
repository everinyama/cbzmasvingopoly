using BillPayments_LookUp_Validation.Models.Responses;
using BillPayments_LookUp_Validation.Services;
using System.Text.Json;
using BillPayments_LookUp_Validation.Models.Requests;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class StudentService: IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AddStudentResponse> AddNewStudentAsync(AddStudentRequest request)
        {
            var apiUrl = "http://192.168.3.150:83/api/Student/AddNewStudent";

            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);

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
            var apiUrl = $"http://192.168.3.150:83/api/Student/GetStudentById/{request.FielD_NAME}/{request.Lov}";

            var response = await _httpClient.GetAsync(apiUrl);

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
