using System;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200000F RID: 15
	public abstract class EventTriggerBase<T> : EventTriggerBase where T : class
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002C5C File Offset: 0x00000E5C
		protected EventTriggerBase()
			: base(typeof(T))
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002C6E File Offset: 0x00000E6E
		public new T Source
		{
			get
			{
				return (T)((object)base.Source);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C7B File Offset: 0x00000E7B
		internal sealed override void OnSourceChangedImpl(object oldSource, object newSource)
		{
			base.OnSourceChangedImpl(oldSource, newSource);
			this.OnSourceChanged(oldSource as T, newSource as T);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CA1 File Offset: 0x00000EA1
		protected virtual void OnSourceChanged(T oldSource, T newSource)
		{
		}
	}
}
