using System.Windows;

namespace Sootd
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        private Config config;

        public Categories(Config config)
        {
            InitializeComponent();
            comboBox.ItemsSource = config.Categories;
            comboBox.SelectedItem = config.SelectedCategory;
            this.config = config;
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            config.SelectedCategory = comboBox.SelectedItem.ToString();
            Close();
        }
    }
}
