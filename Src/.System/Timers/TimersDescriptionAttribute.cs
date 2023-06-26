using System;
using System.ComponentModel;

namespace System.Timers
{
	/// <summary>Sets the description that visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x0200006E RID: 110
	[AttributeUsage(AttributeTargets.All)]
	public class TimersDescriptionAttribute : DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.TimersDescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use.</param>
		// Token: 0x0600048A RID: 1162 RVA: 0x0001F38C File Offset: 0x0001D58C
		public TimersDescriptionAttribute(string description)
			: base(description)
		{
		}

		/// <summary>Gets the description that visual designers can display when referencing an event, extender, or property.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001F395 File Offset: 0x0001D595
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

		// Token: 0x04000BD6 RID: 3030
		private bool replaced;
	}
}
