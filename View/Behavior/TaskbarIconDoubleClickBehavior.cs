using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelicopkkiDev.View.Behavior
{
    class TaskbarIconDoubleClickBehavior : BehaviorBase<TaskbarIcon>
    {
        private void TaskbarIconOnTrayMouseDoubleClick(object sender, EventArgs e)
        {
            OpenSettingWindow();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TrayMouseDoubleClick += TaskbarIconOnTrayMouseDoubleClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.TrayMouseDoubleClick -= TaskbarIconOnTrayMouseDoubleClick;
        }
    }
}
