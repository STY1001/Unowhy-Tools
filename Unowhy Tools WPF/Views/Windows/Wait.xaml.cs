using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Unowhy_Tools;

namespace Unowhy_Tools_WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for Wait.xaml
    /// </summary>
    public partial class Wait : UserControl
    {
        public Wait()
        {
            InitializeComponent();

            applylang();

            Visibility = Visibility.Collapsed;
        }

        public async void applylang()
        {
            try
            {
                lab1.Text = await UT.GetLang("wait");
                text.Text = await UT.GetLang("wait");
            }
            catch { }
        }

        public bool IsOpen = false;

        public async Task Show(string title, string img)
        {
            if (IsOpen)
            {
                if (img != "null")
                {
                    icon.Source = UT.GetImgSource(img);
                }
                text.Text = title;
            }
            else
            {
                IsOpen = true;

                if (img != "null")
                {
                    icon.Source = UT.GetImgSource(img);
                }
                text.Text = title;

                Visibility = Visibility.Visible;
                var fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.15),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                var zoomAnimation1 = new DoubleAnimation
                {
                    From = 1.1,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.25),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                var zoomAnimation2 = new DoubleAnimation
                {
                    From = 1.1,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.25),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };

                Storyboard.SetTarget(fadeInAnimation, RootGrid);
                Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(OpacityProperty));

                Storyboard.SetTarget(zoomAnimation1, Border1);
                Storyboard.SetTarget(zoomAnimation2, Border1);
                Storyboard.SetTargetProperty(zoomAnimation1, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
                Storyboard.SetTargetProperty(zoomAnimation2, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

                var storyboard = new Storyboard();
                storyboard.Children.Add(fadeInAnimation);
                storyboard.Children.Add(zoomAnimation1);
                storyboard.Children.Add(zoomAnimation2);

                storyboard.Begin();
            }
        }

        public async Task Hide()
        {
            IsOpen = false;
            var fadeInAnimation2 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            var zoomAnimation12 = new DoubleAnimation
            {
                From = 1,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.15),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            var zoomAnimation22 = new DoubleAnimation
            {
                From = 1,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.15),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            Storyboard.SetTarget(fadeInAnimation2, RootGrid);
            Storyboard.SetTargetProperty(fadeInAnimation2, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(zoomAnimation12, Border1);
            Storyboard.SetTarget(zoomAnimation22, Border1);
            Storyboard.SetTargetProperty(zoomAnimation12, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(zoomAnimation22, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation2);
            storyboard.Children.Add(zoomAnimation12);
            storyboard.Children.Add(zoomAnimation22);

            storyboard.Completed += RealHide;

            storyboard.Begin();
        }

        public void RealHide(object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
    }
}
