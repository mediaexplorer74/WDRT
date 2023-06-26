using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086B RID: 2155
	[Serializable]
	internal class SerializationMonkey : ISerializable, IFieldInfo
	{
		// Token: 0x06005BDF RID: 23519 RVA: 0x001438FD File Offset: 0x00141AFD
		[SecurityCritical]
		internal SerializationMonkey(SerializationInfo info, StreamingContext ctx)
		{
			this._obj.RootSetObjectData(info, ctx);
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x00143912 File Offset: 0x00141B12
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x00143923 File Offset: 0x00141B23
		// (set) Token: 0x06005BE2 RID: 23522 RVA: 0x0014392B File Offset: 0x00141B2B
		public string[] FieldNames
		{
			[SecurityCritical]
			get
			{
				return this.fieldNames;
			}
			[SecurityCritical]
			set
			{
				this.fieldNames = value;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005BE3 RID: 23523 RVA: 0x00143934 File Offset: 0x00141B34
		// (set) Token: 0x06005BE4 RID: 23524 RVA: 0x0014393C File Offset: 0x00141B3C
		public Type[] FieldTypes
		{
			[SecurityCritical]
			get
			{
				return this.fieldTypes;
			}
			[SecurityCritical]
			set
			{
				this.fieldTypes = value;
			}
		}

		// Token: 0x04002987 RID: 10631
		internal ISerializationRootObject _obj;

		// Token: 0x04002988 RID: 10632
		internal string[] fieldNames;

		// Token: 0x04002989 RID: 10633
		internal Type[] fieldTypes;
	}
}
