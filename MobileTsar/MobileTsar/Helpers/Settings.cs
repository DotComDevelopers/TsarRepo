// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MobileTsar.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		//private static ISettings AppSettings
		//{
		//	get
		//	{
		//		return CrossSettings.Current;
		//	}
		//}
	  private static ISettings AppSettings
	  {
	    get
	    {
	      if (CrossSettings.IsSupported)
	        return CrossSettings.Current;

	      return null; // or your custom implementation 
	    }
	  }
    //#region Setting Constants

    //private const string SettingsKey = "settings_key";
    //private static readonly string SettingsDefault = string.Empty;

    //#endregion


  //  public static string Username
		//{
		//	get
		//	{
		//		return AppSettings.GetValueOrDefault("Username", "");
		//	}
		//	set
		//	{
		//		AppSettings.AddOrUpdateValue("Username", value);
		//	}
		//}
	  public static string Username
	  {
	    get
	    {
	      if (CrossSettings.IsSupported)
	        return AppSettings.GetValueOrDefault("Username", "");

	      return null; // or your custom implementation 
	    }
	    set
	    {
	      AppSettings.AddOrUpdateValue("Username", value);
	    }
	  }


    // public static string Password
    //{
    //  get
    //  {
    //    return AppSettings.GetValueOrDefault("Password", "");
    //  }
    //  set
    //  {
    //    AppSettings.AddOrUpdateValue("Password", value);
    //  }
	  //}


	  public static string Password
    {
	    get
	    {
	      if (CrossSettings.IsSupported)
	        return AppSettings.GetValueOrDefault("Password", "");

	      return null; // or your custom implementation 
	    }
	    set
	    {
	      AppSettings.AddOrUpdateValue("Password", value);
	    }
	  }

	  public static string VaultPassword
	  {
	    get
	    {
	      if (CrossSettings.IsSupported)
	        return AppSettings.GetValueOrDefault("VaultPassword", "");

	      return null; // or your custom implementation 
	    }
	    set
	    {
	      AppSettings.AddOrUpdateValue("VaultPassword", value);
	    }
	  }

    //public static string AccessToken
    //{
    //  get
    //  {
    //    return AppSettings.GetValueOrDefault("AccessToken", "");
    //  }
    //  set
    //  {
    //    AppSettings.AddOrUpdateValue("AccessToken", value);
    //  }
    //}
    public static string AccessToken
	  {
	    get
	    {
	      if (CrossSettings.IsSupported)
	        return AppSettings.GetValueOrDefault("AccessToken", "");

        return "O5elrr1ks8rk4k1B-AeIBcp-ts0IrGJ1g5D0vMxAhIrrisSMN0h2Ut2eV3ef7iT7e96XGepFXNm47MBFYsH1tsgWeYn8TgLYtzvbBYUHZaR-bi3-DzomVKh7_abdXrbZ8sf3wtMs_3OxEzUVooLrq2FxEV-vPEyHg57Ncv0-Zxow8yUvNx7QwJGXTmGxrrIDHKNRcb0RPjmVcNXwNc1bD5yYQqFTSnTfJY-oZC6U46z09CR8iGkdlUWxuqqOEfO4zDHTXT74zOadgLmucttRRivqRjtiOVQ0ZsfqjIPJqyupgdg9G5n5IrUW8Oia3ObB3xDasrOZL0aC_4HpPn0DQwa3kkFFpxDtpzijGtN0GERiuwkTZVYuipnQyQGqh0Yztg8RA0BzS7cxZgqhKLkHDxRql90Uy4_6tuRp1c0Dz5jH8neHxh9h4iXbafh5QdPCIpxySUZtWZE8udICiYPWnjN1-zzbpeU7S1tMFPx3nAjSFTWcveMw7fhilK3X4cBJcHlhSdyrtDuAJiVr5B91JQ"; // or your custom implementation 
	    }
	    set
      {
        AppSettings.AddOrUpdateValue("AccessToken", value);
      }
    }

  }
}