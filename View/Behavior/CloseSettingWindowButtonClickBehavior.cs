using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelicopkkiDev.View.Behavior
{
    class CloseSettingWindowButtonClickBehavior : BehaviorBase<Button>
    {
        private void ButtonOnClick(object sender, EventArgs e)
        {
            CloseSettingWindow();
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
