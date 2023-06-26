using System;
using System.ComponentModel;

namespace System.Configuration
{
	/// <summary>Provides data for the <see cref="E:System.Configuration.ApplicationSettingsBase.SettingChanging" /> event.</summary>
	// Token: 0x0200007D RID: 125
	public class SettingChangingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingChangingEventArgs" /> class.</summary>
		/// <param name="settingName">A <see cref="T:System.String" /> containing the name of the application setting.</param>
		/// <param name="settingClass">A <see cref="T:System.String" /> containing a category description of the setting. Often this parameter is set to the application settings group name.</param>
		/// <param name="settingKey">A <see cref="T:System.String" /> containing the application settings key.</param>
		/// <param name="newValue">An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</param>
		/// <param name="cancel">
		///   <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
		// Token: 0x060004FB RID: 1275 RVA: 0x00020C50 File Offset: 0x0001EE50
		public SettingChangingEventArgs(string settingName, string settingClass, string settingKey, object newValue, bool cancel)
			: base(cancel)
		{
			this._settingName = settingName;
			this._settingClass = settingClass;
			this._settingKey = settingKey;
			this._newValue = newValue;
		}

		/// <summary>Gets the new value being assigned to the application settings property.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the new value to be assigned to the application settings property.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00020C77 File Offset: 0x0001EE77
		public object NewValue
		{
			get
			{
				return this._newValue;
			}
		}

		/// <summary>Gets the application settings property category.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a category description of the setting. Typically, this parameter is set to the application settings group name.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00020C7F File Offset: 0x0001EE7F
		public string SettingClass
		{
			get
			{
				return this._settingClass;
			}
		}

		/// <summary>Gets the name of the application setting associated with the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application setting.</returns>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00020C87 File Offset: 0x0001EE87
		public string SettingName
		{
			get
			{
				return this._settingName;
			}
		}

		/// <summary>Gets the application settings key associated with the property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the application settings key.</returns>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00020C8F File Offset: 0x0001EE8F
		public string SettingKey
		{
			get
			{
				return this._settingKey;
			}
		}

		// Token: 0x04000C09 RID: 3081
		private string _settingClass;

		// Token: 0x04000C0A RID: 3082
		private string _settingName;

		// Token: 0x04000C0B RID: 3083
		private string _settingKey;

		// Token: 0x04000C0C RID: 3084
		private object _newValue;
	}
}
