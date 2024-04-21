using System.Windows;
using System.Windows.Controls.Primitives;

namespace MagicQuizDesktop.CustomControls
{

    public class Toggle_Button : ToggleButton
    {
        static Toggle_Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toggle_Button), new FrameworkPropertyMetadata(typeof(Toggle_Button)));
        }
    }
}
