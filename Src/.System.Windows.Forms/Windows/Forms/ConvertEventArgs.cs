using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.Format" /> and <see cref="E:System.Windows.Forms.Binding.Parse" /> events.</summary>
	// Token: 0x02000171 RID: 369
	public class ConvertEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ConvertEventArgs" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the value of the current property.</param>
		/// <param name="desiredType">The <see cref="T:System.Type" /> of the value.</param>
		// Token: 0x0600137F RID: 4991 RVA: 0x0004134E File Offset: 0x0003F54E
		public ConvertEventArgs(object value, Type desiredType)
		{
			this.value = value;
			this.desiredType = desiredType;
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</summary>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</returns>
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00041364 File Offset: 0x0003F564
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x0004136C File Offset: 0x0003F56C
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Gets the data type of the desired value.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the desired value.</returns>
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00041375 File Offset: 0x0003F575
		public Type DesiredType
		{
			get
			{
				return this.desiredType;
			}
		}

		// Token: 0x0400092B RID: 2347
		private object value;

		// Token: 0x0400092C RID: 2348
		private Type desiredType;
	}
}
