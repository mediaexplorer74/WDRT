using System;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005F9 RID: 1529
	[Serializable]
	internal struct ConstArray
	{
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x0010412B File Offset: 0x0010232B
		public IntPtr Signature
		{
			get
			{
				return this.m_constArray;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06004692 RID: 18066 RVA: 0x00104133 File Offset: 0x00102333
		public int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000A9F RID: 2719
		public unsafe byte this[int index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index < 0 || index >= this.m_length)
				{
					throw new IndexOutOfRangeException();
				}
				return ((byte*)this.m_constArray.ToPointer())[index];
			}
		}

		// Token: 0x04001D55 RID: 7509
		internal int m_length;

		// Token: 0x04001D56 RID: 7510
		internal IntPtr m_constArray;
	}
}
