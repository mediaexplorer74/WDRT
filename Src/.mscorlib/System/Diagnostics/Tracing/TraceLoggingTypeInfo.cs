using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000484 RID: 1156
	internal abstract class TraceLoggingTypeInfo
	{
		// Token: 0x0600376E RID: 14190 RVA: 0x000D686A File Offset: 0x000D4A6A
		internal TraceLoggingTypeInfo(Type dataType)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			this.name = dataType.Name;
			this.dataType = dataType;
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000D68A8 File Offset: 0x000D4AA8
		internal TraceLoggingTypeInfo(Type dataType, string name, EventLevel level, EventOpcode opcode, EventKeywords keywords, EventTags tags)
		{
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			if (name == null)
			{
				throw new ArgumentNullException("eventName");
			}
			Statics.CheckName(name);
			this.name = name;
			this.keywords = keywords;
			this.level = level;
			this.opcode = opcode;
			this.tags = tags;
			this.dataType = dataType;
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000D691E File Offset: 0x000D4B1E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000D6926 File Offset: 0x000D4B26
		public EventLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000D692E File Offset: 0x000D4B2E
		public EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000D6936 File Offset: 0x000D4B36
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x000D693E File Offset: 0x000D4B3E
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000D6946 File Offset: 0x000D4B46
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x06003776 RID: 14198
		public abstract void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format);

		// Token: 0x06003777 RID: 14199
		public abstract void WriteObjectData(TraceLoggingDataCollector collector, object value);

		// Token: 0x06003778 RID: 14200 RVA: 0x000D694E File Offset: 0x000D4B4E
		public virtual object GetData(object value)
		{
			return value;
		}

		// Token: 0x040018AD RID: 6317
		private readonly string name;

		// Token: 0x040018AE RID: 6318
		private readonly EventKeywords keywords;

		// Token: 0x040018AF RID: 6319
		private readonly EventLevel level = (EventLevel)(-1);

		// Token: 0x040018B0 RID: 6320
		private readonly EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x040018B1 RID: 6321
		private readonly EventTags tags;

		// Token: 0x040018B2 RID: 6322
		private readonly Type dataType;
	}
}
