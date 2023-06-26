using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Provides version information for a physical file on disk.</summary>
	// Token: 0x020004D6 RID: 1238
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class FileVersionInfo
	{
		// Token: 0x06002EA7 RID: 11943 RVA: 0x000D1ED4 File Offset: 0x000D00D4
		private FileVersionInfo(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Gets the comments associated with the file.</summary>
		/// <returns>The comments associated with the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000D1EE3 File Offset: 0x000D00E3
		public string Comments
		{
			get
			{
				return this.comments;
			}
		}

		/// <summary>Gets the name of the company that produced the file.</summary>
		/// <returns>The name of the company that produced the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000D1EEB File Offset: 0x000D00EB
		public string CompanyName
		{
			get
			{
				return this.companyName;
			}
		}

		/// <summary>Gets the build number of the file.</summary>
		/// <returns>A value representing the build number of the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x000D1EF3 File Offset: 0x000D00F3
		public int FileBuildPart
		{
			get
			{
				return this.fileBuild;
			}
		}

		/// <summary>Gets the description of the file.</summary>
		/// <returns>The description of the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000D1EFB File Offset: 0x000D00FB
		public string FileDescription
		{
			get
			{
				return this.fileDescription;
			}
		}

		/// <summary>Gets the major part of the version number.</summary>
		/// <returns>A value representing the major part of the version number or 0 (zero) if the file did not contain version information.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000D1F03 File Offset: 0x000D0103
		public int FileMajorPart
		{
			get
			{
				return this.fileMajor;
			}
		}

		/// <summary>Gets the minor part of the version number of the file.</summary>
		/// <returns>A value representing the minor part of the version number of the file or 0 (zero) if the file did not contain version information.</returns>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000D1F0B File Offset: 0x000D010B
		public int FileMinorPart
		{
			get
			{
				return this.fileMinor;
			}
		}

		/// <summary>Gets the name of the file that this instance of <see cref="T:System.Diagnostics.FileVersionInfo" /> describes.</summary>
		/// <returns>The name of the file described by this instance of <see cref="T:System.Diagnostics.FileVersionInfo" />.</returns>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000D1F13 File Offset: 0x000D0113
		public string FileName
		{
			get
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				return this.fileName;
			}
		}

		/// <summary>Gets the file private part number.</summary>
		/// <returns>A value representing the file private part number or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000D1F2C File Offset: 0x000D012C
		public int FilePrivatePart
		{
			get
			{
				return this.filePrivate;
			}
		}

		/// <summary>Gets the file version number.</summary>
		/// <returns>The version number of the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000D1F34 File Offset: 0x000D0134
		public string FileVersion
		{
			get
			{
				return this.fileVersion;
			}
		}

		/// <summary>Gets the internal name of the file, if one exists.</summary>
		/// <returns>The internal name of the file. If none exists, this property will contain the original name of the file without the extension.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000D1F3C File Offset: 0x000D013C
		public string InternalName
		{
			get
			{
				return this.internalName;
			}
		}

		/// <summary>Gets a value that specifies whether the file contains debugging information or is compiled with debugging features enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the file contains debugging information or is compiled with debugging features enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x000D1F44 File Offset: 0x000D0144
		public bool IsDebug
		{
			get
			{
				return (this.fileFlags & 1) != 0;
			}
		}

		/// <summary>Gets a value that specifies whether the file has been modified and is not identical to the original shipping file of the same version number.</summary>
		/// <returns>
		///   <see langword="true" /> if the file is patched; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000D1F51 File Offset: 0x000D0151
		public bool IsPatched
		{
			get
			{
				return (this.fileFlags & 4) != 0;
			}
		}

		/// <summary>Gets a value that specifies whether the file was built using standard release procedures.</summary>
		/// <returns>
		///   <see langword="true" /> if the file is a private build; <see langword="false" /> if the file was built using standard release procedures or if the file did not contain version information.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002EB4 RID: 11956 RVA: 0x000D1F5E File Offset: 0x000D015E
		public bool IsPrivateBuild
		{
			get
			{
				return (this.fileFlags & 8) != 0;
			}
		}

		/// <summary>Gets a value that specifies whether the file is a development version, rather than a commercially released product.</summary>
		/// <returns>
		///   <see langword="true" /> if the file is prerelease; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000D1F6B File Offset: 0x000D016B
		public bool IsPreRelease
		{
			get
			{
				return (this.fileFlags & 2) != 0;
			}
		}

		/// <summary>Gets a value that specifies whether the file is a special build.</summary>
		/// <returns>
		///   <see langword="true" /> if the file is a special build; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000D1F78 File Offset: 0x000D0178
		public bool IsSpecialBuild
		{
			get
			{
				return (this.fileFlags & 32) != 0;
			}
		}

		/// <summary>Gets the default language string for the version info block.</summary>
		/// <returns>The description string for the Microsoft Language Identifier in the version resource or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000D1F86 File Offset: 0x000D0186
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		/// <summary>Gets all copyright notices that apply to the specified file.</summary>
		/// <returns>The copyright notices that apply to the specified file.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000D1F8E File Offset: 0x000D018E
		public string LegalCopyright
		{
			get
			{
				return this.legalCopyright;
			}
		}

		/// <summary>Gets the trademarks and registered trademarks that apply to the file.</summary>
		/// <returns>The trademarks and registered trademarks that apply to the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000D1F96 File Offset: 0x000D0196
		public string LegalTrademarks
		{
			get
			{
				return this.legalTrademarks;
			}
		}

		/// <summary>Gets the name the file was created with.</summary>
		/// <returns>The name the file was created with or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x000D1F9E File Offset: 0x000D019E
		public string OriginalFilename
		{
			get
			{
				return this.originalFilename;
			}
		}

		/// <summary>Gets information about a private version of the file.</summary>
		/// <returns>Information about a private version of the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000D1FA6 File Offset: 0x000D01A6
		public string PrivateBuild
		{
			get
			{
				return this.privateBuild;
			}
		}

		/// <summary>Gets the build number of the product this file is associated with.</summary>
		/// <returns>A value representing the build number of the product this file is associated with or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000D1FAE File Offset: 0x000D01AE
		public int ProductBuildPart
		{
			get
			{
				return this.productBuild;
			}
		}

		/// <summary>Gets the major part of the version number for the product this file is associated with.</summary>
		/// <returns>A value representing the major part of the product version number or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000D1FB6 File Offset: 0x000D01B6
		public int ProductMajorPart
		{
			get
			{
				return this.productMajor;
			}
		}

		/// <summary>Gets the minor part of the version number for the product the file is associated with.</summary>
		/// <returns>A value representing the minor part of the product version number or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x000D1FBE File Offset: 0x000D01BE
		public int ProductMinorPart
		{
			get
			{
				return this.productMinor;
			}
		}

		/// <summary>Gets the name of the product this file is distributed with.</summary>
		/// <returns>The name of the product this file is distributed with or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000D1FC6 File Offset: 0x000D01C6
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		/// <summary>Gets the private part number of the product this file is associated with.</summary>
		/// <returns>A value representing the private part number of the product this file is associated with or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000D1FCE File Offset: 0x000D01CE
		public int ProductPrivatePart
		{
			get
			{
				return this.productPrivate;
			}
		}

		/// <summary>Gets the version of the product this file is distributed with.</summary>
		/// <returns>The version of the product this file is distributed with or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000D1FD6 File Offset: 0x000D01D6
		public string ProductVersion
		{
			get
			{
				return this.productVersion;
			}
		}

		/// <summary>Gets the special build information for the file.</summary>
		/// <returns>The special build information for the file or <see langword="null" /> if the file did not contain version information.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000D1FDE File Offset: 0x000D01DE
		public string SpecialBuild
		{
			get
			{
				return this.specialBuild;
			}
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000D1FE8 File Offset: 0x000D01E8
		private static string ConvertTo8DigitHex(int value)
		{
			string text = Convert.ToString(value, 16);
			text = text.ToUpper(CultureInfo.InvariantCulture);
			if (text.Length == 8)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(8);
			for (int i = text.Length; i < 8; i++)
			{
				stringBuilder.Append("0");
			}
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000D2048 File Offset: 0x000D0248
		private static Microsoft.Win32.NativeMethods.VS_FIXEDFILEINFO GetFixedFileInfo(IntPtr memPtr)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			if (Microsoft.Win32.UnsafeNativeMethods.VerQueryValue(new HandleRef(null, memPtr), "\\", ref zero, out num))
			{
				Microsoft.Win32.NativeMethods.VS_FIXEDFILEINFO vs_FIXEDFILEINFO = new Microsoft.Win32.NativeMethods.VS_FIXEDFILEINFO();
				Marshal.PtrToStructure(zero, vs_FIXEDFILEINFO);
				return vs_FIXEDFILEINFO;
			}
			return new Microsoft.Win32.NativeMethods.VS_FIXEDFILEINFO();
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000D2088 File Offset: 0x000D0288
		private static string GetFileVersionLanguage(IntPtr memPtr)
		{
			int num = FileVersionInfo.GetVarEntry(memPtr) >> 16;
			StringBuilder stringBuilder = new StringBuilder(256);
			Microsoft.Win32.UnsafeNativeMethods.VerLanguageName(num, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000D20C0 File Offset: 0x000D02C0
		private static string GetFileVersionString(IntPtr memPtr, string name)
		{
			string text = "";
			IntPtr zero = IntPtr.Zero;
			int num;
			if (Microsoft.Win32.UnsafeNativeMethods.VerQueryValue(new HandleRef(null, memPtr), name, ref zero, out num) && zero != IntPtr.Zero)
			{
				text = Marshal.PtrToStringAuto(zero);
			}
			return text;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000D2104 File Offset: 0x000D0304
		private static int GetVarEntry(IntPtr memPtr)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			if (Microsoft.Win32.UnsafeNativeMethods.VerQueryValue(new HandleRef(null, memPtr), "\\VarFileInfo\\Translation", ref zero, out num))
			{
				return ((int)Marshal.ReadInt16(zero) << 16) + (int)Marshal.ReadInt16((IntPtr)((long)zero + 2L));
			}
			return 67699940;
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000D2154 File Offset: 0x000D0354
		private bool GetVersionInfoForCodePage(IntPtr memIntPtr, string codepage)
		{
			string text = "\\\\StringFileInfo\\\\{0}\\\\{1}";
			this.companyName = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "CompanyName" }));
			this.fileDescription = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "FileDescription" }));
			this.fileVersion = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "FileVersion" }));
			this.internalName = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "InternalName" }));
			this.legalCopyright = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "LegalCopyright" }));
			this.originalFilename = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "OriginalFilename" }));
			this.productName = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "ProductName" }));
			this.productVersion = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "ProductVersion" }));
			this.comments = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "Comments" }));
			this.legalTrademarks = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "LegalTrademarks" }));
			this.privateBuild = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "PrivateBuild" }));
			this.specialBuild = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, text, new object[] { codepage, "SpecialBuild" }));
			this.language = FileVersionInfo.GetFileVersionLanguage(memIntPtr);
			Microsoft.Win32.NativeMethods.VS_FIXEDFILEINFO fixedFileInfo = FileVersionInfo.GetFixedFileInfo(memIntPtr);
			this.fileMajor = FileVersionInfo.HIWORD(fixedFileInfo.dwFileVersionMS);
			this.fileMinor = FileVersionInfo.LOWORD(fixedFileInfo.dwFileVersionMS);
			this.fileBuild = FileVersionInfo.HIWORD(fixedFileInfo.dwFileVersionLS);
			this.filePrivate = FileVersionInfo.LOWORD(fixedFileInfo.dwFileVersionLS);
			this.productMajor = FileVersionInfo.HIWORD(fixedFileInfo.dwProductVersionMS);
			this.productMinor = FileVersionInfo.LOWORD(fixedFileInfo.dwProductVersionMS);
			this.productBuild = FileVersionInfo.HIWORD(fixedFileInfo.dwProductVersionLS);
			this.productPrivate = FileVersionInfo.LOWORD(fixedFileInfo.dwProductVersionLS);
			this.fileFlags = fixedFileInfo.dwFileFlags;
			return this.fileVersion != string.Empty;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000D240A File Offset: 0x000D060A
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string GetFullPathWithAssert(string fileName)
		{
			return Path.GetFullPath(fileName);
		}

		/// <summary>Returns a <see cref="T:System.Diagnostics.FileVersionInfo" /> representing the version information associated with the specified file.</summary>
		/// <param name="fileName">The fully qualified path and name of the file to retrieve the version information for.</param>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> containing information about the file. If the file did not contain version information, the <see cref="T:System.Diagnostics.FileVersionInfo" /> contains only the name of the file requested.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified cannot be found.</exception>
		// Token: 0x06002ECA RID: 11978 RVA: 0x000D2414 File Offset: 0x000D0614
		public unsafe static FileVersionInfo GetVersionInfo(string fileName)
		{
			if (!File.Exists(fileName))
			{
				string fullPathWithAssert = FileVersionInfo.GetFullPathWithAssert(fileName);
				new FileIOPermission(FileIOPermissionAccess.Read, fullPathWithAssert).Demand();
				throw new FileNotFoundException(fileName);
			}
			int num;
			int fileVersionInfoSize = Microsoft.Win32.UnsafeNativeMethods.GetFileVersionInfoSize(fileName, out num);
			FileVersionInfo fileVersionInfo = new FileVersionInfo(fileName);
			if (fileVersionInfoSize != 0)
			{
				byte[] array = new byte[fileVersionInfoSize];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				IntPtr intPtr = new IntPtr((void*)ptr);
				if (Microsoft.Win32.UnsafeNativeMethods.GetFileVersionInfo(fileName, 0, fileVersionInfoSize, new HandleRef(null, intPtr)))
				{
					int varEntry = FileVersionInfo.GetVarEntry(intPtr);
					if (!fileVersionInfo.GetVersionInfoForCodePage(intPtr, FileVersionInfo.ConvertTo8DigitHex(varEntry)))
					{
						int[] array3 = new int[] { 67699888, 67699940, 67698688 };
						foreach (int num2 in array3)
						{
							if (num2 != varEntry && fileVersionInfo.GetVersionInfoForCodePage(intPtr, FileVersionInfo.ConvertTo8DigitHex(num2)))
							{
								break;
							}
						}
					}
				}
				array2 = null;
			}
			return fileVersionInfo;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000D2500 File Offset: 0x000D0700
		private static int HIWORD(int dword)
		{
			return Microsoft.Win32.NativeMethods.Util.HIWORD(dword);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000D2508 File Offset: 0x000D0708
		private static int LOWORD(int dword)
		{
			return Microsoft.Win32.NativeMethods.Util.LOWORD(dword);
		}

		/// <summary>Returns a partial list of properties in the <see cref="T:System.Diagnostics.FileVersionInfo" /> and their values.</summary>
		/// <returns>A list of the following properties in this class and their values:  
		///  <see cref="P:System.Diagnostics.FileVersionInfo.FileName" />, <see cref="P:System.Diagnostics.FileVersionInfo.InternalName" />, <see cref="P:System.Diagnostics.FileVersionInfo.OriginalFilename" />, <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" />, <see cref="P:System.Diagnostics.FileVersionInfo.FileDescription" />, <see cref="P:System.Diagnostics.FileVersionInfo.ProductName" />, <see cref="P:System.Diagnostics.FileVersionInfo.ProductVersion" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsDebug" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPatched" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPreRelease" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsPrivateBuild" />, <see cref="P:System.Diagnostics.FileVersionInfo.IsSpecialBuild" />,  
		///  <see cref="P:System.Diagnostics.FileVersionInfo.Language" />.  
		///  If the file did not contain version information, this list will contain only the name of the requested file. Boolean values will be <see langword="false" />, and all other entries will be <see langword="null" />.</returns>
		// Token: 0x06002ECD RID: 11981 RVA: 0x000D2510 File Offset: 0x000D0710
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			string text = "\r\n";
			stringBuilder.Append("File:             ");
			stringBuilder.Append(this.FileName);
			stringBuilder.Append(text);
			stringBuilder.Append("InternalName:     ");
			stringBuilder.Append(this.InternalName);
			stringBuilder.Append(text);
			stringBuilder.Append("OriginalFilename: ");
			stringBuilder.Append(this.OriginalFilename);
			stringBuilder.Append(text);
			stringBuilder.Append("FileVersion:      ");
			stringBuilder.Append(this.FileVersion);
			stringBuilder.Append(text);
			stringBuilder.Append("FileDescription:  ");
			stringBuilder.Append(this.FileDescription);
			stringBuilder.Append(text);
			stringBuilder.Append("Product:          ");
			stringBuilder.Append(this.ProductName);
			stringBuilder.Append(text);
			stringBuilder.Append("ProductVersion:   ");
			stringBuilder.Append(this.ProductVersion);
			stringBuilder.Append(text);
			stringBuilder.Append("Debug:            ");
			stringBuilder.Append(this.IsDebug.ToString());
			stringBuilder.Append(text);
			stringBuilder.Append("Patched:          ");
			stringBuilder.Append(this.IsPatched.ToString());
			stringBuilder.Append(text);
			stringBuilder.Append("PreRelease:       ");
			stringBuilder.Append(this.IsPreRelease.ToString());
			stringBuilder.Append(text);
			stringBuilder.Append("PrivateBuild:     ");
			stringBuilder.Append(this.IsPrivateBuild.ToString());
			stringBuilder.Append(text);
			stringBuilder.Append("SpecialBuild:     ");
			stringBuilder.Append(this.IsSpecialBuild.ToString());
			stringBuilder.Append(text);
			stringBuilder.Append("Language:         ");
			stringBuilder.Append(this.Language);
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		// Token: 0x04002772 RID: 10098
		private string fileName;

		// Token: 0x04002773 RID: 10099
		private string companyName;

		// Token: 0x04002774 RID: 10100
		private string fileDescription;

		// Token: 0x04002775 RID: 10101
		private string fileVersion;

		// Token: 0x04002776 RID: 10102
		private string internalName;

		// Token: 0x04002777 RID: 10103
		private string legalCopyright;

		// Token: 0x04002778 RID: 10104
		private string originalFilename;

		// Token: 0x04002779 RID: 10105
		private string productName;

		// Token: 0x0400277A RID: 10106
		private string productVersion;

		// Token: 0x0400277B RID: 10107
		private string comments;

		// Token: 0x0400277C RID: 10108
		private string legalTrademarks;

		// Token: 0x0400277D RID: 10109
		private string privateBuild;

		// Token: 0x0400277E RID: 10110
		private string specialBuild;

		// Token: 0x0400277F RID: 10111
		private string language;

		// Token: 0x04002780 RID: 10112
		private int fileMajor;

		// Token: 0x04002781 RID: 10113
		private int fileMinor;

		// Token: 0x04002782 RID: 10114
		private int fileBuild;

		// Token: 0x04002783 RID: 10115
		private int filePrivate;

		// Token: 0x04002784 RID: 10116
		private int productMajor;

		// Token: 0x04002785 RID: 10117
		private int productMinor;

		// Token: 0x04002786 RID: 10118
		private int productBuild;

		// Token: 0x04002787 RID: 10119
		private int productPrivate;

		// Token: 0x04002788 RID: 10120
		private int fileFlags;
	}
}
