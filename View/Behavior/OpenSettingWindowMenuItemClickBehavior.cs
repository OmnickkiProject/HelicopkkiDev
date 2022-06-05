using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelicopkkiDev.View.Behavior
{
    class OpenSettingWindowMenuItemClickBehavior : BehaviorBase<MenuItem>
    {
        private void MenuItemOnClick(object sender, EventArgs e)
        {
            OpenSettingWindow();
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += MenuItemOnClick;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= MenuItemOnClick;
        }
    }
}
