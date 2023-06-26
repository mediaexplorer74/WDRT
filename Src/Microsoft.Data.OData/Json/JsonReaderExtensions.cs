using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x0200024A RID: 586
	internal static class JsonReaderExtensions
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x00046DE2 File Offset: 0x00044FE2
		internal static void ReadStartObject(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartObject);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00046DEB File Offset: 0x00044FEB
		internal static void ReadEndObject(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndObject);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00046DF4 File Offset: 0x00044FF4
		internal static void ReadStartArray(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.StartArray);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00046DFD File Offset: 0x00044FFD
		internal static void ReadEndArray(this JsonReader jsonReader)
		{
			jsonReader.ReadNext(JsonNodeType.EndArray);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00046E06 File Offset: 0x00045006
		internal static string GetPropertyName(this JsonReader jsonReader)
		{
			return (string)jsonReader.Value;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x00046E14 File Offset: 0x00045014
		internal static string ReadPropertyName(this JsonReader jsonReader)
		{
			jsonReader.ValidateNodeType(JsonNodeType.Property);
			string propertyName = jsonReader.GetPropertyName();
			jsonReader.ReadNext();
			return propertyName;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00046E38 File Offset: 0x00045038
		internal static object ReadPrimitiveValue(this JsonReader jsonReader)
		{
			object value = jsonReader.Value;
			jsonReader.ReadNext(JsonNodeType.PrimitiveValue);
			return value;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00046E54 File Offset: 0x00045054
		internal static string ReadStringValue(this JsonReader jsonReader)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			string text = obj as string;
			if (obj == null || text != null)
			{
				return text;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsString(obj));
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00046E84 File Offset: 0x00045084
		internal static string ReadStringValue(this JsonReader jsonReader, string propertyName)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			string text = obj as string;
			if (obj == null || text != null)
			{
				return text;
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadPropertyValueAsString(obj, propertyName));
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00046EB4 File Offset: 0x000450B4
		internal static double? ReadDoubleValue(this JsonReader jsonReader)
		{
			object obj = jsonReader.ReadPrimitiveValue();
			double? num = obj as double?;
			if (obj == null || num != null)
			{
				return num;
			}
			int? num2 = obj as int?;
			if (num2 != null)
			{
				return new double?((double)num2.Value);
			}
			throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_CannotReadValueAsDouble(obj));
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00046F10 File Offset: 0x00045110
		internal static void SkipValue(this JsonReader jsonReader)
		{
			jsonReader.SkipValue(null);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00046F1C File Offset: 0x0004511C
		internal static void SkipValue(this JsonReader jsonReader, StringBuilder jsonRawValueStringBuilder)
		{
			int num = 0;
			do
			{
				switch (jsonReader.NodeType)
				{
				case JsonNodeType.StartObject:
				case JsonNodeType.StartArray:
					num++;
					break;
				case JsonNodeType.EndObject:
				case JsonNodeType.EndArray:
					num--;
					break;
				}
				if (jsonRawValueStringBuilder != null)
				{
					jsonRawValueStringBuilder.Append(jsonReader.RawValue);
				}
				jsonReader.ReadNext();
			}
			while (num > 0);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00046F79 File Offset: 0x00045179
		internal static JsonNodeType ReadNext(this JsonReader jsonReader)
		{
			jsonReader.Read();
			return jsonReader.NodeType;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00046F88 File Offset: 0x00045188
		internal static bool IsOnValueNode(this JsonReader jsonReader)
		{
			JsonNodeType nodeType = jsonReader.NodeType;
			return nodeType == JsonNodeType.PrimitiveValue || nodeType == JsonNodeType.StartObject || nodeType == JsonNodeType.StartArray;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00046FAA File Offset: 0x000451AA
		[Conditional("DEBUG")]
		internal static void AssertNotBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00046FAC File Offset: 0x000451AC
		[Conditional("DEBUG")]
		internal static void AssertBuffering(this BufferingJsonReader bufferedJsonReader)
		{
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00046FAE File Offset: 0x000451AE
		internal static ODataException CreateException(string exceptionMessage)
		{
			return new ODataException(exceptionMessage);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00046FB6 File Offset: 0x000451B6
		private static void ReadNext(this JsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			jsonReader.ValidateNodeType(expectedNodeType);
			jsonReader.Read();
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00046FC6 File Offset: 0x000451C6
		private static void ValidateNodeType(this JsonReader jsonReader, JsonNodeType expectedNodeType)
		{
			if (jsonReader.NodeType != expectedNodeType)
			{
				throw JsonReaderExtensions.CreateException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(expectedNodeType, jsonReader.NodeType));
			}
		}
	}
}
