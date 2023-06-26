using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A77 RID: 2679
	[Serializable]
	internal sealed class MLangCodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x060068CA RID: 26826 RVA: 0x0016387C File Offset: 0x00161A7C
		internal MLangCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
			}
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x00163940 File Offset: 0x00161B40
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			this.realEncoding = Encoding.GetEncoding(this.m_codePage);
			if (!this.m_deserializedFromEverett && !this.m_isReadOnly)
			{
				this.realEncoding = (Encoding)this.realEncoding.Clone();
				this.realEncoding.EncoderFallback = this.encoderFallback;
				this.realEncoding.DecoderFallback = this.decoderFallback;
			}
			return this.realEncoding;
		}

		// Token: 0x060068CC RID: 26828 RVA: 0x001639AC File Offset: 0x00161BAC
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002EDE RID: 11998
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04002EDF RID: 11999
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04002EE0 RID: 12000
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04002EE1 RID: 12001
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04002EE2 RID: 12002
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04002EE3 RID: 12003
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000CB0 RID: 3248
		[Serializable]
		internal sealed class MLangEncoder : ISerializable, IObjectReference
		{
			// Token: 0x060071A3 RID: 29091 RVA: 0x00188148 File Offset: 0x00186348
			internal MLangEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x060071A4 RID: 29092 RVA: 0x0018817E File Offset: 0x0018637E
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetEncoder();
			}

			// Token: 0x060071A5 RID: 29093 RVA: 0x0018818B File Offset: 0x0018638B
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040038C2 RID: 14530
			[NonSerialized]
			private Encoding realEncoding;
		}

		// Token: 0x02000CB1 RID: 3249
		[Serializable]
		internal sealed class MLangDecoder : ISerializable, IObjectReference
		{
			// Token: 0x060071A6 RID: 29094 RVA: 0x0018819C File Offset: 0x0018639C
			internal MLangDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x060071A7 RID: 29095 RVA: 0x001881D2 File Offset: 0x001863D2
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x060071A8 RID: 29096 RVA: 0x001881DF File Offset: 0x001863DF
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040038C3 RID: 14531
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
