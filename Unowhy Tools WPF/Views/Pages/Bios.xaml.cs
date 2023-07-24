using Wpf.Ui.Common.Interfaces;
using Unowhy_Tools_WPF.ViewModels;
using System.Windows;

using Unowhy_Tools;
using System.Diagnostics;
using System;
using Microsoft.Win32;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.IO.Pipes;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security.Policy;

namespace Unowhy_Tools_WPF.Views.Pages;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
public partial class Bios : INavigableView<DashboardViewModel>
{
    UT.Data UTdata = new UT.Data();

    public DashboardViewModel ViewModel
    {
        get;
    }

    public async void GoForw(object sender, RoutedEventArgs e)
    {
        //UT.anim.BackBtnAnim(BackBTN);
        await Task.Delay(150);
        //UT.anim.TransitionBack(RootGrid);
        await Task.Delay(200);
        UT.NavigateTo(typeof(Dashboard));
    }

    public async void Init(object sender, EventArgs e)
    {
        applylang();
    }

    public async void InitAnim(object sender, RoutedEventArgs e)
    {
        

        await UT.DeployBack(typeof(Dashboard), RootGrid, RootBorder);
        UT.anim.BorderZoomOut(RootBorder);

        
    }

    public void applylang()
    {

    }

    public Bios(DashboardViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}