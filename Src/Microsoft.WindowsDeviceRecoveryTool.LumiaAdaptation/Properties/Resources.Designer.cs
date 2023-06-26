using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Properties
{
	// Token: 0x0200000A RID: 10
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00005C6E File Offset: 0x00003E6E
		internal Resources()
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005C78 File Offset: 0x00003E78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.LumiaAdaptation.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00005CC0 File Offset: 0x00003EC0
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00005CD7 File Offset: 0x00003ED7
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00005CE0 File Offset: 0x00003EE0
		internal static Bitmap LumiaLogo
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("LumiaLogo", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00005D10 File Offset: 0x00003F10
		internal static Bitmap MicrosoftLumia
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("MicrosoftLumia", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00005D40 File Offset: 0x00003F40
		internal static Bitmap NokiaLumia
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("NokiaLumia", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x04000037 RID: 55
		private static ResourceManager resourceMan;

		// Token: 0x04000038 RID: 56
		private static CultureInfo resourceCulture;
	}
}
