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
using Cosmetics_Shop.DBModels;
using Microsoft.EntityFrameworkCore;
using Cosmetics_Shop.Services.EventAggregatorMessages;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Cosmetics_Shop
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            m_window = new LoginWindow();
            m_window.Activate();

            var eventAggregator = ServiceProvider.GetService<IEventAggregator>();
            eventAggregator.Subscribe<LogoutMessage>((message) =>
            {
                m_window = new LoginWindow();
                m_window.Activate();
            });
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IDao, SqlDao>();
            services.AddSingleton<IFilePickerService, FilePickerService>();
            services.AddSingleton<UserSession>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<UserViewModel>();
            services.AddTransient<AdminViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<PurchasePageViewModel>();
            services.AddTransient<DashboardPageViewModel>();
            services.AddTransient<ProductThumbnailViewModel>();
            services.AddTransient<ProductDetailViewModel>();
            services.AddTransient<AccountViewModel>();
            services.AddTransient<CartPageViewModel>();
            services.AddTransient<CartThumbnailViewModel>();
            services.AddTransient<ReviewThumbnailViewModel>();
            services.AddTransient<PaymentProductThumbnailViewModel>();
            services.AddTransient<PaymentPageViewModel>();

            var basePath = AppContext.BaseDirectory;
            var jsonFilePath = System.IO.Path.Combine(basePath, "appsettings.json");
            var jsonContent = File.ReadAllText(jsonFilePath);
            var rootNode = JsonNode.Parse(jsonContent);
            var connectionString = rootNode["ConnectionStrings"]["DefaultConnection"].ToString();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

            ServiceProvider = services.BuildServiceProvider();
        }

        private Window m_window;
    }
}
