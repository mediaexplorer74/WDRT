using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event.</summary>
	// Token: 0x02000584 RID: 1412
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the index of the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added, changed, or removed.</param>
		// Token: 0x0600341D RID: 13341 RVA: 0x000E43B2 File Offset: 0x000E25B2
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex)
			: this(listChangedType, newIndex, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change, the index of the affected item, and a <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added or changed.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the item.</param>
		// Token: 0x0600341E RID: 13342 RVA: 0x000E43BD File Offset: 0x000E25BD
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc)
			: this(listChangedType, newIndex)
		{
			this.propDesc = propDesc;
			this.oldIndex = newIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, removed, or changed.</param>
		// Token: 0x0600341F RID: 13343 RVA: 0x000E43D5 File Offset: 0x000E25D5
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.listChangedType = listChangedType;
			this.propDesc = propDesc;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the old and new index of the item that was moved.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The new index of the item that was moved.</param>
		/// <param name="oldIndex">The old index of the item that was moved.</param>
		// Token: 0x06003420 RID: 13344 RVA: 0x000E43EB File Offset: 0x000E25EB
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.listChangedType = listChangedType;
			this.newIndex = newIndex;
			this.oldIndex = oldIndex;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</returns>
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x000E4408 File Offset: 0x000E2608
		public ListChangedType ListChangedType
		{
			get
			{
				return this.listChangedType;
			}
		}

		/// <summary>Gets the index of the item affected by the change.</summary>
		/// <returns>The index of the affected by the change.</returns>
		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000E4410 File Offset: 0x000E2610
		public int NewIndex
		{
			get
			{
				return this.newIndex;
			}
		}

		/// <summary>Gets the old index of an item that has been moved.</summary>
		/// <returns>The old index of the moved item.</returns>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x000E4418 File Offset: 0x000E2618
		public int OldIndex
		{
			get
			{
				return this.oldIndex;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, changed, or deleted.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected by the change.</returns>
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000E4420 File Offset: 0x000E2620
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propDesc;
			}
		}

		// Token: 0x040029C1 RID: 10689
		private ListChangedType listChangedType;

		// Token: 0x040029C2 RID: 10690
		private int newIndex;

		// Token: 0x040029C3 RID: 10691
		private int oldIndex;

		// Token: 0x040029C4 RID: 10692
		private PropertyDescriptor propDesc;
	}
}
