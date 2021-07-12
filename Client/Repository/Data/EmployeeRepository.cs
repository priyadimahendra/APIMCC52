using API.Model;
using Client.Base;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<ShowProfile>> ShowProfile()
        {
            List<ShowProfile> entities = new List<ShowProfile>();

            using (var response = await httpClient.GetAsync(request + "ShowProfile/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<ShowProfile>>(apiResponse);
            }
            return entities;
        }

        public async Task<ShowProfile> ShowProfile(string id)
        {
            ShowProfile entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ShowProfile>(apiResponse);
            }
            return entity;
        }

    }
}
