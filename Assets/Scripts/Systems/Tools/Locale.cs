
using System.Collections.Generic;

public class Locale
{
    private static Locale _inst;

    public static Locale Inst
    {
        get
        {
            if (_inst == null) _inst = new Locale();
            return _inst;
        }
    }

    private Dictionary<string, string> _quests = new Dictionary<string, string>();

    public Locale()
    {
        _quests.Add("1:destroy:20", "Уничтожено Духов Земли [b]{0}[/b] из [b]{1}[/b]");
    }


    public string GetQuestLocale(string key)
    {
        string result;
        return _quests.TryGetValue(key, out result) ? result : "";
    }

}
