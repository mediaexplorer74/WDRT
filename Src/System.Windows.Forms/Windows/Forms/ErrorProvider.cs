using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	/// <summary>Provides a user interface for indicating that a control on a form has an error associated with it.</summary>
	// Token: 0x0200024B RID: 587
	[ProvideProperty("IconPadding", typeof(Control))]
	[ProvideProperty("IconAlignment", typeof(Control))]
	[ProvideProperty("Error", typeof(Control))]
	[ToolboxItemFilter("System.Windows.Forms")]
	[ComplexBindingProperties("DataSource", "DataMember")]
	[SRDescription("DescriptionErrorProvider")]
	public class ErrorProvider : Component, IExtenderProvider, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class and initializes the default settings for <see cref="P:System.Windows.Forms.ErrorProvider.BlinkRate" />, <see cref="P:System.Windows.Forms.ErrorProvider.BlinkStyle" />, and the <see cref="P:System.Windows.Forms.ErrorProvider.Icon" />.</summary>
		// Token: 0x06002514 RID: 9492 RVA: 0x000AD590 File Offset: 0x000AB790
		public ErrorProvider()
		{
			this.icon = ErrorProvider.DefaultIcon;
			this.blinkRate = 250;
			this.blinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
			this.currentChanged = new EventHandler(this.ErrorManager_CurrentChanged);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class attached to a container.</summary>
		/// <param name="parentControl">The container of the control to monitor for errors.</param>
		// Token: 0x06002515 RID: 9493 RVA: 0x000AD5FA File Offset: 0x000AB7FA
		public ErrorProvider(ContainerControl parentControl)
			: this()
		{
			this.parentControl = parentControl;
			this.propChangedEvent = new EventHandler(this.ParentControl_BindingContextChanged);
			parentControl.BindingContextChanged += this.propChangedEvent;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ErrorProvider" /> class attached to an <see cref="T:System.ComponentModel.IContainer" /> implementation.</summary>
		/// <param name="container">The <see cref="T:System.ComponentModel.IContainer" /> to monitor for errors.</param>
		// Token: 0x06002516 RID: 9494 RVA: 0x000AD627 File Offset: 0x000AB827
		public ErrorProvider(IContainer container)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> of the <see cref="T:System.ComponentModel.Component" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.Component" />, or null if the <see cref="T:System.ComponentModel.Component" /> is not encapsulated in an <see cref="T:System.ComponentModel.IContainer" />, the <see cref="T:System.ComponentModel.Component" /> does not have an <see cref="T:System.ComponentModel.ISite" /> associated with it, or the <see cref="T:System.ComponentModel.Component" /> is removed from its <see cref="T:System.ComponentModel.IContainer" />.</returns>
		// Token: 0x17000888 RID: 2184
		// (set) Token: 0x06002517 RID: 9495 RVA: 0x000AD644 File Offset: 0x000AB844
		public override ISite Site
		{
			set
			{
				base.Site = value;
				if (value == null)
				{
					return;
				}
				IDesignerHost designerHost = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent is ContainerControl)
					{
						this.ContainerControl = (ContainerControl)rootComponent;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating when the error icon flashes.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle" /> values. The default is <see cref="F:System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ErrorBlinkStyle" /> values.</exception>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000AD690 File Offset: 0x000AB890
		// (set) Token: 0x06002519 RID: 9497 RVA: 0x000AD6A4 File Offset: 0x000AB8A4
		[SRCategory("CatBehavior")]
		[DefaultValue(ErrorBlinkStyle.BlinkIfDifferentError)]
		[SRDescription("ErrorProviderBlinkStyleDescr")]
		public ErrorBlinkStyle BlinkStyle
		{
			get
			{
				if (this.blinkRate == 0)
				{
					return ErrorBlinkStyle.NeverBlink;
				}
				return this.blinkStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ErrorBlinkStyle));
				}
				if (this.blinkRate == 0)
				{
					value = ErrorBlinkStyle.NeverBlink;
				}
				if (this.blinkStyle == value)
				{
					return;
				}
				if (value == ErrorBlinkStyle.AlwaysBlink)
				{
					this.showIcon = true;
					this.blinkStyle = ErrorBlinkStyle.AlwaysBlink;
					using (IEnumerator enumerator = this.windows.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							ErrorProvider.ErrorWindow errorWindow = (ErrorProvider.ErrorWindow)obj;
							errorWindow.StartBlinking();
						}
						return;
					}
				}
				if (this.blinkStyle == ErrorBlinkStyle.AlwaysBlink)
				{
					this.blinkStyle = value;
					using (IEnumerator enumerator2 = this.windows.Values.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							ErrorProvider.ErrorWindow errorWindow2 = (ErrorProvider.ErrorWindow)obj2;
							errorWindow2.StopBlinking();
						}
						return;
					}
				}
				this.blinkStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating the parent control for this <see cref="T:System.Windows.Forms.ErrorProvider" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContainerControl" /> that contains the controls that the <see cref="T:System.Windows.Forms.ErrorProvider" /> is attached to.</returns>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000AD7B4 File Offset: 0x000AB9B4
		// (set) Token: 0x0600251B RID: 9499 RVA: 0x000AD7BC File Offset: 0x000AB9BC
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[SRDescription("ErrorProviderContainerControlDescr")]
		public ContainerControl ContainerControl
		{
			[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
			[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
			get
			{
				return this.parentControl;
			}
			set
			{
				if (this.parentControl != value)
				{
					if (this.parentControl != null)
					{
						this.parentControl.BindingContextChanged -= this.propChangedEvent;
					}
					this.parentControl = value;
					if (this.parentControl != null)
					{
						this.parentControl.BindingContextChanged += this.propChangedEvent;
					}
					this.Set_ErrorManager(this.DataSource, this.DataMember, true);
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the component is used in a locale that supports right-to-left fonts.</summary>
		/// <returns>
		///   <see langword="true" /> if the component is used in a right-to-left locale; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x000AD81E File Offset: 0x000ABA1E
		// (set) Token: 0x0600251D RID: 9501 RVA: 0x000AD826 File Offset: 0x000ABA26
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftDescr")]
		public virtual bool RightToLeft
		{
			get
			{
				return this.rightToLeft;
			}
			set
			{
				if (value != this.rightToLeft)
				{
					this.rightToLeft = value;
					this.OnRightToLeftChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ErrorProvider.RightToLeft" /> property changes value.</summary>
		// Token: 0x14000199 RID: 409
		// (add) Token: 0x0600251E RID: 9502 RVA: 0x000AD843 File Offset: 0x000ABA43
		// (remove) Token: 0x0600251F RID: 9503 RVA: 0x000AD85C File Offset: 0x000ABA5C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftChangedDescr")]
		public event EventHandler RightToLeftChanged
		{
			add
			{
				this.onRightToLeftChanged = (EventHandler)Delegate.Combine(this.onRightToLeftChanged, value);
			}
			remove
			{
				this.onRightToLeftChanged = (EventHandler)Delegate.Remove(this.onRightToLeftChanged, value);
			}
		}

		/// <summary>Gets or sets an object that contains data about the component.</summary>
		/// <returns>An object that contains data about the control. The default is <see langword="null" />.</returns>
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002520 RID: 9504 RVA: 0x000AD875 File Offset: 0x000ABA75
		// (set) Token: 0x06002521 RID: 9505 RVA: 0x000AD87D File Offset: 0x000ABA7D
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000AD888 File Offset: 0x000ABA88
		private void Set_ErrorManager(object newDataSource, string newDataMember, bool force)
		{
			if (this.inSetErrorManager)
			{
				return;
			}
			this.inSetErrorManager = true;
			try
			{
				bool flag = this.DataSource != newDataSource;
				bool flag2 = this.DataMember != newDataMember;
				if (flag || flag2 || force)
				{
					this.dataSource = newDataSource;
					this.dataMember = newDataMember;
					if (this.initializing)
					{
						this.setErrorManagerOnEndInit = true;
					}
					else
					{
						this.UnwireEvents(this.errorManager);
						if (this.parentControl != null && this.dataSource != null && this.parentControl.BindingContext != null)
						{
							this.errorManager = this.parentControl.BindingContext[this.dataSource, this.dataMember];
						}
						else
						{
							this.errorManager = null;
						}
						this.WireEvents(this.errorManager);
						if (this.errorManager != null)
						{
							this.UpdateBinding();
						}
					}
				}
			}
			finally
			{
				this.inSetErrorManager = false;
			}
		}

		/// <summary>Gets or sets the data source that the <see cref="T:System.Windows.Forms.ErrorProvider" /> monitors.</summary>
		/// <returns>A data source based on the <see cref="T:System.Collections.IList" /> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet" /> to be monitored for errors.</returns>
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002523 RID: 9507 RVA: 0x000AD974 File Offset: 0x000ABB74
		// (set) Token: 0x06002524 RID: 9508 RVA: 0x000AD97C File Offset: 0x000ABB7C
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("ErrorProviderDataSourceDescr")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.parentControl != null && value != null && !string.IsNullOrEmpty(this.dataMember))
				{
					try
					{
						this.errorManager = this.parentControl.BindingContext[value, this.dataMember];
					}
					catch (ArgumentException)
					{
						this.dataMember = "";
					}
				}
				this.Set_ErrorManager(value, this.DataMember, false);
			}
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000AD9EC File Offset: 0x000ABBEC
		private bool ShouldSerializeDataSource()
		{
			return this.dataSource != null;
		}

		/// <summary>Gets or sets the list within a data source to monitor.</summary>
		/// <returns>The string that represents a list within the data source specified by the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" /> to be monitored. Typically, this will be a <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x000AD9F7 File Offset: 0x000ABBF7
		// (set) Token: 0x06002527 RID: 9511 RVA: 0x000AD9FF File Offset: 0x000ABBFF
		[DefaultValue(null)]
		[SRCategory("CatData")]
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ErrorProviderDataMemberDescr")]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				this.Set_ErrorManager(this.DataSource, value, false);
			}
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000ADA19 File Offset: 0x000ABC19
		private bool ShouldSerializeDataMember()
		{
			return this.dataMember != null && this.dataMember.Length != 0;
		}

		/// <summary>Provides a method to set both the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" /> and <see cref="P:System.Windows.Forms.ErrorProvider.DataMember" /> at run time.</summary>
		/// <param name="newDataSource">A data set based on the <see cref="T:System.Collections.IList" /> interface to be monitored for errors. Typically, this is a <see cref="T:System.Data.DataSet" /> to be monitored for errors.</param>
		/// <param name="newDataMember">A collection within the <paramref name="newDataSource" /> to monitor for errors. Typically, this will be a <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x06002529 RID: 9513 RVA: 0x000ADA33 File Offset: 0x000ABC33
		public void BindToDataAndErrors(object newDataSource, string newDataMember)
		{
			this.Set_ErrorManager(newDataSource, newDataMember, false);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000ADA40 File Offset: 0x000ABC40
		private void WireEvents(BindingManagerBase listManager)
		{
			if (listManager != null)
			{
				listManager.CurrentChanged += this.currentChanged;
				listManager.BindingComplete += this.ErrorManager_BindingComplete;
				CurrencyManager currencyManager = listManager as CurrencyManager;
				if (currencyManager != null)
				{
					currencyManager.ItemChanged += this.ErrorManager_ItemChanged;
					currencyManager.Bindings.CollectionChanged += this.ErrorManager_BindingsChanged;
				}
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000ADAA4 File Offset: 0x000ABCA4
		private void UnwireEvents(BindingManagerBase listManager)
		{
			if (listManager != null)
			{
				listManager.CurrentChanged -= this.currentChanged;
				listManager.BindingComplete -= this.ErrorManager_BindingComplete;
				CurrencyManager currencyManager = listManager as CurrencyManager;
				if (currencyManager != null)
				{
					currencyManager.ItemChanged -= this.ErrorManager_ItemChanged;
					currencyManager.Bindings.CollectionChanged -= this.ErrorManager_BindingsChanged;
				}
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000ADB08 File Offset: 0x000ABD08
		private void ErrorManager_BindingComplete(object sender, BindingCompleteEventArgs e)
		{
			Binding binding = e.Binding;
			if (binding != null && binding.Control != null)
			{
				this.SetError(binding.Control, (e.ErrorText == null) ? string.Empty : e.ErrorText);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000ADB48 File Offset: 0x000ABD48
		private void ErrorManager_BindingsChanged(object sender, CollectionChangeEventArgs e)
		{
			this.ErrorManager_CurrentChanged(this.errorManager, e);
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x000ADB57 File Offset: 0x000ABD57
		private void ParentControl_BindingContextChanged(object sender, EventArgs e)
		{
			this.Set_ErrorManager(this.DataSource, this.DataMember, true);
		}

		/// <summary>Provides a method to update the bindings of the <see cref="P:System.Windows.Forms.ErrorProvider.DataSource" />, <see cref="P:System.Windows.Forms.ErrorProvider.DataMember" />, and the error text.</summary>
		// Token: 0x0600252F RID: 9519 RVA: 0x000ADB6C File Offset: 0x000ABD6C
		public void UpdateBinding()
		{
			this.ErrorManager_CurrentChanged(this.errorManager, EventArgs.Empty);
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x000ADB80 File Offset: 0x000ABD80
		private void ErrorManager_ItemChanged(object sender, ItemChangedEventArgs e)
		{
			BindingsCollection bindings = this.errorManager.Bindings;
			int count = bindings.Count;
			if (e.Index == -1 && this.errorManager.Count == 0)
			{
				for (int i = 0; i < count; i++)
				{
					if (bindings[i].Control != null)
					{
						this.SetError(bindings[i].Control, "");
					}
				}
				return;
			}
			this.ErrorManager_CurrentChanged(sender, e);
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000ADBF0 File Offset: 0x000ABDF0
		private void ErrorManager_CurrentChanged(object sender, EventArgs e)
		{
			if (this.errorManager.Count == 0)
			{
				return;
			}
			object obj = this.errorManager.Current;
			if (!(obj is IDataErrorInfo))
			{
				return;
			}
			BindingsCollection bindings = this.errorManager.Bindings;
			int count = bindings.Count;
			foreach (object obj2 in this.items.Values)
			{
				ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)obj2;
				controlItem.BlinkPhase = 0;
			}
			Hashtable hashtable = new Hashtable(count);
			for (int i = 0; i < count; i++)
			{
				if (bindings[i].Control != null)
				{
					BindToObject bindToObject = bindings[i].BindToObject;
					string text = ((IDataErrorInfo)obj)[bindToObject.BindingMemberInfo.BindingField];
					if (text == null)
					{
						text = "";
					}
					string text2 = "";
					if (hashtable.Contains(bindings[i].Control))
					{
						text2 = (string)hashtable[bindings[i].Control];
					}
					if (string.IsNullOrEmpty(text2))
					{
						text2 = text;
					}
					else
					{
						text2 = text2 + "\r\n" + text;
					}
					hashtable[bindings[i].Control] = text2;
				}
			}
			foreach (object obj3 in hashtable)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
				this.SetError((Control)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
		}

		/// <summary>Gets or sets the rate at which the error icon flashes.</summary>
		/// <returns>The rate, in milliseconds, at which the error icon should flash. The default is 250 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero.</exception>
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002532 RID: 9522 RVA: 0x000ADD9C File Offset: 0x000ABF9C
		// (set) Token: 0x06002533 RID: 9523 RVA: 0x000ADDA4 File Offset: 0x000ABFA4
		[SRCategory("CatBehavior")]
		[DefaultValue(250)]
		[SRDescription("ErrorProviderBlinkRateDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int BlinkRate
		{
			get
			{
				return this.blinkRate;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BlinkRate", value, SR.GetString("BlinkRateMustBeZeroOrMore"));
				}
				this.blinkRate = value;
				if (this.blinkRate == 0)
				{
					this.BlinkStyle = ErrorBlinkStyle.NeverBlink;
				}
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000ADDDC File Offset: 0x000ABFDC
		private static Icon DefaultIcon
		{
			get
			{
				if (ErrorProvider.defaultIcon == null)
				{
					Type typeFromHandle = typeof(ErrorProvider);
					lock (typeFromHandle)
					{
						if (ErrorProvider.defaultIcon == null)
						{
							ErrorProvider.defaultIcon = new Icon(typeof(ErrorProvider), "Error.ico");
						}
					}
				}
				return ErrorProvider.defaultIcon;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Icon" /> that is displayed next to a control when an error description string has been set for the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Icon" /> that signals an error has occurred. The default icon consists of an exclamation point in a circle with a red background.</returns>
		/// <exception cref="T:System.ArgumentNullException">The assigned value of the <see cref="T:System.Drawing.Icon" /> is <see langword="null" />.</exception>
		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000ADE48 File Offset: 0x000AC048
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x000ADE50 File Offset: 0x000AC050
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderIconDescr")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.icon = value;
				this.DisposeRegion();
				ErrorProvider.ErrorWindow[] array = new ErrorProvider.ErrorWindow[this.windows.Values.Count];
				this.windows.Values.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Update(false);
				}
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000ADEB7 File Offset: 0x000AC0B7
		internal ErrorProvider.IconRegion Region
		{
			get
			{
				if (this.region == null)
				{
					this.region = new ErrorProvider.IconRegion(this.Icon);
				}
				return this.region;
			}
		}

		/// <summary>Signals the object that initialization is starting.</summary>
		// Token: 0x06002538 RID: 9528 RVA: 0x000ADED8 File Offset: 0x000AC0D8
		void ISupportInitialize.BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x000ADEE1 File Offset: 0x000AC0E1
		private void EndInitCore()
		{
			this.initializing = false;
			if (this.setErrorManagerOnEndInit)
			{
				this.setErrorManagerOnEndInit = false;
				this.Set_ErrorManager(this.DataSource, this.DataMember, true);
			}
		}

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x0600253A RID: 9530 RVA: 0x000ADF0C File Offset: 0x000AC10C
		void ISupportInitialize.EndInit()
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null && !supportInitializeNotification.IsInitialized)
			{
				supportInitializeNotification.Initialized += this.DataSource_Initialized;
				return;
			}
			this.EndInitCore();
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000ADF4C File Offset: 0x000AC14C
		private void DataSource_Initialized(object sender, EventArgs e)
		{
			ISupportInitializeNotification supportInitializeNotification = this.DataSource as ISupportInitializeNotification;
			if (supportInitializeNotification != null)
			{
				supportInitializeNotification.Initialized -= this.DataSource_Initialized;
			}
			this.EndInitCore();
		}

		/// <summary>Clears all settings associated with this component.</summary>
		// Token: 0x0600253C RID: 9532 RVA: 0x000ADF80 File Offset: 0x000AC180
		public void Clear()
		{
			ErrorProvider.ErrorWindow[] array = new ErrorProvider.ErrorWindow[this.windows.Values.Count];
			this.windows.Values.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Dispose();
			}
			this.windows.Clear();
			foreach (object obj in this.items.Values)
			{
				ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)obj;
				if (controlItem != null)
				{
					controlItem.Dispose();
				}
			}
			this.items.Clear();
		}

		/// <summary>Gets a value indicating whether a control can be extended.</summary>
		/// <param name="extendee">The control to be extended.</param>
		/// <returns>
		///   <see langword="true" /> if the control can be extended; otherwise, <see langword="false" />.  
		/// This property will be <see langword="true" /> if the object is a <see cref="T:System.Windows.Forms.Control" /> and is not a <see cref="T:System.Windows.Forms.Form" /> or <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
		// Token: 0x0600253D RID: 9533 RVA: 0x000AE038 File Offset: 0x000AC238
		public bool CanExtend(object extendee)
		{
			return extendee is Control && !(extendee is Form) && !(extendee is ToolBar);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600253E RID: 9534 RVA: 0x000AE058 File Offset: 0x000AC258
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Clear();
				this.DisposeRegion();
				this.UnwireEvents(this.errorManager);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x000AE07C File Offset: 0x000AC27C
		private void DisposeRegion()
		{
			if (this.region != null)
			{
				this.region.Dispose();
				this.region = null;
			}
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x000AE098 File Offset: 0x000AC298
		private ErrorProvider.ControlItem EnsureControlItem(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)this.items[control];
			if (controlItem == null)
			{
				int num = this.itemIdCounter + 1;
				this.itemIdCounter = num;
				controlItem = new ErrorProvider.ControlItem(this, control, (IntPtr)num);
				this.items[control] = controlItem;
			}
			return controlItem;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000AE0F4 File Offset: 0x000AC2F4
		internal ErrorProvider.ErrorWindow EnsureErrorWindow(Control parent)
		{
			ErrorProvider.ErrorWindow errorWindow = (ErrorProvider.ErrorWindow)this.windows[parent];
			if (errorWindow == null)
			{
				errorWindow = new ErrorProvider.ErrorWindow(this, parent);
				this.windows[parent] = errorWindow;
			}
			return errorWindow;
		}

		/// <summary>Returns the current error description string for the specified control.</summary>
		/// <param name="control">The item to get the error description string for.</param>
		/// <returns>The error description string for the specified control.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06002542 RID: 9538 RVA: 0x000AE12C File Offset: 0x000AC32C
		[DefaultValue("")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderErrorDescr")]
		public string GetError(Control control)
		{
			return this.EnsureControlItem(control).Error;
		}

		/// <summary>Gets a value indicating where the error icon should be placed in relation to the control.</summary>
		/// <param name="control">The control to get the icon location for.</param>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ErrorIconAlignment" /> values. The default icon alignment is <see cref="F:System.Windows.Forms.ErrorIconAlignment.MiddleRight" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06002543 RID: 9539 RVA: 0x000AE13A File Offset: 0x000AC33A
		[DefaultValue(ErrorIconAlignment.MiddleRight)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderIconAlignmentDescr")]
		public ErrorIconAlignment GetIconAlignment(Control control)
		{
			return this.EnsureControlItem(control).IconAlignment;
		}

		/// <summary>Returns the amount of extra space to leave next to the error icon.</summary>
		/// <param name="control">The control to get the padding for.</param>
		/// <returns>The number of pixels to leave between the icon and the control.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06002544 RID: 9540 RVA: 0x000AE148 File Offset: 0x000AC348
		[DefaultValue(0)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ErrorProviderIconPaddingDescr")]
		public int GetIconPadding(Control control)
		{
			return this.EnsureControlItem(control).IconPadding;
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x000AE156 File Offset: 0x000AC356
		private void ResetIcon()
		{
			this.Icon = ErrorProvider.DefaultIcon;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ErrorProvider.RightToLeftChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002546 RID: 9542 RVA: 0x000AE164 File Offset: 0x000AC364
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			foreach (object obj in this.windows.Values)
			{
				ErrorProvider.ErrorWindow errorWindow = (ErrorProvider.ErrorWindow)obj;
				errorWindow.Update(false);
			}
			if (this.onRightToLeftChanged != null)
			{
				this.onRightToLeftChanged(this, e);
			}
		}

		/// <summary>Sets the error description string for the specified control.</summary>
		/// <param name="control">The control to set the error description string for.</param>
		/// <param name="value">The error description string, or <see langword="null" /> or <see cref="F:System.String.Empty" /> to remove the error.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06002547 RID: 9543 RVA: 0x000AE1D8 File Offset: 0x000AC3D8
		public void SetError(Control control, string value)
		{
			this.EnsureControlItem(control).Error = value;
		}

		/// <summary>Sets the location where the error icon should be placed in relation to the control.</summary>
		/// <param name="control">The control to set the icon location for.</param>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.ErrorIconAlignment" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06002548 RID: 9544 RVA: 0x000AE1E7 File Offset: 0x000AC3E7
		public void SetIconAlignment(Control control, ErrorIconAlignment value)
		{
			this.EnsureControlItem(control).IconAlignment = value;
		}

		/// <summary>Sets the amount of extra space to leave between the specified control and the error icon.</summary>
		/// <param name="control">The <paramref name="control" /> to set the padding for.</param>
		/// <param name="padding">The number of pixels to add between the icon and the <paramref name="control" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is <see langword="null" />.</exception>
		// Token: 0x06002549 RID: 9545 RVA: 0x000AE1F6 File Offset: 0x000AC3F6
		public void SetIconPadding(Control control, int padding)
		{
			this.EnsureControlItem(control).IconPadding = padding;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000AE205 File Offset: 0x000AC405
		private bool ShouldSerializeIcon()
		{
			return this.Icon != ErrorProvider.DefaultIcon;
		}

		// Token: 0x04000F6B RID: 3947
		private Hashtable items = new Hashtable();

		// Token: 0x04000F6C RID: 3948
		private Hashtable windows = new Hashtable();

		// Token: 0x04000F6D RID: 3949
		private Icon icon = ErrorProvider.DefaultIcon;

		// Token: 0x04000F6E RID: 3950
		private ErrorProvider.IconRegion region;

		// Token: 0x04000F6F RID: 3951
		private int itemIdCounter;

		// Token: 0x04000F70 RID: 3952
		private int blinkRate;

		// Token: 0x04000F71 RID: 3953
		private ErrorBlinkStyle blinkStyle;

		// Token: 0x04000F72 RID: 3954
		private bool showIcon = true;

		// Token: 0x04000F73 RID: 3955
		private bool inSetErrorManager;

		// Token: 0x04000F74 RID: 3956
		private bool setErrorManagerOnEndInit;

		// Token: 0x04000F75 RID: 3957
		private bool initializing;

		// Token: 0x04000F76 RID: 3958
		[ThreadStatic]
		private static Icon defaultIcon;

		// Token: 0x04000F77 RID: 3959
		private const int defaultBlinkRate = 250;

		// Token: 0x04000F78 RID: 3960
		private const ErrorBlinkStyle defaultBlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;

		// Token: 0x04000F79 RID: 3961
		private const ErrorIconAlignment defaultIconAlignment = ErrorIconAlignment.MiddleRight;

		// Token: 0x04000F7A RID: 3962
		private ContainerControl parentControl;

		// Token: 0x04000F7B RID: 3963
		private object dataSource;

		// Token: 0x04000F7C RID: 3964
		private string dataMember;

		// Token: 0x04000F7D RID: 3965
		private BindingManagerBase errorManager;

		// Token: 0x04000F7E RID: 3966
		private EventHandler currentChanged;

		// Token: 0x04000F7F RID: 3967
		private EventHandler propChangedEvent;

		// Token: 0x04000F80 RID: 3968
		private EventHandler onRightToLeftChanged;

		// Token: 0x04000F81 RID: 3969
		private bool rightToLeft;

		// Token: 0x04000F82 RID: 3970
		private object userData;

		// Token: 0x02000687 RID: 1671
		internal class ErrorWindow : NativeWindow
		{
			// Token: 0x0600671A RID: 26394 RVA: 0x00181584 File Offset: 0x0017F784
			public ErrorWindow(ErrorProvider provider, Control parent)
			{
				this.provider = provider;
				this.parent = parent;
			}

			// Token: 0x0600671B RID: 26395 RVA: 0x001815D8 File Offset: 0x0017F7D8
			public void Add(ErrorProvider.ControlItem item)
			{
				this.items.Add(item);
				if (!this.EnsureCreated())
				{
					return;
				}
				NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
				toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
				toolinfo_T.hwnd = base.Handle;
				toolinfo_T.uId = item.Id;
				toolinfo_T.lpszText = item.Error;
				toolinfo_T.uFlags = 16;
				UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_ADDTOOL, 0, toolinfo_T);
				this.Update(false);
			}

			// Token: 0x0600671C RID: 26396 RVA: 0x00181663 File Offset: 0x0017F863
			public void Dispose()
			{
				this.EnsureDestroyed();
			}

			// Token: 0x0600671D RID: 26397 RVA: 0x0018166C File Offset: 0x0017F86C
			private bool EnsureCreated()
			{
				if (base.Handle == IntPtr.Zero)
				{
					if (!this.parent.IsHandleCreated)
					{
						return false;
					}
					this.CreateHandle(new CreateParams
					{
						Caption = string.Empty,
						Style = 1342177280,
						ClassStyle = 8,
						X = 0,
						Y = 0,
						Width = 0,
						Height = 0,
						Parent = this.parent.Handle
					});
					NativeMethods.INITCOMMONCONTROLSEX initcommoncontrolsex = new NativeMethods.INITCOMMONCONTROLSEX();
					initcommoncontrolsex.dwICC = 8;
					initcommoncontrolsex.dwSize = Marshal.SizeOf(initcommoncontrolsex);
					SafeNativeMethods.InitCommonControlsEx(initcommoncontrolsex);
					CreateParams createParams = new CreateParams();
					createParams.Parent = base.Handle;
					createParams.ClassName = "tooltips_class32";
					createParams.Style = 1;
					this.tipWindow = new NativeWindow();
					this.tipWindow.CreateHandle(createParams);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
					SafeNativeMethods.SetWindowPos(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.HWND_TOP, 0, 0, 0, 0, 19);
					UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), 1027, 3, 0);
				}
				return true;
			}

			// Token: 0x0600671E RID: 26398 RVA: 0x001817CC File Offset: 0x0017F9CC
			private void EnsureDestroyed()
			{
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				if (this.tipWindow != null)
				{
					this.tipWindow.DestroyHandle();
					this.tipWindow = null;
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.HWND_TOP, this.windowBounds.X, this.windowBounds.Y, this.windowBounds.Width, this.windowBounds.Height, 131);
				if (this.parent != null)
				{
					this.parent.Invalidate(true);
				}
				this.DestroyHandle();
				if (this.mirrordc != null)
				{
					this.mirrordc.Dispose();
				}
			}

			// Token: 0x0600671F RID: 26399 RVA: 0x00181884 File Offset: 0x0017FA84
			private void CreateMirrorDC(IntPtr hdc, int originOffset)
			{
				this.mirrordc = DeviceContext.FromHdc(hdc);
				if (this.parent.IsMirrored && this.mirrordc != null)
				{
					this.mirrordc.SaveHdc();
					this.mirrordcExtent = this.mirrordc.ViewportExtent;
					this.mirrordcOrigin = this.mirrordc.ViewportOrigin;
					this.mirrordcMode = this.mirrordc.SetMapMode(DeviceContextMapMode.Anisotropic);
					this.mirrordc.ViewportExtent = new Size(-this.mirrordcExtent.Width, this.mirrordcExtent.Height);
					this.mirrordc.ViewportOrigin = new Point(this.mirrordcOrigin.X + originOffset, this.mirrordcOrigin.Y);
				}
			}

			// Token: 0x06006720 RID: 26400 RVA: 0x00181948 File Offset: 0x0017FB48
			private void RestoreMirrorDC()
			{
				if (this.parent.IsMirrored && this.mirrordc != null)
				{
					this.mirrordc.ViewportExtent = this.mirrordcExtent;
					this.mirrordc.ViewportOrigin = this.mirrordcOrigin;
					this.mirrordc.SetMapMode(this.mirrordcMode);
					this.mirrordc.RestoreHdc();
					this.mirrordc.Dispose();
				}
				this.mirrordc = null;
				this.mirrordcExtent = Size.Empty;
				this.mirrordcOrigin = Point.Empty;
				this.mirrordcMode = DeviceContextMapMode.Text;
			}

			// Token: 0x06006721 RID: 26401 RVA: 0x001819D8 File Offset: 0x0017FBD8
			private void OnPaint(ref Message m)
			{
				NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
				IntPtr intPtr = UnsafeNativeMethods.BeginPaint(new HandleRef(this, base.Handle), ref paintstruct);
				try
				{
					this.CreateMirrorDC(intPtr, this.windowBounds.Width - 1);
					try
					{
						for (int i = 0; i < this.items.Count; i++)
						{
							ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)this.items[i];
							Rectangle iconBounds = controlItem.GetIconBounds(this.provider.Region.Size);
							SafeNativeMethods.DrawIconEx(new HandleRef(this, this.mirrordc.Hdc), iconBounds.X - this.windowBounds.X, iconBounds.Y - this.windowBounds.Y, new HandleRef(this.provider.Region, this.provider.Region.IconHandle), iconBounds.Width, iconBounds.Height, 0, NativeMethods.NullHandleRef, 3);
						}
					}
					finally
					{
						this.RestoreMirrorDC();
					}
				}
				finally
				{
					UnsafeNativeMethods.EndPaint(new HandleRef(this, base.Handle), ref paintstruct);
				}
			}

			// Token: 0x06006722 RID: 26402 RVA: 0x0003B8FD File Offset: 0x00039AFD
			protected override void OnThreadException(Exception e)
			{
				Application.OnThreadException(e);
			}

			// Token: 0x06006723 RID: 26403 RVA: 0x00181B08 File Offset: 0x0017FD08
			private void OnTimer(object sender, EventArgs e)
			{
				int num = 0;
				for (int i = 0; i < this.items.Count; i++)
				{
					num += ((ErrorProvider.ControlItem)this.items[i]).BlinkPhase;
				}
				if (num == 0 && this.provider.BlinkStyle != ErrorBlinkStyle.AlwaysBlink)
				{
					this.timer.Stop();
				}
				this.Update(true);
			}

			// Token: 0x06006724 RID: 26404 RVA: 0x00181B6C File Offset: 0x0017FD6C
			private void OnToolTipVisibilityChanging(IntPtr id, bool toolTipShown)
			{
				for (int i = 0; i < this.items.Count; i++)
				{
					if (((ErrorProvider.ControlItem)this.items[i]).Id == id)
					{
						((ErrorProvider.ControlItem)this.items[i]).ToolTipShown = toolTipShown;
					}
				}
			}

			// Token: 0x06006725 RID: 26405 RVA: 0x00181BC4 File Offset: 0x0017FDC4
			public void Remove(ErrorProvider.ControlItem item)
			{
				this.items.Remove(item);
				if (this.tipWindow != null)
				{
					NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
					toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
					toolinfo_T.hwnd = base.Handle;
					toolinfo_T.uId = item.Id;
					UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_DELTOOL, 0, toolinfo_T);
				}
				if (this.items.Count == 0)
				{
					this.EnsureDestroyed();
					return;
				}
				this.Update(false);
			}

			// Token: 0x06006726 RID: 26406 RVA: 0x00181C50 File Offset: 0x0017FE50
			internal void StartBlinking()
			{
				if (this.timer == null)
				{
					this.timer = new Timer();
					this.timer.Tick += this.OnTimer;
				}
				this.timer.Interval = this.provider.BlinkRate;
				this.timer.Start();
				this.Update(false);
			}

			// Token: 0x06006727 RID: 26407 RVA: 0x00181CAF File Offset: 0x0017FEAF
			internal void StopBlinking()
			{
				if (this.timer != null)
				{
					this.timer.Stop();
				}
				this.Update(false);
			}

			// Token: 0x06006728 RID: 26408 RVA: 0x00181CCC File Offset: 0x0017FECC
			public void Update(bool timerCaused)
			{
				ErrorProvider.IconRegion region = this.provider.Region;
				Size size = region.Size;
				this.windowBounds = Rectangle.Empty;
				for (int i = 0; i < this.items.Count; i++)
				{
					ErrorProvider.ControlItem controlItem = (ErrorProvider.ControlItem)this.items[i];
					Rectangle iconBounds = controlItem.GetIconBounds(size);
					if (this.windowBounds.IsEmpty)
					{
						this.windowBounds = iconBounds;
					}
					else
					{
						this.windowBounds = Rectangle.Union(this.windowBounds, iconBounds);
					}
				}
				Region region2 = new Region(new Rectangle(0, 0, 0, 0));
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					for (int j = 0; j < this.items.Count; j++)
					{
						ErrorProvider.ControlItem controlItem2 = (ErrorProvider.ControlItem)this.items[j];
						Rectangle iconBounds2 = controlItem2.GetIconBounds(size);
						iconBounds2.X -= this.windowBounds.X;
						iconBounds2.Y -= this.windowBounds.Y;
						bool flag = true;
						if (!controlItem2.ToolTipShown)
						{
							switch (this.provider.BlinkStyle)
							{
							case ErrorBlinkStyle.BlinkIfDifferentError:
								flag = controlItem2.BlinkPhase == 0 || (controlItem2.BlinkPhase > 0 && (controlItem2.BlinkPhase & 1) == (j & 1));
								break;
							case ErrorBlinkStyle.AlwaysBlink:
								flag = (j & 1) == 0 == this.provider.showIcon;
								break;
							}
						}
						if (flag)
						{
							region.Region.Translate(iconBounds2.X, iconBounds2.Y);
							region2.Union(region.Region);
							region.Region.Translate(-iconBounds2.X, -iconBounds2.Y);
						}
						if (this.tipWindow != null)
						{
							NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
							toolinfo_T.cbSize = Marshal.SizeOf(toolinfo_T);
							toolinfo_T.hwnd = base.Handle;
							toolinfo_T.uId = controlItem2.Id;
							toolinfo_T.lpszText = controlItem2.Error;
							toolinfo_T.rect = NativeMethods.RECT.FromXYWH(iconBounds2.X, iconBounds2.Y, iconBounds2.Width, iconBounds2.Height);
							toolinfo_T.uFlags = 16;
							if (this.provider.RightToLeft)
							{
								toolinfo_T.uFlags |= 4;
							}
							UnsafeNativeMethods.SendMessage(new HandleRef(this.tipWindow, this.tipWindow.Handle), NativeMethods.TTM_SETTOOLINFO, 0, toolinfo_T);
						}
						if (timerCaused && controlItem2.BlinkPhase > 0)
						{
							ErrorProvider.ControlItem controlItem3 = controlItem2;
							int blinkPhase = controlItem3.BlinkPhase;
							controlItem3.BlinkPhase = blinkPhase - 1;
						}
					}
					if (timerCaused)
					{
						this.provider.showIcon = !this.provider.showIcon;
					}
					DeviceContext deviceContext = null;
					using (DeviceContext deviceContext = DeviceContext.FromHwnd(base.Handle))
					{
						this.CreateMirrorDC(deviceContext.Hdc, this.windowBounds.Width);
						Graphics graphics = Graphics.FromHdcInternal(this.mirrordc.Hdc);
						try
						{
							intPtr = region2.GetHrgn(graphics);
							System.Internal.HandleCollector.Add(intPtr, NativeMethods.CommonHandles.GDI);
						}
						finally
						{
							graphics.Dispose();
							this.RestoreMirrorDC();
						}
						if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, base.Handle), new HandleRef(region2, intPtr), true) != 0)
						{
							intPtr = IntPtr.Zero;
						}
					}
				}
				finally
				{
					region2.Dispose();
					if (intPtr != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
					}
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.HWND_TOP, this.windowBounds.X, this.windowBounds.Y, this.windowBounds.Width, this.windowBounds.Height, 16);
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), null, false);
			}

			// Token: 0x06006729 RID: 26409 RVA: 0x001820DC File Offset: 0x001802DC
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 15)
				{
					if (msg != 20)
					{
						if (msg == 78)
						{
							NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
							if (nmhdr.code == -521 || nmhdr.code == -522)
							{
								this.OnToolTipVisibilityChanging(nmhdr.idFrom, nmhdr.code == -521);
								return;
							}
						}
						else
						{
							base.WndProc(ref m);
						}
					}
					return;
				}
				this.OnPaint(ref m);
			}

			// Token: 0x04003A84 RID: 14980
			private ArrayList items = new ArrayList();

			// Token: 0x04003A85 RID: 14981
			private Control parent;

			// Token: 0x04003A86 RID: 14982
			private ErrorProvider provider;

			// Token: 0x04003A87 RID: 14983
			private Rectangle windowBounds = Rectangle.Empty;

			// Token: 0x04003A88 RID: 14984
			private Timer timer;

			// Token: 0x04003A89 RID: 14985
			private NativeWindow tipWindow;

			// Token: 0x04003A8A RID: 14986
			private DeviceContext mirrordc;

			// Token: 0x04003A8B RID: 14987
			private Size mirrordcExtent = Size.Empty;

			// Token: 0x04003A8C RID: 14988
			private Point mirrordcOrigin = Point.Empty;

			// Token: 0x04003A8D RID: 14989
			private DeviceContextMapMode mirrordcMode = DeviceContextMapMode.Text;
		}

		// Token: 0x02000688 RID: 1672
		internal class ControlItem
		{
			// Token: 0x0600672A RID: 26410 RVA: 0x00182158 File Offset: 0x00180358
			public ControlItem(ErrorProvider provider, Control control, IntPtr id)
			{
				this.toolTipShown = false;
				this.iconAlignment = ErrorIconAlignment.MiddleRight;
				this.error = string.Empty;
				this.id = id;
				this.control = control;
				this.provider = provider;
				this.control.HandleCreated += this.OnCreateHandle;
				this.control.HandleDestroyed += this.OnDestroyHandle;
				this.control.LocationChanged += this.OnBoundsChanged;
				this.control.SizeChanged += this.OnBoundsChanged;
				this.control.VisibleChanged += this.OnParentVisibleChanged;
				this.control.ParentChanged += this.OnParentVisibleChanged;
			}

			// Token: 0x0600672B RID: 26411 RVA: 0x00182224 File Offset: 0x00180424
			public void Dispose()
			{
				if (this.control != null)
				{
					this.control.HandleCreated -= this.OnCreateHandle;
					this.control.HandleDestroyed -= this.OnDestroyHandle;
					this.control.LocationChanged -= this.OnBoundsChanged;
					this.control.SizeChanged -= this.OnBoundsChanged;
					this.control.VisibleChanged -= this.OnParentVisibleChanged;
					this.control.ParentChanged -= this.OnParentVisibleChanged;
				}
				this.error = string.Empty;
			}

			// Token: 0x1700167E RID: 5758
			// (get) Token: 0x0600672C RID: 26412 RVA: 0x001822D1 File Offset: 0x001804D1
			public IntPtr Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x1700167F RID: 5759
			// (get) Token: 0x0600672D RID: 26413 RVA: 0x001822D9 File Offset: 0x001804D9
			// (set) Token: 0x0600672E RID: 26414 RVA: 0x001822E1 File Offset: 0x001804E1
			public int BlinkPhase
			{
				get
				{
					return this.blinkPhase;
				}
				set
				{
					this.blinkPhase = value;
				}
			}

			// Token: 0x17001680 RID: 5760
			// (get) Token: 0x0600672F RID: 26415 RVA: 0x001822EA File Offset: 0x001804EA
			// (set) Token: 0x06006730 RID: 26416 RVA: 0x001822F2 File Offset: 0x001804F2
			public int IconPadding
			{
				get
				{
					return this.iconPadding;
				}
				set
				{
					if (this.iconPadding != value)
					{
						this.iconPadding = value;
						this.UpdateWindow();
					}
				}
			}

			// Token: 0x17001681 RID: 5761
			// (get) Token: 0x06006731 RID: 26417 RVA: 0x0018230A File Offset: 0x0018050A
			// (set) Token: 0x06006732 RID: 26418 RVA: 0x00182314 File Offset: 0x00180514
			public string Error
			{
				get
				{
					return this.error;
				}
				set
				{
					if (value == null)
					{
						value = "";
					}
					if (this.error.Equals(value) && this.provider.BlinkStyle != ErrorBlinkStyle.AlwaysBlink)
					{
						return;
					}
					bool flag = this.error.Length == 0;
					this.error = value;
					if (value.Length == 0)
					{
						this.RemoveFromWindow();
						return;
					}
					if (flag)
					{
						this.AddToWindow();
						return;
					}
					if (this.provider.BlinkStyle != ErrorBlinkStyle.NeverBlink)
					{
						this.StartBlinking();
						return;
					}
					this.UpdateWindow();
				}
			}

			// Token: 0x17001682 RID: 5762
			// (get) Token: 0x06006733 RID: 26419 RVA: 0x00182392 File Offset: 0x00180592
			// (set) Token: 0x06006734 RID: 26420 RVA: 0x0018239A File Offset: 0x0018059A
			public ErrorIconAlignment IconAlignment
			{
				get
				{
					return this.iconAlignment;
				}
				set
				{
					if (this.iconAlignment != value)
					{
						if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
						{
							throw new InvalidEnumArgumentException("value", (int)value, typeof(ErrorIconAlignment));
						}
						this.iconAlignment = value;
						this.UpdateWindow();
					}
				}
			}

			// Token: 0x17001683 RID: 5763
			// (get) Token: 0x06006735 RID: 26421 RVA: 0x001823D8 File Offset: 0x001805D8
			// (set) Token: 0x06006736 RID: 26422 RVA: 0x001823E0 File Offset: 0x001805E0
			public bool ToolTipShown
			{
				get
				{
					return this.toolTipShown;
				}
				set
				{
					this.toolTipShown = value;
				}
			}

			// Token: 0x06006737 RID: 26423 RVA: 0x001823E9 File Offset: 0x001805E9
			internal ErrorIconAlignment RTLTranslateIconAlignment(ErrorIconAlignment align)
			{
				if (!this.provider.RightToLeft)
				{
					return align;
				}
				switch (align)
				{
				case ErrorIconAlignment.TopLeft:
					return ErrorIconAlignment.TopRight;
				case ErrorIconAlignment.TopRight:
					return ErrorIconAlignment.TopLeft;
				case ErrorIconAlignment.MiddleLeft:
					return ErrorIconAlignment.MiddleRight;
				case ErrorIconAlignment.MiddleRight:
					return ErrorIconAlignment.MiddleLeft;
				case ErrorIconAlignment.BottomLeft:
					return ErrorIconAlignment.BottomRight;
				case ErrorIconAlignment.BottomRight:
					return ErrorIconAlignment.BottomLeft;
				default:
					return align;
				}
			}

			// Token: 0x06006738 RID: 26424 RVA: 0x00182428 File Offset: 0x00180628
			internal Rectangle GetIconBounds(Size size)
			{
				int num = 0;
				int num2 = 0;
				switch (this.RTLTranslateIconAlignment(this.IconAlignment))
				{
				case ErrorIconAlignment.TopLeft:
				case ErrorIconAlignment.MiddleLeft:
				case ErrorIconAlignment.BottomLeft:
					num = this.control.Left - size.Width - this.iconPadding;
					break;
				case ErrorIconAlignment.TopRight:
				case ErrorIconAlignment.MiddleRight:
				case ErrorIconAlignment.BottomRight:
					num = this.control.Right + this.iconPadding;
					break;
				}
				switch (this.IconAlignment)
				{
				case ErrorIconAlignment.TopLeft:
				case ErrorIconAlignment.TopRight:
					num2 = this.control.Top;
					break;
				case ErrorIconAlignment.MiddleLeft:
				case ErrorIconAlignment.MiddleRight:
					num2 = this.control.Top + (this.control.Height - size.Height) / 2;
					break;
				case ErrorIconAlignment.BottomLeft:
				case ErrorIconAlignment.BottomRight:
					num2 = this.control.Bottom - size.Height;
					break;
				}
				return new Rectangle(num, num2, size.Width, size.Height);
			}

			// Token: 0x06006739 RID: 26425 RVA: 0x00182518 File Offset: 0x00180718
			private void UpdateWindow()
			{
				if (this.window != null)
				{
					this.window.Update(false);
				}
			}

			// Token: 0x0600673A RID: 26426 RVA: 0x0018252E File Offset: 0x0018072E
			private void StartBlinking()
			{
				if (this.window != null)
				{
					this.BlinkPhase = 10;
					this.window.StartBlinking();
				}
			}

			// Token: 0x0600673B RID: 26427 RVA: 0x0018254C File Offset: 0x0018074C
			private void AddToWindow()
			{
				if (this.window == null && (this.control.Created || this.control.RecreatingHandle) && this.control.Visible && this.control.ParentInternal != null && this.error.Length > 0)
				{
					this.window = this.provider.EnsureErrorWindow(this.control.ParentInternal);
					this.window.Add(this);
					if (this.provider.BlinkStyle != ErrorBlinkStyle.NeverBlink)
					{
						this.StartBlinking();
					}
				}
			}

			// Token: 0x0600673C RID: 26428 RVA: 0x001825DF File Offset: 0x001807DF
			private void RemoveFromWindow()
			{
				if (this.window != null)
				{
					this.window.Remove(this);
					this.window = null;
				}
			}

			// Token: 0x0600673D RID: 26429 RVA: 0x001825FC File Offset: 0x001807FC
			private void OnBoundsChanged(object sender, EventArgs e)
			{
				this.UpdateWindow();
			}

			// Token: 0x0600673E RID: 26430 RVA: 0x00182604 File Offset: 0x00180804
			private void OnParentVisibleChanged(object sender, EventArgs e)
			{
				this.BlinkPhase = 0;
				this.RemoveFromWindow();
				this.AddToWindow();
			}

			// Token: 0x0600673F RID: 26431 RVA: 0x00182619 File Offset: 0x00180819
			private void OnCreateHandle(object sender, EventArgs e)
			{
				this.AddToWindow();
			}

			// Token: 0x06006740 RID: 26432 RVA: 0x00182621 File Offset: 0x00180821
			private void OnDestroyHandle(object sender, EventArgs e)
			{
				this.RemoveFromWindow();
			}

			// Token: 0x04003A8E RID: 14990
			private string error;

			// Token: 0x04003A8F RID: 14991
			private Control control;

			// Token: 0x04003A90 RID: 14992
			private ErrorProvider.ErrorWindow window;

			// Token: 0x04003A91 RID: 14993
			private ErrorProvider provider;

			// Token: 0x04003A92 RID: 14994
			private int blinkPhase;

			// Token: 0x04003A93 RID: 14995
			private IntPtr id;

			// Token: 0x04003A94 RID: 14996
			private int iconPadding;

			// Token: 0x04003A95 RID: 14997
			private bool toolTipShown;

			// Token: 0x04003A96 RID: 14998
			private ErrorIconAlignment iconAlignment;

			// Token: 0x04003A97 RID: 14999
			private const int startingBlinkPhase = 10;
		}

		// Token: 0x02000689 RID: 1673
		internal class IconRegion
		{
			// Token: 0x06006741 RID: 26433 RVA: 0x00182629 File Offset: 0x00180829
			public IconRegion(Icon icon)
			{
				this.icon = new Icon(icon, 16, 16);
			}

			// Token: 0x17001684 RID: 5764
			// (get) Token: 0x06006742 RID: 26434 RVA: 0x00182641 File Offset: 0x00180841
			public IntPtr IconHandle
			{
				get
				{
					return this.icon.Handle;
				}
			}

			// Token: 0x17001685 RID: 5765
			// (get) Token: 0x06006743 RID: 26435 RVA: 0x00182650 File Offset: 0x00180850
			public Region Region
			{
				get
				{
					if (this.region == null)
					{
						this.region = new Region(new Rectangle(0, 0, 0, 0));
						IntPtr intPtr = IntPtr.Zero;
						try
						{
							Size size = this.icon.Size;
							Bitmap bitmap = this.icon.ToBitmap();
							bitmap.MakeTransparent();
							intPtr = ControlPaint.CreateHBitmapTransparencyMask(bitmap);
							bitmap.Dispose();
							int num = 16;
							int num2 = 2 * ((size.Width + 15) / num);
							byte[] array = new byte[num2 * size.Height];
							SafeNativeMethods.GetBitmapBits(new HandleRef(null, intPtr), array.Length, array);
							for (int i = 0; i < size.Height; i++)
							{
								for (int j = 0; j < size.Width; j++)
								{
									if (((int)array[i * num2 + j / 8] & (1 << 7 - j % 8)) == 0)
									{
										this.region.Union(new Rectangle(j, i, 1, 1));
									}
								}
							}
							this.region.Intersect(new Rectangle(0, 0, size.Width, size.Height));
						}
						finally
						{
							if (intPtr != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
							}
						}
					}
					return this.region;
				}
			}

			// Token: 0x17001686 RID: 5766
			// (get) Token: 0x06006744 RID: 26436 RVA: 0x00182794 File Offset: 0x00180994
			public Size Size
			{
				get
				{
					return this.icon.Size;
				}
			}

			// Token: 0x06006745 RID: 26437 RVA: 0x001827A1 File Offset: 0x001809A1
			public void Dispose()
			{
				if (this.region != null)
				{
					this.region.Dispose();
					this.region = null;
				}
				this.icon.Dispose();
			}

			// Token: 0x04003A98 RID: 15000
			private Region region;

			// Token: 0x04003A99 RID: 15001
			private Icon icon;
		}
	}
}
