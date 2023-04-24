using System.ComponentModel;
using System.Windows;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace TaxiApp.WindowsApp
{
    public sealed class ElementScope
    {
        private readonly FrameworkElement _element;
        private readonly IServiceScope _scope;

        public ElementScope(FrameworkElement element)
        {
            _element = element;

            if (!DesignerProperties.GetIsInDesignMode(_element))
                _scope = App.Current.ServiceProvider.CreateScope();

            _element.Loaded += OnLoaded;
            _element.Unloaded += OnUnloaded;
        }

        ~ElementScope()
        {
            _scope?.Dispose();
        }

        public void AddDataContext<T>()
        {
            AddDataContext<T>(null);
        }

        public void AddDataContext<T>(Action<T> initialization)
        {
            if (DesignerProperties.GetIsInDesignMode(_element))
                return;

            var dataContext = _scope.ServiceProvider.GetRequiredService<T>();

            initialization?.Invoke(dataContext);

            _element.DataContext = dataContext;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(_element))
                return;
            
            //_scope.SetScope(_element);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(_element))
                return;

            //_scope.RemoveScope();
        }
    }
}
