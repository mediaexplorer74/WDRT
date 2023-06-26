using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Specifies the value of the <see cref="T:System.Runtime.InteropServices.CharSet" /> enumeration. This class cannot be inherited.</summary>
	// Token: 0x0200093D RID: 2365
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DefaultCharSetAttribute" /> class with the specified <see cref="T:System.Runtime.InteropServices.CharSet" /> value.</summary>
		/// <param name="charSet">One of the <see cref="T:System.Runtime.InteropServices.CharSet" /> values.</param>
		// Token: 0x06006076 RID: 24694 RVA: 0x0014D710 File Offset: 0x0014B910
		[__DynamicallyInvokable]
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this._CharSet = charSet;
		}

		/// <summary>Gets the default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</summary>
		/// <returns>The default value of <see cref="T:System.Runtime.InteropServices.CharSet" /> for any call to <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.</returns>
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06006077 RID: 24695 RVA: 0x0014D71F File Offset: 0x0014B91F
		[__DynamicallyInvokable]
		public CharSet CharSet
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CharSet;
			}
		}

		// Token: 0x04002B38 RID: 11064
		internal CharSet _CharSet;
	}
}
