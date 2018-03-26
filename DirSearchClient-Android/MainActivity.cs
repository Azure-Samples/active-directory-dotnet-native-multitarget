using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;

namespace DirSearchClient_Android
{
    [Activity(Label = "DirSearchClient_Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
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

                EditText searchTermText = FindViewById<EditText>(Resource.Id.searchTermText);
                TextView statusResult = FindViewById<TextView>(Resource.Id.statusResult);
                TextView givenResult = FindViewById<TextView>(Resource.Id.givenResult);
                TextView surnameResult = FindViewById<TextView>(Resource.Id.surnameResult);
                TextView upnResult = FindViewById<TextView>(Resource.Id.upnResult);
                TextView phoneResult = FindViewById<TextView>(Resource.Id.phoneResult);

                if (string.IsNullOrEmpty(searchTermText.Text))
                {
                    statusResult.SetText(Resource.String.InvalidSearch);
                    statusResult.SetTextColor(Color.White);
                    givenResult.SetText(Resource.String.EmptyString);
                    surnameResult.SetText(Resource.String.EmptyString);
                    upnResult.SetText(Resource.String.EmptyString);
                    phoneResult.SetText(Resource.String.EmptyString);
                    return;
                }

                List<User> results = await DirectorySearcher.SearchByAlias(searchTermText.Text, new PlatformParameters(this));
                if (results.Count == 0)
                {
                    statusResult.SetText(Resource.String.UserNotFound);
                    statusResult.SetTextColor(Color.White);
                    givenResult.SetText(Resource.String.EmptyString);
                    surnameResult.SetText(Resource.String.EmptyString);
                    upnResult.SetText(Resource.String.EmptyString);
                    phoneResult.SetText(Resource.String.EmptyString);
                }
                else if (results[0].error != null)
                {
                    statusResult.SetText("Error! " + results[0].error, TextView.BufferType.Normal);
                    statusResult.SetTextColor(Color.Red);
                    givenResult.SetText(Resource.String.EmptyString);
                    surnameResult.SetText(Resource.String.EmptyString);
                    upnResult.SetText(Resource.String.EmptyString);
                    phoneResult.SetText(Resource.String.EmptyString);
                }
                else
                {
                    statusResult.SetText(Resource.String.Success);
                    statusResult.SetTextColor(Color.Green);
                    givenResult.SetText(results[0].givenName, TextView.BufferType.Normal);
                    surnameResult.SetText(results[0].surname, TextView.BufferType.Normal);
                    upnResult.SetText(results[0].userPrincipalName, TextView.BufferType.Normal);
                    phoneResult.SetText(results[0].telephoneNumber, TextView.BufferType.Normal);
                }
            };
        }
    }
}

