using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Enums
{
    public sealed class Technologies
    {
        private static readonly Dictionary<string, Technologies> instance = new Dictionary<string, Technologies>();

        private readonly String name;
        private readonly int value;

        public static readonly Technologies C = new Technologies(1, "C");
        public static readonly Technologies CPLUSPLUS = new Technologies(2, "C++");
        public static readonly Technologies CSHARP = new Technologies(3, "C#");
        public static readonly Technologies JAVA = new Technologies(4, "Java");
        public static readonly Technologies JS = new Technologies(5, "Javascript");
        public static readonly Technologies PERL = new Technologies(6, "Perl");
        public static readonly Technologies PHP = new Technologies(7, "PHP");
        public static readonly Technologies PYTHON = new Technologies(8, "Python");
        public static readonly Technologies RUBY = new Technologies(9, "Ruby");

        private Technologies(int value, String name)
        {
            this.name = name;
            this.value = value;
            instance[name] = this;
        }

        public override String ToString()
        {
            return name;
        }

        public static Dictionary<string, string> getDictionary()
        {
            Dictionary<string, string> output = new Dictionary<string, string>();

            foreach (KeyValuePair<string, Technologies> entry in instance)
            {
                output.Add(entry.Value.value.ToString(), entry.Value.ToString());
            }
            return output;
        }

        public static explicit operator Technologies(string str)
        {
            Technologies result;
            if (instance.TryGetValue(str, out result))
                return result;
            else
                throw new InvalidCastException(str + " is an unrecognized technology");
        }
    }
}