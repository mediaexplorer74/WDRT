using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
	// Token: 0x020005D8 RID: 1496
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerVerbCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class.</summary>
		// Token: 0x06003791 RID: 14225 RVA: 0x000EFFFD File Offset: 0x000EE1FD
		public DesignerVerbCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class using the specified array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array that indicates the verbs to contain within the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003792 RID: 14226 RVA: 0x000F0005 File Offset: 0x000EE205
		public DesignerVerbCollection(DesignerVerb[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <param name="index">The index at which to get or set the <see cref="T:System.ComponentModel.Design.DesignerVerb" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at each valid index in the collection.</returns>
		// Token: 0x17000D5D RID: 3421
		public DesignerVerb this[int index]
		{
			get
			{
				return (DesignerVerb)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add to the collection.</param>
		/// <returns>The index in the collection at which the verb was added.</returns>
		// Token: 0x06003795 RID: 14229 RVA: 0x000F0036 File Offset: 0x000EE236
		public int Add(DesignerVerb value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified set of designer verbs to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003796 RID: 14230 RVA: 0x000F0044 File Offset: 0x000EE244
		public void AddRange(DesignerVerb[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the specified collection of designer verbs to the collection.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06003797 RID: 14231 RVA: 0x000F0078 File Offset: 0x000EE278
		public void AddRange(DesignerVerbCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Inserts the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <param name="index">The index in the collection at which to insert the verb.</param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to insert in the collection.</param>
		// Token: 0x06003798 RID: 14232 RVA: 0x000F00B4 File Offset: 0x000EE2B4
		public void Insert(int index, DesignerVerb value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> whose index to get in the collection.</param>
		/// <returns>The index of the specified object if it is found in the list; otherwise, -1.</returns>
		// Token: 0x06003799 RID: 14233 RVA: 0x000F00C3 File Offset: 0x000EE2C3
		public int IndexOf(DesignerVerb value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600379A RID: 14234 RVA: 0x000F00D1 File Offset: 0x000EE2D1
		public bool Contains(DesignerVerb value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove from the collection.</param>
		// Token: 0x0600379B RID: 14235 RVA: 0x000F00DF File Offset: 0x000EE2DF
		public void Remove(DesignerVerb value)
		{
			base.List.Remove(value);
		}

		/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array beginning at the specified destination index.</summary>
		/// <param name="array">The array to copy collection members to.</param>
		/// <param name="index">The destination index to begin copying to.</param>
		// Token: 0x0600379C RID: 14236 RVA: 0x000F00ED File Offset: 0x000EE2ED
		public void CopyTo(DesignerVerb[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Raises the <see langword="Set" /> event.</summary>
		/// <param name="index">The index at which to set the item.</param>
		/// <param name="oldValue">The old object.</param>
		/// <param name="newValue">The new object.</param>
		// Token: 0x0600379D RID: 14237 RVA: 0x000F00FC File Offset: 0x000EE2FC
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Raises the <see langword="Insert" /> event.</summary>
		/// <param name="index">The index at which to insert an item.</param>
		/// <param name="value">The object to insert.</param>
		// Token: 0x0600379E RID: 14238 RVA: 0x000F00FE File Offset: 0x000EE2FE
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Raises the <see langword="Clear" /> event.</summary>
		// Token: 0x0600379F RID: 14239 RVA: 0x000F0100 File Offset: 0x000EE300
		protected override void OnClear()
		{
		}

		/// <summary>Raises the <see langword="Remove" /> event.</summary>
		/// <param name="index">The index at which to remove the item.</param>
		/// <param name="value">The object to remove.</param>
		// Token: 0x060037A0 RID: 14240 RVA: 0x000F0102 File Offset: 0x000EE302
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Raises the <see langword="Validate" /> event.</summary>
		/// <param name="value">The object to validate.</param>
		// Token: 0x060037A1 RID: 14241 RVA: 0x000F0104 File Offset: 0x000EE304
		protected override void OnValidate(object value)
		{
		}
	}
}
