using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Represents the collection of data bindings for a control.</summary>
	// Token: 0x0200016B RID: 363
	[DefaultEvent("CollectionChanged")]
	[Editor("System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter("System.Windows.Forms.Design.ControlBindingsConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ControlBindingsCollection : BindingsCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> class with the specified bindable control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</param>
		// Token: 0x06001305 RID: 4869 RVA: 0x0003CEA5 File Offset: 0x0003B0A5
		public ControlBindingsCollection(IBindableComponent control)
		{
			this.control = control;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the binding collection belongs to.</returns>
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0003CEB4 File Offset: 0x0003B0B4
		public IBindableComponent BindableComponent
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the control that the collection belongs to.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the collection belongs to.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0003CEBC File Offset: 0x0003B0BC
		public Control Control
		{
			get
			{
				return this.control as Control;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Binding" /> specified by the control's property name.</summary>
		/// <param name="propertyName">The name of the property on the data-bound control.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> that binds the specified control property to a data source.</returns>
		// Token: 0x1700045B RID: 1115
		public Binding this[string propertyName]
		{
			get
			{
				foreach (object obj in this)
				{
					Binding binding = (Binding)obj;
					if (string.Equals(binding.PropertyName, propertyName, StringComparison.OrdinalIgnoreCase))
					{
						return binding;
					}
				}
				return null;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.Binding" /> to the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="binding" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The control property is already data-bound.
		/// -or-
		/// The <see cref="T:System.Windows.Forms.Binding" /> does not specify a valid column of the <see cref="P:System.Windows.Forms.Binding.DataSource" />.</exception>
		// Token: 0x06001309 RID: 4873 RVA: 0x0003CF30 File Offset: 0x0003B130
		public new void Add(Binding binding)
		{
			base.Add(binding);
		}

		/// <summary>Creates a <see cref="T:System.Windows.Forms.Binding" /> using the specified control property name, data source, and data member, and adds it to the collection.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="binding" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Exception">The <paramref name="propertyName" /> is already data-bound.
		/// -or-
		/// The <paramref name="dataMember" /> doesn't specify a valid member of the <paramref name="dataSource" />.</exception>
		// Token: 0x0600130A RID: 4874 RVA: 0x0003CF3C File Offset: 0x0003B13C
		public Binding Add(string propertyName, object dataSource, string dataMember)
		{
			return this.Add(propertyName, dataSource, dataMember, false, this.DefaultDataSourceUpdateMode, null, string.Empty, null);
		}

		/// <summary>Creates a binding with the specified control property name, data source, data member, and information about whether formatting is enabled, and adds the binding to the collection.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" /></param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.  
		///  -or-  
		///  The property given is a read-only property.</exception>
		/// <exception cref="T:System.Exception">If formatting is disabled and the <paramref name="propertyName" /> is neither a valid property of a control nor an empty string ("").</exception>
		// Token: 0x0600130B RID: 4875 RVA: 0x0003CF60 File Offset: 0x0003B160
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, this.DefaultDataSourceUpdateMode, null, string.Empty, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting, propagating values to the data source based on the specified update setting, and adding the binding to the collection.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.  
		///  -or-  
		///  The specified data member does not exist on the data source.  
		///  -or-  
		///  The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x0600130C RID: 4876 RVA: 0x0003CF88 File Offset: 0x0003B188
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, null, string.Empty, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull" /> is returned from the data source, and adding the binding to the collection.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">When the data source has this value, the bound property is set to <see cref="T:System.DBNull" />.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" /></returns>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.  
		///  -or-  
		///  The specified data member does not exist on the data source.  
		///  -or-  
		///  The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x0600130D RID: 4877 RVA: 0x0003CFAC File Offset: 0x0003B1AC
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, string.Empty, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting with the specified format string, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull" /> is returned from the data source, and adding the binding to the collection.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">When the data source has this value, the bound property is set to <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" /></returns>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.  
		///  -or-  
		///  The specified data member does not exist on the data source.  
		///  -or-  
		///  The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x0600130E RID: 4878 RVA: 0x0003CFD0 File Offset: 0x0003B1D0
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString)
		{
			return this.Add(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, formatString, null);
		}

		/// <summary>Creates a binding that binds the specified control property to the specified data member of the specified data source, optionally enabling formatting with the specified format string, propagating values to the data source based on the specified update setting, setting the property to the specified value when <see cref="T:System.DBNull" /> is returned from the data source, setting the specified format provider, and adding the binding to the collection.</summary>
		/// <param name="propertyName">The name of the control property to bind.</param>
		/// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source.</param>
		/// <param name="dataMember">The property or list to bind to.</param>
		/// <param name="formattingEnabled">
		///   <see langword="true" /> to format the displayed data; otherwise, <see langword="false" />.</param>
		/// <param name="updateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
		/// <param name="nullValue">When the data source has this value, the bound property is set to <see cref="T:System.DBNull" />.</param>
		/// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed</param>
		/// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
		/// <returns>The newly created <see cref="T:System.Windows.Forms.Binding" />.</returns>
		/// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control or is read-only.  
		///  -or-  
		///  The specified data member does not exist on the data source.  
		///  -or-  
		///  The data source, data member, or control property specified are associated with another binding in the collection.</exception>
		// Token: 0x0600130F RID: 4879 RVA: 0x0003CFF0 File Offset: 0x0003B1F0
		public Binding Add(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode updateMode, object nullValue, string formatString, IFormatProvider formatInfo)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			Binding binding = new Binding(propertyName, dataSource, dataMember, formattingEnabled, updateMode, nullValue, formatString, formatInfo);
			this.Add(binding);
			return binding;
		}

		/// <summary>Adds a binding to the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to add.</param>
		// Token: 0x06001310 RID: 4880 RVA: 0x0003D028 File Offset: 0x0003B228
		protected override void AddCore(Binding dataBinding)
		{
			if (dataBinding == null)
			{
				throw new ArgumentNullException("dataBinding");
			}
			if (dataBinding.BindableComponent == this.control)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd1"));
			}
			if (dataBinding.BindableComponent != null)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionAdd2"));
			}
			dataBinding.SetBindableComponent(this.control);
			base.AddCore(dataBinding);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0003D08C File Offset: 0x0003B28C
		internal void CheckDuplicates(Binding binding)
		{
			if (binding.PropertyName.Length == 0)
			{
				return;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (binding != base[i] && base[i].PropertyName.Length > 0 && string.Compare(binding.PropertyName, base[i].PropertyName, false, CultureInfo.InvariantCulture) == 0)
				{
					throw new ArgumentException(SR.GetString("BindingsCollectionDup"), "binding");
				}
			}
		}

		/// <summary>Clears the collection of any bindings.</summary>
		// Token: 0x06001312 RID: 4882 RVA: 0x0003D10A File Offset: 0x0003B30A
		public new void Clear()
		{
			base.Clear();
		}

		/// <summary>Clears the bindings in the collection.</summary>
		// Token: 0x06001313 RID: 4883 RVA: 0x0003D114 File Offset: 0x0003B314
		protected override void ClearCore()
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				Binding binding = base[i];
				binding.SetBindableComponent(null);
			}
			base.ClearCore();
		}

		/// <summary>Gets or sets the default <see cref="P:System.Windows.Forms.Binding.DataSourceUpdateMode" /> for a <see cref="T:System.Windows.Forms.Binding" /> in the collection.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0003D149 File Offset: 0x0003B349
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x0003D151 File Offset: 0x0003B351
		public DataSourceUpdateMode DefaultDataSourceUpdateMode
		{
			get
			{
				return this.defaultDataSourceUpdateMode;
			}
			set
			{
				this.defaultDataSourceUpdateMode = value;
			}
		}

		/// <summary>Deletes the specified <see cref="T:System.Windows.Forms.Binding" /> from the collection.</summary>
		/// <param name="binding">The <see cref="T:System.Windows.Forms.Binding" /> to remove.</param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="binding" /> is <see langword="null" />.</exception>
		// Token: 0x06001316 RID: 4886 RVA: 0x0003D15A File Offset: 0x0003B35A
		public new void Remove(Binding binding)
		{
			base.Remove(binding);
		}

		/// <summary>Deletes the <see cref="T:System.Windows.Forms.Binding" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0, or it is greater than the number of bindings in the collection.</exception>
		// Token: 0x06001317 RID: 4887 RVA: 0x0003D163 File Offset: 0x0003B363
		public new void RemoveAt(int index)
		{
			base.RemoveAt(index);
		}

		/// <summary>Removes the specified binding from the collection.</summary>
		/// <param name="dataBinding">The <see cref="T:System.Windows.Forms.Binding" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The binding belongs to another <see cref="T:System.Windows.Forms.ControlBindingsCollection" />.</exception>
		// Token: 0x06001318 RID: 4888 RVA: 0x0003D16C File Offset: 0x0003B36C
		protected override void RemoveCore(Binding dataBinding)
		{
			if (dataBinding.BindableComponent != this.control)
			{
				throw new ArgumentException(SR.GetString("BindingsCollectionForeign"));
			}
			dataBinding.SetBindableComponent(null);
			base.RemoveCore(dataBinding);
		}

		// Token: 0x040008FE RID: 2302
		internal IBindableComponent control;

		// Token: 0x040008FF RID: 2303
		private DataSourceUpdateMode defaultDataSourceUpdateMode;
	}
}
