using System;
using System.Windows.Input;

namespace Microsoft.WindowsDeviceRecoveryTool.Framework
{
	// Token: 0x02000088 RID: 136
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class CustomCommandAttribute : Attribute
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00017224 File Offset: 0x00015424
		public CustomCommandAttribute()
		{
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001722E File Offset: 0x0001542E
		public CustomCommandAttribute(Key key)
		{
			this.KeyGesture = new KeyGesture(key);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00017245 File Offset: 0x00015445
		public CustomCommandAttribute(Key key, ModifierKeys modifier)
		{
			this.KeyGesture = new KeyGesture(key, modifier);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001725D File Offset: 0x0001545D
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x00017265 File Offset: 0x00015465
		public KeyGesture KeyGesture { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001726E File Offset: 0x0001546E
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00017276 File Offset: 0x00015476
		public bool IsAsynchronous { get; set; }
	}
}
