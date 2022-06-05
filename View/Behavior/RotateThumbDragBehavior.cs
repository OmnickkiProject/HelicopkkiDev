using HelicopkkiDev.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HelicopkkiDev.View.Behavior
{
    class RotateThumbDragBehavior : BehaviorBase<Thumb>
    {
        public double ObjectAngleToBeModified
        {
            get { return (double)GetValue(ObjectAngleToBeModifiedProperty); }
            set { SetValue(ObjectAngleToBeModifiedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectAngleToBeModified.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectAngleToBeModifiedProperty =
            DependencyProperty.Register("ObjectAngleToBeModified", typeof(double), typeof(RotateThumbDragBehavior), new PropertyMetadata());


        public Vector ObjectSize
        {
            get { return (Vector)GetValue(ObjectSizeProperty); }
            set { SetValue(ObjectSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectSizeProperty =
            DependencyProperty.Register("ObjectSize", typeof(Vector), typeof(RotateThumbDragBehavior), new PropertyMetadata());


        public Point ObjectPosition
        {
            get { return (Point)GetValue(ObjectPositionProperty); }
            set { SetValue(ObjectPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectPositionProperty =
            DependencyProperty.Register("ObjectPosition", typeof(Point), typeof(RotateThumbDragBehavior), new PropertyMetadata());


        public bool IsObjectSnapped
        {
            get { return (bool)GetValue(IsObjectSnappedProperty); }
            set { SetValue(IsObjectSnappedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsObjectSnapped.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsObjectSnappedProperty =
            DependencyProperty.Register("IsObjectSnapped", typeof(bool), typeof(RotateThumbDragBehavior), new PropertyMetadata());


        Point _characterCenterPoint;
        Vector _startVector;
        double _initialAngle;

        double _targetAngleModel;
        bool _targetSnapLineModel;

        private void RotateThumbDragStarted(double targetAngle)
        {
            Point mouseStartPoint;

            _characterCenterPoint = ObjectPosition + ObjectSize * 0.5;

            mouseStartPoint = Mouse.GetPosition(null);
            _startVector = mouseStartPoint - _characterCenterPoint;
            _initialAngle = targetAngle;
        }

        private void RotateThumbDragDelta()
        {
            Point mouseCurrentPoint = Mouse.GetPosition(null);
            Vector deltaVector = mouseCurrentPoint - _characterCenterPoint;

            double nonSnapAngle = (_initialAngle + Vector.AngleBetween(_startVector, deltaVector)).ToDeg180();

            // 각도 Snap 범위는 +- 5도로 고정
            double snapRange = 5;

            if (Math.Abs(nonSnapAngle) < snapRange)
            {
                _targetAngleModel = 0;
                _targetSnapLineModel = true;
            }
            else if (nonSnapAngle < 90 + snapRange && nonSnapAngle > 90 - snapRange)
            {
                _targetAngleModel = 90;
                _targetSnapLineModel = true;
            }
            else if (nonSnapAngle < -90 + snapRange && nonSnapAngle > -90 - snapRange)
            {
                _targetAngleModel = -90;
                _targetSnapLineModel = true;
            }
            else if (Math.Abs(nonSnapAngle) > 180 - snapRange)
            {
                _targetAngleModel = 180;
                _targetSnapLineModel = true;
            }
            else
            {
                _targetAngleModel = nonSnapAngle;
                if (_targetSnapLineModel)
                {
                    _targetSnapLineModel = false;
                }
            }
        }

        private void ThumbOnDragStarted(object sender, DragStartedEventArgs e)
        {
            RotateThumbDragStarted(ObjectAngleToBeModified);
        }

        private void ThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            RotateThumbDragDelta();
            ObjectAngleToBeModified = _targetAngleModel;
            IsObjectSnapped = _targetSnapLineModel;
        }

        private void ThumbOnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (IsObjectSnapped)
            {
                IsObjectSnapped = false;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DragStarted += ThumbOnDragStarted;
            AssociatedObject.DragDelta += ThumbOnDragDelta;
            AssociatedObject.DragCompleted += ThumbOnDragCompleted;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DragStarted -= ThumbOnDragStarted;
            AssociatedObject.DragDelta -= ThumbOnDragDelta;
            AssociatedObject.DragCompleted -= ThumbOnDragCompleted;
        }
    }
}
