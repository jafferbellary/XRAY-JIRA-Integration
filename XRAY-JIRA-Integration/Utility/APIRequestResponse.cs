using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRAY_JIRA_Integration.Utility
{
    public class APIRequestResponse
    {
        public static String postAuthenticationRequest()
        {
            //System.out.println("Client ID::: "+ConfigFileReader.getValue("client_id"));
            Console.WriteLine("Client ID::: " + ConfigurationManager.AppSettings["client_id"]);
            string POST_PARAMS = "{\"client_id\": \"" + ConfigurationManager.AppSettings["client_id"] + "\",\"client_secret\": \"" + ConfigurationManager.AppSettings["client_secret"] + "\"}"; ;
            Uri obj = new Uri("https://xray.cloud.xpand-it.com/api/v1/authenticate");
            //URL obj = new URL("https://xray.cloud.xpand-it.com/api/v1/authenticate");
            RestClient Client = new RestClient();
            RestRequest Request = new RestRequest(obj, Method.POST);
            Request.AddHeader("Content-Type", "application/json");
            Request.RequestFormat = DataFormat.Json;
            Request.AddJsonBody(new { client_id = ConfigurationManager.AppSettings["client_id"], client_secret = ConfigurationManager.AppSettings["client_secret"] });
            IRestResponse restResponse = Client.Execute(Request);

            //var _dict = JSONUtils.JsonConverter<Dictionary<string, string>>(restResponse.Content);
            var dict = restResponse.Content;
            //var _access_token = _dict["access_token"];
            //var authenticator = new JwtAuthenticator(_access_token);
            //Client.Authenticator = authenticator;

            int responseCode = (int)restResponse.StatusCode;
            Console.WriteLine("AUTHENTICATION POST Response Code :  " + responseCode);
            //HttpURLConnection postConnection = (HttpURLConnection)obj.openConnection();
            //postConnection.setRequestMethod("POST");
            //postConnection.setRequestProperty("Content-Type", "application/json");
            //postConnection.setDoOutput(true);
            //OutputStream os = postConnection.getOutputStream();
            //os.write(POST_PARAMS.getBytes());
            //os.flush();
            //os.close();
            //int responseCode = postConnection.getResponseCode();
            //System.out.println("AUTHENTICATION POST Response Code :  " + responseCode);
            //      BufferedReader in = new BufferedReader(new InputStreamReader(postConnection.getInputStream()));
            //      String inputLine;
            //      StringBuilder response = new StringBuilder();
            //      while ((inputLine = in.readLine()) != null) {
            //          response.append(inputLine);
            //      }
            //in.close();        

            //return restResponse.ToString();
            return dict.ToString();
        }

        public static void postBodyRequest(String jsonInputString, String autherizationToken)
        {
            Uri obj = new Uri("https://xray.cloud.xpand-it.com/api/v1/import/execution");
            RestClient Client = new RestClient();
            RestRequest Request = new RestRequest(obj, Method.POST);
            Request.AddHeader("Authorization", "Bearer " + autherizationToken);
            Request.AddHeader("Content-Type", "application/json; charset=utf-8");
            Request.RequestFormat = DataFormat.Json;
            Request.AddJsonBody(jsonInputString);
            IRestResponse restResponse = Client.Execute(Request);
            //HttpURLConnection postConnection = (HttpURLConnection)obj.openConnection();
            //postConnection.setRequestMethod("POST");
            //postConnection.setRequestProperty("Authorization", "Bearer " + autherizationToken);
            //postConnection.setRequestProperty("Content-Type", "application/json");
            //postConnection.setDoOutput(true);

            //OutputStream os = postConnection.getOutputStream();
            //byte[] input = jsonInputString.getBytes("utf-8");
            //os.write(input, 0, input.length);
            //os.flush();
            //os.close();
            //int responseCode = postConnection.getResponseCode();
            int responseCode = (int)restResponse.StatusCode;
            Console.WriteLine("UPDATE EXECUTION RESULTS POST Response Code :  " + responseCode);
            //BufferedReader in = new BufferedReader(new InputStreamReader(postConnection.getInputStream()));
            //String inputLine = null;
            //StringBuilder response = new StringBuilder();
            //while ((inputLine = in.readLine()) != null) 
            //{
            //    response.append(inputLine);
            //}
            Console.WriteLine("Response::: " + restResponse.Content);
		    //in.close();
        }
    }
}

