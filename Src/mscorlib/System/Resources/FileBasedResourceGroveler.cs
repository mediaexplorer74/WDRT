using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200038A RID: 906
	internal class FileBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x06002CFF RID: 11519 RVA: 0x000AAB4A File Offset: 0x000A8D4A
		public FileBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000AAB5C File Offset: 0x000A8D5C
		[SecuritySafeCritical]
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet resourceSet = null;
			ResourceSet resourceSet2;
			try
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				string resourceFileName = this._mediator.GetResourceFileName(culture);
				string text = this.FindResourceFile(culture, resourceFileName);
				if (text == null)
				{
					if (tryParents && culture.HasInvariantCultureName)
					{
						throw new MissingManifestResourceException(string.Concat(new string[]
						{
							Environment.GetResourceString("MissingManifestResource_NoNeutralDisk"),
							Environment.NewLine,
							"baseName: ",
							this._mediator.BaseNameField,
							"  locationInfo: ",
							(this._mediator.LocationInfo == null) ? "<null>" : this._mediator.LocationInfo.FullName,
							"  fileName: ",
							this._mediator.GetResourceFileName(culture)
						}));
					}
				}
				else
				{
					resourceSet = this.CreateResourceSet(text);
				}
				resourceSet2 = resourceSet;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return resourceSet2;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000AAC50 File Offset: 0x000A8E50
		public bool HasNeutralResources(CultureInfo culture, string defaultResName)
		{
			string text = this.FindResourceFile(culture, defaultResName);
			if (text == null || !File.Exists(text))
			{
				string text2 = this._mediator.ModuleDir;
				if (text != null)
				{
					text2 = Path.GetDirectoryName(text);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000AAC8C File Offset: 0x000A8E8C
		private string FindResourceFile(CultureInfo culture, string fileName)
		{
			if (this._mediator.ModuleDir != null)
			{
				string text = Path.Combine(this._mediator.ModuleDir, fileName);
				if (File.Exists(text))
				{
					return text;
				}
			}
			if (File.Exists(fileName))
			{
				return fileName;
			}
			return null;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000AACD0 File Offset: 0x000A8ED0
		[SecurityCritical]
		private ResourceSet CreateResourceSet(string file)
		{
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(file);
			}
			object[] array = new object[] { file };
			ResourceSet resourceSet;
			try
			{
				resourceSet = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, array);
			}
			catch (MissingMethodException ex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResMgrBadResSet_Type", new object[] { this._mediator.UserResourceSet.AssemblyQualifiedName }), ex);
			}
			return resourceSet;
		}

		// Token: 0x04001233 RID: 4659
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
