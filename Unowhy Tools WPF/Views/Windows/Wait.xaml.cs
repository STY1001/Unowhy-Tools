using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            Visibility = Visibility.Hidden;
        }

        private bool _hideRequest = false;
        private bool _result = false;
        private UIElement _parent;

        public void SetParent(UIElement parent)
        {
            _parent = parent;
        }


        public void Show()
        {
            Visibility = Visibility.Visible;

            _parent.IsEnabled = true;

            _hideRequest = false;
        }

        public void Hide()
        {
            _hideRequest = true;
            Visibility = Visibility.Hidden;
            _parent.IsEnabled = true;
        }
    }
}
