using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000025 RID: 37
	internal static class TypeConverterHelper
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00005078 File Offset: 0x00003278
		internal static object DoConversionFrom(TypeConverter converter, object value)
		{
			object obj = value;
			try
			{
				if (converter != null && value != null && converter.CanConvertFrom(value.GetType()))
				{
					obj = converter.ConvertFrom(null, CultureInfo.InvariantCulture, value);
				}
			}
			catch (Exception ex)
			{
				if (!TypeConverterHelper.ShouldEatException(ex))
				{
					throw;
				}
			}
			return obj;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000050C8 File Offset: 0x000032C8
		private static bool ShouldEatException(Exception e)
		{
			bool flag = false;
			if (e.InnerException != null)
			{
				flag |= TypeConverterHelper.ShouldEatException(e.InnerException);
			}
			return flag | (e is FormatException);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000050FA File Offset: 0x000032FA
		internal static TypeConverter GetTypeConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}
	}
}
