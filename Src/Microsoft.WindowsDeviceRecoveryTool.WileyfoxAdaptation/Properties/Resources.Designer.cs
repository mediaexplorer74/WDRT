using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.WileyfoxAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000028B4 File Offset: 0x00000AB4
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000028BC File Offset: 0x00000ABC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.WileyfoxAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000028EF File Offset: 0x00000AEF
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
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000028F7 File Offset: 0x00000AF7
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000290D File Offset: 0x00000B0D
		internal static string FriendlyName_Wileyfox_Pro
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Wileyfox_Pro", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002923 File Offset: 0x00000B23
		internal static Bitmap Wileyfox_logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Wileyfox_logo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000293E File Offset: 0x00000B3E
		internal static Bitmap Wileyfox_Pro_mobileImage
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Wileyfox_Pro_mobileImage", Resources.resourceCulture);
			}
		}

		// Token: 0x0400000D RID: 13
		private static ResourceManager resourceMan;

		// Token: 0x0400000E RID: 14
		private static CultureInfo resourceCulture;
	}
}
