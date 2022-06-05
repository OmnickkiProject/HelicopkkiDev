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
    class CloseCharacterWindowMenuItemClickBehavior : BehaviorBase<MenuItem>
    {
        private void MenuItemOnClick(object sender, EventArgs e)
        {
            CharacterWindow.Instance.Close();
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
