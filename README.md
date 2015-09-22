---
services:
platforms:
author: azure
---

NativeClient-MultiTarget-DotNet
===============================

This sample solution shows how to build a native application that uses Xamarin to target several different platforms with a single shared C# code base.  The application signs users in with Azure Active Directory (AAD), using the Active Directory Authentication Library (ADAL) to obtain a JWT access token through the OAuth 2.0 protocol.  The access token is sent to AAD's Graph API to authenticate the user and obtain information about other users in their organization.

## About The Sample
If you would like to get started immediately, skip this section and jump to *How To Run The Sample*.

This sample solution is a "Directory Searcher" that contains five projects, each of which is a different type of application: iOS, Android, Windows Phone 8.1, Windows Store 8.1, and WPF.  The solution is built using the [Xamarin Platform](http://xamarin.com/platform), which allows all five applications to be written in C# and ported to the corresponding platform.  Each application contains two major portions: a platform specific project that is used primarily for UI, and a shared portable class library (PCL) that contains the application logic.  In addition, the Windows Phone and Windows Store applications are written as a Universal App, so even UI code is shared across these two projects.

In this application, the platform specific projects are effectively only responsible from presenting the UI.  Each application receives an input search term from the user, and calls a method `SearchByAlias` in the application's `DirectorySearcherLib` PCL.  `SearchByAlias` returns a `User` object, which is used to present information about the search results such as the user's name, user principal name, and phone number.

The `DirectorySearcherLib` PCL is the application's shared C# code base, which contains both the identity related logic and the search related logic.  It leverages ADAL.NET v3, which is also a PCL, to automatically sign users in with the OAuth 2.0 protocol, acquire an access token for the AAD Graph API, cache tokens, maintain user sessions, and so on.  It then queries AAD's Graph API for information about a user in the authenticated user's tenant with a matching alias, and returns results to the platform specific UI code.  By writing both the identity and search logic in the `DirectorySearcherLib` PCL, the code only needs to be written once and can be reused across each platform.

## How To Run The Sample

To run this entire sample you will need:
- An Internet connection
- A Windows machine (necessary if you want to run the Windows Phone, Windows Store, or WPF apps)
- An OS X machine (necessary if you want to run the iOS app)
- Visual Studio 2013 (recommended) or Xamarin Studio
- An Azure subscription (a free trial is sufficient)
- A Xamarin Business subscription (a [free trial](http://developer.xamarin.com/guides/cross-platform/getting_started/beginning_a_xamarin_trial/) is sufficient)
- [Xamarin for Windows](https://xamarin.com/download), which includes Xamarin.iOS, Xamarin.Android, Visual Studio Integration (recommended for this sample), and optionally Xamarin Studio (in lieu of Visual Studio).
- [Xamarin for OS X](https://xamarin.com/download), which includes Xamarin.iOS, Xamarin.Android, Xamarin.Mac, and Xamarin Studio.

Every Azure subscription has an associated Azure Active Directory tenant.  If you don't already have an Azure subscription, you can get a free subscription by signing up at [http://www.windowsazure.com](http://www.windowsazure.com).  All of the Azure AD features used by this sample are available free of charge.

### Step 1: Setup your Xamarin development environment

We recommend you begin with the [Xamarin Download Page](https://xamarin.com/download), installing Xamarin on both your Mac and PC.  If you don't have both machines available, you can still run the sample, but certain projects will have to be omitted.  Follow the [detailed installation guides](http://developer.xamarin.com/guides/cross-platform/getting_started/installation/) for iOS and Android, and if you would like to understand more about the options available to you for development, have a look at the [Building Cross Platform Applications](http://developer.xamarin.com/guides/cross-platform/application_fundamentals/building_cross_platform_applications/part_1_-_understanding_the_xamarin_mobile_platform/) guide.  You do not need to setup a device for development at this time, nor do you need a Apple Developer Program subscription (unless, of course, you want to run the iOS app on a device).

For this sample, we recommend that you use the Visual Studio Integration to run the sample in Visual Studio, and connect to your OS X machine by [pairing via the Xamarin Build Host](http://developer.xamarin.com/guides/ios/getting_started/installation/windows/), which comes as part of your Xamarin for OS X installation.

### Step 2:  Clone or download this repository

Once you have completed your IDE setup, from your shell or command line run:

`git clone https://github.com/AzureADSamples/NativeClient-MultiTarget-DotNet.git`

or download and exact the repository .zip file.

### Step 3:  Create user(s) in your Azure Active Directory tenant

For this sample, you must have at least one user homed in the AAD tenant in which you will register the application.  That is to say that you can not run the sample with your Microsoft Account, so create a new user with the same domain as your tenant if necessary. 

### Step 4:  Register the sample with your Azure Active Directory tenant

This step and the following are actually optional - the sample is configured to run with any tenant out of the box.  But for best understanding, we recommend completing these two steps and registering the application in your own tenant.

1. Sign in to the [Azure management portal](https://manage.windowsazure.com).
2. Click on Active Directory in the left hand nav.
3. Click the directory tenant where you wish to register the sample application.
4. Click the Applications tab.
5. In the drawer, click Add.
6. Click "Add an application my organization is developing".
7. Enter a friendly name for the application, for example "DirectorySearcherClient", select "Native Client Application", and click next.
8. Enter a Redirect Uri value of your choosing and of form `http://MyDirectorySearcherApp`.
10. While still in the Azure portal, click the Configure tab of your application.
11. Find the Client ID value and copy it aside, you will need this later when configuring your application.
13. In the Permissions to Other Applications configuration section, ensure that "Access your organization's directory" and "Enable sign-on and read user's profiles" are selected under "Delegated permissions" for Windows Azure Active Directory.  Save the configuration.

### Step 5:  Configure the sample to use your Azure AD tenant

1. Open the solution in Visual Studio 2013.
2. Open the `DirectorySearcher.cs` file in the `DirectorySearcherLib (Portable)` project.
3. Find the `clientId` member variable and replace its value with the Client Id you copied from the Azure portal.
5. Find the `returnUri` member variable and replace the value with the redirect Uri you registerd in the Azure portal.

### Step 6:  Run the sample(s)

Clean the solution, rebuild the solution, and run it!  Explore each different platform by running each project, searching for users by their alias, logging in with users in your tenant, and viewing results.  You'll notice that the user needs to consent to the app on first login - this is because the app is configured to work with any tenant.  If you would like to remove this consent flow, see the comments in the `Directory Searcher` class.

## How to Recreate this Sample

Coming soon.
