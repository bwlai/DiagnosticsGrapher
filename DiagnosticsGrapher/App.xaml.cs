using System.Windows;
using DiagnosticsGrapher.Data;

namespace DiagnosticsGrapher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Grapher.StartUp();
        }
    }
}
