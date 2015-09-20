using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Threading;
using SoApi;

namespace Sootd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Config config = new Config();
        private StackOverflow api;
        private List<Question> currentQuestions;
        private NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            api = new StackOverflow(config);
            currentQuestions = api.GetNext();
            Update();
            //DataContext = currentQuestion;

            notifyIcon = new NotifyIcon
            {
                Icon = new Icon("icon.ico"),
                Visible = true
            };
            notifyIcon.DoubleClick += delegate
                {
                    Show();
                    WindowState = WindowState.Normal;
                };
            
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(1, 0, 0);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            NextQuestion(null, null);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }

            base.OnStateChanged(e);
        }

        private void Update()
        {
            if (currentQuestions.Count > 2)
            {
                title1.Text = "1. " + currentQuestions[0].Title;
                hyperlink1.NavigateUri = new Uri(currentQuestions[0].Url);
                title2.Text = "2. " + currentQuestions[1].Title;
                hyperlink2.NavigateUri = new Uri(currentQuestions[1].Url);
                title3.Text = "3. " + currentQuestions[2].Title;
                hyperlink3.NavigateUri = new Uri(currentQuestions[2].Url);
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenBrowser(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            Process.Start(hyperlink?.NavigateUri.ToString() ?? hyperlink1.NavigateUri.ToString());
        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            currentQuestions = api.GetNext();
            Update();
            notifyIcon.ShowBalloonTip(5000, null, currentQuestions[0].Title, ToolTipIcon.None);

        }

        private void SelectCategory(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new Categories(config);
            var showDialog = categoryWindow.ShowDialog();
            if (showDialog != null && showDialog.Value)
            {
                api.LoadQuestions();
                NextQuestion(null, null);
            }
        }

        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}

