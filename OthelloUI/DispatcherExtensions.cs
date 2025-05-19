using System.Windows;

namespace UI
{
    public static class DispatcherExtensions
    {
        public static void RunOnUIThread(this Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
