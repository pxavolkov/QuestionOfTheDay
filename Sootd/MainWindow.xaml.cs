using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SoApi;

namespace Sootd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Config config = new Config();
        private Categories categoryWindow;
        private Categories CategoryWindow => categoryWindow ?? (categoryWindow = new Categories(config));
        private StackOverflow api = new StackOverflow();
        private Question currentQuestion;

        public MainWindow()
        {
            InitializeComponent();
            currentQuestion = api.GetNext(config.SelectedCategory);
            Update();
            //DataContext = currentQuestion;
        }

        private void Update()
        {
            title.Text = currentQuestion.Title;
            hyperlink.NavigateUri = new Uri(currentQuestion.Url);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenBrowser(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(currentQuestion.Url);
        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            currentQuestion = api.GetNext(config.SelectedCategory);
            Update();
        }

        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            CategoryWindow.ShowDialog();
            NextQuestion(null, null);
        }
    }
}
