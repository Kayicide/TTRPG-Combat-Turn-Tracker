using Microsoft.AspNetCore.Components;
using TTRPG_Combat_Turn_Tracker.Shared.Enums;
using TTRPG_Combat_Turn_Tracker.Shared.Objects;

namespace TTRPG_Combat_Turn_Tracker.Client.Components
{
    public partial class CharacterCard : ComponentBase
    {
        [Parameter] public Character Character { get; set; }
        [Parameter] public CharacterPosition Position { get; set; }

        private QuickHealthEdit _quickHealthEdit;

        private string _bgColour = "bg-gray-700";
        private string _textSize = "text-xl xl:text-2xl 2xl:text-4xl";

        public override Task SetParametersAsync(ParameterView parameters)
        {
            base.SetParametersAsync(parameters);

            if (Position == CharacterPosition.Current)
            {
                _bgColour = "bg-gray-800";
                _textSize = "text-2xl lg:text-4xl xl:text-6xl 2xl:text-7xl";
            }
            return Task.CompletedTask;
        }
        private void OpenQuickHealthEditModal()
        {
            _quickHealthEdit.OpenModal(Character);
        }
    }
}
