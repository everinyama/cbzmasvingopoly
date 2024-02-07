using BillPayments_LookUp_Validation.Models;
using BillPayments_LookUp_Validation.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;
using BillPayments_LookUp_Validation.Models.Requests;
using BillPayments_LookUp_Validation.Models.Responses;

namespace BillPayments_LookUp_Validation.ServicesImplement
{

    public class ValidateImplement_MasvingoPolyCollege : ControllerBase, IValidate_MasvingoPolyCollege
    {
        private readonly IStudentService _studentservice;
        private readonly IConfiguration _configuration;

        public ValidateImplement_MasvingoPolyCollege()
        {
        }
        public ValidateImplement_MasvingoPolyCollege(IStudentService studentService, IConfiguration configuration)
        {
            _studentservice = studentService;
            _configuration = configuration;
        }

        public string validate_masvingo_poly(BillValidation billerVallidation)
        {
            // Switch by college
            //
            // Create the request URL
            string masvingoLookupUrl = _configuration["MasvingoLookUpUrl"] + billerVallidation.FieldValue;
            
            // Create the request object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(masvingoLookupUrl);
            request.Method = "GET";
            //request.Proxy = new WebProxy(_configuration["CbzProxyIP"]);

            try
            {
                // Get the response from the server
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Read the response stream
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream);
                        string responseJson = reader.ReadToEnd();
                        int statusCode = (int)response.StatusCode;
                        // Handle the response data
                        //Console.WriteLine("Response: " + responseJson);

                        if (!responseJson.Contains("StudentNo"))
                        {
                            BILLVALIDATION bILLVALIDATION = new BILLVALIDATION()
                            {
                                STATUS = new STATUS()
                                {
                                    VALID = "N",
                                    DESC = "Student Number Not Found"

                                },
                                FIELDDESCRIPTION = "",
                                FIELDVALUE = ""
                            };

                            // Stringify the response object to XML string

                            string data_jsonObject = JsonConvert.SerializeObject(bILLVALIDATION);
                            var docBillerValidationResponse = JsonConvert.DeserializeXmlNode(data_jsonObject, "BILL_VALIDATION");

                            using (var stringWriter = new StringWriter())
                            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                            {
                                docBillerValidationResponse!.WriteTo(xmlTextWriter);
                                xmlTextWriter.Flush();
                                string xmloutput = stringWriter.GetStringBuilder().ToString();
                                return xmloutput;
                            };
                        }
                        else
                        {

                            MasvingoPolyCollegeSuccess myApiResponse = JsonConvert.DeserializeObject<MasvingoPolyCollegeSuccess>(responseJson)!;

                            GetStudentByIdRequest getStudentById = new()
                            {
                                FielD_NAME = "UZ_STUDENT_REG",
                                Lov = "H220193A_test"
                            };

                            try
                            {
                             
                                GetStudentByIdResponse getStudentResponse = _studentservice.GetStudentByIdAsync(getStudentById).GetAwaiter().GetResult();

                                if (getStudentResponse.StatusCode == 100)
                                {
                                    AddStudentRequest addStudentRequest = new()
                                    {
                                        LoV_DESC = myApiResponse.Level,
                                        Lov = myApiResponse.StudentNo,
                                        FielD_NAME = myApiResponse.Name
                                    };

                                    AddStudentResponse addStudentResponse = _studentservice.AddNewStudentAsync(addStudentRequest).GetAwaiter().GetResult();
                                    Console.WriteLine(addStudentResponse);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle exceptions
                            }


                            // Prepare the MFS/Mobile app developers response object

                            BILLVALIDATION bILLVALIDATION = new BILLVALIDATION()
                            {
                                STATUS = new STATUS()
                                {
                                    VALID = "Y",
                                    DESC = "Account is valid"

                                },
                                FIELDDESCRIPTION = myApiResponse.Name,
                                FIELDVALUE = myApiResponse.StudentNo
                            };

                            // Stringify the response object to XML string

                            string data_jsonObject = JsonConvert.SerializeObject(bILLVALIDATION);
                            var docBillerValidationResponse = JsonConvert.DeserializeXmlNode(data_jsonObject, "BILL_VALIDATION");

                            using (var stringWriter = new StringWriter())
                            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                            {
                                docBillerValidationResponse!.WriteTo(xmlTextWriter);
                                xmlTextWriter.Flush();
                                string xmloutput = stringWriter.GetStringBuilder().ToString();
                                return xmloutput;
                            };
                        }

                    return responseJson;
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle any exceptions that occur during the request
                // Console.WriteLine("An error occurred: " + ex.Message);

                // Prepare the MFS/Mobile app developers response object

                BILLVALIDATION bILLVALIDATION = new BILLVALIDATION()
                {
                    STATUS = new STATUS()
                    {
                        VALID = "N",
                        DESC = ex.Message

                    },
                    FIELDDESCRIPTION = "",
                    FIELDVALUE = ""
                };

                // Stringify the response object to XML string

                string data_jsonObject = JsonConvert.SerializeObject(bILLVALIDATION);
                var docBillerValidationResponse = JsonConvert.DeserializeXmlNode(data_jsonObject, "BILL_VALIDATION");

                using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    docBillerValidationResponse!.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    string xmloutput = stringWriter.GetStringBuilder().ToString();
                    return xmloutput;
                };

            }

            return "first test";
       }
    }
}
