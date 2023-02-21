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

namespace Unowhy_Tools_WPF.Views.Windows
{
    /// <summary>
    /// Interaction logic for DialogQ.xaml
    /// </summary>
    public partial class DialogQ : System.Windows.Controls.UserControl
    {
        public DialogQ()
        {
            InitializeComponent();
            Visibility = Visibility.Hidden;
        }

        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;

        public void SetParent(UIElement parent)
        {
            _parent = parent;
        }

        #region Message

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                "Message", typeof(string), typeof(DialogQ), new UIPropertyMetadata(string.Empty));

        #endregion

        public bool ShowDialog(string message)
        {
            Message = message;
            Visibility = Visibility.Visible;

            _parent.IsEnabled = true;

            _hideRequest = false;
            while (!_hideRequest)
            {
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                this.Dispatcher.Invoke(
                    DispatcherPriority.Background,
                    new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return _result;
        }

        private void HideHandlerDialog()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _parent.IsEnabled = true;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _result = true;
            HideHandlerDialog();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            _result = false;
            HideHandlerDialog();
        }
    }
}
