using System;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072E RID: 1838
	internal sealed class SurrogateForCyclicalReference : ISerializationSurrogate
	{
		// Token: 0x060051BA RID: 20922 RVA: 0x001214A6 File Offset: 0x0011F6A6
		internal SurrogateForCyclicalReference(ISerializationSurrogate innerSurrogate)
		{
			if (innerSurrogate == null)
			{
				throw new ArgumentNullException("innerSurrogate");
			}
			this.innerSurrogate = innerSurrogate;
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x001214C3 File Offset: 0x0011F6C3
		[SecurityCritical]
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			this.innerSurrogate.GetObjectData(obj, info, context);
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x001214D3 File Offset: 0x0011F6D3
		[SecurityCritical]
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			return this.innerSurrogate.SetObjectData(obj, info, context, selector);
		}

		// Token: 0x04002447 RID: 9287
		private ISerializationSurrogate innerSurrogate;
	}
}
