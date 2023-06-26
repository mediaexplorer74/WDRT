using System;
using System.Data;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E1 RID: 225
	[NullableContext(1)]
	[Nullable(0)]
	public class DataSetConverter : JsonConverter
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x00030D88 File Offset: 0x0002EF88
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			DataSet dataSet = (DataSet)value;
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			DataTableConverter dataTableConverter = new DataTableConverter();
			writer.WriteStartObject();
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName(dataTable.TableName) : dataTable.TableName);
				dataTableConverter.WriteJson(writer, dataTable, serializer);
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00030E30 File Offset: 0x0002F030
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			DataSet dataSet = ((objectType == typeof(DataSet)) ? new DataSet() : ((DataSet)Activator.CreateInstance(objectType)));
			DataTableConverter dataTableConverter = new DataTableConverter();
			reader.ReadAndAssert();
			while (reader.TokenType == JsonToken.PropertyName)
			{
				DataTable dataTable = dataSet.Tables[(string)reader.Value];
				bool flag = dataTable != null;
				dataTable = (DataTable)dataTableConverter.ReadJson(reader, typeof(DataTable), dataTable, serializer);
				if (!flag)
				{
					dataSet.Tables.Add(dataTable);
				}
				reader.ReadAndAssert();
			}
			return dataSet;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00030ECF File Offset: 0x0002F0CF
		public override bool CanConvert(Type valueType)
		{
			return typeof(DataSet).IsAssignableFrom(valueType);
		}
	}
}
