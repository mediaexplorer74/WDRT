using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Spatial;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace System.Data.Services.Client
{
	// Token: 0x020000AB RID: 171
	internal sealed class PrimitiveType
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x000152C8 File Offset: 0x000134C8
		static PrimitiveType()
		{
			PrimitiveType.InitializeTypes();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00015316 File Offset: 0x00013516
		private PrimitiveType(Type clrType, string edmTypeName, EdmPrimitiveTypeKind primitiveKind, PrimitiveTypeConverter typeConverter, bool hasReverseMapping)
		{
			this.ClrType = clrType;
			this.EdmTypeName = edmTypeName;
			this.PrimitiveKind = primitiveKind;
			this.TypeConverter = typeConverter;
			this.HasReverseMapping = hasReverseMapping;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00015343 File Offset: 0x00013543
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001534B File Offset: 0x0001354B
		internal Type ClrType { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00015354 File Offset: 0x00013554
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0001535C File Offset: 0x0001355C
		internal string EdmTypeName { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00015365 File Offset: 0x00013565
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0001536D File Offset: 0x0001356D
		internal PrimitiveTypeConverter TypeConverter { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00015376 File Offset: 0x00013576
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001537E File Offset: 0x0001357E
		internal bool HasReverseMapping { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00015387 File Offset: 0x00013587
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0001538F File Offset: 0x0001358F
		internal EdmPrimitiveTypeKind PrimitiveKind { get; private set; }

		// Token: 0x0600059B RID: 1435 RVA: 0x000153BC File Offset: 0x000135BC
		internal static bool TryGetPrimitiveType(Type clrType, out PrimitiveType ptype)
		{
			Type type = Nullable.GetUnderlyingType(clrType) ?? clrType;
			if (!PrimitiveType.TryGetWellKnownPrimitiveType(type, out ptype))
			{
				lock (PrimitiveType.knownNonPrimitiveTypes)
				{
					if (PrimitiveType.knownNonPrimitiveTypes.Contains(clrType))
					{
						ptype = null;
						return false;
					}
				}
				KeyValuePair<Type, PrimitiveType>[] array;
				lock (PrimitiveType.derivedPrimitiveTypeMapping)
				{
					if (PrimitiveType.derivedPrimitiveTypeMapping.TryGetValue(clrType, out ptype))
					{
						return true;
					}
					array = PrimitiveType.clrMapping.Where((KeyValuePair<Type, PrimitiveType> m) => !m.Key.IsPrimitive() && !m.Key.IsSealed()).Concat(PrimitiveType.derivedPrimitiveTypeMapping).ToArray<KeyValuePair<Type, PrimitiveType>>();
				}
				KeyValuePair<Type, PrimitiveType> keyValuePair = new KeyValuePair<Type, PrimitiveType>(typeof(object), null);
				foreach (KeyValuePair<Type, PrimitiveType> keyValuePair2 in array)
				{
					if (type.IsSubclassOf(keyValuePair2.Key) && keyValuePair2.Key.IsSubclassOf(keyValuePair.Key))
					{
						keyValuePair = keyValuePair2;
					}
				}
				if (keyValuePair.Value == null)
				{
					lock (PrimitiveType.knownNonPrimitiveTypes)
					{
						PrimitiveType.knownNonPrimitiveTypes.Add(clrType);
					}
					return false;
				}
				ptype = keyValuePair.Value;
				lock (PrimitiveType.derivedPrimitiveTypeMapping)
				{
					PrimitiveType.derivedPrimitiveTypeMapping[type] = ptype;
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00015588 File Offset: 0x00013788
		internal static bool TryGetPrimitiveType(string edmTypeName, out PrimitiveType ptype)
		{
			return PrimitiveType.edmMapping.TryGetValue(edmTypeName, out ptype);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00015598 File Offset: 0x00013798
		internal static bool IsKnownType(Type type)
		{
			PrimitiveType primitiveType;
			return PrimitiveType.TryGetPrimitiveType(type, out primitiveType);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000155AD File Offset: 0x000137AD
		internal static bool IsKnownNullableType(Type type)
		{
			return PrimitiveType.IsKnownType(Nullable.GetUnderlyingType(type) ?? type);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000155BF File Offset: 0x000137BF
		internal static void DeleteKnownType(Type clrType, string edmTypeName)
		{
			PrimitiveType.clrMapping.Remove(clrType);
			if (edmTypeName != null)
			{
				PrimitiveType.edmMapping.Remove(edmTypeName);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000155DC File Offset: 0x000137DC
		internal static void RegisterKnownType(Type clrType, string edmTypeName, EdmPrimitiveTypeKind primitiveKind, PrimitiveTypeConverter converter, bool twoWay)
		{
			PrimitiveType primitiveType = new PrimitiveType(clrType, edmTypeName, primitiveKind, converter, twoWay);
			PrimitiveType.clrMapping.Add(clrType, primitiveType);
			if (twoWay)
			{
				PrimitiveType.edmMapping.Add(edmTypeName, primitiveType);
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00015611 File Offset: 0x00013811
		internal IEdmPrimitiveType CreateEdmPrimitiveType()
		{
			return PrimitiveType.ClientEdmPrimitiveType.CreateType(this.PrimitiveKind);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00015620 File Offset: 0x00013820
		private static void InitializeTypes()
		{
			PrimitiveType.RegisterKnownType(typeof(bool), "Edm.Boolean", EdmPrimitiveTypeKind.Boolean, new BooleanTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(byte), "Edm.Byte", EdmPrimitiveTypeKind.Byte, new ByteTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(byte[]), "Edm.Binary", EdmPrimitiveTypeKind.Binary, new ByteArrayTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(DateTime), "Edm.DateTime", EdmPrimitiveTypeKind.DateTime, new DateTimeTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(DateTimeOffset), "Edm.DateTimeOffset", EdmPrimitiveTypeKind.DateTimeOffset, new DateTimeOffsetTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(decimal), "Edm.Decimal", EdmPrimitiveTypeKind.Decimal, new DecimalTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(double), "Edm.Double", EdmPrimitiveTypeKind.Double, new DoubleTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(Guid), "Edm.Guid", EdmPrimitiveTypeKind.Guid, new GuidTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(short), "Edm.Int16", EdmPrimitiveTypeKind.Int16, new Int16TypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(int), "Edm.Int32", EdmPrimitiveTypeKind.Int32, new Int32TypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(long), "Edm.Int64", EdmPrimitiveTypeKind.Int64, new Int64TypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(float), "Edm.Single", EdmPrimitiveTypeKind.Single, new SingleTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(string), "Edm.String", EdmPrimitiveTypeKind.String, new StringTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(sbyte), "Edm.SByte", EdmPrimitiveTypeKind.SByte, new SByteTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(TimeSpan), "Edm.Time", EdmPrimitiveTypeKind.Time, new TimeSpanTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(Geography), "Edm.Geography", EdmPrimitiveTypeKind.Geography, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyPoint), "Edm.GeographyPoint", EdmPrimitiveTypeKind.GeographyPoint, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyLineString), "Edm.GeographyLineString", EdmPrimitiveTypeKind.GeographyLineString, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyPolygon), "Edm.GeographyPolygon", EdmPrimitiveTypeKind.GeographyPolygon, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyCollection), "Edm.GeographyCollection", EdmPrimitiveTypeKind.GeographyCollection, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyMultiPoint), "Edm.GeographyMultiPoint", EdmPrimitiveTypeKind.GeographyMultiPoint, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyMultiLineString), "Edm.GeographyMultiLineString", EdmPrimitiveTypeKind.GeographyMultiLineString, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeographyMultiPolygon), "Edm.GeographyMultiPolygon", EdmPrimitiveTypeKind.GeographyMultiPolygon, new GeographyTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(Geometry), "Edm.Geometry", EdmPrimitiveTypeKind.Geometry, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryPoint), "Edm.GeometryPoint", EdmPrimitiveTypeKind.GeometryPoint, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryLineString), "Edm.GeometryLineString", EdmPrimitiveTypeKind.GeometryLineString, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryPolygon), "Edm.GeometryPolygon", EdmPrimitiveTypeKind.GeometryPolygon, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryCollection), "Edm.GeometryCollection", EdmPrimitiveTypeKind.GeometryCollection, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryMultiPoint), "Edm.GeometryMultiPoint", EdmPrimitiveTypeKind.GeometryMultiPoint, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryMultiLineString), "Edm.GeometryMultiLineString", EdmPrimitiveTypeKind.GeometryMultiLineString, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(GeometryMultiPolygon), "Edm.GeometryMultiPolygon", EdmPrimitiveTypeKind.GeometryMultiPolygon, new GeometryTypeConverter(), true);
			PrimitiveType.RegisterKnownType(typeof(DataServiceStreamLink), "Edm.Stream", EdmPrimitiveTypeKind.Stream, new NamedStreamTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(char), "Edm.String", EdmPrimitiveTypeKind.String, new CharTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(char[]), "Edm.String", EdmPrimitiveTypeKind.String, new CharArrayTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(Type), "Edm.String", EdmPrimitiveTypeKind.String, new ClrTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(Uri), "Edm.String", EdmPrimitiveTypeKind.String, new UriTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(XDocument), "Edm.String", EdmPrimitiveTypeKind.String, new XDocumentTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(XElement), "Edm.String", EdmPrimitiveTypeKind.String, new XElementTypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(ushort), null, EdmPrimitiveTypeKind.String, new UInt16TypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(uint), null, EdmPrimitiveTypeKind.String, new UInt32TypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(ulong), null, EdmPrimitiveTypeKind.String, new UInt64TypeConverter(), false);
			PrimitiveType.RegisterKnownType(typeof(PrimitiveType.BinaryTypeSub), "Edm.Binary", EdmPrimitiveTypeKind.Binary, new BinaryTypeConverter(), false);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00015AB0 File Offset: 0x00013CB0
		private static bool TryGetWellKnownPrimitiveType(Type clrType, out PrimitiveType ptype)
		{
			ptype = null;
			if (!PrimitiveType.clrMapping.TryGetValue(clrType, out ptype) && PrimitiveType.IsBinaryType(clrType))
			{
				ptype = PrimitiveType.clrMapping[typeof(PrimitiveType.BinaryTypeSub)];
			}
			return ptype != null;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00015AEC File Offset: 0x00013CEC
		private static bool IsBinaryType(Type type)
		{
			if (BinaryTypeConverter.BinaryType == null && type.Name == "Binary" && type.Namespace == "System.Data.Linq" && AssemblyName.ReferenceMatchesDefinition(type.Assembly.GetName(), new AssemblyName("System.Data.Linq")))
			{
				BinaryTypeConverter.BinaryType = type;
			}
			return type == BinaryTypeConverter.BinaryType;
		}

		// Token: 0x04000308 RID: 776
		private static readonly Dictionary<Type, PrimitiveType> clrMapping = new Dictionary<Type, PrimitiveType>(EqualityComparer<Type>.Default);

		// Token: 0x04000309 RID: 777
		private static readonly Dictionary<Type, PrimitiveType> derivedPrimitiveTypeMapping = new Dictionary<Type, PrimitiveType>(EqualityComparer<Type>.Default);

		// Token: 0x0400030A RID: 778
		private static readonly Dictionary<string, PrimitiveType> edmMapping = new Dictionary<string, PrimitiveType>(StringComparer.Ordinal);

		// Token: 0x0400030B RID: 779
		private static readonly HashSet<Type> knownNonPrimitiveTypes = new HashSet<Type>(EqualityComparer<Type>.Default);

		// Token: 0x020000AC RID: 172
		private sealed class BinaryTypeSub
		{
		}

		// Token: 0x020000AD RID: 173
		private class ClientEdmPrimitiveType : EdmType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
		{
			// Token: 0x060005A7 RID: 1447 RVA: 0x00015B5F File Offset: 0x00013D5F
			private ClientEdmPrimitiveType(string namespaceName, string name, EdmPrimitiveTypeKind primitiveKind)
			{
				this.namespaceName = namespaceName;
				this.name = name;
				this.primitiveKind = primitiveKind;
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00015B7C File Offset: 0x00013D7C
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00015B84 File Offset: 0x00013D84
			public string Namespace
			{
				get
				{
					return this.namespaceName;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x00015B8C File Offset: 0x00013D8C
			public EdmPrimitiveTypeKind PrimitiveKind
			{
				get
				{
					return this.primitiveKind;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060005AB RID: 1451 RVA: 0x00015B94 File Offset: 0x00013D94
			public EdmSchemaElementKind SchemaElementKind
			{
				get
				{
					return EdmSchemaElementKind.TypeDefinition;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x00015B97 File Offset: 0x00013D97
			public override EdmTypeKind TypeKind
			{
				get
				{
					return EdmTypeKind.Primitive;
				}
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00015B9A File Offset: 0x00013D9A
			public static IEdmPrimitiveType CreateType(EdmPrimitiveTypeKind primitiveKind)
			{
				return new PrimitiveType.ClientEdmPrimitiveType("Edm", primitiveKind.ToString(), primitiveKind);
			}

			// Token: 0x04000312 RID: 786
			private readonly string namespaceName;

			// Token: 0x04000313 RID: 787
			private readonly string name;

			// Token: 0x04000314 RID: 788
			private readonly EdmPrimitiveTypeKind primitiveKind;
		}
	}
}
