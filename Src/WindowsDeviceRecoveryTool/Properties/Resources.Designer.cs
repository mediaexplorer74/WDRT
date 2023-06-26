using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.Properties
{
	// Token: 0x0200007C RID: 124
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00016287 File Offset: 0x00014487
		internal Resources()
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00016294 File Offset: 0x00014494
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000162DC File Offset: 0x000144DC
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000162F3 File Offset: 0x000144F3
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

		// Token: 0x040001D3 RID: 467
		private static ResourceManager resourceMan;

		// Token: 0x040001D4 RID: 468
		private static CultureInfo resourceCulture;
	}
}
