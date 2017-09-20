using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

public static class ExtensionMethods
{
    public static T DeepClone<T>(T a)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }
    }

    public static object InvokeMethod<T>(T obj, string methodName, params object[] args)
    {
        var type = typeof(T);
        var method = type.GetMethod(methodName);
        return method.Invoke(obj, args);
    }

    public static Dictionary<string, object> ToDictionary(object myObj)
    {
        return myObj.GetType()
            .GetProperties()
            .Select(pi => new { Name = pi.Name, Value = pi.GetValue(myObj, null) })
            .Union(
                myObj.GetType()
                .GetFields()
                .Select(fi => new { Name = fi.Name, Value = fi.GetValue(myObj) })
             )
            .ToDictionary(ks => ks.Name, vs => vs.Value);
    }
    
    public static T ToObject<T>(this IDictionary<string, object> dict) where T : class, new()
    {
        var t = new T();
        PropertyInfo[] properties = t.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                continue;

            KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

            // Find which property type (int, string, double? etc) the CURRENT property is...
            Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

            // Fix nullables...
            Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

            // ...and change the type
            object newA = Convert.ChangeType(item.Value, newT);
            t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
        }
        return t;
    }

    public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
    {
        return source.GetType().GetProperties(bindingAttr).ToDictionary
        (
            propInfo => propInfo.Name,
            propInfo => propInfo.GetValue(source, null)
        );

    }

    public static string HashSHA256(string value)
    {
        var sha1 = SHA256.Create();
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hash = sha1.ComputeHash(inputBytes);
        var sb = new StringBuilder();
        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
}
