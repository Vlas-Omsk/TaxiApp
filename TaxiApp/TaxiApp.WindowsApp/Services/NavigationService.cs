using System;
using System.Collections.Generic;
using TaxiApp.WindowsApp.Controls;

namespace TaxiApp.WindowsApp.Services
{
    internal sealed class NavigationService
    {
        private readonly Stack<View> _history = new();

        public View CurrentView => _history.Peek();

        public void NavigateTo(View view)
        {
            _history.Push(view);

            CurrentViewChanged?.Invoke(this, EventArgs.Empty);
        }

        public void NavigateBack()
        {
            _history.Pop();

            CurrentViewChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CurrentViewChanged;
    }
}
