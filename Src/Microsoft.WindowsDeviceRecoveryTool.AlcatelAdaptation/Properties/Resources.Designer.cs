using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.AlcatelAdaptation.Properties
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002D44 File Offset: 0x00000F44
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002D50 File Offset: 0x00000F50
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.AlcatelAdaptation.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002D98 File Offset: 0x00000F98
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002DAF File Offset: 0x00000FAF
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
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002DB8 File Offset: 0x00000FB8
		internal static Bitmap _5055W_front
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("_5055W_front", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002DE8 File Offset: 0x00000FE8
		internal static Bitmap Alcatel_Logo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Alcatel_Logo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002E18 File Offset: 0x00001018
		internal static string FriendlyName_Fierce_XL
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Fierce_XL", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002E40 File Offset: 0x00001040
		internal static string FriendlyName_IDOL4_PRO
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_IDOL4_PRO", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002E68 File Offset: 0x00001068
		internal static string FriendlyName_IDOL4S
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_IDOL4S", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002E90 File Offset: 0x00001090
		internal static string FriendlyName_IDOL4S_NA
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_IDOL4S_NA", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002EB8 File Offset: 0x000010B8
		internal static string FriendlyName_IDOL4S_TMibile
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_IDOL4S_TMibile", Resources.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002EE0 File Offset: 0x000010E0
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F08 File Offset: 0x00001108
		internal static Bitmap IDOL_4S_device_front
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("IDOL_4S_device_front", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000012 RID: 18
		private static ResourceManager resourceMan;

		// Token: 0x04000013 RID: 19
		private static CultureInfo resourceCulture;
	}
}
