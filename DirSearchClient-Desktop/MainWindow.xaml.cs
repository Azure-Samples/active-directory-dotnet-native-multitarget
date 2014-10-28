using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirSearchClient_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWin32Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<User> results = new List<User>();
            SearchResults.ItemsSource = results;            
        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTermText.Text)) 
            {
                MessageBox.Show("Please enter a valid search term.");
                return;
            }

            List<User> results = await DirectorySearcher.SearchByAlias(SearchTermText.Text, new AuthorizationParameters(PromptBehavior.Auto, this.Handle));
            if (results.Count == 0)
            {
                StatusResult.Text = "User Not Found. Try Another Term.";
                StatusResult.Foreground = new SolidColorBrush(Colors.Black);
            }
            else if (results[0].error != null) 
            {
                StatusResult.Text = "Error! " + results[0].error;
                StatusResult.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                StatusResult.Text = "Success";
                StatusResult.Foreground = new SolidColorBrush(Colors.Green);
            }

            SearchResults.ItemsSource = results;
        }

        public IntPtr Handle
        {
            get {
                var interopHelper = new WindowInteropHelper(this);
                return interopHelper.Handle;
            }
        }
    }
}
