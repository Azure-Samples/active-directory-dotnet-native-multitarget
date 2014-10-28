using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace DirSearchClient_Universal
{
    static class UnivDirectoryHelper
    {
        public static async Task Search(object sender, RoutedEventArgs e, GridView SearchResults, TextBox SearchTermText, TextBlock StatusResult, IAuthorizationParameters parent)
        {
            if (string.IsNullOrEmpty(SearchTermText.Text))
            {
                MessageDialog dialog = new MessageDialog("Please enter a valid search term.");
                await dialog.ShowAsync();
                return;
            }

            List<User> results = await DirectorySearcherLib.DirectorySearcher.SearchByAlias(SearchTermText.Text, parent);
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
