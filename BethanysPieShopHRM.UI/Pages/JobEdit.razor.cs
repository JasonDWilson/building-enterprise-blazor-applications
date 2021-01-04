using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Components;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages
{
    public partial class JobEdit
    {

        public Job Job { get; set; } = new Job();
        [Inject]
        public IJobDataService JobDataService { get; set; }

        [Parameter]
        public int JobId { get; set; }
        public string Message { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task HandleValidSubmit()
        {
            if (Job.Id == 0) // New 
            {
                await JobDataService.AddJob(Job);
            }
            else
            {
                await JobDataService.UpdateJob(Job);
            }

            NavigationManager.NavigateTo("/jobs");
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/jobs");
        }

        protected override async Task OnInitializedAsync()
        {
            if (JobId != 0)
            {
                Job = await JobDataService.GetJobById(JobId);
            }
        }
    }
}
