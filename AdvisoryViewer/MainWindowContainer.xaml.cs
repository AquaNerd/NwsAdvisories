using System;
using System.Linq;
using System.Windows;
using MahApps.Metro;

namespace AdvisoryViewer {
    public partial class MainWindowContainer {
        private Theme currentTheme = Theme.Dark;
        private Accent currentAccent = null;

        public MainWindowContainer() {
            InitializeComponent();
        }

        private void ChangeAccent(string accentName) {
            this.currentAccent = ThemeManager.DefaultAccents.First(x => x.Name == accentName);

            ThemeManager.ChangeTheme(this, this.currentAccent, this.currentTheme);
        }

        private void ThemeLight(object sender, System.Windows.RoutedEventArgs e) {
            this.currentTheme = Theme.Light;
            ThemeManager.ChangeTheme(this, this.currentAccent, Theme.Light);
        }

        private void ThemeDark(object sender, System.Windows.RoutedEventArgs e) {
            this.currentTheme = Theme.Dark;
            ThemeManager.ChangeTheme(this, this.currentAccent, Theme.Dark);
        }

        private void AccentVS(object sender, RoutedEventArgs e) {
            this.currentAccent = new Accent("VS", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/VS/Colours.xaml", UriKind.RelativeOrAbsolute));

            ThemeManager.ChangeTheme(this, this.currentAccent, this.currentTheme);

            tglTheme.IsEnabled = false;
            tglTheme.IsChecked = true;
        }

        private void AccentRed(object sender, RoutedEventArgs e) {
            this.ChangeAccent("Red");
            tglTheme.IsEnabled = true;
        }

        private void AccentGreen(object sender, RoutedEventArgs e) {
            this.ChangeAccent("Green");
            tglTheme.IsEnabled = true;
        }

        private void AccentBlue(object sender, RoutedEventArgs e) {
            this.ChangeAccent("Blue");
            tglTheme.IsEnabled = true;
        }

        private void AccentPurple(object sender, RoutedEventArgs e) {
            this.ChangeAccent("Purple");
            tglTheme.IsEnabled = true;
        }

        private void AccentOrange(object sender, RoutedEventArgs e) {
            this.ChangeAccent("Orange");
            tglTheme.IsEnabled = true;
        }
    }
}