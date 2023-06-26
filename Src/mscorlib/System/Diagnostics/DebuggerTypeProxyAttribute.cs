using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the display proxy for a type.</summary>
	// Token: 0x020003EC RID: 1004
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type of the proxy.</summary>
		/// <param name="type">The proxy type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x0600332A RID: 13098 RVA: 0x000C644C File Offset: 0x000C464C
		[__DynamicallyInvokable]
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type name of the proxy.</summary>
		/// <param name="typeName">The type name of the proxy type.</param>
		// Token: 0x0600332B RID: 13099 RVA: 0x000C6474 File Offset: 0x000C4674
		[__DynamicallyInvokable]
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets the type name of the proxy type.</summary>
		/// <returns>The type name of the proxy type.</returns>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000C6483 File Offset: 0x000C4683
		[__DynamicallyInvokable]
		public string ProxyTypeName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets or sets the target type for the attribute.</summary>
		/// <returns>The target type for the attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> is set to <see langword="null" />.</exception>
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000C64B4 File Offset: 0x000C46B4
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x000C648B File Offset: 0x000C468B
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

		/// <summary>Gets or sets the name of the target type.</summary>
		/// <returns>The name of the target type.</returns>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C64BC File Offset: 0x000C46BC
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x000C64C4 File Offset: 0x000C46C4
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

		// Token: 0x040016B0 RID: 5808
		private string typeName;

		// Token: 0x040016B1 RID: 5809
		private string targetName;

		// Token: 0x040016B2 RID: 5810
		private Type target;
	}
}
