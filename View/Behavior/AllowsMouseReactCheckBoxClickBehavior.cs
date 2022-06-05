using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HelicopkkiDev.View.Behavior
{
    class AllowsMouseReactionCheckBoxClickBehavior : BehaviorBase<CheckBox>
    {
        #region DependencyProperty
        public static DependencyProperty AllowsMouseReactionProperty = DependencyProperty.Register(
        "AllowsMouseReaction",
        typeof(bool),
        typeof(AllowsMouseReactionCheckBoxClickBehavior),
        new PropertyMetadata());

        public bool AllowsMouseReaction
        {
            get => (bool)GetValue(AllowsMouseReactionProperty);
            set => SetValue(AllowsMouseReactionProperty, value);
        }

        public static DependencyProperty ReactCharacterByMouseProperty = DependencyProperty.Register(
        "ReactCharacterByMouse",
        typeof(Action<object, EventArgs>),
        typeof(AllowsMouseReactionCheckBoxClickBehavior),
        new PropertyMetadata());

        public Action<object, EventArgs> ReactCharacterByMouse
        {
            get => (Action<object, EventArgs>)GetValue(ReactCharacterByMouseProperty);
            set => SetValue(ReactCharacterByMouseProperty, value);
        }

        public double CharacterFlippedScaleX
        {
            get { return (double)GetValue(CharacterFlippedScaleXProperty); }
            set { SetValue(CharacterFlippedScaleXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterFlippedScaleX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterFlippedScaleXProperty =
            DependencyProperty.Register("CharacterFlippedScaleX", typeof(double), typeof(AllowsMouseReactionCheckBoxClickBehavior), new PropertyMetadata());


        public double CharacterTiltedAngle
        {
            get { return (double)GetValue(CharacterTiltedAngleProperty); }
            set { SetValue(CharacterTiltedAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CharacterTiltedAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CharacterTiltedAngleProperty =
            DependencyProperty.Register("CharacterTiltedAngle", typeof(double), typeof(AllowsMouseReactionCheckBoxClickBehavior), new PropertyMetadata());
        #endregion

        private void CheckBoxOnClick(object sender, EventArgs e)
        {
            if (AllowsMouseReaction)
            {
                CompositionTarget.Rendering += ReactCharacterByMouse.Invoke;
            }
            else
            {
                CompositionTarget.Rendering -= ReactCharacterByMouse.Invoke;
                CharacterFlippedScaleX = 1.0;
                CharacterTiltedAngle = 0.0;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += CheckBoxOnClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= CheckBoxOnClick;
        }
    }
}
