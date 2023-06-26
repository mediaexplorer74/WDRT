using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.VAIOAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000027BC File Offset: 0x000009BC
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000027C4 File Offset: 0x000009C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.VAIOAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000027F7 File Offset: 0x000009F7
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
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027FF File Offset: 0x000009FF
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002815 File Offset: 0x00000A15
		internal static string FriendlyName_VPB051
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_VPB051", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000282B File Offset: 0x00000A2B
		internal static Bitmap logo_white_vaio
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_white_vaio", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002846 File Offset: 0x00000A46
		internal static Bitmap VAIO_Phone_Biz_VPB0511S
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("VAIO_Phone_Biz_VPB0511S", Resources.resourceCulture);
			}
		}

		// Token: 0x0400000C RID: 12
		private static ResourceManager resourceMan;

		// Token: 0x0400000D RID: 13
		private static CultureInfo resourceCulture;
	}
}
