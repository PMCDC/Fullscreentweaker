using MaterialDesignThemes.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace FT.Client.Views.Common
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : System.Windows.Controls.UserControl
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

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(@"Based on the Fullscreenizer app originaly made by Kostas ""Bad Sector"" Michalopoulos");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Instructions:");
            stringBuilder.AppendLine(@"1. Open the game you want to force in ""Borderless Fullscreen"".");
            stringBuilder.AppendLine("2. Set the game into windowed mode.");
            stringBuilder.AppendLine("3. Hit refresh to make the game appear into the list.");
            stringBuilder.AppendLine(@"4. Hit the Fullscreenize button to make the game ""Borderless Fullscreen"".");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("Note:");
            stringBuilder.AppendLine("Some games may require that you press Fullscreenize more than once in order to make it work.");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine(@$"//Version {Core.Constants.FullscreenTweaker.VERSION}");
            stringBuilder.AppendLine(@"//Coded by Pierre-Marc Coursol de Carufel");

            System.Windows.Forms.MessageBox.Show(stringBuilder.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
