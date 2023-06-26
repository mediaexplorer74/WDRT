using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200075A RID: 1882
	internal class ValueTypeFixupInfo
	{
		// Token: 0x0600530C RID: 21260 RVA: 0x001252A0 File Offset: 0x001234A0
		public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
		{
			if (member == null && parentIndex == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustSupplyParent"));
			}
			if (containerID == 0L && member == null)
			{
				this.m_containerID = containerID;
				this.m_parentField = member;
				this.m_parentIndex = parentIndex;
			}
			if (member != null)
			{
				if (parentIndex != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MemberAndArray"));
				}
				if (member.FieldType.IsValueType && containerID == 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustSupplyContainer"));
				}
			}
			this.m_containerID = containerID;
			this.m_parentField = member;
			this.m_parentIndex = parentIndex;
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x0600530D RID: 21261 RVA: 0x00125341 File Offset: 0x00123541
		public long ContainerID
		{
			get
			{
				return this.m_containerID;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x0600530E RID: 21262 RVA: 0x00125349 File Offset: 0x00123549
		public FieldInfo ParentField
		{
			get
			{
				return this.m_parentField;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600530F RID: 21263 RVA: 0x00125351 File Offset: 0x00123551
		public int[] ParentIndex
		{
			get
			{
				return this.m_parentIndex;
			}
		}

		// Token: 0x040024D1 RID: 9425
		private long m_containerID;

		// Token: 0x040024D2 RID: 9426
		private FieldInfo m_parentField;

		// Token: 0x040024D3 RID: 9427
		private int[] m_parentIndex;
	}
}
