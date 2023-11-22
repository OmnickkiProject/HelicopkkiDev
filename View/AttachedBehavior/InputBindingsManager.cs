/*
 * Original Code
 * https://stackoverflow.com/questions/563195/bind-textbox-on-enter-key-press
 * modified by HGS
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace HelicopkkiDev.View.AttachedBehavior
{
    /// <summary>
    /// TextBox에 Text 입력 후 Enter키 입력 시 값 업데이트
    /// </summary>
    class InputBindingsManager
    {
        public static readonly DependencyProperty UpdatePropertySourceWhenEnterPressedProperty = DependencyProperty.RegisterAttached(
            "UpdatePropertySourceWhenEnterPressed", typeof(DependencyProperty), typeof(InputBindingsManager), new PropertyMetadata(null, OnUpdatePropertySourceWhenEnterPressedPropertyChanged));

        public static void SetUpdatePropertySourceWhenEnterPressed(DependencyObject dp, DependencyProperty value)
        {
            dp.SetValue(UpdatePropertySourceWhenEnterPressedProperty, value);
        }

        public static DependencyProperty GetUpdatePropertySourceWhenEnterPressed(DependencyObject dp)
        {
            return (DependencyProperty)dp.GetValue(UpdatePropertySourceWhenEnterPressedProperty);
        }

        private static void OnUpdatePropertySourceWhenEnterPressedPropertyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = dp as UIElement;

            if (element == null)
            {
                return;
            }

            if (e.OldValue != null)
            {
                element.PreviewKeyDown -= HandlePreviewKeyDown;
            }

            if (e.NewValue != null)
            {
                element.PreviewKeyDown += new KeyEventHandler(HandlePreviewKeyDown);
            }


        }

        static void HandlePreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // 값 업데이트
                DoUpdateSource(e.Source);

                // 부모 요소로 포커스 이동 후 keyboard focus 해제
                FrameworkElement frameworkElement = e.OriginalSource as FrameworkElement;

                FrameworkElement parent = (FrameworkElement) frameworkElement.Parent;
                while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
                {
                    parent = (FrameworkElement)parent.Parent;
                }

                DependencyObject scope = FocusManager.GetFocusScope(frameworkElement);
                FocusManager.SetFocusedElement(scope, parent);

                Keyboard.ClearFocus();
            }
        }

        static void DoUpdateSource(object source)
        {
            DependencyProperty property =
                GetUpdatePropertySourceWhenEnterPressed(source as DependencyObject);

            if (property == null)
            {
                return;
            }

            UIElement elt = source as UIElement;

            if (elt == null)
            {
                return;
            }

            BindingExpression binding = BindingOperations.GetBindingExpression(elt, property);

            if (binding != null)
            {
                binding.UpdateSource();
            }
        }
    }
}
