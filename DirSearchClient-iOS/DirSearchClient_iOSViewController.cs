using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;

namespace DirSearchClient_iOS
{
    public partial class DirSearchClientiOSViewController : UIViewController
    {
        public DirSearchClientiOSViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            SearchButton.TouchUpInside += async (object sender, EventArgs e) =>
            {
                if (string.IsNullOrEmpty(SearchTermText.Text))
                {
                    StatusResult.Text = "Please Enter A Valid Search Term";
                    StatusResult.TextColor = UIColor.Black;
                    GivenResult.Text = "";
                    SurnameResult.Text = "";
                    UpnResult.Text = "";
                    PhoneResult.Text = "";
                    return;
                }

                List<User> results = await DirectorySearcher.SearchByAlias(SearchTermText.Text, new AuthorizationParameters(this));
                if (results.Count == 0) 
                {
                    StatusResult.Text = "User Not Found";
                    StatusResult.TextColor = UIColor.Black;
                    GivenResult.Text = "";
                    SurnameResult.Text = "";
                    UpnResult.Text = "";
                    PhoneResult.Text = "";
                } 
                else if (results[0].error != null) 
                {
                    StatusResult.Text = "Error! " + results[0].error;
                    StatusResult.TextColor = UIColor.Red;
                    GivenResult.Text = "";
                    SurnameResult.Text = "";
                    UpnResult.Text = "";
                    PhoneResult.Text = "";
                }
                else
                {
                    StatusResult.TextColor = UIColor.Green;
                    StatusResult.Text = "Success";
                    GivenResult.Text = results[0].givenName;
                    SurnameResult.Text = results[0].surname;
                    UpnResult.Text = results[0].userPrincipalName;
                    PhoneResult.Text = results[0].telephoneNumber;
                }

            };
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}