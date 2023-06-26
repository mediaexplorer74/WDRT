using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	/// <summary>Provides support for design-time license context serialization.</summary>
	// Token: 0x020005DB RID: 1499
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesigntimeLicenseContextSerializer
	{
		// Token: 0x060037AC RID: 14252 RVA: 0x000F056A File Offset: 0x000EE76A
		private DesigntimeLicenseContextSerializer()
		{
		}

		/// <summary>Serializes the licenses within the specified design-time license context using the specified key and output stream.</summary>
		/// <param name="o">The stream to output to.</param>
		/// <param name="cryptoKey">The key to use for encryption.</param>
		/// <param name="context">A <see cref="T:System.ComponentModel.Design.DesigntimeLicenseContext" /> indicating the license context.</param>
		// Token: 0x060037AD RID: 14253 RVA: 0x000F0574 File Offset: 0x000EE774
		public static void Serialize(Stream o, string cryptoKey, DesigntimeLicenseContext context)
		{
			IFormatter formatter = new BinaryFormatter();
			formatter.Serialize(o, new object[] { cryptoKey, context.savedLicenseKeys });
		}

		// Token: 0x060037AE RID: 14254 RVA: 0x000F05A4 File Offset: 0x000EE7A4
		internal static void Deserialize(Stream o, string cryptoKey, RuntimeLicenseContext context)
		{
			IFormatter formatter = new BinaryFormatter();
			object obj = formatter.Deserialize(o);
			if (obj is object[])
			{
				object[] array = (object[])obj;
				if (array[0] is string && (string)array[0] == cryptoKey)
				{
					context.savedLicenseKeys = (Hashtable)array[1];
				}
			}
		}
	}
}
