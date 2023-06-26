using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides an implementation of a <see cref="T:System.ComponentModel.LicenseProvider" />. The provider works in a similar fashion to the Microsoft .NET Framework standard licensing model.</summary>
	// Token: 0x02000582 RID: 1410
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class LicFileLicenseProvider : LicenseProvider
	{
		/// <summary>Determines whether the key that the <see cref="M:System.ComponentModel.LicFileLicenseProvider.GetLicense(System.ComponentModel.LicenseContext,System.Type,System.Object,System.Boolean)" /> method retrieves is valid for the specified type.</summary>
		/// <param name="key">The <see cref="P:System.ComponentModel.License.LicenseKey" /> to check.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the <see cref="T:System.ComponentModel.License" />.</param>
		/// <returns>
		///   <see langword="true" /> if the key is a valid <see cref="P:System.ComponentModel.License.LicenseKey" /> for the specified type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003412 RID: 13330 RVA: 0x000E41D6 File Offset: 0x000E23D6
		protected virtual bool IsKeyValid(string key, Type type)
		{
			return key != null && key.StartsWith(this.GetKey(type));
		}

		/// <summary>Returns a key for the specified type.</summary>
		/// <param name="type">The object type to return the key.</param>
		/// <returns>A confirmation that the <paramref name="type" /> parameter is licensed.</returns>
		// Token: 0x06003413 RID: 13331 RVA: 0x000E41EA File Offset: 0x000E23EA
		protected virtual string GetKey(Type type)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} is a licensed component.", new object[] { type.FullName });
		}

		/// <summary>Returns a license for the instance of the component, if one is available.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies where you can use the licensed object.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the component requesting the <see cref="T:System.ComponentModel.License" />.</param>
		/// <param name="instance">An object that requests the <see cref="T:System.ComponentModel.License" />.</param>
		/// <param name="allowExceptions">
		///   <see langword="true" /> if a <see cref="T:System.ComponentModel.LicenseException" /> should be thrown when a component cannot be granted a license; otherwise, <see langword="false" />.</param>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />. If this method cannot find a valid <see cref="T:System.ComponentModel.License" /> or a valid <paramref name="context" /> parameter, it returns <see langword="null" />.</returns>
		// Token: 0x06003414 RID: 13332 RVA: 0x000E420C File Offset: 0x000E240C
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			LicFileLicenseProvider.LicFileLicense licFileLicense = null;
			if (context != null)
			{
				if (context.UsageMode == LicenseUsageMode.Runtime)
				{
					string savedLicenseKey = context.GetSavedLicenseKey(type, null);
					if (savedLicenseKey != null && this.IsKeyValid(savedLicenseKey, type))
					{
						licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, savedLicenseKey);
					}
				}
				if (licFileLicense == null)
				{
					string text = null;
					if (context != null)
					{
						ITypeResolutionService typeResolutionService = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
						if (typeResolutionService != null)
						{
							text = typeResolutionService.GetPathOfAssembly(type.Assembly.GetName());
						}
					}
					if (text == null)
					{
						text = type.Module.FullyQualifiedName;
					}
					string directoryName = Path.GetDirectoryName(text);
					string text2 = directoryName + "\\" + type.FullName + ".lic";
					if (File.Exists(text2))
					{
						Stream stream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read);
						StreamReader streamReader = new StreamReader(stream);
						string text3 = streamReader.ReadLine();
						streamReader.Close();
						if (this.IsKeyValid(text3, type))
						{
							licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, this.GetKey(type));
						}
					}
					if (licFileLicense != null)
					{
						context.SetSavedLicenseKey(type, licFileLicense.LicenseKey);
					}
				}
			}
			return licFileLicense;
		}

		// Token: 0x02000893 RID: 2195
		private class LicFileLicense : License
		{
			// Token: 0x06004576 RID: 17782 RVA: 0x0012252B File Offset: 0x0012072B
			public LicFileLicense(LicFileLicenseProvider owner, string key)
			{
				this.owner = owner;
				this.key = key;
			}

			// Token: 0x17000FB5 RID: 4021
			// (get) Token: 0x06004577 RID: 17783 RVA: 0x00122541 File Offset: 0x00120741
			public override string LicenseKey
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x06004578 RID: 17784 RVA: 0x00122549 File Offset: 0x00120749
			public override void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			// Token: 0x040037B3 RID: 14259
			private LicFileLicenseProvider owner;

			// Token: 0x040037B4 RID: 14260
			private string key;
		}
	}
}
