using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x020004DB RID: 1243
	[AttributeUsage(AttributeTargets.All)]
	public class MonitoringDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.MonitoringDescriptionAttribute" /> class, using the specified description.</summary>
		/// <param name="description">The application-defined description text.</param>
		// Token: 0x06002EE3 RID: 12003 RVA: 0x000D28FF File Offset: 0x000D0AFF
		public MonitoringDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets description text associated with the item monitored.</summary>
		/// <returns>An application-defined description.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000D2908 File Offset: 0x000D0B08
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x0400278C RID: 10124
		private bool replaced;
	}
}
