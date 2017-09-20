using System;
using System.Collections.Generic;
using JsonFx.Json;

public class RewardItemConverter : JsonConverter
{
    /*
    public override bool CanConvert(Type t)
    {
        return t == typeof(RewardItem);
    }

    public override Dictionary<string, object> WriteJson(Type type, object value)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(Type type, Dictionary<string, object> value)
    {
        if (type == typeof(RewardItem) || typeof(RewardItem).IsAssignableFrom(type))
        {
            if (value.ContainsKey("amount"))
            {
                return JsonReader.CoerceType<AmountItem>(value);
            }
            if (value.ContainsKey("power"))
            {
                return JsonReader.CoerceType<SoulItem>(value);
            }
        }

        return ReadJson(type, value);
    }
    */
    public override bool CanConvert(Type t)
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, object> WriteJson(Type type, object value)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(Type type, Dictionary<string, object> value)
    {
        throw new NotImplementedException();
    }
}
