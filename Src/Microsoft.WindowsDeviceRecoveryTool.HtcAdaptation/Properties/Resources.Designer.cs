using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation.Properties
{
	// Token: 0x02000008 RID: 8
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003436 File Offset: 0x00001636
		internal Resources()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003440 File Offset: 0x00001640
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.HtcAdaptation.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003488 File Offset: 0x00001688
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000349F File Offset: 0x0000169F
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000034A8 File Offset: 0x000016A8
		internal static string FriendlyName_HTC_8X
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_HTC_8X", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000034D0 File Offset: 0x000016D0
		internal static string FriendlyName_HTC_One
		{
			get
			{
				return Resources.ResourceManager.GetString("FriendlyName_HTC_One", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000034F8 File Offset: 0x000016F8
		internal static Bitmap HTC8X
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("HTC8X", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003528 File Offset: 0x00001728
		internal static Bitmap HtcLogo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("HtcLogo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003558 File Offset: 0x00001758
		internal static Bitmap HTCOne
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("HTCOne", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000018 RID: 24
		private static ResourceManager resourceMan;

		// Token: 0x04000019 RID: 25
		private static CultureInfo resourceCulture;
	}
}
