using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to commit or rollback changes to an object that is used as a data source.</summary>
	// Token: 0x02000560 RID: 1376
	[global::__DynamicallyInvokable]
	public interface IEditableObject
	{
		/// <summary>Begins an edit on an object.</summary>
		// Token: 0x0600338E RID: 13198
		[global::__DynamicallyInvokable]
		void BeginEdit();

		/// <summary>Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.</summary>
		// Token: 0x0600338F RID: 13199
		[global::__DynamicallyInvokable]
		void EndEdit();

		/// <summary>Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.</summary>
		// Token: 0x06003390 RID: 13200
		[global::__DynamicallyInvokable]
		void CancelEdit();
	}
}
