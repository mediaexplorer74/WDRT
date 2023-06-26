using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal
{
	/// <summary>Provides access to data from an <see cref="T:System.ActivationContext" /> object.</summary>
	// Token: 0x0200066B RID: 1643
	[ComVisible(false)]
	public static class InternalActivationContextHelper
	{
		/// <summary>Gets the contents of the application manifest from an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="appInfo">The object containing the manifest.</param>
		/// <returns>The application manifest that is contained by the <see cref="T:System.ActivationContext" /> object.</returns>
		// Token: 0x06004F2D RID: 20269 RVA: 0x0011DACC File Offset: 0x0011BCCC
		[SecuritySafeCritical]
		public static object GetActivationContextData(ActivationContext appInfo)
		{
			return appInfo.ActivationContextData;
		}

		/// <summary>Gets the manifest of the last deployment component in an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="appInfo">The object containing the manifest.</param>
		/// <returns>The manifest of the last deployment component in the <see cref="T:System.ActivationContext" /> object.</returns>
		// Token: 0x06004F2E RID: 20270 RVA: 0x0011DAD4 File Offset: 0x0011BCD4
		[SecuritySafeCritical]
		public static object GetApplicationComponentManifest(ActivationContext appInfo)
		{
			return appInfo.ApplicationComponentManifest;
		}

		/// <summary>Gets the manifest of the first deployment component in an <see cref="T:System.ActivationContext" /> object.</summary>
		/// <param name="appInfo">The object containing the manifest.</param>
		/// <returns>The manifest of the first deployment component in the <see cref="T:System.ActivationContext" /> object.</returns>
		// Token: 0x06004F2F RID: 20271 RVA: 0x0011DADC File Offset: 0x0011BCDC
		[SecuritySafeCritical]
		public static object GetDeploymentComponentManifest(ActivationContext appInfo)
		{
			return appInfo.DeploymentComponentManifest;
		}

		/// <summary>Informs an <see cref="T:System.ActivationContext" /> to get ready to be run.</summary>
		/// <param name="appInfo">The object to inform.</param>
		// Token: 0x06004F30 RID: 20272 RVA: 0x0011DAE4 File Offset: 0x0011BCE4
		public static void PrepareForExecution(ActivationContext appInfo)
		{
			appInfo.PrepareForExecution();
		}

		/// <summary>Gets a value indicating whether this is the first time this <see cref="T:System.ActivationContext" /> object has been run.</summary>
		/// <param name="appInfo">The object to examine.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ActivationContext" /> indicates it is running for the first time; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004F31 RID: 20273 RVA: 0x0011DAEC File Offset: 0x0011BCEC
		public static bool IsFirstRun(ActivationContext appInfo)
		{
			return appInfo.LastApplicationStateResult == ActivationContext.ApplicationStateDisposition.RunningFirstTime;
		}

		/// <summary>Gets a byte array containing the raw content of the application manifest.</summary>
		/// <param name="appInfo">The object to get bytes from.</param>
		/// <returns>An array containing the application manifest as raw data.</returns>
		// Token: 0x06004F32 RID: 20274 RVA: 0x0011DAFB File Offset: 0x0011BCFB
		public static byte[] GetApplicationManifestBytes(ActivationContext appInfo)
		{
			if (appInfo == null)
			{
				throw new ArgumentNullException("appInfo");
			}
			return appInfo.GetApplicationManifestBytes();
		}

		/// <summary>Gets a byte array containing the raw content of the deployment manifest.</summary>
		/// <param name="appInfo">The object to get bytes from.</param>
		/// <returns>An array containing the deployment manifest as raw data.</returns>
		// Token: 0x06004F33 RID: 20275 RVA: 0x0011DB11 File Offset: 0x0011BD11
		public static byte[] GetDeploymentManifestBytes(ActivationContext appInfo)
		{
			if (appInfo == null)
			{
				throw new ArgumentNullException("appInfo");
			}
			return appInfo.GetDeploymentManifestBytes();
		}
	}
}
