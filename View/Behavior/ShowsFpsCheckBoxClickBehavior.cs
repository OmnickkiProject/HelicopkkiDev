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
    class ShowsFpsCheckBoxClickBehavior : BehaviorBase<CheckBox>
    {
        public Action<object, EventArgs> CalculateFps
        {
            get { return (Action<object, EventArgs>)GetValue(CalculateFpsProperty); }
            set { SetValue(CalculateFpsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalculateFps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalculateFpsProperty =
            DependencyProperty.Register("CalculateFps", typeof(Action<object, EventArgs>), typeof(ShowsFpsCheckBoxClickBehavior), new PropertyMetadata());


        public bool ShowsFps
        {
            get { return (bool)GetValue(ShowsFpsProperty); }
            set { SetValue(ShowsFpsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowsFps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowsFpsProperty =
            DependencyProperty.Register("ShowsFps", typeof(bool), typeof(ShowsFpsCheckBoxClickBehavior), new PropertyMetadata());


        private void CheckBoxOnClick(object sender, EventArgs e)
        {
            if (ShowsFps)
            {
                CompositionTarget.Rendering += CalculateFps.Invoke;
            }
            else
            {
                CompositionTarget.Rendering -= CalculateFps.Invoke;
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
