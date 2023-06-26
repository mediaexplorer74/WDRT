using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Properties
{
	// Token: 0x0200000B RID: 11
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004F77 File Offset: 0x00003177
		internal Resources()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004F84 File Offset: 0x00003184
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.AnalogAdaptation.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004FCC File Offset: 0x000031CC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004FE3 File Offset: 0x000031E3
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
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004FEC File Offset: 0x000031EC
		internal static Bitmap Analog
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("Analog", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000501C File Offset: 0x0000321C
		internal static string HoloLens_SalesName
		{
			get
			{
				return Resources.ResourceManager.GetString("HoloLens_SalesName", Resources.resourceCulture);
			}
		}

		// Token: 0x0400003A RID: 58
		private static ResourceManager resourceMan;

		// Token: 0x0400003B RID: 59
		private static CultureInfo resourceCulture;
	}
}
