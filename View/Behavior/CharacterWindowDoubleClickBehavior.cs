using Microsoft.Xaml.Behaviors;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace HelicopkkiDev.View.Behavior
{
    class CharacterWindowDoubleClickBehavior : BehaviorBase<Window>
    {
        private void WindowOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                OpenSettingWindow();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDoubleClick += WindowOnMouseDoubleClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseDoubleClick -= WindowOnMouseDoubleClick;
        }
    }
}
