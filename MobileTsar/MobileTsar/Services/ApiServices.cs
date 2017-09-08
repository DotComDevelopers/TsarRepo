using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MobileTsar.Models;
using MobileTsar.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileTsar.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var client = new HttpClient();


            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://api20170705060604.azurewebsites.net/api/Account/Register",
                content);

            return response.IsSuccessStatusCode;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://api20170705060604.azurewebsites.net/token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var jwt = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
            var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);
            return accessToken;

        }

        public async Task<List<Timesheet>> GetTimesheetAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync("http://tsar1.azurewebsites.net/api/mobiletimesheets");

            var timesheets = JsonConvert.DeserializeObject<List<Timesheet>>(json);
            return timesheets;
        }

      public async Task<User> GetUsernameAsync(string accessToken)
      {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var json = await client.GetStringAsync("http://api20170705060604.azurewebsites.net/api/Account/UserInfo");
        
        var user = JsonConvert.DeserializeObject<User>(json);
        return user;
      }



    public async Task PostTimesheetAsync(Timesheet timesheet, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(timesheet);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://tsar1.azurewebsites.net/api/mobiletimesheets", content);
            //add success message here using response 

        }

        public async Task<List<Client>> GetClientAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync("http://tsar1.azurewebsites.net/api/MobileClients");

            var clients = JsonConvert.DeserializeObject<List<Client>>(json);
            return clients;
        }


        //GET CONSULTANTS
        public async Task<List<Consultant>> GetConsultantAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = await client.GetStringAsync("http://tsar1.azurewebsites.net/api/MobileConsultants");

            var consultants = JsonConvert.DeserializeObject<List<Consultant>>(json);
            return consultants;

        }


        public async Task PutTimesheetAsync(int id, Timesheet timesheet, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = JsonConvert.SerializeObject(timesheet);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync("http://tsar1.azurewebsites.net/api/mobiletimesheets/" + id, content);

        }


        public async Task<List<Location>> GetLocationAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = await client.GetStringAsync("http://tsar1.azurewebsites.net/api/MobileLocations");

            var locations = JsonConvert.DeserializeObject<List<Location>>(json);
            return locations;
        }

        public async Task PostLocationAsync(Location location, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(location);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://tsar1.azurewebsites.net/api/MobileLocations", content);
            

        }

        public async Task PutLocationAsync(int id, Location location, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = JsonConvert.SerializeObject(location);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync("http://tsar1.azurewebsites.net/api/MobileLocations/" + id, content);

        }


    }
}

