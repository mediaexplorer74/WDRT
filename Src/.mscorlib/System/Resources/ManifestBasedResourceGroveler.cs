using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Resources
{
	// Token: 0x0200038E RID: 910
	internal class ManifestBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x06002D0D RID: 11533 RVA: 0x000AAD58 File Offset: 0x000A8F58
		public ManifestBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000AAD68 File Offset: 0x000A8F68
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet resourceSet = null;
			Stream stream = null;
			RuntimeAssembly runtimeAssembly = null;
			CultureInfo cultureInfo = this.UltimateFallbackFixup(culture);
			if (cultureInfo.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				runtimeAssembly = this._mediator.MainAssembly;
			}
			else if (!cultureInfo.HasInvariantCultureName && !this._mediator.TryLookingForSatellite(cultureInfo))
			{
				runtimeAssembly = null;
			}
			else
			{
				runtimeAssembly = this.GetSatelliteAssembly(cultureInfo, ref stackMark);
				if (runtimeAssembly == null)
				{
					bool flag = culture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite;
					if (flag)
					{
						this.HandleSatelliteMissing();
					}
				}
			}
			string resourceFileName = this._mediator.GetResourceFileName(cultureInfo);
			if (runtimeAssembly != null)
			{
				lock (localResourceSets)
				{
					if (localResourceSets.TryGetValue(culture.Name, out resourceSet) && FrameworkEventSource.IsInitialized)
					{
						FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCacheUnexpected(this._mediator.BaseName, this._mediator.MainAssembly, culture.Name);
					}
				}
				stream = this.GetManifestResourceStream(runtimeAssembly, resourceFileName, ref stackMark);
			}
			if (FrameworkEventSource.IsInitialized)
			{
				if (stream != null)
				{
					FrameworkEventSource.Log.ResourceManagerStreamFound(this._mediator.BaseName, this._mediator.MainAssembly, culture.Name, runtimeAssembly, resourceFileName);
				}
				else
				{
					FrameworkEventSource.Log.ResourceManagerStreamNotFound(this._mediator.BaseName, this._mediator.MainAssembly, culture.Name, runtimeAssembly, resourceFileName);
				}
			}
			if (createIfNotExists && stream != null && resourceSet == null)
			{
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerCreatingResourceSet(this._mediator.BaseName, this._mediator.MainAssembly, culture.Name, resourceFileName);
				}
				resourceSet = this.CreateResourceSet(stream, runtimeAssembly);
			}
			else if (stream == null && tryParents)
			{
				bool hasInvariantCultureName = culture.HasInvariantCultureName;
				if (hasInvariantCultureName)
				{
					this.HandleResourceStreamMissing(resourceFileName);
				}
			}
			if (!createIfNotExists && stream != null && resourceSet == null && FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerNotCreatingResourceSet(this._mediator.BaseName, this._mediator.MainAssembly, culture.Name);
			}
			return resourceSet;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000AAF7C File Offset: 0x000A917C
		public bool HasNeutralResources(CultureInfo culture, string defaultResName)
		{
			string text = defaultResName;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				text = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString() + defaultResName;
			}
			string[] manifestResourceNames = this._mediator.MainAssembly.GetManifestResourceNames();
			foreach (string text2 in manifestResourceNames)
			{
				if (text2.Equals(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000AB00C File Offset: 0x000A920C
		private CultureInfo UltimateFallbackFixup(CultureInfo lookForCulture)
		{
			CultureInfo cultureInfo = lookForCulture;
			if (lookForCulture.Name == this._mediator.NeutralResourcesCulture.Name && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerNeutralResourcesSufficient(this._mediator.BaseName, this._mediator.MainAssembly, lookForCulture.Name);
				}
				cultureInfo = CultureInfo.InvariantCulture;
			}
			else if (lookForCulture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite)
			{
				cultureInfo = this._mediator.NeutralResourcesCulture;
			}
			return cultureInfo;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000AB0A0 File Offset: 0x000A92A0
		[SecurityCritical]
		internal static CultureInfo GetNeutralResourcesLanguage(Assembly a, ref UltimateResourceFallbackLocation fallbackLocation)
		{
			string text = null;
			short num = 0;
			if (!ManifestBasedResourceGroveler.GetNeutralResourcesLanguageAttribute(((RuntimeAssembly)a).GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref text), out num))
			{
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerNeutralResourceAttributeMissing(a);
				}
				fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
				return CultureInfo.InvariantCulture;
			}
			if (num < 0 || num > 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc", new object[] { num }));
			}
			fallbackLocation = (UltimateResourceFallbackLocation)num;
			CultureInfo cultureInfo2;
			try
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(text);
				cultureInfo2 = cultureInfo;
			}
			catch (ArgumentException ex)
			{
				if (!(a == typeof(object).Assembly))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_Asm_Culture", new object[]
					{
						a.ToString(),
						text
					}), ex);
				}
				cultureInfo2 = CultureInfo.InvariantCulture;
			}
			return cultureInfo2;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000AB178 File Offset: 0x000A9378
		[SecurityCritical]
		internal ResourceSet CreateResourceSet(Stream store, Assembly assembly)
		{
			if (store.CanSeek && store.Length > 4L)
			{
				long position = store.Position;
				BinaryReader binaryReader = new BinaryReader(store);
				int num = binaryReader.ReadInt32();
				if (num == ResourceManager.MagicNumber)
				{
					int num2 = binaryReader.ReadInt32();
					string text;
					string text2;
					if (num2 == ResourceManager.HeaderVersionNumber)
					{
						binaryReader.ReadInt32();
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
					}
					else
					{
						if (num2 <= ResourceManager.HeaderVersionNumber)
						{
							throw new NotSupportedException(Environment.GetResourceString("NotSupported_ObsoleteResourcesFile", new object[] { this._mediator.MainAssembly.GetSimpleName() }));
						}
						int num3 = binaryReader.ReadInt32();
						long num4 = binaryReader.BaseStream.Position + (long)num3;
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
						binaryReader.BaseStream.Seek(num4, SeekOrigin.Begin);
					}
					store.Position = position;
					if (this.CanUseDefaultResourceClasses(text, text2))
					{
						return new RuntimeResourceSet(store);
					}
					Type type = Type.GetType(text, true);
					IResourceReader resourceReader = (IResourceReader)Activator.CreateInstance(type, new object[] { store });
					object[] array = new object[] { resourceReader };
					Type type2;
					if (this._mediator.UserResourceSet == null)
					{
						type2 = Type.GetType(text2, true, false);
					}
					else
					{
						type2 = this._mediator.UserResourceSet;
					}
					return (ResourceSet)Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, array, null, null);
				}
				else
				{
					store.Position = position;
				}
			}
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(store);
			}
			object[] array2 = new object[] { store, assembly };
			ResourceSet resourceSet2;
			try
			{
				try
				{
					return (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, array2);
				}
				catch (MissingMethodException)
				{
				}
				array2 = new object[] { store };
				ResourceSet resourceSet = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, array2);
				resourceSet2 = resourceSet;
			}
			catch (MissingMethodException ex)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResMgrBadResSet_Type", new object[] { this._mediator.UserResourceSet.AssemblyQualifiedName }), ex);
			}
			return resourceSet2;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000AB3C0 File Offset: 0x000A95C0
		[SecurityCritical]
		private Stream GetManifestResourceStream(RuntimeAssembly satellite, string fileName, ref StackCrawlMark stackMark)
		{
			bool flag = this._mediator.MainAssembly == satellite && this._mediator.CallingAssembly == this._mediator.MainAssembly;
			Stream stream = satellite.GetManifestResourceStream(this._mediator.LocationInfo, fileName, flag, ref stackMark);
			if (stream == null)
			{
				stream = this.CaseInsensitiveManifestResourceStreamLookup(satellite, fileName);
			}
			return stream;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000AB424 File Offset: 0x000A9624
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Stream CaseInsensitiveManifestResourceStreamLookup(RuntimeAssembly satellite, string name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._mediator.LocationInfo != null)
			{
				string @namespace = this._mediator.LocationInfo.Namespace;
				if (@namespace != null)
				{
					stringBuilder.Append(@namespace);
					if (name != null)
					{
						stringBuilder.Append(Type.Delimiter);
					}
				}
			}
			stringBuilder.Append(name);
			string text = stringBuilder.ToString();
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			string text2 = null;
			foreach (string text3 in satellite.GetManifestResourceNames())
			{
				if (compareInfo.Compare(text3, text, CompareOptions.IgnoreCase) == 0)
				{
					if (text2 != null)
					{
						throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_MultipleBlobs", new object[]
						{
							text,
							satellite.ToString()
						}));
					}
					text2 = text3;
				}
			}
			if (FrameworkEventSource.IsInitialized)
			{
				if (text2 != null)
				{
					FrameworkEventSource.Log.ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(this._mediator.BaseName, this._mediator.MainAssembly, satellite.GetSimpleName(), text);
				}
				else
				{
					FrameworkEventSource.Log.ResourceManagerCaseInsensitiveResourceStreamLookupFailed(this._mediator.BaseName, this._mediator.MainAssembly, satellite.GetSimpleName(), text);
				}
			}
			if (text2 == null)
			{
				return null;
			}
			bool flag = this._mediator.MainAssembly == satellite && this._mediator.CallingAssembly == this._mediator.MainAssembly;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Stream manifestResourceStream = satellite.GetManifestResourceStream(text2, ref stackCrawlMark, flag);
			if (manifestResourceStream != null && FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerManifestResourceAccessDenied(this._mediator.BaseName, this._mediator.MainAssembly, satellite.GetSimpleName(), text2);
			}
			return manifestResourceStream;
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000AB5C4 File Offset: 0x000A97C4
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private RuntimeAssembly GetSatelliteAssembly(CultureInfo lookForCulture, ref StackCrawlMark stackMark)
		{
			if (!this._mediator.LookedForSatelliteContractVersion)
			{
				this._mediator.SatelliteContractVersion = this._mediator.ObtainSatelliteContractVersion(this._mediator.MainAssembly);
				this._mediator.LookedForSatelliteContractVersion = true;
			}
			RuntimeAssembly runtimeAssembly = null;
			string satelliteAssemblyName = this.GetSatelliteAssemblyName();
			try
			{
				runtimeAssembly = this._mediator.MainAssembly.InternalGetSatelliteAssembly(satelliteAssemblyName, lookForCulture, this._mediator.SatelliteContractVersion, false, ref stackMark);
			}
			catch (FileLoadException ex)
			{
				int hresult = ex._HResult;
				Win32Native.MakeHRFromErrorCode(5);
			}
			catch (BadImageFormatException ex2)
			{
			}
			if (FrameworkEventSource.IsInitialized)
			{
				if (runtimeAssembly != null)
				{
					FrameworkEventSource.Log.ResourceManagerGetSatelliteAssemblySucceeded(this._mediator.BaseName, this._mediator.MainAssembly, lookForCulture.Name, satelliteAssemblyName);
				}
				else
				{
					FrameworkEventSource.Log.ResourceManagerGetSatelliteAssemblyFailed(this._mediator.BaseName, this._mediator.MainAssembly, lookForCulture.Name, satelliteAssemblyName);
				}
			}
			return runtimeAssembly;
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000AB6CC File Offset: 0x000A98CC
		private bool CanUseDefaultResourceClasses(string readerTypeName, string resSetTypeName)
		{
			if (this._mediator.UserResourceSet != null)
			{
				return false;
			}
			AssemblyName assemblyName = new AssemblyName(ResourceManager.MscorlibName);
			return (readerTypeName == null || ResourceManager.CompareNames(readerTypeName, ResourceManager.ResReaderTypeName, assemblyName)) && (resSetTypeName == null || ResourceManager.CompareNames(resSetTypeName, ResourceManager.ResSetTypeName, assemblyName));
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000AB720 File Offset: 0x000A9920
		[SecurityCritical]
		private string GetSatelliteAssemblyName()
		{
			string simpleName = this._mediator.MainAssembly.GetSimpleName();
			return simpleName + ".resources";
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000AB74C File Offset: 0x000A994C
		[SecurityCritical]
		private void HandleSatelliteMissing()
		{
			string text = this._mediator.MainAssembly.GetSimpleName() + ".resources.dll";
			if (this._mediator.SatelliteContractVersion != null)
			{
				text = text + ", Version=" + this._mediator.SatelliteContractVersion.ToString();
			}
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this._mediator.MainAssembly.GetPublicKey());
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			int num = publicKeyToken.Length;
			StringBuilder stringBuilder = new StringBuilder(num * 2);
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(publicKeyToken[i].ToString("x", CultureInfo.InvariantCulture));
			}
			string text2 = text;
			string text3 = ", PublicKeyToken=";
			StringBuilder stringBuilder2 = stringBuilder;
			text = text2 + text3 + ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
			string text4 = this._mediator.NeutralResourcesCulture.Name;
			if (text4.Length == 0)
			{
				text4 = "<invariant>";
			}
			throw new MissingSatelliteAssemblyException(Environment.GetResourceString("MissingSatelliteAssembly_Culture_Name", new object[]
			{
				this._mediator.NeutralResourcesCulture,
				text
			}), text4);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000AB868 File Offset: 0x000A9A68
		[SecurityCritical]
		private void HandleResourceStreamMissing(string fileName)
		{
			if (this._mediator.MainAssembly == typeof(object).Assembly && this._mediator.BaseName.Equals("mscorlib"))
			{
				string text = "mscorlib.resources couldn't be found!  Large parts of the BCL won't work!";
				Environment.FailFast(text);
			}
			string text2 = string.Empty;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				text2 = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString();
			}
			text2 += fileName;
			throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoNeutralAsm", new object[]
			{
				text2,
				this._mediator.MainAssembly.GetSimpleName()
			}));
		}

		// Token: 0x06002D1A RID: 11546
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNeutralResourcesLanguageAttribute(RuntimeAssembly assemblyHandle, StringHandleOnStack cultureName, out short fallbackLocation);

		// Token: 0x04001234 RID: 4660
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
