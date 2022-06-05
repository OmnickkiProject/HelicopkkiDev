using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HelicopkkiDev.View.Behavior
{
    class RelocateMenuItemClickBehavior : BehaviorBase<MenuItem>
    {
        #region Dependency Property
        public Vector ObjectSize
        {
            get { return (Vector)GetValue(ObjectSizeProperty); }
            set { SetValue(ObjectSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectSize.  This enables animation, styling, binding, etc...
        public static DependencyProperty ObjectSizeProperty =
            DependencyProperty.Register("ObjectSize", typeof(Vector), typeof(RelocateMenuItemClickBehavior), new PropertyMetadata());


        public Point ObjectPositionToBeModified
        {
            get { return (Point)GetValue(ObjectPositionToBeModifiedProperty); }
            set { SetValue(ObjectPositionToBeModifiedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectPosition.  This enables animation, styling, binding, etc...
        public static DependencyProperty ObjectPositionToBeModifiedProperty =
            DependencyProperty.Register("ObjectPositionToBeModified", typeof(Point), typeof(RelocateMenuItemClickBehavior), new PropertyMetadata());
        #endregion


        private void MenuItemOnClick(object sender, EventArgs e)
        {
            ObjectPositionToBeModified = new Point(
                SystemParameters.WorkArea.Width * 0.5 - ObjectSize.X * 0.5,
                SystemParameters.WorkArea.Height * 0.5 - ObjectSize.Y * 0.5
            );
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
