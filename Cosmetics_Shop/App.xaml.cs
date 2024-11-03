using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Cosmetics_Shop.Views;
using Microsoft.Extensions.DependencyInjection;
using Cosmetics_Shop.Services;
using Cosmetics_Shop.ViewModels;
using Cosmetics_Shop.Views.Pages;
using Cosmetics_Shop.Services.Interfaces;
using Cosmetics_Shop.Models.DataService;
using Cosmetics_Shop.Models;
using Cosmetics_Shop.ViewModels.PageViewModels;
using Cosmetics_Shop.ViewModels.UserControlViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        /// 
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Cấu hình dịch vụ
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Activate Main Window
            m_window = new LoginWindow();
            m_window.Activate();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // Add Singleton
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IDao, MockDao>();
            services.AddSingleton<UserSession>();

            // Add Transient
            services.AddTransient<LoginViewModel>();
            services.AddTransient<UserViewModel>();
            services.AddTransient<AdminViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<PurchasePageViewModel>();
            services.AddTransient<DashboardPageViewModel>();
            services.AddTransient<ProductThumbnailViewModel>();
            
            

            // Add Scoped
            ServiceProvider = services.BuildServiceProvider();
        }

        private Window m_window;
    }
}

