using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using DirectorySearcherLib;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

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
                User result = await DirectorySearcher.SearchByAlias(SearchTermText.Text, new AuthorizationParameters(this)); // add this param
                if (result.error != null) {
                    StatusResult.Text = "Error! " + result.error.message.value;
                    GivenResult.Text = "";
                    SurnameResult.Text = "";
                    UpnResult.Text = "";
                    PhoneResult.Text = "";
                }
                else if (result.value.Count == 0) {
                    StatusResult.Text = "User Not Found";
                    GivenResult.Text = "";
                    SurnameResult.Text = "";
                    UpnResult.Text = "";
                    PhoneResult.Text = "";
                }
                else
                {
                    StatusResult.Text = "Success";
                    GivenResult.Text = result.value[0].givenName;
                    SurnameResult.Text = result.value[0].surname;
                    UpnResult.Text = result.value[0].userPrincipalName;
                    PhoneResult.Text = result.value[0].telephoneNumber == null ? "Not Listed" : result.value[0].telephoneNumber;
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