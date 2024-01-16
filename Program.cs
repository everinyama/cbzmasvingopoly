using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MasvingoPoly_Post
{
    class Program
    {
        static void Main(string[] args)
        {
            SendTransaction();
        }

        static async void SendTransaction()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM tblTransactions WHERE TransStatus IS NULL";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string ID = row["ID"].ToString();
                            string bankAccount = row["bankAccount"].ToString();
                            string amount = row["amount"].ToString();
                            string narrative = row["narrative"].ToString();
                            DateTime PaymntDate = Convert.ToDateTime(row["PaymentDate"].ToString());
                            string PaymentDate = PaymntDate.ToString("dd/MM/yyyy");
                            string reference = row["reference"].ToString();
                            string nr1 = row["nr1"].ToString();
                            string nr2 = row["nr2"].ToString();
                            string nr3 = row["nr3"].ToString();
                            string nr4 = row["nr4"].ToString();
                            string transactionDate = row["transactionDate"].ToString();
                            string status = row["status"].ToString();


                            string requestJson = "{";
                            requestJson += "\"id\":\"" + ID + "\",";
                            requestJson += "\"bankAccount\":\"" + bankAccount + "\",";
                            requestJson += "\"amount\":\"" + amount + "\",";
                            requestJson += "\"narrative\":\"" + narrative + "\",";
                            requestJson += "\"reference\":\"" + reference + "\",";
                            requestJson += "\"nr1\":\"" + nr1 + "\",";
                            requestJson += "\"nr2\":\"" + nr2 + "\",";
                            requestJson += "\"nr3\":\"" + nr3 + "\",";
                            requestJson += "\"nr4\":\"" + nr4 + "\",";
                            requestJson += "\"transactionDate\":\"" + transactionDate + "\",";
                            requestJson += "\"status\":\"" + status + "\"";
                            requestJson += "}";


                            WebProxy myProxy = new WebProxy();
                            Uri newUri = new Uri(ConfigurationManager.AppSettings["proxy"].ToString());
                            myProxy.Address = newUri;
                            myProxy.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["username"].ToString(), ConfigurationManager.AppSettings["password"].ToString(), ConfigurationManager.AppSettings["domain"].ToString());
                            string key = ConfigurationManager.AppSettings["key"].ToString();

                            var client = new RestClient(ConfigurationManager.AppSettings["gzu_api"].ToString());
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", "Basic " + key);
                            //request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["api_user"].ToString(), ConfigurationManager.AppSettings["api_password"].ToString());
                            client.Proxy = myProxy;


                            request.AddParameter("application/json", requestJson, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            string myresponse = response.Content;
                            string statusCode = response.StatusCode.ToString();



                            if (statusCode == "OK")
                            {
                                UpdateResponse(ID, statusCode, myresponse);
                            }
                            else
                            {
                                UpdateResponse(ID, statusCode, myresponse);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }


            }
        }

        private static void UpdateResponse(string ID, string statusCode, string myresponse)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tblTransactions SET TransStatus='" + statusCode + "', GzuResponse = '" + myresponse + "', ResponseDate = GetDate()  WHERE ID = '" + ID + "'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }
s
    }
}
