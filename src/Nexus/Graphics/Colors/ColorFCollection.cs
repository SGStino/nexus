using Nexus.Design;
using Nexus.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Nexus.Graphics.Colors
{
    [TypeConverter(typeof(ColorFCollectionConverter))]
    public class ColorFCollection : List<ColorF>
    {
        public ColorFCollection()
        {

        }

        public ColorFCollection(IEnumerable<ColorF> collection)
            : base(collection)
        {

        }

        internal string ConvertToString(string format, IFormatProvider provider)
        {
            if (this.Count == 0)
                return string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                builder.AppendFormat(provider, "{0:" + format + "}", new object[] { this[i] });
                if (i != (this.Count - 1))
                    builder.Append(" ");
            }
            return builder.ToString();
        }

        public static ColorFCollection Parse(string source)
        {
            IFormatProvider cultureInfo = CultureInfo.InvariantCulture;
            TokenizerHelper helper = new TokenizerHelper(source, cultureInfo);
            ColorFCollection colors = new ColorFCollection();
            while (helper.NextToken())
            {
                ColorF color = new ColorF(Convert.ToSingle(helper.GetCurrentToken(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo), Convert.ToSingle(helper.NextTokenRequired(), cultureInfo));
                colors.Add(color);
            }
            return colors;
        }
    }
}