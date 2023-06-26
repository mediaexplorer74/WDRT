using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides properties and methods to add a license to a component and to manage a <see cref="T:System.ComponentModel.LicenseProvider" />. This class cannot be inherited.</summary>
	// Token: 0x0200057E RID: 1406
	[HostProtection(SecurityAction.LinkDemand, ExternalProcessMgmt = true)]
	public sealed class LicenseManager
	{
		// Token: 0x060033F3 RID: 13299 RVA: 0x000E3BFE File Offset: 0x000E1DFE
		private LicenseManager()
		{
		}

		/// <summary>Gets or sets the current <see cref="T:System.ComponentModel.LicenseContext" />, which specifies when you can use the licensed object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property is currently locked and cannot be changed.</exception>
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x000E3C08 File Offset: 0x000E1E08
		// (set) Token: 0x060033F5 RID: 13301 RVA: 0x000E3C68 File Offset: 0x000E1E68
		public static LicenseContext CurrentContext
		{
			get
			{
				if (LicenseManager.context == null)
				{
					object obj = LicenseManager.internalSyncObject;
					lock (obj)
					{
						if (LicenseManager.context == null)
						{
							LicenseManager.context = new RuntimeLicenseContext();
						}
					}
				}
				return LicenseManager.context;
			}
			set
			{
				object obj = LicenseManager.internalSyncObject;
				lock (obj)
				{
					if (LicenseManager.contextLockHolder != null)
					{
						throw new InvalidOperationException(SR.GetString("LicMgrContextCannotBeChanged"));
					}
					LicenseManager.context = value;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.LicenseUsageMode" /> which specifies when you can use the licensed object for the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.LicenseUsageMode" /> values, as specified in the <see cref="P:System.ComponentModel.LicenseManager.CurrentContext" /> property.</returns>
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000E3CC0 File Offset: 0x000E1EC0
		public static LicenseUsageMode UsageMode
		{
			get
			{
				if (LicenseManager.context != null)
				{
					return LicenseManager.context.UsageMode;
				}
				return LicenseUsageMode.Runtime;
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000E3CDC File Offset: 0x000E1EDC
		private static void CacheProvider(Type type, LicenseProvider provider)
		{
			if (LicenseManager.providers == null)
			{
				LicenseManager.providers = new Hashtable();
			}
			LicenseManager.providers[type] = provider;
			if (provider != null)
			{
				if (LicenseManager.providerInstances == null)
				{
					LicenseManager.providerInstances = new Hashtable();
				}
				LicenseManager.providerInstances[provider.GetType()] = provider;
			}
		}

		/// <summary>Creates an instance of the specified type, given a context in which you can use the licensed instance.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create.</param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance.</param>
		/// <returns>An instance of the specified type.</returns>
		// Token: 0x060033F8 RID: 13304 RVA: 0x000E3D37 File Offset: 0x000E1F37
		public static object CreateWithContext(Type type, LicenseContext creationContext)
		{
			return LicenseManager.CreateWithContext(type, creationContext, new object[0]);
		}

		/// <summary>Creates an instance of the specified type with the specified arguments, given a context in which you can use the licensed instance.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create.</param>
		/// <param name="creationContext">A <see cref="T:System.ComponentModel.LicenseContext" /> that specifies when you can use the licensed instance.</param>
		/// <param name="args">An array of type <see cref="T:System.Object" /> that represents the arguments for the type.</param>
		/// <returns>An instance of the specified type with the given array of arguments.</returns>
		// Token: 0x060033F9 RID: 13305 RVA: 0x000E3D48 File Offset: 0x000E1F48
		public static object CreateWithContext(Type type, LicenseContext creationContext, object[] args)
		{
			object obj = null;
			object obj2 = LicenseManager.internalSyncObject;
			lock (obj2)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = creationContext;
					LicenseManager.LockContext(LicenseManager.selfLock);
					try
					{
						obj = SecurityUtils.SecureCreateInstance(type, args);
					}
					catch (TargetInvocationException ex)
					{
						throw ex.InnerException;
					}
				}
				finally
				{
					LicenseManager.UnlockContext(LicenseManager.selfLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			return obj;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000E3DD8 File Offset: 0x000E1FD8
		private static bool GetCachedNoLicenseProvider(Type type)
		{
			return LicenseManager.providers != null && LicenseManager.providers.ContainsKey(type);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000E3DF2 File Offset: 0x000E1FF2
		private static LicenseProvider GetCachedProvider(Type type)
		{
			if (LicenseManager.providers != null)
			{
				return (LicenseProvider)LicenseManager.providers[type];
			}
			return null;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000E3E11 File Offset: 0x000E2011
		private static LicenseProvider GetCachedProviderInstance(Type providerType)
		{
			if (LicenseManager.providerInstances != null)
			{
				return (LicenseProvider)LicenseManager.providerInstances[providerType];
			}
			return null;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000E3E30 File Offset: 0x000E2030
		private static IntPtr GetLicenseInteropHelperType()
		{
			return typeof(LicenseManager.LicenseInteropHelper).TypeHandle.Value;
		}

		/// <summary>Returns whether the given type has a valid license.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> to find a valid license for.</param>
		/// <returns>
		///   <see langword="true" /> if the given type is licensed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060033FE RID: 13310 RVA: 0x000E3E54 File Offset: 0x000E2054
		public static bool IsLicensed(Type type)
		{
			License license;
			bool flag = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return flag;
		}

		/// <summary>Determines whether a valid license can be granted for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the <see cref="T:System.ComponentModel.License" />.</param>
		/// <returns>
		///   <see langword="true" /> if a valid license can be granted; otherwise, <see langword="false" />.</returns>
		// Token: 0x060033FF RID: 13311 RVA: 0x000E3E78 File Offset: 0x000E2078
		public static bool IsValid(Type type)
		{
			License license;
			bool flag = LicenseManager.ValidateInternal(type, null, false, out license);
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
			return flag;
		}

		/// <summary>Determines whether a valid license can be granted for the specified instance of the type. This method creates a valid <see cref="T:System.ComponentModel.License" />.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license.</param>
		/// <param name="instance">An object of the specified type or a type derived from the specified type.</param>
		/// <param name="license">A <see cref="T:System.ComponentModel.License" /> that is a valid license, or <see langword="null" /> if a valid license cannot be granted.</param>
		/// <returns>
		///   <see langword="true" /> if a valid <see cref="T:System.ComponentModel.License" /> can be granted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003400 RID: 13312 RVA: 0x000E3E9C File Offset: 0x000E209C
		public static bool IsValid(Type type, object instance, out License license)
		{
			return LicenseManager.ValidateInternal(type, instance, false, out license);
		}

		/// <summary>Prevents changes being made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to lock.</param>
		/// <exception cref="T:System.InvalidOperationException">The context is already locked.</exception>
		// Token: 0x06003401 RID: 13313 RVA: 0x000E3EA8 File Offset: 0x000E20A8
		public static void LockContext(object contextUser)
		{
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.contextLockHolder != null)
				{
					throw new InvalidOperationException(SR.GetString("LicMgrAlreadyLocked"));
				}
				LicenseManager.contextLockHolder = contextUser;
			}
		}

		/// <summary>Allows changes to be made to the current <see cref="T:System.ComponentModel.LicenseContext" /> of the given object.</summary>
		/// <param name="contextUser">The object whose current context you want to unlock.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contextUser" /> represents a different user than the one specified in a previous call to <see cref="M:System.ComponentModel.LicenseManager.LockContext(System.Object)" />.</exception>
		// Token: 0x06003402 RID: 13314 RVA: 0x000E3F00 File Offset: 0x000E2100
		public static void UnlockContext(object contextUser)
		{
			object obj = LicenseManager.internalSyncObject;
			lock (obj)
			{
				if (LicenseManager.contextLockHolder != contextUser)
				{
					throw new ArgumentException(SR.GetString("LicMgrDifferentUser"));
				}
				LicenseManager.contextLockHolder = null;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000E3F58 File Offset: 0x000E2158
		private static bool ValidateInternal(Type type, object instance, bool allowExceptions, out License license)
		{
			string text;
			return LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, type, instance, allowExceptions, out license, out text);
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000E3F78 File Offset: 0x000E2178
		private static bool ValidateInternalRecursive(LicenseContext context, Type type, object instance, bool allowExceptions, out License license, out string licenseKey)
		{
			LicenseProvider licenseProvider = LicenseManager.GetCachedProvider(type);
			if (licenseProvider == null && !LicenseManager.GetCachedNoLicenseProvider(type))
			{
				LicenseProviderAttribute licenseProviderAttribute = (LicenseProviderAttribute)Attribute.GetCustomAttribute(type, typeof(LicenseProviderAttribute), false);
				if (licenseProviderAttribute != null)
				{
					Type licenseProvider2 = licenseProviderAttribute.LicenseProvider;
					licenseProvider = LicenseManager.GetCachedProviderInstance(licenseProvider2);
					if (licenseProvider == null)
					{
						licenseProvider = (LicenseProvider)SecurityUtils.SecureCreateInstance(licenseProvider2);
					}
				}
				LicenseManager.CacheProvider(type, licenseProvider);
			}
			license = null;
			bool flag = true;
			licenseKey = null;
			if (licenseProvider != null)
			{
				license = licenseProvider.GetLicense(context, type, instance, allowExceptions);
				if (license == null)
				{
					flag = false;
				}
				else
				{
					licenseKey = license.LicenseKey;
				}
			}
			if (flag && instance == null)
			{
				Type baseType = type.BaseType;
				if (baseType != typeof(object) && baseType != null)
				{
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
					string text;
					flag = LicenseManager.ValidateInternalRecursive(context, baseType, null, allowExceptions, out license, out text);
					if (license != null)
					{
						license.Dispose();
						license = null;
					}
				}
			}
			return flag;
		}

		/// <summary>Determines whether a license can be granted for the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license.</param>
		/// <exception cref="T:System.ComponentModel.LicenseException">A <see cref="T:System.ComponentModel.License" /> cannot be granted.</exception>
		// Token: 0x06003405 RID: 13317 RVA: 0x000E4060 File Offset: 0x000E2260
		public static void Validate(Type type)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, null, true, out license))
			{
				throw new LicenseException(type);
			}
			if (license != null)
			{
				license.Dispose();
				license = null;
			}
		}

		/// <summary>Determines whether a license can be granted for the instance of the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of object that requests the license.</param>
		/// <param name="instance">An <see cref="T:System.Object" /> of the specified type or a type derived from the specified type.</param>
		/// <returns>A valid <see cref="T:System.ComponentModel.License" />.</returns>
		/// <exception cref="T:System.ComponentModel.LicenseException">The type is licensed, but a <see cref="T:System.ComponentModel.License" /> cannot be granted.</exception>
		// Token: 0x06003406 RID: 13318 RVA: 0x000E408C File Offset: 0x000E228C
		public static License Validate(Type type, object instance)
		{
			License license;
			if (!LicenseManager.ValidateInternal(type, instance, true, out license))
			{
				throw new LicenseException(type, instance);
			}
			return license;
		}

		// Token: 0x040029B0 RID: 10672
		private static readonly object selfLock = new object();

		// Token: 0x040029B1 RID: 10673
		private static volatile LicenseContext context = null;

		// Token: 0x040029B2 RID: 10674
		private static object contextLockHolder = null;

		// Token: 0x040029B3 RID: 10675
		private static volatile Hashtable providers;

		// Token: 0x040029B4 RID: 10676
		private static volatile Hashtable providerInstances;

		// Token: 0x040029B5 RID: 10677
		private static object internalSyncObject = new object();

		// Token: 0x02000892 RID: 2194
		private class LicenseInteropHelper
		{
			// Token: 0x06004570 RID: 17776 RVA: 0x00122368 File Offset: 0x00120568
			private static object AllocateAndValidateLicense(RuntimeTypeHandle rth, IntPtr bstrKey, int fDesignTime)
			{
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				LicenseManager.LicenseInteropHelper.CLRLicenseContext clrlicenseContext = new LicenseManager.LicenseInteropHelper.CLRLicenseContext((fDesignTime != 0) ? LicenseUsageMode.Designtime : LicenseUsageMode.Runtime, typeFromHandle);
				if (fDesignTime == 0 && bstrKey != (IntPtr)0)
				{
					clrlicenseContext.SetSavedLicenseKey(typeFromHandle, Marshal.PtrToStringBSTR(bstrKey));
				}
				object obj;
				try
				{
					obj = LicenseManager.CreateWithContext(typeFromHandle, clrlicenseContext);
				}
				catch (LicenseException ex)
				{
					throw new COMException(ex.Message, -2147221230);
				}
				return obj;
			}

			// Token: 0x06004571 RID: 17777 RVA: 0x001223D8 File Offset: 0x001205D8
			private static int RequestLicKey(RuntimeTypeHandle rth, ref IntPtr pbstrKey)
			{
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				License license;
				string text;
				if (!LicenseManager.ValidateInternalRecursive(LicenseManager.CurrentContext, typeFromHandle, null, false, out license, out text))
				{
					return -2147483640;
				}
				if (text == null)
				{
					return -2147483640;
				}
				pbstrKey = Marshal.StringToBSTR(text);
				if (license != null)
				{
					license.Dispose();
					license = null;
				}
				return 0;
			}

			// Token: 0x06004572 RID: 17778 RVA: 0x00122424 File Offset: 0x00120624
			private void GetLicInfo(RuntimeTypeHandle rth, ref int pRuntimeKeyAvail, ref int pLicVerified)
			{
				pRuntimeKeyAvail = 0;
				pLicVerified = 0;
				Type typeFromHandle = Type.GetTypeFromHandle(rth);
				if (this.helperContext == null)
				{
					this.helperContext = new DesigntimeLicenseContext();
				}
				else
				{
					this.helperContext.savedLicenseKeys.Clear();
				}
				License license;
				string text;
				if (LicenseManager.ValidateInternalRecursive(this.helperContext, typeFromHandle, null, false, out license, out text))
				{
					if (this.helperContext.savedLicenseKeys.Contains(typeFromHandle.AssemblyQualifiedName))
					{
						pRuntimeKeyAvail = 1;
					}
					if (license != null)
					{
						license.Dispose();
						license = null;
						pLicVerified = 1;
					}
				}
			}

			// Token: 0x06004573 RID: 17779 RVA: 0x001224A0 File Offset: 0x001206A0
			private void GetCurrentContextInfo(ref int fDesignTime, ref IntPtr bstrKey, RuntimeTypeHandle rth)
			{
				this.savedLicenseContext = LicenseManager.CurrentContext;
				this.savedType = Type.GetTypeFromHandle(rth);
				if (this.savedLicenseContext.UsageMode == LicenseUsageMode.Designtime)
				{
					fDesignTime = 1;
					bstrKey = (IntPtr)0;
					return;
				}
				fDesignTime = 0;
				string savedLicenseKey = this.savedLicenseContext.GetSavedLicenseKey(this.savedType, null);
				bstrKey = Marshal.StringToBSTR(savedLicenseKey);
			}

			// Token: 0x06004574 RID: 17780 RVA: 0x001224FC File Offset: 0x001206FC
			private void SaveKeyInCurrentContext(IntPtr bstrKey)
			{
				if (bstrKey != (IntPtr)0)
				{
					this.savedLicenseContext.SetSavedLicenseKey(this.savedType, Marshal.PtrToStringBSTR(bstrKey));
				}
			}

			// Token: 0x040037AC RID: 14252
			private const int S_OK = 0;

			// Token: 0x040037AD RID: 14253
			private const int E_NOTIMPL = -2147467263;

			// Token: 0x040037AE RID: 14254
			private const int CLASS_E_NOTLICENSED = -2147221230;

			// Token: 0x040037AF RID: 14255
			private const int E_FAIL = -2147483640;

			// Token: 0x040037B0 RID: 14256
			private DesigntimeLicenseContext helperContext;

			// Token: 0x040037B1 RID: 14257
			private LicenseContext savedLicenseContext;

			// Token: 0x040037B2 RID: 14258
			private Type savedType;

			// Token: 0x0200092F RID: 2351
			internal class CLRLicenseContext : LicenseContext
			{
				// Token: 0x06004681 RID: 18049 RVA: 0x00125FE4 File Offset: 0x001241E4
				public CLRLicenseContext(LicenseUsageMode usageMode, Type type)
				{
					this.usageMode = usageMode;
					this.type = type;
				}

				// Token: 0x17000FE6 RID: 4070
				// (get) Token: 0x06004682 RID: 18050 RVA: 0x00125FFA File Offset: 0x001241FA
				public override LicenseUsageMode UsageMode
				{
					get
					{
						return this.usageMode;
					}
				}

				// Token: 0x06004683 RID: 18051 RVA: 0x00126002 File Offset: 0x00124202
				public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
				{
					if (!(type == this.type))
					{
						return null;
					}
					return this.key;
				}

				// Token: 0x06004684 RID: 18052 RVA: 0x0012601A File Offset: 0x0012421A
				public override void SetSavedLicenseKey(Type type, string key)
				{
					if (type == this.type)
					{
						this.key = key;
					}
				}

				// Token: 0x04003DBE RID: 15806
				private LicenseUsageMode usageMode;

				// Token: 0x04003DBF RID: 15807
				private Type type;

				// Token: 0x04003DC0 RID: 15808
				private string key;
			}
		}
	}
}
