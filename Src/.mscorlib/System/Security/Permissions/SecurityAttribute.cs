using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the base attribute class for declarative security from which <see cref="T:System.Security.Permissions.CodeAccessSecurityAttribute" /> is derived.</summary>
	// Token: 0x020002ED RID: 749
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Security.Permissions.SecurityAttribute" /> with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x0600266F RID: 9839 RVA: 0x0008DB10 File Offset: 0x0008BD10
		protected SecurityAttribute(SecurityAction action)
		{
			this.m_action = action;
		}

		/// <summary>Gets or sets a security action.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x0008DB1F File Offset: 0x0008BD1F
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x0008DB27 File Offset: 0x0008BD27
		public SecurityAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether full (unrestricted) permission to the resource protected by the attribute is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if full permission to the protected resource is declared; otherwise, <see langword="false" />.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x0008DB30 File Offset: 0x0008BD30
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x0008DB38 File Offset: 0x0008BD38
		public bool Unrestricted
		{
			get
			{
				return this.m_unrestricted;
			}
			set
			{
				this.m_unrestricted = value;
			}
		}

		/// <summary>When overridden in a derived class, creates a permission object that can then be serialized into binary form and persistently stored along with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>A serializable permission object.</returns>
		// Token: 0x06002674 RID: 9844
		public abstract IPermission CreatePermission();

		// Token: 0x06002675 RID: 9845 RVA: 0x0008DB44 File Offset: 0x0008BD44
		[SecurityCritical]
		internal static IntPtr FindSecurityAttributeTypeHandle(string typeName)
		{
			PermissionSet.s_fullTrust.Assert();
			Type type = Type.GetType(typeName, false, false);
			if (type == null)
			{
				return IntPtr.Zero;
			}
			return type.TypeHandle.Value;
		}

		// Token: 0x04000EEE RID: 3822
		internal SecurityAction m_action;

		// Token: 0x04000EEF RID: 3823
		internal bool m_unrestricted;
	}
}
