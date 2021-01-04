using BethanysPieShopHRM.ComponentsLibrary.Map;
using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages
{
    public partial class JobDetail
    {

        [Parameter]
        public int Id { get; set; }

        public Job Job { get; set; } = new Job();
        [Inject]
        public IJobDataService JobDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Job = await JobDataService.GetJobById(Id);
        }
    }
}
