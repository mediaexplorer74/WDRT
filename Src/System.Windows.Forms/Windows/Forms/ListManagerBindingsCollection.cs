using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020002D1 RID: 721
	[DefaultEvent("CollectionChanged")]
	internal class ListManagerBindingsCollection : BindingsCollection
	{
		// Token: 0x06002CB5 RID: 11445 RVA: 0x000C8E58 File Offset: 0x000C7058
		internal ListManagerBindingsCollection(BindingManagerBase bindingManagerBase)
		{
			this.bindingManagerBase = bindingManagerBase;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000C8E68 File Offset: 0x000C7068
		protected override void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.BindingManagerBase == this.bindingManagerBase)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd1"), "dataBinding");
			}
			if (dataBinding.BindingManagerBase != null)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd2"), "dataBinding");
			}
			dataBinding.SetListManager(this.bindingManagerBase);
			base.AddCore(dataBinding);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000C8ED8 File Offset: 0x000C70D8
		protected override void ClearCore()
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Binding binding = base[i];
				binding.SetListManager(null);
			}
			base.ClearCore();
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000C8F0D File Offset: 0x000C710D
		protected override void RemoveCore(Binding dataBinding)
		{
			if (dataBinding.BindingManagerBase != this.bindingManagerBase)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionForeign"));
			}
			dataBinding.SetListManager(null);
			base.RemoveCore(dataBinding);
		}

		// Token: 0x0400128C RID: 4748
		private BindingManagerBase bindingManagerBase;
	}
}
