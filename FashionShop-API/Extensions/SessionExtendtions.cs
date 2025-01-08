using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FashionShop_API.Extensions
{
    public static class SessionExtendtions
    {
        // gán session 
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        // lấy session
        public static T ? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

    }
}
