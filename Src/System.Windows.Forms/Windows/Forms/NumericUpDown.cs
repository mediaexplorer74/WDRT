using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows spin box (also known as an up-down control) that displays numeric values.</summary>
	// Token: 0x0200030C RID: 780
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("ValueChanged")]
	[DefaultBindingProperty("Value")]
	[SRDescription("DescriptionNumericUpDown")]
	public class NumericUpDown : UpDownBase, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDown" /> class.</summary>
		// Token: 0x060031B5 RID: 12725 RVA: 0x000E0400 File Offset: 0x000DE600
		public NumericUpDown()
		{
			base.SetState2(2048, true);
			this.Text = "0";
			this.StopAcceleration();
		}

		/// <summary>Gets a collection of sorted acceleration objects for the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> containing the sorted acceleration objects for the <see cref="T:System.Windows.Forms.NumericUpDown" /> control</returns>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000E045C File Offset: 0x000DE65C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public NumericUpDownAccelerationCollection Accelerations
		{
			get
			{
				if (this.accelerations == null)
				{
					this.accelerations = new NumericUpDownAccelerationCollection();
				}
				return this.accelerations;
			}
		}

		/// <summary>Gets or sets the number of decimal places to display in the spin box (also known as an up-down control).</summary>
		/// <returns>The number of decimal places to display in the spin box. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than 0.  
		///  -or-  
		///  The value assigned is greater than 99.</exception>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x000E0477 File Offset: 0x000DE677
		// (set) Token: 0x060031B8 RID: 12728 RVA: 0x000E0480 File Offset: 0x000DE680
		[SRCategory("CatData")]
		[DefaultValue(0)]
		[SRDescription("NumericUpDownDecimalPlacesDescr")]
		public int DecimalPlaces
		{
			get
			{
				return this.decimalPlaces;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("DecimalPlaces", SR.GetString("InvalidBoundArgument", new object[]
					{
						"DecimalPlaces",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture),
						"99"
					}));
				}
				this.decimalPlaces = value;
				this.UpdateEditText();
			}
		}

		/// <summary>Gets or sets a value indicating whether the spin box (also known as an up-down control) should display the value it contains in hexadecimal format.</summary>
		/// <returns>
		///   <see langword="true" /> if the spin box should display its value in hexadecimal format; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060031B9 RID: 12729 RVA: 0x000E04EE File Offset: 0x000DE6EE
		// (set) Token: 0x060031BA RID: 12730 RVA: 0x000E04F6 File Offset: 0x000DE6F6
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("NumericUpDownHexadecimalDescr")]
		public bool Hexadecimal
		{
			get
			{
				return this.hexadecimal;
			}
			set
			{
				this.hexadecimal = value;
				this.UpdateEditText();
			}
		}

		/// <summary>Gets or sets the value to increment or decrement the spin box (also known as an up-down control) when the up or down buttons are clicked.</summary>
		/// <returns>The value to increment or decrement the <see cref="P:System.Windows.Forms.NumericUpDown.Value" /> property when the up or down buttons are clicked on the spin box. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is not greater than or equal to zero.</exception>
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060031BB RID: 12731 RVA: 0x000E0505 File Offset: 0x000DE705
		// (set) Token: 0x060031BC RID: 12732 RVA: 0x000E0530 File Offset: 0x000DE730
		[SRCategory("CatData")]
		[SRDescription("NumericUpDownIncrementDescr")]
		public decimal Increment
		{
			get
			{
				if (this.accelerationsCurrentIndex != -1)
				{
					return this.Accelerations[this.accelerationsCurrentIndex].Increment;
				}
				return this.increment;
			}
			set
			{
				if (value < 0m)
				{
					throw new ArgumentOutOfRangeException("Increment", SR.GetString("InvalidArgument", new object[]
					{
						"Increment",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.increment = value;
			}
		}

		/// <summary>Gets or sets the maximum value for the spin box (also known as an up-down control).</summary>
		/// <returns>The maximum value for the spin box. The default value is 100.</returns>
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x000E0583 File Offset: 0x000DE783
		// (set) Token: 0x060031BE RID: 12734 RVA: 0x000E058B File Offset: 0x000DE78B
		[SRCategory("CatData")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("NumericUpDownMaximumDescr")]
		public decimal Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				this.maximum = value;
				if (this.minimum > this.maximum)
				{
					this.minimum = this.maximum;
				}
				this.Value = this.Constrain(this.currentValue);
			}
		}

		/// <summary>Gets or sets the minimum allowed value for the spin box (also known as an up-down control).</summary>
		/// <returns>The minimum allowed value for the spin box. The default value is 0.</returns>
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060031BF RID: 12735 RVA: 0x000E05C5 File Offset: 0x000DE7C5
		// (set) Token: 0x060031C0 RID: 12736 RVA: 0x000E05CD File Offset: 0x000DE7CD
		[SRCategory("CatData")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("NumericUpDownMinimumDescr")]
		public decimal Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				this.minimum = value;
				if (this.minimum > this.maximum)
				{
					this.maximum = value;
				}
				this.Value = this.Constrain(this.currentValue);
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060031C1 RID: 12737 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x060031C2 RID: 12738 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.NumericUpDown.Padding" /> property changes.</summary>
		// Token: 0x14000249 RID: 585
		// (add) Token: 0x060031C3 RID: 12739 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x060031C4 RID: 12740 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000E0602 File Offset: 0x000DE802
		private bool Spinning
		{
			get
			{
				return this.accelerations != null && this.buttonPressedStartTime != -1L;
			}
		}

		/// <summary>Gets or sets the text to be displayed in the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
		/// <returns>
		///   <see langword="Null" />.</returns>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000E061B File Offset: 0x000DE81B
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000E0623 File Offset: 0x000DE823
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.NumericUpDown.Text" /> property changes.</summary>
		// Token: 0x1400024A RID: 586
		// (add) Token: 0x060031C8 RID: 12744 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x060031C9 RID: 12745 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a thousands separator is displayed in the spin box (also known as an up-down control) when appropriate.</summary>
		/// <returns>
		///   <see langword="true" /> if a thousands separator is displayed in the spin box when appropriate; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000E062C File Offset: 0x000DE82C
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x000E0634 File Offset: 0x000DE834
		[SRCategory("CatData")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("NumericUpDownThousandsSeparatorDescr")]
		public bool ThousandsSeparator
		{
			get
			{
				return this.thousandsSeparator;
			}
			set
			{
				this.thousandsSeparator = value;
				this.UpdateEditText();
			}
		}

		/// <summary>Gets or sets the value assigned to the spin box (also known as an up-down control).</summary>
		/// <returns>The numeric value of the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="P:System.Windows.Forms.NumericUpDown.Minimum" /> property value.  
		///  -or-  
		///  The assigned value is greater than the <see cref="P:System.Windows.Forms.NumericUpDown.Maximum" /> property value.</exception>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000E0643 File Offset: 0x000DE843
		// (set) Token: 0x060031CD RID: 12749 RVA: 0x000E065C File Offset: 0x000DE85C
		[SRCategory("CatAppearance")]
		[Bindable(true)]
		[SRDescription("NumericUpDownValueDescr")]
		public decimal Value
		{
			get
			{
				if (base.UserEdit)
				{
					this.ValidateEditText();
				}
				return this.currentValue;
			}
			set
			{
				if (value != this.currentValue)
				{
					if (!this.initializing && (value < this.minimum || value > this.maximum))
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'Minimum'",
							"'Maximum'"
						}));
					}
					this.currentValue = value;
					this.OnValueChanged(EventArgs.Empty);
					this.currentValueChanged = true;
					this.UpdateEditText();
				}
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.NumericUpDown.Value" /> property has been changed in some way.</summary>
		// Token: 0x1400024B RID: 587
		// (add) Token: 0x060031CE RID: 12750 RVA: 0x000E06FF File Offset: 0x000DE8FF
		// (remove) Token: 0x060031CF RID: 12751 RVA: 0x000E0718 File Offset: 0x000DE918
		[SRCategory("CatAction")]
		[SRDescription("NumericUpDownOnValueChangedDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				this.onValueChanged = (EventHandler)Delegate.Combine(this.onValueChanged, value);
			}
			remove
			{
				this.onValueChanged = (EventHandler)Delegate.Remove(this.onValueChanged, value);
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x060031D0 RID: 12752 RVA: 0x000E0731 File Offset: 0x000DE931
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000E073A File Offset: 0x000DE93A
		private decimal Constrain(decimal value)
		{
			if (value < this.minimum)
			{
				value = this.minimum;
			}
			if (value > this.maximum)
			{
				value = this.maximum;
			}
			return value;
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x060031D2 RID: 12754 RVA: 0x000E0769 File Offset: 0x000DE969
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new NumericUpDown.NumericUpDownAccessibleObject(this);
		}

		/// <summary>Decrements the value of the spin box (also known as an up-down control).</summary>
		// Token: 0x060031D3 RID: 12755 RVA: 0x000E0774 File Offset: 0x000DE974
		public override void DownButton()
		{
			this.SetNextAcceleration();
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			decimal num = this.currentValue;
			try
			{
				num -= this.Increment;
				if (num < this.minimum)
				{
					num = this.minimum;
					if (this.Spinning)
					{
						this.StopAcceleration();
					}
				}
			}
			catch (OverflowException)
			{
				num = this.minimum;
			}
			this.Value = num;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.NumericUpDown" /> control that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x060031D4 RID: 12756 RVA: 0x000E07F0 File Offset: 0x000DE9F0
		public void EndInit()
		{
			this.initializing = false;
			this.Value = this.Constrain(this.currentValue);
			this.UpdateEditText();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060031D5 RID: 12757 RVA: 0x000E0811 File Offset: 0x000DEA11
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (base.InterceptArrowKeys && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && !this.Spinning)
			{
				this.StartAcceleration();
			}
			base.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060031D6 RID: 12758 RVA: 0x000E0844 File Offset: 0x000DEA44
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (base.InterceptArrowKeys && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
			{
				this.StopAcceleration();
			}
			base.OnKeyUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x060031D7 RID: 12759 RVA: 0x000E0870 File Offset: 0x000DEA70
		protected override void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			base.OnTextBoxKeyPress(source, e);
			NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
			string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
			string numberGroupSeparator = numberFormat.NumberGroupSeparator;
			string negativeSign = numberFormat.NegativeSign;
			string text = e.KeyChar.ToString();
			if (!char.IsDigit(e.KeyChar) && !text.Equals(numberDecimalSeparator) && !text.Equals(numberGroupSeparator) && !text.Equals(negativeSign) && e.KeyChar != '\b' && (!this.Hexadecimal || ((e.KeyChar < 'a' || e.KeyChar > 'f') && (e.KeyChar < 'A' || e.KeyChar > 'F'))) && (Control.ModifierKeys & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				e.Handled = true;
				SafeNativeMethods.MessageBeep(0);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.NumericUpDown.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060031D8 RID: 12760 RVA: 0x000E0935 File Offset: 0x000DEB35
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060031D9 RID: 12761 RVA: 0x000E094C File Offset: 0x000DEB4C
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (base.UserEdit)
			{
				this.UpdateEditText();
			}
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000E0963 File Offset: 0x000DEB63
		internal override void OnStartTimer()
		{
			this.StartAcceleration();
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000E096B File Offset: 0x000DEB6B
		internal override void OnStopTimer()
		{
			this.StopAcceleration();
		}

		/// <summary>Converts the text displayed in the spin box (also known as an up-down control) to a numeric value and evaluates it.</summary>
		// Token: 0x060031DC RID: 12764 RVA: 0x000E0974 File Offset: 0x000DEB74
		protected void ParseEditText()
		{
			try
			{
				if (!string.IsNullOrEmpty(this.Text) && (this.Text.Length != 1 || !(this.Text == "-")))
				{
					if (this.Hexadecimal)
					{
						this.Value = this.Constrain(Convert.ToDecimal(Convert.ToInt32(this.Text, 16)));
					}
					else
					{
						this.Value = this.Constrain(decimal.Parse(this.Text, CultureInfo.CurrentCulture));
					}
				}
			}
			catch
			{
			}
			finally
			{
				base.UserEdit = false;
			}
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000E0A1C File Offset: 0x000DEC1C
		private void SetNextAcceleration()
		{
			if (this.Spinning && this.accelerationsCurrentIndex < this.accelerations.Count - 1)
			{
				long ticks = DateTime.Now.Ticks;
				long num = ticks - this.buttonPressedStartTime;
				long num2 = 10000000L * (long)this.accelerations[this.accelerationsCurrentIndex + 1].Seconds;
				if (num > num2)
				{
					this.buttonPressedStartTime = ticks;
					this.accelerationsCurrentIndex++;
				}
			}
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000E0A97 File Offset: 0x000DEC97
		private void ResetIncrement()
		{
			this.Increment = NumericUpDown.DefaultIncrement;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000E0AA4 File Offset: 0x000DECA4
		private void ResetMaximum()
		{
			this.Maximum = NumericUpDown.DefaultMaximum;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000E0AB1 File Offset: 0x000DECB1
		private void ResetMinimum()
		{
			this.Minimum = NumericUpDown.DefaultMinimum;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000E0ABE File Offset: 0x000DECBE
		private void ResetValue()
		{
			this.Value = NumericUpDown.DefaultValue;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000E0ACC File Offset: 0x000DECCC
		private bool ShouldSerializeIncrement()
		{
			return !this.Increment.Equals(NumericUpDown.DefaultIncrement);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000E0AF0 File Offset: 0x000DECF0
		private bool ShouldSerializeMaximum()
		{
			return !this.Maximum.Equals(NumericUpDown.DefaultMaximum);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000E0B14 File Offset: 0x000DED14
		private bool ShouldSerializeMinimum()
		{
			return !this.Minimum.Equals(NumericUpDown.DefaultMinimum);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000E0B38 File Offset: 0x000DED38
		private bool ShouldSerializeValue()
		{
			return !this.Value.Equals(NumericUpDown.DefaultValue);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000E0B5C File Offset: 0x000DED5C
		private void StartAcceleration()
		{
			this.buttonPressedStartTime = DateTime.Now.Ticks;
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000E0B7C File Offset: 0x000DED7C
		private void StopAcceleration()
		{
			this.accelerationsCurrentIndex = -1;
			this.buttonPressedStartTime = -1L;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.NumericUpDown" />.</returns>
		// Token: 0x060031E8 RID: 12776 RVA: 0x000E0B90 File Offset: 0x000DED90
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				", Minimum = ",
				this.Minimum.ToString(CultureInfo.CurrentCulture),
				", Maximum = ",
				this.Maximum.ToString(CultureInfo.CurrentCulture)
			});
		}

		/// <summary>Increments the value of the spin box (also known as an up-down control).</summary>
		// Token: 0x060031E9 RID: 12777 RVA: 0x000E0BF4 File Offset: 0x000DEDF4
		public override void UpButton()
		{
			this.SetNextAcceleration();
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			decimal num = this.currentValue;
			try
			{
				num += this.Increment;
				if (num > this.maximum)
				{
					num = this.maximum;
					if (this.Spinning)
					{
						this.StopAcceleration();
					}
				}
			}
			catch (OverflowException)
			{
				num = this.maximum;
			}
			this.Value = num;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000E0C70 File Offset: 0x000DEE70
		private string GetNumberText(decimal num)
		{
			string text;
			if (this.Hexadecimal)
			{
				text = ((long)num).ToString("X", CultureInfo.InvariantCulture);
			}
			else
			{
				text = num.ToString((this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
			}
			return text;
		}

		/// <summary>Displays the current value of the spin box (also known as an up-down control) in the appropriate format.</summary>
		// Token: 0x060031EB RID: 12779 RVA: 0x000E0CDC File Offset: 0x000DEEDC
		protected override void UpdateEditText()
		{
			if (this.initializing)
			{
				return;
			}
			if (base.UserEdit)
			{
				this.ParseEditText();
			}
			if (this.currentValueChanged || (!string.IsNullOrEmpty(this.Text) && (this.Text.Length != 1 || !(this.Text == "-"))))
			{
				this.currentValueChanged = false;
				base.ChangingText = true;
				this.Text = this.GetNumberText(this.currentValue);
			}
		}

		/// <summary>Validates and updates the text displayed in the spin box (also known as an up-down control).</summary>
		// Token: 0x060031EC RID: 12780 RVA: 0x000E0D55 File Offset: 0x000DEF55
		protected override void ValidateEditText()
		{
			this.ParseEditText();
			this.UpdateEditText();
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000E0D64 File Offset: 0x000DEF64
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = base.PreferredHeight;
			int num = (this.Hexadecimal ? 16 : 10);
			int largestDigit = this.GetLargestDigit(0, num);
			int num2 = (int)Math.Floor(Math.Log(Math.Max(-(double)this.Minimum, (double)this.Maximum), (double)num));
			int num3;
			if (this.Hexadecimal)
			{
				num3 = (int)Math.Floor(Math.Log(9.2233720368547758E+18, (double)num));
			}
			else
			{
				num3 = (int)Math.Floor(Math.Log(7.9228162514264338E+28, (double)num));
			}
			bool flag = num2 >= num3;
			decimal num4;
			if (largestDigit != 0 || num2 == 1)
			{
				num4 = largestDigit;
			}
			else
			{
				num4 = this.GetLargestDigit(1, num);
			}
			if (flag)
			{
				num2 = num3 - 1;
			}
			for (int i = 0; i < num2; i++)
			{
				num4 = num4 * num + largestDigit;
			}
			int num5 = TextRenderer.MeasureText(this.GetNumberText(num4), this.Font).Width;
			if (flag)
			{
				string text;
				if (this.Hexadecimal)
				{
					text = ((long)num4).ToString("X", CultureInfo.InvariantCulture);
				}
				else
				{
					text = num4.ToString(CultureInfo.CurrentCulture);
				}
				int width = TextRenderer.MeasureText(text, this.Font).Width;
				num5 += width / (num2 + 1);
			}
			int num6 = base.SizeFromClientSize(num5, preferredHeight).Width + this.upDownButtons.Width;
			return new Size(num6, preferredHeight) + this.Padding.Size;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000E0F08 File Offset: 0x000DF108
		private int GetLargestDigit(int start, int end)
		{
			int num = -1;
			int num2 = -1;
			for (int i = start; i < end; i++)
			{
				char c;
				if (i < 10)
				{
					c = i.ToString(CultureInfo.InvariantCulture)[0];
				}
				else
				{
					c = (char)(65 + (i - 10));
				}
				Size size = TextRenderer.MeasureText(c.ToString(), this.Font);
				if (size.Width >= num2)
				{
					num2 = size.Width;
					num = i;
				}
			}
			return num;
		}

		// Token: 0x04001E3D RID: 7741
		private static readonly decimal DefaultValue = 0m;

		// Token: 0x04001E3E RID: 7742
		private static readonly decimal DefaultMinimum = 0m;

		// Token: 0x04001E3F RID: 7743
		private static readonly decimal DefaultMaximum = 100m;

		// Token: 0x04001E40 RID: 7744
		private const int DefaultDecimalPlaces = 0;

		// Token: 0x04001E41 RID: 7745
		private static readonly decimal DefaultIncrement = 1m;

		// Token: 0x04001E42 RID: 7746
		private const bool DefaultThousandsSeparator = false;

		// Token: 0x04001E43 RID: 7747
		private const bool DefaultHexadecimal = false;

		// Token: 0x04001E44 RID: 7748
		private const int InvalidValue = -1;

		// Token: 0x04001E45 RID: 7749
		private int decimalPlaces;

		// Token: 0x04001E46 RID: 7750
		private decimal increment = NumericUpDown.DefaultIncrement;

		// Token: 0x04001E47 RID: 7751
		private bool thousandsSeparator;

		// Token: 0x04001E48 RID: 7752
		private decimal minimum = NumericUpDown.DefaultMinimum;

		// Token: 0x04001E49 RID: 7753
		private decimal maximum = NumericUpDown.DefaultMaximum;

		// Token: 0x04001E4A RID: 7754
		private bool hexadecimal;

		// Token: 0x04001E4B RID: 7755
		private decimal currentValue = NumericUpDown.DefaultValue;

		// Token: 0x04001E4C RID: 7756
		private bool currentValueChanged;

		// Token: 0x04001E4D RID: 7757
		private EventHandler onValueChanged;

		// Token: 0x04001E4E RID: 7758
		private bool initializing;

		// Token: 0x04001E4F RID: 7759
		private NumericUpDownAccelerationCollection accelerations;

		// Token: 0x04001E50 RID: 7760
		private int accelerationsCurrentIndex;

		// Token: 0x04001E51 RID: 7761
		private long buttonPressedStartTime;

		// Token: 0x020007C8 RID: 1992
		[ComVisible(true)]
		internal class NumericUpDownAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006D4F RID: 27983 RVA: 0x0009B733 File Offset: 0x00099933
			public NumericUpDownAccessibleObject(NumericUpDown owner)
				: base(owner)
			{
			}

			// Token: 0x170017E4 RID: 6116
			// (get) Token: 0x06006D50 RID: 27984 RVA: 0x00191330 File Offset: 0x0018F530
			// (set) Token: 0x06006D51 RID: 27985 RVA: 0x00010E62 File Offset: 0x0000F062
			public override string Name
			{
				get
				{
					string name = base.Name;
					return ((NumericUpDown)base.Owner).GetAccessibleName(name);
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x170017E5 RID: 6117
			// (get) Token: 0x06006D52 RID: 27986 RVA: 0x00191358 File Offset: 0x0018F558
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (AccessibilityImprovements.Level1)
					{
						return AccessibleRole.SpinButton;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x06006D53 RID: 27987 RVA: 0x00191384 File Offset: 0x0018F584
			public override AccessibleObject GetChild(int index)
			{
				if (index >= 0 && index < this.GetChildCount())
				{
					if (index == 0)
					{
						return ((UpDownBase)base.Owner).TextBox.AccessibilityObject.Parent;
					}
					if (index == 1)
					{
						return ((UpDownBase)base.Owner).UpDownButtonsInternal.AccessibilityObject.Parent;
					}
				}
				return null;
			}

			// Token: 0x06006D54 RID: 27988 RVA: 0x00016041 File Offset: 0x00014241
			public override int GetChildCount()
			{
				return 2;
			}
		}
	}
}
