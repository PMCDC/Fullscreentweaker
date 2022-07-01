using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FT.Client.Views.Common
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var window = GetWindow();
            window.Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaximizeAndUnmaximizeWindow();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            var window = GetWindow();
            window.WindowState = WindowState.Minimized;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var window = GetWindow();
            window.DragMove();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MaximizeAndUnmaximizeWindow();
        }

        private Window GetWindow()
        {
            return Window.GetWindow(this);
        }

        private void MaximizeAndUnmaximizeWindow()
        {
            var window = GetWindow();
            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            MaximizeButtonIcon.Kind = window.WindowState == WindowState.Maximized ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
        }
    }
}
