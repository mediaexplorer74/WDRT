using System;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides <see langword="static" /> methods for retrieving feature information from the current system.</summary>
	// Token: 0x0200024C RID: 588
	public abstract class FeatureSupport : IFeatureSupport
	{
		/// <summary>Determines whether any version of the specified feature is installed in the system. This method is <see langword="static" />.</summary>
		/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface.</param>
		/// <param name="featureConstName">The fully qualified name of the feature to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the specified feature is present; <see langword="false" /> if the specified feature is not present or if the product containing the feature is not installed.</returns>
		// Token: 0x0600254B RID: 9547 RVA: 0x000AE217 File Offset: 0x000AC417
		public static bool IsPresent(string featureClassName, string featureConstName)
		{
			return FeatureSupport.IsPresent(featureClassName, featureConstName, new Version(0, 0, 0, 0));
		}

		/// <summary>Determines whether the specified or newer version of the specified feature is installed in the system. This method is <see langword="static" />.</summary>
		/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface.</param>
		/// <param name="featureConstName">The fully qualified name of the feature to look for.</param>
		/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature.</param>
		/// <returns>
		///   <see langword="true" /> if the feature is present and its version number is greater than or equal to the specified minimum version number; <see langword="false" /> if the feature is not installed or its version number is below the specified minimum number.</returns>
		// Token: 0x0600254C RID: 9548 RVA: 0x000AE22C File Offset: 0x000AC42C
		public static bool IsPresent(string featureClassName, string featureConstName, Version minimumVersion)
		{
			object obj = null;
			Type type = null;
			try
			{
				type = Type.GetType(featureClassName);
			}
			catch (ArgumentException)
			{
			}
			if (type != null)
			{
				FieldInfo field = type.GetField(featureConstName);
				if (field != null)
				{
					obj = field.GetValue(null);
				}
			}
			if (obj != null && typeof(IFeatureSupport).IsAssignableFrom(type))
			{
				IFeatureSupport featureSupport = (IFeatureSupport)SecurityUtils.SecureCreateInstance(type);
				if (featureSupport != null)
				{
					return featureSupport.IsPresent(obj, minimumVersion);
				}
			}
			return false;
		}

		/// <summary>Gets the version of the specified feature that is available on the system.</summary>
		/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface.</param>
		/// <param name="featureConstName">The fully qualified name of the feature to look for.</param>
		/// <returns>A <see cref="T:System.Version" /> with the version number of the specified feature available on the system; or <see langword="null" /> if the feature is not installed.</returns>
		// Token: 0x0600254D RID: 9549 RVA: 0x000AE2AC File Offset: 0x000AC4AC
		public static Version GetVersionPresent(string featureClassName, string featureConstName)
		{
			object obj = null;
			Type type = null;
			try
			{
				type = Type.GetType(featureClassName);
			}
			catch (ArgumentException)
			{
			}
			if (type != null)
			{
				FieldInfo field = type.GetField(featureConstName);
				if (field != null)
				{
					obj = field.GetValue(null);
				}
			}
			if (obj != null)
			{
				IFeatureSupport featureSupport = (IFeatureSupport)SecurityUtils.SecureCreateInstance(type);
				if (featureSupport != null)
				{
					return featureSupport.GetVersionPresent(obj);
				}
			}
			return null;
		}

		/// <summary>Determines whether any version of the specified feature is installed in the system.</summary>
		/// <param name="feature">The feature to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the feature is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600254E RID: 9550 RVA: 0x000AE318 File Offset: 0x000AC518
		public virtual bool IsPresent(object feature)
		{
			return this.IsPresent(feature, new Version(0, 0, 0, 0));
		}

		/// <summary>Determines whether the specified or newer version of the specified feature is installed in the system.</summary>
		/// <param name="feature">The feature to look for.</param>
		/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the feature is present and its version number is greater than or equal to the specified minimum version number; <see langword="false" /> if the feature is not installed or its version number is below the specified minimum number.</returns>
		// Token: 0x0600254F RID: 9551 RVA: 0x000AE32C File Offset: 0x000AC52C
		public virtual bool IsPresent(object feature, Version minimumVersion)
		{
			Version versionPresent = this.GetVersionPresent(feature);
			return versionPresent != null && versionPresent.CompareTo(minimumVersion) >= 0;
		}

		/// <summary>When overridden in a derived class, gets the version of the specified feature that is available on the system.</summary>
		/// <param name="feature">The feature whose version is requested.</param>
		/// <returns>A <see cref="T:System.Version" /> representing the version number of the specified feature available on the system; or <see langword="null" /> if the feature is not installed.</returns>
		// Token: 0x06002550 RID: 9552
		public abstract Version GetVersionPresent(object feature);
	}
}
