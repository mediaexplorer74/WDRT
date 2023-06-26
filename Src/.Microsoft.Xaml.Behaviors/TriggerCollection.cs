using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000023 RID: 35
	public sealed class TriggerCollection : AttachableCollection<TriggerBase>
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00004F7F File Offset: 0x0000317F
		internal TriggerCollection()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004F88 File Offset: 0x00003188
		protected override void OnAttached()
		{
			foreach (TriggerBase triggerBase in this)
			{
				triggerBase.Attach(base.AssociatedObject);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004FDC File Offset: 0x000031DC
		protected override void OnDetaching()
		{
			foreach (TriggerBase triggerBase in this)
			{
				triggerBase.Detach();
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005028 File Offset: 0x00003228
		internal override void ItemAdded(TriggerBase item)
		{
			if (base.AssociatedObject != null)
			{
				item.Attach(base.AssociatedObject);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000503E File Offset: 0x0000323E
		internal override void ItemRemoved(TriggerBase item)
		{
			if (((IAttachedObject)item).AssociatedObject != null)
			{
				item.Detach();
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000504E File Offset: 0x0000324E
		protected override Freezable CreateInstanceCore()
		{
			return new TriggerCollection();
		}
	}
}
