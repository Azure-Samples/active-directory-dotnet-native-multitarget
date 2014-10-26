using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace DirSearchClient_Android
{
    [Activity(Label = "DirSearchClient_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.searchButton);

            button.Click += async delegate {

                EditText searchTerm = FindViewById<EditText>(Resource.Id.searchTermText);
                string rez = await DirectorySearcher.SearchByAlias(searchTerm.Text, new AuthorizationParameters(this));
                TextView rezText = FindViewById<TextView>(Resource.Id.resultTextView);
                rezText.Text = rez;
                button.Text = string.Format("{0} clicks!", count++); 
            
            };
        }
    }
}

