using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.FreetelAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000029BC File Offset: 0x00000BBC
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000029C4 File Offset: 0x00000BC4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.FreetelAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000029F0 File Offset: 0x00000BF0
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000029F7 File Offset: 0x00000BF7
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
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000029FF File Offset: 0x00000BFF
		internal static Bitmap Freetel_Logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Freetel_Logo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002A1A File Offset: 0x00000C1A
		internal static string FriendlyName_Katana01
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Katana01", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A30 File Offset: 0x00000C30
		internal static string FriendlyName_Katana02
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Katana02", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A46 File Offset: 0x00000C46
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A5C File Offset: 0x00000C5C
		internal static Bitmap Katana01
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Katana01", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002A77 File Offset: 0x00000C77
		internal static Bitmap Katana02
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Katana02", Resources.resourceCulture);
			}
		}

		// Token: 0x0400000F RID: 15
		private static ResourceManager resourceMan;

		// Token: 0x04000010 RID: 16
		private static CultureInfo resourceCulture;
	}
}
