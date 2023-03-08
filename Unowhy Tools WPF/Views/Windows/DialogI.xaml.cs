using System.Windows.Controls;
using Unowhy_Tools_WPF.ViewModels;

using Unowhy_Tools;
using System.Windows.Forms;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System;
using System.Windows.Media;

namespace Unowhy_Tools_WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for DialogQ.xaml
    /// </summary>
    public partial class DialogI : System.Windows.Controls.UserControl
    {
        public DialogI()
        {
            InitializeComponent();
            Visibility = Visibility.Collapsed;
        }

        private bool _hideRequest = false;
        private bool _result = false;


        public bool ShowDialog(string message, BitmapImage image)
        {
            icon.Source = image;
            text.Text = message;
            Visibility = Visibility.Visible;
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };

            var zoomAnimation1 = new DoubleAnimation
            {
                From = 0.50,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.15),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };
            
            var zoomAnimation2 = new DoubleAnimation
            {
                From = 0.50,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.15),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(fadeInAnimation, Border1);
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

            _hideRequest = false;
            while (!_hideRequest)
            {
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return _result;
        }

        private void HideDialog()
        {
            var fadeInAnimation2 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.1),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            var zoomAnimation12 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            var zoomAnimation22 = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(fadeInAnimation2, Border1);
            Storyboard.SetTargetProperty(fadeInAnimation2, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(zoomAnimation12, Border1);
            Storyboard.SetTarget(zoomAnimation22, Border1);
            Storyboard.SetTargetProperty(zoomAnimation12, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));
            Storyboard.SetTargetProperty(zoomAnimation22, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation2);
            storyboard.Children.Add(zoomAnimation12);
            storyboard.Children.Add(zoomAnimation22);

            storyboard.Completed += RealHideDialog;

            storyboard.Begin();
        }

        private void RealHideDialog(object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            _hideRequest = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            HideDialog();
            _result = true;
        }
    }
}
