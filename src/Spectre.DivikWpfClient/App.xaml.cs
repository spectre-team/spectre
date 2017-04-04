using System.Windows;
using Spectre.DivikWpfClient.ViewModel;

namespace Spectre.DivikWpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Handles the DispatcherUnhandledException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var dataContext = (MainPageVm) Resources["MainPageVm"];
            dataContext.HandleExceptionCommand.Execute(e.Exception.Message);
        }
    }
}
