using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TTRPG_Combat_Turn_Tracker.Client.Services;

namespace TTRPG_Combat_Turn_Tracker.Client.Pages
{
    public partial class Index : ComponentBase, IDisposable
    {
        [Inject] public CharacterService CharacterService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CharacterService.InitializeHubConnectionAsync();
            await CharacterService.JoinEncounter("test");
            CharacterService.OnChange += OnChangeHandler;
        }

        public void Dispose()
        {
            CharacterService.OnChange -= OnChangeHandler;
        }
        private async void OnChangeHandler()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
