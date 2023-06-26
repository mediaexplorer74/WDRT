using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000483 RID: 1155
	internal class TraceLoggingMetadataCollector
	{
		// Token: 0x0600375D RID: 14173 RVA: 0x000D649F File Offset: 0x000D469F
		internal TraceLoggingMetadataCollector()
		{
			this.impl = new TraceLoggingMetadataCollector.Impl();
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000D64BD File Offset: 0x000D46BD
		private TraceLoggingMetadataCollector(TraceLoggingMetadataCollector other, FieldMetadata group)
		{
			this.impl = other.impl;
			this.currentGroup = group;
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600375F RID: 14175 RVA: 0x000D64E3 File Offset: 0x000D46E3
		// (set) Token: 0x06003760 RID: 14176 RVA: 0x000D64EB File Offset: 0x000D46EB
		internal EventFieldTags Tags { get; set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000D64F4 File Offset: 0x000D46F4
		internal int ScratchSize
		{
			get
			{
				return (int)this.impl.scratchSize;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x000D6501 File Offset: 0x000D4701
		internal int DataCount
		{
			get
			{
				return (int)this.impl.dataCount;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000D650E File Offset: 0x000D470E
		internal int PinCount
		{
			get
			{
				return (int)this.impl.pinCount;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000D651B File Offset: 0x000D471B
		private bool BeginningBufferedArray
		{
			get
			{
				return this.bufferedArrayFieldCount == 0;
			}
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x000D6528 File Offset: 0x000D4728
		public TraceLoggingMetadataCollector AddGroup(string name)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = this;
			if (name != null || this.BeginningBufferedArray)
			{
				FieldMetadata fieldMetadata = new FieldMetadata(name, TraceLoggingDataType.Struct, this.Tags, this.BeginningBufferedArray);
				this.AddField(fieldMetadata);
				traceLoggingMetadataCollector = new TraceLoggingMetadataCollector(this, fieldMetadata);
			}
			return traceLoggingMetadataCollector;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000D6568 File Offset: 0x000D4768
		public void AddScalar(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			int num;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
				break;
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
				goto IL_6F;
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.HexInt32:
				num = 4;
				goto IL_8B;
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt64:
				num = 8;
				goto IL_8B;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case (TraceLoggingDataType)19:
				goto IL_80;
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.SystemTime:
				num = 16;
				goto IL_8B;
			default:
				if (traceLoggingDataType != TraceLoggingDataType.Char8)
				{
					if (traceLoggingDataType != TraceLoggingDataType.Char16)
					{
						goto IL_80;
					}
					goto IL_6F;
				}
				break;
			}
			num = 1;
			goto IL_8B;
			IL_6F:
			num = 2;
			goto IL_8B;
			IL_80:
			throw new ArgumentOutOfRangeException("type");
			IL_8B:
			this.impl.AddScalar(num);
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000D6628 File Offset: 0x000D4828
		public void AddBinary(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			if (traceLoggingDataType != TraceLoggingDataType.Binary && traceLoggingDataType - TraceLoggingDataType.CountedUtf16String > 1)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000D6684 File Offset: 0x000D4884
		public void AddArray(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Utf16String:
			case TraceLoggingDataType.MbcsString:
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt32:
			case TraceLoggingDataType.HexInt64:
				goto IL_7C;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case TraceLoggingDataType.SystemTime:
			case (TraceLoggingDataType)19:
				break;
			default:
				if (traceLoggingDataType == TraceLoggingDataType.Char8 || traceLoggingDataType == TraceLoggingDataType.Char16)
				{
					goto IL_7C;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_7C:
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, true));
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000D6750 File Offset: 0x000D4950
		public void BeginBufferedArray()
		{
			if (this.bufferedArrayFieldCount >= 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedNestedArraysEnums"));
			}
			this.bufferedArrayFieldCount = 0;
			this.impl.BeginBuffered();
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000D677D File Offset: 0x000D497D
		public void EndBufferedArray()
		{
			if (this.bufferedArrayFieldCount != 1)
			{
				throw new InvalidOperationException(Environment.GetResourceString("EventSource_IncorrentlyAuthoredTypeInfo"));
			}
			this.bufferedArrayFieldCount = int.MinValue;
			this.impl.EndBuffered();
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000D67B0 File Offset: 0x000D49B0
		public void AddCustom(string name, TraceLoggingDataType type, byte[] metadata)
		{
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(Environment.GetResourceString("EventSource_NotSupportedCustomSerializedData"));
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, metadata));
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x000D6800 File Offset: 0x000D4A00
		internal byte[] GetMetadata()
		{
			int num = this.impl.Encode(null);
			byte[] array = new byte[num];
			this.impl.Encode(array);
			return array;
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000D682F File Offset: 0x000D4A2F
		private void AddField(FieldMetadata fieldMetadata)
		{
			this.Tags = EventFieldTags.None;
			this.bufferedArrayFieldCount++;
			this.impl.fields.Add(fieldMetadata);
			if (this.currentGroup != null)
			{
				this.currentGroup.IncrementStructFieldCount();
			}
		}

		// Token: 0x040018A9 RID: 6313
		private readonly TraceLoggingMetadataCollector.Impl impl;

		// Token: 0x040018AA RID: 6314
		private readonly FieldMetadata currentGroup;

		// Token: 0x040018AB RID: 6315
		private int bufferedArrayFieldCount = int.MinValue;

		// Token: 0x02000B9A RID: 2970
		private class Impl
		{
			// Token: 0x06006CBD RID: 27837 RVA: 0x00179779 File Offset: 0x00177979
			public void AddScalar(int size)
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						if (!this.scalar)
						{
							this.dataCount += 1;
						}
						this.scalar = true;
						this.scratchSize = (short)((int)this.scratchSize + size);
					}
				}
			}

			// Token: 0x06006CBE RID: 27838 RVA: 0x001797B0 File Offset: 0x001779B0
			public void AddNonscalar()
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						this.scalar = false;
						this.pinCount += 1;
						this.dataCount += 1;
					}
				}
			}

			// Token: 0x06006CBF RID: 27839 RVA: 0x001797DF File Offset: 0x001779DF
			public void BeginBuffered()
			{
				if (this.bufferNesting == 0)
				{
					this.AddNonscalar();
				}
				this.bufferNesting++;
			}

			// Token: 0x06006CC0 RID: 27840 RVA: 0x001797FD File Offset: 0x001779FD
			public void EndBuffered()
			{
				this.bufferNesting--;
			}

			// Token: 0x06006CC1 RID: 27841 RVA: 0x00179810 File Offset: 0x00177A10
			public int Encode(byte[] metadata)
			{
				int num = 0;
				foreach (FieldMetadata fieldMetadata in this.fields)
				{
					fieldMetadata.Encode(ref num, metadata);
				}
				return num;
			}

			// Token: 0x04003531 RID: 13617
			internal readonly List<FieldMetadata> fields = new List<FieldMetadata>();

			// Token: 0x04003532 RID: 13618
			internal short scratchSize;

			// Token: 0x04003533 RID: 13619
			internal sbyte dataCount;

			// Token: 0x04003534 RID: 13620
			internal sbyte pinCount;

			// Token: 0x04003535 RID: 13621
			private int bufferNesting;

			// Token: 0x04003536 RID: 13622
			private bool scalar;
		}
	}
}
