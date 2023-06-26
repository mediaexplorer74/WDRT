using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for code access security.</summary>
	// Token: 0x020002EE RID: 750
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeAccessSecurityAttribute : SecurityAttribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002676 RID: 9846 RVA: 0x0008DB83 File Offset: 0x0008BD83
		protected CodeAccessSecurityAttribute(SecurityAction action)
			: base(action)
		{
		}
	}
}
