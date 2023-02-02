using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TTRPG_Combat_Turn_Tracker.Client.Services;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Client.Components
{
    public partial class QuickHealthEdit : ComponentBase
    {
        [Parameter] public bool IsOpen { get; set; }
        [Inject] public CharacterService CharacterService { get; set; }

        private Character _character;

        ElementReference textInput;

        private int _current = 0;
        private int _max = 0;
        public void Close()
        {
            IsOpen = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (IsOpen)
                await textInput.FocusAsync();
        }

        public void Enter(KeyboardEventArgs e)
        {
            if(e.Code == "Enter" || e.Code == "NumPadEnter")
                Set();
        }

        public void OpenModal(Character character)
        {
            _character = character;
            _current = _character.CurrentHealth;
            _max = _character.MaxHealth;

            IsOpen = true;

            StateHasChanged();
        }

        public void Set()
        {
            _character.Health.SetCurrent(_current);
            CharacterService.CharacterUpdated();
            StateHasChanged();
            Close();
        }
    }
}
