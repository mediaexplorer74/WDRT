using System;

namespace System.ComponentModel
{
	/// <summary>Specifies how the list changed.</summary>
	// Token: 0x02000586 RID: 1414
	public enum ListChangedType
	{
		/// <summary>Much of the list has changed. Any listening controls should refresh all their data from the list.</summary>
		// Token: 0x040029C6 RID: 10694
		Reset,
		/// <summary>An item added to the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was added.</summary>
		// Token: 0x040029C7 RID: 10695
		ItemAdded,
		/// <summary>An item deleted from the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was deleted.</summary>
		// Token: 0x040029C8 RID: 10696
		ItemDeleted,
		/// <summary>An item moved within the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.OldIndex" /> contains the previous index for the item, whereas <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the new index for the item.</summary>
		// Token: 0x040029C9 RID: 10697
		ItemMoved,
		/// <summary>An item changed in the list. <see cref="P:System.ComponentModel.ListChangedEventArgs.NewIndex" /> contains the index of the item that was changed.</summary>
		// Token: 0x040029CA RID: 10698
		ItemChanged,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was added, which changed the schema.</summary>
		// Token: 0x040029CB RID: 10699
		PropertyDescriptorAdded,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was deleted, which changed the schema.</summary>
		// Token: 0x040029CC RID: 10700
		PropertyDescriptorDeleted,
		/// <summary>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> was changed, which changed the schema.</summary>
		// Token: 0x040029CD RID: 10701
		PropertyDescriptorChanged
	}
}
