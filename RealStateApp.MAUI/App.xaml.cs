using RealStateApp.MAUI.Pages;

namespace RealStateApp.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXdcd3RXQ2hfVUJ+XEo=");

            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
