using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages
{
    public partial class EmployeeEdit
    {
        //needed to bind to select to value
        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        //used to store state of screen
        protected string Message = string.Empty;
        protected bool Saved;
        protected string StatusClass = string.Empty;

        public List<Country> Countries { get; set; } = new List<Country>();

        [Inject]
        public ICountryDataService CountryDataService { get; set; }

        public Employee Employee { get; set; } = new Employee();
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        public InputText LastNameInputText { get; set; }

        [Inject]
        public ProtectedLocalStorage LocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task HandleValidSubmit()
        {
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0) //new
            {
                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }

        protected void NavigateToOverview() { NavigationManager.NavigateTo("/employeeoverview"); }

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await CountryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            var savedEmployee = await LocalStorageService.GetAsync<Employee>("Employee");

            if (savedEmployee != null && employeeId == 0)
            {
                Employee = savedEmployee;
            }
            else if (employeeId == 0) //new employee is being created
            {
                //add some defaults
                Employee = new Employee
                {
                    CountryId = 1,
                    JobCategoryId = 1,
                    BirthDate = DateTime.Now,
                    JoinedDate = DateTime.Now
                };
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployeeDetailsAsync(int.Parse(EmployeeId));
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }

        public async Task TempSave()
        {
            await LocalStorageService.SetAsync("Employee", Employee);
            NavigateToOverview();
        }
    }
}
