using System;
using System.Configuration;
using System.Drawing.Configuration;
using System.IO;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x020000FC RID: 252
	internal static class BitmapSelector
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C95C File Offset: 0x0000AB5C
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000C9A5 File Offset: 0x0000ABA5
		internal static string Suffix
		{
			get
			{
				if (BitmapSelector._suffix == null)
				{
					BitmapSelector._suffix = string.Empty;
					SystemDrawingSection systemDrawingSection = ConfigurationManager.GetSection("system.drawing") as SystemDrawingSection;
					if (systemDrawingSection != null)
					{
						string bitmapSuffix = systemDrawingSection.BitmapSuffix;
						if (bitmapSuffix != null && bitmapSuffix != null)
						{
							BitmapSelector._suffix = bitmapSuffix;
						}
					}
				}
				return BitmapSelector._suffix;
			}
			set
			{
				BitmapSelector._suffix = value;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		internal static string AppendSuffix(string filePath)
		{
			string text;
			try
			{
				text = Path.ChangeExtension(filePath, BitmapSelector.Suffix + Path.GetExtension(filePath));
			}
			catch (ArgumentException)
			{
				text = filePath;
			}
			return text;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		public static string GetFileName(string originalPath)
		{
			if (BitmapSelector.Suffix == string.Empty)
			{
				return originalPath;
			}
			string text = BitmapSelector.AppendSuffix(originalPath);
			if (!File.Exists(text))
			{
				return originalPath;
			}
			return text;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000CA20 File Offset: 0x0000AC20
		private static Stream GetResourceStreamHelper(Assembly assembly, Type type, string name)
		{
			Stream stream = null;
			try
			{
				stream = assembly.GetManifestResourceStream(type, name);
			}
			catch (FileNotFoundException)
			{
			}
			return stream;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000CA50 File Offset: 0x0000AC50
		private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, string typeName)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, assembly.GetType(typeName));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000CA60 File Offset: 0x0000AC60
		private static bool DoesAssemblyHaveCustomAttribute(Assembly assembly, Type attrType)
		{
			if (attrType != null)
			{
				object[] customAttributes = assembly.GetCustomAttributes(attrType, false);
				if (customAttributes.Length != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000CA86 File Offset: 0x0000AC86
		internal static bool SatelliteAssemblyOptIn(Assembly assembly)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof(BitmapSuffixInSatelliteAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSatelliteAssemblyAttribute");
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		internal static bool SameAssemblyOptIn(Assembly assembly)
		{
			return BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, typeof(BitmapSuffixInSameAssemblyAttribute)) || BitmapSelector.DoesAssemblyHaveCustomAttribute(assembly, "System.Drawing.BitmapSuffixInSameAssemblyAttribute");
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		public static Stream GetResourceStream(Assembly assembly, Type type, string originalName)
		{
			if (BitmapSelector.Suffix != string.Empty)
			{
				try
				{
					if (BitmapSelector.SameAssemblyOptIn(assembly))
					{
						string text = BitmapSelector.AppendSuffix(originalName);
						Stream resourceStreamHelper = BitmapSelector.GetResourceStreamHelper(assembly, type, text);
						if (resourceStreamHelper != null)
						{
							return resourceStreamHelper;
						}
					}
				}
				catch
				{
				}
				try
				{
					if (BitmapSelector.SatelliteAssemblyOptIn(assembly))
					{
						AssemblyName name = assembly.GetName();
						AssemblyName assemblyName = name;
						assemblyName.Name += BitmapSelector.Suffix;
						name.ProcessorArchitecture = ProcessorArchitecture.None;
						Assembly assembly2 = Assembly.Load(name);
						if (assembly2 != null)
						{
							Stream resourceStreamHelper2 = BitmapSelector.GetResourceStreamHelper(assembly2, type, originalName);
							if (resourceStreamHelper2 != null)
							{
								return resourceStreamHelper2;
							}
						}
					}
				}
				catch
				{
				}
			}
			return assembly.GetManifestResourceStream(type, originalName);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000CB88 File Offset: 0x0000AD88
		public static Stream GetResourceStream(Type type, string originalName)
		{
			return BitmapSelector.GetResourceStream(type.Module.Assembly, type, originalName);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public static Icon CreateIcon(Type type, string originalName)
		{
			return new Icon(BitmapSelector.GetResourceStream(type, originalName));
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000CBAA File Offset: 0x0000ADAA
		public static Bitmap CreateBitmap(Type type, string originalName)
		{
			return new Bitmap(BitmapSelector.GetResourceStream(type, originalName));
		}

		// Token: 0x04000431 RID: 1073
		private static string _suffix;
	}
}
