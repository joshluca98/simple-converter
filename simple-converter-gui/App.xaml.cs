using System.Globalization;
using System.Windows;

namespace simple_converter_gui
{
   
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
        }
    }

}
