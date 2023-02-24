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

            lab1.Text = UT.GetLang("wait");
            text.Text = UT.GetLang("wait");

            Visibility = Visibility.Hidden;
        }

        public async Task Show()
        {
            Visibility = Visibility.Visible;
        }

        public async Task Hide()
        {
            Visibility = Visibility.Hidden;
        }
    }
}
