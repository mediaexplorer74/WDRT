using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000028CC File Offset: 0x00000ACC
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000028D4 File Offset: 0x00000AD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.TrekStorAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002900 File Offset: 0x00000B00
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002907 File Offset: 0x00000B07
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
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000290F File Offset: 0x00000B0F
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002925 File Offset: 0x00000B25
		internal static string FriendlyName_TrekStor_WinPhone50LTE
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_TrekStor_WinPhone50LTE", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000293B File Offset: 0x00000B3B
		internal static Bitmap Trekstor_logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Trekstor_logo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002956 File Offset: 0x00000B56
		internal static Bitmap WinPhone_4_7_HD_wp47
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("WinPhone_4_7_HD_wp47", Resources.resourceCulture);
			}
		}

		// Token: 0x0400000D RID: 13
		private static ResourceManager resourceMan;

		// Token: 0x0400000E RID: 14
		private static CultureInfo resourceCulture;
	}
}
