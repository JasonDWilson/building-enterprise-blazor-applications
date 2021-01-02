using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.UI.Pages
{
    public partial class TaskList
    {
        [Parameter]
        public int Count { get; set; }

        [Inject]
        public ILogger<TaskList> Logger { get; set; }

        [Inject]
        public NavigationManager navManager { get; set; }

        public List<HRTask> Tasks { get; set; } = new List<HRTask>();

        [Inject]
        public ITaskDataService taskService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            List<HRTask> tasks = new List<HRTask>();
            try
            {
                tasks = (await taskService.GetAllTasks()).ToList();
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex, "Error getting tasks");
            }

            if (Count != 0)
                tasks = tasks.Take(Count).ToList();
            Tasks = tasks;
        }

        public void AddTask() { navManager.NavigateTo("taskedit"); }
    }
}
