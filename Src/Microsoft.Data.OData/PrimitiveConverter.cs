using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData
{
	// Token: 0x02000241 RID: 577
	internal sealed class PrimitiveConverter
	{
		// Token: 0x06001271 RID: 4721 RVA: 0x0004558C File Offset: 0x0004378C
		internal PrimitiveConverter(KeyValuePair<Type, IPrimitiveTypeConverter>[] spatialPrimitiveTypeConverters)
		{
			this.spatialPrimitiveTypeConverters = new Dictionary<Type, IPrimitiveTypeConverter>(EqualityComparer<Type>.Default);
			foreach (KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair in spatialPrimitiveTypeConverters)
			{
				this.spatialPrimitiveTypeConverters.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000455E5 File Offset: 0x000437E5
		internal static PrimitiveConverter Instance
		{
			get
			{
				return PrimitiveConverter.primitiveConverter;
			}
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000455EC File Offset: 0x000437EC
		internal bool TryTokenizeFromXml(XmlReader reader, Type targetType, out object tokenizedPropertyValue)
		{
			tokenizedPropertyValue = null;
			IPrimitiveTypeConverter primitiveTypeConverter;
			if (this.TryGetConverter(targetType, out primitiveTypeConverter))
			{
				tokenizedPropertyValue = primitiveTypeConverter.TokenizeFromXml(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00045630 File Offset: 0x00043830
		internal bool TryWriteAtom(object instance, XmlWriter writer)
		{
			return this.TryWriteValue(instance, delegate(IPrimitiveTypeConverter ptc)
			{
				ptc.WriteAtom(instance, writer);
			});
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004566C File Offset: 0x0004386C
		internal void WriteVerboseJson(object instance, IJsonWriter jsonWriter, string typeName, ODataVersion odataVersion)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			this.TryGetConverter(type, out primitiveTypeConverter);
			primitiveTypeConverter.WriteVerboseJson(instance, jsonWriter, typeName, odataVersion);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00045698 File Offset: 0x00043898
		internal void WriteJsonLight(object instance, IJsonWriter jsonWriter, ODataVersion odataVersion)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			this.TryGetConverter(type, out primitiveTypeConverter);
			primitiveTypeConverter.WriteJsonLight(instance, jsonWriter, odataVersion);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000456C0 File Offset: 0x000438C0
		private bool TryWriteValue(object instance, Action<IPrimitiveTypeConverter> writeMethod)
		{
			Type type = instance.GetType();
			IPrimitiveTypeConverter primitiveTypeConverter;
			if (this.TryGetConverter(type, out primitiveTypeConverter))
			{
				writeMethod(primitiveTypeConverter);
				return true;
			}
			return false;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000456EC File Offset: 0x000438EC
		private bool TryGetConverter(Type type, out IPrimitiveTypeConverter primitiveTypeConverter)
		{
			if (typeof(ISpatial).IsAssignableFrom(type))
			{
				KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair = new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(object), null);
				foreach (KeyValuePair<Type, IPrimitiveTypeConverter> keyValuePair2 in this.spatialPrimitiveTypeConverters)
				{
					if (keyValuePair2.Key.IsAssignableFrom(type) && keyValuePair.Key.IsAssignableFrom(keyValuePair2.Key))
					{
						keyValuePair = keyValuePair2;
					}
				}
				primitiveTypeConverter = keyValuePair.Value;
				return keyValuePair.Value != null;
			}
			primitiveTypeConverter = null;
			return false;
		}

		// Token: 0x040006AB RID: 1707
		private static readonly IPrimitiveTypeConverter geographyTypeConverter = new GeographyTypeConverter();

		// Token: 0x040006AC RID: 1708
		private static readonly IPrimitiveTypeConverter geometryTypeConverter = new GeometryTypeConverter();

		// Token: 0x040006AD RID: 1709
		private static readonly PrimitiveConverter primitiveConverter = new PrimitiveConverter(new KeyValuePair<Type, IPrimitiveTypeConverter>[]
		{
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyPoint), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyLineString), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyPolygon), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyCollection), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyMultiPoint), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyMultiLineString), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeographyMultiPolygon), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(Geography), PrimitiveConverter.geographyTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryPoint), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryLineString), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryPolygon), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryCollection), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryMultiPoint), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryMultiLineString), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(GeometryMultiPolygon), PrimitiveConverter.geometryTypeConverter),
			new KeyValuePair<Type, IPrimitiveTypeConverter>(typeof(Geometry), PrimitiveConverter.geometryTypeConverter)
		});

		// Token: 0x040006AE RID: 1710
		private readonly Dictionary<Type, IPrimitiveTypeConverter> spatialPrimitiveTypeConverters;
	}
}
