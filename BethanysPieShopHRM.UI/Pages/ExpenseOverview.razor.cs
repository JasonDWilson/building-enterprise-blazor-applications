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
    public partial class ExpenseOverview
    {

        public List<Expense> Expenses { get; set; }
        [Inject]
        public IExpenseDataService ExpenseService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Expenses = (await ExpenseService.GetAllExpenses()).ToList();
        }
    }
}
