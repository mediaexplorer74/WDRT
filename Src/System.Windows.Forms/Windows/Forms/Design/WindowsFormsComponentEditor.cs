using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a base class for editors that use a modal dialog to display a properties page similar to an ActiveX control's property page.</summary>
	// Token: 0x0200048E RID: 1166
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class WindowsFormsComponentEditor : ComponentEditor
	{
		/// <summary>Creates an editor window that allows the user to edit the specified component, using the specified context information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
		/// <param name="component">The component to edit.</param>
		/// <returns>
		///   <see langword="true" /> if the component was changed during editing; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E3E RID: 20030 RVA: 0x00142701 File Offset: 0x00140901
		public override bool EditComponent(ITypeDescriptorContext context, object component)
		{
			return this.EditComponent(context, component, null);
		}

		/// <summary>Creates an editor window that allows the user to edit the specified component, using the specified window that owns the component.</summary>
		/// <param name="component">The component to edit.</param>
		/// <param name="owner">An <see cref="T:System.Windows.Forms.IWin32Window" /> that the component belongs to.</param>
		/// <returns>
		///   <see langword="true" /> if the component was changed during editing; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E3F RID: 20031 RVA: 0x0014270C File Offset: 0x0014090C
		public bool EditComponent(object component, IWin32Window owner)
		{
			return this.EditComponent(null, component, owner);
		}

		/// <summary>Creates an editor window that allows the user to edit the specified component.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
		/// <param name="component">The component to edit.</param>
		/// <param name="owner">An <see cref="T:System.Windows.Forms.IWin32Window" /> that the component belongs to.</param>
		/// <returns>
		///   <see langword="true" /> if the component was changed during editing; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004E40 RID: 20032 RVA: 0x00142718 File Offset: 0x00140918
		public virtual bool EditComponent(ITypeDescriptorContext context, object component, IWin32Window owner)
		{
			bool flag = false;
			Type[] componentEditorPages = this.GetComponentEditorPages();
			if (componentEditorPages != null && componentEditorPages.Length != 0)
			{
				ComponentEditorForm componentEditorForm = new ComponentEditorForm(component, componentEditorPages);
				if (componentEditorForm.ShowForm(owner, this.GetInitialComponentEditorPageIndex()) == DialogResult.OK)
				{
					flag = true;
				}
			}
			return flag;
		}

		/// <summary>Gets the component editor pages associated with the component editor.</summary>
		/// <returns>An array of component editor pages.</returns>
		// Token: 0x06004E41 RID: 20033 RVA: 0x00015C90 File Offset: 0x00013E90
		protected virtual Type[] GetComponentEditorPages()
		{
			return null;
		}

		/// <summary>Gets the index of the initial component editor page for the component editor to display.</summary>
		/// <returns>The index of the component editor page that the component editor will initially display.</returns>
		// Token: 0x06004E42 RID: 20034 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected virtual int GetInitialComponentEditorPageIndex()
		{
			return 0;
		}
	}
}
