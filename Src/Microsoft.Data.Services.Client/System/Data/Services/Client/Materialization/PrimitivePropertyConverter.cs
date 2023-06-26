using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Spatial;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x02000048 RID: 72
	internal class PrimitivePropertyConverter
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000C464 File Offset: 0x0000A664
		internal PrimitivePropertyConverter(ODataFormat format)
		{
			this.format = format;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		internal object ConvertPrimitiveValue(object value, Type propertyType)
		{
			if (propertyType != null && value != null)
			{
				Type type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
				TypeCode typeCode = PlatformHelper.GetTypeCode(type);
				switch (typeCode)
				{
				case TypeCode.Boolean:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
				case TypeCode.DateTime:
				case TypeCode.String:
					return this.ConvertValueIfNeeded(value, propertyType);
				}
				if (typeCode == TypeCode.Char || typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64 || type == typeof(char[]) || type == typeof(Type) || type == typeof(Uri) || type == typeof(XDocument) || type == typeof(XElement))
				{
					PrimitiveType primitiveType;
					PrimitiveType.TryGetPrimitiveType(propertyType, out primitiveType);
					string text = (string)this.ConvertValueIfNeeded(value, typeof(string));
					return primitiveType.TypeConverter.Parse(text);
				}
				if (propertyType == BinaryTypeConverter.BinaryType)
				{
					byte[] array = (byte[])this.ConvertValueIfNeeded(value, typeof(byte[]));
					return Activator.CreateInstance(BinaryTypeConverter.BinaryType, new object[] { array });
				}
			}
			return this.ConvertValueIfNeeded(value, propertyType);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C610 File Offset: 0x0000A810
		private static object ConvertNonSpatialValue(object value, Type targetType)
		{
			switch (PlatformHelper.GetTypeCode(targetType))
			{
			case TypeCode.Boolean:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
			}
			string text = ClientConvert.ToString(value);
			return ClientConvert.ChangeType(text, targetType);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C670 File Offset: 0x0000A870
		private object ConvertValueIfNeeded(object value, Type targetType)
		{
			if (value == null || targetType.IsInstanceOfType(value))
			{
				return value;
			}
			if (typeof(ISpatial).IsAssignableFrom(targetType) || value is ISpatial)
			{
				return this.ConvertSpatialValue(value, targetType);
			}
			Type underlyingType = Nullable.GetUnderlyingType(targetType);
			if (underlyingType != null)
			{
				targetType = underlyingType;
			}
			return PrimitivePropertyConverter.ConvertNonSpatialValue(value, targetType);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		private object ConvertSpatialValue(object value, Type targetType)
		{
			if (typeof(Geometry).IsAssignableFrom(targetType))
			{
				Geography geography = value as Geography;
				if (geography == null)
				{
					return value;
				}
				return this.ConvertSpatialValue<Geography, Geometry>(geography);
			}
			else
			{
				Geometry geometry = value as Geometry;
				if (geometry == null)
				{
					return value;
				}
				return this.ConvertSpatialValue<Geometry, Geography>(geometry);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C710 File Offset: 0x0000A910
		private TOut ConvertSpatialValue<TIn, TOut>(TIn valueToConvert) where TIn : ISpatial where TOut : class, ISpatial
		{
			if (this.format == ODataFormat.Atom)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
					{
						this.lazyGmlFormatter.Value.Write(valueToConvert, xmlWriter);
					}
					memoryStream.Position = 0L;
					using (XmlReader xmlReader = XmlReader.Create(memoryStream))
					{
						return this.lazyGmlFormatter.Value.Read<TOut>(xmlReader);
					}
				}
			}
			IDictionary<string, object> dictionary = this.lazyGeoJsonFormatter.Value.Write(valueToConvert);
			return this.lazyGeoJsonFormatter.Value.Read<TOut>(dictionary);
		}

		// Token: 0x04000238 RID: 568
		private readonly ODataFormat format;

		// Token: 0x04000239 RID: 569
		private readonly SimpleLazy<GeoJsonObjectFormatter> lazyGeoJsonFormatter = new SimpleLazy<GeoJsonObjectFormatter>(new Func<GeoJsonObjectFormatter>(GeoJsonObjectFormatter.Create));

		// Token: 0x0400023A RID: 570
		private readonly SimpleLazy<GmlFormatter> lazyGmlFormatter = new SimpleLazy<GmlFormatter>(new Func<GmlFormatter>(GmlFormatter.Create));
	}
}
