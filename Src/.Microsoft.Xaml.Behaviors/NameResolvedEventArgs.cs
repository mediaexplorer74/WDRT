using System;

namespace Microsoft.Xaml.Behaviors
{
	// Token: 0x02000017 RID: 23
	internal sealed class NameResolvedEventArgs : EventArgs
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000040C8 File Offset: 0x000022C8
		public object OldObject
		{
			get
			{
				return this.oldObject;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000040D0 File Offset: 0x000022D0
		public object NewObject
		{
			get
			{
				return this.newObject;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000040D8 File Offset: 0x000022D8
		public NameResolvedEventArgs(object oldObject, object newObject)
		{
			this.oldObject = oldObject;
			this.newObject = newObject;
		}

		// Token: 0x04000045 RID: 69
		private object oldObject;

		// Token: 0x04000046 RID: 70
		private object newObject;
	}
}
