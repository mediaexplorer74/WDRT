using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;

namespace System.Data.Services.Client
{
	// Token: 0x0200012A RID: 298
	internal static class Util
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x0002882C File Offset: 0x00026A2C
		internal static Version GetVersionFromMaxProtocolVersion(DataServiceProtocolVersion maxProtocolVersion)
		{
			switch (maxProtocolVersion)
			{
			case DataServiceProtocolVersion.V1:
				return Util.DataServiceVersion1;
			case DataServiceProtocolVersion.V2:
				return Util.DataServiceVersion2;
			case DataServiceProtocolVersion.V3:
				return Util.DataServiceVersion3;
			default:
				return Util.DataServiceVersion2;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00028866 File Offset: 0x00026A66
		[Conditional("DEBUG")]
		internal static void DebugInjectFault(string state)
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00028868 File Offset: 0x00026A68
		internal static T CheckArgumentNull<T>([Util.ValidatedNotNullAttribute] T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw Error.ArgumentNull(parameterName);
			}
			return value;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002887A File Offset: 0x00026A7A
		internal static void CheckArgumentNullAndEmpty([Util.ValidatedNotNullAttribute] string value, string parameterName)
		{
			Util.CheckArgumentNull<string>(value, parameterName);
			Util.CheckArgumentNotEmpty(value, parameterName);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0002888B File Offset: 0x00026A8B
		internal static void CheckArgumentNotEmpty(string value, string parameterName)
		{
			if (value != null && value.Length == 0)
			{
				throw Error.Argument(Strings.Util_EmptyString, parameterName);
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000288A4 File Offset: 0x00026AA4
		internal static void CheckArgumentNotEmpty<T>(T[] value, string parameterName) where T : class
		{
			Util.CheckArgumentNull<T[]>(value, parameterName);
			if (value.Length == 0)
			{
				throw Error.Argument(Strings.Util_EmptyArray, parameterName);
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (object.ReferenceEquals(value[i], null))
				{
					throw Error.Argument(Strings.Util_NullArrayElement, parameterName);
				}
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000288F8 File Offset: 0x00026AF8
		internal static MergeOption CheckEnumerationValue(MergeOption value, string parameterName)
		{
			switch (value)
			{
			case MergeOption.AppendOnly:
			case MergeOption.OverwriteChanges:
			case MergeOption.PreserveChanges:
			case MergeOption.NoTracking:
				return value;
			default:
				throw Error.ArgumentOutOfRange(parameterName);
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00028928 File Offset: 0x00026B28
		internal static DataServiceProtocolVersion CheckEnumerationValue(DataServiceProtocolVersion value, string parameterName)
		{
			switch (value)
			{
			case DataServiceProtocolVersion.V1:
			case DataServiceProtocolVersion.V2:
			case DataServiceProtocolVersion.V3:
				return value;
			default:
				throw Error.ArgumentOutOfRange(parameterName);
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00028954 File Offset: 0x00026B54
		internal static HttpStack CheckEnumerationValue(HttpStack value, string parameterName)
		{
			if (value == HttpStack.Auto)
			{
				return value;
			}
			throw Error.ArgumentOutOfRange(parameterName);
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00028970 File Offset: 0x00026B70
		internal static char[] GetWhitespaceForTracing(int depth)
		{
			char[] array = Util.whitespaceForTracing;
			while (array.Length <= depth)
			{
				char[] array2 = new char[2 * array.Length];
				array2[0] = '\r';
				array2[1] = '\n';
				for (int i = 2; i < array2.Length; i++)
				{
					array2[i] = ' ';
				}
				Interlocked.CompareExchange<char[]>(ref Util.whitespaceForTracing, array2, array);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000289C3 File Offset: 0x00026BC3
		internal static void Dispose<T>(ref T disposable) where T : class, IDisposable
		{
			Util.Dispose<T>(disposable);
			disposable = default(T);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000289D7 File Offset: 0x00026BD7
		internal static void Dispose<T>(T disposable) where T : class, IDisposable
		{
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000289EE File Offset: 0x00026BEE
		internal static bool IsKnownClientExcption(Exception ex)
		{
			return ex is DataServiceClientException || ex is DataServiceQueryException || ex is DataServiceRequestException;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00028A0B File Offset: 0x00026C0B
		internal static T NullCheck<T>(T value, InternalError errorcode) where T : class
		{
			if (object.ReferenceEquals(value, null))
			{
				Error.ThrowInternalError(errorcode);
			}
			return value;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00028A24 File Offset: 0x00026C24
		internal static bool DoesNullAttributeSayTrue(XmlReader reader)
		{
			string attribute = reader.GetAttribute("null", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			return attribute != null && XmlConvert.ToBoolean(attribute);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00028A50 File Offset: 0x00026C50
		internal static void SetNextLinkForCollection(object collection, DataServiceQueryContinuation continuation)
		{
			foreach (PropertyInfo propertyInfo in collection.GetType().GetPublicProperties(true))
			{
				if (!(propertyInfo.Name != "Continuation") && propertyInfo.CanWrite && typeof(DataServiceQueryContinuation).IsAssignableFrom(propertyInfo.PropertyType))
				{
					propertyInfo.SetValue(collection, continuation, null);
				}
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00028AD8 File Offset: 0x00026CD8
		internal static bool IsNullableType(Type t)
		{
			return t.IsClass() || Nullable.GetUnderlyingType(t) != null;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00028AF5 File Offset: 0x00026CF5
		internal static object ActivatorCreateInstance(Type type, params object[] arguments)
		{
			if (arguments.Length == 0)
			{
				return Activator.CreateInstance(type);
			}
			return Activator.CreateInstance(type, arguments);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00028B0A File Offset: 0x00026D0A
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static object ConstructorInvoke(ConstructorInfo constructor, object[] arguments)
		{
			if (constructor == null)
			{
				throw new MissingMethodException();
			}
			return constructor.Invoke(arguments);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00028B22 File Offset: 0x00026D22
		internal static bool IsFlagSet(SaveChangesOptions options, SaveChangesOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00028B2A File Offset: 0x00026D2A
		internal static bool IsBatch(SaveChangesOptions options)
		{
			return Util.IsBatchWithSingleChangeset(options) || Util.IsBatchWithIndependentOperations(options);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00028B3C File Offset: 0x00026D3C
		internal static bool IsBatchWithSingleChangeset(SaveChangesOptions options)
		{
			return Util.IsFlagSet(options, SaveChangesOptions.Batch);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00028B4A File Offset: 0x00026D4A
		internal static bool IsBatchWithIndependentOperations(SaveChangesOptions options)
		{
			return Util.IsFlagSet(options, SaveChangesOptions.BatchWithIndependentOperations);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00028B59 File Offset: 0x00026D59
		internal static bool IncludeLinkState(EntityStates x)
		{
			return EntityStates.Modified == x || EntityStates.Unchanged == x;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00028B68 File Offset: 0x00026D68
		[Conditional("TRACE")]
		internal static void TraceElement(XmlReader reader, TextWriter writer)
		{
			if (writer != null)
			{
				writer.Write(Util.GetWhitespaceForTracing(2 + reader.Depth), 0, 2 + reader.Depth);
				writer.Write("<{0}", reader.Name);
				if (reader.MoveToFirstAttribute())
				{
					do
					{
						writer.Write(" {0}=\"{1}\"", reader.Name, reader.Value);
					}
					while (reader.MoveToNextAttribute());
					reader.MoveToElement();
				}
				writer.Write(reader.IsEmptyElement ? " />" : ">");
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00028BED File Offset: 0x00026DED
		[Conditional("TRACE")]
		internal static void TraceEndElement(XmlReader reader, TextWriter writer, bool indent)
		{
			if (writer != null)
			{
				if (indent)
				{
					writer.Write(Util.GetWhitespaceForTracing(2 + reader.Depth), 0, 2 + reader.Depth);
				}
				writer.Write("</{0}>", reader.Name);
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00028C22 File Offset: 0x00026E22
		[Conditional("TRACE")]
		internal static void TraceText(TextWriter writer, string value)
		{
			if (writer != null)
			{
				writer.Write(value);
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00028C30 File Offset: 0x00026E30
		internal static IEnumerable<T> GetEnumerable<T>(IEnumerable enumerable, Func<object, T> valueConverter)
		{
			List<T> list = new List<T>();
			foreach (object obj in enumerable)
			{
				list.Add(valueConverter(obj));
			}
			return list;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00028C8C File Offset: 0x00026E8C
		internal static Version ToVersion(this DataServiceProtocolVersion protocolVersion)
		{
			switch (protocolVersion)
			{
			case DataServiceProtocolVersion.V1:
				return Util.DataServiceVersion1;
			case DataServiceProtocolVersion.V2:
				return Util.DataServiceVersion2;
			default:
				return Util.DataServiceVersion3;
			}
		}

		// Token: 0x040005B1 RID: 1457
		internal const string VersionSuffix = ";NetFx";

		// Token: 0x040005B2 RID: 1458
		internal const string CodeGeneratorToolName = "System.Data.Services.Design";

		// Token: 0x040005B3 RID: 1459
		internal const string LoadPropertyMethodName = "LoadProperty";

		// Token: 0x040005B4 RID: 1460
		internal const string ExecuteMethodName = "Execute";

		// Token: 0x040005B5 RID: 1461
		internal const string ExecuteMethodNameForVoidResults = "ExecuteVoid";

		// Token: 0x040005B6 RID: 1462
		internal const string SaveChangesMethodName = "SaveChanges";

		// Token: 0x040005B7 RID: 1463
		internal static readonly Version DataServiceVersionEmpty = new Version(0, 0);

		// Token: 0x040005B8 RID: 1464
		internal static readonly Version DataServiceVersion1 = new Version(1, 0);

		// Token: 0x040005B9 RID: 1465
		internal static readonly Version DataServiceVersion2 = new Version(2, 0);

		// Token: 0x040005BA RID: 1466
		internal static readonly Version DataServiceVersion3 = new Version(3, 0);

		// Token: 0x040005BB RID: 1467
		internal static readonly Version[] SupportedResponseVersions = new Version[]
		{
			Util.DataServiceVersion1,
			Util.DataServiceVersion2,
			Util.DataServiceVersion3
		};

		// Token: 0x040005BC RID: 1468
		private static char[] whitespaceForTracing = new char[] { '\r', '\n', ' ', ' ', ' ', ' ', ' ' };

		// Token: 0x0200012B RID: 299
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
