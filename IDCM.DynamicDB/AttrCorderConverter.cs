using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDCM.DynamicDB
{
    public class AttrCorderConverter
    {
        public static readonly AttrCorderConverter Hide = new AttrCorderConverter("Hide", -1);
        public static readonly AttrCorderConverter KeepHide = new AttrCorderConverter("KeepHide", 0);
        public static readonly AttrCorderConverter Show = new AttrCorderConverter("Show", 1);
        public static readonly AttrCorderConverter Invalid = new AttrCorderConverter("Invalid", Int32.MinValue);
        
        /// <summary>
        /// For iterator 
        /// </summary>
        public static IEnumerable<AttrCorderConverter> Values
        {
            get
            {
                yield return Hide;
                yield return KeepHide;
                yield return Show;
                yield return Invalid;
            }
        }

        private readonly string name;
        private readonly int value;

        AttrCorderConverter(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name { get { return name; } }

        public int Value { get { return value; } }

        public override string ToString()
        {
            return name;
        }
        public override bool Equals(Object obj)
        {
            return this.name.Equals(obj.ToString());
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        public static int toHide(int num)
        {
            return (num<KeepHide.value)?num:num*Hide.value;
        }
        public static int toShow(int num)
        {
            return (num > KeepHide.value) ? num : num * Hide.value;
        }
    }
}
