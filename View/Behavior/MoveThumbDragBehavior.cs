using HelicopkkiDev.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace HelicopkkiDev.View.Behavior
{
    class MoveThumbDragBehavior : BehaviorBase<Thumb>
    {

        public Point ObjectPositionToBeModified
        {
            get { return (Point)GetValue(ObjectPositionToBeModifiedProperty); }
            set { SetValue(ObjectPositionToBeModifiedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectPositionToBeModified.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectPositionToBeModifiedProperty =
            DependencyProperty.Register("ObjectPositionToBeModified", typeof(Point), typeof(MoveThumbDragBehavior), new PropertyMetadata());


        public double ObjectAngle
        {
            get { return (double)GetValue(ObjectAngleProperty); }
            set { SetValue(ObjectAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectAngleProperty =
            DependencyProperty.Register("ObjectAngle", typeof(double), typeof(MoveThumbDragBehavior), new PropertyMetadata());



        private void ThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Vector delta = ConvertHelper.DeltaToScreenGrid(e.HorizontalChange, e.VerticalChange, ObjectAngle);
            ObjectPositionToBeModified += delta;
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DragDelta += ThumbOnDragDelta;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DragDelta -= ThumbOnDragDelta;
        }
    }
}
