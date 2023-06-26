using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Xml.Serialization;

namespace System.Diagnostics
{
	/// <summary>Provides an abstract base class to create new debugging and tracing switches.</summary>
	// Token: 0x020004A5 RID: 1189
	public abstract class Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class.</summary>
		/// <param name="displayName">The name of the switch.</param>
		/// <param name="description">The description for the switch.</param>
		// Token: 0x06002BFC RID: 11260 RVA: 0x000C6ABD File Offset: 0x000C4CBD
		protected Switch(string displayName, string description)
			: this(displayName, description, "0")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class, specifying the display name, description, and default value for the switch.</summary>
		/// <param name="displayName">The name of the switch.</param>
		/// <param name="description">The description of the switch.</param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06002BFD RID: 11261 RVA: 0x000C6ACC File Offset: 0x000C4CCC
		protected Switch(string displayName, string description, string defaultSwitchValue)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.displayName = displayName;
			this.description = description;
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				Switch.switches.Add(new WeakReference(this));
			}
			this.defaultValue = defaultSwitchValue;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000C6B4C File Offset: 0x000C4D4C
		private static void _pruneCachedSwitches()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				if (Switch.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(Switch.switches.Count);
					for (int i = 0; i < Switch.switches.Count; i++)
					{
						Switch @switch = (Switch)Switch.switches[i].Target;
						if (@switch != null)
						{
							list2.Add(Switch.switches[i]);
						}
					}
					if (list2.Count < Switch.switches.Count)
					{
						Switch.switches.Clear();
						Switch.switches.AddRange(list2);
						Switch.switches.TrimExcess();
					}
					Switch.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the case-insensitive custom attributes for the trace switch.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000C6C24 File Offset: 0x000C4E24
		[XmlIgnore]
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a name used to identify the switch.</summary>
		/// <returns>The name used to identify the switch. The default value is an empty string ("").</returns>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000C6C45 File Offset: 0x000C4E45
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets a description of the switch.</summary>
		/// <returns>The description of the switch. The default value is an empty string ("").</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000C6C4D File Offset: 0x000C4E4D
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the current setting for this switch.</summary>
		/// <returns>The current setting for this switch. The default is zero.</returns>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000C6C63 File Offset: 0x000C4E63
		// (set) Token: 0x06002C03 RID: 11267 RVA: 0x000C6C84 File Offset: 0x000C4E84
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized && this.InitializeWithStatus())
				{
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				bool flag = false;
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					this.initialized = true;
					if (this.switchSetting != value)
					{
						this.switchSetting = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnSwitchSettingChanged();
				}
			}
		}

		/// <summary>Gets or sets the value of the switch.</summary>
		/// <returns>A string representing the value of the switch.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is <see langword="null" />.  
		///  -or-  
		///  The value does not consist solely of an optional negative sign followed by a sequence of digits ranging from 0 to 9.  
		///  -or-  
		///  The value represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
		// (set) Token: 0x06002C05 RID: 11269 RVA: 0x000C6CF4 File Offset: 0x000C4EF4
		protected string Value
		{
			get
			{
				this.Initialize();
				return this.switchValueString;
			}
			set
			{
				this.Initialize();
				this.switchValueString = value;
				try
				{
					this.OnValueChanged();
				}
				catch (ArgumentException ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("BadConfigSwitchValue", new object[] { this.DisplayName }), ex);
				}
				catch (FormatException ex2)
				{
					throw new ConfigurationErrorsException(SR.GetString("BadConfigSwitchValue", new object[] { this.DisplayName }), ex2);
				}
				catch (OverflowException ex3)
				{
					throw new ConfigurationErrorsException(SR.GetString("BadConfigSwitchValue", new object[] { this.DisplayName }), ex3);
				}
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000C6DA4 File Offset: 0x000C4FA4
		private void Initialize()
		{
			this.InitializeWithStatus();
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000C6DB0 File Offset: 0x000C4FB0
		private bool InitializeWithStatus()
		{
			if (!this.initialized)
			{
				object critSec = TraceInternal.critSec;
				lock (critSec)
				{
					if (this.initialized || this.initializing)
					{
						return false;
					}
					this.initializing = true;
					if (this.switchSettings == null && !this.InitializeConfigSettings())
					{
						this.initialized = true;
						this.initializing = false;
						return false;
					}
					if (this.switchSettings != null)
					{
						SwitchElement switchElement = this.switchSettings[this.displayName];
						if (switchElement != null)
						{
							string value = switchElement.Value;
							if (value != null)
							{
								this.Value = value;
							}
							else
							{
								this.Value = this.defaultValue;
							}
							try
							{
								TraceUtils.VerifyAttributes(switchElement.Attributes, this.GetSupportedAttributes(), this);
							}
							catch (ConfigurationException)
							{
								this.initialized = false;
								this.initializing = false;
								throw;
							}
							this.attributes = new StringDictionary();
							this.attributes.ReplaceHashtable(switchElement.Attributes);
						}
						else
						{
							this.switchValueString = this.defaultValue;
							this.OnValueChanged();
						}
					}
					else
					{
						this.switchValueString = this.defaultValue;
						this.OnValueChanged();
					}
					this.initialized = true;
					this.initializing = false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000C6F28 File Offset: 0x000C5128
		private bool InitializeConfigSettings()
		{
			if (this.switchSettings != null)
			{
				return true;
			}
			if (!DiagnosticsConfiguration.CanInitialize())
			{
				return false;
			}
			this.switchSettings = DiagnosticsConfiguration.SwitchSettings;
			return true;
		}

		/// <summary>Gets the custom attributes supported by the switch.</summary>
		/// <returns>A string array that contains the names of the custom attributes supported by the switch, or <see langword="null" /> if there no custom attributes are supported.</returns>
		// Token: 0x06002C09 RID: 11273 RVA: 0x000C6F49 File Offset: 0x000C5149
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property is changed.</summary>
		// Token: 0x06002C0A RID: 11274 RVA: 0x000C6F4C File Offset: 0x000C514C
		protected virtual void OnSwitchSettingChanged()
		{
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.Value" /> property is changed.</summary>
		// Token: 0x06002C0B RID: 11275 RVA: 0x000C6F4E File Offset: 0x000C514E
		protected virtual void OnValueChanged()
		{
			this.SwitchSetting = int.Parse(this.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000C6F68 File Offset: 0x000C5168
		internal static void RefreshAll()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				for (int i = 0; i < Switch.switches.Count; i++)
				{
					Switch @switch = (Switch)Switch.switches[i].Target;
					if (@switch != null)
					{
						@switch.Refresh();
					}
				}
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000C6FDC File Offset: 0x000C51DC
		internal void Refresh()
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.initialized = false;
				this.switchSettings = null;
				this.Initialize();
			}
		}

		// Token: 0x04002699 RID: 9881
		private SwitchElementsCollection switchSettings;

		// Token: 0x0400269A RID: 9882
		private readonly string description;

		// Token: 0x0400269B RID: 9883
		private readonly string displayName;

		// Token: 0x0400269C RID: 9884
		private int switchSetting;

		// Token: 0x0400269D RID: 9885
		private volatile bool initialized;

		// Token: 0x0400269E RID: 9886
		private bool initializing;

		// Token: 0x0400269F RID: 9887
		private volatile string switchValueString = string.Empty;

		// Token: 0x040026A0 RID: 9888
		private StringDictionary attributes;

		// Token: 0x040026A1 RID: 9889
		private string defaultValue;

		// Token: 0x040026A2 RID: 9890
		private static List<WeakReference> switches = new List<WeakReference>();

		// Token: 0x040026A3 RID: 9891
		private static int s_LastCollectionCount;
	}
}
