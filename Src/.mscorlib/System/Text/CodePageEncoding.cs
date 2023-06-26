using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A5A RID: 2650
	[Serializable]
	internal sealed class CodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06006775 RID: 26485 RVA: 0x0015E990 File Offset: 0x0015CB90
		internal CodePageEncoding(SerializationInfo info, StreamingContext context)
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

		// Token: 0x06006776 RID: 26486 RVA: 0x0015EA54 File Offset: 0x0015CC54
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

		// Token: 0x06006777 RID: 26487 RVA: 0x0015EAC0 File Offset: 0x0015CCC0
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002E3C RID: 11836
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04002E3D RID: 11837
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04002E3E RID: 11838
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04002E3F RID: 11839
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04002E40 RID: 11840
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04002E41 RID: 11841
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000CA9 RID: 3241
		[Serializable]
		internal sealed class Decoder : ISerializable, IObjectReference
		{
			// Token: 0x0600716C RID: 29036 RVA: 0x0018773B File Offset: 0x0018593B
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x0600716D RID: 29037 RVA: 0x00187771 File Offset: 0x00185971
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x0600716E RID: 29038 RVA: 0x0018777E File Offset: 0x0018597E
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x0400389F RID: 14495
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
