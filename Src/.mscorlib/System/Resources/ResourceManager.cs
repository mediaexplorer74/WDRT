using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Resources
{
	/// <summary>Represents a resource manager that provides convenient access to culture-specific resources at run time.</summary>
	// Token: 0x02000395 RID: 917
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ResourceManager
	{
		// Token: 0x06002D32 RID: 11570 RVA: 0x000ABAFC File Offset: 0x000A9CFC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Init()
		{
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class with default values.</summary>
		// Token: 0x06002D33 RID: 11571 RVA: 0x000ABB10 File Offset: 0x000A9D10
		protected ResourceManager()
		{
			this.Init();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000ABB48 File Offset: 0x000A9D48
		private ResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (resourceDir == null)
			{
				throw new ArgumentNullException("resourceDir");
			}
			this.BaseNameField = baseName;
			this.moduleDir = resourceDir;
			this._userResourceSet = usingResourceSet;
			this.ResourceSets = new Hashtable();
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			this.UseManifest = false;
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				string resourceFileName = this.GetResourceFileName(invariantCulture);
				if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
				{
					FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
					return;
				}
				FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, resourceFileName);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that looks up resources contained in files with the specified root name in the given assembly.</summary>
		/// <param name="baseName">The root name of the resource file without its extension but including any fully qualified namespace name. For example, the root name for the resource file named MyApplication.MyResource.en-US.resources is MyApplication.MyResource.</param>
		/// <param name="assembly">The main assembly for the resources.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="assembly" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D35 RID: 11573 RVA: 0x000ABC28 File Offset: 0x000A9E28
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
			{
				this.m_callingAssembly = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that uses a specified <see cref="T:System.Resources.ResourceSet" /> class to look up resources contained in files with the specified root name in the given assembly.</summary>
		/// <param name="baseName">The root name of the resource file without its extension but including any fully qualified namespace name. For example, the root name for the resource file named MyApplication.MyResource.en-US.resources is MyApplication.MyResource.</param>
		/// <param name="assembly">The main assembly for the resources.</param>
		/// <param name="usingResourceSet">The type of the custom <see cref="T:System.Resources.ResourceSet" /> to use. If <see langword="null" />, the default runtime <see cref="T:System.Resources.ResourceSet" /> object is used.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="usingResourceset" /> is not a derived class of <see cref="T:System.Resources.ResourceSet" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="assembly" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D36 RID: 11574 RVA: 0x000ABCCC File Offset: 0x000A9ECC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(string baseName, Assembly assembly, Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!(assembly is RuntimeAssembly))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			if (usingResourceSet != null && usingResourceSet != ResourceManager._minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager._minResourceSet))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ResMgrNotResSet"), "usingResourceSet");
			}
			this._userResourceSet = usingResourceSet;
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (assembly == typeof(object).Assembly && this.m_callingAssembly != assembly)
			{
				this.m_callingAssembly = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResourceManager" /> class that looks up resources in satellite assemblies based on information from the specified type object.</summary>
		/// <param name="resourceSource">A type from which the resource manager derives all information for finding .resources files.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="resourceSource" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D37 RID: 11575 RVA: 0x000ABDA8 File Offset: 0x000A9FA8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public ResourceManager(Type resourceSource)
		{
			if (null == resourceSource)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (!(resourceSource is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			this._locationInfo = resourceSource;
			this.MainAssembly = this._locationInfo.Assembly;
			this.BaseNameField = resourceSource.Name;
			this.SetAppXConfiguration();
			this.CommonAssemblyInit();
			this.m_callingAssembly = (RuntimeAssembly)Assembly.GetCallingAssembly();
			if (this.MainAssembly == typeof(object).Assembly && this.m_callingAssembly != this.MainAssembly)
			{
				this.m_callingAssembly = null;
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000ABE5D File Offset: 0x000AA05D
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this._resourceSets = null;
			this.resourceGroveler = null;
			this._lastUsedResourceCache = null;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000ABE74 File Offset: 0x000AA074
		[SecuritySafeCritical]
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
			if (this.UseManifest)
			{
				this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			}
			else
			{
				this.resourceGroveler = new FileBasedResourceGroveler(resourceManagerMediator);
			}
			if (this.m_callingAssembly == null)
			{
				this.m_callingAssembly = (RuntimeAssembly)this._callingAssembly;
			}
			if (this.UseManifest && this._neutralResourcesCulture == null)
			{
				this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x000ABF06 File Offset: 0x000AA106
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this._callingAssembly = this.m_callingAssembly;
			this.UseSatelliteAssem = this.UseManifest;
			this.ResourceSets = new Hashtable();
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x000ABF2C File Offset: 0x000AA12C
		[SecuritySafeCritical]
		private void CommonAssemblyInit()
		{
			if (!this._bUsingModernResourceManagement)
			{
				this.UseManifest = true;
				this._resourceSets = new Dictionary<string, ResourceSet>();
				this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
				this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
				ResourceManager.ResourceManagerMediator resourceManagerMediator = new ResourceManager.ResourceManagerMediator(this);
				this.resourceGroveler = new ManifestBasedResourceGroveler(resourceManagerMediator);
			}
			this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, ref this._fallbackLoc);
			if (!this._bUsingModernResourceManagement)
			{
				if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
				{
					CultureInfo invariantCulture = CultureInfo.InvariantCulture;
					string resourceFileName = this.GetResourceFileName(invariantCulture);
					if (this.resourceGroveler.HasNeutralResources(invariantCulture, resourceFileName))
					{
						FrameworkEventSource.Log.ResourceManagerNeutralResourcesFound(this.BaseNameField, this.MainAssembly, resourceFileName);
					}
					else
					{
						string text = resourceFileName;
						if (this._locationInfo != null && this._locationInfo.Namespace != null)
						{
							text = this._locationInfo.Namespace + Type.Delimiter.ToString() + resourceFileName;
						}
						FrameworkEventSource.Log.ResourceManagerNeutralResourcesNotFound(this.BaseNameField, this.MainAssembly, text);
					}
				}
				this.ResourceSets = new Hashtable();
			}
		}

		/// <summary>Gets the root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</summary>
		/// <returns>The root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000AC048 File Offset: 0x000AA248
		public virtual string BaseName
		{
			get
			{
				return this.BaseNameField;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the resource manager allows case-insensitive resource lookups in the <see cref="M:System.Resources.ResourceManager.GetString(System.String)" /> and <see cref="M:System.Resources.ResourceManager.GetObject(System.String)" /> methods.</summary>
		/// <returns>
		///   <see langword="true" /> to ignore case during resource lookup; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000AC050 File Offset: 0x000AA250
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x000AC058 File Offset: 0x000AA258
		public virtual bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
			set
			{
				this._ignoreCase = value;
			}
		}

		/// <summary>Gets the type of the resource set object that the resource manager uses to construct a <see cref="T:System.Resources.ResourceSet" /> object.</summary>
		/// <returns>The type of the resource set object that the resource manager uses to construct a <see cref="T:System.Resources.ResourceSet" /> object.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000AC061 File Offset: 0x000AA261
		public virtual Type ResourceSetType
		{
			get
			{
				if (!(this._userResourceSet == null))
				{
					return this._userResourceSet;
				}
				return typeof(RuntimeResourceSet);
			}
		}

		/// <summary>Gets or sets the location from which to retrieve default fallback resources.</summary>
		/// <returns>One of the enumeration values that specifies where the resource manager can look for fallback resources.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002D40 RID: 11584 RVA: 0x000AC082 File Offset: 0x000AA282
		// (set) Token: 0x06002D41 RID: 11585 RVA: 0x000AC08A File Offset: 0x000AA28A
		protected UltimateResourceFallbackLocation FallbackLocation
		{
			get
			{
				return this._fallbackLoc;
			}
			set
			{
				this._fallbackLoc = value;
			}
		}

		/// <summary>Tells the resource manager to call the <see cref="M:System.Resources.ResourceSet.Close" /> method on all <see cref="T:System.Resources.ResourceSet" /> objects and release all resources.</summary>
		// Token: 0x06002D42 RID: 11586 RVA: 0x000AC094 File Offset: 0x000AA294
		public virtual void ReleaseAllResources()
		{
			if (FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerReleasingResources(this.BaseNameField, this.MainAssembly);
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				IDictionaryEnumerator dictionaryEnumerator = resourceSets.GetEnumerator();
				IDictionaryEnumerator dictionaryEnumerator2 = null;
				if (this.ResourceSets != null)
				{
					dictionaryEnumerator2 = this.ResourceSets.GetEnumerator();
				}
				this.ResourceSets = new Hashtable();
				while (dictionaryEnumerator.MoveNext())
				{
					((ResourceSet)dictionaryEnumerator.Value).Close();
				}
				if (dictionaryEnumerator2 != null)
				{
					while (dictionaryEnumerator2.MoveNext())
					{
						((ResourceSet)dictionaryEnumerator2.Value).Close();
					}
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.Resources.ResourceManager" /> object that searches a specific directory instead of an assembly manifest for resources.</summary>
		/// <param name="baseName">The root name of the resources. For example, the root name for the resource file named "MyResource.en-US.resources" is "MyResource".</param>
		/// <param name="resourceDir">The name of the directory to search for the resources. <paramref name="resourceDir" /> can be an absolute path or a relative path from the application directory.</param>
		/// <param name="usingResourceSet">The type of the custom <see cref="T:System.Resources.ResourceSet" /> to use. If <see langword="null" />, the default runtime <see cref="T:System.Resources.ResourceSet" /> object is used.</param>
		/// <returns>A new instance of a resource manager that searches the specified directory instead of an assembly manifest for resources.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="baseName" /> or <paramref name="resourceDir" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002D43 RID: 11587 RVA: 0x000AC16C File Offset: 0x000AA36C
		public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, Type usingResourceSet)
		{
			return new ResourceManager(baseName, resourceDir, usingResourceSet);
		}

		/// <summary>Generates the name of the resource file for the given <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <param name="culture">The culture object for which a resource file name is constructed.</param>
		/// <returns>The name that can be used for a resource file for the given <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		// Token: 0x06002D44 RID: 11588 RVA: 0x000AC178 File Offset: 0x000AA378
		protected virtual string GetResourceFileName(CultureInfo culture)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			stringBuilder.Append(this.BaseNameField);
			if (!culture.HasInvariantCultureName)
			{
				CultureInfo.VerifyCultureName(culture.Name, true);
				stringBuilder.Append('.');
				stringBuilder.Append(culture.Name);
			}
			stringBuilder.Append(".resources");
			return stringBuilder.ToString();
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000AC1DC File Offset: 0x000AA3DC
		internal ResourceSet GetFirstResourceSet(CultureInfo culture)
		{
			if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
			{
				culture = CultureInfo.InvariantCulture;
			}
			if (this._lastUsedResourceCache != null)
			{
				ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
				lock (lastUsedResourceCache)
				{
					if (culture.Name == this._lastUsedResourceCache.lastCultureName)
					{
						return this._lastUsedResourceCache.lastResourceSet;
					}
				}
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					resourceSets.TryGetValue(culture.Name, out resourceSet);
				}
			}
			if (resourceSet != null)
			{
				if (this._lastUsedResourceCache != null)
				{
					ResourceManager.CultureNameResourceSetPair lastUsedResourceCache2 = this._lastUsedResourceCache;
					lock (lastUsedResourceCache2)
					{
						this._lastUsedResourceCache.lastCultureName = culture.Name;
						this._lastUsedResourceCache.lastResourceSet = resourceSet;
					}
				}
				return resourceSet;
			}
			return null;
		}

		/// <summary>Retrieves the resource set for a particular culture.</summary>
		/// <param name="culture">The culture whose resources are to be retrieved.</param>
		/// <param name="createIfNotExists">
		///   <see langword="true" /> to load the resource set, if it has not been loaded yet; otherwise, <see langword="false" />.</param>
		/// <param name="tryParents">
		///   <see langword="true" /> to use resource fallback to load an appropriate resource if the resource set cannot be found; <see langword="false" /> to bypass the resource fallback process.</param>
		/// <returns>The resource set for the specified culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="culture" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">
		///   <paramref name="tryParents" /> is <see langword="true" />, no usable set of resources has been found, and there are no default culture resources.</exception>
		// Token: 0x06002D46 RID: 11590 RVA: 0x000AC310 File Offset: 0x000AA510
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> dictionary = resourceSets;
				lock (dictionary)
				{
					ResourceSet resourceSet;
					if (resourceSets.TryGetValue(culture.Name, out resourceSet))
					{
						return resourceSet;
					}
				}
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (this.UseManifest && culture.HasInvariantCultureName)
			{
				string resourceFileName = this.GetResourceFileName(culture);
				RuntimeAssembly runtimeAssembly = (RuntimeAssembly)this.MainAssembly;
				Stream manifestResourceStream = runtimeAssembly.GetManifestResourceStream(this._locationInfo, resourceFileName, this.m_callingAssembly == this.MainAssembly, ref stackCrawlMark);
				if (createIfNotExists && manifestResourceStream != null)
				{
					ResourceSet resourceSet = ((ManifestBasedResourceGroveler)this.resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
					ResourceManager.AddResourceSet(resourceSets, culture.Name, ref resourceSet);
					return resourceSet;
				}
			}
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
		}

		/// <summary>Provides the implementation for finding a resource set.</summary>
		/// <param name="culture">The culture object to look for.</param>
		/// <param name="createIfNotExists">
		///   <see langword="true" /> to load the resource set, if it has not been loaded yet; otherwise, <see langword="false" />.</param>
		/// <param name="tryParents">
		///   <see langword="true" /> to check parent <see cref="T:System.Globalization.CultureInfo" /> objects if the resource set cannot be loaded; otherwise, <see langword="false" />.</param>
		/// <returns>The specified resource set.</returns>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">The main assembly does not contain a .resources file, which is required to look up a resource.</exception>
		/// <exception cref="T:System.ExecutionEngineException">There was an internal error in the runtime.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The satellite assembly associated with <paramref name="culture" /> could not be located.</exception>
		// Token: 0x06002D47 RID: 11591 RVA: 0x000AC400 File Offset: 0x000AA600
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents, ref stackCrawlMark);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x000AC41C File Offset: 0x000AA61C
		[SecurityCritical]
		private ResourceSet InternalGetResourceSet(CultureInfo requestedCulture, bool createIfNotExists, bool tryParents, ref StackCrawlMark stackMark)
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			CultureInfo cultureInfo = null;
			Dictionary<string, ResourceSet> dictionary = resourceSets;
			lock (dictionary)
			{
				if (resourceSets.TryGetValue(requestedCulture.Name, out resourceSet))
				{
					if (FrameworkEventSource.IsInitialized)
					{
						FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, requestedCulture.Name);
					}
					return resourceSet;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(requestedCulture, this._neutralResourcesCulture, tryParents);
			foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
			{
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerLookingForResourceSet(this.BaseNameField, this.MainAssembly, cultureInfo2.Name);
				}
				Dictionary<string, ResourceSet> dictionary2 = resourceSets;
				lock (dictionary2)
				{
					if (resourceSets.TryGetValue(cultureInfo2.Name, out resourceSet))
					{
						if (FrameworkEventSource.IsInitialized)
						{
							FrameworkEventSource.Log.ResourceManagerFoundResourceSetInCache(this.BaseNameField, this.MainAssembly, cultureInfo2.Name);
						}
						if (requestedCulture != cultureInfo2)
						{
							cultureInfo = cultureInfo2;
						}
						break;
					}
				}
				resourceSet = this.resourceGroveler.GrovelForResourceSet(cultureInfo2, resourceSets, tryParents, createIfNotExists, ref stackMark);
				if (resourceSet != null)
				{
					cultureInfo = cultureInfo2;
					break;
				}
			}
			if (resourceSet != null && cultureInfo != null)
			{
				foreach (CultureInfo cultureInfo3 in resourceFallbackManager)
				{
					ResourceManager.AddResourceSet(resourceSets, cultureInfo3.Name, ref resourceSet);
					if (cultureInfo3 == cultureInfo)
					{
						break;
					}
				}
			}
			return resourceSet;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000AC5E4 File Offset: 0x000AA7E4
		private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
		{
			lock (localResourceSets)
			{
				ResourceSet resourceSet;
				if (localResourceSets.TryGetValue(cultureName, out resourceSet))
				{
					if (resourceSet != rs)
					{
						if (!localResourceSets.ContainsValue(rs))
						{
							rs.Dispose();
						}
						rs = resourceSet;
					}
				}
				else
				{
					localResourceSets.Add(cultureName, rs);
				}
			}
		}

		/// <summary>Returns the version specified by the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> attribute in the given assembly.</summary>
		/// <param name="a">The assembly to check for the <see cref="T:System.Resources.SatelliteContractVersionAttribute" /> attribute.</param>
		/// <returns>The satellite contract version of the given assembly, or <see langword="null" /> if no version was found.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Version" /> found in the assembly <paramref name="a" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="a" /> is <see langword="null" />.</exception>
		// Token: 0x06002D4A RID: 11594 RVA: 0x000AC648 File Offset: 0x000AA848
		protected static Version GetSatelliteContractVersion(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a", Environment.GetResourceString("ArgumentNull_Assembly"));
			}
			string text = null;
			if (a.ReflectionOnly)
			{
				foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(a))
				{
					if (customAttributeData.Constructor.DeclaringType == typeof(SatelliteContractVersionAttribute))
					{
						text = (string)customAttributeData.ConstructorArguments[0].Value;
						break;
					}
				}
				if (text == null)
				{
					return null;
				}
			}
			else
			{
				object[] customAttributes = a.GetCustomAttributes(typeof(SatelliteContractVersionAttribute), false);
				if (customAttributes.Length == 0)
				{
					return null;
				}
				text = ((SatelliteContractVersionAttribute)customAttributes[0]).Version;
			}
			Version version;
			try
			{
				version = new Version(text);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				if (a == typeof(object).Assembly)
				{
					return null;
				}
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidSatelliteContract_Asm_Ver", new object[]
				{
					a.ToString(),
					text
				}), ex);
			}
			return version;
		}

		/// <summary>Returns culture-specific information for the main assembly's default resources by retrieving the value of the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> attribute on a specified assembly.</summary>
		/// <param name="a">The assembly for which to return culture-specific information.</param>
		/// <returns>The culture from the <see cref="T:System.Resources.NeutralResourcesLanguageAttribute" /> attribute, if found; otherwise, the invariant culture.</returns>
		// Token: 0x06002D4B RID: 11595 RVA: 0x000AC77C File Offset: 0x000AA97C
		[SecuritySafeCritical]
		protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
		{
			UltimateResourceFallbackLocation ultimateResourceFallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
			return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, ref ultimateResourceFallbackLocation);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x000AC798 File Offset: 0x000AA998
		internal static bool CompareNames(string asmTypeName1, string typeName2, AssemblyName asmName2)
		{
			int num = asmTypeName1.IndexOf(',');
			if (((num == -1) ? asmTypeName1.Length : num) != typeName2.Length)
			{
				return false;
			}
			if (string.Compare(asmTypeName1, 0, typeName2, 0, typeName2.Length, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (num == -1)
			{
				return true;
			}
			while (char.IsWhiteSpace(asmTypeName1[++num]))
			{
			}
			AssemblyName assemblyName = new AssemblyName(asmTypeName1.Substring(num));
			if (string.Compare(assemblyName.Name, asmName2.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (assemblyName.CultureInfo != null && asmName2.CultureInfo != null && assemblyName.CultureInfo.LCID != asmName2.CultureInfo.LCID)
			{
				return false;
			}
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			byte[] publicKeyToken2 = asmName2.GetPublicKeyToken();
			if (publicKeyToken != null && publicKeyToken2 != null)
			{
				if (publicKeyToken.Length != publicKeyToken2.Length)
				{
					return false;
				}
				for (int i = 0; i < publicKeyToken.Length; i++)
				{
					if (publicKeyToken[i] != publicKeyToken2[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000AC890 File Offset: 0x000AAA90
		[SecuritySafeCritical]
		private string GetStringFromPRI(string stringName, string startingCulture, string neutralResourcesCulture)
		{
			if (stringName.Length == 0)
			{
				return null;
			}
			return this._WinRTResourceManager.GetString(stringName, string.IsNullOrEmpty(startingCulture) ? null : startingCulture, string.IsNullOrEmpty(neutralResourcesCulture) ? null : neutralResourcesCulture);
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000AC8D0 File Offset: 0x000AAAD0
		[SecurityCritical]
		internal static WindowsRuntimeResourceManagerBase GetWinRTResourceManager()
		{
			Type type = Type.GetType("System.Resources.WindowsRuntimeResourceManager, System.Runtime.WindowsRuntime, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", true);
			return (WindowsRuntimeResourceManagerBase)Activator.CreateInstance(type, true);
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000AC8F8 File Offset: 0x000AAAF8
		[SecuritySafeCritical]
		private bool ShouldUseSatelliteAssemblyResourceLookupUnderAppX(RuntimeAssembly resourcesAssembly)
		{
			return resourcesAssembly.IsFrameworkAssembly();
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000AC910 File Offset: 0x000AAB10
		[SecuritySafeCritical]
		private void SetAppXConfiguration()
		{
			bool flag = false;
			RuntimeAssembly runtimeAssembly = (RuntimeAssembly)this.MainAssembly;
			if (runtimeAssembly == null)
			{
				runtimeAssembly = this.m_callingAssembly;
			}
			if (runtimeAssembly != null && runtimeAssembly != typeof(object).Assembly && AppDomain.IsAppXModel() && !AppDomain.IsAppXNGen)
			{
				ResourceManager.s_IsAppXModel = true;
				string text = ((this._locationInfo == null) ? this.BaseNameField : this._locationInfo.FullName);
				if (text == null)
				{
					text = string.Empty;
				}
				WindowsRuntimeResourceManagerBase windowsRuntimeResourceManagerBase = null;
				bool flag2 = false;
				if (AppDomain.IsAppXDesignMode())
				{
					windowsRuntimeResourceManagerBase = ResourceManager.GetWinRTResourceManager();
					try
					{
						PRIExceptionInfo priexceptionInfo;
						flag2 = windowsRuntimeResourceManagerBase.Initialize(runtimeAssembly.Location, text, out priexceptionInfo);
						flag = !flag2;
					}
					catch (Exception ex)
					{
						flag = true;
						if (ex.IsTransient)
						{
							throw;
						}
					}
				}
				if (!flag)
				{
					this._bUsingModernResourceManagement = !this.ShouldUseSatelliteAssemblyResourceLookupUnderAppX(runtimeAssembly);
					if (this._bUsingModernResourceManagement)
					{
						if (windowsRuntimeResourceManagerBase != null && flag2)
						{
							this._WinRTResourceManager = windowsRuntimeResourceManagerBase;
							this._PRIonAppXInitialized = true;
							return;
						}
						this._WinRTResourceManager = ResourceManager.GetWinRTResourceManager();
						try
						{
							this._PRIonAppXInitialized = this._WinRTResourceManager.Initialize(runtimeAssembly.Location, text, out this._PRIExceptionInfo);
						}
						catch (FileNotFoundException)
						{
						}
						catch (Exception ex2)
						{
							if (ex2.HResult != -2147009761)
							{
								throw;
							}
						}
					}
				}
			}
		}

		/// <summary>Returns the value of the specified string resource.</summary>
		/// <param name="name">The name of the resource to retrieve.</param>
		/// <returns>The value of the resource localized for the caller's current UI culture, or <see langword="null" /> if <paramref name="name" /> cannot be found in a resource set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a string.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources has been found, and there are no resources for the default culture. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002D51 RID: 11601 RVA: 0x000ACA84 File Offset: 0x000AAC84
		[__DynamicallyInvokable]
		public virtual string GetString(string name)
		{
			return this.GetString(name, null);
		}

		/// <summary>Returns the value of the string resource localized for the specified culture.</summary>
		/// <param name="name">The name of the resource to retrieve.</param>
		/// <param name="culture">An object that represents the culture for which the resource is localized.</param>
		/// <returns>The value of the resource localized for the specified culture, or <see langword="null" /> if <paramref name="name" /> cannot be found in a resource set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a string.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources has been found, and there are no resources for a default culture. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002D52 RID: 11602 RVA: 0x000ACA90 File Offset: 0x000AAC90
		[__DynamicallyInvokable]
		public virtual string GetString(string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
			{
				culture = null;
			}
			if (!this._bUsingModernResourceManagement)
			{
				if (culture == null)
				{
					culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
				}
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
				}
				ResourceSet resourceSet = this.GetFirstResourceSet(culture);
				if (resourceSet != null)
				{
					string @string = resourceSet.GetString(name, this._ignoreCase);
					if (@string != null)
					{
						return @string;
					}
				}
				ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, true);
				foreach (CultureInfo cultureInfo in resourceFallbackManager)
				{
					ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
					if (resourceSet2 == null)
					{
						break;
					}
					if (resourceSet2 != resourceSet)
					{
						string string2 = resourceSet2.GetString(name, this._ignoreCase);
						if (string2 != null)
						{
							if (this._lastUsedResourceCache != null)
							{
								ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
								lock (lastUsedResourceCache)
								{
									this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
									this._lastUsedResourceCache.lastResourceSet = resourceSet2;
								}
							}
							return string2;
						}
						resourceSet = resourceSet2;
					}
				}
				if (FrameworkEventSource.IsInitialized)
				{
					FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
				}
				return null;
			}
			if (this._PRIonAppXInitialized)
			{
				return this.GetStringFromPRI(name, (culture == null) ? null : culture.Name, this._neutralResourcesCulture.Name);
			}
			if (this._PRIExceptionInfo != null && this._PRIExceptionInfo._PackageSimpleName != null && this._PRIExceptionInfo._ResWFile != null)
			{
				throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_ResWFileNotLoaded", new object[]
				{
					this._PRIExceptionInfo._ResWFile,
					this._PRIExceptionInfo._PackageSimpleName
				}));
			}
			throw new MissingManifestResourceException(Environment.GetResourceString("MissingManifestResource_NoPRIresources"));
		}

		/// <summary>Returns the value of the specified non-string resource.</summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <returns>The value of the resource localized for the caller's current culture settings. If an appropriate resource set exists but <paramref name="name" /> cannot be found, the method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of localized resources has been found, and there are no default culture resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002D53 RID: 11603 RVA: 0x000ACCA4 File Offset: 0x000AAEA4
		public virtual object GetObject(string name)
		{
			return this.GetObject(name, null, true);
		}

		/// <summary>Gets the value of the specified non-string resource localized for the specified culture.</summary>
		/// <param name="name">The name of the resource to get.</param>
		/// <param name="culture">The culture for which the resource is localized. If the resource is not localized for this culture, the resource manager uses fallback rules to locate an appropriate resource.  
		///  If this value is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> object is obtained by using the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
		/// <returns>The value of the resource, localized for the specified culture. If an appropriate resource set exists but <paramref name="name" /> cannot be found, the method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources have been found, and there are no default culture resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002D54 RID: 11604 RVA: 0x000ACCAF File Offset: 0x000AAEAF
		public virtual object GetObject(string name, CultureInfo culture)
		{
			return this.GetObject(name, culture, true);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000ACCBC File Offset: 0x000AAEBC
		private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (ResourceManager.s_IsAppXModel && culture == CultureInfo.CurrentUICulture)
			{
				culture = null;
			}
			if (culture == null)
			{
				culture = Thread.CurrentThread.GetCurrentUICultureNoAppX();
			}
			if (FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerLookupStarted(this.BaseNameField, this.MainAssembly, culture.Name);
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				object @object = resourceSet.GetObject(name, this._ignoreCase);
				if (@object != null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
					if (unmanagedMemoryStream != null && wrapUnmanagedMemStream)
					{
						return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream);
					}
					return @object;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, true);
			foreach (CultureInfo cultureInfo in resourceFallbackManager)
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					object object2 = resourceSet2.GetObject(name, this._ignoreCase);
					if (object2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						UnmanagedMemoryStream unmanagedMemoryStream2 = object2 as UnmanagedMemoryStream;
						if (unmanagedMemoryStream2 != null && wrapUnmanagedMemStream)
						{
							return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream2);
						}
						return object2;
					}
					else
					{
						resourceSet = resourceSet2;
					}
				}
			}
			if (FrameworkEventSource.IsInitialized)
			{
				FrameworkEventSource.Log.ResourceManagerLookupFailed(this.BaseNameField, this.MainAssembly, culture.Name);
			}
			return null;
		}

		/// <summary>Returns an unmanaged memory stream object from the specified resource.</summary>
		/// <param name="name">The name of a resource.</param>
		/// <returns>An unmanaged memory stream object that represents a resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a <see cref="T:System.IO.MemoryStream" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources is found, and there are no default resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002D56 RID: 11606 RVA: 0x000ACE74 File Offset: 0x000AB074
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name)
		{
			return this.GetStream(name, null);
		}

		/// <summary>Returns an unmanaged memory stream object from the specified resource, using the specified culture.</summary>
		/// <param name="name">The name of a resource.</param>
		/// <param name="culture">An  object that specifies the culture to use for the resource lookup. If <paramref name="culture" /> is <see langword="null" />, the culture for the current thread is used.</param>
		/// <returns>An unmanaged memory stream object that represents a resource.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the specified resource is not a <see cref="T:System.IO.MemoryStream" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Resources.MissingManifestResourceException">No usable set of resources is found, and there are no default resources. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		/// <exception cref="T:System.Resources.MissingSatelliteAssemblyException">The default culture's resources reside in a satellite assembly that could not be found. For information about how to handle this exception, see the "Handling MissingManifestResourceException and MissingSatelliteAssemblyException Exceptions" section in the <see cref="T:System.Resources.ResourceManager" /> class topic.</exception>
		// Token: 0x06002D57 RID: 11607 RVA: 0x000ACE80 File Offset: 0x000AB080
		[ComVisible(false)]
		public UnmanagedMemoryStream GetStream(string name, CultureInfo culture)
		{
			object @object = this.GetObject(name, culture, false);
			UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
			if (unmanagedMemoryStream == null && @object != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotStream_Name", new object[] { name }));
			}
			return unmanagedMemoryStream;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000ACEC0 File Offset: 0x000AB0C0
		[SecurityCritical]
		private bool TryLookingForSatellite(CultureInfo lookForCulture)
		{
			if (!ResourceManager._checkedConfigFile)
			{
				lock (this)
				{
					if (!ResourceManager._checkedConfigFile)
					{
						ResourceManager._checkedConfigFile = true;
						ResourceManager._installedSatelliteInfo = this.GetSatelliteAssembliesFromConfig();
					}
				}
			}
			if (ResourceManager._installedSatelliteInfo == null)
			{
				return true;
			}
			string[] array = (string[])ResourceManager._installedSatelliteInfo[this.MainAssembly.FullName];
			if (array == null)
			{
				return true;
			}
			int num = Array.IndexOf<string>(array, lookForCulture.Name);
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled())
			{
				if (num < 0)
				{
					FrameworkEventSource.Log.ResourceManagerCultureNotFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
				}
				else
				{
					FrameworkEventSource.Log.ResourceManagerCultureFoundInConfigFile(this.BaseNameField, this.MainAssembly, lookForCulture.Name);
				}
			}
			return num >= 0;
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000ACFB0 File Offset: 0x000AB1B0
		[SecurityCritical]
		private Hashtable GetSatelliteAssembliesFromConfig()
		{
			string configurationFileInternal = AppDomain.CurrentDomain.FusionStore.ConfigurationFileInternal;
			if (configurationFileInternal == null)
			{
				return null;
			}
			if (configurationFileInternal.Length >= 2 && (configurationFileInternal[1] == Path.VolumeSeparatorChar || (configurationFileInternal[0] == Path.DirectorySeparatorChar && configurationFileInternal[1] == Path.DirectorySeparatorChar)) && !File.InternalExists(configurationFileInternal))
			{
				return null;
			}
			ConfigTreeParser configTreeParser = new ConfigTreeParser();
			string text = "/configuration/satelliteassemblies";
			ConfigNode configNode = null;
			try
			{
				configNode = configTreeParser.Parse(configurationFileInternal, text, true);
			}
			catch (Exception)
			{
			}
			if (configNode == null)
			{
				return null;
			}
			Hashtable hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
			foreach (ConfigNode configNode2 in configNode.Children)
			{
				if (!string.Equals(configNode2.Name, "assembly"))
				{
					throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTag", new object[]
					{
						Path.GetFileName(configurationFileInternal),
						configNode2.Name
					}));
				}
				if (configNode2.Attributes.Count == 0)
				{
					throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagNoAttr", new object[] { Path.GetFileName(configurationFileInternal) }));
				}
				DictionaryEntry dictionaryEntry = configNode2.Attributes[0];
				string text2 = (string)dictionaryEntry.Value;
				if (!object.Equals(dictionaryEntry.Key, "name") || string.IsNullOrEmpty(text2) || configNode2.Attributes.Count > 1)
				{
					throw new ApplicationException(Environment.GetResourceString("XMLSyntax_InvalidSyntaxSatAssemTagBadAttr", new object[]
					{
						Path.GetFileName(configurationFileInternal),
						dictionaryEntry.Key,
						dictionaryEntry.Value
					}));
				}
				ArrayList arrayList = new ArrayList(5);
				foreach (ConfigNode configNode3 in configNode2.Children)
				{
					if (configNode3.Value != null)
					{
						arrayList.Add(configNode3.Value);
					}
				}
				string[] array = new string[arrayList.Count];
				for (int i = 0; i < array.Length; i++)
				{
					string text3 = (string)arrayList[i];
					array[i] = text3;
					if (FrameworkEventSource.IsInitialized)
					{
						FrameworkEventSource.Log.ResourceManagerAddingCultureFromConfigFile(this.BaseNameField, this.MainAssembly, text3);
					}
				}
				hashtable.Add(text2, array);
			}
			return hashtable;
		}

		/// <summary>Specifies the root name of the resource files that the <see cref="T:System.Resources.ResourceManager" /> searches for resources.</summary>
		// Token: 0x0400123D RID: 4669
		protected string BaseNameField;

		/// <summary>Contains a <see cref="T:System.Collections.Hashtable" /> that returns a mapping from cultures to <see cref="T:System.Resources.ResourceSet" /> objects.</summary>
		// Token: 0x0400123E RID: 4670
		[Obsolete("call InternalGetResourceSet instead")]
		protected Hashtable ResourceSets;

		// Token: 0x0400123F RID: 4671
		[NonSerialized]
		private Dictionary<string, ResourceSet> _resourceSets;

		// Token: 0x04001240 RID: 4672
		private string moduleDir;

		/// <summary>Specifies the main assembly that contains the resources.</summary>
		// Token: 0x04001241 RID: 4673
		protected Assembly MainAssembly;

		// Token: 0x04001242 RID: 4674
		private Type _locationInfo;

		// Token: 0x04001243 RID: 4675
		private Type _userResourceSet;

		// Token: 0x04001244 RID: 4676
		private CultureInfo _neutralResourcesCulture;

		// Token: 0x04001245 RID: 4677
		[NonSerialized]
		private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;

		// Token: 0x04001246 RID: 4678
		private bool _ignoreCase;

		// Token: 0x04001247 RID: 4679
		private bool UseManifest;

		// Token: 0x04001248 RID: 4680
		[OptionalField(VersionAdded = 1)]
		private bool UseSatelliteAssem;

		// Token: 0x04001249 RID: 4681
		private static volatile Hashtable _installedSatelliteInfo;

		// Token: 0x0400124A RID: 4682
		private static volatile bool _checkedConfigFile;

		// Token: 0x0400124B RID: 4683
		[OptionalField]
		private UltimateResourceFallbackLocation _fallbackLoc;

		// Token: 0x0400124C RID: 4684
		[OptionalField]
		private Version _satelliteContractVersion;

		// Token: 0x0400124D RID: 4685
		[OptionalField]
		private bool _lookedForSatelliteContractVersion;

		// Token: 0x0400124E RID: 4686
		[OptionalField(VersionAdded = 1)]
		private Assembly _callingAssembly;

		// Token: 0x0400124F RID: 4687
		[OptionalField(VersionAdded = 4)]
		private RuntimeAssembly m_callingAssembly;

		// Token: 0x04001250 RID: 4688
		[NonSerialized]
		private IResourceGroveler resourceGroveler;

		/// <summary>Holds the number used to identify resource files.</summary>
		// Token: 0x04001251 RID: 4689
		public static readonly int MagicNumber = -1091581234;

		/// <summary>Specifies the version of resource file headers that the current implementation of <see cref="T:System.Resources.ResourceManager" /> can interpret and produce.</summary>
		// Token: 0x04001252 RID: 4690
		public static readonly int HeaderVersionNumber = 1;

		// Token: 0x04001253 RID: 4691
		private static readonly Type _minResourceSet = typeof(ResourceSet);

		// Token: 0x04001254 RID: 4692
		internal static readonly string ResReaderTypeName = typeof(ResourceReader).FullName;

		// Token: 0x04001255 RID: 4693
		internal static readonly string ResSetTypeName = typeof(RuntimeResourceSet).FullName;

		// Token: 0x04001256 RID: 4694
		internal static readonly string MscorlibName = typeof(ResourceReader).Assembly.FullName;

		// Token: 0x04001257 RID: 4695
		internal const string ResFileExtension = ".resources";

		// Token: 0x04001258 RID: 4696
		internal const int ResFileExtensionLength = 10;

		// Token: 0x04001259 RID: 4697
		internal static readonly int DEBUG = 0;

		// Token: 0x0400125A RID: 4698
		private static volatile bool s_IsAppXModel;

		// Token: 0x0400125B RID: 4699
		[NonSerialized]
		private bool _bUsingModernResourceManagement;

		// Token: 0x0400125C RID: 4700
		[SecurityCritical]
		[NonSerialized]
		private WindowsRuntimeResourceManagerBase _WinRTResourceManager;

		// Token: 0x0400125D RID: 4701
		[NonSerialized]
		private bool _PRIonAppXInitialized;

		// Token: 0x0400125E RID: 4702
		[NonSerialized]
		private PRIExceptionInfo _PRIExceptionInfo;

		// Token: 0x02000B5F RID: 2911
		internal class CultureNameResourceSetPair
		{
			// Token: 0x04003444 RID: 13380
			public string lastCultureName;

			// Token: 0x04003445 RID: 13381
			public ResourceSet lastResourceSet;
		}

		// Token: 0x02000B60 RID: 2912
		internal class ResourceManagerMediator
		{
			// Token: 0x06006C1E RID: 27678 RVA: 0x00177677 File Offset: 0x00175877
			internal ResourceManagerMediator(ResourceManager rm)
			{
				if (rm == null)
				{
					throw new ArgumentNullException("rm");
				}
				this._rm = rm;
			}

			// Token: 0x1700123D RID: 4669
			// (get) Token: 0x06006C1F RID: 27679 RVA: 0x00177694 File Offset: 0x00175894
			internal string ModuleDir
			{
				get
				{
					return this._rm.moduleDir;
				}
			}

			// Token: 0x1700123E RID: 4670
			// (get) Token: 0x06006C20 RID: 27680 RVA: 0x001776A1 File Offset: 0x001758A1
			internal Type LocationInfo
			{
				get
				{
					return this._rm._locationInfo;
				}
			}

			// Token: 0x1700123F RID: 4671
			// (get) Token: 0x06006C21 RID: 27681 RVA: 0x001776AE File Offset: 0x001758AE
			internal Type UserResourceSet
			{
				get
				{
					return this._rm._userResourceSet;
				}
			}

			// Token: 0x17001240 RID: 4672
			// (get) Token: 0x06006C22 RID: 27682 RVA: 0x001776BB File Offset: 0x001758BB
			internal string BaseNameField
			{
				get
				{
					return this._rm.BaseNameField;
				}
			}

			// Token: 0x17001241 RID: 4673
			// (get) Token: 0x06006C23 RID: 27683 RVA: 0x001776C8 File Offset: 0x001758C8
			// (set) Token: 0x06006C24 RID: 27684 RVA: 0x001776D5 File Offset: 0x001758D5
			internal CultureInfo NeutralResourcesCulture
			{
				get
				{
					return this._rm._neutralResourcesCulture;
				}
				set
				{
					this._rm._neutralResourcesCulture = value;
				}
			}

			// Token: 0x06006C25 RID: 27685 RVA: 0x001776E3 File Offset: 0x001758E3
			internal string GetResourceFileName(CultureInfo culture)
			{
				return this._rm.GetResourceFileName(culture);
			}

			// Token: 0x17001242 RID: 4674
			// (get) Token: 0x06006C26 RID: 27686 RVA: 0x001776F1 File Offset: 0x001758F1
			// (set) Token: 0x06006C27 RID: 27687 RVA: 0x001776FE File Offset: 0x001758FE
			internal bool LookedForSatelliteContractVersion
			{
				get
				{
					return this._rm._lookedForSatelliteContractVersion;
				}
				set
				{
					this._rm._lookedForSatelliteContractVersion = value;
				}
			}

			// Token: 0x17001243 RID: 4675
			// (get) Token: 0x06006C28 RID: 27688 RVA: 0x0017770C File Offset: 0x0017590C
			// (set) Token: 0x06006C29 RID: 27689 RVA: 0x00177719 File Offset: 0x00175919
			internal Version SatelliteContractVersion
			{
				get
				{
					return this._rm._satelliteContractVersion;
				}
				set
				{
					this._rm._satelliteContractVersion = value;
				}
			}

			// Token: 0x06006C2A RID: 27690 RVA: 0x00177727 File Offset: 0x00175927
			internal Version ObtainSatelliteContractVersion(Assembly a)
			{
				return ResourceManager.GetSatelliteContractVersion(a);
			}

			// Token: 0x17001244 RID: 4676
			// (get) Token: 0x06006C2B RID: 27691 RVA: 0x0017772F File Offset: 0x0017592F
			// (set) Token: 0x06006C2C RID: 27692 RVA: 0x0017773C File Offset: 0x0017593C
			internal UltimateResourceFallbackLocation FallbackLoc
			{
				get
				{
					return this._rm.FallbackLocation;
				}
				set
				{
					this._rm._fallbackLoc = value;
				}
			}

			// Token: 0x17001245 RID: 4677
			// (get) Token: 0x06006C2D RID: 27693 RVA: 0x0017774A File Offset: 0x0017594A
			internal RuntimeAssembly CallingAssembly
			{
				get
				{
					return this._rm.m_callingAssembly;
				}
			}

			// Token: 0x17001246 RID: 4678
			// (get) Token: 0x06006C2E RID: 27694 RVA: 0x00177757 File Offset: 0x00175957
			internal RuntimeAssembly MainAssembly
			{
				get
				{
					return (RuntimeAssembly)this._rm.MainAssembly;
				}
			}

			// Token: 0x17001247 RID: 4679
			// (get) Token: 0x06006C2F RID: 27695 RVA: 0x00177769 File Offset: 0x00175969
			internal string BaseName
			{
				get
				{
					return this._rm.BaseName;
				}
			}

			// Token: 0x06006C30 RID: 27696 RVA: 0x00177776 File Offset: 0x00175976
			[SecurityCritical]
			internal bool TryLookingForSatellite(CultureInfo lookForCulture)
			{
				return this._rm.TryLookingForSatellite(lookForCulture);
			}

			// Token: 0x04003446 RID: 13382
			private ResourceManager _rm;
		}
	}
}
