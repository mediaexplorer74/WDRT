using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.McjAdaptation.Properties
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002BEF File Offset: 0x00000DEF
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BFC File Offset: 0x00000DFC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.McjAdaptation.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002C44 File Offset: 0x00000E44
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002C5B File Offset: 0x00000E5B
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
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C64 File Offset: 0x00000E64
		internal static string FriendlyName_MADOSMA_Q501
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_MADOSMA_Q501", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002C8C File Offset: 0x00000E8C
		internal static string FriendlyName_MADOSMA_Q601
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_MADOSMA_Q601", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002CB4 File Offset: 0x00000EB4
		internal static string FriendlyName_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002CDC File Offset: 0x00000EDC
		internal static Bitmap Madosma
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Madosma", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D0C File Offset: 0x00000F0C
		internal static Bitmap Madosma_Q601
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Madosma_Q601", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002D3C File Offset: 0x00000F3C
		internal static Bitmap MadosmaLogo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("MadosmaLogo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D6C File Offset: 0x00000F6C
		internal static Bitmap McjLogo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("McjLogo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000010 RID: 16
		private static ResourceManager resourceMan;

		// Token: 0x04000011 RID: 17
		private static CultureInfo resourceCulture;
	}
}
