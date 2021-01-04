using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Components;
using BethanysPieShopHRM.UI.Components;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages
{
    public partial class EmployeeOverview
    {

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public List<Employee> Employees { get; set; }

        [Inject]
        public ILogger<EmployeeOverview> Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                throw new Exception("blah");
            }
            catch (Exception e)
            {
                Logger.LogError("that wasn't good", e);
            }
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async void AddEmployeeDialog_OnDialogClose()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }
    }
}
