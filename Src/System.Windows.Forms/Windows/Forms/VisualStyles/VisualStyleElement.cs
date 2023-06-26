using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Identifies a control or user interface (UI) element that is drawn with visual styles.</summary>
	// Token: 0x02000452 RID: 1106
	public class VisualStyleElement
	{
		// Token: 0x06004D61 RID: 19809 RVA: 0x0013FAB5 File Offset: 0x0013DCB5
		private VisualStyleElement(string className, int part, int state)
		{
			this.className = className;
			this.part = part;
			this.state = state;
		}

		/// <summary>Creates a new visual style element from the specified class, part, and state values.</summary>
		/// <param name="className">A string that represents the class name of the visual style element to be created.</param>
		/// <param name="part">A value that represents the part of the visual style element to be created.</param>
		/// <param name="state">A value that represents the state of the visual style element to be created.</param>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> with the <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.ClassName" />, <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.Part" />, and <see cref="P:System.Windows.Forms.VisualStyles.VisualStyleElement.State" /> properties initialized to the <paramref name="className" />, <paramref name="part" />, and <paramref name="state" /> parameters.</returns>
		// Token: 0x06004D62 RID: 19810 RVA: 0x0013FAD2 File Offset: 0x0013DCD2
		public static VisualStyleElement CreateElement(string className, int part, int state)
		{
			return new VisualStyleElement(className, part, state);
		}

		/// <summary>Gets the class name of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A string that represents the class name of a visual style element.</returns>
		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06004D63 RID: 19811 RVA: 0x0013FADC File Offset: 0x0013DCDC
		public string ClassName
		{
			get
			{
				return this.className;
			}
		}

		/// <summary>Gets a value indicating the part of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A value that represents the part of a visual style element.</returns>
		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x0013FAE4 File Offset: 0x0013DCE4
		public int Part
		{
			get
			{
				return this.part;
			}
		}

		/// <summary>Gets a value indicating the state of the visual style element that this <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> represents.</summary>
		/// <returns>A value that represents the state of a visual style element.</returns>
		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06004D65 RID: 19813 RVA: 0x0013FAEC File Offset: 0x0013DCEC
		public int State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0400324B RID: 12875
		internal static readonly int Count = 25;

		// Token: 0x0400324C RID: 12876
		private string className;

		// Token: 0x0400324D RID: 12877
		private int part;

		// Token: 0x0400324E RID: 12878
		private int state;

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for button-related controls. This class cannot be inherited.</summary>
		// Token: 0x02000832 RID: 2098
		public static class Button
		{
			// Token: 0x0400435D RID: 17245
			private static readonly string className = "BUTTON";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the button control. This class cannot be inherited.</summary>
			// Token: 0x020008D0 RID: 2256
			public static class PushButton
			{
				/// <summary>Gets a visual style element that represents a normal button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal button.</returns>
				// Token: 0x1700193D RID: 6461
				// (get) Token: 0x060072E8 RID: 29416 RVA: 0x001A393F File Offset: 0x001A1B3F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.normal == null)
						{
							VisualStyleElement.Button.PushButton.normal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 1);
						}
						return VisualStyleElement.Button.PushButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot button.</returns>
				// Token: 0x1700193E RID: 6462
				// (get) Token: 0x060072E9 RID: 29417 RVA: 0x001A3962 File Offset: 0x001A1B62
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.hot == null)
						{
							VisualStyleElement.Button.PushButton.hot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 2);
						}
						return VisualStyleElement.Button.PushButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed button.</returns>
				// Token: 0x1700193F RID: 6463
				// (get) Token: 0x060072EA RID: 29418 RVA: 0x001A3985 File Offset: 0x001A1B85
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.pressed == null)
						{
							VisualStyleElement.Button.PushButton.pressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 3);
						}
						return VisualStyleElement.Button.PushButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled button.</returns>
				// Token: 0x17001940 RID: 6464
				// (get) Token: 0x060072EB RID: 29419 RVA: 0x001A39A8 File Offset: 0x001A1BA8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Button.PushButton.disabled == null)
						{
							VisualStyleElement.Button.PushButton.disabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 4);
						}
						return VisualStyleElement.Button.PushButton.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a default button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a default button.</returns>
				// Token: 0x17001941 RID: 6465
				// (get) Token: 0x060072EC RID: 29420 RVA: 0x001A39CB File Offset: 0x001A1BCB
				public static VisualStyleElement Default
				{
					get
					{
						if (VisualStyleElement.Button.PushButton._default == null)
						{
							VisualStyleElement.Button.PushButton._default = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.PushButton.part, 5);
						}
						return VisualStyleElement.Button.PushButton._default;
					}
				}

				// Token: 0x04004560 RID: 17760
				private static readonly int part = 1;

				// Token: 0x04004561 RID: 17761
				private static VisualStyleElement normal;

				// Token: 0x04004562 RID: 17762
				private static VisualStyleElement hot;

				// Token: 0x04004563 RID: 17763
				private static VisualStyleElement pressed;

				// Token: 0x04004564 RID: 17764
				private static VisualStyleElement disabled;

				// Token: 0x04004565 RID: 17765
				private static VisualStyleElement _default;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the radio button control. This class cannot be inherited.</summary>
			// Token: 0x020008D1 RID: 2257
			public static class RadioButton
			{
				/// <summary>Gets a visual style element that represents a normal radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal radio button in the unchecked state.</returns>
				// Token: 0x17001942 RID: 6466
				// (get) Token: 0x060072EE RID: 29422 RVA: 0x001A39F6 File Offset: 0x001A1BF6
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckednormal == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 1);
						}
						return VisualStyleElement.Button.RadioButton.uncheckednormal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot radio button in the unchecked state.</returns>
				// Token: 0x17001943 RID: 6467
				// (get) Token: 0x060072EF RID: 29423 RVA: 0x001A3A19 File Offset: 0x001A1C19
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckedhot == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 2);
						}
						return VisualStyleElement.Button.RadioButton.uncheckedhot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed radio button in the unchecked state.</returns>
				// Token: 0x17001944 RID: 6468
				// (get) Token: 0x060072F0 RID: 29424 RVA: 0x001A3A3C File Offset: 0x001A1C3C
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckedpressed == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 3);
						}
						return VisualStyleElement.Button.RadioButton.uncheckedpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled radio button in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled radio button in the unchecked state.</returns>
				// Token: 0x17001945 RID: 6469
				// (get) Token: 0x060072F1 RID: 29425 RVA: 0x001A3A5F File Offset: 0x001A1C5F
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.uncheckeddisabled == null)
						{
							VisualStyleElement.Button.RadioButton.uncheckeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 4);
						}
						return VisualStyleElement.Button.RadioButton.uncheckeddisabled;
					}
				}

				/// <summary>Gets a visual style element that represents a normal radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal radio button in the checked state.</returns>
				// Token: 0x17001946 RID: 6470
				// (get) Token: 0x060072F2 RID: 29426 RVA: 0x001A3A82 File Offset: 0x001A1C82
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkednormal == null)
						{
							VisualStyleElement.Button.RadioButton.checkednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 5);
						}
						return VisualStyleElement.Button.RadioButton.checkednormal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot radio button in the checked state.</returns>
				// Token: 0x17001947 RID: 6471
				// (get) Token: 0x060072F3 RID: 29427 RVA: 0x001A3AA5 File Offset: 0x001A1CA5
				public static VisualStyleElement CheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkedhot == null)
						{
							VisualStyleElement.Button.RadioButton.checkedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 6);
						}
						return VisualStyleElement.Button.RadioButton.checkedhot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed radio button in the checked state.</returns>
				// Token: 0x17001948 RID: 6472
				// (get) Token: 0x060072F4 RID: 29428 RVA: 0x001A3AC8 File Offset: 0x001A1CC8
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkedpressed == null)
						{
							VisualStyleElement.Button.RadioButton.checkedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 7);
						}
						return VisualStyleElement.Button.RadioButton.checkedpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled radio button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled radio button in the checked state.</returns>
				// Token: 0x17001949 RID: 6473
				// (get) Token: 0x060072F5 RID: 29429 RVA: 0x001A3AEB File Offset: 0x001A1CEB
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.RadioButton.checkeddisabled == null)
						{
							VisualStyleElement.Button.RadioButton.checkeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.RadioButton.part, 8);
						}
						return VisualStyleElement.Button.RadioButton.checkeddisabled;
					}
				}

				// Token: 0x04004566 RID: 17766
				private static readonly int part = 2;

				// Token: 0x04004567 RID: 17767
				internal static readonly int HighContrastDisabledPart = 8;

				// Token: 0x04004568 RID: 17768
				private static VisualStyleElement uncheckednormal;

				// Token: 0x04004569 RID: 17769
				private static VisualStyleElement uncheckedhot;

				// Token: 0x0400456A RID: 17770
				private static VisualStyleElement uncheckedpressed;

				// Token: 0x0400456B RID: 17771
				private static VisualStyleElement uncheckeddisabled;

				// Token: 0x0400456C RID: 17772
				private static VisualStyleElement checkednormal;

				// Token: 0x0400456D RID: 17773
				private static VisualStyleElement checkedhot;

				// Token: 0x0400456E RID: 17774
				private static VisualStyleElement checkedpressed;

				// Token: 0x0400456F RID: 17775
				private static VisualStyleElement checkeddisabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the check box control. This class cannot be inherited.</summary>
			// Token: 0x020008D2 RID: 2258
			public static class CheckBox
			{
				/// <summary>Gets a visual style element that represents a normal check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the unchecked state.</returns>
				// Token: 0x1700194A RID: 6474
				// (get) Token: 0x060072F7 RID: 29431 RVA: 0x001A3B1C File Offset: 0x001A1D1C
				public static VisualStyleElement UncheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckednormal == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 1);
						}
						return VisualStyleElement.Button.CheckBox.uncheckednormal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the unchecked state.</returns>
				// Token: 0x1700194B RID: 6475
				// (get) Token: 0x060072F8 RID: 29432 RVA: 0x001A3B3F File Offset: 0x001A1D3F
				public static VisualStyleElement UncheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckedhot == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 2);
						}
						return VisualStyleElement.Button.CheckBox.uncheckedhot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the unchecked state.</returns>
				// Token: 0x1700194C RID: 6476
				// (get) Token: 0x060072F9 RID: 29433 RVA: 0x001A3B62 File Offset: 0x001A1D62
				public static VisualStyleElement UncheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckedpressed == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 3);
						}
						return VisualStyleElement.Button.CheckBox.uncheckedpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the unchecked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the unchecked state.</returns>
				// Token: 0x1700194D RID: 6477
				// (get) Token: 0x060072FA RID: 29434 RVA: 0x001A3B85 File Offset: 0x001A1D85
				public static VisualStyleElement UncheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.uncheckeddisabled == null)
						{
							VisualStyleElement.Button.CheckBox.uncheckeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 4);
						}
						return VisualStyleElement.Button.CheckBox.uncheckeddisabled;
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the checked state.</returns>
				// Token: 0x1700194E RID: 6478
				// (get) Token: 0x060072FB RID: 29435 RVA: 0x001A3BA8 File Offset: 0x001A1DA8
				public static VisualStyleElement CheckedNormal
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkednormal == null)
						{
							VisualStyleElement.Button.CheckBox.checkednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 5);
						}
						return VisualStyleElement.Button.CheckBox.checkednormal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the checked state.</returns>
				// Token: 0x1700194F RID: 6479
				// (get) Token: 0x060072FC RID: 29436 RVA: 0x001A3BCB File Offset: 0x001A1DCB
				public static VisualStyleElement CheckedHot
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkedhot == null)
						{
							VisualStyleElement.Button.CheckBox.checkedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 6);
						}
						return VisualStyleElement.Button.CheckBox.checkedhot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the checked state.</returns>
				// Token: 0x17001950 RID: 6480
				// (get) Token: 0x060072FD RID: 29437 RVA: 0x001A3BEE File Offset: 0x001A1DEE
				public static VisualStyleElement CheckedPressed
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkedpressed == null)
						{
							VisualStyleElement.Button.CheckBox.checkedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 7);
						}
						return VisualStyleElement.Button.CheckBox.checkedpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the checked state.</returns>
				// Token: 0x17001951 RID: 6481
				// (get) Token: 0x060072FE RID: 29438 RVA: 0x001A3C11 File Offset: 0x001A1E11
				public static VisualStyleElement CheckedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.checkeddisabled == null)
						{
							VisualStyleElement.Button.CheckBox.checkeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 8);
						}
						return VisualStyleElement.Button.CheckBox.checkeddisabled;
					}
				}

				/// <summary>Gets a visual style element that represents a normal check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal check box in the indeterminate state.</returns>
				// Token: 0x17001952 RID: 6482
				// (get) Token: 0x060072FF RID: 29439 RVA: 0x001A3C34 File Offset: 0x001A1E34
				public static VisualStyleElement MixedNormal
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixednormal == null)
						{
							VisualStyleElement.Button.CheckBox.mixednormal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 9);
						}
						return VisualStyleElement.Button.CheckBox.mixednormal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot check box in the indeterminate state.</returns>
				// Token: 0x17001953 RID: 6483
				// (get) Token: 0x06007300 RID: 29440 RVA: 0x001A3C58 File Offset: 0x001A1E58
				public static VisualStyleElement MixedHot
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixedhot == null)
						{
							VisualStyleElement.Button.CheckBox.mixedhot = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 10);
						}
						return VisualStyleElement.Button.CheckBox.mixedhot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed check box in the indeterminate state.</returns>
				// Token: 0x17001954 RID: 6484
				// (get) Token: 0x06007301 RID: 29441 RVA: 0x001A3C7C File Offset: 0x001A1E7C
				public static VisualStyleElement MixedPressed
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixedpressed == null)
						{
							VisualStyleElement.Button.CheckBox.mixedpressed = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 11);
						}
						return VisualStyleElement.Button.CheckBox.mixedpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled check box in the indeterminate state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled check box in the indeterminate state.</returns>
				// Token: 0x17001955 RID: 6485
				// (get) Token: 0x06007302 RID: 29442 RVA: 0x001A3CA0 File Offset: 0x001A1EA0
				public static VisualStyleElement MixedDisabled
				{
					get
					{
						if (VisualStyleElement.Button.CheckBox.mixeddisabled == null)
						{
							VisualStyleElement.Button.CheckBox.mixeddisabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.CheckBox.part, 12);
						}
						return VisualStyleElement.Button.CheckBox.mixeddisabled;
					}
				}

				// Token: 0x04004570 RID: 17776
				private static readonly int part = 3;

				// Token: 0x04004571 RID: 17777
				internal static readonly int HighContrastDisabledPart = 9;

				// Token: 0x04004572 RID: 17778
				private static VisualStyleElement uncheckednormal;

				// Token: 0x04004573 RID: 17779
				private static VisualStyleElement uncheckedhot;

				// Token: 0x04004574 RID: 17780
				private static VisualStyleElement uncheckedpressed;

				// Token: 0x04004575 RID: 17781
				private static VisualStyleElement uncheckeddisabled;

				// Token: 0x04004576 RID: 17782
				private static VisualStyleElement checkednormal;

				// Token: 0x04004577 RID: 17783
				private static VisualStyleElement checkedhot;

				// Token: 0x04004578 RID: 17784
				private static VisualStyleElement checkedpressed;

				// Token: 0x04004579 RID: 17785
				private static VisualStyleElement checkeddisabled;

				// Token: 0x0400457A RID: 17786
				private static VisualStyleElement mixednormal;

				// Token: 0x0400457B RID: 17787
				private static VisualStyleElement mixedhot;

				// Token: 0x0400457C RID: 17788
				private static VisualStyleElement mixedpressed;

				// Token: 0x0400457D RID: 17789
				private static VisualStyleElement mixeddisabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the group box control. This class cannot be inherited.</summary>
			// Token: 0x020008D3 RID: 2259
			public static class GroupBox
			{
				/// <summary>Gets a visual style element that represents a normal group box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal group box.</returns>
				// Token: 0x17001956 RID: 6486
				// (get) Token: 0x06007304 RID: 29444 RVA: 0x001A3CD3 File Offset: 0x001A1ED3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Button.GroupBox.normal == null)
						{
							VisualStyleElement.Button.GroupBox.normal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.GroupBox.part, 1);
						}
						return VisualStyleElement.Button.GroupBox.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled group box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled group box.</returns>
				// Token: 0x17001957 RID: 6487
				// (get) Token: 0x06007305 RID: 29445 RVA: 0x001A3CF6 File Offset: 0x001A1EF6
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Button.GroupBox.disabled == null)
						{
							VisualStyleElement.Button.GroupBox.disabled = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.GroupBox.part, 2);
						}
						return VisualStyleElement.Button.GroupBox.disabled;
					}
				}

				// Token: 0x0400457E RID: 17790
				private static readonly int part = 4;

				// Token: 0x0400457F RID: 17791
				internal static readonly int HighContrastDisabledPart = 10;

				// Token: 0x04004580 RID: 17792
				private static VisualStyleElement normal;

				// Token: 0x04004581 RID: 17793
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a user button. This class cannot be inherited.</summary>
			// Token: 0x020008D4 RID: 2260
			public static class UserButton
			{
				/// <summary>Gets a visual style element that represents a user button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a user button.</returns>
				// Token: 0x17001958 RID: 6488
				// (get) Token: 0x06007307 RID: 29447 RVA: 0x001A3D28 File Offset: 0x001A1F28
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Button.UserButton.normal == null)
						{
							VisualStyleElement.Button.UserButton.normal = new VisualStyleElement(VisualStyleElement.Button.className, VisualStyleElement.Button.UserButton.part, 0);
						}
						return VisualStyleElement.Button.UserButton.normal;
					}
				}

				// Token: 0x04004582 RID: 17794
				private static readonly int part = 5;

				// Token: 0x04004583 RID: 17795
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains a class that provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the drop-down arrow of the combo box control. This class cannot be inherited.</summary>
		// Token: 0x02000833 RID: 2099
		public static class ComboBox
		{
			// Token: 0x0400435E RID: 17246
			private static readonly string className = "COMBOBOX";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the different states of the drop-down arrow of the combo box control. This class cannot be inherited.</summary>
			// Token: 0x020008D5 RID: 2261
			public static class DropDownButton
			{
				/// <summary>Gets a visual style element that represents a drop-down arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the normal state.</returns>
				// Token: 0x17001959 RID: 6489
				// (get) Token: 0x06007309 RID: 29449 RVA: 0x001A3D53 File Offset: 0x001A1F53
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.normal == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 1);
						}
						return VisualStyleElement.ComboBox.DropDownButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the hot state.</returns>
				// Token: 0x1700195A RID: 6490
				// (get) Token: 0x0600730A RID: 29450 RVA: 0x001A3D76 File Offset: 0x001A1F76
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.hot == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.hot = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 2);
						}
						return VisualStyleElement.ComboBox.DropDownButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the pressed state.</returns>
				// Token: 0x1700195B RID: 6491
				// (get) Token: 0x0600730B RID: 29451 RVA: 0x001A3D99 File Offset: 0x001A1F99
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.pressed == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.pressed = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 3);
						}
						return VisualStyleElement.ComboBox.DropDownButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down arrow in the disabled state.</returns>
				// Token: 0x1700195C RID: 6492
				// (get) Token: 0x0600730C RID: 29452 RVA: 0x001A3DBC File Offset: 0x001A1FBC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButton.disabled == null)
						{
							VisualStyleElement.ComboBox.DropDownButton.disabled = new VisualStyleElement(VisualStyleElement.ComboBox.className, VisualStyleElement.ComboBox.DropDownButton.part, 4);
						}
						return VisualStyleElement.ComboBox.DropDownButton.disabled;
					}
				}

				// Token: 0x04004584 RID: 17796
				private static readonly int part = 1;

				// Token: 0x04004585 RID: 17797
				private static VisualStyleElement normal;

				// Token: 0x04004586 RID: 17798
				private static VisualStyleElement hot;

				// Token: 0x04004587 RID: 17799
				private static VisualStyleElement pressed;

				// Token: 0x04004588 RID: 17800
				private static VisualStyleElement disabled;
			}

			// Token: 0x020008D6 RID: 2262
			internal static class Border
			{
				// Token: 0x1700195D RID: 6493
				// (get) Token: 0x0600730E RID: 29454 RVA: 0x001A3DE7 File Offset: 0x001A1FE7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.Border.normal == null)
						{
							VisualStyleElement.ComboBox.Border.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 4, 3);
						}
						return VisualStyleElement.ComboBox.Border.normal;
					}
				}

				// Token: 0x04004589 RID: 17801
				private const int part = 4;

				// Token: 0x0400458A RID: 17802
				private static VisualStyleElement normal;
			}

			// Token: 0x020008D7 RID: 2263
			internal static class ReadOnlyButton
			{
				// Token: 0x1700195E RID: 6494
				// (get) Token: 0x0600730F RID: 29455 RVA: 0x001A3E06 File Offset: 0x001A2006
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.ReadOnlyButton.normal == null)
						{
							VisualStyleElement.ComboBox.ReadOnlyButton.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 5, 2);
						}
						return VisualStyleElement.ComboBox.ReadOnlyButton.normal;
					}
				}

				// Token: 0x0400458B RID: 17803
				private const int part = 5;

				// Token: 0x0400458C RID: 17804
				private static VisualStyleElement normal;
			}

			// Token: 0x020008D8 RID: 2264
			internal static class DropDownButtonRight
			{
				// Token: 0x1700195F RID: 6495
				// (get) Token: 0x06007310 RID: 29456 RVA: 0x001A3E25 File Offset: 0x001A2025
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButtonRight.normal == null)
						{
							VisualStyleElement.ComboBox.DropDownButtonRight.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 6, 1);
						}
						return VisualStyleElement.ComboBox.DropDownButtonRight.normal;
					}
				}

				// Token: 0x0400458D RID: 17805
				private const int part = 6;

				// Token: 0x0400458E RID: 17806
				private static VisualStyleElement normal;
			}

			// Token: 0x020008D9 RID: 2265
			internal static class DropDownButtonLeft
			{
				// Token: 0x17001960 RID: 6496
				// (get) Token: 0x06007311 RID: 29457 RVA: 0x001A3E44 File Offset: 0x001A2044
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ComboBox.DropDownButtonLeft.normal == null)
						{
							VisualStyleElement.ComboBox.DropDownButtonLeft.normal = new VisualStyleElement(VisualStyleElement.ComboBox.className, 7, 2);
						}
						return VisualStyleElement.ComboBox.DropDownButtonLeft.normal;
					}
				}

				// Token: 0x0400458F RID: 17807
				private const int part = 7;

				// Token: 0x04004590 RID: 17808
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a page. This class cannot be inherited.</summary>
		// Token: 0x02000834 RID: 2100
		public static class Page
		{
			// Token: 0x0400435F RID: 17247
			private static readonly string className = "PAGE";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page up indicator of an up-down or spin box control. This class cannot be inherited.</summary>
			// Token: 0x020008DA RID: 2266
			public static class Up
			{
				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the normal state.</returns>
				// Token: 0x17001961 RID: 6497
				// (get) Token: 0x06007312 RID: 29458 RVA: 0x001A3E63 File Offset: 0x001A2063
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.Up.normal == null)
						{
							VisualStyleElement.Page.Up.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 1);
						}
						return VisualStyleElement.Page.Up.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the hot state.</returns>
				// Token: 0x17001962 RID: 6498
				// (get) Token: 0x06007313 RID: 29459 RVA: 0x001A3E86 File Offset: 0x001A2086
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.Up.hot == null)
						{
							VisualStyleElement.Page.Up.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 2);
						}
						return VisualStyleElement.Page.Up.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the pressed state.</returns>
				// Token: 0x17001963 RID: 6499
				// (get) Token: 0x06007314 RID: 29460 RVA: 0x001A3EA9 File Offset: 0x001A20A9
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.Up.pressed == null)
						{
							VisualStyleElement.Page.Up.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 3);
						}
						return VisualStyleElement.Page.Up.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a page up indicator of an up-down or spin box control in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page up indicator of an up-down or spin box control in the disabled state.</returns>
				// Token: 0x17001964 RID: 6500
				// (get) Token: 0x06007315 RID: 29461 RVA: 0x001A3ECC File Offset: 0x001A20CC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.Up.disabled == null)
						{
							VisualStyleElement.Page.Up.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Up.part, 4);
						}
						return VisualStyleElement.Page.Up.disabled;
					}
				}

				// Token: 0x04004591 RID: 17809
				private static readonly int part = 1;

				// Token: 0x04004592 RID: 17810
				private static VisualStyleElement normal;

				// Token: 0x04004593 RID: 17811
				private static VisualStyleElement hot;

				// Token: 0x04004594 RID: 17812
				private static VisualStyleElement pressed;

				// Token: 0x04004595 RID: 17813
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a down indicator in an up-down or spin box control. This class cannot be inherited.</summary>
			// Token: 0x020008DB RID: 2267
			public static class Down
			{
				/// <summary>Gets a visual style element that represents the down indicator of an up-down or spin box control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator up an up-down or spin box control in the normal state.</returns>
				// Token: 0x17001965 RID: 6501
				// (get) Token: 0x06007317 RID: 29463 RVA: 0x001A3EF7 File Offset: 0x001A20F7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.Down.normal == null)
						{
							VisualStyleElement.Page.Down.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 1);
						}
						return VisualStyleElement.Page.Down.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a down indicator of an up-down or spin box control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the down indicator of an up-down or spin box in the hot state.</returns>
				// Token: 0x17001966 RID: 6502
				// (get) Token: 0x06007318 RID: 29464 RVA: 0x001A3F1A File Offset: 0x001A211A
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.Down.hot == null)
						{
							VisualStyleElement.Page.Down.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 2);
						}
						return VisualStyleElement.Page.Down.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the down indicator of an up-down or spin box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator of an up-down or spin box in the pressed state.</returns>
				// Token: 0x17001967 RID: 6503
				// (get) Token: 0x06007319 RID: 29465 RVA: 0x001A3F3D File Offset: 0x001A213D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.Down.pressed == null)
						{
							VisualStyleElement.Page.Down.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 3);
						}
						return VisualStyleElement.Page.Down.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the disabled state of the down indicator in an up-down or spin box control.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a down indicator of an up-down or spin box control in the disabled state.</returns>
				// Token: 0x17001968 RID: 6504
				// (get) Token: 0x0600731A RID: 29466 RVA: 0x001A3F60 File Offset: 0x001A2160
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.Down.disabled == null)
						{
							VisualStyleElement.Page.Down.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.Down.part, 4);
						}
						return VisualStyleElement.Page.Down.disabled;
					}
				}

				// Token: 0x04004596 RID: 17814
				private static readonly int part = 2;

				// Token: 0x04004597 RID: 17815
				private static VisualStyleElement normal;

				// Token: 0x04004598 RID: 17816
				private static VisualStyleElement hot;

				// Token: 0x04004599 RID: 17817
				private static VisualStyleElement pressed;

				// Token: 0x0400459A RID: 17818
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page forward indicator of a pager control. This class cannot be inherited.</summary>
			// Token: 0x020008DC RID: 2268
			public static class UpHorizontal
			{
				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the normal state.</returns>
				// Token: 0x17001969 RID: 6505
				// (get) Token: 0x0600731C RID: 29468 RVA: 0x001A3F8B File Offset: 0x001A218B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.normal == null)
						{
							VisualStyleElement.Page.UpHorizontal.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 1);
						}
						return VisualStyleElement.Page.UpHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the hot state.</returns>
				// Token: 0x1700196A RID: 6506
				// (get) Token: 0x0600731D RID: 29469 RVA: 0x001A3FAE File Offset: 0x001A21AE
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.hot == null)
						{
							VisualStyleElement.Page.UpHorizontal.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 2);
						}
						return VisualStyleElement.Page.UpHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the pressed state.</returns>
				// Token: 0x1700196B RID: 6507
				// (get) Token: 0x0600731E RID: 29470 RVA: 0x001A3FD1 File Offset: 0x001A21D1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.pressed == null)
						{
							VisualStyleElement.Page.UpHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 3);
						}
						return VisualStyleElement.Page.UpHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a page forward indicator of a pager control in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page forward indicator of a pager control in the disabled state.</returns>
				// Token: 0x1700196C RID: 6508
				// (get) Token: 0x0600731F RID: 29471 RVA: 0x001A3FF4 File Offset: 0x001A21F4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.UpHorizontal.disabled == null)
						{
							VisualStyleElement.Page.UpHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.UpHorizontal.part, 4);
						}
						return VisualStyleElement.Page.UpHorizontal.disabled;
					}
				}

				// Token: 0x0400459B RID: 17819
				private static readonly int part = 3;

				// Token: 0x0400459C RID: 17820
				private static VisualStyleElement normal;

				// Token: 0x0400459D RID: 17821
				private static VisualStyleElement hot;

				// Token: 0x0400459E RID: 17822
				private static VisualStyleElement pressed;

				// Token: 0x0400459F RID: 17823
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a page backward indicator in a pager control. This class cannot be inherited.</summary>
			// Token: 0x020008DD RID: 2269
			public static class DownHorizontal
			{
				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the normal state.</returns>
				// Token: 0x1700196D RID: 6509
				// (get) Token: 0x06007321 RID: 29473 RVA: 0x001A401F File Offset: 0x001A221F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.normal == null)
						{
							VisualStyleElement.Page.DownHorizontal.normal = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 1);
						}
						return VisualStyleElement.Page.DownHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the hot state.</returns>
				// Token: 0x1700196E RID: 6510
				// (get) Token: 0x06007322 RID: 29474 RVA: 0x001A4042 File Offset: 0x001A2242
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.hot == null)
						{
							VisualStyleElement.Page.DownHorizontal.hot = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 2);
						}
						return VisualStyleElement.Page.DownHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents page backward indicator of a pager control in the pressed state.</returns>
				// Token: 0x1700196F RID: 6511
				// (get) Token: 0x06007323 RID: 29475 RVA: 0x001A4065 File Offset: 0x001A2265
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.pressed == null)
						{
							VisualStyleElement.Page.DownHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 3);
						}
						return VisualStyleElement.Page.DownHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a page backward indicator of a pager control in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a page backward indicator of a pager control in the disabled state.</returns>
				// Token: 0x17001970 RID: 6512
				// (get) Token: 0x06007324 RID: 29476 RVA: 0x001A4088 File Offset: 0x001A2288
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Page.DownHorizontal.disabled == null)
						{
							VisualStyleElement.Page.DownHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Page.className, VisualStyleElement.Page.DownHorizontal.part, 4);
						}
						return VisualStyleElement.Page.DownHorizontal.disabled;
					}
				}

				// Token: 0x040045A0 RID: 17824
				private static readonly int part = 4;

				// Token: 0x040045A1 RID: 17825
				private static VisualStyleElement normal;

				// Token: 0x040045A2 RID: 17826
				private static VisualStyleElement hot;

				// Token: 0x040045A3 RID: 17827
				private static VisualStyleElement pressed;

				// Token: 0x040045A4 RID: 17828
				private static VisualStyleElement disabled;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the arrows of a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
		// Token: 0x02000835 RID: 2101
		public static class Spin
		{
			// Token: 0x04004360 RID: 17248
			private static readonly string className = "SPIN";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upward-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
			// Token: 0x020008DE RID: 2270
			public static class Up
			{
				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the normal state.</returns>
				// Token: 0x17001971 RID: 6513
				// (get) Token: 0x06007326 RID: 29478 RVA: 0x001A40B3 File Offset: 0x001A22B3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.Up.normal == null)
						{
							VisualStyleElement.Spin.Up.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 1);
						}
						return VisualStyleElement.Spin.Up.normal;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the hot state.</returns>
				// Token: 0x17001972 RID: 6514
				// (get) Token: 0x06007327 RID: 29479 RVA: 0x001A40D6 File Offset: 0x001A22D6
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.Up.hot == null)
						{
							VisualStyleElement.Spin.Up.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 2);
						}
						return VisualStyleElement.Spin.Up.hot;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the pressed state.</returns>
				// Token: 0x17001973 RID: 6515
				// (get) Token: 0x06007328 RID: 29480 RVA: 0x001A40F9 File Offset: 0x001A22F9
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.Up.pressed == null)
						{
							VisualStyleElement.Spin.Up.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 3);
						}
						return VisualStyleElement.Spin.Up.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x17001974 RID: 6516
				// (get) Token: 0x06007329 RID: 29481 RVA: 0x001A411C File Offset: 0x001A231C
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.Up.disabled == null)
						{
							VisualStyleElement.Spin.Up.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Up.part, 4);
						}
						return VisualStyleElement.Spin.Up.disabled;
					}
				}

				// Token: 0x040045A5 RID: 17829
				private static readonly int part = 1;

				// Token: 0x040045A6 RID: 17830
				private static VisualStyleElement normal;

				// Token: 0x040045A7 RID: 17831
				private static VisualStyleElement hot;

				// Token: 0x040045A8 RID: 17832
				private static VisualStyleElement pressed;

				// Token: 0x040045A9 RID: 17833
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the downward-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
			// Token: 0x020008DF RID: 2271
			public static class Down
			{
				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the normal state.</returns>
				// Token: 0x17001975 RID: 6517
				// (get) Token: 0x0600732B RID: 29483 RVA: 0x001A4147 File Offset: 0x001A2347
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.Down.normal == null)
						{
							VisualStyleElement.Spin.Down.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 1);
						}
						return VisualStyleElement.Spin.Down.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the hot state.</returns>
				// Token: 0x17001976 RID: 6518
				// (get) Token: 0x0600732C RID: 29484 RVA: 0x001A416A File Offset: 0x001A236A
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.Down.hot == null)
						{
							VisualStyleElement.Spin.Down.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 2);
						}
						return VisualStyleElement.Spin.Down.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the pressed state.</returns>
				// Token: 0x17001977 RID: 6519
				// (get) Token: 0x0600732D RID: 29485 RVA: 0x001A418D File Offset: 0x001A238D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.Down.pressed == null)
						{
							VisualStyleElement.Spin.Down.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 3);
						}
						return VisualStyleElement.Spin.Down.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x17001978 RID: 6520
				// (get) Token: 0x0600732E RID: 29486 RVA: 0x001A41B0 File Offset: 0x001A23B0
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.Down.disabled == null)
						{
							VisualStyleElement.Spin.Down.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.Down.part, 4);
						}
						return VisualStyleElement.Spin.Down.disabled;
					}
				}

				// Token: 0x040045AA RID: 17834
				private static readonly int part = 2;

				// Token: 0x040045AB RID: 17835
				private static VisualStyleElement normal;

				// Token: 0x040045AC RID: 17836
				private static VisualStyleElement hot;

				// Token: 0x040045AD RID: 17837
				private static VisualStyleElement pressed;

				// Token: 0x040045AE RID: 17838
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
			// Token: 0x020008E0 RID: 2272
			public static class UpHorizontal
			{
				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the normal state.</returns>
				// Token: 0x17001979 RID: 6521
				// (get) Token: 0x06007330 RID: 29488 RVA: 0x001A41DB File Offset: 0x001A23DB
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.normal == null)
						{
							VisualStyleElement.Spin.UpHorizontal.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 1);
						}
						return VisualStyleElement.Spin.UpHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the hot state.</returns>
				// Token: 0x1700197A RID: 6522
				// (get) Token: 0x06007331 RID: 29489 RVA: 0x001A41FE File Offset: 0x001A23FE
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.hot == null)
						{
							VisualStyleElement.Spin.UpHorizontal.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 2);
						}
						return VisualStyleElement.Spin.UpHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the pressed state.</returns>
				// Token: 0x1700197B RID: 6523
				// (get) Token: 0x06007332 RID: 29490 RVA: 0x001A4221 File Offset: 0x001A2421
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.pressed == null)
						{
							VisualStyleElement.Spin.UpHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 3);
						}
						return VisualStyleElement.Spin.UpHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x1700197C RID: 6524
				// (get) Token: 0x06007333 RID: 29491 RVA: 0x001A4244 File Offset: 0x001A2444
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.UpHorizontal.disabled == null)
						{
							VisualStyleElement.Spin.UpHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.UpHorizontal.part, 4);
						}
						return VisualStyleElement.Spin.UpHorizontal.disabled;
					}
				}

				// Token: 0x040045AF RID: 17839
				private static readonly int part = 3;

				// Token: 0x040045B0 RID: 17840
				private static VisualStyleElement normal;

				// Token: 0x040045B1 RID: 17841
				private static VisualStyleElement hot;

				// Token: 0x040045B2 RID: 17842
				private static VisualStyleElement pressed;

				// Token: 0x040045B3 RID: 17843
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left-pointing arrow for a spin button control (also known as an up-down control). This class cannot be inherited.</summary>
			// Token: 0x020008E1 RID: 2273
			public static class DownHorizontal
			{
				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the normal state.</returns>
				// Token: 0x1700197D RID: 6525
				// (get) Token: 0x06007335 RID: 29493 RVA: 0x001A426F File Offset: 0x001A246F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.normal == null)
						{
							VisualStyleElement.Spin.DownHorizontal.normal = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 1);
						}
						return VisualStyleElement.Spin.DownHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the hot state.</returns>
				// Token: 0x1700197E RID: 6526
				// (get) Token: 0x06007336 RID: 29494 RVA: 0x001A4292 File Offset: 0x001A2492
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.hot == null)
						{
							VisualStyleElement.Spin.DownHorizontal.hot = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 2);
						}
						return VisualStyleElement.Spin.DownHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the pressed state.</returns>
				// Token: 0x1700197F RID: 6527
				// (get) Token: 0x06007337 RID: 29495 RVA: 0x001A42B5 File Offset: 0x001A24B5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.pressed == null)
						{
							VisualStyleElement.Spin.DownHorizontal.pressed = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 3);
						}
						return VisualStyleElement.Spin.DownHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing spin button arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing spin button arrow in the disabled state.</returns>
				// Token: 0x17001980 RID: 6528
				// (get) Token: 0x06007338 RID: 29496 RVA: 0x001A42D8 File Offset: 0x001A24D8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Spin.DownHorizontal.disabled == null)
						{
							VisualStyleElement.Spin.DownHorizontal.disabled = new VisualStyleElement(VisualStyleElement.Spin.className, VisualStyleElement.Spin.DownHorizontal.part, 4);
						}
						return VisualStyleElement.Spin.DownHorizontal.disabled;
					}
				}

				// Token: 0x040045B4 RID: 17844
				private static readonly int part = 4;

				// Token: 0x040045B5 RID: 17845
				private static VisualStyleElement normal;

				// Token: 0x040045B6 RID: 17846
				private static VisualStyleElement hot;

				// Token: 0x040045B7 RID: 17847
				private static VisualStyleElement pressed;

				// Token: 0x040045B8 RID: 17848
				private static VisualStyleElement disabled;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the scroll bar control. This class cannot be inherited.</summary>
		// Token: 0x02000836 RID: 2102
		public static class ScrollBar
		{
			// Token: 0x04004361 RID: 17249
			private static readonly string className = "SCROLLBAR";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state and direction of a scroll arrow. This class cannot be inherited.</summary>
			// Token: 0x020008E2 RID: 2274
			public static class ArrowButton
			{
				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17001981 RID: 6529
				// (get) Token: 0x0600733A RID: 29498 RVA: 0x001A4303 File Offset: 0x001A2503
				public static VisualStyleElement UpNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.upnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.upnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 1);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.upnormal;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17001982 RID: 6530
				// (get) Token: 0x0600733B RID: 29499 RVA: 0x001A4326 File Offset: 0x001A2526
				public static VisualStyleElement UpHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.uphot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.uphot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 2);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.uphot;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x17001983 RID: 6531
				// (get) Token: 0x0600733C RID: 29500 RVA: 0x001A4349 File Offset: 0x001A2549
				public static VisualStyleElement UpPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.uppressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.uppressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 3);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.uppressed;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17001984 RID: 6532
				// (get) Token: 0x0600733D RID: 29501 RVA: 0x001A436C File Offset: 0x001A256C
				public static VisualStyleElement UpDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.updisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.updisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 4);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.updisabled;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17001985 RID: 6533
				// (get) Token: 0x0600733E RID: 29502 RVA: 0x001A438F File Offset: 0x001A258F
				public static VisualStyleElement DownNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 5);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downnormal;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the hot state.</returns>
				// Token: 0x17001986 RID: 6534
				// (get) Token: 0x0600733F RID: 29503 RVA: 0x001A43B2 File Offset: 0x001A25B2
				public static VisualStyleElement DownHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downhot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downhot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 6);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downhot;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x17001987 RID: 6535
				// (get) Token: 0x06007340 RID: 29504 RVA: 0x001A43D5 File Offset: 0x001A25D5
				public static VisualStyleElement DownPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downpressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downpressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 7);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17001988 RID: 6536
				// (get) Token: 0x06007341 RID: 29505 RVA: 0x001A43F8 File Offset: 0x001A25F8
				public static VisualStyleElement DownDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.downdisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.downdisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 8);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.downdisabled;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the normal state.</returns>
				// Token: 0x17001989 RID: 6537
				// (get) Token: 0x06007342 RID: 29506 RVA: 0x001A441B File Offset: 0x001A261B
				public static VisualStyleElement LeftNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.leftnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.leftnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 9);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.leftnormal;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the hot state.</returns>
				// Token: 0x1700198A RID: 6538
				// (get) Token: 0x06007343 RID: 29507 RVA: 0x001A443F File Offset: 0x001A263F
				public static VisualStyleElement LeftHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.lefthot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.lefthot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 10);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.lefthot;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x1700198B RID: 6539
				// (get) Token: 0x06007344 RID: 29508 RVA: 0x001A4463 File Offset: 0x001A2663
				public static VisualStyleElement LeftPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.leftpressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.leftpressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 11);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.leftpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x1700198C RID: 6540
				// (get) Token: 0x06007345 RID: 29509 RVA: 0x001A4487 File Offset: 0x001A2687
				public static VisualStyleElement LeftDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.leftdisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.leftdisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 12);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.leftdisabled;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the normal state.</returns>
				// Token: 0x1700198D RID: 6541
				// (get) Token: 0x06007346 RID: 29510 RVA: 0x001A44AB File Offset: 0x001A26AB
				public static VisualStyleElement RightNormal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.rightnormal == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.rightnormal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 13);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.rightnormal;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the hot state.</returns>
				// Token: 0x1700198E RID: 6542
				// (get) Token: 0x06007347 RID: 29511 RVA: 0x001A44CF File Offset: 0x001A26CF
				public static VisualStyleElement RightHot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.righthot == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.righthot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 14);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.righthot;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the pressed state.</returns>
				// Token: 0x1700198F RID: 6543
				// (get) Token: 0x06007348 RID: 29512 RVA: 0x001A44F3 File Offset: 0x001A26F3
				public static VisualStyleElement RightPressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.rightpressed == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.rightpressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 15);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.rightpressed;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing scroll arrow in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing scroll arrow in the disabled state.</returns>
				// Token: 0x17001990 RID: 6544
				// (get) Token: 0x06007349 RID: 29513 RVA: 0x001A4517 File Offset: 0x001A2717
				public static VisualStyleElement RightDisabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ArrowButton.rightdisabled == null)
						{
							VisualStyleElement.ScrollBar.ArrowButton.rightdisabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ArrowButton.part, 16);
						}
						return VisualStyleElement.ScrollBar.ArrowButton.rightdisabled;
					}
				}

				// Token: 0x040045B9 RID: 17849
				private static readonly int part = 1;

				// Token: 0x040045BA RID: 17850
				private static VisualStyleElement upnormal;

				// Token: 0x040045BB RID: 17851
				private static VisualStyleElement uphot;

				// Token: 0x040045BC RID: 17852
				private static VisualStyleElement uppressed;

				// Token: 0x040045BD RID: 17853
				private static VisualStyleElement updisabled;

				// Token: 0x040045BE RID: 17854
				private static VisualStyleElement downnormal;

				// Token: 0x040045BF RID: 17855
				private static VisualStyleElement downhot;

				// Token: 0x040045C0 RID: 17856
				private static VisualStyleElement downpressed;

				// Token: 0x040045C1 RID: 17857
				private static VisualStyleElement downdisabled;

				// Token: 0x040045C2 RID: 17858
				private static VisualStyleElement leftnormal;

				// Token: 0x040045C3 RID: 17859
				private static VisualStyleElement lefthot;

				// Token: 0x040045C4 RID: 17860
				private static VisualStyleElement leftpressed;

				// Token: 0x040045C5 RID: 17861
				private static VisualStyleElement leftdisabled;

				// Token: 0x040045C6 RID: 17862
				private static VisualStyleElement rightnormal;

				// Token: 0x040045C7 RID: 17863
				private static VisualStyleElement righthot;

				// Token: 0x040045C8 RID: 17864
				private static VisualStyleElement rightpressed;

				// Token: 0x040045C9 RID: 17865
				private static VisualStyleElement rightdisabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x020008E3 RID: 2275
			public static class ThumbButtonHorizontal
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
				// Token: 0x17001991 RID: 6545
				// (get) Token: 0x0600734B RID: 29515 RVA: 0x001A4543 File Offset: 0x001A2743
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 1);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
				// Token: 0x17001992 RID: 6546
				// (get) Token: 0x0600734C RID: 29516 RVA: 0x001A4566 File Offset: 0x001A2766
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.hot == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 2);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
				// Token: 0x17001993 RID: 6547
				// (get) Token: 0x0600734D RID: 29517 RVA: 0x001A4589 File Offset: 0x001A2789
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.pressed == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 3);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
				// Token: 0x17001994 RID: 6548
				// (get) Token: 0x0600734E RID: 29518 RVA: 0x001A45AC File Offset: 0x001A27AC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonHorizontal.disabled == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonHorizontal.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonHorizontal.part, 4);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonHorizontal.disabled;
					}
				}

				// Token: 0x040045CA RID: 17866
				private static readonly int part = 2;

				// Token: 0x040045CB RID: 17867
				private static VisualStyleElement normal;

				// Token: 0x040045CC RID: 17868
				private static VisualStyleElement hot;

				// Token: 0x040045CD RID: 17869
				private static VisualStyleElement pressed;

				// Token: 0x040045CE RID: 17870
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x020008E4 RID: 2276
			public static class ThumbButtonVertical
			{
				/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
				// Token: 0x17001995 RID: 6549
				// (get) Token: 0x06007350 RID: 29520 RVA: 0x001A45D7 File Offset: 0x001A27D7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 1);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
				// Token: 0x17001996 RID: 6550
				// (get) Token: 0x06007351 RID: 29521 RVA: 0x001A45FA File Offset: 0x001A27FA
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.hot == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 2);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state.</returns>
				// Token: 0x17001997 RID: 6551
				// (get) Token: 0x06007352 RID: 29522 RVA: 0x001A461D File Offset: 0x001A281D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.pressed == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 3);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
				// Token: 0x17001998 RID: 6552
				// (get) Token: 0x06007353 RID: 29523 RVA: 0x001A4640 File Offset: 0x001A2840
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.ThumbButtonVertical.disabled == null)
						{
							VisualStyleElement.ScrollBar.ThumbButtonVertical.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.ThumbButtonVertical.part, 4);
						}
						return VisualStyleElement.ScrollBar.ThumbButtonVertical.disabled;
					}
				}

				// Token: 0x040045CF RID: 17871
				private static readonly int part = 3;

				// Token: 0x040045D0 RID: 17872
				private static VisualStyleElement normal;

				// Token: 0x040045D1 RID: 17873
				private static VisualStyleElement hot;

				// Token: 0x040045D2 RID: 17874
				private static VisualStyleElement pressed;

				// Token: 0x040045D3 RID: 17875
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right part of a horizontal scroll bar track. This class cannot be inherited.</summary>
			// Token: 0x020008E5 RID: 2277
			public static class RightTrackHorizontal
			{
				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the normal state.</returns>
				// Token: 0x17001999 RID: 6553
				// (get) Token: 0x06007355 RID: 29525 RVA: 0x001A466B File Offset: 0x001A286B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 1);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the hot state.</returns>
				// Token: 0x1700199A RID: 6554
				// (get) Token: 0x06007356 RID: 29526 RVA: 0x001A468E File Offset: 0x001A288E
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.hot == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 2);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the pressed state.</returns>
				// Token: 0x1700199B RID: 6555
				// (get) Token: 0x06007357 RID: 29527 RVA: 0x001A46B1 File Offset: 0x001A28B1
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.pressed == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 3);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the right part of a horizontal scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right part of a horizontal scroll bar track in the disabled state.</returns>
				// Token: 0x1700199C RID: 6556
				// (get) Token: 0x06007358 RID: 29528 RVA: 0x001A46D4 File Offset: 0x001A28D4
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.RightTrackHorizontal.disabled == null)
						{
							VisualStyleElement.ScrollBar.RightTrackHorizontal.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.RightTrackHorizontal.part, 4);
						}
						return VisualStyleElement.ScrollBar.RightTrackHorizontal.disabled;
					}
				}

				// Token: 0x040045D4 RID: 17876
				private static readonly int part = 4;

				// Token: 0x040045D5 RID: 17877
				private static VisualStyleElement normal;

				// Token: 0x040045D6 RID: 17878
				private static VisualStyleElement hot;

				// Token: 0x040045D7 RID: 17879
				private static VisualStyleElement pressed;

				// Token: 0x040045D8 RID: 17880
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left part of a horizontal scroll bar track. This class cannot be inherited.</summary>
			// Token: 0x020008E6 RID: 2278
			public static class LeftTrackHorizontal
			{
				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the normal state.</returns>
				// Token: 0x1700199D RID: 6557
				// (get) Token: 0x0600735A RID: 29530 RVA: 0x001A46FF File Offset: 0x001A28FF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 1);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the hot state.</returns>
				// Token: 0x1700199E RID: 6558
				// (get) Token: 0x0600735B RID: 29531 RVA: 0x001A4722 File Offset: 0x001A2922
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.hot == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 2);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the pressed state.</returns>
				// Token: 0x1700199F RID: 6559
				// (get) Token: 0x0600735C RID: 29532 RVA: 0x001A4745 File Offset: 0x001A2945
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.pressed == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 3);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the left part of a horizontal scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left part of a horizontal scroll bar track in the disabled state.</returns>
				// Token: 0x170019A0 RID: 6560
				// (get) Token: 0x0600735D RID: 29533 RVA: 0x001A4768 File Offset: 0x001A2968
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LeftTrackHorizontal.disabled == null)
						{
							VisualStyleElement.ScrollBar.LeftTrackHorizontal.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LeftTrackHorizontal.part, 4);
						}
						return VisualStyleElement.ScrollBar.LeftTrackHorizontal.disabled;
					}
				}

				// Token: 0x040045D9 RID: 17881
				private static readonly int part = 5;

				// Token: 0x040045DA RID: 17882
				private static VisualStyleElement normal;

				// Token: 0x040045DB RID: 17883
				private static VisualStyleElement hot;

				// Token: 0x040045DC RID: 17884
				private static VisualStyleElement pressed;

				// Token: 0x040045DD RID: 17885
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the lower part of a vertical scroll bar track. This class cannot be inherited.</summary>
			// Token: 0x020008E7 RID: 2279
			public static class LowerTrackVertical
			{
				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the normal state.</returns>
				// Token: 0x170019A1 RID: 6561
				// (get) Token: 0x0600735F RID: 29535 RVA: 0x001A4793 File Offset: 0x001A2993
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 1);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the hot state.</returns>
				// Token: 0x170019A2 RID: 6562
				// (get) Token: 0x06007360 RID: 29536 RVA: 0x001A47B6 File Offset: 0x001A29B6
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.hot == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 2);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the pressed state.</returns>
				// Token: 0x170019A3 RID: 6563
				// (get) Token: 0x06007361 RID: 29537 RVA: 0x001A47D9 File Offset: 0x001A29D9
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.pressed == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 3);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the lower part of a vertical scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the lower part of a vertical scroll bar track in the disabled state.</returns>
				// Token: 0x170019A4 RID: 6564
				// (get) Token: 0x06007362 RID: 29538 RVA: 0x001A47FC File Offset: 0x001A29FC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.LowerTrackVertical.disabled == null)
						{
							VisualStyleElement.ScrollBar.LowerTrackVertical.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.LowerTrackVertical.part, 4);
						}
						return VisualStyleElement.ScrollBar.LowerTrackVertical.disabled;
					}
				}

				// Token: 0x040045DE RID: 17886
				private static readonly int part = 6;

				// Token: 0x040045DF RID: 17887
				private static VisualStyleElement normal;

				// Token: 0x040045E0 RID: 17888
				private static VisualStyleElement hot;

				// Token: 0x040045E1 RID: 17889
				private static VisualStyleElement pressed;

				// Token: 0x040045E2 RID: 17890
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upper part of a vertical scroll bar track. This class cannot be inherited.</summary>
			// Token: 0x020008E8 RID: 2280
			public static class UpperTrackVertical
			{
				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the normal state.</returns>
				// Token: 0x170019A5 RID: 6565
				// (get) Token: 0x06007364 RID: 29540 RVA: 0x001A4827 File Offset: 0x001A2A27
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 1);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the hot state.</returns>
				// Token: 0x170019A6 RID: 6566
				// (get) Token: 0x06007365 RID: 29541 RVA: 0x001A484A File Offset: 0x001A2A4A
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.hot == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.hot = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 2);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the pressed state.</returns>
				// Token: 0x170019A7 RID: 6567
				// (get) Token: 0x06007366 RID: 29542 RVA: 0x001A486D File Offset: 0x001A2A6D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.pressed == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.pressed = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 3);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the upper part of a vertical scroll bar track in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the upper part of a vertical scroll bar track in the disabled state.</returns>
				// Token: 0x170019A8 RID: 6568
				// (get) Token: 0x06007367 RID: 29543 RVA: 0x001A4890 File Offset: 0x001A2A90
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ScrollBar.UpperTrackVertical.disabled == null)
						{
							VisualStyleElement.ScrollBar.UpperTrackVertical.disabled = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.UpperTrackVertical.part, 4);
						}
						return VisualStyleElement.ScrollBar.UpperTrackVertical.disabled;
					}
				}

				// Token: 0x040045E3 RID: 17891
				private static readonly int part = 7;

				// Token: 0x040045E4 RID: 17892
				private static VisualStyleElement normal;

				// Token: 0x040045E5 RID: 17893
				private static VisualStyleElement hot;

				// Token: 0x040045E6 RID: 17894
				private static VisualStyleElement pressed;

				// Token: 0x040045E7 RID: 17895
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a horizontal scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x020008E9 RID: 2281
			public static class GripperHorizontal
			{
				/// <summary>Gets a visual style element that represents a grip for a horizontal scroll box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a horizontal scroll box.</returns>
				// Token: 0x170019A9 RID: 6569
				// (get) Token: 0x06007369 RID: 29545 RVA: 0x001A48BB File Offset: 0x001A2ABB
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.GripperHorizontal.normal == null)
						{
							VisualStyleElement.ScrollBar.GripperHorizontal.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.GripperHorizontal.part, 0);
						}
						return VisualStyleElement.ScrollBar.GripperHorizontal.normal;
					}
				}

				// Token: 0x040045E8 RID: 17896
				private static readonly int part = 8;

				// Token: 0x040045E9 RID: 17897
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of a vertical scroll box (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x020008EA RID: 2282
			public static class GripperVertical
			{
				/// <summary>Gets a visual style element that represents a grip for a vertical scroll box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a grip for a vertical scroll box.</returns>
				// Token: 0x170019AA RID: 6570
				// (get) Token: 0x0600736B RID: 29547 RVA: 0x001A48E6 File Offset: 0x001A2AE6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ScrollBar.GripperVertical.normal == null)
						{
							VisualStyleElement.ScrollBar.GripperVertical.normal = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.GripperVertical.part, 0);
						}
						return VisualStyleElement.ScrollBar.GripperVertical.normal;
					}
				}

				// Token: 0x040045EA RID: 17898
				private static readonly int part = 9;

				// Token: 0x040045EB RID: 17899
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the sizing handle of a scroll bar. This class cannot be inherited.</summary>
			// Token: 0x020008EB RID: 2283
			public static class SizeBox
			{
				/// <summary>Gets a visual style element that represents a sizing handle that is aligned to the right.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sizing handle that is aligned to the right.</returns>
				// Token: 0x170019AB RID: 6571
				// (get) Token: 0x0600736D RID: 29549 RVA: 0x001A4912 File Offset: 0x001A2B12
				public static VisualStyleElement RightAlign
				{
					get
					{
						if (VisualStyleElement.ScrollBar.SizeBox.rightalign == null)
						{
							VisualStyleElement.ScrollBar.SizeBox.rightalign = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.SizeBox.part, 1);
						}
						return VisualStyleElement.ScrollBar.SizeBox.rightalign;
					}
				}

				/// <summary>Gets a visual style element that represents a sizing handle that is aligned to the left.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sizing handle that is aligned to the left.</returns>
				// Token: 0x170019AC RID: 6572
				// (get) Token: 0x0600736E RID: 29550 RVA: 0x001A4935 File Offset: 0x001A2B35
				public static VisualStyleElement LeftAlign
				{
					get
					{
						if (VisualStyleElement.ScrollBar.SizeBox.leftalign == null)
						{
							VisualStyleElement.ScrollBar.SizeBox.leftalign = new VisualStyleElement(VisualStyleElement.ScrollBar.className, VisualStyleElement.ScrollBar.SizeBox.part, 2);
						}
						return VisualStyleElement.ScrollBar.SizeBox.leftalign;
					}
				}

				// Token: 0x040045EC RID: 17900
				private static readonly int part = 10;

				// Token: 0x040045ED RID: 17901
				private static VisualStyleElement rightalign;

				// Token: 0x040045EE RID: 17902
				private static VisualStyleElement leftalign;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a tab control. This class cannot be inherited.</summary>
		// Token: 0x02000837 RID: 2103
		public static class Tab
		{
			// Token: 0x04004362 RID: 17250
			private static readonly string className = "TAB";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top, left, and right borders with other tab controls. This class cannot be inherited.</summary>
			// Token: 0x020008EC RID: 2284
			public static class TabItem
			{
				/// <summary>Gets a visual style element that represents a normal tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x170019AD RID: 6573
				// (get) Token: 0x06007370 RID: 29552 RVA: 0x001A4961 File Offset: 0x001A2B61
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.normal == null)
						{
							VisualStyleElement.Tab.TabItem.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 1);
						}
						return VisualStyleElement.Tab.TabItem.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x170019AE RID: 6574
				// (get) Token: 0x06007371 RID: 29553 RVA: 0x001A4984 File Offset: 0x001A2B84
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.hot == null)
						{
							VisualStyleElement.Tab.TabItem.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 2);
						}
						return VisualStyleElement.Tab.TabItem.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x170019AF RID: 6575
				// (get) Token: 0x06007372 RID: 29554 RVA: 0x001A49A7 File Offset: 0x001A2BA7
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.pressed == null)
						{
							VisualStyleElement.Tab.TabItem.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 3);
						}
						return VisualStyleElement.Tab.TabItem.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled tab control that shares its top, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top, left, and right borders with other tab controls.</returns>
				// Token: 0x170019B0 RID: 6576
				// (get) Token: 0x06007373 RID: 29555 RVA: 0x001A49CA File Offset: 0x001A2BCA
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TabItem.disabled == null)
						{
							VisualStyleElement.Tab.TabItem.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItem.part, 4);
						}
						return VisualStyleElement.Tab.TabItem.disabled;
					}
				}

				// Token: 0x040045EF RID: 17903
				private static readonly int part = 1;

				// Token: 0x040045F0 RID: 17904
				private static VisualStyleElement normal;

				// Token: 0x040045F1 RID: 17905
				private static VisualStyleElement hot;

				// Token: 0x040045F2 RID: 17906
				private static VisualStyleElement pressed;

				// Token: 0x040045F3 RID: 17907
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top and right borders with other tab controls. This class cannot be inherited.</summary>
			// Token: 0x020008ED RID: 2285
			public static class TabItemLeftEdge
			{
				/// <summary>Gets a visual style element that represents a normal tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x170019B1 RID: 6577
				// (get) Token: 0x06007375 RID: 29557 RVA: 0x001A49F5 File Offset: 0x001A2BF5
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.normal == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 1);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x170019B2 RID: 6578
				// (get) Token: 0x06007376 RID: 29558 RVA: 0x001A4A18 File Offset: 0x001A2C18
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.hot == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 2);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x170019B3 RID: 6579
				// (get) Token: 0x06007377 RID: 29559 RVA: 0x001A4A3B File Offset: 0x001A2C3B
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.pressed == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 3);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled tab control that shares its top and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top and right borders with other tab controls.</returns>
				// Token: 0x170019B4 RID: 6580
				// (get) Token: 0x06007378 RID: 29560 RVA: 0x001A4A5E File Offset: 0x001A2C5E
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemLeftEdge.disabled == null)
						{
							VisualStyleElement.Tab.TabItemLeftEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemLeftEdge.part, 4);
						}
						return VisualStyleElement.Tab.TabItemLeftEdge.disabled;
					}
				}

				// Token: 0x040045F4 RID: 17908
				private static readonly int part = 2;

				// Token: 0x040045F5 RID: 17909
				private static VisualStyleElement normal;

				// Token: 0x040045F6 RID: 17910
				private static VisualStyleElement hot;

				// Token: 0x040045F7 RID: 17911
				private static VisualStyleElement pressed;

				// Token: 0x040045F8 RID: 17912
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its top and left borders with other tab controls. This class cannot be inherited.</summary>
			// Token: 0x020008EE RID: 2286
			public static class TabItemRightEdge
			{
				/// <summary>Gets a visual style element that represents a normal tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x170019B5 RID: 6581
				// (get) Token: 0x0600737A RID: 29562 RVA: 0x001A4A89 File Offset: 0x001A2C89
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.normal == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 1);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x170019B6 RID: 6582
				// (get) Token: 0x0600737B RID: 29563 RVA: 0x001A4AAC File Offset: 0x001A2CAC
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.hot == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 2);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x170019B7 RID: 6583
				// (get) Token: 0x0600737C RID: 29564 RVA: 0x001A4ACF File Offset: 0x001A2CCF
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.pressed == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 3);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled tab control that shares its top and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its top and left borders with other tab controls.</returns>
				// Token: 0x170019B8 RID: 6584
				// (get) Token: 0x0600737D RID: 29565 RVA: 0x001A4AF2 File Offset: 0x001A2CF2
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemRightEdge.disabled == null)
						{
							VisualStyleElement.Tab.TabItemRightEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemRightEdge.part, 4);
						}
						return VisualStyleElement.Tab.TabItemRightEdge.disabled;
					}
				}

				// Token: 0x040045F9 RID: 17913
				private static readonly int part = 3;

				// Token: 0x040045FA RID: 17914
				private static VisualStyleElement normal;

				// Token: 0x040045FB RID: 17915
				private static VisualStyleElement hot;

				// Token: 0x040045FC RID: 17916
				private static VisualStyleElement pressed;

				// Token: 0x040045FD RID: 17917
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tab control that shares its top border with another tab control. This class cannot be inherited.</summary>
			// Token: 0x020008EF RID: 2287
			public static class TabItemBothEdges
			{
				/// <summary>Gets a visual style element that represents a tab control that shares its top border with another tab control.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tab control that shares its top border with another tab control.</returns>
				// Token: 0x170019B9 RID: 6585
				// (get) Token: 0x0600737F RID: 29567 RVA: 0x001A4B1D File Offset: 0x001A2D1D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TabItemBothEdges.normal == null)
						{
							VisualStyleElement.Tab.TabItemBothEdges.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TabItemBothEdges.part, 0);
						}
						return VisualStyleElement.Tab.TabItemBothEdges.normal;
					}
				}

				// Token: 0x040045FE RID: 17918
				private static readonly int part = 4;

				// Token: 0x040045FF RID: 17919
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom, left, and right borders with other tab controls. This class cannot be inherited.</summary>
			// Token: 0x020008F0 RID: 2288
			public static class TopTabItem
			{
				/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x170019BA RID: 6586
				// (get) Token: 0x06007381 RID: 29569 RVA: 0x001A4B48 File Offset: 0x001A2D48
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.normal == null)
						{
							VisualStyleElement.Tab.TopTabItem.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 1);
						}
						return VisualStyleElement.Tab.TopTabItem.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x170019BB RID: 6587
				// (get) Token: 0x06007382 RID: 29570 RVA: 0x001A4B6B File Offset: 0x001A2D6B
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.hot == null)
						{
							VisualStyleElement.Tab.TopTabItem.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 2);
						}
						return VisualStyleElement.Tab.TopTabItem.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x170019BC RID: 6588
				// (get) Token: 0x06007383 RID: 29571 RVA: 0x001A4B8E File Offset: 0x001A2D8E
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.pressed == null)
						{
							VisualStyleElement.Tab.TopTabItem.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 3);
						}
						return VisualStyleElement.Tab.TopTabItem.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom, left, and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom, left, and right borders with other tab controls.</returns>
				// Token: 0x170019BD RID: 6589
				// (get) Token: 0x06007384 RID: 29572 RVA: 0x001A4BB1 File Offset: 0x001A2DB1
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItem.disabled == null)
						{
							VisualStyleElement.Tab.TopTabItem.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItem.part, 4);
						}
						return VisualStyleElement.Tab.TopTabItem.disabled;
					}
				}

				// Token: 0x04004600 RID: 17920
				private static readonly int part = 5;

				// Token: 0x04004601 RID: 17921
				private static VisualStyleElement normal;

				// Token: 0x04004602 RID: 17922
				private static VisualStyleElement hot;

				// Token: 0x04004603 RID: 17923
				private static VisualStyleElement pressed;

				// Token: 0x04004604 RID: 17924
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom and right borders with other tab controls. This class cannot be inherited.</summary>
			// Token: 0x020008F1 RID: 2289
			public static class TopTabItemLeftEdge
			{
				/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x170019BE RID: 6590
				// (get) Token: 0x06007386 RID: 29574 RVA: 0x001A4BDC File Offset: 0x001A2DDC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.normal == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 1);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x170019BF RID: 6591
				// (get) Token: 0x06007387 RID: 29575 RVA: 0x001A4BFF File Offset: 0x001A2DFF
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.hot == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 2);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x170019C0 RID: 6592
				// (get) Token: 0x06007388 RID: 29576 RVA: 0x001A4C22 File Offset: 0x001A2E22
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.pressed == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 3);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom and right borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom and right borders with other tab controls.</returns>
				// Token: 0x170019C1 RID: 6593
				// (get) Token: 0x06007389 RID: 29577 RVA: 0x001A4C45 File Offset: 0x001A2E45
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemLeftEdge.disabled == null)
						{
							VisualStyleElement.Tab.TopTabItemLeftEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemLeftEdge.part, 4);
						}
						return VisualStyleElement.Tab.TopTabItemLeftEdge.disabled;
					}
				}

				// Token: 0x04004605 RID: 17925
				private static readonly int part = 6;

				// Token: 0x04004606 RID: 17926
				private static VisualStyleElement normal;

				// Token: 0x04004607 RID: 17927
				private static VisualStyleElement hot;

				// Token: 0x04004608 RID: 17928
				private static VisualStyleElement pressed;

				// Token: 0x04004609 RID: 17929
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tab control that shares its bottom and left borders with other tab controls. This class cannot be inherited.</summary>
			// Token: 0x020008F2 RID: 2290
			public static class TopTabItemRightEdge
			{
				/// <summary>Gets a visual style element that represents a normal tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x170019C2 RID: 6594
				// (get) Token: 0x0600738B RID: 29579 RVA: 0x001A4C70 File Offset: 0x001A2E70
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.normal == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 1);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x170019C3 RID: 6595
				// (get) Token: 0x0600738C RID: 29580 RVA: 0x001A4C93 File Offset: 0x001A2E93
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.hot == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.hot = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 2);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x170019C4 RID: 6596
				// (get) Token: 0x0600738D RID: 29581 RVA: 0x001A4CB6 File Offset: 0x001A2EB6
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.pressed == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.pressed = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 3);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled tab control that shares its bottom and left borders with other tab controls.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled tab control that shares its bottom and left borders with other tab controls.</returns>
				// Token: 0x170019C5 RID: 6597
				// (get) Token: 0x0600738E RID: 29582 RVA: 0x001A4CD9 File Offset: 0x001A2ED9
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemRightEdge.disabled == null)
						{
							VisualStyleElement.Tab.TopTabItemRightEdge.disabled = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemRightEdge.part, 4);
						}
						return VisualStyleElement.Tab.TopTabItemRightEdge.disabled;
					}
				}

				// Token: 0x0400460A RID: 17930
				private static readonly int part = 7;

				// Token: 0x0400460B RID: 17931
				private static VisualStyleElement normal;

				// Token: 0x0400460C RID: 17932
				private static VisualStyleElement hot;

				// Token: 0x0400460D RID: 17933
				private static VisualStyleElement pressed;

				// Token: 0x0400460E RID: 17934
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tab control that shares its bottom border with another tab control. This class cannot be inherited.</summary>
			// Token: 0x020008F3 RID: 2291
			public static class TopTabItemBothEdges
			{
				/// <summary>Gets a visual style element that represents a tab control that shares its bottom border with another tab control.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tab control that shares its bottom border with another tab control.</returns>
				// Token: 0x170019C6 RID: 6598
				// (get) Token: 0x06007390 RID: 29584 RVA: 0x001A4D04 File Offset: 0x001A2F04
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.TopTabItemBothEdges.normal == null)
						{
							VisualStyleElement.Tab.TopTabItemBothEdges.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.TopTabItemBothEdges.part, 0);
						}
						return VisualStyleElement.Tab.TopTabItemBothEdges.normal;
					}
				}

				// Token: 0x0400460F RID: 17935
				private static readonly int part = 8;

				// Token: 0x04004610 RID: 17936
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the border of a tab control page. This class cannot be inherited.</summary>
			// Token: 0x020008F4 RID: 2292
			public static class Pane
			{
				/// <summary>Gets a visual style element that represents the border of a tab control page.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the border of a tab control page.</returns>
				// Token: 0x170019C7 RID: 6599
				// (get) Token: 0x06007392 RID: 29586 RVA: 0x001A4D2F File Offset: 0x001A2F2F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.Pane.normal == null)
						{
							VisualStyleElement.Tab.Pane.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.Pane.part, 0);
						}
						return VisualStyleElement.Tab.Pane.normal;
					}
				}

				// Token: 0x04004611 RID: 17937
				private static readonly int part = 9;

				// Token: 0x04004612 RID: 17938
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the interior of a tab control page. This class cannot be inherited.</summary>
			// Token: 0x020008F5 RID: 2293
			public static class Body
			{
				/// <summary>Gets a visual style element that represents the interior of a tab control page.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the interior of a tab control page.</returns>
				// Token: 0x170019C8 RID: 6600
				// (get) Token: 0x06007394 RID: 29588 RVA: 0x001A4D5B File Offset: 0x001A2F5B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Tab.Body.normal == null)
						{
							VisualStyleElement.Tab.Body.normal = new VisualStyleElement(VisualStyleElement.Tab.className, VisualStyleElement.Tab.Body.part, 0);
						}
						return VisualStyleElement.Tab.Body.normal;
					}
				}

				// Token: 0x04004613 RID: 17939
				private static readonly int part = 10;

				// Token: 0x04004614 RID: 17940
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the Explorer Bar. This class cannot be inherited.</summary>
		// Token: 0x02000838 RID: 2104
		public static class ExplorerBar
		{
			// Token: 0x04004363 RID: 17251
			private static readonly string className = "EXPLORERBAR";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008F6 RID: 2294
			public static class HeaderBackground
			{
				/// <summary>Gets a visual style element that represents the background of the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the Explorer Bar.</returns>
				// Token: 0x170019C9 RID: 6601
				// (get) Token: 0x06007396 RID: 29590 RVA: 0x001A4D87 File Offset: 0x001A2F87
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderBackground.normal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderBackground.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderBackground.part, 0);
						}
						return VisualStyleElement.ExplorerBar.HeaderBackground.normal;
					}
				}

				// Token: 0x04004615 RID: 17941
				private static readonly int part = 1;

				// Token: 0x04004616 RID: 17942
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008F7 RID: 2295
			public static class HeaderClose
			{
				/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
				// Token: 0x170019CA RID: 6602
				// (get) Token: 0x06007398 RID: 29592 RVA: 0x001A4DB2 File Offset: 0x001A2FB2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderClose.normal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderClose.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderClose.part, 1);
						}
						return VisualStyleElement.ExplorerBar.HeaderClose.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
				// Token: 0x170019CB RID: 6603
				// (get) Token: 0x06007399 RID: 29593 RVA: 0x001A4DD5 File Offset: 0x001A2FD5
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderClose.hot == null)
						{
							VisualStyleElement.ExplorerBar.HeaderClose.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderClose.part, 2);
						}
						return VisualStyleElement.ExplorerBar.HeaderClose.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state.</returns>
				// Token: 0x170019CC RID: 6604
				// (get) Token: 0x0600739A RID: 29594 RVA: 0x001A4DF8 File Offset: 0x001A2FF8
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderClose.pressed == null)
						{
							VisualStyleElement.ExplorerBar.HeaderClose.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderClose.part, 3);
						}
						return VisualStyleElement.ExplorerBar.HeaderClose.pressed;
					}
				}

				// Token: 0x04004617 RID: 17943
				private static readonly int part = 2;

				// Token: 0x04004618 RID: 17944
				private static VisualStyleElement normal;

				// Token: 0x04004619 RID: 17945
				private static VisualStyleElement hot;

				// Token: 0x0400461A RID: 17946
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Auto Hide button (which is displayed as a push pin) of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008F8 RID: 2296
			public static class HeaderPin
			{
				/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the normal state.</returns>
				// Token: 0x170019CD RID: 6605
				// (get) Token: 0x0600739C RID: 29596 RVA: 0x001A4E23 File Offset: 0x001A3023
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.normal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 1);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.normal;
					}
				}

				/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the hot state.</returns>
				// Token: 0x170019CE RID: 6606
				// (get) Token: 0x0600739D RID: 29597 RVA: 0x001A4E46 File Offset: 0x001A3046
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.hot == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 2);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.hot;
					}
				}

				/// <summary>Gets a visual style element that represents an Auto Hide button (which is displayed as a push pin) in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an Auto Hide button in the pressed state.</returns>
				// Token: 0x170019CF RID: 6607
				// (get) Token: 0x0600739E RID: 29598 RVA: 0x001A4E69 File Offset: 0x001A3069
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.pressed == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 3);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the normal state.</returns>
				// Token: 0x170019D0 RID: 6608
				// (get) Token: 0x0600739F RID: 29599 RVA: 0x001A4E8C File Offset: 0x001A308C
				public static VisualStyleElement SelectedNormal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.selectednormal == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.selectednormal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 4);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.selectednormal;
					}
				}

				/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the hot state.</returns>
				// Token: 0x170019D1 RID: 6609
				// (get) Token: 0x060073A0 RID: 29600 RVA: 0x001A4EAF File Offset: 0x001A30AF
				public static VisualStyleElement SelectedHot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.selectedhot == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.selectedhot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 5);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.selectedhot;
					}
				}

				/// <summary>Gets a visual style element that represents a selected Auto Hide button (which is displayed as a push pin) in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected Auto Hide button in the pressed state.</returns>
				// Token: 0x170019D2 RID: 6610
				// (get) Token: 0x060073A1 RID: 29601 RVA: 0x001A4ED2 File Offset: 0x001A30D2
				public static VisualStyleElement SelectedPressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.HeaderPin.selectedpressed == null)
						{
							VisualStyleElement.ExplorerBar.HeaderPin.selectedpressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.HeaderPin.part, 6);
						}
						return VisualStyleElement.ExplorerBar.HeaderPin.selectedpressed;
					}
				}

				// Token: 0x0400461B RID: 17947
				private static readonly int part = 3;

				// Token: 0x0400461C RID: 17948
				private static VisualStyleElement normal;

				// Token: 0x0400461D RID: 17949
				private static VisualStyleElement hot;

				// Token: 0x0400461E RID: 17950
				private static VisualStyleElement pressed;

				// Token: 0x0400461F RID: 17951
				private static VisualStyleElement selectednormal;

				// Token: 0x04004620 RID: 17952
				private static VisualStyleElement selectedhot;

				// Token: 0x04004621 RID: 17953
				private static VisualStyleElement selectedpressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expanded-menu arrow of the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008F9 RID: 2297
			public static class IEBarMenu
			{
				/// <summary>Gets a visual style element that represents a normal menu button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal menu button.</returns>
				// Token: 0x170019D3 RID: 6611
				// (get) Token: 0x060073A3 RID: 29603 RVA: 0x001A4EFD File Offset: 0x001A30FD
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.IEBarMenu.normal == null)
						{
							VisualStyleElement.ExplorerBar.IEBarMenu.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.IEBarMenu.part, 1);
						}
						return VisualStyleElement.ExplorerBar.IEBarMenu.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot menu button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot menu button.</returns>
				// Token: 0x170019D4 RID: 6612
				// (get) Token: 0x060073A4 RID: 29604 RVA: 0x001A4F20 File Offset: 0x001A3120
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.IEBarMenu.hot == null)
						{
							VisualStyleElement.ExplorerBar.IEBarMenu.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.IEBarMenu.part, 2);
						}
						return VisualStyleElement.ExplorerBar.IEBarMenu.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed menu button.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed menu button.</returns>
				// Token: 0x170019D5 RID: 6613
				// (get) Token: 0x060073A5 RID: 29605 RVA: 0x001A4F43 File Offset: 0x001A3143
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.IEBarMenu.pressed == null)
						{
							VisualStyleElement.ExplorerBar.IEBarMenu.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.IEBarMenu.part, 3);
						}
						return VisualStyleElement.ExplorerBar.IEBarMenu.pressed;
					}
				}

				// Token: 0x04004622 RID: 17954
				private static readonly int part = 4;

				// Token: 0x04004623 RID: 17955
				private static VisualStyleElement normal;

				// Token: 0x04004624 RID: 17956
				private static VisualStyleElement hot;

				// Token: 0x04004625 RID: 17957
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008FA RID: 2298
			public static class NormalGroupBackground
			{
				/// <summary>Gets a visual style element that represents the background of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a common group of items in the Explorer Bar.</returns>
				// Token: 0x170019D6 RID: 6614
				// (get) Token: 0x060073A7 RID: 29607 RVA: 0x001A4F6E File Offset: 0x001A316E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupBackground.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupBackground.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupBackground.part, 0);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupBackground.normal;
					}
				}

				// Token: 0x04004626 RID: 17958
				private static readonly int part = 5;

				// Token: 0x04004627 RID: 17959
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the collapse button of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008FB RID: 2299
			public static class NormalGroupCollapse
			{
				/// <summary>Gets a visual style element that represents a normal collapse button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal collapse button.</returns>
				// Token: 0x170019D7 RID: 6615
				// (get) Token: 0x060073A9 RID: 29609 RVA: 0x001A4F99 File Offset: 0x001A3199
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupCollapse.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupCollapse.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupCollapse.part, 1);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupCollapse.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot collapse button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot collapse button.</returns>
				// Token: 0x170019D8 RID: 6616
				// (get) Token: 0x060073AA RID: 29610 RVA: 0x001A4FBC File Offset: 0x001A31BC
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupCollapse.hot == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupCollapse.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupCollapse.part, 2);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupCollapse.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed collapse button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed collapse button.</returns>
				// Token: 0x170019D9 RID: 6617
				// (get) Token: 0x060073AB RID: 29611 RVA: 0x001A4FDF File Offset: 0x001A31DF
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupCollapse.pressed == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupCollapse.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupCollapse.part, 3);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupCollapse.pressed;
					}
				}

				// Token: 0x04004628 RID: 17960
				private static readonly int part = 6;

				// Token: 0x04004629 RID: 17961
				private static VisualStyleElement normal;

				// Token: 0x0400462A RID: 17962
				private static VisualStyleElement hot;

				// Token: 0x0400462B RID: 17963
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expand button of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008FC RID: 2300
			public static class NormalGroupExpand
			{
				/// <summary>Gets a visual style element that represents a normal expand button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal expand button.</returns>
				// Token: 0x170019DA RID: 6618
				// (get) Token: 0x060073AD RID: 29613 RVA: 0x001A500A File Offset: 0x001A320A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupExpand.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupExpand.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupExpand.part, 1);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupExpand.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot expand button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot expand button.</returns>
				// Token: 0x170019DB RID: 6619
				// (get) Token: 0x060073AE RID: 29614 RVA: 0x001A502D File Offset: 0x001A322D
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupExpand.hot == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupExpand.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupExpand.part, 2);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupExpand.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed expand button of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed expand button.</returns>
				// Token: 0x170019DC RID: 6620
				// (get) Token: 0x060073AF RID: 29615 RVA: 0x001A5050 File Offset: 0x001A3250
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupExpand.pressed == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupExpand.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupExpand.part, 3);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupExpand.pressed;
					}
				}

				// Token: 0x0400462C RID: 17964
				private static readonly int part = 7;

				// Token: 0x0400462D RID: 17965
				private static VisualStyleElement normal;

				// Token: 0x0400462E RID: 17966
				private static VisualStyleElement hot;

				// Token: 0x0400462F RID: 17967
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title bar of a common group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008FD RID: 2301
			public static class NormalGroupHead
			{
				/// <summary>Gets a visual style element that represents the title bar of a common group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a common group of items in the Explorer Bar.</returns>
				// Token: 0x170019DD RID: 6621
				// (get) Token: 0x060073B1 RID: 29617 RVA: 0x001A507B File Offset: 0x001A327B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.NormalGroupHead.normal == null)
						{
							VisualStyleElement.ExplorerBar.NormalGroupHead.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.NormalGroupHead.part, 0);
						}
						return VisualStyleElement.ExplorerBar.NormalGroupHead.normal;
					}
				}

				// Token: 0x04004630 RID: 17968
				private static readonly int part = 8;

				// Token: 0x04004631 RID: 17969
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008FE RID: 2302
			public static class SpecialGroupBackground
			{
				/// <summary>Gets a visual style element that represents the background of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a special group of items in the Explorer Bar.</returns>
				// Token: 0x170019DE RID: 6622
				// (get) Token: 0x060073B3 RID: 29619 RVA: 0x001A50A6 File Offset: 0x001A32A6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupBackground.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupBackground.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupBackground.part, 0);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupBackground.normal;
					}
				}

				// Token: 0x04004632 RID: 17970
				private static readonly int part = 9;

				// Token: 0x04004633 RID: 17971
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the collapse button of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x020008FF RID: 2303
			public static class SpecialGroupCollapse
			{
				/// <summary>Gets a visual style element that represents a normal collapse button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal collapse button.</returns>
				// Token: 0x170019DF RID: 6623
				// (get) Token: 0x060073B5 RID: 29621 RVA: 0x001A50D2 File Offset: 0x001A32D2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupCollapse.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupCollapse.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupCollapse.part, 1);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupCollapse.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot collapse button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot collapse button.</returns>
				// Token: 0x170019E0 RID: 6624
				// (get) Token: 0x060073B6 RID: 29622 RVA: 0x001A50F5 File Offset: 0x001A32F5
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupCollapse.hot == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupCollapse.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupCollapse.part, 2);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupCollapse.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed collapse button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed collapse button.</returns>
				// Token: 0x170019E1 RID: 6625
				// (get) Token: 0x060073B7 RID: 29623 RVA: 0x001A5118 File Offset: 0x001A3318
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupCollapse.pressed == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupCollapse.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupCollapse.part, 3);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupCollapse.pressed;
					}
				}

				// Token: 0x04004634 RID: 17972
				private static readonly int part = 10;

				// Token: 0x04004635 RID: 17973
				private static VisualStyleElement normal;

				// Token: 0x04004636 RID: 17974
				private static VisualStyleElement hot;

				// Token: 0x04004637 RID: 17975
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the expand button of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000900 RID: 2304
			public static class SpecialGroupExpand
			{
				/// <summary>Gets a visual style element that represents a normal expand button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal expand button.</returns>
				// Token: 0x170019E2 RID: 6626
				// (get) Token: 0x060073B9 RID: 29625 RVA: 0x001A5144 File Offset: 0x001A3344
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupExpand.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupExpand.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupExpand.part, 1);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupExpand.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot expand button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot expand button.</returns>
				// Token: 0x170019E3 RID: 6627
				// (get) Token: 0x060073BA RID: 29626 RVA: 0x001A5167 File Offset: 0x001A3367
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupExpand.hot == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupExpand.hot = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupExpand.part, 2);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupExpand.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed expand button of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed expand button.</returns>
				// Token: 0x170019E4 RID: 6628
				// (get) Token: 0x060073BB RID: 29627 RVA: 0x001A518A File Offset: 0x001A338A
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupExpand.pressed == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupExpand.pressed = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupExpand.part, 3);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupExpand.pressed;
					}
				}

				// Token: 0x04004638 RID: 17976
				private static readonly int part = 11;

				// Token: 0x04004639 RID: 17977
				private static VisualStyleElement normal;

				// Token: 0x0400463A RID: 17978
				private static VisualStyleElement hot;

				// Token: 0x0400463B RID: 17979
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title bar of a special group of items in the Explorer Bar. This class cannot be inherited.</summary>
			// Token: 0x02000901 RID: 2305
			public static class SpecialGroupHead
			{
				/// <summary>Gets a visual style element that represents the title bar of a special group of items in the Explorer Bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a special group of items in the Explorer Bar.</returns>
				// Token: 0x170019E5 RID: 6629
				// (get) Token: 0x060073BD RID: 29629 RVA: 0x001A51B6 File Offset: 0x001A33B6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ExplorerBar.SpecialGroupHead.normal == null)
						{
							VisualStyleElement.ExplorerBar.SpecialGroupHead.normal = new VisualStyleElement(VisualStyleElement.ExplorerBar.className, VisualStyleElement.ExplorerBar.SpecialGroupHead.part, 0);
						}
						return VisualStyleElement.ExplorerBar.SpecialGroupHead.normal;
					}
				}

				// Token: 0x0400463C RID: 17980
				private static readonly int part = 12;

				// Token: 0x0400463D RID: 17981
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each part of the header control. This class cannot be inherited.</summary>
		// Token: 0x02000839 RID: 2105
		public static class Header
		{
			// Token: 0x04004364 RID: 17252
			private static readonly string className = "HEADER";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the header control. This class cannot be inherited.</summary>
			// Token: 0x02000902 RID: 2306
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a normal header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal header item.</returns>
				// Token: 0x170019E6 RID: 6630
				// (get) Token: 0x060073BF RID: 29631 RVA: 0x001A51E2 File Offset: 0x001A33E2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Header.Item.normal == null)
						{
							VisualStyleElement.Header.Item.normal = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.Item.part, 1);
						}
						return VisualStyleElement.Header.Item.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot header item.</returns>
				// Token: 0x170019E7 RID: 6631
				// (get) Token: 0x060073C0 RID: 29632 RVA: 0x001A5205 File Offset: 0x001A3405
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Header.Item.hot == null)
						{
							VisualStyleElement.Header.Item.hot = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.Item.part, 2);
						}
						return VisualStyleElement.Header.Item.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed header item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed header item.</returns>
				// Token: 0x170019E8 RID: 6632
				// (get) Token: 0x060073C1 RID: 29633 RVA: 0x001A5228 File Offset: 0x001A3428
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Header.Item.pressed == null)
						{
							VisualStyleElement.Header.Item.pressed = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.Item.part, 3);
						}
						return VisualStyleElement.Header.Item.pressed;
					}
				}

				// Token: 0x0400463E RID: 17982
				private static readonly int part = 1;

				// Token: 0x0400463F RID: 17983
				private static VisualStyleElement normal;

				// Token: 0x04004640 RID: 17984
				private static VisualStyleElement hot;

				// Token: 0x04004641 RID: 17985
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the leftmost item of the header control. This class cannot be inherited.</summary>
			// Token: 0x02000903 RID: 2307
			public static class ItemLeft
			{
				/// <summary>Gets a visual style element that represents the leftmost header item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the normal state.</returns>
				// Token: 0x170019E9 RID: 6633
				// (get) Token: 0x060073C3 RID: 29635 RVA: 0x001A5253 File Offset: 0x001A3453
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Header.ItemLeft.normal == null)
						{
							VisualStyleElement.Header.ItemLeft.normal = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemLeft.part, 1);
						}
						return VisualStyleElement.Header.ItemLeft.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the leftmost header item in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the hot state.</returns>
				// Token: 0x170019EA RID: 6634
				// (get) Token: 0x060073C4 RID: 29636 RVA: 0x001A5276 File Offset: 0x001A3476
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Header.ItemLeft.hot == null)
						{
							VisualStyleElement.Header.ItemLeft.hot = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemLeft.part, 2);
						}
						return VisualStyleElement.Header.ItemLeft.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the leftmost header item in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the leftmost header item in the pressed state.</returns>
				// Token: 0x170019EB RID: 6635
				// (get) Token: 0x060073C5 RID: 29637 RVA: 0x001A5299 File Offset: 0x001A3499
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Header.ItemLeft.pressed == null)
						{
							VisualStyleElement.Header.ItemLeft.pressed = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemLeft.part, 3);
						}
						return VisualStyleElement.Header.ItemLeft.pressed;
					}
				}

				// Token: 0x04004642 RID: 17986
				private static readonly int part = 2;

				// Token: 0x04004643 RID: 17987
				private static VisualStyleElement normal;

				// Token: 0x04004644 RID: 17988
				private static VisualStyleElement hot;

				// Token: 0x04004645 RID: 17989
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the rightmost item of the header control. This class cannot be inherited.</summary>
			// Token: 0x02000904 RID: 2308
			public static class ItemRight
			{
				/// <summary>Gets a visual style element that represents the rightmost header item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the normal state.</returns>
				// Token: 0x170019EC RID: 6636
				// (get) Token: 0x060073C7 RID: 29639 RVA: 0x001A52C4 File Offset: 0x001A34C4
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Header.ItemRight.normal == null)
						{
							VisualStyleElement.Header.ItemRight.normal = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemRight.part, 1);
						}
						return VisualStyleElement.Header.ItemRight.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the rightmost header item in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the hot state.</returns>
				// Token: 0x170019ED RID: 6637
				// (get) Token: 0x060073C8 RID: 29640 RVA: 0x001A52E7 File Offset: 0x001A34E7
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Header.ItemRight.hot == null)
						{
							VisualStyleElement.Header.ItemRight.hot = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemRight.part, 2);
						}
						return VisualStyleElement.Header.ItemRight.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the rightmost header item in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the rightmost header item in the pressed state.</returns>
				// Token: 0x170019EE RID: 6638
				// (get) Token: 0x060073C9 RID: 29641 RVA: 0x001A530A File Offset: 0x001A350A
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Header.ItemRight.pressed == null)
						{
							VisualStyleElement.Header.ItemRight.pressed = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.ItemRight.part, 3);
						}
						return VisualStyleElement.Header.ItemRight.pressed;
					}
				}

				// Token: 0x04004646 RID: 17990
				private static readonly int part = 3;

				// Token: 0x04004647 RID: 17991
				private static VisualStyleElement normal;

				// Token: 0x04004648 RID: 17992
				private static VisualStyleElement hot;

				// Token: 0x04004649 RID: 17993
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the sort arrow of a header item. This class cannot be inherited.</summary>
			// Token: 0x02000905 RID: 2309
			public static class SortArrow
			{
				/// <summary>Gets a visual style element that represents an upward-pointing sort arrow.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing sort arrow.</returns>
				// Token: 0x170019EF RID: 6639
				// (get) Token: 0x060073CB RID: 29643 RVA: 0x001A5335 File Offset: 0x001A3535
				public static VisualStyleElement SortedUp
				{
					get
					{
						if (VisualStyleElement.Header.SortArrow.sortedup == null)
						{
							VisualStyleElement.Header.SortArrow.sortedup = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.SortArrow.part, 1);
						}
						return VisualStyleElement.Header.SortArrow.sortedup;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing sort arrow.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing sort arrow.</returns>
				// Token: 0x170019F0 RID: 6640
				// (get) Token: 0x060073CC RID: 29644 RVA: 0x001A5358 File Offset: 0x001A3558
				public static VisualStyleElement SortedDown
				{
					get
					{
						if (VisualStyleElement.Header.SortArrow.sorteddown == null)
						{
							VisualStyleElement.Header.SortArrow.sorteddown = new VisualStyleElement(VisualStyleElement.Header.className, VisualStyleElement.Header.SortArrow.part, 2);
						}
						return VisualStyleElement.Header.SortArrow.sorteddown;
					}
				}

				// Token: 0x0400464A RID: 17994
				private static readonly int part = 4;

				// Token: 0x0400464B RID: 17995
				private static VisualStyleElement sortedup;

				// Token: 0x0400464C RID: 17996
				private static VisualStyleElement sorteddown;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the list view control. This class cannot be inherited.</summary>
		// Token: 0x0200083A RID: 2106
		public static class ListView
		{
			// Token: 0x04004365 RID: 17253
			private static readonly string className = "LISTVIEW";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of an item of the list view control. This class cannot be inherited.</summary>
			// Token: 0x02000906 RID: 2310
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a normal list view item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal list view item.</returns>
				// Token: 0x170019F1 RID: 6641
				// (get) Token: 0x060073CE RID: 29646 RVA: 0x001A5383 File Offset: 0x001A3583
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.Item.normal == null)
						{
							VisualStyleElement.ListView.Item.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 1);
						}
						return VisualStyleElement.ListView.Item.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot list view item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot list view item.</returns>
				// Token: 0x170019F2 RID: 6642
				// (get) Token: 0x060073CF RID: 29647 RVA: 0x001A53A6 File Offset: 0x001A35A6
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ListView.Item.hot == null)
						{
							VisualStyleElement.ListView.Item.hot = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 2);
						}
						return VisualStyleElement.ListView.Item.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a selected list view item that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected list view item that has focus.</returns>
				// Token: 0x170019F3 RID: 6643
				// (get) Token: 0x060073D0 RID: 29648 RVA: 0x001A53C9 File Offset: 0x001A35C9
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.ListView.Item.selected == null)
						{
							VisualStyleElement.ListView.Item.selected = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 3);
						}
						return VisualStyleElement.ListView.Item.selected;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled list view item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled list view item.</returns>
				// Token: 0x170019F4 RID: 6644
				// (get) Token: 0x060073D1 RID: 29649 RVA: 0x001A53EC File Offset: 0x001A35EC
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ListView.Item.disabled == null)
						{
							VisualStyleElement.ListView.Item.disabled = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 4);
						}
						return VisualStyleElement.ListView.Item.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a selected list view item without focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected list view item without focus.</returns>
				// Token: 0x170019F5 RID: 6645
				// (get) Token: 0x060073D2 RID: 29650 RVA: 0x001A540F File Offset: 0x001A360F
				public static VisualStyleElement SelectedNotFocus
				{
					get
					{
						if (VisualStyleElement.ListView.Item.selectednotfocus == null)
						{
							VisualStyleElement.ListView.Item.selectednotfocus = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Item.part, 5);
						}
						return VisualStyleElement.ListView.Item.selectednotfocus;
					}
				}

				// Token: 0x0400464D RID: 17997
				private static readonly int part = 1;

				// Token: 0x0400464E RID: 17998
				private static VisualStyleElement normal;

				// Token: 0x0400464F RID: 17999
				private static VisualStyleElement hot;

				// Token: 0x04004650 RID: 18000
				private static VisualStyleElement selected;

				// Token: 0x04004651 RID: 18001
				private static VisualStyleElement disabled;

				// Token: 0x04004652 RID: 18002
				private static VisualStyleElement selectednotfocus;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a list view item group. This class cannot be inherited.</summary>
			// Token: 0x02000907 RID: 2311
			public static class Group
			{
				/// <summary>Gets a visual style element that represents a list view item group.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a group of list view items.</returns>
				// Token: 0x170019F6 RID: 6646
				// (get) Token: 0x060073D4 RID: 29652 RVA: 0x001A543A File Offset: 0x001A363A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.Group.normal == null)
						{
							VisualStyleElement.ListView.Group.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Group.part, 0);
						}
						return VisualStyleElement.ListView.Group.normal;
					}
				}

				// Token: 0x04004653 RID: 18003
				private static readonly int part = 2;

				// Token: 0x04004654 RID: 18004
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a list view in detail view. This class cannot be inherited.</summary>
			// Token: 0x02000908 RID: 2312
			public static class Detail
			{
				/// <summary>Gets a visual style element that represents a list view in detail view.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a list view in detail view.</returns>
				// Token: 0x170019F7 RID: 6647
				// (get) Token: 0x060073D6 RID: 29654 RVA: 0x001A5465 File Offset: 0x001A3665
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.Detail.normal == null)
						{
							VisualStyleElement.ListView.Detail.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.Detail.part, 0);
						}
						return VisualStyleElement.ListView.Detail.normal;
					}
				}

				// Token: 0x04004655 RID: 18005
				private static readonly int part = 3;

				// Token: 0x04004656 RID: 18006
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a sorted list view control in detail view This class cannot be inherited.</summary>
			// Token: 0x02000909 RID: 2313
			public static class SortedDetail
			{
				/// <summary>Gets a visual style element that represents a sorted list view control in detail view.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a sorted list view control in detail view.</returns>
				// Token: 0x170019F8 RID: 6648
				// (get) Token: 0x060073D8 RID: 29656 RVA: 0x001A5490 File Offset: 0x001A3690
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.SortedDetail.normal == null)
						{
							VisualStyleElement.ListView.SortedDetail.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.SortedDetail.part, 0);
						}
						return VisualStyleElement.ListView.SortedDetail.normal;
					}
				}

				// Token: 0x04004657 RID: 18007
				private static readonly int part = 4;

				// Token: 0x04004658 RID: 18008
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the text area of a list view that contains no items. This class cannot be inherited.</summary>
			// Token: 0x0200090A RID: 2314
			public static class EmptyText
			{
				/// <summary>Gets a visual style element that represents the text area of a list view that contains no items.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the text area that accompanies an empty list view.</returns>
				// Token: 0x170019F9 RID: 6649
				// (get) Token: 0x060073DA RID: 29658 RVA: 0x001A54BB File Offset: 0x001A36BB
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ListView.EmptyText.normal == null)
						{
							VisualStyleElement.ListView.EmptyText.normal = new VisualStyleElement(VisualStyleElement.ListView.className, VisualStyleElement.ListView.EmptyText.part, 0);
						}
						return VisualStyleElement.ListView.EmptyText.normal;
					}
				}

				// Token: 0x04004659 RID: 18009
				private static readonly int part = 5;

				// Token: 0x0400465A RID: 18010
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a menu band. This class cannot be inherited.</summary>
		// Token: 0x0200083B RID: 2107
		public static class MenuBand
		{
			// Token: 0x04004366 RID: 17254
			private static readonly string className = "MENUBAND";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the new application button of a menu band. This class cannot be inherited.</summary>
			// Token: 0x0200090B RID: 2315
			public static class NewApplicationButton
			{
				/// <summary>Gets a visual style element that represents the new application button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the normal state.</returns>
				// Token: 0x170019FA RID: 6650
				// (get) Token: 0x060073DC RID: 29660 RVA: 0x001A54E6 File Offset: 0x001A36E6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.normal == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.normal = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 1);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the hot state.</returns>
				// Token: 0x170019FB RID: 6651
				// (get) Token: 0x060073DD RID: 29661 RVA: 0x001A5509 File Offset: 0x001A3709
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.hot == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.hot = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 2);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the pressed state.</returns>
				// Token: 0x170019FC RID: 6652
				// (get) Token: 0x060073DE RID: 29662 RVA: 0x001A552C File Offset: 0x001A372C
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.pressed == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.pressed = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 3);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the disabled state.</returns>
				// Token: 0x170019FD RID: 6653
				// (get) Token: 0x060073DF RID: 29663 RVA: 0x001A554F File Offset: 0x001A374F
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.disabled == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.disabled = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 4);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the checked state.</returns>
				// Token: 0x170019FE RID: 6654
				// (get) Token: 0x060073E0 RID: 29664 RVA: 0x001A5572 File Offset: 0x001A3772
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton._checked == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton._checked = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 5);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton._checked;
					}
				}

				/// <summary>Gets a visual style element that represents the new application button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the new application button in the hot and checked states.</returns>
				// Token: 0x170019FF RID: 6655
				// (get) Token: 0x060073E1 RID: 29665 RVA: 0x001A5595 File Offset: 0x001A3795
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.MenuBand.NewApplicationButton.hotchecked == null)
						{
							VisualStyleElement.MenuBand.NewApplicationButton.hotchecked = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.NewApplicationButton.part, 6);
						}
						return VisualStyleElement.MenuBand.NewApplicationButton.hotchecked;
					}
				}

				// Token: 0x0400465B RID: 18011
				private static readonly int part = 1;

				// Token: 0x0400465C RID: 18012
				private static VisualStyleElement normal;

				// Token: 0x0400465D RID: 18013
				private static VisualStyleElement hot;

				// Token: 0x0400465E RID: 18014
				private static VisualStyleElement pressed;

				// Token: 0x0400465F RID: 18015
				private static VisualStyleElement disabled;

				// Token: 0x04004660 RID: 18016
				private static VisualStyleElement _checked;

				// Token: 0x04004661 RID: 18017
				private static VisualStyleElement hotchecked;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu band separator. This class cannot be inherited.</summary>
			// Token: 0x0200090C RID: 2316
			public static class Separator
			{
				/// <summary>Gets a visual style element that represents a separator between items in a menu band.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a separator between items in a menu band.</returns>
				// Token: 0x17001A00 RID: 6656
				// (get) Token: 0x060073E3 RID: 29667 RVA: 0x001A55C0 File Offset: 0x001A37C0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.MenuBand.Separator.normal == null)
						{
							VisualStyleElement.MenuBand.Separator.normal = new VisualStyleElement(VisualStyleElement.MenuBand.className, VisualStyleElement.MenuBand.Separator.part, 0);
						}
						return VisualStyleElement.MenuBand.Separator.normal;
					}
				}

				// Token: 0x04004662 RID: 18018
				private static readonly int part = 2;

				// Token: 0x04004663 RID: 18019
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a menu. This class cannot be inherited.</summary>
		// Token: 0x0200083C RID: 2108
		public static class Menu
		{
			// Token: 0x04004367 RID: 17255
			private static readonly string className = "MENU";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a menu item. This class cannot be inherited.</summary>
			// Token: 0x0200090D RID: 2317
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a menu item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item in the normal state.</returns>
				// Token: 0x17001A01 RID: 6657
				// (get) Token: 0x060073E5 RID: 29669 RVA: 0x001A55EB File Offset: 0x001A37EB
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.Item.normal == null)
						{
							VisualStyleElement.Menu.Item.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Item.part, 1);
						}
						return VisualStyleElement.Menu.Item.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a menu item in the selected state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item in the selected state.</returns>
				// Token: 0x17001A02 RID: 6658
				// (get) Token: 0x060073E6 RID: 29670 RVA: 0x001A560E File Offset: 0x001A380E
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.Menu.Item.selected == null)
						{
							VisualStyleElement.Menu.Item.selected = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Item.part, 2);
						}
						return VisualStyleElement.Menu.Item.selected;
					}
				}

				/// <summary>Gets a visual style element that represents a menu item that has been demoted.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item that has been demoted.</returns>
				// Token: 0x17001A03 RID: 6659
				// (get) Token: 0x060073E7 RID: 29671 RVA: 0x001A5631 File Offset: 0x001A3831
				public static VisualStyleElement Demoted
				{
					get
					{
						if (VisualStyleElement.Menu.Item.demoted == null)
						{
							VisualStyleElement.Menu.Item.demoted = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Item.part, 3);
						}
						return VisualStyleElement.Menu.Item.demoted;
					}
				}

				// Token: 0x04004664 RID: 18020
				private static readonly int part = 1;

				// Token: 0x04004665 RID: 18021
				private static VisualStyleElement normal;

				// Token: 0x04004666 RID: 18022
				private static VisualStyleElement selected;

				// Token: 0x04004667 RID: 18023
				private static VisualStyleElement demoted;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the drop-down arrow of a menu. This class cannot be inherited.</summary>
			// Token: 0x0200090E RID: 2318
			public static class DropDown
			{
				/// <summary>Gets a visual style element that represents the drop-down arrow of a menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the drop-down arrow of a menu.</returns>
				// Token: 0x17001A04 RID: 6660
				// (get) Token: 0x060073E9 RID: 29673 RVA: 0x001A565C File Offset: 0x001A385C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.DropDown.normal == null)
						{
							VisualStyleElement.Menu.DropDown.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.DropDown.part, 0);
						}
						return VisualStyleElement.Menu.DropDown.normal;
					}
				}

				// Token: 0x04004668 RID: 18024
				private static readonly int part = 2;

				// Token: 0x04004669 RID: 18025
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu bar item. This class cannot be inherited.</summary>
			// Token: 0x0200090F RID: 2319
			public static class BarItem
			{
				/// <summary>Gets a visual style element that represents a menu bar item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu bar item.</returns>
				// Token: 0x17001A05 RID: 6661
				// (get) Token: 0x060073EB RID: 29675 RVA: 0x001A5687 File Offset: 0x001A3887
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.BarItem.normal == null)
						{
							VisualStyleElement.Menu.BarItem.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.BarItem.part, 0);
						}
						return VisualStyleElement.Menu.BarItem.normal;
					}
				}

				// Token: 0x0400466A RID: 18026
				private static readonly int part = 3;

				// Token: 0x0400466B RID: 18027
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the drop-down arrow of a menu bar. This class cannot be inherited.</summary>
			// Token: 0x02000910 RID: 2320
			public static class BarDropDown
			{
				/// <summary>Gets a visual style element that represents the drop-down arrow of a menu bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the drop-down arrow of a menu bar.</returns>
				// Token: 0x17001A06 RID: 6662
				// (get) Token: 0x060073ED RID: 29677 RVA: 0x001A56B2 File Offset: 0x001A38B2
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.BarDropDown.normal == null)
						{
							VisualStyleElement.Menu.BarDropDown.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.BarDropDown.part, 0);
						}
						return VisualStyleElement.Menu.BarDropDown.normal;
					}
				}

				// Token: 0x0400466C RID: 18028
				private static readonly int part = 4;

				// Token: 0x0400466D RID: 18029
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the chevron of a menu. This class cannot be inherited.</summary>
			// Token: 0x02000911 RID: 2321
			public static class Chevron
			{
				/// <summary>Gets a visual style element that represents a menu chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu chevron.</returns>
				// Token: 0x17001A07 RID: 6663
				// (get) Token: 0x060073EF RID: 29679 RVA: 0x001A56DD File Offset: 0x001A38DD
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.Chevron.normal == null)
						{
							VisualStyleElement.Menu.Chevron.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Chevron.part, 0);
						}
						return VisualStyleElement.Menu.Chevron.normal;
					}
				}

				// Token: 0x0400466E RID: 18030
				private static readonly int part = 5;

				// Token: 0x0400466F RID: 18031
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a menu item separator. This class cannot be inherited.</summary>
			// Token: 0x02000912 RID: 2322
			public static class Separator
			{
				/// <summary>Gets a visual style element that represents a menu item separator.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a menu item separator.</returns>
				// Token: 0x17001A08 RID: 6664
				// (get) Token: 0x060073F1 RID: 29681 RVA: 0x001A5708 File Offset: 0x001A3908
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Menu.Separator.normal == null)
						{
							VisualStyleElement.Menu.Separator.normal = new VisualStyleElement(VisualStyleElement.Menu.className, VisualStyleElement.Menu.Separator.part, 0);
						}
						return VisualStyleElement.Menu.Separator.normal;
					}
				}

				// Token: 0x04004670 RID: 18032
				private static readonly int part = 6;

				// Token: 0x04004671 RID: 18033
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the progress bar control. This class cannot be inherited.</summary>
		// Token: 0x0200083D RID: 2109
		public static class ProgressBar
		{
			// Token: 0x04004368 RID: 17256
			private static readonly string className = "PROGRESS";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the frame of a horizontal progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000913 RID: 2323
			public static class Bar
			{
				/// <summary>Gets a visual style element that represents a horizontal progress bar frame.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal progress bar frame.</returns>
				// Token: 0x17001A09 RID: 6665
				// (get) Token: 0x060073F3 RID: 29683 RVA: 0x001A5733 File Offset: 0x001A3933
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.Bar.normal == null)
						{
							VisualStyleElement.ProgressBar.Bar.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.Bar.part, 0);
						}
						return VisualStyleElement.ProgressBar.Bar.normal;
					}
				}

				// Token: 0x04004672 RID: 18034
				private static readonly int part = 1;

				// Token: 0x04004673 RID: 18035
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the frame of a vertical progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000914 RID: 2324
			public static class BarVertical
			{
				/// <summary>Gets a visual style element that represents a vertical progress bar frame.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical progress bar frame.</returns>
				// Token: 0x17001A0A RID: 6666
				// (get) Token: 0x060073F5 RID: 29685 RVA: 0x001A575E File Offset: 0x001A395E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.BarVertical.normal == null)
						{
							VisualStyleElement.ProgressBar.BarVertical.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.BarVertical.part, 0);
						}
						return VisualStyleElement.ProgressBar.BarVertical.normal;
					}
				}

				// Token: 0x04004674 RID: 18036
				private static readonly int part = 2;

				// Token: 0x04004675 RID: 18037
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pieces that fill a horizontal progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000915 RID: 2325
			public static class Chunk
			{
				/// <summary>Gets a visual style element that represents the pieces that fill a horizontal progress bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the pieces that fill a horizontal progress bar.</returns>
				// Token: 0x17001A0B RID: 6667
				// (get) Token: 0x060073F7 RID: 29687 RVA: 0x001A5789 File Offset: 0x001A3989
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.Chunk.normal == null)
						{
							VisualStyleElement.ProgressBar.Chunk.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.Chunk.part, 0);
						}
						return VisualStyleElement.ProgressBar.Chunk.normal;
					}
				}

				// Token: 0x04004676 RID: 18038
				private static readonly int part = 3;

				// Token: 0x04004677 RID: 18039
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pieces that fill a vertical progress bar. This class cannot be inherited.</summary>
			// Token: 0x02000916 RID: 2326
			public static class ChunkVertical
			{
				/// <summary>Gets a visual style element that represents the pieces that fill a vertical progress bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the pieces that fill a vertical progress bar.</returns>
				// Token: 0x17001A0C RID: 6668
				// (get) Token: 0x060073F9 RID: 29689 RVA: 0x001A57B4 File Offset: 0x001A39B4
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ProgressBar.ChunkVertical.normal == null)
						{
							VisualStyleElement.ProgressBar.ChunkVertical.normal = new VisualStyleElement(VisualStyleElement.ProgressBar.className, VisualStyleElement.ProgressBar.ChunkVertical.part, 0);
						}
						return VisualStyleElement.ProgressBar.ChunkVertical.normal;
					}
				}

				// Token: 0x04004678 RID: 18040
				private static readonly int part = 4;

				// Token: 0x04004679 RID: 18041
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the rebar control. This class cannot be inherited.</summary>
		// Token: 0x0200083E RID: 2110
		public static class Rebar
		{
			// Token: 0x04004369 RID: 17257
			private static readonly string className = "REBAR";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the gripper bar of a horizontal rebar control. This class cannot be inherited.</summary>
			// Token: 0x02000917 RID: 2327
			public static class Gripper
			{
				/// <summary>Gets a visual style element that represents a gripper bar for a horizontal rebar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a gripper bar for a horizontal rebar.</returns>
				// Token: 0x17001A0D RID: 6669
				// (get) Token: 0x060073FB RID: 29691 RVA: 0x001A57DF File Offset: 0x001A39DF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.Gripper.normal == null)
						{
							VisualStyleElement.Rebar.Gripper.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Gripper.part, 0);
						}
						return VisualStyleElement.Rebar.Gripper.normal;
					}
				}

				// Token: 0x0400467A RID: 18042
				private static readonly int part = 1;

				// Token: 0x0400467B RID: 18043
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the gripper bar of a vertical rebar. This class cannot be inherited.</summary>
			// Token: 0x02000918 RID: 2328
			public static class GripperVertical
			{
				/// <summary>Gets a visual style element that represents a gripper bar for a vertical rebar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a gripper bar for a vertical rebar.</returns>
				// Token: 0x17001A0E RID: 6670
				// (get) Token: 0x060073FD RID: 29693 RVA: 0x001A580A File Offset: 0x001A3A0A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.GripperVertical.normal == null)
						{
							VisualStyleElement.Rebar.GripperVertical.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.GripperVertical.part, 0);
						}
						return VisualStyleElement.Rebar.GripperVertical.normal;
					}
				}

				// Token: 0x0400467C RID: 18044
				private static readonly int part = 2;

				// Token: 0x0400467D RID: 18045
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a rebar band. This class cannot be inherited.</summary>
			// Token: 0x02000919 RID: 2329
			public static class Band
			{
				/// <summary>Gets a visual style element that represents a rebar band.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a rebar band.</returns>
				// Token: 0x17001A0F RID: 6671
				// (get) Token: 0x060073FF RID: 29695 RVA: 0x001A5835 File Offset: 0x001A3A35
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.Band.normal == null)
						{
							VisualStyleElement.Rebar.Band.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Band.part, 0);
						}
						return VisualStyleElement.Rebar.Band.normal;
					}
				}

				// Token: 0x0400467E RID: 18046
				private static readonly int part = 3;

				// Token: 0x0400467F RID: 18047
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a horizontal chevron. This class cannot be inherited.</summary>
			// Token: 0x0200091A RID: 2330
			public static class Chevron
			{
				/// <summary>Gets a visual style element that represents a normal chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal chevron.</returns>
				// Token: 0x17001A10 RID: 6672
				// (get) Token: 0x06007401 RID: 29697 RVA: 0x001A5860 File Offset: 0x001A3A60
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.Chevron.normal == null)
						{
							VisualStyleElement.Rebar.Chevron.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Chevron.part, 1);
						}
						return VisualStyleElement.Rebar.Chevron.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot chevron.</returns>
				// Token: 0x17001A11 RID: 6673
				// (get) Token: 0x06007402 RID: 29698 RVA: 0x001A5883 File Offset: 0x001A3A83
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Rebar.Chevron.hot == null)
						{
							VisualStyleElement.Rebar.Chevron.hot = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Chevron.part, 2);
						}
						return VisualStyleElement.Rebar.Chevron.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed chevron.</returns>
				// Token: 0x17001A12 RID: 6674
				// (get) Token: 0x06007403 RID: 29699 RVA: 0x001A58A6 File Offset: 0x001A3AA6
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Rebar.Chevron.pressed == null)
						{
							VisualStyleElement.Rebar.Chevron.pressed = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.Chevron.part, 3);
						}
						return VisualStyleElement.Rebar.Chevron.pressed;
					}
				}

				// Token: 0x04004680 RID: 18048
				private static readonly int part = 4;

				// Token: 0x04004681 RID: 18049
				private static VisualStyleElement normal;

				// Token: 0x04004682 RID: 18050
				private static VisualStyleElement hot;

				// Token: 0x04004683 RID: 18051
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a chevron. This class cannot be inherited.</summary>
			// Token: 0x0200091B RID: 2331
			public static class ChevronVertical
			{
				/// <summary>Gets a visual style element that represents a normal chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal chevron.</returns>
				// Token: 0x17001A13 RID: 6675
				// (get) Token: 0x06007405 RID: 29701 RVA: 0x001A58D1 File Offset: 0x001A3AD1
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Rebar.ChevronVertical.normal == null)
						{
							VisualStyleElement.Rebar.ChevronVertical.normal = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.ChevronVertical.part, 1);
						}
						return VisualStyleElement.Rebar.ChevronVertical.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot chevron.</returns>
				// Token: 0x17001A14 RID: 6676
				// (get) Token: 0x06007406 RID: 29702 RVA: 0x001A58F4 File Offset: 0x001A3AF4
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Rebar.ChevronVertical.hot == null)
						{
							VisualStyleElement.Rebar.ChevronVertical.hot = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.ChevronVertical.part, 2);
						}
						return VisualStyleElement.Rebar.ChevronVertical.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a pressed chevron.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pressed chevron.</returns>
				// Token: 0x17001A15 RID: 6677
				// (get) Token: 0x06007407 RID: 29703 RVA: 0x001A5917 File Offset: 0x001A3B17
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Rebar.ChevronVertical.pressed == null)
						{
							VisualStyleElement.Rebar.ChevronVertical.pressed = new VisualStyleElement(VisualStyleElement.Rebar.className, VisualStyleElement.Rebar.ChevronVertical.part, 3);
						}
						return VisualStyleElement.Rebar.ChevronVertical.pressed;
					}
				}

				// Token: 0x04004684 RID: 18052
				private static readonly int part = 5;

				// Token: 0x04004685 RID: 18053
				private static VisualStyleElement normal;

				// Token: 0x04004686 RID: 18054
				private static VisualStyleElement hot;

				// Token: 0x04004687 RID: 18055
				private static VisualStyleElement pressed;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the Start menu. This class cannot be inherited.</summary>
		// Token: 0x0200083F RID: 2111
		public static class StartPanel
		{
			// Token: 0x0400436A RID: 17258
			private static readonly string className = "STARTPANEL";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the top border of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x0200091C RID: 2332
			public static class UserPane
			{
				/// <summary>Gets a visual style element that represents the top border of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the top border of the Start menu.</returns>
				// Token: 0x17001A16 RID: 6678
				// (get) Token: 0x06007409 RID: 29705 RVA: 0x001A5942 File Offset: 0x001A3B42
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.UserPane.normal == null)
						{
							VisualStyleElement.StartPanel.UserPane.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.UserPane.part, 0);
						}
						return VisualStyleElement.StartPanel.UserPane.normal;
					}
				}

				// Token: 0x04004688 RID: 18056
				private static readonly int part = 1;

				// Token: 0x04004689 RID: 18057
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the All Programs item in the Start menu. This class cannot be inherited.</summary>
			// Token: 0x0200091D RID: 2333
			public static class MorePrograms
			{
				/// <summary>Gets a visual style element that represents the background of the All Programs menu item.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the All Programs menu item.</returns>
				// Token: 0x17001A17 RID: 6679
				// (get) Token: 0x0600740B RID: 29707 RVA: 0x001A596D File Offset: 0x001A3B6D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.MorePrograms.normal == null)
						{
							VisualStyleElement.StartPanel.MorePrograms.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MorePrograms.part, 0);
						}
						return VisualStyleElement.StartPanel.MorePrograms.normal;
					}
				}

				// Token: 0x0400468A RID: 18058
				private static readonly int part = 2;

				// Token: 0x0400468B RID: 18059
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the All Programs arrow in the Start menu. This class cannot be inherited.</summary>
			// Token: 0x0200091E RID: 2334
			public static class MoreProgramsArrow
			{
				/// <summary>Gets a visual style element that represents the All Programs arrow in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the normal state.</returns>
				// Token: 0x17001A18 RID: 6680
				// (get) Token: 0x0600740D RID: 29709 RVA: 0x001A5998 File Offset: 0x001A3B98
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.MoreProgramsArrow.normal == null)
						{
							VisualStyleElement.StartPanel.MoreProgramsArrow.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MoreProgramsArrow.part, 1);
						}
						return VisualStyleElement.StartPanel.MoreProgramsArrow.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the All Programs arrow in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the hot state.</returns>
				// Token: 0x17001A19 RID: 6681
				// (get) Token: 0x0600740E RID: 29710 RVA: 0x001A59BB File Offset: 0x001A3BBB
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.StartPanel.MoreProgramsArrow.hot == null)
						{
							VisualStyleElement.StartPanel.MoreProgramsArrow.hot = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MoreProgramsArrow.part, 2);
						}
						return VisualStyleElement.StartPanel.MoreProgramsArrow.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the All Programs arrow in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the All Programs arrow in the pressed state.</returns>
				// Token: 0x17001A1A RID: 6682
				// (get) Token: 0x0600740F RID: 29711 RVA: 0x001A59DE File Offset: 0x001A3BDE
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.StartPanel.MoreProgramsArrow.pressed == null)
						{
							VisualStyleElement.StartPanel.MoreProgramsArrow.pressed = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.MoreProgramsArrow.part, 3);
						}
						return VisualStyleElement.StartPanel.MoreProgramsArrow.pressed;
					}
				}

				// Token: 0x0400468C RID: 18060
				private static readonly int part = 3;

				// Token: 0x0400468D RID: 18061
				private static VisualStyleElement normal;

				// Token: 0x0400468E RID: 18062
				private static VisualStyleElement hot;

				// Token: 0x0400468F RID: 18063
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the left side of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x0200091F RID: 2335
			public static class ProgList
			{
				/// <summary>Gets a visual style element that represents the background of the left side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the left side of the Start menu.</returns>
				// Token: 0x17001A1B RID: 6683
				// (get) Token: 0x06007411 RID: 29713 RVA: 0x001A5A09 File Offset: 0x001A3C09
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.ProgList.normal == null)
						{
							VisualStyleElement.StartPanel.ProgList.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.ProgList.part, 0);
						}
						return VisualStyleElement.StartPanel.ProgList.normal;
					}
				}

				// Token: 0x04004690 RID: 18064
				private static readonly int part = 4;

				// Token: 0x04004691 RID: 18065
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bar that separates groups of items in the left side of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000920 RID: 2336
			public static class ProgListSeparator
			{
				/// <summary>Gets a visual style element that represents the bar that separates groups of items in the left side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bar that separates groups of items in the left side of the Start menu.</returns>
				// Token: 0x17001A1C RID: 6684
				// (get) Token: 0x06007413 RID: 29715 RVA: 0x001A5A34 File Offset: 0x001A3C34
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.ProgListSeparator.normal == null)
						{
							VisualStyleElement.StartPanel.ProgListSeparator.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.ProgListSeparator.part, 0);
						}
						return VisualStyleElement.StartPanel.ProgListSeparator.normal;
					}
				}

				// Token: 0x04004692 RID: 18066
				private static readonly int part = 5;

				// Token: 0x04004693 RID: 18067
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the right side of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000921 RID: 2337
			public static class PlaceList
			{
				/// <summary>Gets a visual style element that represents the background of the right side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the right side of the Start menu.</returns>
				// Token: 0x17001A1D RID: 6685
				// (get) Token: 0x06007415 RID: 29717 RVA: 0x001A5A5F File Offset: 0x001A3C5F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.PlaceList.normal == null)
						{
							VisualStyleElement.StartPanel.PlaceList.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.PlaceList.part, 0);
						}
						return VisualStyleElement.StartPanel.PlaceList.normal;
					}
				}

				// Token: 0x04004694 RID: 18068
				private static readonly int part = 6;

				// Token: 0x04004695 RID: 18069
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bar that separates groups of items in the right side of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000922 RID: 2338
			public static class PlaceListSeparator
			{
				/// <summary>Gets a visual style element that represents the bar that separates groups of items in the right side of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bar that separates groups of items in the right side of the Start menu.</returns>
				// Token: 0x17001A1E RID: 6686
				// (get) Token: 0x06007417 RID: 29719 RVA: 0x001A5A8A File Offset: 0x001A3C8A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.PlaceListSeparator.normal == null)
						{
							VisualStyleElement.StartPanel.PlaceListSeparator.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.PlaceListSeparator.part, 0);
						}
						return VisualStyleElement.StartPanel.PlaceListSeparator.normal;
					}
				}

				// Token: 0x04004696 RID: 18070
				private static readonly int part = 7;

				// Token: 0x04004697 RID: 18071
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the bottom border of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000923 RID: 2339
			public static class LogOff
			{
				/// <summary>Gets a visual style element that represents the bottom border of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of the Start menu.</returns>
				// Token: 0x17001A1F RID: 6687
				// (get) Token: 0x06007419 RID: 29721 RVA: 0x001A5AB5 File Offset: 0x001A3CB5
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOff.normal == null)
						{
							VisualStyleElement.StartPanel.LogOff.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOff.part, 0);
						}
						return VisualStyleElement.StartPanel.LogOff.normal;
					}
				}

				// Token: 0x04004698 RID: 18072
				private static readonly int part = 8;

				// Token: 0x04004699 RID: 18073
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Log Off and Shut Down buttons in the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000924 RID: 2340
			public static class LogOffButtons
			{
				/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the normal state.</returns>
				// Token: 0x17001A20 RID: 6688
				// (get) Token: 0x0600741B RID: 29723 RVA: 0x001A5AE0 File Offset: 0x001A3CE0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOffButtons.normal == null)
						{
							VisualStyleElement.StartPanel.LogOffButtons.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOffButtons.part, 1);
						}
						return VisualStyleElement.StartPanel.LogOffButtons.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the hot state.</returns>
				// Token: 0x17001A21 RID: 6689
				// (get) Token: 0x0600741C RID: 29724 RVA: 0x001A5B03 File Offset: 0x001A3D03
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOffButtons.hot == null)
						{
							VisualStyleElement.StartPanel.LogOffButtons.hot = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOffButtons.part, 2);
						}
						return VisualStyleElement.StartPanel.LogOffButtons.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the Log Off and Shut Down buttons in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Log Off and Shut Down buttons in the pressed state.</returns>
				// Token: 0x17001A22 RID: 6690
				// (get) Token: 0x0600741D RID: 29725 RVA: 0x001A5B26 File Offset: 0x001A3D26
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.StartPanel.LogOffButtons.pressed == null)
						{
							VisualStyleElement.StartPanel.LogOffButtons.pressed = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.LogOffButtons.part, 3);
						}
						return VisualStyleElement.StartPanel.LogOffButtons.pressed;
					}
				}

				// Token: 0x0400469A RID: 18074
				private static readonly int part = 9;

				// Token: 0x0400469B RID: 18075
				private static VisualStyleElement normal;

				// Token: 0x0400469C RID: 18076
				private static VisualStyleElement hot;

				// Token: 0x0400469D RID: 18077
				private static VisualStyleElement pressed;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the user picture on the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000925 RID: 2341
			public static class UserPicture
			{
				/// <summary>Gets a visual style element that represents the background of the user picture on the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the user picture on the Start menu.</returns>
				// Token: 0x17001A23 RID: 6691
				// (get) Token: 0x0600741F RID: 29727 RVA: 0x001A5B52 File Offset: 0x001A3D52
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.UserPicture.normal == null)
						{
							VisualStyleElement.StartPanel.UserPicture.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.UserPicture.part, 0);
						}
						return VisualStyleElement.StartPanel.UserPicture.normal;
					}
				}

				// Token: 0x0400469E RID: 18078
				private static readonly int part = 10;

				// Token: 0x0400469F RID: 18079
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the preview area of the Start menu. This class cannot be inherited.</summary>
			// Token: 0x02000926 RID: 2342
			public static class Preview
			{
				/// <summary>Gets a visual style element that represents the preview area of the Start menu.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the preview area of the Start menu.</returns>
				// Token: 0x17001A24 RID: 6692
				// (get) Token: 0x06007421 RID: 29729 RVA: 0x001A5B7E File Offset: 0x001A3D7E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.StartPanel.Preview.normal == null)
						{
							VisualStyleElement.StartPanel.Preview.normal = new VisualStyleElement(VisualStyleElement.StartPanel.className, VisualStyleElement.StartPanel.Preview.part, 0);
						}
						return VisualStyleElement.StartPanel.Preview.normal;
					}
				}

				// Token: 0x040046A0 RID: 18080
				private static readonly int part = 11;

				// Token: 0x040046A1 RID: 18081
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the status bar. This class cannot be inherited.</summary>
		// Token: 0x02000840 RID: 2112
		public static class Status
		{
			// Token: 0x0400436B RID: 17259
			private static readonly string className = "STATUS";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the status bar. This class cannot be inherited.</summary>
			// Token: 0x02000927 RID: 2343
			public static class Bar
			{
				/// <summary>Gets a visual style element that represents the background of the status bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the status bar.</returns>
				// Token: 0x17001A25 RID: 6693
				// (get) Token: 0x06007423 RID: 29731 RVA: 0x001A5BAA File Offset: 0x001A3DAA
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.Bar.normal == null)
						{
							VisualStyleElement.Status.Bar.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.Bar.part, 0);
						}
						return VisualStyleElement.Status.Bar.normal;
					}
				}

				// Token: 0x040046A2 RID: 18082
				private static readonly int part;

				// Token: 0x040046A3 RID: 18083
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a status bar pane. This class cannot be inherited.</summary>
			// Token: 0x02000928 RID: 2344
			public static class Pane
			{
				/// <summary>Gets a visual style element that represents a status bar pane.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a status bar pane.</returns>
				// Token: 0x17001A26 RID: 6694
				// (get) Token: 0x06007424 RID: 29732 RVA: 0x001A5BCD File Offset: 0x001A3DCD
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.Pane.normal == null)
						{
							VisualStyleElement.Status.Pane.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.Pane.part, 0);
						}
						return VisualStyleElement.Status.Pane.normal;
					}
				}

				// Token: 0x040046A4 RID: 18084
				private static readonly int part = 1;

				// Token: 0x040046A5 RID: 18085
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the pane of the grip in the status bar. This class cannot be inherited.</summary>
			// Token: 0x02000929 RID: 2345
			public static class GripperPane
			{
				/// <summary>Gets a visual style element that represents a pane for the status bar grip.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a pane for the status bar grip.</returns>
				// Token: 0x17001A27 RID: 6695
				// (get) Token: 0x06007426 RID: 29734 RVA: 0x001A5BF8 File Offset: 0x001A3DF8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.GripperPane.normal == null)
						{
							VisualStyleElement.Status.GripperPane.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.GripperPane.part, 0);
						}
						return VisualStyleElement.Status.GripperPane.normal;
					}
				}

				// Token: 0x040046A6 RID: 18086
				private static readonly int part = 2;

				// Token: 0x040046A7 RID: 18087
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the grip of the status bar. This class cannot be inherited.</summary>
			// Token: 0x0200092A RID: 2346
			public static class Gripper
			{
				/// <summary>Gets a visual style element that represents the status bar grip.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the status bar grip.</returns>
				// Token: 0x17001A28 RID: 6696
				// (get) Token: 0x06007428 RID: 29736 RVA: 0x001A5C23 File Offset: 0x001A3E23
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Status.Gripper.normal == null)
						{
							VisualStyleElement.Status.Gripper.normal = new VisualStyleElement(VisualStyleElement.Status.className, VisualStyleElement.Status.Gripper.part, 0);
						}
						return VisualStyleElement.Status.Gripper.normal;
					}
				}

				// Token: 0x040046A8 RID: 18088
				private static readonly int part = 3;

				// Token: 0x040046A9 RID: 18089
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for parts of the taskbar. This class cannot be inherited.</summary>
		// Token: 0x02000841 RID: 2113
		public static class TaskBand
		{
			// Token: 0x0400436C RID: 17260
			private static readonly string className = "TASKBAND";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a group counter of the taskbar. This class cannot be inherited.</summary>
			// Token: 0x0200092B RID: 2347
			public static class GroupCount
			{
				/// <summary>Gets a visual style element that represents a group counter for the taskbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a group counter for the taskbar.</returns>
				// Token: 0x17001A29 RID: 6697
				// (get) Token: 0x0600742A RID: 29738 RVA: 0x001A5C4E File Offset: 0x001A3E4E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskBand.GroupCount.normal == null)
						{
							VisualStyleElement.TaskBand.GroupCount.normal = new VisualStyleElement(VisualStyleElement.TaskBand.className, VisualStyleElement.TaskBand.GroupCount.part, 0);
						}
						return VisualStyleElement.TaskBand.GroupCount.normal;
					}
				}

				// Token: 0x040046AA RID: 18090
				private static readonly int part = 1;

				// Token: 0x040046AB RID: 18091
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a flashing window button in the taskbar. This class cannot be inherited.</summary>
			// Token: 0x0200092C RID: 2348
			public static class FlashButton
			{
				/// <summary>Gets a visual style element that represents a flashing window button in the taskbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a flashing window button in the taskbar.</returns>
				// Token: 0x17001A2A RID: 6698
				// (get) Token: 0x0600742C RID: 29740 RVA: 0x001A5C79 File Offset: 0x001A3E79
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskBand.FlashButton.normal == null)
						{
							VisualStyleElement.TaskBand.FlashButton.normal = new VisualStyleElement(VisualStyleElement.TaskBand.className, VisualStyleElement.TaskBand.FlashButton.part, 0);
						}
						return VisualStyleElement.TaskBand.FlashButton.normal;
					}
				}

				// Token: 0x040046AC RID: 18092
				private static readonly int part = 2;

				// Token: 0x040046AD RID: 18093
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a flashing menu item of a window button in the taskbar. This class cannot be inherited.</summary>
			// Token: 0x0200092D RID: 2349
			public static class FlashButtonGroupMenu
			{
				/// <summary>Gets a visual style element that represents a flashing menu item for a window button in the taskbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a flashing menu item for a window button in the taskbar.</returns>
				// Token: 0x17001A2B RID: 6699
				// (get) Token: 0x0600742E RID: 29742 RVA: 0x001A5CA4 File Offset: 0x001A3EA4
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskBand.FlashButtonGroupMenu.normal == null)
						{
							VisualStyleElement.TaskBand.FlashButtonGroupMenu.normal = new VisualStyleElement(VisualStyleElement.TaskBand.className, VisualStyleElement.TaskBand.FlashButtonGroupMenu.part, 0);
						}
						return VisualStyleElement.TaskBand.FlashButtonGroupMenu.normal;
					}
				}

				// Token: 0x040046AE RID: 18094
				private static readonly int part = 3;

				// Token: 0x040046AF RID: 18095
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains a class that provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the taskbar clock. This class cannot be inherited.</summary>
		// Token: 0x02000842 RID: 2114
		public static class TaskbarClock
		{
			// Token: 0x0400436D RID: 17261
			private static readonly string className = "CLOCK";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the taskbar clock. This class cannot be inherited.</summary>
			// Token: 0x0200092E RID: 2350
			public static class Time
			{
				/// <summary>Gets a visual style element that represents the background of the taskbar clock.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the taskbar clock.</returns>
				// Token: 0x17001A2C RID: 6700
				// (get) Token: 0x06007430 RID: 29744 RVA: 0x001A5CCF File Offset: 0x001A3ECF
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TaskbarClock.Time.normal == null)
						{
							VisualStyleElement.TaskbarClock.Time.normal = new VisualStyleElement(VisualStyleElement.TaskbarClock.className, VisualStyleElement.TaskbarClock.Time.part, 1);
						}
						return VisualStyleElement.TaskbarClock.Time.normal;
					}
				}

				// Token: 0x040046B0 RID: 18096
				private static readonly int part = 1;

				// Token: 0x040046B1 RID: 18097
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the taskbar. This class cannot be inherited.</summary>
		// Token: 0x02000843 RID: 2115
		public static class Taskbar
		{
			// Token: 0x0400436E RID: 17262
			private static readonly string className = "TASKBAR";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the bottom of the screen. This class cannot be inherited.</summary>
			// Token: 0x0200092F RID: 2351
			public static class BackgroundBottom
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the bottom of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the bottom of the screen.</returns>
				// Token: 0x17001A2D RID: 6701
				// (get) Token: 0x06007432 RID: 29746 RVA: 0x001A5CFA File Offset: 0x001A3EFA
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundBottom.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundBottom.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundBottom.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundBottom.normal;
					}
				}

				// Token: 0x040046B2 RID: 18098
				private static readonly int part = 1;

				// Token: 0x040046B3 RID: 18099
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the right side of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000930 RID: 2352
			public static class BackgroundRight
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the right side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the right side of the screen.</returns>
				// Token: 0x17001A2E RID: 6702
				// (get) Token: 0x06007434 RID: 29748 RVA: 0x001A5D25 File Offset: 0x001A3F25
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundRight.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundRight.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundRight.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundRight.normal;
					}
				}

				// Token: 0x040046B4 RID: 18100
				private static readonly int part = 2;

				// Token: 0x040046B5 RID: 18101
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the top of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000931 RID: 2353
			public static class BackgroundTop
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the top of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the top of the screen.</returns>
				// Token: 0x17001A2F RID: 6703
				// (get) Token: 0x06007436 RID: 29750 RVA: 0x001A5D50 File Offset: 0x001A3F50
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundTop.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundTop.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundTop.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundTop.normal;
					}
				}

				// Token: 0x040046B6 RID: 18102
				private static readonly int part = 3;

				// Token: 0x040046B7 RID: 18103
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a taskbar that is docked on the left side of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000932 RID: 2354
			public static class BackgroundLeft
			{
				/// <summary>Gets a visual style element that represents the background of a taskbar that is docked on the left side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a taskbar that is docked on the left side of the screen.</returns>
				// Token: 0x17001A30 RID: 6704
				// (get) Token: 0x06007438 RID: 29752 RVA: 0x001A5D7B File Offset: 0x001A3F7B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.BackgroundLeft.normal == null)
						{
							VisualStyleElement.Taskbar.BackgroundLeft.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.BackgroundLeft.part, 0);
						}
						return VisualStyleElement.Taskbar.BackgroundLeft.normal;
					}
				}

				// Token: 0x040046B8 RID: 18104
				private static readonly int part = 4;

				// Token: 0x040046B9 RID: 18105
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the bottom of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000933 RID: 2355
			public static class SizingBarBottom
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the bottom of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the bottom of the screen.</returns>
				// Token: 0x17001A31 RID: 6705
				// (get) Token: 0x0600743A RID: 29754 RVA: 0x001A5DA6 File Offset: 0x001A3FA6
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarBottom.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarBottom.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarBottom.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarBottom.normal;
					}
				}

				// Token: 0x040046BA RID: 18106
				private static readonly int part = 5;

				// Token: 0x040046BB RID: 18107
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the right side of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000934 RID: 2356
			public static class SizingBarRight
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the right side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the right side of the screen.</returns>
				// Token: 0x17001A32 RID: 6706
				// (get) Token: 0x0600743C RID: 29756 RVA: 0x001A5DD1 File Offset: 0x001A3FD1
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarRight.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarRight.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarRight.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarRight.normal;
					}
				}

				// Token: 0x040046BC RID: 18108
				private static readonly int part = 6;

				// Token: 0x040046BD RID: 18109
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the top of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000935 RID: 2357
			public static class SizingBarTop
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the top of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the top of the screen.</returns>
				// Token: 0x17001A33 RID: 6707
				// (get) Token: 0x0600743E RID: 29758 RVA: 0x001A5DFC File Offset: 0x001A3FFC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarTop.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarTop.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarTop.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarTop.normal;
					}
				}

				// Token: 0x040046BE RID: 18110
				private static readonly int part = 7;

				// Token: 0x040046BF RID: 18111
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing bar of a taskbar that is docked on the left side of the screen. This class cannot be inherited.</summary>
			// Token: 0x02000936 RID: 2358
			public static class SizingBarLeft
			{
				/// <summary>Gets a visual style element that represents the sizing bar for a taskbar that is docked on the left side of the screen.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing bar for a taskbar that is docked on the left side of the screen.</returns>
				// Token: 0x17001A34 RID: 6708
				// (get) Token: 0x06007440 RID: 29760 RVA: 0x001A5E27 File Offset: 0x001A4027
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Taskbar.SizingBarLeft.normal == null)
						{
							VisualStyleElement.Taskbar.SizingBarLeft.normal = new VisualStyleElement(VisualStyleElement.Taskbar.className, VisualStyleElement.Taskbar.SizingBarLeft.part, 0);
						}
						return VisualStyleElement.Taskbar.SizingBarLeft.normal;
					}
				}

				// Token: 0x040046C0 RID: 18112
				private static readonly int part = 8;

				// Token: 0x040046C1 RID: 18113
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a toolbar. This class cannot be inherited.</summary>
		// Token: 0x02000844 RID: 2116
		public static class ToolBar
		{
			// Token: 0x0400436F RID: 17263
			private static readonly string className = "TOOLBAR";

			// Token: 0x02000937 RID: 2359
			internal static class Bar
			{
				// Token: 0x17001A35 RID: 6709
				// (get) Token: 0x06007442 RID: 29762 RVA: 0x001A5E52 File Offset: 0x001A4052
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.Bar.normal == null)
						{
							VisualStyleElement.ToolBar.Bar.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Bar.part, 0);
						}
						return VisualStyleElement.ToolBar.Bar.normal;
					}
				}

				// Token: 0x040046C2 RID: 18114
				private static readonly int part;

				// Token: 0x040046C3 RID: 18115
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a toolbar button. This class cannot be inherited.</summary>
			// Token: 0x02000938 RID: 2360
			public static class Button
			{
				/// <summary>Gets a visual style element that represents a toolbar button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the normal state.</returns>
				// Token: 0x17001A36 RID: 6710
				// (get) Token: 0x06007443 RID: 29763 RVA: 0x001A5E75 File Offset: 0x001A4075
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.normal == null)
						{
							VisualStyleElement.ToolBar.Button.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 1);
						}
						return VisualStyleElement.ToolBar.Button.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot state.</returns>
				// Token: 0x17001A37 RID: 6711
				// (get) Token: 0x06007444 RID: 29764 RVA: 0x001A5E98 File Offset: 0x001A4098
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.hot == null)
						{
							VisualStyleElement.ToolBar.Button.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 2);
						}
						return VisualStyleElement.ToolBar.Button.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the pressed state.</returns>
				// Token: 0x17001A38 RID: 6712
				// (get) Token: 0x06007445 RID: 29765 RVA: 0x001A5EBB File Offset: 0x001A40BB
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.pressed == null)
						{
							VisualStyleElement.ToolBar.Button.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 3);
						}
						return VisualStyleElement.ToolBar.Button.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the disabled state.</returns>
				// Token: 0x17001A39 RID: 6713
				// (get) Token: 0x06007446 RID: 29766 RVA: 0x001A5EDE File Offset: 0x001A40DE
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.disabled == null)
						{
							VisualStyleElement.ToolBar.Button.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 4);
						}
						return VisualStyleElement.ToolBar.Button.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the checked state.</returns>
				// Token: 0x17001A3A RID: 6714
				// (get) Token: 0x06007447 RID: 29767 RVA: 0x001A5F01 File Offset: 0x001A4101
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button._checked == null)
						{
							VisualStyleElement.ToolBar.Button._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 5);
						}
						return VisualStyleElement.ToolBar.Button._checked;
					}
				}

				/// <summary>Gets a visual style element that represents a toolbar button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a toolbar button in the hot and checked states.</returns>
				// Token: 0x17001A3B RID: 6715
				// (get) Token: 0x06007448 RID: 29768 RVA: 0x001A5F24 File Offset: 0x001A4124
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.Button.hotchecked == null)
						{
							VisualStyleElement.ToolBar.Button.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.Button.part, 6);
						}
						return VisualStyleElement.ToolBar.Button.hotchecked;
					}
				}

				// Token: 0x040046C4 RID: 18116
				private static readonly int part = 1;

				// Token: 0x040046C5 RID: 18117
				private static VisualStyleElement normal;

				// Token: 0x040046C6 RID: 18118
				private static VisualStyleElement hot;

				// Token: 0x040046C7 RID: 18119
				private static VisualStyleElement pressed;

				// Token: 0x040046C8 RID: 18120
				private static VisualStyleElement disabled;

				// Token: 0x040046C9 RID: 18121
				private static VisualStyleElement _checked;

				// Token: 0x040046CA RID: 18122
				private static VisualStyleElement hotchecked;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a drop-down toolbar button. This class cannot be inherited.</summary>
			// Token: 0x02000939 RID: 2361
			public static class DropDownButton
			{
				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the normal state.</returns>
				// Token: 0x17001A3C RID: 6716
				// (get) Token: 0x0600744A RID: 29770 RVA: 0x001A5F4F File Offset: 0x001A414F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.normal == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 1);
						}
						return VisualStyleElement.ToolBar.DropDownButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot state.</returns>
				// Token: 0x17001A3D RID: 6717
				// (get) Token: 0x0600744B RID: 29771 RVA: 0x001A5F72 File Offset: 0x001A4172
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.hot == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 2);
						}
						return VisualStyleElement.ToolBar.DropDownButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the pressed state.</returns>
				// Token: 0x17001A3E RID: 6718
				// (get) Token: 0x0600744C RID: 29772 RVA: 0x001A5F95 File Offset: 0x001A4195
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.pressed == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 3);
						}
						return VisualStyleElement.ToolBar.DropDownButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the disabled state.</returns>
				// Token: 0x17001A3F RID: 6719
				// (get) Token: 0x0600744D RID: 29773 RVA: 0x001A5FB8 File Offset: 0x001A41B8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.disabled == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 4);
						}
						return VisualStyleElement.ToolBar.DropDownButton.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the checked state.</returns>
				// Token: 0x17001A40 RID: 6720
				// (get) Token: 0x0600744E RID: 29774 RVA: 0x001A5FDB File Offset: 0x001A41DB
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton._checked == null)
						{
							VisualStyleElement.ToolBar.DropDownButton._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 5);
						}
						return VisualStyleElement.ToolBar.DropDownButton._checked;
					}
				}

				/// <summary>Gets a visual style element that represents a drop-down toolbar button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a drop-down toolbar button in the hot and checked states.</returns>
				// Token: 0x17001A41 RID: 6721
				// (get) Token: 0x0600744F RID: 29775 RVA: 0x001A5FFE File Offset: 0x001A41FE
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.DropDownButton.hotchecked == null)
						{
							VisualStyleElement.ToolBar.DropDownButton.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.DropDownButton.part, 6);
						}
						return VisualStyleElement.ToolBar.DropDownButton.hotchecked;
					}
				}

				// Token: 0x040046CB RID: 18123
				private static readonly int part = 2;

				// Token: 0x040046CC RID: 18124
				private static VisualStyleElement normal;

				// Token: 0x040046CD RID: 18125
				private static VisualStyleElement hot;

				// Token: 0x040046CE RID: 18126
				private static VisualStyleElement pressed;

				// Token: 0x040046CF RID: 18127
				private static VisualStyleElement disabled;

				// Token: 0x040046D0 RID: 18128
				private static VisualStyleElement _checked;

				// Token: 0x040046D1 RID: 18129
				private static VisualStyleElement hotchecked;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the regular button portion of a combined regular button and drop-down button. This class cannot be inherited.</summary>
			// Token: 0x0200093A RID: 2362
			public static class SplitButton
			{
				/// <summary>Gets a visual style element that represents a split button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the normal state.</returns>
				// Token: 0x17001A42 RID: 6722
				// (get) Token: 0x06007451 RID: 29777 RVA: 0x001A6029 File Offset: 0x001A4229
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.normal == null)
						{
							VisualStyleElement.ToolBar.SplitButton.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 1);
						}
						return VisualStyleElement.ToolBar.SplitButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot state.</returns>
				// Token: 0x17001A43 RID: 6723
				// (get) Token: 0x06007452 RID: 29778 RVA: 0x001A604C File Offset: 0x001A424C
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.hot == null)
						{
							VisualStyleElement.ToolBar.SplitButton.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 2);
						}
						return VisualStyleElement.ToolBar.SplitButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the pressed state.</returns>
				// Token: 0x17001A44 RID: 6724
				// (get) Token: 0x06007453 RID: 29779 RVA: 0x001A606F File Offset: 0x001A426F
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.pressed == null)
						{
							VisualStyleElement.ToolBar.SplitButton.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 3);
						}
						return VisualStyleElement.ToolBar.SplitButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the disabled state.</returns>
				// Token: 0x17001A45 RID: 6725
				// (get) Token: 0x06007454 RID: 29780 RVA: 0x001A6092 File Offset: 0x001A4292
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.disabled == null)
						{
							VisualStyleElement.ToolBar.SplitButton.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 4);
						}
						return VisualStyleElement.ToolBar.SplitButton.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the checked state.</returns>
				// Token: 0x17001A46 RID: 6726
				// (get) Token: 0x06007455 RID: 29781 RVA: 0x001A60B5 File Offset: 0x001A42B5
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton._checked == null)
						{
							VisualStyleElement.ToolBar.SplitButton._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 5);
						}
						return VisualStyleElement.ToolBar.SplitButton._checked;
					}
				}

				/// <summary>Gets a visual style element that represents a split button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split button in the hot and checked states.</returns>
				// Token: 0x17001A47 RID: 6727
				// (get) Token: 0x06007456 RID: 29782 RVA: 0x001A60D8 File Offset: 0x001A42D8
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButton.hotchecked == null)
						{
							VisualStyleElement.ToolBar.SplitButton.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButton.part, 6);
						}
						return VisualStyleElement.ToolBar.SplitButton.hotchecked;
					}
				}

				// Token: 0x040046D2 RID: 18130
				private static readonly int part = 3;

				// Token: 0x040046D3 RID: 18131
				private static VisualStyleElement normal;

				// Token: 0x040046D4 RID: 18132
				private static VisualStyleElement hot;

				// Token: 0x040046D5 RID: 18133
				private static VisualStyleElement pressed;

				// Token: 0x040046D6 RID: 18134
				private static VisualStyleElement disabled;

				// Token: 0x040046D7 RID: 18135
				private static VisualStyleElement _checked;

				// Token: 0x040046D8 RID: 18136
				private static VisualStyleElement hotchecked;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the drop-down portion of a combined regular button and drop-down button. This class cannot be inherited.</summary>
			// Token: 0x0200093B RID: 2363
			public static class SplitButtonDropDown
			{
				/// <summary>Gets a visual style element that represents a split drop-down button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the normal state.</returns>
				// Token: 0x17001A48 RID: 6728
				// (get) Token: 0x06007458 RID: 29784 RVA: 0x001A6103 File Offset: 0x001A4303
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.normal == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 1);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot state.</returns>
				// Token: 0x17001A49 RID: 6729
				// (get) Token: 0x06007459 RID: 29785 RVA: 0x001A6126 File Offset: 0x001A4326
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.hot == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.hot = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 2);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the pressed state.</returns>
				// Token: 0x17001A4A RID: 6730
				// (get) Token: 0x0600745A RID: 29786 RVA: 0x001A6149 File Offset: 0x001A4349
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.pressed == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.pressed = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 3);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the disabled state.</returns>
				// Token: 0x17001A4B RID: 6731
				// (get) Token: 0x0600745B RID: 29787 RVA: 0x001A616C File Offset: 0x001A436C
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.disabled == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.disabled = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 4);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the checked state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the checked state.</returns>
				// Token: 0x17001A4C RID: 6732
				// (get) Token: 0x0600745C RID: 29788 RVA: 0x001A618F File Offset: 0x001A438F
				public static VisualStyleElement Checked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown._checked == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown._checked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 5);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown._checked;
					}
				}

				/// <summary>Gets a visual style element that represents a split drop-down button in the hot and checked states.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a split drop-down button in the hot and checked states.</returns>
				// Token: 0x17001A4D RID: 6733
				// (get) Token: 0x0600745D RID: 29789 RVA: 0x001A61B2 File Offset: 0x001A43B2
				public static VisualStyleElement HotChecked
				{
					get
					{
						if (VisualStyleElement.ToolBar.SplitButtonDropDown.hotchecked == null)
						{
							VisualStyleElement.ToolBar.SplitButtonDropDown.hotchecked = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SplitButtonDropDown.part, 6);
						}
						return VisualStyleElement.ToolBar.SplitButtonDropDown.hotchecked;
					}
				}

				// Token: 0x040046D9 RID: 18137
				private static readonly int part = 4;

				// Token: 0x040046DA RID: 18138
				private static VisualStyleElement normal;

				// Token: 0x040046DB RID: 18139
				private static VisualStyleElement hot;

				// Token: 0x040046DC RID: 18140
				private static VisualStyleElement pressed;

				// Token: 0x040046DD RID: 18141
				private static VisualStyleElement disabled;

				// Token: 0x040046DE RID: 18142
				private static VisualStyleElement _checked;

				// Token: 0x040046DF RID: 18143
				private static VisualStyleElement hotchecked;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a horizontal separator of the toolbar. This class cannot be inherited.</summary>
			// Token: 0x0200093C RID: 2364
			public static class SeparatorHorizontal
			{
				/// <summary>Gets a visual style element that represents a horizontal separator of the toolbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal separator of the toolbar.</returns>
				// Token: 0x17001A4E RID: 6734
				// (get) Token: 0x0600745F RID: 29791 RVA: 0x001A61DD File Offset: 0x001A43DD
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SeparatorHorizontal.normal == null)
						{
							VisualStyleElement.ToolBar.SeparatorHorizontal.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SeparatorHorizontal.part, 0);
						}
						return VisualStyleElement.ToolBar.SeparatorHorizontal.normal;
					}
				}

				// Token: 0x040046E0 RID: 18144
				private static readonly int part = 5;

				// Token: 0x040046E1 RID: 18145
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a vertical separator of the toolbar. This class cannot be inherited.</summary>
			// Token: 0x0200093D RID: 2365
			public static class SeparatorVertical
			{
				/// <summary>Gets a visual style element that represents a vertical separator of the toolbar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical separator of the toolbar.</returns>
				// Token: 0x17001A4F RID: 6735
				// (get) Token: 0x06007461 RID: 29793 RVA: 0x001A6208 File Offset: 0x001A4408
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolBar.SeparatorVertical.normal == null)
						{
							VisualStyleElement.ToolBar.SeparatorVertical.normal = new VisualStyleElement(VisualStyleElement.ToolBar.className, VisualStyleElement.ToolBar.SeparatorVertical.part, 0);
						}
						return VisualStyleElement.ToolBar.SeparatorVertical.normal;
					}
				}

				// Token: 0x040046E2 RID: 18146
				private static readonly int part = 6;

				// Token: 0x040046E3 RID: 18147
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a ToolTip. This class cannot be inherited.</summary>
		// Token: 0x02000845 RID: 2117
		public static class ToolTip
		{
			// Token: 0x04004370 RID: 17264
			private static readonly string className = "TOOLTIP";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a standard ToolTip. This class cannot be inherited.</summary>
			// Token: 0x0200093E RID: 2366
			public static class Standard
			{
				/// <summary>Gets a visual style element that represents a standard ToolTip that contains text.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains text.</returns>
				// Token: 0x17001A50 RID: 6736
				// (get) Token: 0x06007463 RID: 29795 RVA: 0x001A6233 File Offset: 0x001A4433
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.Standard.normal == null)
						{
							VisualStyleElement.ToolTip.Standard.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Standard.part, 1);
						}
						return VisualStyleElement.ToolTip.Standard.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a standard ToolTip that contains a link.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a standard ToolTip that contains a link.</returns>
				// Token: 0x17001A51 RID: 6737
				// (get) Token: 0x06007464 RID: 29796 RVA: 0x001A6256 File Offset: 0x001A4456
				public static VisualStyleElement Link
				{
					get
					{
						if (VisualStyleElement.ToolTip.Standard.link == null)
						{
							VisualStyleElement.ToolTip.Standard.link = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Standard.part, 2);
						}
						return VisualStyleElement.ToolTip.Standard.link;
					}
				}

				// Token: 0x040046E4 RID: 18148
				private static readonly int part = 1;

				// Token: 0x040046E5 RID: 18149
				private static VisualStyleElement normal;

				// Token: 0x040046E6 RID: 18150
				private static VisualStyleElement link;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title area of a standard ToolTip. This class cannot be inherited.</summary>
			// Token: 0x0200093F RID: 2367
			public static class StandardTitle
			{
				/// <summary>Gets a visual style element that represents the title area of a standard ToolTip.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title area of a standard ToolTip.</returns>
				// Token: 0x17001A52 RID: 6738
				// (get) Token: 0x06007466 RID: 29798 RVA: 0x001A6281 File Offset: 0x001A4481
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.StandardTitle.normal == null)
						{
							VisualStyleElement.ToolTip.StandardTitle.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.StandardTitle.part, 0);
						}
						return VisualStyleElement.ToolTip.StandardTitle.normal;
					}
				}

				// Token: 0x040046E7 RID: 18151
				private static readonly int part = 2;

				// Token: 0x040046E8 RID: 18152
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for a balloon ToolTip. This class cannot be inherited.</summary>
			// Token: 0x02000940 RID: 2368
			public static class Balloon
			{
				/// <summary>Gets a visual style element that represents a balloon ToolTip that contains text.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a balloon ToolTip that contains text.</returns>
				// Token: 0x17001A53 RID: 6739
				// (get) Token: 0x06007468 RID: 29800 RVA: 0x001A62AC File Offset: 0x001A44AC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.Balloon.normal == null)
						{
							VisualStyleElement.ToolTip.Balloon.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Balloon.part, 1);
						}
						return VisualStyleElement.ToolTip.Balloon.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a balloon ToolTip that contains a link.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a balloon ToolTip that contains a link.</returns>
				// Token: 0x17001A54 RID: 6740
				// (get) Token: 0x06007469 RID: 29801 RVA: 0x001A62CF File Offset: 0x001A44CF
				public static VisualStyleElement Link
				{
					get
					{
						if (VisualStyleElement.ToolTip.Balloon.link == null)
						{
							VisualStyleElement.ToolTip.Balloon.link = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Balloon.part, 2);
						}
						return VisualStyleElement.ToolTip.Balloon.link;
					}
				}

				// Token: 0x040046E9 RID: 18153
				private static readonly int part = 3;

				// Token: 0x040046EA RID: 18154
				private static VisualStyleElement normal;

				// Token: 0x040046EB RID: 18155
				private static VisualStyleElement link;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the title area of a balloon ToolTip. This class cannot be inherited.</summary>
			// Token: 0x02000941 RID: 2369
			public static class BalloonTitle
			{
				/// <summary>Gets a visual style element that represents the title area of a balloon ToolTip.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title area of a balloon ToolTip.</returns>
				// Token: 0x17001A55 RID: 6741
				// (get) Token: 0x0600746B RID: 29803 RVA: 0x001A62FA File Offset: 0x001A44FA
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.BalloonTitle.normal == null)
						{
							VisualStyleElement.ToolTip.BalloonTitle.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.BalloonTitle.part, 0);
						}
						return VisualStyleElement.ToolTip.BalloonTitle.normal;
					}
				}

				// Token: 0x040046EC RID: 18156
				private static readonly int part = 4;

				// Token: 0x040046ED RID: 18157
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a ToolTip. This class cannot be inherited.</summary>
			// Token: 0x02000942 RID: 2370
			public static class Close
			{
				/// <summary>Gets a visual style element that represents the ToolTip Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the normal state.</returns>
				// Token: 0x17001A56 RID: 6742
				// (get) Token: 0x0600746D RID: 29805 RVA: 0x001A6325 File Offset: 0x001A4525
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.ToolTip.Close.normal == null)
						{
							VisualStyleElement.ToolTip.Close.normal = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Close.part, 1);
						}
						return VisualStyleElement.ToolTip.Close.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the ToolTip Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the hot state.</returns>
				// Token: 0x17001A57 RID: 6743
				// (get) Token: 0x0600746E RID: 29806 RVA: 0x001A6348 File Offset: 0x001A4548
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.ToolTip.Close.hot == null)
						{
							VisualStyleElement.ToolTip.Close.hot = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Close.part, 2);
						}
						return VisualStyleElement.ToolTip.Close.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the ToolTip Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the ToolTip Close button in the pressed state.</returns>
				// Token: 0x17001A58 RID: 6744
				// (get) Token: 0x0600746F RID: 29807 RVA: 0x001A636B File Offset: 0x001A456B
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.ToolTip.Close.pressed == null)
						{
							VisualStyleElement.ToolTip.Close.pressed = new VisualStyleElement(VisualStyleElement.ToolTip.className, VisualStyleElement.ToolTip.Close.part, 3);
						}
						return VisualStyleElement.ToolTip.Close.pressed;
					}
				}

				// Token: 0x040046EE RID: 18158
				private static readonly int part = 5;

				// Token: 0x040046EF RID: 18159
				private static VisualStyleElement normal;

				// Token: 0x040046F0 RID: 18160
				private static VisualStyleElement hot;

				// Token: 0x040046F1 RID: 18161
				private static VisualStyleElement pressed;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the track bar control. This class cannot be inherited.</summary>
		// Token: 0x02000846 RID: 2118
		public static class TrackBar
		{
			// Token: 0x04004371 RID: 17265
			private static readonly string className = "TRACKBAR";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the track for a horizontal track bar. This class cannot be inherited.</summary>
			// Token: 0x02000943 RID: 2371
			public static class Track
			{
				/// <summary>Gets a visual style element that represents the track for a horizontal track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the track for a horizontal track bar.</returns>
				// Token: 0x17001A59 RID: 6745
				// (get) Token: 0x06007471 RID: 29809 RVA: 0x001A6396 File Offset: 0x001A4596
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.Track.normal == null)
						{
							VisualStyleElement.TrackBar.Track.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Track.part, 1);
						}
						return VisualStyleElement.TrackBar.Track.normal;
					}
				}

				// Token: 0x040046F2 RID: 18162
				private static readonly int part = 1;

				// Token: 0x040046F3 RID: 18163
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the track for a vertical track bar. This class cannot be inherited.</summary>
			// Token: 0x02000944 RID: 2372
			public static class TrackVertical
			{
				/// <summary>Gets a visual style element that represents the track for a vertical track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the track for a vertical track bar.</returns>
				// Token: 0x17001A5A RID: 6746
				// (get) Token: 0x06007473 RID: 29811 RVA: 0x001A63C1 File Offset: 0x001A45C1
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.TrackVertical.normal == null)
						{
							VisualStyleElement.TrackBar.TrackVertical.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.TrackVertical.part, 1);
						}
						return VisualStyleElement.TrackBar.TrackVertical.normal;
					}
				}

				// Token: 0x040046F4 RID: 18164
				private static readonly int part = 2;

				// Token: 0x040046F5 RID: 18165
				private static VisualStyleElement normal;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the slider (also known as the thumb) of a horizontal track bar. This class cannot be inherited.</summary>
			// Token: 0x02000945 RID: 2373
			public static class Thumb
			{
				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the normal state.</returns>
				// Token: 0x17001A5B RID: 6747
				// (get) Token: 0x06007475 RID: 29813 RVA: 0x001A63EC File Offset: 0x001A45EC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.normal == null)
						{
							VisualStyleElement.TrackBar.Thumb.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 1);
						}
						return VisualStyleElement.TrackBar.Thumb.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the hot state.</returns>
				// Token: 0x17001A5C RID: 6748
				// (get) Token: 0x06007476 RID: 29814 RVA: 0x001A640F File Offset: 0x001A460F
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.hot == null)
						{
							VisualStyleElement.TrackBar.Thumb.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 2);
						}
						return VisualStyleElement.TrackBar.Thumb.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the pressed state.</returns>
				// Token: 0x17001A5D RID: 6749
				// (get) Token: 0x06007477 RID: 29815 RVA: 0x001A6432 File Offset: 0x001A4632
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.pressed == null)
						{
							VisualStyleElement.TrackBar.Thumb.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 3);
						}
						return VisualStyleElement.TrackBar.Thumb.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar that has focus.</returns>
				// Token: 0x17001A5E RID: 6750
				// (get) Token: 0x06007478 RID: 29816 RVA: 0x001A6455 File Offset: 0x001A4655
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.focused == null)
						{
							VisualStyleElement.TrackBar.Thumb.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 4);
						}
						return VisualStyleElement.TrackBar.Thumb.focused;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a horizontal track bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a horizontal track bar in the disabled state.</returns>
				// Token: 0x17001A5F RID: 6751
				// (get) Token: 0x06007479 RID: 29817 RVA: 0x001A6478 File Offset: 0x001A4678
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.Thumb.disabled == null)
						{
							VisualStyleElement.TrackBar.Thumb.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Thumb.part, 5);
						}
						return VisualStyleElement.TrackBar.Thumb.disabled;
					}
				}

				// Token: 0x040046F6 RID: 18166
				private static readonly int part = 3;

				// Token: 0x040046F7 RID: 18167
				private static VisualStyleElement normal;

				// Token: 0x040046F8 RID: 18168
				private static VisualStyleElement hot;

				// Token: 0x040046F9 RID: 18169
				private static VisualStyleElement pressed;

				// Token: 0x040046FA RID: 18170
				private static VisualStyleElement focused;

				// Token: 0x040046FB RID: 18171
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the downward-pointing track bar slider (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x02000946 RID: 2374
			public static class ThumbBottom
			{
				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the normal state.</returns>
				// Token: 0x17001A60 RID: 6752
				// (get) Token: 0x0600747B RID: 29819 RVA: 0x001A64A3 File Offset: 0x001A46A3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the hot state.</returns>
				// Token: 0x17001A61 RID: 6753
				// (get) Token: 0x0600747C RID: 29820 RVA: 0x001A64C6 File Offset: 0x001A46C6
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the pressed state.</returns>
				// Token: 0x17001A62 RID: 6754
				// (get) Token: 0x0600747D RID: 29821 RVA: 0x001A64E9 File Offset: 0x001A46E9
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider that has focus.</returns>
				// Token: 0x17001A63 RID: 6755
				// (get) Token: 0x0600747E RID: 29822 RVA: 0x001A650C File Offset: 0x001A470C
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.focused;
					}
				}

				/// <summary>Gets a visual style element that represents a downward-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a downward-pointing track bar slider in the disabled state.</returns>
				// Token: 0x17001A64 RID: 6756
				// (get) Token: 0x0600747F RID: 29823 RVA: 0x001A652F File Offset: 0x001A472F
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbBottom.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbBottom.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbBottom.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbBottom.disabled;
					}
				}

				// Token: 0x040046FC RID: 18172
				private static readonly int part = 4;

				// Token: 0x040046FD RID: 18173
				private static VisualStyleElement normal;

				// Token: 0x040046FE RID: 18174
				private static VisualStyleElement hot;

				// Token: 0x040046FF RID: 18175
				private static VisualStyleElement pressed;

				// Token: 0x04004700 RID: 18176
				private static VisualStyleElement focused;

				// Token: 0x04004701 RID: 18177
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the upward-pointing track bar slider (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x02000947 RID: 2375
			public static class ThumbTop
			{
				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the normal state.</returns>
				// Token: 0x17001A65 RID: 6757
				// (get) Token: 0x06007481 RID: 29825 RVA: 0x001A655A File Offset: 0x001A475A
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbTop.normal;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the hot state.</returns>
				// Token: 0x17001A66 RID: 6758
				// (get) Token: 0x06007482 RID: 29826 RVA: 0x001A657D File Offset: 0x001A477D
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbTop.hot;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the pressed state.</returns>
				// Token: 0x17001A67 RID: 6759
				// (get) Token: 0x06007483 RID: 29827 RVA: 0x001A65A0 File Offset: 0x001A47A0
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbTop.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider that has focus.</returns>
				// Token: 0x17001A68 RID: 6760
				// (get) Token: 0x06007484 RID: 29828 RVA: 0x001A65C3 File Offset: 0x001A47C3
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbTop.focused;
					}
				}

				/// <summary>Gets a visual style element that represents an upward-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an upward-pointing track bar slider in the disabled state.</returns>
				// Token: 0x17001A69 RID: 6761
				// (get) Token: 0x06007485 RID: 29829 RVA: 0x001A65E6 File Offset: 0x001A47E6
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbTop.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbTop.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbTop.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbTop.disabled;
					}
				}

				// Token: 0x04004702 RID: 18178
				private static readonly int part = 5;

				// Token: 0x04004703 RID: 18179
				private static VisualStyleElement normal;

				// Token: 0x04004704 RID: 18180
				private static VisualStyleElement hot;

				// Token: 0x04004705 RID: 18181
				private static VisualStyleElement pressed;

				// Token: 0x04004706 RID: 18182
				private static VisualStyleElement focused;

				// Token: 0x04004707 RID: 18183
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the slider (also known as the thumb) of a vertical track bar. This class cannot be inherited.</summary>
			// Token: 0x02000948 RID: 2376
			public static class ThumbVertical
			{
				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the normal state.</returns>
				// Token: 0x17001A6A RID: 6762
				// (get) Token: 0x06007487 RID: 29831 RVA: 0x001A6611 File Offset: 0x001A4811
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the hot state.</returns>
				// Token: 0x17001A6B RID: 6763
				// (get) Token: 0x06007488 RID: 29832 RVA: 0x001A6634 File Offset: 0x001A4834
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the pressed state.</returns>
				// Token: 0x17001A6C RID: 6764
				// (get) Token: 0x06007489 RID: 29833 RVA: 0x001A6657 File Offset: 0x001A4857
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar that has focus.</returns>
				// Token: 0x17001A6D RID: 6765
				// (get) Token: 0x0600748A RID: 29834 RVA: 0x001A667A File Offset: 0x001A487A
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.focused;
					}
				}

				/// <summary>Gets a visual style element that represents the slider of a vertical track bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the slider of a vertical track bar in the disabled state.</returns>
				// Token: 0x17001A6E RID: 6766
				// (get) Token: 0x0600748B RID: 29835 RVA: 0x001A669D File Offset: 0x001A489D
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbVertical.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbVertical.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbVertical.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbVertical.disabled;
					}
				}

				// Token: 0x04004708 RID: 18184
				private static readonly int part = 6;

				// Token: 0x04004709 RID: 18185
				private static VisualStyleElement normal;

				// Token: 0x0400470A RID: 18186
				private static VisualStyleElement hot;

				// Token: 0x0400470B RID: 18187
				private static VisualStyleElement pressed;

				// Token: 0x0400470C RID: 18188
				private static VisualStyleElement focused;

				// Token: 0x0400470D RID: 18189
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left-pointing track bar slider (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x02000949 RID: 2377
			public static class ThumbLeft
			{
				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the normal state.</returns>
				// Token: 0x17001A6F RID: 6767
				// (get) Token: 0x0600748D RID: 29837 RVA: 0x001A66C8 File Offset: 0x001A48C8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the hot state.</returns>
				// Token: 0x17001A70 RID: 6768
				// (get) Token: 0x0600748E RID: 29838 RVA: 0x001A66EB File Offset: 0x001A48EB
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the pressed state.</returns>
				// Token: 0x17001A71 RID: 6769
				// (get) Token: 0x0600748F RID: 29839 RVA: 0x001A670E File Offset: 0x001A490E
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider that has focus.</returns>
				// Token: 0x17001A72 RID: 6770
				// (get) Token: 0x06007490 RID: 29840 RVA: 0x001A6731 File Offset: 0x001A4931
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.focused;
					}
				}

				/// <summary>Gets a visual style element that represents a left-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a left-pointing track bar slider in the disabled state.</returns>
				// Token: 0x17001A73 RID: 6771
				// (get) Token: 0x06007491 RID: 29841 RVA: 0x001A6754 File Offset: 0x001A4954
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbLeft.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbLeft.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbLeft.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbLeft.disabled;
					}
				}

				// Token: 0x0400470E RID: 18190
				private static readonly int part = 7;

				// Token: 0x0400470F RID: 18191
				private static VisualStyleElement normal;

				// Token: 0x04004710 RID: 18192
				private static VisualStyleElement hot;

				// Token: 0x04004711 RID: 18193
				private static VisualStyleElement pressed;

				// Token: 0x04004712 RID: 18194
				private static VisualStyleElement focused;

				// Token: 0x04004713 RID: 18195
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right-pointing track bar slider (also known as the thumb). This class cannot be inherited.</summary>
			// Token: 0x0200094A RID: 2378
			public static class ThumbRight
			{
				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the normal state.</returns>
				// Token: 0x17001A74 RID: 6772
				// (get) Token: 0x06007493 RID: 29843 RVA: 0x001A677F File Offset: 0x001A497F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.normal == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 1);
						}
						return VisualStyleElement.TrackBar.ThumbRight.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the hot state.</returns>
				// Token: 0x17001A75 RID: 6773
				// (get) Token: 0x06007494 RID: 29844 RVA: 0x001A67A2 File Offset: 0x001A49A2
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.hot == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.hot = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 2);
						}
						return VisualStyleElement.TrackBar.ThumbRight.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the pressed state.</returns>
				// Token: 0x17001A76 RID: 6774
				// (get) Token: 0x06007495 RID: 29845 RVA: 0x001A67C5 File Offset: 0x001A49C5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.pressed == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.pressed = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 3);
						}
						return VisualStyleElement.TrackBar.ThumbRight.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider that has focus.</returns>
				// Token: 0x17001A77 RID: 6775
				// (get) Token: 0x06007496 RID: 29846 RVA: 0x001A67E8 File Offset: 0x001A49E8
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.focused == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.focused = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 4);
						}
						return VisualStyleElement.TrackBar.ThumbRight.focused;
					}
				}

				/// <summary>Gets a visual style element that represents a right-pointing track bar slider in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a right-pointing track bar slider in the disabled state.</returns>
				// Token: 0x17001A78 RID: 6776
				// (get) Token: 0x06007497 RID: 29847 RVA: 0x001A680B File Offset: 0x001A4A0B
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TrackBar.ThumbRight.disabled == null)
						{
							VisualStyleElement.TrackBar.ThumbRight.disabled = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.ThumbRight.part, 5);
						}
						return VisualStyleElement.TrackBar.ThumbRight.disabled;
					}
				}

				// Token: 0x04004714 RID: 18196
				private static readonly int part = 8;

				// Token: 0x04004715 RID: 18197
				private static VisualStyleElement normal;

				// Token: 0x04004716 RID: 18198
				private static VisualStyleElement hot;

				// Token: 0x04004717 RID: 18199
				private static VisualStyleElement pressed;

				// Token: 0x04004718 RID: 18200
				private static VisualStyleElement focused;

				// Token: 0x04004719 RID: 18201
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a single tick of a horizontal track bar. This class cannot be inherited.</summary>
			// Token: 0x0200094B RID: 2379
			public static class Ticks
			{
				/// <summary>Gets a visual style element that represents a single tick of a horizontal track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a single tick of a horizontal track bar.</returns>
				// Token: 0x17001A79 RID: 6777
				// (get) Token: 0x06007499 RID: 29849 RVA: 0x001A6836 File Offset: 0x001A4A36
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.Ticks.normal == null)
						{
							VisualStyleElement.TrackBar.Ticks.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.Ticks.part, 1);
						}
						return VisualStyleElement.TrackBar.Ticks.normal;
					}
				}

				// Token: 0x0400471A RID: 18202
				private static readonly int part = 9;

				// Token: 0x0400471B RID: 18203
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a single tick of a vertical track bar. This class cannot be inherited.</summary>
			// Token: 0x0200094C RID: 2380
			public static class TicksVertical
			{
				/// <summary>Gets a visual style element that represents a single tick of a vertical track bar.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a single tick of a vertical track bar.</returns>
				// Token: 0x17001A7A RID: 6778
				// (get) Token: 0x0600749B RID: 29851 RVA: 0x001A6862 File Offset: 0x001A4A62
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrackBar.TicksVertical.normal == null)
						{
							VisualStyleElement.TrackBar.TicksVertical.normal = new VisualStyleElement(VisualStyleElement.TrackBar.className, VisualStyleElement.TrackBar.TicksVertical.part, 1);
						}
						return VisualStyleElement.TrackBar.TicksVertical.normal;
					}
				}

				// Token: 0x0400471C RID: 18204
				private static readonly int part = 10;

				// Token: 0x0400471D RID: 18205
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of the tree view control. This class cannot be inherited.</summary>
		// Token: 0x02000847 RID: 2119
		public static class TreeView
		{
			// Token: 0x04004372 RID: 17266
			private static readonly string className = "TREEVIEW";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a tree view item. This class cannot be inherited.</summary>
			// Token: 0x0200094D RID: 2381
			public static class Item
			{
				/// <summary>Gets a visual style element that represents a tree view item in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the normal state.</returns>
				// Token: 0x17001A7B RID: 6779
				// (get) Token: 0x0600749D RID: 29853 RVA: 0x001A688E File Offset: 0x001A4A8E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.normal == null)
						{
							VisualStyleElement.TreeView.Item.normal = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 1);
						}
						return VisualStyleElement.TreeView.Item.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the hot state.</returns>
				// Token: 0x17001A7C RID: 6780
				// (get) Token: 0x0600749E RID: 29854 RVA: 0x001A68B1 File Offset: 0x001A4AB1
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.hot == null)
						{
							VisualStyleElement.TreeView.Item.hot = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 2);
						}
						return VisualStyleElement.TreeView.Item.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item that is in the selected state and has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item that is in the selected state and has focus.</returns>
				// Token: 0x17001A7D RID: 6781
				// (get) Token: 0x0600749F RID: 29855 RVA: 0x001A68D4 File Offset: 0x001A4AD4
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.selected == null)
						{
							VisualStyleElement.TreeView.Item.selected = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 3);
						}
						return VisualStyleElement.TreeView.Item.selected;
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item in the disabled state.</returns>
				// Token: 0x17001A7E RID: 6782
				// (get) Token: 0x060074A0 RID: 29856 RVA: 0x001A68F7 File Offset: 0x001A4AF7
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.disabled == null)
						{
							VisualStyleElement.TreeView.Item.disabled = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 4);
						}
						return VisualStyleElement.TreeView.Item.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a tree view item that is in the selected state but does not have focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view item that is in the selected state but does not have focus.</returns>
				// Token: 0x17001A7F RID: 6783
				// (get) Token: 0x060074A1 RID: 29857 RVA: 0x001A691A File Offset: 0x001A4B1A
				public static VisualStyleElement SelectedNotFocus
				{
					get
					{
						if (VisualStyleElement.TreeView.Item.selectednotfocus == null)
						{
							VisualStyleElement.TreeView.Item.selectednotfocus = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Item.part, 5);
						}
						return VisualStyleElement.TreeView.Item.selectednotfocus;
					}
				}

				// Token: 0x0400471E RID: 18206
				private static readonly int part = 1;

				// Token: 0x0400471F RID: 18207
				private static VisualStyleElement normal;

				// Token: 0x04004720 RID: 18208
				private static VisualStyleElement hot;

				// Token: 0x04004721 RID: 18209
				private static VisualStyleElement selected;

				// Token: 0x04004722 RID: 18210
				private static VisualStyleElement disabled;

				// Token: 0x04004723 RID: 18211
				private static VisualStyleElement selectednotfocus;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the plus sign (+) and minus sign (-) buttons of a tree view control. This class cannot be inherited.</summary>
			// Token: 0x0200094E RID: 2382
			public static class Glyph
			{
				/// <summary>Gets a visual style element that represents a minus sign (-) button of a tree view node.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a minus sign button of a tree view node.</returns>
				// Token: 0x17001A80 RID: 6784
				// (get) Token: 0x060074A3 RID: 29859 RVA: 0x001A6945 File Offset: 0x001A4B45
				public static VisualStyleElement Closed
				{
					get
					{
						if (VisualStyleElement.TreeView.Glyph.closed == null)
						{
							VisualStyleElement.TreeView.Glyph.closed = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Glyph.part, 1);
						}
						return VisualStyleElement.TreeView.Glyph.closed;
					}
				}

				/// <summary>Gets a visual style element that represents a plus sign (+) button of a tree view node.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a plus sign button of a tree view node.</returns>
				// Token: 0x17001A81 RID: 6785
				// (get) Token: 0x060074A4 RID: 29860 RVA: 0x001A6968 File Offset: 0x001A4B68
				public static VisualStyleElement Opened
				{
					get
					{
						if (VisualStyleElement.TreeView.Glyph.opened == null)
						{
							VisualStyleElement.TreeView.Glyph.opened = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Glyph.part, 2);
						}
						return VisualStyleElement.TreeView.Glyph.opened;
					}
				}

				// Token: 0x04004724 RID: 18212
				private static readonly int part = 2;

				// Token: 0x04004725 RID: 18213
				private static VisualStyleElement closed;

				// Token: 0x04004726 RID: 18214
				private static VisualStyleElement opened;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for a tree view branch. This class cannot be inherited.</summary>
			// Token: 0x0200094F RID: 2383
			public static class Branch
			{
				/// <summary>Gets a visual style element that represents a tree view branch.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a tree view branch.</returns>
				// Token: 0x17001A82 RID: 6786
				// (get) Token: 0x060074A6 RID: 29862 RVA: 0x001A6993 File Offset: 0x001A4B93
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TreeView.Branch.normal == null)
						{
							VisualStyleElement.TreeView.Branch.normal = new VisualStyleElement(VisualStyleElement.TreeView.className, VisualStyleElement.TreeView.Branch.part, 0);
						}
						return VisualStyleElement.TreeView.Branch.normal;
					}
				}

				// Token: 0x04004727 RID: 18215
				private static readonly int part = 3;

				// Token: 0x04004728 RID: 18216
				private static VisualStyleElement normal;
			}
		}

		// Token: 0x02000848 RID: 2120
		internal static class ExplorerTreeView
		{
			// Token: 0x04004373 RID: 17267
			private static readonly string className = "Explorer::TreeView";

			// Token: 0x02000950 RID: 2384
			public static class Glyph
			{
				// Token: 0x17001A83 RID: 6787
				// (get) Token: 0x060074A8 RID: 29864 RVA: 0x001A69BE File Offset: 0x001A4BBE
				public static VisualStyleElement Closed
				{
					get
					{
						if (VisualStyleElement.ExplorerTreeView.Glyph.closed == null)
						{
							VisualStyleElement.ExplorerTreeView.Glyph.closed = new VisualStyleElement(VisualStyleElement.ExplorerTreeView.className, VisualStyleElement.ExplorerTreeView.Glyph.part, 1);
						}
						return VisualStyleElement.ExplorerTreeView.Glyph.closed;
					}
				}

				// Token: 0x17001A84 RID: 6788
				// (get) Token: 0x060074A9 RID: 29865 RVA: 0x001A69E1 File Offset: 0x001A4BE1
				public static VisualStyleElement Opened
				{
					get
					{
						if (VisualStyleElement.ExplorerTreeView.Glyph.opened == null)
						{
							VisualStyleElement.ExplorerTreeView.Glyph.opened = new VisualStyleElement(VisualStyleElement.ExplorerTreeView.className, VisualStyleElement.ExplorerTreeView.Glyph.part, 2);
						}
						return VisualStyleElement.ExplorerTreeView.Glyph.opened;
					}
				}

				// Token: 0x04004729 RID: 18217
				private static readonly int part = 2;

				// Token: 0x0400472A RID: 18218
				private static VisualStyleElement closed;

				// Token: 0x0400472B RID: 18219
				private static VisualStyleElement opened;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a text box. This class cannot be inherited.</summary>
		// Token: 0x02000849 RID: 2121
		public static class TextBox
		{
			// Token: 0x04004374 RID: 17268
			private static readonly string className = "EDIT";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of a text box. This class cannot be inherited.</summary>
			// Token: 0x02000951 RID: 2385
			public static class TextEdit
			{
				/// <summary>Gets a visual style element that represents a normal text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a normal text box.</returns>
				// Token: 0x17001A85 RID: 6789
				// (get) Token: 0x060074AB RID: 29867 RVA: 0x001A6A0C File Offset: 0x001A4C0C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.normal == null)
						{
							VisualStyleElement.TextBox.TextEdit.normal = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 1);
						}
						return VisualStyleElement.TextBox.TextEdit.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a hot text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a hot text box.</returns>
				// Token: 0x17001A86 RID: 6790
				// (get) Token: 0x060074AC RID: 29868 RVA: 0x001A6A2F File Offset: 0x001A4C2F
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.hot == null)
						{
							VisualStyleElement.TextBox.TextEdit.hot = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 2);
						}
						return VisualStyleElement.TextBox.TextEdit.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a selected text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a selected text box.</returns>
				// Token: 0x17001A87 RID: 6791
				// (get) Token: 0x060074AD RID: 29869 RVA: 0x001A6A52 File Offset: 0x001A4C52
				public static VisualStyleElement Selected
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.selected == null)
						{
							VisualStyleElement.TextBox.TextEdit.selected = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 3);
						}
						return VisualStyleElement.TextBox.TextEdit.selected;
					}
				}

				/// <summary>Gets a visual style element that represents a disabled text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a disabled text box.</returns>
				// Token: 0x17001A88 RID: 6792
				// (get) Token: 0x060074AE RID: 29870 RVA: 0x001A6A75 File Offset: 0x001A4C75
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.disabled == null)
						{
							VisualStyleElement.TextBox.TextEdit.disabled = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 4);
						}
						return VisualStyleElement.TextBox.TextEdit.disabled;
					}
				}

				/// <summary>Gets a visual style element that represents a text box that has focus.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box that has focus.</returns>
				// Token: 0x17001A89 RID: 6793
				// (get) Token: 0x060074AF RID: 29871 RVA: 0x001A6A98 File Offset: 0x001A4C98
				public static VisualStyleElement Focused
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.focused == null)
						{
							VisualStyleElement.TextBox.TextEdit.focused = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 5);
						}
						return VisualStyleElement.TextBox.TextEdit.focused;
					}
				}

				/// <summary>Gets a visual style element that represents a read-only text box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a read-only text box.</returns>
				// Token: 0x17001A8A RID: 6794
				// (get) Token: 0x060074B0 RID: 29872 RVA: 0x001A6ABB File Offset: 0x001A4CBB
				public static VisualStyleElement ReadOnly
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit._readonly == null)
						{
							VisualStyleElement.TextBox.TextEdit._readonly = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 6);
						}
						return VisualStyleElement.TextBox.TextEdit._readonly;
					}
				}

				/// <summary>Gets a visual style element that represents a text box in assist mode.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a text box in assist mode.</returns>
				// Token: 0x17001A8B RID: 6795
				// (get) Token: 0x060074B1 RID: 29873 RVA: 0x001A6ADE File Offset: 0x001A4CDE
				public static VisualStyleElement Assist
				{
					get
					{
						if (VisualStyleElement.TextBox.TextEdit.assist == null)
						{
							VisualStyleElement.TextBox.TextEdit.assist = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.TextEdit.part, 7);
						}
						return VisualStyleElement.TextBox.TextEdit.assist;
					}
				}

				// Token: 0x0400472C RID: 18220
				private static readonly int part = 1;

				// Token: 0x0400472D RID: 18221
				private static VisualStyleElement normal;

				// Token: 0x0400472E RID: 18222
				private static VisualStyleElement hot;

				// Token: 0x0400472F RID: 18223
				private static VisualStyleElement selected;

				// Token: 0x04004730 RID: 18224
				private static VisualStyleElement disabled;

				// Token: 0x04004731 RID: 18225
				private static VisualStyleElement focused;

				// Token: 0x04004732 RID: 18226
				private static VisualStyleElement _readonly;

				// Token: 0x04004733 RID: 18227
				private static VisualStyleElement assist;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the caret of a text box. This class cannot be inherited.</summary>
			// Token: 0x02000952 RID: 2386
			public static class Caret
			{
				/// <summary>Gets a visual style element that represents a text box caret.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the insertion point of a text box.</returns>
				// Token: 0x17001A8C RID: 6796
				// (get) Token: 0x060074B3 RID: 29875 RVA: 0x001A6B09 File Offset: 0x001A4D09
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TextBox.Caret.normal == null)
						{
							VisualStyleElement.TextBox.Caret.normal = new VisualStyleElement(VisualStyleElement.TextBox.className, VisualStyleElement.TextBox.Caret.part, 0);
						}
						return VisualStyleElement.TextBox.Caret.normal;
					}
				}

				// Token: 0x04004734 RID: 18228
				private static readonly int part = 2;

				// Token: 0x04004735 RID: 18229
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the background of the notification area, which is located at the far right of the taskbar. This class cannot be inherited.</summary>
		// Token: 0x0200084A RID: 2122
		public static class TrayNotify
		{
			// Token: 0x04004375 RID: 17269
			private static readonly string className = "TRAYNOTIFY";

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of the notification area. This class cannot be inherited.</summary>
			// Token: 0x02000953 RID: 2387
			public static class Background
			{
				/// <summary>Gets a visual style element that represents the background of the notification area.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of the notification area.</returns>
				// Token: 0x17001A8D RID: 6797
				// (get) Token: 0x060074B5 RID: 29877 RVA: 0x001A6B34 File Offset: 0x001A4D34
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrayNotify.Background.normal == null)
						{
							VisualStyleElement.TrayNotify.Background.normal = new VisualStyleElement(VisualStyleElement.TrayNotify.className, VisualStyleElement.TrayNotify.Background.part, 0);
						}
						return VisualStyleElement.TrayNotify.Background.normal;
					}
				}

				// Token: 0x04004736 RID: 18230
				private static readonly int part = 1;

				// Token: 0x04004737 RID: 18231
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for an animated background of the notification area. This class cannot be inherited.</summary>
			// Token: 0x02000954 RID: 2388
			public static class AnimateBackground
			{
				/// <summary>Gets a visual style element that represents an animated background of the notification area.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents an animated background of the notification area.</returns>
				// Token: 0x17001A8E RID: 6798
				// (get) Token: 0x060074B7 RID: 29879 RVA: 0x001A6B5F File Offset: 0x001A4D5F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.TrayNotify.AnimateBackground.normal == null)
						{
							VisualStyleElement.TrayNotify.AnimateBackground.normal = new VisualStyleElement(VisualStyleElement.TrayNotify.className, VisualStyleElement.TrayNotify.AnimateBackground.part, 0);
						}
						return VisualStyleElement.TrayNotify.AnimateBackground.normal;
					}
				}

				// Token: 0x04004738 RID: 18232
				private static readonly int part = 2;

				// Token: 0x04004739 RID: 18233
				private static VisualStyleElement normal;
			}
		}

		/// <summary>Contains classes that provide <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for the parts of a window. This class cannot be inherited.</summary>
		// Token: 0x0200084B RID: 2123
		public static class Window
		{
			// Token: 0x04004376 RID: 17270
			private static readonly string className = "WINDOW";

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a window. This class cannot be inherited.</summary>
			// Token: 0x02000955 RID: 2389
			public static class Caption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active window.</returns>
				// Token: 0x17001A8F RID: 6799
				// (get) Token: 0x060074B9 RID: 29881 RVA: 0x001A6B8A File Offset: 0x001A4D8A
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.Caption.active == null)
						{
							VisualStyleElement.Window.Caption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Caption.part, 1);
						}
						return VisualStyleElement.Window.Caption.active;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive window.</returns>
				// Token: 0x17001A90 RID: 6800
				// (get) Token: 0x060074BA RID: 29882 RVA: 0x001A6BAD File Offset: 0x001A4DAD
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.Caption.inactive == null)
						{
							VisualStyleElement.Window.Caption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Caption.part, 2);
						}
						return VisualStyleElement.Window.Caption.inactive;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled window.</returns>
				// Token: 0x17001A91 RID: 6801
				// (get) Token: 0x060074BB RID: 29883 RVA: 0x001A6BD0 File Offset: 0x001A4DD0
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.Caption.disabled == null)
						{
							VisualStyleElement.Window.Caption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Caption.part, 3);
						}
						return VisualStyleElement.Window.Caption.disabled;
					}
				}

				// Token: 0x0400473A RID: 18234
				private static readonly int part = 1;

				// Token: 0x0400473B RID: 18235
				private static VisualStyleElement active;

				// Token: 0x0400473C RID: 18236
				private static VisualStyleElement inactive;

				// Token: 0x0400473D RID: 18237
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000956 RID: 2390
			public static class SmallCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window.</returns>
				// Token: 0x17001A92 RID: 6802
				// (get) Token: 0x060074BD RID: 29885 RVA: 0x001A6BFB File Offset: 0x001A4DFB
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaption.active == null)
						{
							VisualStyleElement.Window.SmallCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaption.part, 1);
						}
						return VisualStyleElement.Window.SmallCaption.active;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window.</returns>
				// Token: 0x17001A93 RID: 6803
				// (get) Token: 0x060074BE RID: 29886 RVA: 0x001A6C1E File Offset: 0x001A4E1E
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaption.inactive == null)
						{
							VisualStyleElement.Window.SmallCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaption.part, 2);
						}
						return VisualStyleElement.Window.SmallCaption.inactive;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window.</returns>
				// Token: 0x17001A94 RID: 6804
				// (get) Token: 0x060074BF RID: 29887 RVA: 0x001A6C41 File Offset: 0x001A4E41
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaption.disabled == null)
						{
							VisualStyleElement.Window.SmallCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaption.part, 3);
						}
						return VisualStyleElement.Window.SmallCaption.disabled;
					}
				}

				// Token: 0x0400473E RID: 18238
				private static readonly int part = 2;

				// Token: 0x0400473F RID: 18239
				private static VisualStyleElement active;

				// Token: 0x04004740 RID: 18240
				private static VisualStyleElement inactive;

				// Token: 0x04004741 RID: 18241
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized window. This class cannot be inherited.</summary>
			// Token: 0x02000957 RID: 2391
			public static class MinCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of a minimized active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized active window.</returns>
				// Token: 0x17001A95 RID: 6805
				// (get) Token: 0x060074C1 RID: 29889 RVA: 0x001A6C6C File Offset: 0x001A4E6C
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.MinCaption.active == null)
						{
							VisualStyleElement.Window.MinCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinCaption.part, 1);
						}
						return VisualStyleElement.Window.MinCaption.active;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a minimized inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized inactive window.</returns>
				// Token: 0x17001A96 RID: 6806
				// (get) Token: 0x060074C2 RID: 29890 RVA: 0x001A6C8F File Offset: 0x001A4E8F
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.MinCaption.inactive == null)
						{
							VisualStyleElement.Window.MinCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinCaption.part, 2);
						}
						return VisualStyleElement.Window.MinCaption.inactive;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a minimized disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a minimized disabled window.</returns>
				// Token: 0x17001A97 RID: 6807
				// (get) Token: 0x060074C3 RID: 29891 RVA: 0x001A6CB2 File Offset: 0x001A4EB2
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MinCaption.disabled == null)
						{
							VisualStyleElement.Window.MinCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinCaption.part, 3);
						}
						return VisualStyleElement.Window.MinCaption.disabled;
					}
				}

				// Token: 0x04004742 RID: 18242
				private static readonly int part = 3;

				// Token: 0x04004743 RID: 18243
				private static VisualStyleElement active;

				// Token: 0x04004744 RID: 18244
				private static VisualStyleElement inactive;

				// Token: 0x04004745 RID: 18245
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a minimized small window. This class cannot be inherited.</summary>
			// Token: 0x02000958 RID: 2392
			public static class SmallMinCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is minimized.</returns>
				// Token: 0x17001A98 RID: 6808
				// (get) Token: 0x060074C5 RID: 29893 RVA: 0x001A6CDD File Offset: 0x001A4EDD
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallMinCaption.active == null)
						{
							VisualStyleElement.Window.SmallMinCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMinCaption.part, 1);
						}
						return VisualStyleElement.Window.SmallMinCaption.active;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is minimized.</returns>
				// Token: 0x17001A99 RID: 6809
				// (get) Token: 0x060074C6 RID: 29894 RVA: 0x001A6D00 File Offset: 0x001A4F00
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallMinCaption.inactive == null)
						{
							VisualStyleElement.Window.SmallMinCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMinCaption.part, 2);
						}
						return VisualStyleElement.Window.SmallMinCaption.inactive;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is minimized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is minimized.</returns>
				// Token: 0x17001A9A RID: 6810
				// (get) Token: 0x060074C7 RID: 29895 RVA: 0x001A6D23 File Offset: 0x001A4F23
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallMinCaption.disabled == null)
						{
							VisualStyleElement.Window.SmallMinCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMinCaption.part, 3);
						}
						return VisualStyleElement.Window.SmallMinCaption.disabled;
					}
				}

				// Token: 0x04004746 RID: 18246
				private static readonly int part = 4;

				// Token: 0x04004747 RID: 18247
				private static VisualStyleElement active;

				// Token: 0x04004748 RID: 18248
				private static VisualStyleElement inactive;

				// Token: 0x04004749 RID: 18249
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized window. This class cannot be inherited.</summary>
			// Token: 0x02000959 RID: 2393
			public static class MaxCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of a maximized active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized active window.</returns>
				// Token: 0x17001A9B RID: 6811
				// (get) Token: 0x060074C9 RID: 29897 RVA: 0x001A6D4E File Offset: 0x001A4F4E
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.MaxCaption.active == null)
						{
							VisualStyleElement.Window.MaxCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxCaption.part, 1);
						}
						return VisualStyleElement.Window.MaxCaption.active;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a maximized inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized inactive window.</returns>
				// Token: 0x17001A9C RID: 6812
				// (get) Token: 0x060074CA RID: 29898 RVA: 0x001A6D71 File Offset: 0x001A4F71
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.MaxCaption.inactive == null)
						{
							VisualStyleElement.Window.MaxCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxCaption.part, 2);
						}
						return VisualStyleElement.Window.MaxCaption.inactive;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a maximized disabled window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a maximized disabled window.</returns>
				// Token: 0x17001A9D RID: 6813
				// (get) Token: 0x060074CB RID: 29899 RVA: 0x001A6D94 File Offset: 0x001A4F94
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MaxCaption.disabled == null)
						{
							VisualStyleElement.Window.MaxCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxCaption.part, 3);
						}
						return VisualStyleElement.Window.MaxCaption.disabled;
					}
				}

				// Token: 0x0400474A RID: 18250
				private static readonly int part = 5;

				// Token: 0x0400474B RID: 18251
				private static VisualStyleElement active;

				// Token: 0x0400474C RID: 18252
				private static VisualStyleElement inactive;

				// Token: 0x0400474D RID: 18253
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the title bar of a maximized small window. This class cannot be inherited.</summary>
			// Token: 0x0200095A RID: 2394
			public static class SmallMaxCaption
			{
				/// <summary>Gets a visual style element that represents the title bar of an active small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an active small window that is maximized.</returns>
				// Token: 0x17001A9E RID: 6814
				// (get) Token: 0x060074CD RID: 29901 RVA: 0x001A6DBF File Offset: 0x001A4FBF
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallMaxCaption.active == null)
						{
							VisualStyleElement.Window.SmallMaxCaption.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMaxCaption.part, 1);
						}
						return VisualStyleElement.Window.SmallMaxCaption.active;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of an inactive small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of an inactive small window that is maximized.</returns>
				// Token: 0x17001A9F RID: 6815
				// (get) Token: 0x060074CE RID: 29902 RVA: 0x001A6DE2 File Offset: 0x001A4FE2
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallMaxCaption.inactive == null)
						{
							VisualStyleElement.Window.SmallMaxCaption.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMaxCaption.part, 2);
						}
						return VisualStyleElement.Window.SmallMaxCaption.inactive;
					}
				}

				/// <summary>Gets a visual style element that represents the title bar of a disabled small window that is maximized.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the title bar of a disabled small window that is maximized.</returns>
				// Token: 0x17001AA0 RID: 6816
				// (get) Token: 0x060074CF RID: 29903 RVA: 0x001A6E05 File Offset: 0x001A5005
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallMaxCaption.disabled == null)
						{
							VisualStyleElement.Window.SmallMaxCaption.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallMaxCaption.part, 3);
						}
						return VisualStyleElement.Window.SmallMaxCaption.disabled;
					}
				}

				// Token: 0x0400474E RID: 18254
				private static readonly int part = 6;

				// Token: 0x0400474F RID: 18255
				private static VisualStyleElement active;

				// Token: 0x04004750 RID: 18256
				private static VisualStyleElement inactive;

				// Token: 0x04004751 RID: 18257
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a window. This class cannot be inherited.</summary>
			// Token: 0x0200095B RID: 2395
			public static class FrameLeft
			{
				/// <summary>Gets a visual style element that represents the left border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active window.</returns>
				// Token: 0x17001AA1 RID: 6817
				// (get) Token: 0x060074D1 RID: 29905 RVA: 0x001A6E30 File Offset: 0x001A5030
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.FrameLeft.active == null)
						{
							VisualStyleElement.Window.FrameLeft.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameLeft.part, 1);
						}
						return VisualStyleElement.Window.FrameLeft.active;
					}
				}

				/// <summary>Gets a visual style element that represents the left border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive window.</returns>
				// Token: 0x17001AA2 RID: 6818
				// (get) Token: 0x060074D2 RID: 29906 RVA: 0x001A6E53 File Offset: 0x001A5053
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.FrameLeft.inactive == null)
						{
							VisualStyleElement.Window.FrameLeft.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameLeft.part, 2);
						}
						return VisualStyleElement.Window.FrameLeft.inactive;
					}
				}

				// Token: 0x04004752 RID: 18258
				private static readonly int part = 7;

				// Token: 0x04004753 RID: 18259
				private static VisualStyleElement active;

				// Token: 0x04004754 RID: 18260
				private static VisualStyleElement inactive;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a window. This class cannot be inherited.</summary>
			// Token: 0x0200095C RID: 2396
			public static class FrameRight
			{
				/// <summary>Gets a visual style element that represents the right border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active window.</returns>
				// Token: 0x17001AA3 RID: 6819
				// (get) Token: 0x060074D4 RID: 29908 RVA: 0x001A6E7E File Offset: 0x001A507E
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.FrameRight.active == null)
						{
							VisualStyleElement.Window.FrameRight.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameRight.part, 1);
						}
						return VisualStyleElement.Window.FrameRight.active;
					}
				}

				/// <summary>Gets a visual style element that represents the right border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive window.</returns>
				// Token: 0x17001AA4 RID: 6820
				// (get) Token: 0x060074D5 RID: 29909 RVA: 0x001A6EA1 File Offset: 0x001A50A1
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.FrameRight.inactive == null)
						{
							VisualStyleElement.Window.FrameRight.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameRight.part, 2);
						}
						return VisualStyleElement.Window.FrameRight.inactive;
					}
				}

				// Token: 0x04004755 RID: 18261
				private static readonly int part = 8;

				// Token: 0x04004756 RID: 18262
				private static VisualStyleElement active;

				// Token: 0x04004757 RID: 18263
				private static VisualStyleElement inactive;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a window. This class cannot be inherited.</summary>
			// Token: 0x0200095D RID: 2397
			public static class FrameBottom
			{
				/// <summary>Gets a visual style element that represents the bottom border of an active window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active window.</returns>
				// Token: 0x17001AA5 RID: 6821
				// (get) Token: 0x060074D7 RID: 29911 RVA: 0x001A6ECC File Offset: 0x001A50CC
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.FrameBottom.active == null)
						{
							VisualStyleElement.Window.FrameBottom.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameBottom.part, 1);
						}
						return VisualStyleElement.Window.FrameBottom.active;
					}
				}

				/// <summary>Gets a visual style element that represents the bottom border of an inactive window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive window.</returns>
				// Token: 0x17001AA6 RID: 6822
				// (get) Token: 0x060074D8 RID: 29912 RVA: 0x001A6EEF File Offset: 0x001A50EF
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.FrameBottom.inactive == null)
						{
							VisualStyleElement.Window.FrameBottom.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameBottom.part, 2);
						}
						return VisualStyleElement.Window.FrameBottom.inactive;
					}
				}

				// Token: 0x04004758 RID: 18264
				private static readonly int part = 9;

				// Token: 0x04004759 RID: 18265
				private static VisualStyleElement active;

				// Token: 0x0400475A RID: 18266
				private static VisualStyleElement inactive;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the left border of a small window. This class cannot be inherited.</summary>
			// Token: 0x0200095E RID: 2398
			public static class SmallFrameLeft
			{
				/// <summary>Gets a visual style element that represents the left border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an active small window.</returns>
				// Token: 0x17001AA7 RID: 6823
				// (get) Token: 0x060074DA RID: 29914 RVA: 0x001A6F1B File Offset: 0x001A511B
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameLeft.active == null)
						{
							VisualStyleElement.Window.SmallFrameLeft.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameLeft.part, 1);
						}
						return VisualStyleElement.Window.SmallFrameLeft.active;
					}
				}

				/// <summary>Gets a visual style element that represents the left border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the left border of an inactive small window.</returns>
				// Token: 0x17001AA8 RID: 6824
				// (get) Token: 0x060074DB RID: 29915 RVA: 0x001A6F3E File Offset: 0x001A513E
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameLeft.inactive == null)
						{
							VisualStyleElement.Window.SmallFrameLeft.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameLeft.part, 2);
						}
						return VisualStyleElement.Window.SmallFrameLeft.inactive;
					}
				}

				// Token: 0x0400475B RID: 18267
				private static readonly int part = 10;

				// Token: 0x0400475C RID: 18268
				private static VisualStyleElement active;

				// Token: 0x0400475D RID: 18269
				private static VisualStyleElement inactive;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the right border of a small window. This class cannot be inherited.</summary>
			// Token: 0x0200095F RID: 2399
			public static class SmallFrameRight
			{
				/// <summary>Gets a visual style element that represents the right border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an active small window.</returns>
				// Token: 0x17001AA9 RID: 6825
				// (get) Token: 0x060074DD RID: 29917 RVA: 0x001A6F6A File Offset: 0x001A516A
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameRight.active == null)
						{
							VisualStyleElement.Window.SmallFrameRight.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameRight.part, 1);
						}
						return VisualStyleElement.Window.SmallFrameRight.active;
					}
				}

				/// <summary>Gets a visual style element that represents the right border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the right border of an inactive small window.</returns>
				// Token: 0x17001AAA RID: 6826
				// (get) Token: 0x060074DE RID: 29918 RVA: 0x001A6F8D File Offset: 0x001A518D
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameRight.inactive == null)
						{
							VisualStyleElement.Window.SmallFrameRight.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameRight.part, 2);
						}
						return VisualStyleElement.Window.SmallFrameRight.inactive;
					}
				}

				// Token: 0x0400475E RID: 18270
				private static readonly int part = 11;

				// Token: 0x0400475F RID: 18271
				private static VisualStyleElement active;

				// Token: 0x04004760 RID: 18272
				private static VisualStyleElement inactive;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the bottom border of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000960 RID: 2400
			public static class SmallFrameBottom
			{
				/// <summary>Gets a visual style element that represents the bottom border of an active small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an active small window.</returns>
				// Token: 0x17001AAB RID: 6827
				// (get) Token: 0x060074E0 RID: 29920 RVA: 0x001A6FB9 File Offset: 0x001A51B9
				public static VisualStyleElement Active
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameBottom.active == null)
						{
							VisualStyleElement.Window.SmallFrameBottom.active = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameBottom.part, 1);
						}
						return VisualStyleElement.Window.SmallFrameBottom.active;
					}
				}

				/// <summary>Gets a visual style element that represents the bottom border of an inactive small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the bottom border of an inactive small window.</returns>
				// Token: 0x17001AAC RID: 6828
				// (get) Token: 0x060074E1 RID: 29921 RVA: 0x001A6FDC File Offset: 0x001A51DC
				public static VisualStyleElement Inactive
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameBottom.inactive == null)
						{
							VisualStyleElement.Window.SmallFrameBottom.inactive = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameBottom.part, 2);
						}
						return VisualStyleElement.Window.SmallFrameBottom.inactive;
					}
				}

				// Token: 0x04004761 RID: 18273
				private static readonly int part = 12;

				// Token: 0x04004762 RID: 18274
				private static VisualStyleElement active;

				// Token: 0x04004763 RID: 18275
				private static VisualStyleElement inactive;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the System button of a window. This class cannot be inherited.</summary>
			// Token: 0x02000961 RID: 2401
			public static class SysButton
			{
				/// <summary>Gets a visual style element that represents a System button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the normal state.</returns>
				// Token: 0x17001AAD RID: 6829
				// (get) Token: 0x060074E3 RID: 29923 RVA: 0x001A7008 File Offset: 0x001A5208
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.normal == null)
						{
							VisualStyleElement.Window.SysButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 1);
						}
						return VisualStyleElement.Window.SysButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a System button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the hot state.</returns>
				// Token: 0x17001AAE RID: 6830
				// (get) Token: 0x060074E4 RID: 29924 RVA: 0x001A702B File Offset: 0x001A522B
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.hot == null)
						{
							VisualStyleElement.Window.SysButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 2);
						}
						return VisualStyleElement.Window.SysButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a System button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the pressed state.</returns>
				// Token: 0x17001AAF RID: 6831
				// (get) Token: 0x060074E5 RID: 29925 RVA: 0x001A704E File Offset: 0x001A524E
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.pressed == null)
						{
							VisualStyleElement.Window.SysButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 3);
						}
						return VisualStyleElement.Window.SysButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a System button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a System button in the disabled state.</returns>
				// Token: 0x17001AB0 RID: 6832
				// (get) Token: 0x060074E6 RID: 29926 RVA: 0x001A7071 File Offset: 0x001A5271
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SysButton.disabled == null)
						{
							VisualStyleElement.Window.SysButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SysButton.part, 4);
						}
						return VisualStyleElement.Window.SysButton.disabled;
					}
				}

				// Token: 0x04004764 RID: 18276
				private static readonly int part = 13;

				// Token: 0x04004765 RID: 18277
				private static VisualStyleElement normal;

				// Token: 0x04004766 RID: 18278
				private static VisualStyleElement hot;

				// Token: 0x04004767 RID: 18279
				private static VisualStyleElement pressed;

				// Token: 0x04004768 RID: 18280
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the System button of a multiple-document interface (MDI) child window with visual styles. This class cannot be inherited.</summary>
			// Token: 0x02000962 RID: 2402
			public static class MdiSysButton
			{
				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the normal state.</returns>
				// Token: 0x17001AB1 RID: 6833
				// (get) Token: 0x060074E8 RID: 29928 RVA: 0x001A709D File Offset: 0x001A529D
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.normal == null)
						{
							VisualStyleElement.Window.MdiSysButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 1);
						}
						return VisualStyleElement.Window.MdiSysButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the hot state.</returns>
				// Token: 0x17001AB2 RID: 6834
				// (get) Token: 0x060074E9 RID: 29929 RVA: 0x001A70C0 File Offset: 0x001A52C0
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.hot == null)
						{
							VisualStyleElement.Window.MdiSysButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 2);
						}
						return VisualStyleElement.Window.MdiSysButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001AB3 RID: 6835
				// (get) Token: 0x060074EA RID: 29930 RVA: 0x001A70E3 File Offset: 0x001A52E3
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.pressed == null)
						{
							VisualStyleElement.Window.MdiSysButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 3);
						}
						return VisualStyleElement.Window.MdiSysButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the System button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the System button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001AB4 RID: 6836
				// (get) Token: 0x060074EB RID: 29931 RVA: 0x001A7106 File Offset: 0x001A5306
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiSysButton.disabled == null)
						{
							VisualStyleElement.Window.MdiSysButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiSysButton.part, 4);
						}
						return VisualStyleElement.Window.MdiSysButton.disabled;
					}
				}

				// Token: 0x04004769 RID: 18281
				private static readonly int part = 14;

				// Token: 0x0400476A RID: 18282
				private static VisualStyleElement normal;

				// Token: 0x0400476B RID: 18283
				private static VisualStyleElement hot;

				// Token: 0x0400476C RID: 18284
				private static VisualStyleElement pressed;

				// Token: 0x0400476D RID: 18285
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a window. This class cannot be inherited.</summary>
			// Token: 0x02000963 RID: 2403
			public static class MinButton
			{
				/// <summary>Gets a visual style element that represents a Minimize button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the normal state.</returns>
				// Token: 0x17001AB5 RID: 6837
				// (get) Token: 0x060074ED RID: 29933 RVA: 0x001A7132 File Offset: 0x001A5332
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.normal == null)
						{
							VisualStyleElement.Window.MinButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 1);
						}
						return VisualStyleElement.Window.MinButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the hot state.</returns>
				// Token: 0x17001AB6 RID: 6838
				// (get) Token: 0x060074EE RID: 29934 RVA: 0x001A7155 File Offset: 0x001A5355
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.hot == null)
						{
							VisualStyleElement.Window.MinButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 2);
						}
						return VisualStyleElement.Window.MinButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the pressed state.</returns>
				// Token: 0x17001AB7 RID: 6839
				// (get) Token: 0x060074EF RID: 29935 RVA: 0x001A7178 File Offset: 0x001A5378
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.pressed == null)
						{
							VisualStyleElement.Window.MinButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 3);
						}
						return VisualStyleElement.Window.MinButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a Minimize button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Minimize button in the disabled state.</returns>
				// Token: 0x17001AB8 RID: 6840
				// (get) Token: 0x060074F0 RID: 29936 RVA: 0x001A719B File Offset: 0x001A539B
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MinButton.disabled == null)
						{
							VisualStyleElement.Window.MinButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MinButton.part, 4);
						}
						return VisualStyleElement.Window.MinButton.disabled;
					}
				}

				// Token: 0x0400476E RID: 18286
				private static readonly int part = 15;

				// Token: 0x0400476F RID: 18287
				private static VisualStyleElement normal;

				// Token: 0x04004770 RID: 18288
				private static VisualStyleElement hot;

				// Token: 0x04004771 RID: 18289
				private static VisualStyleElement pressed;

				// Token: 0x04004772 RID: 18290
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Minimize button of a multiple-document interface (MDI) child window. This class cannot be inherited.</summary>
			// Token: 0x02000964 RID: 2404
			public static class MdiMinButton
			{
				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the normal state.</returns>
				// Token: 0x17001AB9 RID: 6841
				// (get) Token: 0x060074F2 RID: 29938 RVA: 0x001A71C7 File Offset: 0x001A53C7
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.normal == null)
						{
							VisualStyleElement.Window.MdiMinButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 1);
						}
						return VisualStyleElement.Window.MdiMinButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the hot state.</returns>
				// Token: 0x17001ABA RID: 6842
				// (get) Token: 0x060074F3 RID: 29939 RVA: 0x001A71EA File Offset: 0x001A53EA
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.hot == null)
						{
							VisualStyleElement.Window.MdiMinButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 2);
						}
						return VisualStyleElement.Window.MdiMinButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001ABB RID: 6843
				// (get) Token: 0x060074F4 RID: 29940 RVA: 0x001A720D File Offset: 0x001A540D
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.pressed == null)
						{
							VisualStyleElement.Window.MdiMinButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 3);
						}
						return VisualStyleElement.Window.MdiMinButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the Minimize button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Minimize button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001ABC RID: 6844
				// (get) Token: 0x060074F5 RID: 29941 RVA: 0x001A7230 File Offset: 0x001A5430
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiMinButton.disabled == null)
						{
							VisualStyleElement.Window.MdiMinButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiMinButton.part, 4);
						}
						return VisualStyleElement.Window.MdiMinButton.disabled;
					}
				}

				// Token: 0x04004773 RID: 18291
				private static readonly int part = 16;

				// Token: 0x04004774 RID: 18292
				private static VisualStyleElement normal;

				// Token: 0x04004775 RID: 18293
				private static VisualStyleElement hot;

				// Token: 0x04004776 RID: 18294
				private static VisualStyleElement pressed;

				// Token: 0x04004777 RID: 18295
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Maximize button of a window. This class cannot be inherited.</summary>
			// Token: 0x02000965 RID: 2405
			public static class MaxButton
			{
				/// <summary>Gets a visual style element that represents a Maximize button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the normal state.</returns>
				// Token: 0x17001ABD RID: 6845
				// (get) Token: 0x060074F7 RID: 29943 RVA: 0x001A725C File Offset: 0x001A545C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.normal == null)
						{
							VisualStyleElement.Window.MaxButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 1);
						}
						return VisualStyleElement.Window.MaxButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the hot state.</returns>
				// Token: 0x17001ABE RID: 6846
				// (get) Token: 0x060074F8 RID: 29944 RVA: 0x001A727F File Offset: 0x001A547F
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.hot == null)
						{
							VisualStyleElement.Window.MaxButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 2);
						}
						return VisualStyleElement.Window.MaxButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the pressed state.</returns>
				// Token: 0x17001ABF RID: 6847
				// (get) Token: 0x060074F9 RID: 29945 RVA: 0x001A72A2 File Offset: 0x001A54A2
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.pressed == null)
						{
							VisualStyleElement.Window.MaxButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 3);
						}
						return VisualStyleElement.Window.MaxButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a Maximize button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Maximize button in the disabled state.</returns>
				// Token: 0x17001AC0 RID: 6848
				// (get) Token: 0x060074FA RID: 29946 RVA: 0x001A72C5 File Offset: 0x001A54C5
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MaxButton.disabled == null)
						{
							VisualStyleElement.Window.MaxButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MaxButton.part, 4);
						}
						return VisualStyleElement.Window.MaxButton.disabled;
					}
				}

				// Token: 0x04004778 RID: 18296
				private static readonly int part = 17;

				// Token: 0x04004779 RID: 18297
				private static VisualStyleElement normal;

				// Token: 0x0400477A RID: 18298
				private static VisualStyleElement hot;

				// Token: 0x0400477B RID: 18299
				private static VisualStyleElement pressed;

				// Token: 0x0400477C RID: 18300
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a window. This class cannot be inherited.</summary>
			// Token: 0x02000966 RID: 2406
			public static class CloseButton
			{
				/// <summary>Gets a visual style element that represents a Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the normal state.</returns>
				// Token: 0x17001AC1 RID: 6849
				// (get) Token: 0x060074FC RID: 29948 RVA: 0x001A72F1 File Offset: 0x001A54F1
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.normal == null)
						{
							VisualStyleElement.Window.CloseButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 1);
						}
						return VisualStyleElement.Window.CloseButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the hot state.</returns>
				// Token: 0x17001AC2 RID: 6850
				// (get) Token: 0x060074FD RID: 29949 RVA: 0x001A7314 File Offset: 0x001A5514
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.hot == null)
						{
							VisualStyleElement.Window.CloseButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 2);
						}
						return VisualStyleElement.Window.CloseButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the pressed state.</returns>
				// Token: 0x17001AC3 RID: 6851
				// (get) Token: 0x060074FE RID: 29950 RVA: 0x001A7337 File Offset: 0x001A5537
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.pressed == null)
						{
							VisualStyleElement.Window.CloseButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 3);
						}
						return VisualStyleElement.Window.CloseButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a Close button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Close button in the disabled state.</returns>
				// Token: 0x17001AC4 RID: 6852
				// (get) Token: 0x060074FF RID: 29951 RVA: 0x001A735A File Offset: 0x001A555A
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.CloseButton.disabled == null)
						{
							VisualStyleElement.Window.CloseButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CloseButton.part, 4);
						}
						return VisualStyleElement.Window.CloseButton.disabled;
					}
				}

				// Token: 0x0400477D RID: 18301
				private static readonly int part = 18;

				// Token: 0x0400477E RID: 18302
				private static VisualStyleElement normal;

				// Token: 0x0400477F RID: 18303
				private static VisualStyleElement hot;

				// Token: 0x04004780 RID: 18304
				private static VisualStyleElement pressed;

				// Token: 0x04004781 RID: 18305
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000967 RID: 2407
			public static class SmallCloseButton
			{
				/// <summary>Gets a visual style element that represents the small Close button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the normal state.</returns>
				// Token: 0x17001AC5 RID: 6853
				// (get) Token: 0x06007501 RID: 29953 RVA: 0x001A7386 File Offset: 0x001A5586
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.normal == null)
						{
							VisualStyleElement.Window.SmallCloseButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 1);
						}
						return VisualStyleElement.Window.SmallCloseButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the hot state.</returns>
				// Token: 0x17001AC6 RID: 6854
				// (get) Token: 0x06007502 RID: 29954 RVA: 0x001A73A9 File Offset: 0x001A55A9
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.hot == null)
						{
							VisualStyleElement.Window.SmallCloseButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 2);
						}
						return VisualStyleElement.Window.SmallCloseButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the pressed state.</returns>
				// Token: 0x17001AC7 RID: 6855
				// (get) Token: 0x06007503 RID: 29955 RVA: 0x001A73CC File Offset: 0x001A55CC
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.pressed == null)
						{
							VisualStyleElement.Window.SmallCloseButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 3);
						}
						return VisualStyleElement.Window.SmallCloseButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the small Close button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the small Close button in the disabled state.</returns>
				// Token: 0x17001AC8 RID: 6856
				// (get) Token: 0x06007504 RID: 29956 RVA: 0x001A73EF File Offset: 0x001A55EF
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.SmallCloseButton.disabled == null)
						{
							VisualStyleElement.Window.SmallCloseButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCloseButton.part, 4);
						}
						return VisualStyleElement.Window.SmallCloseButton.disabled;
					}
				}

				// Token: 0x04004782 RID: 18306
				private static readonly int part = 19;

				// Token: 0x04004783 RID: 18307
				private static VisualStyleElement normal;

				// Token: 0x04004784 RID: 18308
				private static VisualStyleElement hot;

				// Token: 0x04004785 RID: 18309
				private static VisualStyleElement pressed;

				// Token: 0x04004786 RID: 18310
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Close button of a multiple-document interface (MDI) child window. This class cannot be inherited.</summary>
			// Token: 0x02000968 RID: 2408
			public static class MdiCloseButton
			{
				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the normal state.</returns>
				// Token: 0x17001AC9 RID: 6857
				// (get) Token: 0x06007506 RID: 29958 RVA: 0x001A741B File Offset: 0x001A561B
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.normal == null)
						{
							VisualStyleElement.Window.MdiCloseButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 1);
						}
						return VisualStyleElement.Window.MdiCloseButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the hot state.</returns>
				// Token: 0x17001ACA RID: 6858
				// (get) Token: 0x06007507 RID: 29959 RVA: 0x001A743E File Offset: 0x001A563E
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.hot == null)
						{
							VisualStyleElement.Window.MdiCloseButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 2);
						}
						return VisualStyleElement.Window.MdiCloseButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001ACB RID: 6859
				// (get) Token: 0x06007508 RID: 29960 RVA: 0x001A7461 File Offset: 0x001A5661
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.pressed == null)
						{
							VisualStyleElement.Window.MdiCloseButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 3);
						}
						return VisualStyleElement.Window.MdiCloseButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the Close button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Close button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001ACC RID: 6860
				// (get) Token: 0x06007509 RID: 29961 RVA: 0x001A7484 File Offset: 0x001A5684
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiCloseButton.disabled == null)
						{
							VisualStyleElement.Window.MdiCloseButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiCloseButton.part, 4);
						}
						return VisualStyleElement.Window.MdiCloseButton.disabled;
					}
				}

				// Token: 0x04004787 RID: 18311
				private static readonly int part = 20;

				// Token: 0x04004788 RID: 18312
				private static VisualStyleElement normal;

				// Token: 0x04004789 RID: 18313
				private static VisualStyleElement hot;

				// Token: 0x0400478A RID: 18314
				private static VisualStyleElement pressed;

				// Token: 0x0400478B RID: 18315
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a window. This class cannot be inherited.</summary>
			// Token: 0x02000969 RID: 2409
			public static class RestoreButton
			{
				/// <summary>Gets a visual style element that represents a Restore button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the normal state.</returns>
				// Token: 0x17001ACD RID: 6861
				// (get) Token: 0x0600750B RID: 29963 RVA: 0x001A74B0 File Offset: 0x001A56B0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.normal == null)
						{
							VisualStyleElement.Window.RestoreButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 1);
						}
						return VisualStyleElement.Window.RestoreButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the hot state.</returns>
				// Token: 0x17001ACE RID: 6862
				// (get) Token: 0x0600750C RID: 29964 RVA: 0x001A74D3 File Offset: 0x001A56D3
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.hot == null)
						{
							VisualStyleElement.Window.RestoreButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 2);
						}
						return VisualStyleElement.Window.RestoreButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the pressed state.</returns>
				// Token: 0x17001ACF RID: 6863
				// (get) Token: 0x0600750D RID: 29965 RVA: 0x001A74F6 File Offset: 0x001A56F6
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.pressed == null)
						{
							VisualStyleElement.Window.RestoreButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 3);
						}
						return VisualStyleElement.Window.RestoreButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a Restore button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Restore button in the disabled state.</returns>
				// Token: 0x17001AD0 RID: 6864
				// (get) Token: 0x0600750E RID: 29966 RVA: 0x001A7519 File Offset: 0x001A5719
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.RestoreButton.disabled == null)
						{
							VisualStyleElement.Window.RestoreButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.RestoreButton.part, 4);
						}
						return VisualStyleElement.Window.RestoreButton.disabled;
					}
				}

				// Token: 0x0400478C RID: 18316
				private static readonly int part = 21;

				// Token: 0x0400478D RID: 18317
				private static VisualStyleElement normal;

				// Token: 0x0400478E RID: 18318
				private static VisualStyleElement hot;

				// Token: 0x0400478F RID: 18319
				private static VisualStyleElement pressed;

				// Token: 0x04004790 RID: 18320
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Restore button of a multiple-document interface (MDI) child window. This class cannot be inherited.</summary>
			// Token: 0x0200096A RID: 2410
			public static class MdiRestoreButton
			{
				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the normal state.</returns>
				// Token: 0x17001AD1 RID: 6865
				// (get) Token: 0x06007510 RID: 29968 RVA: 0x001A7545 File Offset: 0x001A5745
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.normal == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 1);
						}
						return VisualStyleElement.Window.MdiRestoreButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the hot state.</returns>
				// Token: 0x17001AD2 RID: 6866
				// (get) Token: 0x06007511 RID: 29969 RVA: 0x001A7568 File Offset: 0x001A5768
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.hot == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 2);
						}
						return VisualStyleElement.Window.MdiRestoreButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001AD3 RID: 6867
				// (get) Token: 0x06007512 RID: 29970 RVA: 0x001A758B File Offset: 0x001A578B
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.pressed == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 3);
						}
						return VisualStyleElement.Window.MdiRestoreButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the Restore button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Restore button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001AD4 RID: 6868
				// (get) Token: 0x06007513 RID: 29971 RVA: 0x001A75AE File Offset: 0x001A57AE
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiRestoreButton.disabled == null)
						{
							VisualStyleElement.Window.MdiRestoreButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiRestoreButton.part, 4);
						}
						return VisualStyleElement.Window.MdiRestoreButton.disabled;
					}
				}

				// Token: 0x04004791 RID: 18321
				private static readonly int part = 22;

				// Token: 0x04004792 RID: 18322
				private static VisualStyleElement normal;

				// Token: 0x04004793 RID: 18323
				private static VisualStyleElement hot;

				// Token: 0x04004794 RID: 18324
				private static VisualStyleElement pressed;

				// Token: 0x04004795 RID: 18325
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a window or dialog box. This class cannot be inherited.</summary>
			// Token: 0x0200096B RID: 2411
			public static class HelpButton
			{
				/// <summary>Gets a visual style element that represents a Help button in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the normal state.</returns>
				// Token: 0x17001AD5 RID: 6869
				// (get) Token: 0x06007515 RID: 29973 RVA: 0x001A75DA File Offset: 0x001A57DA
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.normal == null)
						{
							VisualStyleElement.Window.HelpButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 1);
						}
						return VisualStyleElement.Window.HelpButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the hot state.</returns>
				// Token: 0x17001AD6 RID: 6870
				// (get) Token: 0x06007516 RID: 29974 RVA: 0x001A75FD File Offset: 0x001A57FD
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.hot == null)
						{
							VisualStyleElement.Window.HelpButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 2);
						}
						return VisualStyleElement.Window.HelpButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the pressed state.</returns>
				// Token: 0x17001AD7 RID: 6871
				// (get) Token: 0x06007517 RID: 29975 RVA: 0x001A7620 File Offset: 0x001A5820
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.pressed == null)
						{
							VisualStyleElement.Window.HelpButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 3);
						}
						return VisualStyleElement.Window.HelpButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a Help button in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a Help button in the disabled state.</returns>
				// Token: 0x17001AD8 RID: 6872
				// (get) Token: 0x06007518 RID: 29976 RVA: 0x001A7643 File Offset: 0x001A5843
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.HelpButton.disabled == null)
						{
							VisualStyleElement.Window.HelpButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HelpButton.part, 4);
						}
						return VisualStyleElement.Window.HelpButton.disabled;
					}
				}

				// Token: 0x04004796 RID: 18326
				private static readonly int part = 23;

				// Token: 0x04004797 RID: 18327
				private static VisualStyleElement normal;

				// Token: 0x04004798 RID: 18328
				private static VisualStyleElement hot;

				// Token: 0x04004799 RID: 18329
				private static VisualStyleElement pressed;

				// Token: 0x0400479A RID: 18330
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the Help button of a multiple-document interface (MDI) child window. This class cannot be inherited.</summary>
			// Token: 0x0200096C RID: 2412
			public static class MdiHelpButton
			{
				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the normal state.</returns>
				// Token: 0x17001AD9 RID: 6873
				// (get) Token: 0x0600751A RID: 29978 RVA: 0x001A766F File Offset: 0x001A586F
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.normal == null)
						{
							VisualStyleElement.Window.MdiHelpButton.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 1);
						}
						return VisualStyleElement.Window.MdiHelpButton.normal;
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the hot state.</returns>
				// Token: 0x17001ADA RID: 6874
				// (get) Token: 0x0600751B RID: 29979 RVA: 0x001A7692 File Offset: 0x001A5892
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.hot == null)
						{
							VisualStyleElement.Window.MdiHelpButton.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 2);
						}
						return VisualStyleElement.Window.MdiHelpButton.hot;
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the pressed state.</returns>
				// Token: 0x17001ADB RID: 6875
				// (get) Token: 0x0600751C RID: 29980 RVA: 0x001A76B5 File Offset: 0x001A58B5
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.pressed == null)
						{
							VisualStyleElement.Window.MdiHelpButton.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 3);
						}
						return VisualStyleElement.Window.MdiHelpButton.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents the Help button of a multiple-document interface (MDI) child window in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the Help button of an MDI child window in the disabled state.</returns>
				// Token: 0x17001ADC RID: 6876
				// (get) Token: 0x0600751D RID: 29981 RVA: 0x001A76D8 File Offset: 0x001A58D8
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.MdiHelpButton.disabled == null)
						{
							VisualStyleElement.Window.MdiHelpButton.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.MdiHelpButton.part, 4);
						}
						return VisualStyleElement.Window.MdiHelpButton.disabled;
					}
				}

				// Token: 0x0400479B RID: 18331
				private static readonly int part = 24;

				// Token: 0x0400479C RID: 18332
				private static VisualStyleElement normal;

				// Token: 0x0400479D RID: 18333
				private static VisualStyleElement hot;

				// Token: 0x0400479E RID: 18334
				private static VisualStyleElement pressed;

				// Token: 0x0400479F RID: 18335
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the horizontal scroll bar of a window. This class cannot be inherited.</summary>
			// Token: 0x0200096D RID: 2413
			public static class HorizontalScroll
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the normal state.</returns>
				// Token: 0x17001ADD RID: 6877
				// (get) Token: 0x0600751F RID: 29983 RVA: 0x001A7704 File Offset: 0x001A5904
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.normal == null)
						{
							VisualStyleElement.Window.HorizontalScroll.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 1);
						}
						return VisualStyleElement.Window.HorizontalScroll.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the hot state.</returns>
				// Token: 0x17001ADE RID: 6878
				// (get) Token: 0x06007520 RID: 29984 RVA: 0x001A7727 File Offset: 0x001A5927
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.hot == null)
						{
							VisualStyleElement.Window.HorizontalScroll.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 2);
						}
						return VisualStyleElement.Window.HorizontalScroll.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the pressed state.</returns>
				// Token: 0x17001ADF RID: 6879
				// (get) Token: 0x06007521 RID: 29985 RVA: 0x001A774A File Offset: 0x001A594A
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.pressed == null)
						{
							VisualStyleElement.Window.HorizontalScroll.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 3);
						}
						return VisualStyleElement.Window.HorizontalScroll.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll bar in the disabled state.</returns>
				// Token: 0x17001AE0 RID: 6880
				// (get) Token: 0x06007522 RID: 29986 RVA: 0x001A776D File Offset: 0x001A596D
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalScroll.disabled == null)
						{
							VisualStyleElement.Window.HorizontalScroll.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalScroll.part, 4);
						}
						return VisualStyleElement.Window.HorizontalScroll.disabled;
					}
				}

				// Token: 0x040047A0 RID: 18336
				private static readonly int part = 25;

				// Token: 0x040047A1 RID: 18337
				private static VisualStyleElement normal;

				// Token: 0x040047A2 RID: 18338
				private static VisualStyleElement hot;

				// Token: 0x040047A3 RID: 18339
				private static VisualStyleElement pressed;

				// Token: 0x040047A4 RID: 18340
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the horizontal scroll box (also known as the thumb) of a window. This class cannot be inherited.</summary>
			// Token: 0x0200096E RID: 2414
			public static class HorizontalThumb
			{
				/// <summary>Gets a visual style element that represents a horizontal scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the normal state.</returns>
				// Token: 0x17001AE1 RID: 6881
				// (get) Token: 0x06007524 RID: 29988 RVA: 0x001A7799 File Offset: 0x001A5999
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.normal == null)
						{
							VisualStyleElement.Window.HorizontalThumb.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 1);
						}
						return VisualStyleElement.Window.HorizontalThumb.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the hot state.</returns>
				// Token: 0x17001AE2 RID: 6882
				// (get) Token: 0x06007525 RID: 29989 RVA: 0x001A77BC File Offset: 0x001A59BC
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.hot == null)
						{
							VisualStyleElement.Window.HorizontalThumb.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 2);
						}
						return VisualStyleElement.Window.HorizontalThumb.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the pressed state.</returns>
				// Token: 0x17001AE3 RID: 6883
				// (get) Token: 0x06007526 RID: 29990 RVA: 0x001A77DF File Offset: 0x001A59DF
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.pressed == null)
						{
							VisualStyleElement.Window.HorizontalThumb.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 3);
						}
						return VisualStyleElement.Window.HorizontalThumb.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a horizontal scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a horizontal scroll box in the disabled state.</returns>
				// Token: 0x17001AE4 RID: 6884
				// (get) Token: 0x06007527 RID: 29991 RVA: 0x001A7802 File Offset: 0x001A5A02
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.HorizontalThumb.disabled == null)
						{
							VisualStyleElement.Window.HorizontalThumb.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.HorizontalThumb.part, 4);
						}
						return VisualStyleElement.Window.HorizontalThumb.disabled;
					}
				}

				// Token: 0x040047A5 RID: 18341
				private static readonly int part = 26;

				// Token: 0x040047A6 RID: 18342
				private static VisualStyleElement normal;

				// Token: 0x040047A7 RID: 18343
				private static VisualStyleElement hot;

				// Token: 0x040047A8 RID: 18344
				private static VisualStyleElement pressed;

				// Token: 0x040047A9 RID: 18345
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the vertical scroll bar of a window. This class cannot be inherited.</summary>
			// Token: 0x0200096F RID: 2415
			public static class VerticalScroll
			{
				/// <summary>Gets a visual style element that represents a vertical scroll bar in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the normal state.</returns>
				// Token: 0x17001AE5 RID: 6885
				// (get) Token: 0x06007529 RID: 29993 RVA: 0x001A782E File Offset: 0x001A5A2E
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.normal == null)
						{
							VisualStyleElement.Window.VerticalScroll.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 1);
						}
						return VisualStyleElement.Window.VerticalScroll.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll bar in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the hot state.</returns>
				// Token: 0x17001AE6 RID: 6886
				// (get) Token: 0x0600752A RID: 29994 RVA: 0x001A7851 File Offset: 0x001A5A51
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.hot == null)
						{
							VisualStyleElement.Window.VerticalScroll.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 2);
						}
						return VisualStyleElement.Window.VerticalScroll.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll bar in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the pressed state.</returns>
				// Token: 0x17001AE7 RID: 6887
				// (get) Token: 0x0600752B RID: 29995 RVA: 0x001A7874 File Offset: 0x001A5A74
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.pressed == null)
						{
							VisualStyleElement.Window.VerticalScroll.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 3);
						}
						return VisualStyleElement.Window.VerticalScroll.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll bar in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll bar in the disabled state.</returns>
				// Token: 0x17001AE8 RID: 6888
				// (get) Token: 0x0600752C RID: 29996 RVA: 0x001A7897 File Offset: 0x001A5A97
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.VerticalScroll.disabled == null)
						{
							VisualStyleElement.Window.VerticalScroll.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalScroll.part, 4);
						}
						return VisualStyleElement.Window.VerticalScroll.disabled;
					}
				}

				// Token: 0x040047AA RID: 18346
				private static readonly int part = 27;

				// Token: 0x040047AB RID: 18347
				private static VisualStyleElement normal;

				// Token: 0x040047AC RID: 18348
				private static VisualStyleElement hot;

				// Token: 0x040047AD RID: 18349
				private static VisualStyleElement pressed;

				// Token: 0x040047AE RID: 18350
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> objects for each state of the vertical scroll box (also known as the thumb) of a window. This class cannot be inherited.</summary>
			// Token: 0x02000970 RID: 2416
			public static class VerticalThumb
			{
				/// <summary>Gets a visual style element that represents a vertical scroll box in the normal state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the normal state.</returns>
				// Token: 0x17001AE9 RID: 6889
				// (get) Token: 0x0600752E RID: 29998 RVA: 0x001A78C3 File Offset: 0x001A5AC3
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.normal == null)
						{
							VisualStyleElement.Window.VerticalThumb.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 1);
						}
						return VisualStyleElement.Window.VerticalThumb.normal;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the hot state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the hot state.</returns>
				// Token: 0x17001AEA RID: 6890
				// (get) Token: 0x0600752F RID: 29999 RVA: 0x001A78E6 File Offset: 0x001A5AE6
				public static VisualStyleElement Hot
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.hot == null)
						{
							VisualStyleElement.Window.VerticalThumb.hot = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 2);
						}
						return VisualStyleElement.Window.VerticalThumb.hot;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the pressed state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the pressed state.</returns>
				// Token: 0x17001AEB RID: 6891
				// (get) Token: 0x06007530 RID: 30000 RVA: 0x001A7909 File Offset: 0x001A5B09
				public static VisualStyleElement Pressed
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.pressed == null)
						{
							VisualStyleElement.Window.VerticalThumb.pressed = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 3);
						}
						return VisualStyleElement.Window.VerticalThumb.pressed;
					}
				}

				/// <summary>Gets a visual style element that represents a vertical scroll box in the disabled state.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents a vertical scroll box in the disabled state.</returns>
				// Token: 0x17001AEC RID: 6892
				// (get) Token: 0x06007531 RID: 30001 RVA: 0x001A792C File Offset: 0x001A5B2C
				public static VisualStyleElement Disabled
				{
					get
					{
						if (VisualStyleElement.Window.VerticalThumb.disabled == null)
						{
							VisualStyleElement.Window.VerticalThumb.disabled = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.VerticalThumb.part, 4);
						}
						return VisualStyleElement.Window.VerticalThumb.disabled;
					}
				}

				// Token: 0x040047AF RID: 18351
				private static readonly int part = 28;

				// Token: 0x040047B0 RID: 18352
				private static VisualStyleElement normal;

				// Token: 0x040047B1 RID: 18353
				private static VisualStyleElement hot;

				// Token: 0x040047B2 RID: 18354
				private static VisualStyleElement pressed;

				// Token: 0x040047B3 RID: 18355
				private static VisualStyleElement disabled;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the background of a dialog box. This class cannot be inherited.</summary>
			// Token: 0x02000971 RID: 2417
			public static class Dialog
			{
				/// <summary>Gets a visual style element that represents the background of a dialog box.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the background of a dialog box.</returns>
				// Token: 0x17001AED RID: 6893
				// (get) Token: 0x06007533 RID: 30003 RVA: 0x001A7958 File Offset: 0x001A5B58
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.Dialog.normal == null)
						{
							VisualStyleElement.Window.Dialog.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.Dialog.part, 0);
						}
						return VisualStyleElement.Window.Dialog.normal;
					}
				}

				// Token: 0x040047B4 RID: 18356
				private static readonly int part = 29;

				// Token: 0x040047B5 RID: 18357
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a window. This class cannot be inherited.</summary>
			// Token: 0x02000972 RID: 2418
			public static class CaptionSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the title bar of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a window.</returns>
				// Token: 0x17001AEE RID: 6894
				// (get) Token: 0x06007535 RID: 30005 RVA: 0x001A7984 File Offset: 0x001A5B84
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.CaptionSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.CaptionSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.CaptionSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.CaptionSizingTemplate.normal;
					}
				}

				// Token: 0x040047B6 RID: 18358
				private static readonly int part = 30;

				// Token: 0x040047B7 RID: 18359
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000973 RID: 2419
			public static class SmallCaptionSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the title bar of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the title bar of a small window.</returns>
				// Token: 0x17001AEF RID: 6895
				// (get) Token: 0x06007537 RID: 30007 RVA: 0x001A79B0 File Offset: 0x001A5BB0
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallCaptionSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallCaptionSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallCaptionSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallCaptionSizingTemplate.normal;
					}
				}

				// Token: 0x040047B8 RID: 18360
				private static readonly int part = 31;

				// Token: 0x040047B9 RID: 18361
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a window. This class cannot be inherited.</summary>
			// Token: 0x02000974 RID: 2420
			public static class FrameLeftSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the left border of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a window.</returns>
				// Token: 0x17001AF0 RID: 6896
				// (get) Token: 0x06007539 RID: 30009 RVA: 0x001A79DC File Offset: 0x001A5BDC
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.FrameLeftSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.FrameLeftSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameLeftSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.FrameLeftSizingTemplate.normal;
					}
				}

				// Token: 0x040047BA RID: 18362
				private static readonly int part = 32;

				// Token: 0x040047BB RID: 18363
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000975 RID: 2421
			public static class SmallFrameLeftSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the left border of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the left border of a small window.</returns>
				// Token: 0x17001AF1 RID: 6897
				// (get) Token: 0x0600753B RID: 30011 RVA: 0x001A7A08 File Offset: 0x001A5C08
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameLeftSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallFrameLeftSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameLeftSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallFrameLeftSizingTemplate.normal;
					}
				}

				// Token: 0x040047BC RID: 18364
				private static readonly int part = 33;

				// Token: 0x040047BD RID: 18365
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a window. This class cannot be inherited.</summary>
			// Token: 0x02000976 RID: 2422
			public static class FrameRightSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the right border of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a window.</returns>
				// Token: 0x17001AF2 RID: 6898
				// (get) Token: 0x0600753D RID: 30013 RVA: 0x001A7A34 File Offset: 0x001A5C34
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.FrameRightSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.FrameRightSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameRightSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.FrameRightSizingTemplate.normal;
					}
				}

				// Token: 0x040047BE RID: 18366
				private static readonly int part = 34;

				// Token: 0x040047BF RID: 18367
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> for the sizing template of the right border of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000977 RID: 2423
			public static class SmallFrameRightSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the right border of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the right border of a small window.</returns>
				// Token: 0x17001AF3 RID: 6899
				// (get) Token: 0x0600753F RID: 30015 RVA: 0x001A7A60 File Offset: 0x001A5C60
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameRightSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallFrameRightSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameRightSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallFrameRightSizingTemplate.normal;
					}
				}

				// Token: 0x040047C0 RID: 18368
				private static readonly int part = 35;

				// Token: 0x040047C1 RID: 18369
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a window. This class cannot be inherited.</summary>
			// Token: 0x02000978 RID: 2424
			public static class FrameBottomSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the bottom border of a window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a window.</returns>
				// Token: 0x17001AF4 RID: 6900
				// (get) Token: 0x06007541 RID: 30017 RVA: 0x001A7A8C File Offset: 0x001A5C8C
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.FrameBottomSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.FrameBottomSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.FrameBottomSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.FrameBottomSizingTemplate.normal;
					}
				}

				// Token: 0x040047C2 RID: 18370
				private static readonly int part = 36;

				// Token: 0x040047C3 RID: 18371
				private static VisualStyleElement normal;
			}

			/// <summary>Provides a <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a small window. This class cannot be inherited.</summary>
			// Token: 0x02000979 RID: 2425
			public static class SmallFrameBottomSizingTemplate
			{
				/// <summary>Gets a visual style element that represents the sizing template of the bottom border of a small window.</summary>
				/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleElement" /> that represents the sizing template of the bottom border of a small window.</returns>
				// Token: 0x17001AF5 RID: 6901
				// (get) Token: 0x06007543 RID: 30019 RVA: 0x001A7AB8 File Offset: 0x001A5CB8
				public static VisualStyleElement Normal
				{
					get
					{
						if (VisualStyleElement.Window.SmallFrameBottomSizingTemplate.normal == null)
						{
							VisualStyleElement.Window.SmallFrameBottomSizingTemplate.normal = new VisualStyleElement(VisualStyleElement.Window.className, VisualStyleElement.Window.SmallFrameBottomSizingTemplate.part, 0);
						}
						return VisualStyleElement.Window.SmallFrameBottomSizingTemplate.normal;
					}
				}

				// Token: 0x040047C4 RID: 18372
				private static readonly int part = 37;

				// Token: 0x040047C5 RID: 18373
				private static VisualStyleElement normal;
			}
		}
	}
}
