using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using FFAGo;
using Xamarin.Forms.Platform.Android;
using FFAGo.Droid;
using Xamarin.Auth;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace FFAGo.Droid
{
	public class LoginPageRenderer : PageRenderer
	{
		LoginPage page;
		bool loginInProgress;

		public LoginPageRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null ||
				Element == null)
				return;

			page = e.NewElement as LoginPage;

			if (page == null ||
				loginInProgress)
				return;

			loginInProgress = true;
			try
			{
				OAuth2Authenticator auth = new OAuth2Authenticator(
					page.ProviderOAuthSettings.ClientId, // your OAuth2 client id
					page.ProviderOAuthSettings.ClientSecret, // your OAuth2 client secret
					page.ProviderOAuthSettings.ScopesString, // scopes
					new Uri(page.ProviderOAuthSettings.AuthorizeUrl), // the scopes, delimited by the "+" symbol
					new Uri(page.ProviderOAuthSettings.RedirectUrl), // the redirect URL for the service
					new Uri(page.ProviderOAuthSettings.AccessTokenUrl));

				auth.AllowCancel = true;
				auth.Completed += async (sender, args) =>
				{
					if (args.IsAuthenticated)
					{
						var token = args.Account.Properties["access_token"];

						var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?fields=first_name,picture"), null, args.Account);
						var response = await request.GetResponseAsync();

						if (response != null)
						{
							string userJson = response.GetResponseText();

							OAuthMessage result = JsonConvert.DeserializeObject<OAuthMessage>(userJson);

							FFAGo.App.authref = new OAuthMessage(result.first_name, result.picture.data.url);
							MessagingCenter.Send<OAuthMessage, string>(new OAuthMessage(), "Authenticated", token);
						}

					}
					else
					{
						MessagingCenter.Send<OAuthMessage, string>(new OAuthMessage(), "Authenticated", "Canceled!");
					}
					//await page.Navigation.PopModalAsync();
					await page.Navigation.PushModalAsync(new MenuPage((OAuthMessage)FFAGo.App.authref));
					loginInProgress = false;

				};

				auth.Error += (sender, args) =>
				{
					Console.WriteLine("Error: {0}", args.Exception);
				};

				var activity = this.Context as Activity;
				activity.StartActivity((Android.Content.Intent)auth.GetUI(activity));
			}
			catch (Exception ex)
			{
				MessagingCenter.Send<OAuthMessage, string>(new OAuthMessage(), "Authenticated", $"Authentication Exception: {ex.Message}");
			}
		}
	}
}