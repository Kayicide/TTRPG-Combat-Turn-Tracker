using Microsoft.AspNetCore.Components;
using TTRPG_Combat_Turn_Tracker.Client.Services;
using TTRPG_Combat_Turn_Tracker.Shared.Enums;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Client.Components
{
    public partial class AddCharacter: ComponentBase
    {
        [Parameter] public bool IsOpen { get; set; }
        [Inject] public CharacterService CharacterService { get; set; }

        private string _name = "";
        private int _initiative = 0;
        private int _armourClass = 0;
        private int _health = 0;

        private int _strength = 0;
        private int _dexterity = 0;
        private int _constitution = 0;
        private int _intelligence = 0;
        private int _wisdom = 0;
        private int _charisma = 0;

        public void Add()
        {
            CharacterService.AddCharacter(new Character(_name, CharacterType.Ally, _health){ArmourClass = _armourClass, Initiative = _initiative});
            Close();
        }
        public void Close()
        {
            IsOpen = false;
        }

        public void OpenModal()
        {
            IsOpen = true;
            StateHasChanged();
        }
    }
}

