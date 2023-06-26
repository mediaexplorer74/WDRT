using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Properties
{
	// Token: 0x0200000C RID: 12
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002050 File Offset: 0x00000250
		internal Resources()
		{
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000033F2 File Offset: 0x000015F2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.FawkesAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000341E File Offset: 0x0000161E
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003425 File Offset: 0x00001625
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000342D File Offset: 0x0000162D
		internal static string DeviceSalesName
		{
			get
			{
				return Resources.ResourceManager.GetString("DeviceSalesName", Resources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003443 File Offset: 0x00001643
		internal static Bitmap FawkesTile
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("FawkesTile", Resources.resourceCulture);
			}
		}

		// Token: 0x04000012 RID: 18
		private static ResourceManager resourceMan;

		// Token: 0x04000013 RID: 19
		private static CultureInfo resourceCulture;
	}
}
