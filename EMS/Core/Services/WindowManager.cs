using EMS.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EMS
{
    public static class WindowManager
    {
        private static Stack<Window> _windowStack = new Stack<Window>();

        // Push a new window and hide the current top
        public static void Open(Window parent, Window child)
        {
            if (parent != null && !_windowStack.Contains(parent))
                _windowStack.Push(parent); // push parent first

            _windowStack.Push(child);
            child.Closed += Child_Closed!;

            if (parent != null)
                parent.Hide();

            child.Show();
        }

        private static void Child_Closed(object sender, System.EventArgs e)
        {
            var closedWindow = sender as Window;
            if (_windowStack.Count == 0) return;

            if (_windowStack.Peek() == closedWindow)
                _windowStack.Pop();

            // Show the previous window if it exists
            if (_windowStack.Count > 0)
            {
                var previous = _windowStack.Peek();
                if (!previous.IsVisible)
                    previous.Show();
            }
        }

        // Close all windows except LoginWindow
        public static void CloseAll()
        {
            while (_windowStack.Count > 0)
            {
                var win = _windowStack.Pop();
                if (win is LoginWindow)
                {
                    _windowStack.Push(win);
                    break;
                }

                win.Closed -= Child_Closed!;
                win.Close();
            }
        }
    }
}
