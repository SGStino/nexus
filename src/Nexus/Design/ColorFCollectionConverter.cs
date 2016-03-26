using System;
using System.ComponentModel;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using Nexus.Graphics.Colors;

namespace Nexus.Design
{
	public sealed class ColorFCollectionConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
				throw GetConvertFromException(value);

			string source = value as string;
			if (source != null)
				return ColorFCollection.Parse(source);
			return base.ConvertFrom(context, culture, value);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
				return true;

			return ((destinationType == typeof(string)) || base.CanConvertTo(context, destinationType));
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if ((destinationType != null) && (value is ColorFCollection))
			{
				ColorFCollection vectords = (ColorFCollection) value;
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo ci = typeof(ColorFCollection).GetConstructor(new Type[] { typeof(ColorF[]) });
					return new InstanceDescriptor(ci, new object[] { vectords.ToArray() });
				}
				else if (destinationType == typeof(string))
					return vectords.ConvertToString(null, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}