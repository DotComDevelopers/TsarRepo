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

        return "nznbZB8UwZsW71HMsqCcdC_NqiY6A4Vk6pQLy7SsLdwoWm54miZENEJvhnDc4GOg8s9m2SStezhXiUB9CLsRTMcRusK5dYkxynGxXZpevmnLP7cbShiMYic0fA2tU-bj-MA9ComoJ-GHZpIH_wSGjMoarqx9egbUFWw0pL5oYIKWeyaAZx6l8Oyh8UNLGAFbJzoHgtAN3BRrOdU96GrE9_GqmQJE7RyG-cAu2Wm3Z5v6Y9sxzLQXTl3Rmja-TQeuxSOvldGNySKQ09cP15RT8zjWvin9uOUPMUyXYMsjjIosOp5Gu1FwAvcvQfuHSw4qB_Upg8ZoSaBvUDrfuKTNKOK5vzv92NDjqTNfaYcEZ3DZ8yUWYoCvSHFiKNzzOa4dlTJZvekgjuYbV_zEFMiMVwOiTXi533fT-dbg_zBZrisZkocYf0hvaTc4RNK9ggYXqFSZdkqYLsC7IazXO4kLyOzjCY3AIYmukkPWqAlThQnXYULA7JbyAFKSNHkFqKotyQTrA3G8B0MkPd0tlsjgYg"; // or your custom implementation 
	    }
	    set
      {
        AppSettings.AddOrUpdateValue("AccessToken", value);
      }
    }

  }
}