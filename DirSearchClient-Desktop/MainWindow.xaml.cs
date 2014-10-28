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
            List<UserInfo> results = new List<UserInfo>();
            SearchResults.ItemsSource = results;            
        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            List<UserInfo> results = new List<UserInfo>();
            if (string.IsNullOrEmpty(SearchTermText.Text)) 
            {
                MessageBox.Show("Please enter a valid search term.");
                return;
            }

            User result = await DirectorySearcher.SearchByAlias(SearchTermText.Text, new AuthorizationParameters(PromptBehavior.Auto, this.Handle));
            if (result.error != null) 
            {
                results.Add(new UserInfo { Property = "Error! ", Value = result.error.message.value });
            }
            else if (result.value.Count == 0)
            {
                results.Add(new UserInfo { Property = "Not Found", Value = "User Not Found. Try Another Term." });
            }
            else
            {
                results.Add(new UserInfo { Property = "First Name: ", Value = result.value[0].givenName });
                results.Add(new UserInfo { Property = "Last Name: ", Value = result.value[0].surname });
                results.Add(new UserInfo { Property = "UPN: ", Value = result.value[0].userPrincipalName });
                results.Add(new UserInfo { Property = "Phone: ", Value = result.value[0].telephoneNumber == null ? "Not Listed" : result.value[0].telephoneNumber });
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

    public class UserInfo
    {
        public string Property { get; set; }
        public string Value { get; set; }
    }
}
