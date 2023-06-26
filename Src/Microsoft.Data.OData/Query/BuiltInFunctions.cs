using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000AA RID: 170
	internal static class BuiltInFunctions
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		internal static bool TryGetBuiltInFunction(string name, out FunctionSignatureWithReturnType[] signatures)
		{
			return BuiltInFunctions.builtInFunctions.TryGetValue(name, out signatures);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		internal static string BuildFunctionSignatureListDescription(string name, IEnumerable<FunctionSignature> signatures)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (FunctionSignature functionSignature in signatures)
			{
				FunctionSignatureWithReturnType functionSignatureWithReturnType = (FunctionSignatureWithReturnType)functionSignature;
				stringBuilder.Append(text);
				text = "; ";
				string text2 = "";
				stringBuilder.Append(name);
				stringBuilder.Append('(');
				foreach (IEdmTypeReference edmTypeReference in functionSignatureWithReturnType.ArgumentTypes)
				{
					stringBuilder.Append(text2);
					text2 = ", ";
					if (edmTypeReference.IsODataPrimitiveTypeKind() && edmTypeReference.IsNullable)
					{
						stringBuilder.Append(edmTypeReference.ODataFullName());
						stringBuilder.Append(" Nullable=true");
					}
					else
					{
						stringBuilder.Append(edmTypeReference.ODataFullName());
					}
				}
				stringBuilder.Append(')');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		internal static void CreateSpatialFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType[] array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true)
				})
			};
			functions.Add("geo.distance", array);
			array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPolygon, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPolygon, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryPoint, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPolygon, true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPolygon, true),
					EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyPoint, true)
				})
			};
			functions.Add("geo.intersects", array);
			array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeometryLineString, true) }),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetSpatial(EdmPrimitiveTypeKind.GeographyLineString, true) })
			};
			functions.Add("geo.length", array);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000CA20 File Offset: 0x0000AC20
		private static Dictionary<string, FunctionSignatureWithReturnType[]> InitializeBuiltInFunctions()
		{
			Dictionary<string, FunctionSignatureWithReturnType[]> dictionary = new Dictionary<string, FunctionSignatureWithReturnType[]>(StringComparer.Ordinal);
			BuiltInFunctions.CreateStringFunctions(dictionary);
			BuiltInFunctions.CreateSpatialFunctions(dictionary);
			BuiltInFunctions.CreateDateTimeFunctions(dictionary);
			BuiltInFunctions.CreateMathFunctions(dictionary);
			return dictionary;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000CA54 File Offset: 0x0000AC54
		private static void CreateStringFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("endswith", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("indexof", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("replace", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("startswith", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("tolower", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("toupper", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("trim", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			FunctionSignatureWithReturnType[] array = new FunctionSignatureWithReturnType[]
			{
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true),
					EdmCoreModel.Instance.GetInt32(false)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(false),
					EdmCoreModel.Instance.GetInt32(true)
				}),
				new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
				{
					EdmCoreModel.Instance.GetString(true),
					EdmCoreModel.Instance.GetInt32(true),
					EdmCoreModel.Instance.GetInt32(true)
				})
			};
			functions.Add("substring", array);
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetBoolean(false), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("substringof", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetString(true), new IEdmTypeReference[]
			{
				EdmCoreModel.Instance.GetString(true),
				EdmCoreModel.Instance.GetString(true)
			});
			functions.Add("concat", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
			functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetString(true) });
			functions.Add("length", new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType });
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000CF28 File Offset: 0x0000B128
		private static void CreateDateTimeFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			FunctionSignatureWithReturnType[] array = BuiltInFunctions.CreateDateTimeFunctionSignatureArray();
			FunctionSignatureWithReturnType[] array2 = array.Concat(BuiltInFunctions.CreateTimeSpanFunctionSignatures()).ToArray<FunctionSignatureWithReturnType>();
			functions.Add("year", array);
			functions.Add("month", array);
			functions.Add("day", array);
			functions.Add("hour", array2);
			functions.Add("minute", array2);
			functions.Add("second", array2);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000CF94 File Offset: 0x0000B194
		private static FunctionSignatureWithReturnType[] CreateDateTimeFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTime, true) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType3 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType4 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.DateTimeOffset, true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType2, functionSignatureWithReturnType3, functionSignatureWithReturnType4 };
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000D198 File Offset: 0x0000B398
		private static IEnumerable<FunctionSignatureWithReturnType> CreateTimeSpanFunctionSignatures()
		{
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Time, false) });
			yield return new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetInt32(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetTemporal(EdmPrimitiveTypeKind.Time, true) });
			yield break;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000D1AE File Offset: 0x0000B3AE
		private static void CreateMathFunctions(IDictionary<string, FunctionSignatureWithReturnType[]> functions)
		{
			functions.Add("round", BuiltInFunctions.CreateMathFunctionSignatureArray());
			functions.Add("floor", BuiltInFunctions.CreateMathFunctionSignatureArray());
			functions.Add("ceiling", BuiltInFunctions.CreateMathFunctionSignatureArray());
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		private static FunctionSignatureWithReturnType[] CreateMathFunctionSignatureArray()
		{
			FunctionSignatureWithReturnType functionSignatureWithReturnType = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType2 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDouble(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDouble(true) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType3 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(false) });
			FunctionSignatureWithReturnType functionSignatureWithReturnType4 = new FunctionSignatureWithReturnType(EdmCoreModel.Instance.GetDecimal(false), new IEdmTypeReference[] { EdmCoreModel.Instance.GetDecimal(true) });
			return new FunctionSignatureWithReturnType[] { functionSignatureWithReturnType, functionSignatureWithReturnType3, functionSignatureWithReturnType2, functionSignatureWithReturnType4 };
		}

		// Token: 0x0400014F RID: 335
		private static readonly Dictionary<string, FunctionSignatureWithReturnType[]> builtInFunctions = BuiltInFunctions.InitializeBuiltInFunctions();
	}
}
