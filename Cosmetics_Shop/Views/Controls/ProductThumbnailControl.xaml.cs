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
        /// <summary>
        /// Gets or sets the ViewModel for the product thumbnail.
        /// </summary>
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

        /// <summary>
        /// DependencyProperty for ViewModel to support binding.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel",
                                        typeof(ProductThumbnailViewModel),
                                        typeof(ProductThumbnailControl),
                                        new PropertyMetadata(null));

        private Storyboard _mouseEnter;
        private Storyboard _mouseLeave;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductThumbnailControl"/> class.
        /// </summary>
        public ProductThumbnailControl()
        {
            this.InitializeComponent();
            this.Loaded += ProductThumbnailControl_Loaded;
        }

        /// <summary>
        /// Sets up animations for mouse enter and leave events.
        /// </summary>
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

        /// <summary>
        /// Handles the tap event on the product thumbnail, executing the buy button command.
        /// </summary>
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ICommand command = ViewModel.BuyButtonCommand;
            if (command.CanExecute(null))
            {
                command.Execute(null);
                _mouseLeave?.Begin();
            }
        }

        /// <summary>
        /// Handles the pointer entered event on the product thumbnail, starting the mouse enter animation.
        /// </summary>
        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _mouseEnter?.Begin();
        }

        /// <summary>
        /// Handles the pointer exited event on the product thumbnail, starting the mouse leave animation.
        /// </summary>
        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _mouseLeave?.Begin();
        }
    }
}