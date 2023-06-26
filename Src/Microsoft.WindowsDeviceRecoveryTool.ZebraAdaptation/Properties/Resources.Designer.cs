using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.ZebraAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000027D4 File Offset: 0x000009D4
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027DC File Offset: 0x000009DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.ZebraAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002808 File Offset: 0x00000A08
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000280F File Offset: 0x00000A0F
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
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002817 File Offset: 0x00000A17
		internal static string FirendlyName_ZebraTC700J
		{
			get
			{
				return Resources.ResourceManager.GetString("FirendlyName_ZebraTC700J", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000282D File Offset: 0x00000A2D
		internal static string FriendlyName_Maufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Maufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002843 File Offset: 0x00000A43
		internal static Bitmap Zebra_Logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Zebra_Logo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000285E File Offset: 0x00000A5E
		internal static Bitmap ZebraTC700J
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("ZebraTC700J", Resources.resourceCulture);
			}
		}

		// Token: 0x0400000C RID: 12
		private static ResourceManager resourceMan;

		// Token: 0x0400000D RID: 13
		private static CultureInfo resourceCulture;
	}
}
