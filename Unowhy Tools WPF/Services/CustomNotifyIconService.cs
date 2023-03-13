using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Services;

namespace Unowhy_Tools_WPF.Services;

public class CustomNotifyIconService : NotifyIconService
{
    public CustomNotifyIconService()
    {
        TooltipText = "Unowhy Tools";

        // If this icon is not defined, the application icon will be used.
        Icon = BitmapFrame.Create(new Uri("pack://application:,,,/Resources/UT.png", UriKind.Absolute));
        /*
        ContextMenu = new ContextMenu
        {
            FontSize = 14d,
            Items =
            {
                new Wpf.Ui.Controls.MenuItem
                {
                    Header = "Home",
                    SymbolIcon = SymbolRegular.Home28,
                    Tag = "home"
                },
                new Wpf.Ui.Controls.MenuItem
                {
                    Header = "Save",
                    SymbolIcon = SymbolRegular.Save28,
                    Tag = "save"
                },
                new Wpf.Ui.Controls.MenuItem
                {
                    Header = "Open",
                    SymbolIcon = SymbolRegular.Folder28,
                    Tag = "open"
                },
                new Separator(),
                new Wpf.Ui.Controls.MenuItem
                {
                    Header = "Reload",
                    SymbolIcon = SymbolRegular.ArrowClockwise28,
                    Tag = "reload"
                },
            }
        };

        foreach (var singleContextMenuItem in ContextMenu.Items)
            if (singleContextMenuItem is MenuItem)
                ((MenuItem)singleContextMenuItem).Click += OnMenuItemClick;
        */
    }

    protected override void OnLeftClick()
    {
        System.Diagnostics.Debug.WriteLine($"DEBUG | WPF UI Tray event: {nameof(OnLeftClick)}", "Unowhy_Tools_WPF");
    }

    private void OnMenuItemClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Wpf.Ui.Controls.MenuItem menuItem)
            return;

        System.Diagnostics.Debug.WriteLine($"DEBUG | WPF UI Tray clicked: {menuItem.Tag}", "Unowhy_Tools_WPF");
    }
}
