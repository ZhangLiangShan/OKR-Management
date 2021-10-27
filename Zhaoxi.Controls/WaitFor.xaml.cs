using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zhaoxi.Controls
{
    /// <summary>
    /// WaitFor.xaml 的交互逻辑
    /// </summary>
    public partial class WaitFor : UserControl
    {
        public WaitFor()
        {
            InitializeComponent();

            this.Loaded += WaitFor_Loaded;
        }

        private void WaitFor_Loaded(object sender, RoutedEventArgs e)
        {
            this.RunAnimation();
        }

        private void RunAnimation()
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(1000));
            DoubleAnimation daX = new DoubleAnimation();
            daX.To = 1.8;
            daX.Duration = duration;
            daX.RepeatBehavior = RepeatBehavior.Forever;
            daX.AutoReverse = true;

            DoubleAnimation daY = new DoubleAnimation();
            daY.To = 1.8;
            daY.Duration = duration;
            daY.RepeatBehavior = RepeatBehavior.Forever;
            daY.AutoReverse = true;

            //Task.Run(new Action(async () =>
            //{
            //    this.Dispatcher.Invoke(new Action(() =>
            //    {
            //        this.stOrange.BeginAnimation(ScaleTransform.ScaleXProperty, daX);
            //        this.stOrange.BeginAnimation(ScaleTransform.ScaleYProperty, daY);
            //    }));

            //    await Task.Delay(300);

            //    this.Dispatcher.Invoke(new Action(() =>
            //    {
            //        this.stGreen.BeginAnimation(ScaleTransform.ScaleXProperty, daX);
            //        this.stGreen.BeginAnimation(ScaleTransform.ScaleYProperty, daY);
            //    }));

            //    await Task.Delay(300);

            //    this.Dispatcher.Invoke(new Action(() =>
            //    {
            //        this.stRed.BeginAnimation(ScaleTransform.ScaleXProperty, daX);
            //        this.stRed.BeginAnimation(ScaleTransform.ScaleYProperty, daY);
            //    }));

            //    await Task.Delay(300);

            //    this.Dispatcher.Invoke(new Action(() =>
            //    {
            //        this.stCustom.BeginAnimation(ScaleTransform.ScaleXProperty, daX);
            //        this.stCustom.BeginAnimation(ScaleTransform.ScaleYProperty, daY);
            //    }));
            //}));


            // 关键帧动画
            string textStr = "Hello world....";
            StringAnimationUsingKeyFrames keyFrames = new StringAnimationUsingKeyFrames();
            string txt = "";
            for (int i = 0; i < textStr.Length; i++)
            {
                keyFrames.KeyFrames.Add(new DiscreteStringKeyFrame { Value = txt, KeyTime = TimeSpan.FromMilliseconds(200 * (i)) });
                txt += textStr[i].ToString();
            }

            keyFrames.RepeatBehavior = RepeatBehavior.Forever;
            this.text.BeginAnimation(TextBlock.TextProperty, keyFrames);
        }
    }
}
