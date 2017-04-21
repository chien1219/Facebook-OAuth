using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FFAGo
{

	public class OAuthMessage
	{
		public string id { get; set; }          //input
		public string first_name { get; set; }
		public FacebookPicture picture { get; set; }
		public string email { get; set; }


		public string username;				//output
		public string picture_url;
		private string account;
		private string password;
		private string token;
		public Image profile;
		 /*
			string name;
			string cellphone;
			string c_kid_name;
			string e_kid_name;
			int birth_year;
			int birth_month;
			int birth_day;
			int uniform_number;
			char uniform_size;
			string sex;
			int kid_age;
			string account;
			string password;
			Image picture;
			*/

		public OAuthMessage()
		{
			username = null;
			picture_url = null;
		}

		/*public OAuthMessage(string accountin, string passwordin)
		{
			account = accountin;
			password = passwordin;
		}*/

		public OAuthMessage(string user, string urlin)
		{
			username = user;
			picture_url = urlin;
		}

	}

	public class FacebookPicture
	{
		public FacebookPictureData data { get; set; }
	}

	public class FacebookPictureData
	{
		public string url { get; set; }
	}


}