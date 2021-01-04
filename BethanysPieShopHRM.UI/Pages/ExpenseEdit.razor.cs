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
    public partial class ExpenseEdit
    {

        //needed to bind to select value
        protected string CurrencyId = "1";
        protected string EmployeeId = "1";

        public List<Currency> Currencies { get; set; } = new List<Currency>();

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public Expense Expense { get; set; } = new Expense();
        [Inject]
        public IExpenseApprovalService ExpenseApprovalService { get; set; }

        [Parameter]
        public string ExpenseId { get; set; }

        [Inject]
        public IExpenseDataService ExpenseService { get; set; }
        public string Message { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task HandleValidSubmit()
        {
            Expense.EmployeeId = int.Parse(EmployeeId);
            Expense.CurrencyId = int.Parse(CurrencyId);

            Expense.Amount *= Currencies.FirstOrDefault(x => x.CurrencyId == Expense.CurrencyId).USExchange;
            Expense.Status = await ExpenseApprovalService.GetExpenseStatusAsync(Expense);

            if (Expense.ExpenseId == 0) // New 
            {
                await ExpenseService.AddExpense(Expense);
                NavigationManager.NavigateTo("/expenses");
            }
            else
            {
                await ExpenseService.UpdateExpense(Expense);
                NavigationManager.NavigateTo("/expenses");
            }
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/expenses");
        }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            Currencies = (await ExpenseService.GetAllCurrencies()).ToList();

            int.TryParse(ExpenseId, out var expenseId);

            if (expenseId != 0)
            {
                Expense = await ExpenseService.GetExpenseById(int.Parse(ExpenseId));
            }
            else
            {
                Expense = new Expense() { EmployeeId = 1, CurrencyId = 1, Status = ExpenseStatus.Open, ExpenseType = ExpenseType.Other };
            }

            CurrencyId = Expense.CurrencyId.ToString();
            EmployeeId = Expense.EmployeeId.ToString();
        }
    }
}
