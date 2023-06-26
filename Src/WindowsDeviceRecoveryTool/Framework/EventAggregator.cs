using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.WindowsDeviceRecoveryTool.Common;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x0200008A RID: 138
	[Export]
	public sealed class EventAggregator
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x00017474 File Offset: 0x00015674
		public void Subscribe(ICanHandle instance)
		{
			List<WeakReference> list = this.subscribers;
			lock (list)
			{
				bool flag2 = this.subscribers.Any((WeakReference reference) => reference.Target == instance);
				if (!flag2)
				{
					this.subscribers.Add(new WeakReference(instance));
				}
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000174F8 File Offset: 0x000156F8
		public void Unsubscribe(ICanHandle instance)
		{
			List<WeakReference> list = this.subscribers;
			lock (list)
			{
				WeakReference weakReference = this.subscribers.FirstOrDefault((WeakReference reference) => reference.Target == instance);
				bool flag2 = weakReference != null;
				if (flag2)
				{
					this.subscribers.Remove(weakReference);
				}
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00017578 File Offset: 0x00015778
		public void Publish<TMessage>(TMessage message)
		{
			this.Publish<TMessage>(message, new Action<Action>(this.Execute));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001758F File Offset: 0x0001578F
		private void Execute(Action action)
		{
			AppDispatcher.Execute(action, false);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001759C File Offset: 0x0001579C
		private void Publish<TMessage>(TMessage message, Action<Action> executor)
		{
			List<WeakReference> list = this.subscribers;
			WeakReference[] objectsToNotify;
			lock (list)
			{
				objectsToNotify = this.subscribers.ToArray();
			}
			executor(delegate
			{
				List<WeakReference> list2 = new List<WeakReference>();
				foreach (WeakReference weakReference in objectsToNotify)
				{
					ICanHandle<TMessage> canHandle = weakReference.Target as ICanHandle<TMessage>;
					bool flag2 = canHandle != null;
					if (flag2)
					{
						canHandle.Handle(message);
					}
					else
					{
						bool flag3 = !weakReference.IsAlive;
						if (flag3)
						{
							list2.Add(weakReference);
						}
					}
				}
				bool flag4 = list2.Count > 0;
				if (flag4)
				{
					List<WeakReference> list3 = this.subscribers;
					lock (list3)
					{
						foreach (WeakReference weakReference2 in list2)
						{
							this.subscribers.Remove(weakReference2);
						}
					}
				}
			});
		}

		// Token: 0x0400022A RID: 554
		private readonly List<WeakReference> subscribers = new List<WeakReference>();
	}
}
