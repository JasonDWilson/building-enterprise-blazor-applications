using BethanysPieShopHRM.ComponentsLibrary.Map;
using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages
{
    public partial class EmployeeDetail
    {
        protected string JobCategory = string.Empty;

        public Employee Employee { get; set; } = new Employee();
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Parameter]
        public int EmployeeId { get; set; }

        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetailsAsync(EmployeeId);

            MapMarkers = new List<Marker>
            {
                new Marker
                {
                    Description = $"{Employee.FirstName} {Employee.LastName}",
                    ShowPopup = false,
                    X = Employee.Longitude,
                    Y = Employee.Latitude
                }
            };
            JobCategory = (await JobCategoryDataService.GetJobCategoryById(Employee.JobCategoryId)).JobCategoryName;
        }
    }
}
