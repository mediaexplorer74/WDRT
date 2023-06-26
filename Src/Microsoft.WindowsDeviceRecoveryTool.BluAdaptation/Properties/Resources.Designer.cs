using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.BluAdaptation.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000030D7 File Offset: 0x000012D7
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000030E4 File Offset: 0x000012E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.BluAdaptation.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000312C File Offset: 0x0000132C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00003143 File Offset: 0x00001343
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
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000314C File Offset: 0x0000134C
		internal static Bitmap blulogo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("blulogo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000317C File Offset: 0x0000137C
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000031A4 File Offset: 0x000013A4
		internal static string FriendlyName_Win_HD_LTE
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Win_HD_LTE", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000031CC File Offset: 0x000013CC
		internal static string FriendlyName_WIN_HD_W510
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_WIN_HD_W510", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000031F4 File Offset: 0x000013F4
		internal static string FriendlyName_Win_JR_LTE
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Win_JR_LTE", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000321C File Offset: 0x0000141C
		internal static string FriendlyName_WIN_JR_W410
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_WIN_JR_W410", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003244 File Offset: 0x00001444
		internal static Bitmap winhd
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("winhd", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003274 File Offset: 0x00001474
		internal static Bitmap winhdlte
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("winhdlte", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000032A4 File Offset: 0x000014A4
		internal static Bitmap winjr
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("winjr", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000032D4 File Offset: 0x000014D4
		internal static Bitmap winjrlte
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("winjrlte", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000019 RID: 25
		private static ResourceManager resourceMan;

		// Token: 0x0400001A RID: 26
		private static CultureInfo resourceCulture;
	}
}
