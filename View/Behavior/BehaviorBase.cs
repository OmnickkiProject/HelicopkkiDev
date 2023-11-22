using System;
using System.Windows;
using HelicopkkiDev.View;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace HelicopkkiDev.View.Behavior
{
    class BehaviorBase<T> : Behavior<T> where T : FrameworkElement
    {
        protected static void OpenSettingWindow()
        {
            if (SettingWindow.Instance == null)
            {
                SettingWindow.Instance = new SettingWindow();
                SettingWindow.Instance.Show();
            }
            else if (SettingWindow.Instance.WindowState == WindowState.Minimized)
            {
                SettingWindow.Instance.WindowState = WindowState.Normal;
            }
            else
            {
                SettingWindow.Instance.Activate();
            }
            CharacterWindow.Instance.Activate();
        }

        protected static void CloseSettingWindow()
        {
            SettingWindow.Instance.Close();
            SettingWindow.Instance = null;
        }

        /*
         * Original Code
         * https://kojaedoo.tistory.com/538
         * modified by HGS
         */
        protected static TElement FindVisualParentByName<TElement>(FrameworkElement _Control, string _FindControlName) where TElement : FrameworkElement
        {
            TElement t = null;
            DependencyObject obj = VisualTreeHelper.GetParent(_Control);     

            for (int i = 0; i < 100; i++) //최대 100개의 컨트롤 까지 검색
            {
                string currentName = obj.GetValue(FrameworkElement.NameProperty) as string;
                if (currentName == _FindControlName)
                {
                    t = obj as TElement;
                    break;
                }
                obj = VisualTreeHelper.GetParent(obj);
                if (obj == null)
                {
                    break;
                }
            }

            return t;

        }

        /*
         * Original Code
         * https://kojaedoo.tistory.com/538
         * modified by HGS
         */
        protected static TElement FindVisualChildByName<TElement>(DependencyObject _Control, string _FindControlName) where TElement : DependencyObject
        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(_Control); i++)
            {
                var child = VisualTreeHelper.GetChild(_Control, i);
                string controlName = child.GetValue(FrameworkElement.NameProperty) as string;
                if (controlName == _FindControlName)
                {
                    return child as TElement;
                }
                else
                {
                    TElement result = FindVisualChildByName<TElement>(child, _FindControlName);
                    if (result != null)
                    {
                        return result;
                    }
                }

            }
            return null;
        }
    }
}
