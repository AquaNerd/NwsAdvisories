using System;
using System.Windows;
using Caliburn.Micro;

namespace AdvisoryViewer {
    public sealed class AppWindowManager : WindowManager {
        static readonly ResourceDictionary[] resources;
        static AppWindowManager() {
            resources = new ResourceDictionary[] {
                new ResourceDictionary
                { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml", UriKind.RelativeOrAbsolute) },
                new ResourceDictionary
                { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml", UriKind.RelativeOrAbsolute) },
                new ResourceDictionary
                { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml", UriKind.RelativeOrAbsolute) },
                new ResourceDictionary
                { Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedTabControl.xaml", UriKind.RelativeOrAbsolute) },
                new ResourceDictionary
                { Source = new Uri("pack://application:,,,/ViewerApp;component/Resources/Icons.xaml") }
            };
        }

        protected override Window EnsureWindow(object model, object view, bool isDialog) {
            MainWindowContainer window = view as MainWindowContainer;
            if (window == null) {
                window = new MainWindowContainer() {
                    Content = view,
                    SizeToContent = SizeToContent.Manual,
                    ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                    MinHeight = 150,
                    MinWidth = 500  
                };
                window.Height = window.MinHeight;
                window.Width = window.MinWidth;

                foreach (ResourceDictionary resourceDictionary in resources) {
                    window.Resources.MergedDictionaries.Add(resourceDictionary);
                }
                window.SetValue(View.IsGeneratedProperty, true);
                Window owner = this.InferOwnerOf(window);
                if (owner != null) {
                    window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    window.Owner = owner;
                } else {
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            } else {
                Window owner2 = this.InferOwnerOf(window);
                if (owner2 != null && isDialog) {
                    window.Owner = owner2;
                }
            }
            return window;
        }
    }
}