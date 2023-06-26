using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x02000402 RID: 1026
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use.</param>
		// Token: 0x0600268F RID: 9871 RVA: 0x000B18A6 File Offset: 0x000AFAA6
		public IODescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x000B18AF File Offset: 0x000AFAAF
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

		// Token: 0x040020CA RID: 8394
		private bool replaced;
	}
}
