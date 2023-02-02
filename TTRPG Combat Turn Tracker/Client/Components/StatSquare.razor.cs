using Microsoft.AspNetCore.Components;
using TTRPG_Combat_Turn_Tracker.Shared.Enums;

namespace TTRPG_Combat_Turn_Tracker.Client.Components
{
    public partial class StatSquare : ComponentBase
    {
        [Parameter] public StatType Type { get; set; } = StatType.Unknown;
        [Parameter] public int Value { get; set; } = 0;
        [Parameter] public Size Size { get; set; } = Size.Unknown;

        private string _textSizeExtraLarge = "text-4xl xl:text-5xl 2xl:text-5xl";
        private string _textSizeLarge = "text-2xl xl:text-3xl 2xl:text-5xl";
        private string _textSizeMedium = "text-xl xl:text-2xl 2xl:text-4xl";
        private string _textSizeSmall = "text-xl";
        private string _textSize = "text-xl xl:text-2xl 2xl:text-4xl";

        private string _squareSizeExtraLarge = "w-24 h-24";
        private string _squareSizeLarge = "w-20 h-20";
        private string _squareSizeMedium = "w-14 h-14";
        private string _squareSizeSmall = "w-8 h-8";
        private string _squareSize = "w-14 h-14";


        private string _imageSizeExtraLarge = "h-20 w-20";
        private string _imageSizeLarge = "h-16 w-16";
        private string _imageSizeMedium = "w-11 h-11";
        private string _imageSizeSmall = "w-7 h-7";
        private string _imageSize = "w-11 h-11";

        protected override void OnParametersSet()
        {
            if (Size == Size.ExtraLarge)
            {
                _textSize = _textSizeExtraLarge;
                _squareSize = _squareSizeExtraLarge;
                _imageSize = _imageSizeExtraLarge;
            }

            if (Size == Size.Large)
            {
                _textSize = _textSizeLarge;
                _squareSize = _squareSizeLarge;
                _imageSize = _imageSizeLarge;
            }

            if (Size == Size.Medium)
            {
                _textSize = _textSizeMedium;
                _squareSize = _squareSizeMedium;
                _imageSize = _imageSizeMedium;
            }

            if (Size == Size.Small)
            {
                _textSize = _textSizeSmall;
                _squareSize = _squareSizeSmall;
                _imageSize = _imageSizeSmall;
            }

            base.OnParametersSet();
        }
    }
}
