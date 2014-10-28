// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace DirSearchClient_iOS
{
	[Register ("DirSearchClientiOSViewController")]
	partial class DirSearchClientiOSViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel GivenLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel GivenResult { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel PhoneLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel PhoneResult { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SearchButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField SearchTermText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel StatusResult { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel SurnameLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel SurnameResult { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel UpnLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel UpnResult { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (GivenLabel != null) {
				GivenLabel.Dispose ();
				GivenLabel = null;
			}
			if (GivenResult != null) {
				GivenResult.Dispose ();
				GivenResult = null;
			}
			if (PhoneLabel != null) {
				PhoneLabel.Dispose ();
				PhoneLabel = null;
			}
			if (PhoneResult != null) {
				PhoneResult.Dispose ();
				PhoneResult = null;
			}
			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}
			if (SearchTermText != null) {
				SearchTermText.Dispose ();
				SearchTermText = null;
			}
			if (StatusResult != null) {
				StatusResult.Dispose ();
				StatusResult = null;
			}
			if (SurnameLabel != null) {
				SurnameLabel.Dispose ();
				SurnameLabel = null;
			}
			if (SurnameResult != null) {
				SurnameResult.Dispose ();
				SurnameResult = null;
			}
			if (UpnLabel != null) {
				UpnLabel.Dispose ();
				UpnLabel = null;
			}
			if (UpnResult != null) {
				UpnResult.Dispose ();
				UpnResult = null;
			}
		}
	}
}
