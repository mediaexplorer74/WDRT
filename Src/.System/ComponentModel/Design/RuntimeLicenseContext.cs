using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005DA RID: 1498
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class RuntimeLicenseContext : LicenseContext
	{
		// Token: 0x060037A6 RID: 14246 RVA: 0x000F0134 File Offset: 0x000EE334
		private string GetLocalPath(string fileName)
		{
			Uri uri = new Uri(fileName);
			return uri.LocalPath + uri.Fragment;
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000F015C File Offset: 0x000EE35C
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			if (this.savedLicenseKeys == null || this.savedLicenseKeys[type.AssemblyQualifiedName] == null)
			{
				if (this.savedLicenseKeys == null)
				{
					this.savedLicenseKeys = new Hashtable();
				}
				Uri uri = null;
				if (resourceAssembly == null)
				{
					string licenseFile = AppDomain.CurrentDomain.SetupInformation.LicenseFile;
					FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
					fileIOPermission.Assert();
					string applicationBase;
					try
					{
						applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (licenseFile != null && applicationBase != null)
					{
						uri = new Uri(new Uri(applicationBase), licenseFile);
					}
				}
				if (uri == null)
				{
					if (resourceAssembly == null)
					{
						resourceAssembly = Assembly.GetEntryAssembly();
					}
					if (resourceAssembly == null)
					{
						foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
						{
							if (!assembly.IsDynamic)
							{
								FileIOPermission fileIOPermission2 = new FileIOPermission(PermissionState.Unrestricted);
								fileIOPermission2.Assert();
								string text;
								try
								{
									text = this.GetLocalPath(assembly.EscapedCodeBase);
									text = new FileInfo(text).Name;
								}
								finally
								{
									CodeAccessPermission.RevertAssert();
								}
								Stream stream = assembly.GetManifestResourceStream(text + ".licenses");
								if (stream == null)
								{
									stream = this.CaseInsensitiveManifestResourceStreamLookup(assembly, text + ".licenses");
								}
								if (stream != null)
								{
									DesigntimeLicenseContextSerializer.Deserialize(stream, text.ToUpper(CultureInfo.InvariantCulture), this);
									break;
								}
							}
						}
					}
					else if (!resourceAssembly.IsDynamic)
					{
						FileIOPermission fileIOPermission3 = new FileIOPermission(PermissionState.Unrestricted);
						fileIOPermission3.Assert();
						string text2;
						try
						{
							text2 = this.GetLocalPath(resourceAssembly.EscapedCodeBase);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						text2 = Path.GetFileName(text2);
						string text3 = text2 + ".licenses";
						Stream stream2 = resourceAssembly.GetManifestResourceStream(text3);
						if (stream2 == null)
						{
							string text4 = null;
							CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
							string name = resourceAssembly.GetName().Name;
							foreach (string text5 in resourceAssembly.GetManifestResourceNames())
							{
								if (compareInfo.Compare(text5, text3, CompareOptions.IgnoreCase) == 0 || compareInfo.Compare(text5, name + ".exe.licenses", CompareOptions.IgnoreCase) == 0 || compareInfo.Compare(text5, name + ".dll.licenses", CompareOptions.IgnoreCase) == 0)
								{
									text4 = text5;
									break;
								}
							}
							if (text4 != null)
							{
								stream2 = resourceAssembly.GetManifestResourceStream(text4);
							}
						}
						if (stream2 != null)
						{
							DesigntimeLicenseContextSerializer.Deserialize(stream2, text2.ToUpper(CultureInfo.InvariantCulture), this);
						}
					}
				}
				if (uri != null)
				{
					Stream stream3 = RuntimeLicenseContext.OpenRead(uri);
					if (stream3 != null)
					{
						string[] segments = uri.Segments;
						string text6 = segments[segments.Length - 1];
						string text7 = text6.Substring(0, text6.LastIndexOf("."));
						DesigntimeLicenseContextSerializer.Deserialize(stream3, text7.ToUpper(CultureInfo.InvariantCulture), this);
					}
				}
			}
			return (string)this.savedLicenseKeys[type.AssemblyQualifiedName];
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000F0464 File Offset: 0x000EE664
		private Stream CaseInsensitiveManifestResourceStreamLookup(Assembly satellite, string name)
		{
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			string name2 = satellite.GetName().Name;
			foreach (string text in satellite.GetManifestResourceNames())
			{
				if (compareInfo.Compare(text, name, CompareOptions.IgnoreCase) == 0 || compareInfo.Compare(text, name2 + ".exe.licenses") == 0 || compareInfo.Compare(text, name2 + ".dll.licenses") == 0)
				{
					name = text;
					break;
				}
			}
			return satellite.GetManifestResourceStream(name);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000F04E8 File Offset: 0x000EE6E8
		private static Stream OpenRead(Uri resourceUri)
		{
			Stream stream = null;
			PermissionSet permissionSet = new PermissionSet(PermissionState.Unrestricted);
			permissionSet.Assert();
			try
			{
				stream = new WebClient
				{
					Credentials = CredentialCache.DefaultCredentials
				}.OpenRead(resourceUri.ToString());
			}
			catch (Exception ex)
			{
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return stream;
		}

		// Token: 0x04002AE6 RID: 10982
		private static TraceSwitch RuntimeLicenseContextSwitch = new TraceSwitch("RuntimeLicenseContextTrace", "RuntimeLicenseContext tracing");

		// Token: 0x04002AE7 RID: 10983
		private const int ReadBlock = 400;

		// Token: 0x04002AE8 RID: 10984
		internal Hashtable savedLicenseKeys;
	}
}
