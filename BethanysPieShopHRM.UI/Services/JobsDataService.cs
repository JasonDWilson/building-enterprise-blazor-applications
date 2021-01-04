using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Services
{
    public class JobsDataService : IJobDataService
    {
        private readonly HttpClient _client;

        public JobsDataService(HttpClient client) => _client = client;

        public async Task AddJob(Job newJob) => await _client.PostJsonAsync("jobs", newJob);

        public async Task DeleteJob(int jobId) => await _client.DeleteAsync($"jobs/{jobId}");

        public async Task<IEnumerable<Job>> GetAllJobs() => await _client.GetJsonAsync<Job[]>("jobs");


        public async Task<Job> GetJobById(int jobId) => await _client.GetJsonAsync<Job>($"jobs/{jobId}");

        public async Task UpdateJob(Job updatedJob) => await _client.PutJsonAsync("jobs", updatedJob);
    }
}
