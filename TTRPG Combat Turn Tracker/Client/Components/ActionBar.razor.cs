using Microsoft.AspNetCore.Components;
using TTRPG_Combat_Turn_Tracker.Client.Services;

namespace TTRPG_Combat_Turn_Tracker.Client.Components
{
    public partial class ActionBar : ComponentBase
    {
        [Inject] public CharacterService CharacterService { get; set; }

        private AddCharacter _addCharacterModal;
        private AddMonster _addMonsterModal;
        private void NextTurn()
        {
            CharacterService.NextTurn();
        }

        private void OpenCharacterModal()
        {
            _addCharacterModal.OpenModal();
        }

        private void OpenMonsterModal()
        {
            _addMonsterModal.OpenModal();
        }
    }
}
