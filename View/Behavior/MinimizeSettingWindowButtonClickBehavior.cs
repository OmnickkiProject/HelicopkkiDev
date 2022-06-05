using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelicopkkiDev.View.Behavior
{
    class MinimizeSettingWindowButtonClickBehavior : BehaviorBase<Button>
    {
        private void ButtonOnClick(object sender, EventArgs e)
        {
            SettingWindow.Instance.WindowState = WindowState.Minimized;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += ButtonOnClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= ButtonOnClick;
        }
    }
}
