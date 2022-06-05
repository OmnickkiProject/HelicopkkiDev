using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HelicopkkiDev.View.Behavior
{
    class SettingWindowMouseLeftButtonDownBehavior : BehaviorBase<Window>
    {
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AssociatedObject.DragMove();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        }
    }
}
