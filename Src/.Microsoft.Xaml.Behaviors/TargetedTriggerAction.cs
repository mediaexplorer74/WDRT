using System;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x0200001B RID: 27
	public abstract class TargetedTriggerAction<T> : TargetedTriggerAction where T : class
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00004664 File Offset: 0x00002864
		protected TargetedTriggerAction()
			: base(typeof(T))
		{
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004676 File Offset: 0x00002876
		protected new T Target
		{
			get
			{
				return (T)((object)base.Target);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004683 File Offset: 0x00002883
		internal sealed override void OnTargetChangedImpl(object oldTarget, object newTarget)
		{
			base.OnTargetChangedImpl(oldTarget, newTarget);
			this.OnTargetChanged(oldTarget as T, newTarget as T);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000046A9 File Offset: 0x000028A9
		protected virtual void OnTargetChanged(T oldTarget, T newTarget)
		{
		}
	}
}
