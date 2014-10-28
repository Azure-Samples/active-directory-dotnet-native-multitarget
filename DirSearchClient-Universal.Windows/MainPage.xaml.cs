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

namespace DirSearchClient_Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private async void Search(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTermText.Text))
            {
                MessageDialog dialog = new MessageDialog("Please enter a valid search term.");
                await dialog.ShowAsync();
                return;
            }

            List<Value> results = new List<Value>();
            User result = await DirectorySearcher.SearchByAlias(SearchTermText.Text, new AuthorizationParameters(PromptBehavior.Auto, false));
            if (result.error != null)
            {
                StatusResult.Text = "Error! " + result.error.message.value;
                StatusResult.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                results.Add(new Value());
            }
            else if (result.value.Count == 0)
            {
                StatusResult.Text = "User Not Found. Try Another Term.";
                StatusResult.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                results.Add(new Value());
            }
            else
            {
                StatusResult.Text = "Success";
                StatusResult.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                result.value[0].telephoneNumber = result.value[0].telephoneNumber == null ? "Not Listed." : result.value[0].telephoneNumber;
                results.Add(result.value[0]);
            }

            SearchResults.ItemsSource = results;
        }
    }
}
