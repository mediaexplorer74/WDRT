using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x020002A5 RID: 677
	internal sealed class JsonWriter : IJsonWriter
	{
		// Token: 0x060016C9 RID: 5833 RVA: 0x00053202 File Offset: 0x00051402
		internal JsonWriter(TextWriter writer, bool indent, ODataFormat jsonFormat)
		{
			this.writer = new IndentedTextWriter(writer, indent);
			this.scopes = new Stack<JsonWriter.Scope>();
			this.mustWriteDecimalPointInDoubleValues = jsonFormat == ODataFormat.Json;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00053230 File Offset: 0x00051430
		public void StartPaddingFunctionScope()
		{
			this.StartScope(JsonWriter.ScopeType.Padding);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0005323C File Offset: 0x0005143C
		public void EndPaddingFunctionScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0005327C File Offset: 0x0005147C
		public void StartObjectScope()
		{
			this.StartScope(JsonWriter.ScopeType.Object);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00053288 File Offset: 0x00051488
		public void EndObjectScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000532C8 File Offset: 0x000514C8
		public void StartArrayScope()
		{
			this.StartScope(JsonWriter.ScopeType.Array);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000532D4 File Offset: 0x000514D4
		public void EndArrayScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00053314 File Offset: 0x00051514
		public void WriteDataWrapper()
		{
			this.writer.Write("\"d\":");
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00053326 File Offset: 0x00051526
		public void WriteDataArrayName()
		{
			this.WriteName("results");
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00053334 File Offset: 0x00051534
		public void WriteName(string name)
		{
			JsonWriter.Scope scope = this.scopes.Peek();
			if (scope.ObjectCount != 0)
			{
				this.writer.Write(",");
			}
			scope.ObjectCount++;
			JsonValueUtils.WriteEscapedJsonString(this.writer, name);
			this.writer.Write(":");
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0005338F File Offset: 0x0005158F
		public void WritePaddingFunctionName(string functionName)
		{
			this.writer.Write(functionName);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005339D File Offset: 0x0005159D
		public void WriteValue(bool value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000533B1 File Offset: 0x000515B1
		public void WriteValue(int value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000533C5 File Offset: 0x000515C5
		public void WriteValue(float value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000533D9 File Offset: 0x000515D9
		public void WriteValue(short value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000533ED File Offset: 0x000515ED
		public void WriteValue(long value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00053401 File Offset: 0x00051601
		public void WriteValue(double value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, this.mustWriteDecimalPointInDoubleValues);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0005341B File Offset: 0x0005161B
		public void WriteValue(Guid value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0005342F File Offset: 0x0005162F
		public void WriteValue(decimal value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00053443 File Offset: 0x00051643
		public void WriteValue(DateTime value, ODataVersion odataVersion)
		{
			this.WriteValueSeparator();
			if (odataVersion < ODataVersion.V3)
			{
				JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ODataDateTime);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0005346A File Offset: 0x0005166A
		public void WriteValue(DateTimeOffset value, ODataVersion odataVersion)
		{
			this.WriteValueSeparator();
			if (odataVersion < ODataVersion.V3)
			{
				JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ODataDateTime);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00053491 File Offset: 0x00051691
		public void WriteValue(TimeSpan value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000534A5 File Offset: 0x000516A5
		public void WriteValue(byte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000534B9 File Offset: 0x000516B9
		public void WriteValue(sbyte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000534CD File Offset: 0x000516CD
		public void WriteValue(string value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x000534E1 File Offset: 0x000516E1
		public void WriteRawString(string value)
		{
			this.writer.Write(value);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000534EF File Offset: 0x000516EF
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x000534FC File Offset: 0x000516FC
		public void WriteValueSeparator()
		{
			if (this.scopes.Count == 0)
			{
				return;
			}
			JsonWriter.Scope scope = this.scopes.Peek();
			if (scope.Type == JsonWriter.ScopeType.Array)
			{
				if (scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				scope.ObjectCount++;
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00053554 File Offset: 0x00051754
		public void StartScope(JsonWriter.ScopeType type)
		{
			if (this.scopes.Count != 0 && this.scopes.Peek().Type != JsonWriter.ScopeType.Padding)
			{
				JsonWriter.Scope scope = this.scopes.Peek();
				if (scope.Type == JsonWriter.ScopeType.Array && scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				scope.ObjectCount++;
			}
			JsonWriter.Scope scope2 = new JsonWriter.Scope(type);
			this.scopes.Push(scope2);
			this.writer.Write(scope2.StartString);
			this.writer.IncreaseIndentation();
			this.writer.WriteLine();
		}

		// Token: 0x04000970 RID: 2416
		private readonly IndentedTextWriter writer;

		// Token: 0x04000971 RID: 2417
		private readonly Stack<JsonWriter.Scope> scopes;

		// Token: 0x04000972 RID: 2418
		private readonly bool mustWriteDecimalPointInDoubleValues;

		// Token: 0x020002A6 RID: 678
		internal enum ScopeType
		{
			// Token: 0x04000974 RID: 2420
			Array,
			// Token: 0x04000975 RID: 2421
			Object,
			// Token: 0x04000976 RID: 2422
			Padding
		}

		// Token: 0x020002A7 RID: 679
		private sealed class Scope
		{
			// Token: 0x060016E6 RID: 5862 RVA: 0x000535F8 File Offset: 0x000517F8
			public Scope(JsonWriter.ScopeType type)
			{
				this.type = type;
				switch (type)
				{
				case JsonWriter.ScopeType.Array:
					this.StartString = "[";
					this.EndString = "]";
					return;
				case JsonWriter.ScopeType.Object:
					this.StartString = "{";
					this.EndString = "}";
					return;
				case JsonWriter.ScopeType.Padding:
					this.StartString = "(";
					this.EndString = ")";
					return;
				default:
					return;
				}
			}

			// Token: 0x17000493 RID: 1171
			// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0005366B File Offset: 0x0005186B
			// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00053673 File Offset: 0x00051873
			public string StartString { get; private set; }

			// Token: 0x17000494 RID: 1172
			// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0005367C File Offset: 0x0005187C
			// (set) Token: 0x060016EA RID: 5866 RVA: 0x00053684 File Offset: 0x00051884
			public string EndString { get; private set; }

			// Token: 0x17000495 RID: 1173
			// (get) Token: 0x060016EB RID: 5867 RVA: 0x0005368D File Offset: 0x0005188D
			// (set) Token: 0x060016EC RID: 5868 RVA: 0x00053695 File Offset: 0x00051895
			public int ObjectCount { get; set; }

			// Token: 0x17000496 RID: 1174
			// (get) Token: 0x060016ED RID: 5869 RVA: 0x0005369E File Offset: 0x0005189E
			public JsonWriter.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000977 RID: 2423
			private readonly JsonWriter.ScopeType type;
		}
	}
}
