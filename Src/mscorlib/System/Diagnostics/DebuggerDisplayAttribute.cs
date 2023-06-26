using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines how a class or field is displayed in the debugger variable windows.</summary>
	// Token: 0x020003ED RID: 1005
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" /> class.</summary>
		/// <param name="value">The string to be displayed in the value column for instances of the type; an empty string ("") causes the value column to be hidden.</param>
		// Token: 0x06003331 RID: 13105 RVA: 0x000C64CD File Offset: 0x000C46CD
		[__DynamicallyInvokable]
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				this.value = "";
			}
			else
			{
				this.value = value;
			}
			this.name = "";
			this.type = "";
		}

		/// <summary>Gets the string to display in the value column of the debugger variable windows.</summary>
		/// <returns>The string to display in the value column of the debugger variable.</returns>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C6502 File Offset: 0x000C4702
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets or sets the name to display in the debugger variable windows.</summary>
		/// <returns>The name to display in the debugger variable windows.</returns>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06003333 RID: 13107 RVA: 0x000C650A File Offset: 0x000C470A
		// (set) Token: 0x06003334 RID: 13108 RVA: 0x000C6512 File Offset: 0x000C4712
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the string to display in the type column of the debugger variable windows.</summary>
		/// <returns>The string to display in the type column of the debugger variable windows.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06003335 RID: 13109 RVA: 0x000C651B File Offset: 0x000C471B
		// (set) Token: 0x06003336 RID: 13110 RVA: 0x000C6523 File Offset: 0x000C4723
		[__DynamicallyInvokable]
		public string Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.type;
			}
			[__DynamicallyInvokable]
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the type of the attribute's target.</summary>
		/// <returns>The attribute's target type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerDisplayAttribute.Target" /> is set to <see langword="null" />.</exception>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000C6555 File Offset: 0x000C4755
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x000C652C File Offset: 0x000C472C
		[__DynamicallyInvokable]
		public Type Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.target;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the type name of the attribute's target.</summary>
		/// <returns>The name of the attribute's target type.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000C655D File Offset: 0x000C475D
		// (set) Token: 0x0600333A RID: 13114 RVA: 0x000C6565 File Offset: 0x000C4765
		[__DynamicallyInvokable]
		public string TargetTypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.targetName;
			}
			[__DynamicallyInvokable]
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040016B3 RID: 5811
		private string name;

		// Token: 0x040016B4 RID: 5812
		private string value;

		// Token: 0x040016B5 RID: 5813
		private string type;

		// Token: 0x040016B6 RID: 5814
		private string targetName;

		// Token: 0x040016B7 RID: 5815
		private Type target;
	}
}
