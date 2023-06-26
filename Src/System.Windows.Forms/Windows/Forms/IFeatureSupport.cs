using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a standard interface for retrieving feature information from the current system.</summary>
	// Token: 0x0200028D RID: 653
	public interface IFeatureSupport
	{
		/// <summary>Determines whether any version of the specified feature is currently available on the system.</summary>
		/// <param name="feature">The feature to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the feature is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x060029A2 RID: 10658
		bool IsPresent(object feature);

		/// <summary>Determines whether the specified or newer version of the specified feature is currently available on the system.</summary>
		/// <param name="feature">The feature to look for.</param>
		/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature to look for.</param>
		/// <returns>
		///   <see langword="true" /> if the requested version of the feature is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x060029A3 RID: 10659
		bool IsPresent(object feature, Version minimumVersion);

		/// <summary>Retrieves the version of the specified feature.</summary>
		/// <param name="feature">The feature whose version is requested.</param>
		/// <returns>A <see cref="T:System.Version" /> representing the version number of the specified feature; or <see langword="null" /> if the feature is not installed.</returns>
		// Token: 0x060029A4 RID: 10660
		Version GetVersionPresent(object feature);
	}
}
