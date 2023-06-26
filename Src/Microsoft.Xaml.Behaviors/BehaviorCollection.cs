using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000006 RID: 6
	public sealed class BehaviorCollection : AttachableCollection<Behavior>
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000025E7 File Offset: 0x000007E7
		internal BehaviorCollection()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025F0 File Offset: 0x000007F0
		protected override void OnAttached()
		{
			foreach (Behavior behavior in this)
			{
				behavior.Attach(base.AssociatedObject);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002644 File Offset: 0x00000844
		protected override void OnDetaching()
		{
			foreach (Behavior behavior in this)
			{
				behavior.Detach();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002690 File Offset: 0x00000890
		internal override void ItemAdded(Behavior item)
		{
			if (base.AssociatedObject != null)
			{
				item.Attach(base.AssociatedObject);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026A6 File Offset: 0x000008A6
		internal override void ItemRemoved(Behavior item)
		{
			if (((IAttachedObject)item).AssociatedObject != null)
			{
				item.Detach();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026B6 File Offset: 0x000008B6
		protected override Freezable CreateInstanceCore()
		{
			return new BehaviorCollection();
		}
	}
}
