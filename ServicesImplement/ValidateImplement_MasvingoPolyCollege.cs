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
        private readonly IStudentService _studentService;

        public ValidateImplement_MasvingoPolyCollege(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        public string validate_masvingo_poly(BillValidation billerVallidation)
        {
            // Switch by college
            //
            // Create the request URL
            string url = "https://easylearn.co.zw/portal2/api/cbz/getStudent?target=13&studentNo="+billerVallidation.FieldValue;

            // Create the request object
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

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
                                FielD_NAME = myApiResponse.StudentNo,
                                Lov = myApiResponse.Name
                            };

                            GetStudentByIdResponse getStudentResponse = _studentService.GetStudentByIdAsync(getStudentById).Result;

                            if (getStudentResponse != null)
                            {
                                
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
