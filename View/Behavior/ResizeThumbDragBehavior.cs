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
    class ResizeThumbDragBehavior : BehaviorBase<Thumb>
    {
        public Vector ObjectSizeToBeModified
        {
            get { return (Vector)GetValue(ObjectSizeToBeModifiedProperty); }
            set { SetValue(ObjectSizeToBeModifiedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectSizeToBeModified.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectSizeToBeModifiedProperty =
            DependencyProperty.Register("ObjectSizeToBeModified", typeof(Vector), typeof(ResizeThumbDragBehavior), new PropertyMetadata());


        public double ObjectAngle
        {
            get { return (double)GetValue(ObjectAngleProperty); }
            set { SetValue(ObjectAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectAngleProperty =
            DependencyProperty.Register("ObjectAngle", typeof(double), typeof(ResizeThumbDragBehavior), new PropertyMetadata());


        public Vector ObjectMinSize
        {
            get { return (Vector)GetValue(ObjectMinSizeProperty); }
            set { SetValue(ObjectMinSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectMinSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectMinSizeProperty =
            DependencyProperty.Register("ObjectMinSize", typeof(Vector), typeof(ResizeThumbDragBehavior), new PropertyMetadata());


        public Vector ObjectMaxSize
        {
            get { return (Vector)GetValue(ObjectMaxSizeProperty); }
            set { SetValue(ObjectMaxSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectMaxSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectMaxSizeProperty =
            DependencyProperty.Register("ObjectMaxSize", typeof(Vector), typeof(ResizeThumbDragBehavior), new PropertyMetadata());


        public double ObjectAspectRatio
        {
            get { return (double)GetValue(ObjectAspectRatioProperty); }
            set { SetValue(ObjectAspectRatioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectAspectRatio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectAspectRatioProperty =
            DependencyProperty.Register("ObjectAspectRatio", typeof(double), typeof(ResizeThumbDragBehavior), new PropertyMetadata());


        private void ThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double objectRad = ObjectAngle.ToRadian();
            Vector preSize = ObjectSizeToBeModified;
            Vector dragDelta = ConvertHelper.DeltaToScreenGrid(e.HorizontalChange * Math.Cos(objectRad), e.VerticalChange * -Math.Sin(objectRad), ObjectAngle);

            double resultWidth = (preSize.X + dragDelta.X).ToRange(ObjectMinSize.X, ObjectMaxSize.X);
            double resultHeight = resultWidth * ObjectAspectRatio;
            ObjectSizeToBeModified = new Vector(resultWidth, resultHeight);

            // CharacterPosition Model 내부에서 계산됨
            // ObjectPositionToBeModified -= (ObjectSizeToBeModified - preSize) * 0.5;
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
