using System.Windows;
using System.Windows.Input;

namespace FT.Client.Behaviors
{
    public class MouseBehaviour
    {
        public static readonly DependencyProperty MouseUpCommandProperty = DependencyProperty.RegisterAttached("MouseUpCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseUpCommandChanged)));
        public static readonly DependencyProperty MouseEnterCommandProperty = DependencyProperty.RegisterAttached("MouseEnterCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseEnterCommandChanged)));
        public static readonly DependencyProperty MouseLeaveCommandProperty = DependencyProperty.RegisterAttached("MouseLeaveCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseLeaveCommandChanged)));

        #region MouseUp
        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.MouseUp += new MouseButtonEventHandler(element_MouseUp);
        }

        static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseUpCommand(element);

            command.Execute(e);
        }

        public static void SetMouseUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseUpCommandProperty, value);
        }

        public static ICommand GetMouseUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseUpCommandProperty);
        }
        #endregion

        #region MouseEnter
        private static void MouseEnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.MouseEnter += new MouseEventHandler(element_MouseEnter);
        }

        static void element_MouseEnter(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseEnterCommand(element);

            command.Execute(e);
        }

        public static void SetMouseEnterCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseEnterCommandProperty, value);
        }

        public static ICommand GetMouseEnterCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseEnterCommandProperty);
        }
        #endregion

        #region MouseLeave
        private static void MouseLeaveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.MouseLeave += new MouseEventHandler(element_MouseLeave);
        }

        static void element_MouseLeave(object sender, MouseEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseLeaveCommand(element);

            command.Execute(e);
        }

        public static void SetMouseLeaveCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseLeaveCommandProperty, value);
        }

        public static ICommand GetMouseLeaveCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseLeaveCommandProperty);
        }
        #endregion
    }
}
