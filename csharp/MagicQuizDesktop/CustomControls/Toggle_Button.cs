using System.Windows;
using System.Windows.Controls.Primitives;

namespace MagicQuizDesktop.CustomControls
{

    /// <summary>
    /// Represents a custom toggle button. This button's style and appearances could be set in XAML with a style targeting the Toggle_Button type.
    /// </summary>
    public class Toggle_Button : ToggleButton
    {
        /// <summary>
        /// Static constructor for the Toggle_Button class which overrides the DefaultStyleKeyProperty metadata with its own type.
        /// </summary>
        static Toggle_Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toggle_Button), new FrameworkPropertyMetadata(typeof(Toggle_Button)));
        }
    }
}
