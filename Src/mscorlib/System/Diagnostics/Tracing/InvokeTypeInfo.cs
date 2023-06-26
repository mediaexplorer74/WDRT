using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000448 RID: 1096
	internal sealed class InvokeTypeInfo<ContainerType> : TraceLoggingTypeInfo<ContainerType>
	{
		// Token: 0x0600365D RID: 13917 RVA: 0x000D4444 File Offset: 0x000D2644
		public InvokeTypeInfo(TypeAnalysis typeAnalysis)
			: base(typeAnalysis.name, typeAnalysis.level, typeAnalysis.opcode, typeAnalysis.keywords, typeAnalysis.tags)
		{
			if (typeAnalysis.properties.Length != 0)
			{
				this.properties = typeAnalysis.properties;
				this.accessors = new PropertyAccessor<ContainerType>[this.properties.Length];
				for (int i = 0; i < this.accessors.Length; i++)
				{
					this.accessors[i] = PropertyAccessor<ContainerType>.Create(this.properties[i]);
				}
			}
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000D44C8 File Offset: 0x000D26C8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					EventFieldFormat eventFieldFormat = EventFieldFormat.Default;
					EventFieldAttribute fieldAttribute = propertyAnalysis.fieldAttribute;
					if (fieldAttribute != null)
					{
						traceLoggingMetadataCollector.Tags = fieldAttribute.Tags;
						eventFieldFormat = fieldAttribute.Format;
					}
					propertyAnalysis.typeInfo.WriteMetadata(traceLoggingMetadataCollector, propertyAnalysis.name, eventFieldFormat);
				}
			}
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000D4538 File Offset: 0x000D2738
		public override void WriteData(TraceLoggingDataCollector collector, ref ContainerType value)
		{
			if (this.accessors != null)
			{
				foreach (PropertyAccessor<ContainerType> propertyAccessor in this.accessors)
				{
					propertyAccessor.Write(collector, ref value);
				}
			}
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000D4570 File Offset: 0x000D2770
		public override object GetData(object value)
		{
			if (this.properties != null)
			{
				List<string> list = new List<string>();
				List<object> list2 = new List<object>();
				for (int i = 0; i < this.properties.Length; i++)
				{
					object data = this.accessors[i].GetData((ContainerType)((object)value));
					list.Add(this.properties[i].name);
					list2.Add(this.properties[i].typeInfo.GetData(data));
				}
				return new EventPayload(list, list2);
			}
			return null;
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000D45F0 File Offset: 0x000D27F0
		public override void WriteObjectData(TraceLoggingDataCollector collector, object valueObj)
		{
			if (this.accessors != null)
			{
				ContainerType containerType = ((valueObj == null) ? default(ContainerType) : ((ContainerType)((object)valueObj)));
				this.WriteData(collector, ref containerType);
			}
		}

		// Token: 0x04001851 RID: 6225
		private readonly PropertyAnalysis[] properties;

		// Token: 0x04001852 RID: 6226
		private readonly PropertyAccessor<ContainerType>[] accessors;
	}
}
