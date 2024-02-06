using BillPayments_LookUp_Validation.Models;
using BillPayments_LookUp_Validation.Models.Responses;
using BillPayments_LookUp_Validation.Services;
using System.Text.Json;
using BillPayments_LookUp_Validation.Models.Requests;
using RestSharp;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class GetCOHBalance : IGetCOHBalance
    {
        private readonly IConfiguration _configuration;

        public GetCOHBalance(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<AuthRequest> ReadIncomingEsbRequestAsync(BALANCE_ENQ_REQ request, string reference)
        {
            var username = _configuration["AppSettings:CohUser"];
            var password = _configuration["AppSettings:CohPassword"];
            var privateKey = _configuration["AppSettings:CohPrivateKey"];
            var participant = _configuration["AppSettings:CohParticipant"];
            var provider = _configuration["AppSettings:CohProvider"];

            string encryptedPass = Encrypt(password, reference, privateKey).ToString();

            var authRequest = new AuthRequest()
            {
                type = "AUTH",
                participant = participant,
                username = username,
                password = encryptedPass,
                provider = provider,
                participantReference = reference,
                billAccount = request.ACCOUNT_NUMBER
            };

            return await Task.FromResult(authRequest);
        }

        public async Task<BALANCE_ENQ_RES> SendAuthRequestAsync(AuthRequest authReq)
        {
            try
            {
                var baseUrl = _configuration["AppSettings:CohBaseUrl"];
                var body = JsonSerializer.Serialize(authReq);

                var options = new RestClientOptions(baseUrl)
                {
                    MaxTimeout = -1,
                };

                var client = new RestClient(options);
                var request = new RestRequest("/api/Payments/SubmitRequest", Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddStringBody(body, DataFormat.Json);

                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var responseObject = JsonSerializer.Deserialize<AuthResponse>(response.Content);

                        if (responseObject != null)
                        {
                            if (responseObject.providerDescription == "SUCCESS")
                            {
                                return new BALANCE_ENQ_RES()
                                {
                                    STATUS = new STATUS { VALID = "Y", DESC = "Account is Valid" },
                                    DETAILS = new DETAILS { ACCOUNT_NAME = responseObject.accountName, ACCOUNT_BALANCE = responseObject.accountBalance.ToString() }
                                };
                            }
                            else
                            {
                                return new BALANCE_ENQ_RES()
                                {
                                    STATUS = new STATUS { VALID = "N", DESC = responseObject.providerDescription },
                                    DETAILS = new DETAILS { }
                                };
                            }
                        }
                        throw new ArgumentNullException("responseObject", "responseObject is null");
                    }
                    throw new ArgumentNullException("response.Content", "response.Content is null");
                }
                else
                {
                    if (response.ErrorException != null)
                        return new BALANCE_ENQ_RES()
                        {
                            STATUS = new STATUS { VALID = "N", DESC = response.ErrorException.Message },
                            DETAILS = new DETAILS { }
                        };
                    return new BALANCE_ENQ_RES()
                    {
                        STATUS = new STATUS { VALID = "N", DESC = "One or more errors occurred." },
                        DETAILS = new DETAILS { }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BALANCE_ENQ_RES()
                {
                    STATUS = new STATUS { VALID = "E", DESC = ex.Message },
                    DETAILS = new DETAILS { }
                };
            }
        }
        private static string Encrypt(string userPassword, string participantReference, string privateKey)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 0x80,
                BlockSize = 0x80
            };

            byte[] pwdBytes = Encoding.UTF8.GetBytes(privateKey);
            byte[] keyBytes = new byte[0x10];

            int len = pwdBytes.Length;

            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;

            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();

            byte[] plainText = Encoding.UTF8.GetBytes(userPassword + "|" + participantReference);

            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        public string SerializeToXml(object o)
        {
            //this avoids xml document declaration
            XmlWriterSettings settings = new()
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            var stream = new MemoryStream();
            using (XmlWriter xw = XmlWriter.Create(stream, settings))
            {
                //this avoids xml namespace declaration
                XmlSerializerNamespaces ns = new(new[] { XmlQualifiedName.Empty });
                XmlSerializer x = new(o.GetType(), "");

                x.Serialize(xw, o, ns);
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}