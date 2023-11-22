/*
 * Original Code
 * https://stackoverflow.com/questions/1083224/pushing-read-only-gui-properties-back-into-viewmodel/3667609#3667609
 * modified by HGS
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelicopkkiDev.View.AttachedBehavior
{
    class DataPiping
    {
        #region DataPipes (Attached DependencyProperty)

        public static readonly DependencyProperty DataPipesProperty =
            DependencyProperty.RegisterAttached("DataPipes",
            typeof(DataPipeCollection),
            typeof(DataPiping),
            new UIPropertyMetadata(null));

        public static void SetDataPipes(DependencyObject o, DataPipeCollection value)
        {
            o.SetValue(DataPipesProperty, value);
        }

        public static DataPipeCollection GetDataPipes(DependencyObject o)
        {
            return (DataPipeCollection)o.GetValue(DataPipesProperty);
        }

        #endregion
    }

    class DataPipeCollection : FreezableCollection<DataPipe>
    {

    }

    /// <summary>
    /// 기존 바인딩으로 불가능한 Readonly Source와 Target과의 바인딩을 도와줌
    /// </summary>
    class DataPipe : Freezable
    {
        #region Source (DependencyProperty)

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(DataPipe),
            new PropertyMetadata(null, new PropertyChangedCallback(OnSourceChanged)));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DataPipe)d).OnSourceChanged(e);
        }

        protected virtual void OnSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Target = e.NewValue;
        }

        #endregion

        #region Target (DependencyProperty)

        public object Target
        {
            get { return (object)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(DataPipe),
            new PropertyMetadata(null));

        #endregion

        protected override Freezable CreateInstanceCore()
        {
            return new DataPipe();
        }
    }
}
