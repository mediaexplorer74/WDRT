using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that a field can be missing from a serialization stream so that the <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> and the <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> does not throw an exception.</summary>
	// Token: 0x02000737 RID: 1847
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalFieldAttribute : Attribute
	{
		/// <summary>Gets or sets a version number to indicate when the optional field was added.</summary>
		/// <returns>The version of the <see cref="T:System.Runtime.Serialization.OptionalFieldAttribute" />.</returns>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x0012155E File Offset: 0x0011F75E
		// (set) Token: 0x060051E3 RID: 20963 RVA: 0x00121566 File Offset: 0x0011F766
		public int VersionAdded
		{
			get
			{
				return this.versionAdded;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_OptionalFieldVersionValue"));
				}
				this.versionAdded = value;
			}
		}

		// Token: 0x0400244B RID: 9291
		private int versionAdded = 1;
	}
}
