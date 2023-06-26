using System;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x02000567 RID: 1383
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x0600339E RID: 13214 RVA: 0x000E3820 File Offset: 0x000E1A20
		public InitializationEventAttribute(string eventName)
		{
			this.eventName = eventName;
		}

		/// <summary>Gets the name of the initialization event.</summary>
		/// <returns>The name of the initialization event.</returns>
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x000E382F File Offset: 0x000E1A2F
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x040029AA RID: 10666
		private string eventName;
	}
}
