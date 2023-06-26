using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200001F RID: 31
	public class TriggerActionCollection : AttachableCollection<TriggerAction>
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00004BD7 File Offset: 0x00002DD7
		internal TriggerActionCollection()
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004BE0 File Offset: 0x00002DE0
		protected override void OnAttached()
		{
			foreach (TriggerAction triggerAction in this)
			{
				triggerAction.Attach(base.AssociatedObject);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004C34 File Offset: 0x00002E34
		protected override void OnDetaching()
		{
			foreach (TriggerAction triggerAction in this)
			{
				triggerAction.Detach();
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004C80 File Offset: 0x00002E80
		internal override void ItemAdded(TriggerAction item)
		{
			if (item.IsHosted)
			{
				throw new InvalidOperationException(ExceptionStringTable.CannotHostTriggerActionMultipleTimesExceptionMessage);
			}
			if (base.AssociatedObject != null)
			{
				item.Attach(base.AssociatedObject);
			}
			item.IsHosted = true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004CB0 File Offset: 0x00002EB0
		internal override void ItemRemoved(TriggerAction item)
		{
			if (((IAttachedObject)item).AssociatedObject != null)
			{
				item.Detach();
			}
			item.IsHosted = false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004CC7 File Offset: 0x00002EC7
		protected override Freezable CreateInstanceCore()
		{
			return new TriggerActionCollection();
		}
	}
}
