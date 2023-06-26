using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Defines the counter type, name, and Help string for a custom counter.</summary>
	// Token: 0x020004BF RID: 1215
	[TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class CounterCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of type <see langword="NumberOfItems32" />, and with empty name and help strings.</summary>
		// Token: 0x06002D5A RID: 11610 RVA: 0x000CC3E4 File Offset: 0x000CA5E4
		public CounterCreationData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of the specified type, using the specified counter name and Help strings.</summary>
		/// <param name="counterName">The name of the counter, which must be unique within its category.</param>
		/// <param name="counterHelp">The text that describes the counter's behavior.</param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that identifies the counter's behavior.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a value for <paramref name="counterType" /> that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="counterHelp" /> is <see langword="null" />.</exception>
		// Token: 0x06002D5B RID: 11611 RVA: 0x000CC40D File Offset: 0x000CA60D
		public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
		{
			this.CounterType = counterType;
			this.CounterName = counterName;
			this.CounterHelp = counterHelp;
		}

		/// <summary>Gets or sets the performance counter type of the custom counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that defines the behavior of the performance counter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a type that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration.</exception>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x000CC44B File Offset: 0x000CA64B
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x000CC453 File Offset: 0x000CA653
		[DefaultValue(PerformanceCounterType.NumberOfItems32)]
		[MonitoringDescription("CounterType")]
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PerformanceCounterType), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PerformanceCounterType));
				}
				this.counterType = value;
			}
		}

		/// <summary>Gets or sets the name of the custom counter.</summary>
		/// <returns>A name for the counter, which is unique in its category.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value is not between 1 and 80 characters long or contains double quotes, control characters or leading or trailing spaces.</exception>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x000CC489 File Offset: 0x000CA689
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x000CC491 File Offset: 0x000CA691
		[DefaultValue("")]
		[MonitoringDescription("CounterName")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				PerformanceCounterCategory.CheckValidCounter(value);
				this.counterName = value;
			}
		}

		/// <summary>Gets or sets the custom counter's description.</summary>
		/// <returns>The text that describes the counter's behavior.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is <see langword="null" />.</exception>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000CC4A0 File Offset: 0x000CA6A0
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x000CC4A8 File Offset: 0x000CA6A8
		[DefaultValue("")]
		[MonitoringDescription("CounterHelp")]
		public string CounterHelp
		{
			get
			{
				return this.counterHelp;
			}
			set
			{
				PerformanceCounterCategory.CheckValidHelp(value);
				this.counterHelp = value;
			}
		}

		// Token: 0x0400270E RID: 9998
		private PerformanceCounterType counterType = PerformanceCounterType.NumberOfItems32;

		// Token: 0x0400270F RID: 9999
		private string counterName = string.Empty;

		// Token: 0x04002710 RID: 10000
		private string counterHelp = string.Empty;
	}
}
