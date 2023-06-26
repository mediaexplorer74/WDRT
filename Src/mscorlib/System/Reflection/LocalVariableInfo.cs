using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a local variable and provides access to local variable metadata.</summary>
	// Token: 0x02000612 RID: 1554
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public class LocalVariableInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.LocalVariableInfo" /> class.</summary>
		// Token: 0x06004823 RID: 18467 RVA: 0x0010783E File Offset: 0x00105A3E
		[__DynamicallyInvokable]
		protected LocalVariableInfo()
		{
		}

		/// <summary>Returns a user-readable string that describes the local variable.</summary>
		/// <returns>A string that displays information about the local variable, including the type name, index, and pinned status.</returns>
		// Token: 0x06004824 RID: 18468 RVA: 0x00107848 File Offset: 0x00105A48
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string text = this.LocalType.ToString() + " (" + this.LocalIndex.ToString() + ")";
			if (this.IsPinned)
			{
				text += " (pinned)";
			}
			return text;
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>The type of the local variable.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x00107893 File Offset: 0x00105A93
		[__DynamicallyInvokable]
		public virtual Type LocalType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_type;
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>
		///   <see langword="true" /> if the object referred to by the variable is pinned in memory; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06004826 RID: 18470 RVA: 0x0010789B File Offset: 0x00105A9B
		[__DynamicallyInvokable]
		public virtual bool IsPinned
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isPinned != 0;
			}
		}

		/// <summary>Gets the index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004827 RID: 18471 RVA: 0x001078A6 File Offset: 0x00105AA6
		[__DynamicallyInvokable]
		public virtual int LocalIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_localIndex;
			}
		}

		// Token: 0x04001DE5 RID: 7653
		private RuntimeType m_type;

		// Token: 0x04001DE6 RID: 7654
		private int m_isPinned;

		// Token: 0x04001DE7 RID: 7655
		private int m_localIndex;
	}
}
