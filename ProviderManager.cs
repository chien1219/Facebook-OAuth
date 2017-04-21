using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFAGo
{
	public enum Provider
	{
		Unknown = 0,
		Facebook,
		Google
	}

	public static class ProviderManager
	{
		private static OAuthSettings FacebookOAuthSettings
		{
			get
			{
				return new OAuthSettings
				{
					//ClientId = "755499197960342",							///test
					//ClientSecret = "81d26e3a72fcd413d0064f22532afaf3",		///test
					//ClientId = "1651918068462476",						///demo
					//ClientSecret = "b9e46e4c7257b8c6e108dad79df603d3",	///demo
					ClientId = "754431008067161",                           ///test
					ClientSecret = "2a72e666c8601929f563a98096b06c7f",      ///test

					AuthorizeUrl = "https://m.facebook.com/dialog/oauth/",
					RedirectUrl = "http://www.facebook.com/connect/login_success.html",
					AccessTokenUrl = "https://graph.facebook.com/v2.3/oauth/access_token",
					Scopes = new List<string> {
						""
					}
				};
			}
		}

		private static OAuthSettings GoogleOAuthSettings
		{
			get
			{
				return new OAuthSettings
				{
					ClientId = "",
					ClientSecret = "",
					AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth",
					RedirectUrl = "https://www.googleapis.com/plus/v1/people/me",
					AccessTokenUrl = "https://accounts.google.com/o/oauth2/token",
					Scopes = new List<string> {
						"openid"
					}
				};
			}
		}

		public static OAuthSettings GetProviderOAuthSettings(Provider provider)
		{
			switch (provider)
			{
				case Provider.Facebook:
					{
						return FacebookOAuthSettings;
					}
				case Provider.Google:
					{
						return GoogleOAuthSettings;
					}
			}
			return new OAuthSettings();
		}
	}
}