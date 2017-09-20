using System;

public class LocalizationService : ILoggable
{
    public static string LOCALIZATION = "ru"; //ru

    public void GetLocalization(Action getDataSuccesess, Action<ResponseCode> getDataFailed)
    {
    	// Load zipped localization from server
        getDataSuccesess();
    }
}

