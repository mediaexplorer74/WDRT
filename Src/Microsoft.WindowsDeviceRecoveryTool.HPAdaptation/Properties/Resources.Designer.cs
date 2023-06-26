using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.HPAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002A8C File Offset: 0x00000C8C
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002A94 File Offset: 0x00000C94
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.HPAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002AC0 File Offset: 0x00000CC0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002AC7 File Offset: 0x00000CC7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002ACF File Offset: 0x00000CCF
		internal static Bitmap EliteX3_Gallery_Zoom1
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("EliteX3_Gallery_Zoom1", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002AEA File Offset: 0x00000CEA
		internal static string FriendlyName_HP_Elite_x3
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_HP_Elite_x3", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002B00 File Offset: 0x00000D00
		internal static string FriendlyName_HP_Elite_x3_Telstra
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_HP_Elite_x3_Telstra", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002B16 File Offset: 0x00000D16
		internal static string FriendlyName_HP_Elite_x3_Verizon
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_HP_Elite_x3_Verizon", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002B2C File Offset: 0x00000D2C
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002B42 File Offset: 0x00000D42
		internal static Bitmap HP_logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("HP_logo", Resources.resourceCulture);
			}
		}

		// Token: 0x04000011 RID: 17
		private static ResourceManager resourceMan;

		// Token: 0x04000012 RID: 18
		private static CultureInfo resourceCulture;
	}
}
