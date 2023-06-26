using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.WindowsDeviceRecoveryTool.AcerAdaptation.Properties
{
	// Token: 0x02000007 RID: 7
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002EA9 File Offset: 0x000010A9
		internal Resources()
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002EB1 File Offset: 0x000010B1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.WindowsDeviceRecoveryTool.AcerAdaptation.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002EDD File Offset: 0x000010DD
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002EE4 File Offset: 0x000010E4
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002EEC File Offset: 0x000010EC
		internal static Bitmap AcerLogo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("AcerLogo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002F07 File Offset: 0x00001107
		internal static Bitmap JadePrimo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("JadePrimo", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002F22 File Offset: 0x00001122
		internal static Bitmap M220
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("M220", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002F3D File Offset: 0x0000113D
		internal static Bitmap M330
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("M330", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002F58 File Offset: 0x00001158
		internal static string Name_Jade_Primo
		{
			get
			{
				return Resources.ResourceManager.GetString("Name_Jade_Primo", Resources.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002F6E File Offset: 0x0000116E
		internal static string Name_Liquid_M220
		{
			get
			{
				return Resources.ResourceManager.GetString("Name_Liquid_M220", Resources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002F84 File Offset: 0x00001184
		internal static string Name_Liquid_M330
		{
			get
			{
				return Resources.ResourceManager.GetString("Name_Liquid_M330", Resources.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002F9A File Offset: 0x0000119A
		internal static string Name_Manufacturer
		{
			get
			{
				return Resources.ResourceManager.GetString("Name_Manufacturer", Resources.resourceCulture);
			}
		}

		// Token: 0x04000018 RID: 24
		private static ResourceManager resourceMan;

		// Token: 0x04000019 RID: 25
		private static CultureInfo resourceCulture;
	}
}
