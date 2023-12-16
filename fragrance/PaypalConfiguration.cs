using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fragrance
{
	public static class PaypalConfiguration
	{
		//Variables for storing the clientID and clientSecret key  
		public readonly static string ClientId;
		public readonly static string ClientSecret;
		//Constructor  
		static PaypalConfiguration()
		{
			var config = GetConfig();
			ClientId = config["clientId"];
			ClientSecret = config["clientSecret"];
		}
		// getting properties from the web.config  
		public static Dictionary<string, string> GetConfig()
		{
			return PayPal.Api.ConfigManager.Instance.GetProperties();
		}
		private static string GetAccessToken()
		{
			// getting accesstocken from paypal  
			string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
			return accessToken;
		}
		public static APIContext GetAPIContext()
		{
			// return apicontext object by invoking it with the accesstoken  
			APIContext apiContext = new APIContext(GetAccessToken());
			apiContext.Config = GetConfig();
			return apiContext;
		}
		public static String ConvertToString(this Enum eff)
		{
			return Enum.GetName(eff.GetType(), eff);
		}

		public static EnumType ConverToEnum<EnumType>(this String enumValue)
		{
			return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
		}
	}
}