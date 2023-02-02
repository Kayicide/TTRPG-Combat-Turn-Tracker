using Microsoft.AspNetCore.Components;
using TTRPG_Combat_Turn_Tracker.Client.Services;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Client.Components
{
    public partial class LeftSideBar : ComponentBase, IDisposable
    {
        [Inject] public CharacterService CharacterService { get; set; }

        private QuickHealthEdit _quickHealthEdit;

        protected override void OnInitialized()
        {
            CharacterService.OnChange += OnChangeHandler;
        }

        private void OpenQuickHealthEditModal(Character character)
        {
            _quickHealthEdit.OpenModal(character);
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
