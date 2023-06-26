using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000482 RID: 1154
	internal class TraceLoggingEventTypes
	{
		// Token: 0x06003750 RID: 14160 RVA: 0x000D6195 File Offset: 0x000D4395
		internal TraceLoggingEventTypes(string name, EventTags tags, params Type[] types)
			: this(tags, name, TraceLoggingEventTypes.MakeArray(types))
		{
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000D61A5 File Offset: 0x000D43A5
		internal TraceLoggingEventTypes(string name, EventTags tags, params TraceLoggingTypeInfo[] typeInfos)
			: this(tags, name, TraceLoggingEventTypes.MakeArray(typeInfos))
		{
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000D61B8 File Offset: 0x000D43B8
		internal TraceLoggingEventTypes(string name, EventTags tags, ParameterInfo[] paramInfos)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.typeInfos = this.MakeArray(paramInfos);
			this.name = name;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			for (int i = 0; i < this.typeInfos.Length; i++)
			{
				TraceLoggingTypeInfo traceLoggingTypeInfo = this.typeInfos[i];
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				string text = paramInfos[i].Name;
				if (Statics.ShouldOverrideFieldName(text))
				{
					text = traceLoggingTypeInfo.Name;
				}
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, text, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000D62B0 File Offset: 0x000D44B0
		private TraceLoggingEventTypes(EventTags tags, string defaultName, TraceLoggingTypeInfo[] typeInfos)
		{
			if (defaultName == null)
			{
				throw new ArgumentNullException("defaultName");
			}
			this.typeInfos = typeInfos;
			this.name = defaultName;
			this.tags = tags;
			this.level = 5;
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = new TraceLoggingMetadataCollector();
			foreach (TraceLoggingTypeInfo traceLoggingTypeInfo in typeInfos)
			{
				this.level = Statics.Combine((int)traceLoggingTypeInfo.Level, this.level);
				this.opcode = Statics.Combine((int)traceLoggingTypeInfo.Opcode, this.opcode);
				this.keywords |= traceLoggingTypeInfo.Keywords;
				traceLoggingTypeInfo.WriteMetadata(traceLoggingMetadataCollector, null, EventFieldFormat.Default);
			}
			this.typeMetadata = traceLoggingMetadataCollector.GetMetadata();
			this.scratchSize = traceLoggingMetadataCollector.ScratchSize;
			this.dataCount = traceLoggingMetadataCollector.DataCount;
			this.pinCount = traceLoggingMetadataCollector.PinCount;
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000D6381 File Offset: 0x000D4581
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000D6389 File Offset: 0x000D4589
		internal EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000D6391 File Offset: 0x000D4591
		internal EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000D6399 File Offset: 0x000D4599
		internal EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000D63A1 File Offset: 0x000D45A1
		internal EventTags Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000D63AC File Offset: 0x000D45AC
		internal NameInfo GetNameInfo(string name, EventTags tags)
		{
			NameInfo nameInfo = this.nameInfos.TryGet(new KeyValuePair<string, EventTags>(name, tags));
			if (nameInfo == null)
			{
				nameInfo = this.nameInfos.GetOrAdd(new NameInfo(name, tags, this.typeMetadata.Length));
			}
			return nameInfo;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000D63EC File Offset: 0x000D45EC
		private TraceLoggingTypeInfo[] MakeArray(ParameterInfo[] paramInfos)
		{
			if (paramInfos == null)
			{
				throw new ArgumentNullException("paramInfos");
			}
			List<Type> list = new List<Type>(paramInfos.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[paramInfos.Length];
			for (int i = 0; i < paramInfos.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(paramInfos[i].ParameterType, list);
			}
			return array;
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000D643C File Offset: 0x000D463C
		private static TraceLoggingTypeInfo[] MakeArray(Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			List<Type> list = new List<Type>(types.Length);
			TraceLoggingTypeInfo[] array = new TraceLoggingTypeInfo[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				array[i] = Statics.GetTypeInfoInstance(types[i], list);
			}
			return array;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000D6484 File Offset: 0x000D4684
		private static TraceLoggingTypeInfo[] MakeArray(TraceLoggingTypeInfo[] typeInfos)
		{
			if (typeInfos == null)
			{
				throw new ArgumentNullException("typeInfos");
			}
			return (TraceLoggingTypeInfo[])typeInfos.Clone();
		}

		// Token: 0x0400189E RID: 6302
		internal readonly TraceLoggingTypeInfo[] typeInfos;

		// Token: 0x0400189F RID: 6303
		internal readonly string name;

		// Token: 0x040018A0 RID: 6304
		internal readonly EventTags tags;

		// Token: 0x040018A1 RID: 6305
		internal readonly byte level;

		// Token: 0x040018A2 RID: 6306
		internal readonly byte opcode;

		// Token: 0x040018A3 RID: 6307
		internal readonly EventKeywords keywords;

		// Token: 0x040018A4 RID: 6308
		internal readonly byte[] typeMetadata;

		// Token: 0x040018A5 RID: 6309
		internal readonly int scratchSize;

		// Token: 0x040018A6 RID: 6310
		internal readonly int dataCount;

		// Token: 0x040018A7 RID: 6311
		internal readonly int pinCount;

		// Token: 0x040018A8 RID: 6312
		private ConcurrentSet<KeyValuePair<string, EventTags>, NameInfo> nameInfos;
	}
}
