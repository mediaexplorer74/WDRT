using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.InputLanguageChanging" /> event.</summary>
	// Token: 0x0200029B RID: 667
	public class InputLanguageChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> class with the specified locale, character set, and acceptance.</summary>
		/// <param name="culture">The locale of the requested input language.</param>
		/// <param name="sysCharSet">
		///   <see langword="true" /> if the system default font supports the character set required for the requested input language; otherwise, <see langword="false" />.</param>
		// Token: 0x06002A07 RID: 10759 RVA: 0x000BF2B4 File Offset: 0x000BD4B4
		public InputLanguageChangingEventArgs(CultureInfo culture, bool sysCharSet)
		{
			this.inputLanguage = InputLanguage.FromCulture(culture);
			this.culture = culture;
			this.sysCharSet = sysCharSet;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.InputLanguageChangingEventArgs" /> class with the specified input language, character set, and acceptance of a language change.</summary>
		/// <param name="inputLanguage">The requested input language.</param>
		/// <param name="sysCharSet">
		///   <see langword="true" /> if the system default font supports the character set required for the requested input language; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputLanguage" /> is <see langword="null" />.</exception>
		// Token: 0x06002A08 RID: 10760 RVA: 0x000BF2D6 File Offset: 0x000BD4D6
		public InputLanguageChangingEventArgs(InputLanguage inputLanguage, bool sysCharSet)
		{
			if (inputLanguage == null)
			{
				throw new ArgumentNullException("inputLanguage");
			}
			this.inputLanguage = inputLanguage;
			this.culture = inputLanguage.Culture;
			this.sysCharSet = sysCharSet;
		}

		/// <summary>Gets a value indicating the input language.</summary>
		/// <returns>The input language.</returns>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000BF306 File Offset: 0x000BD506
		public InputLanguage InputLanguage
		{
			get
			{
				return this.inputLanguage;
			}
		}

		/// <summary>Gets the locale of the requested input language.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that specifies the locale of the requested input language.</returns>
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000BF30E File Offset: 0x000BD50E
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets a value indicating whether the system default font supports the character set required for the requested input language.</summary>
		/// <returns>
		///   <see langword="true" /> if the system default font supports the character set required for the requested input language; otherwise, <see langword="false" />.</returns>
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002A0B RID: 10763 RVA: 0x000BF316 File Offset: 0x000BD516
		public bool SysCharSet
		{
			get
			{
				return this.sysCharSet;
			}
		}

		// Token: 0x04001111 RID: 4369
		private readonly InputLanguage inputLanguage;

		// Token: 0x04001112 RID: 4370
		private readonly CultureInfo culture;

		// Token: 0x04001113 RID: 4371
		private readonly bool sysCharSet;
	}
}
