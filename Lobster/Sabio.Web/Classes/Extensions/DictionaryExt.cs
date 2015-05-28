using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web
{
    public static class DictionaryExt
    {


        public static Dictionary<string, string> ToDictionary(this Enum @enum)
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => e.ToString(), e => Enum.GetName(type, e));
        }


        public static Dictionary<string, string> ToDictionary<T>(this T @enum) where T: struct
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().ToDictionary(e => e.ToString(), e => Enum.GetName(type, e));
        }

        public static KeyValuePair<int, string>[] ToJsonDictionary<T>(this T @enum) where T : struct
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().Select(e => new
                KeyValuePair<int, string>(Convert.ToInt32(e.ToString()), Enum.GetName(type, e))).ToArray();
        }

        public static Dictionary<string, int > ToDictionary<T>(this T @enum, int minValue) where T : struct
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().Where(i => i >= minValue).ToDictionary(e => Enum.GetName(type, e),e =>  e);
        }
    }
}