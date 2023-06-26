using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Specifies the name of the return value of a method in a Windows Runtime component.</summary>
	// Token: 0x020009C9 RID: 2505
	[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ReturnValueNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.ReturnValueNameAttribute" /> class, and specifies the name of the return value.</summary>
		/// <param name="name">The name of the return value.</param>
		// Token: 0x060063E1 RID: 25569 RVA: 0x001560A5 File Offset: 0x001542A5
		[__DynamicallyInvokable]
		public ReturnValueNameAttribute(string name)
		{
			this.m_Name = name;
		}

		/// <summary>Gets the name that was specified for the return value of a method in a Windows Runtime component.</summary>
		/// <returns>The name of the method's return value.</returns>
		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060063E2 RID: 25570 RVA: 0x001560B4 File Offset: 0x001542B4
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x04002CE8 RID: 11496
		private string m_Name;
	}
}
