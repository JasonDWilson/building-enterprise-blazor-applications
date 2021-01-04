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
    public partial class JobsOverview
    {

        public List<Job> Jobs { get; set; }
        [Inject]
        public IJobDataService JobService { get; set; }

        protected async Task OnDeleteClick(int id)
        {
            await JobService.DeleteJob(id);
            await OnInitializedAsync();
        }


        protected override async Task OnInitializedAsync()
        {
            Jobs = (await JobService.GetAllJobs()).ToList();
        }
    }
}
