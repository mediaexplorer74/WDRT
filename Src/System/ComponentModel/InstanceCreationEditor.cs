using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Creates an instance of a particular type of property from a drop-down box within the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x0200056C RID: 1388
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class InstanceCreationEditor
	{
		/// <summary>Gets the specified text.</summary>
		/// <returns>The specified text.</returns>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000E38A0 File Offset: 0x000E1AA0
		public virtual string Text
		{
			get
			{
				return SR.GetString("InstanceCreationEditorDefaultText");
			}
		}

		/// <summary>When overridden in a derived class, returns an instance of the specified type.</summary>
		/// <param name="context">The context information.</param>
		/// <param name="instanceType">The specified type.</param>
		/// <returns>An instance of the specified type or <see langword="null" />.</returns>
		// Token: 0x060033AE RID: 13230
		public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);
	}
}
