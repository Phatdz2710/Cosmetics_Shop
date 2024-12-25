using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Cosmetics_Shop.ViewModels.UserControlViewModels;
using System.Windows.Input;
using Windows.UI.Popups;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Cosmetics_Shop.Views.Controls
{
    /// <summary>
    /// Product thumbnail user control
    /// </summary>
    public sealed partial class ProductThumbnailControl : UserControl
    {
        public ProductThumbnailViewModel ViewModel
        {
            get 
            {
                return (ProductThumbnailViewModel)GetValue(ViewModelProperty);
            }
            set 
            { 
                SetValue(ViewModelProperty, value);
                DataContext = value;
            }
        }


        // Sử dụng DependencyProperty để hỗ trợ binding
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(ProductThumbnailViewModel),
                                        typeof(ProductThumbnailControl),
                                        new PropertyMetadata(null));


        private Storyboard _mouseEnter;
        private Storyboard _mouseLeave;
        public ProductThumbnailControl()
        {
            this.InitializeComponent();
            this.Loaded += ProductThumbnailControl_Loaded;
        }

        private void ProductThumbnailControl_Loaded(object sender, RoutedEventArgs e)
        {
            _mouseEnter = new Storyboard();
            _mouseLeave = new Storyboard();

            var thumbailEnterAnimation_ScaleY = new DoubleAnimation()
            {
                To = 1,
                Duration = new Duration(TimeSpan.FromMilliseconds(100)),
                EasingFunction = new CubicEase()
            };
            Storyboard.SetTarget(thumbailEnterAnimation_ScaleY, HoverAnimationGrid);
            Storyboard.SetTargetProperty(thumbailEnterAnimation_ScaleY, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");

            var thumbailEnterAnimation_Opacity = new DoubleAnimation()
            {
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.2)),
                EasingFunction = new CubicEase()
            };
            Storyboard.SetTarget(thumbailEnterAnimation_Opacity, HoverAnimationGrid);
            Storyboard.SetTargetProperty(thumbailEnterAnimation_Opacity, "Opacity");

            var thumbailExitAnimation_ScaleY = new DoubleAnimation()
            {
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.15)),
            };
            Storyboard.SetTarget(thumbailExitAnimation_ScaleY, HoverAnimationGrid);
            Storyboard.SetTargetProperty(thumbailExitAnimation_ScaleY, "(UIElement.RenderTransform).(ScaleTransform.ScaleY)");

            var thumbailExitAnimation_Opacity = new DoubleAnimation()
            {
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.15)),
            };
            Storyboard.SetTarget(thumbailExitAnimation_Opacity, HoverAnimationGrid);
            Storyboard.SetTargetProperty(thumbailExitAnimation_Opacity, "Opacity");

            _mouseEnter.Children.Add(thumbailEnterAnimation_ScaleY);
            _mouseEnter.Children.Add(thumbailEnterAnimation_Opacity);
            _mouseLeave.Children.Add(thumbailExitAnimation_ScaleY);
            _mouseLeave.Children.Add(thumbailExitAnimation_Opacity);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ICommand command = ViewModel.BuyButtonCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
                _mouseLeave?.Begin();
            }

        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _mouseEnter?.Begin();
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _mouseLeave?.Begin();
        }
    }
}
