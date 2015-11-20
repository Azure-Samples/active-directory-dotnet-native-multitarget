using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DirSearchClient_WindowsStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<User> results = new List<User>();
            results.Add(new User());
            SearchResults.ItemsSource = results;

        }

        private async void Search(object sender, RoutedEventArgs e)
        {            
            if (string.IsNullOrEmpty(SearchTermText.Text))
            {
                MessageDialog dialog = new MessageDialog("Please enter a valid search term.");
                await dialog.ShowAsync();
                return;
            }

            List<User> results = await DirectorySearcher.SearchByAlias(SearchTermText.Text, new PlatformParameters(PromptBehavior.Auto, false));
            if (results.Count == 0)
            {
                StatusResult.Text = "User Not Found. Try Another Term.";
                StatusResult.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                results.Add(new User());
            }
            else if (results[0].error != null)
            {
                StatusResult.Text = "Error! " + results[0].error;
                StatusResult.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                StatusResult.Text = "Success";
                StatusResult.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            }

            SearchResults.ItemsSource = results;
        }
    }    
}
