using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides a base class from which all objects to be used as evidence must derive.</summary>
	// Token: 0x0200034C RID: 844
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class EvidenceBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.EvidenceBase" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An object to be used as evidence is not serializable.</exception>
		// Token: 0x06002A54 RID: 10836 RVA: 0x0009E270 File Offset: 0x0009C470
		protected EvidenceBase()
		{
			if (!base.GetType().IsSerializable)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"));
			}
		}

		/// <summary>Creates a new object that is a complete copy of the current instance.</summary>
		/// <returns>A duplicate copy of this evidence object.</returns>
		// Token: 0x06002A55 RID: 10837 RVA: 0x0009E298 File Offset: 0x0009C498
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual EvidenceBase Clone()
		{
			EvidenceBase evidenceBase;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this);
				memoryStream.Position = 0L;
				evidenceBase = binaryFormatter.Deserialize(memoryStream) as EvidenceBase;
			}
			return evidenceBase;
		}
	}
}
