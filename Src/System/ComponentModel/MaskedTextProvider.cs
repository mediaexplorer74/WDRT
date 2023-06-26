using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace System.ComponentModel
{
	/// <summary>Represents a mask-parsing service that can be used by any number of controls that support masking, such as the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
	// Token: 0x0200058D RID: 1421
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class MaskedTextProvider : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		// Token: 0x0600345F RID: 13407 RVA: 0x000E48C7 File Offset: 0x000E2AC7
		public MaskedTextProvider(string mask)
			: this(mask, null, true, '_', '\0', false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		/// <param name="restrictToAscii">
		///   <see langword="true" /> to restrict input to ASCII-compatible characters; otherwise <see langword="false" /> to allow the entire Unicode set.</param>
		// Token: 0x06003460 RID: 13408 RVA: 0x000E48D6 File Offset: 0x000E2AD6
		public MaskedTextProvider(string mask, bool restrictToAscii)
			: this(mask, null, true, '_', '\0', restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and culture.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		// Token: 0x06003461 RID: 13409 RVA: 0x000E48E5 File Offset: 0x000E2AE5
		public MaskedTextProvider(string mask, CultureInfo culture)
			: this(mask, culture, true, '_', '\0', false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="restrictToAscii">
		///   <see langword="true" /> to restrict input to ASCII-compatible characters; otherwise <see langword="false" /> to allow the entire Unicode set.</param>
		// Token: 0x06003462 RID: 13410 RVA: 0x000E48F4 File Offset: 0x000E2AF4
		public MaskedTextProvider(string mask, CultureInfo culture, bool restrictToAscii)
			: this(mask, culture, true, '_', '\0', restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">
		///   <see langword="true" /> to allow the prompt character as input; otherwise <see langword="false" />.</param>
		// Token: 0x06003463 RID: 13411 RVA: 0x000E4903 File Offset: 0x000E2B03
		public MaskedTextProvider(string mask, char passwordChar, bool allowPromptAsInput)
			: this(mask, null, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">
		///   <see langword="true" /> to allow the prompt character as input; otherwise <see langword="false" />.</param>
		// Token: 0x06003464 RID: 13412 RVA: 0x000E4912 File Offset: 0x000E2B12
		public MaskedTextProvider(string mask, CultureInfo culture, char passwordChar, bool allowPromptAsInput)
			: this(mask, culture, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, prompt usage value, prompt character, password character, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="allowPromptAsInput">A <see cref="T:System.Boolean" /> value that specifies whether the prompt character should be allowed as a valid input character.</param>
		/// <param name="promptChar">A <see cref="T:System.Char" /> that will be displayed as a placeholder for user input.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="restrictToAscii">
		///   <see langword="true" /> to restrict input to ASCII-compatible characters; otherwise <see langword="false" /> to allow the entire Unicode set.</param>
		/// <exception cref="T:System.ArgumentException">The mask parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />.  
		///  -or-  
		///  The mask contains one or more non-printable characters.</exception>
		// Token: 0x06003465 RID: 13413 RVA: 0x000E4924 File Offset: 0x000E2B24
		public MaskedTextProvider(string mask, CultureInfo culture, bool allowPromptAsInput, char promptChar, char passwordChar, bool restrictToAscii)
		{
			if (string.IsNullOrEmpty(mask))
			{
				throw new ArgumentException(SR.GetString("MaskedTextProviderMaskNullOrEmpty"), "mask");
			}
			foreach (char c in mask)
			{
				if (!MaskedTextProvider.IsPrintableChar(c))
				{
					throw new ArgumentException(SR.GetString("MaskedTextProviderMaskInvalidChar"));
				}
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			this.flagState = default(BitVector32);
			this.mask = mask;
			this.promptChar = promptChar;
			this.passwordChar = passwordChar;
			if (culture.IsNeutralCulture)
			{
				foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (culture.Equals(cultureInfo.Parent))
					{
						this.culture = cultureInfo;
						break;
					}
				}
				if (this.culture == null)
				{
					this.culture = CultureInfo.InvariantCulture;
				}
			}
			else
			{
				this.culture = culture;
			}
			if (!this.culture.IsReadOnly)
			{
				this.culture = CultureInfo.ReadOnly(this.culture);
			}
			this.flagState[MaskedTextProvider.ALLOW_PROMPT_AS_INPUT] = allowPromptAsInput;
			this.flagState[MaskedTextProvider.ASCII_ONLY] = restrictToAscii;
			this.flagState[MaskedTextProvider.INCLUDE_PROMPT] = false;
			this.flagState[MaskedTextProvider.INCLUDE_LITERALS] = true;
			this.flagState[MaskedTextProvider.RESET_ON_PROMPT] = true;
			this.flagState[MaskedTextProvider.SKIP_SPACE] = true;
			this.flagState[MaskedTextProvider.RESET_ON_LITERALS] = true;
			this.Initialize();
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000E4AAC File Offset: 0x000E2CAC
		private void Initialize()
		{
			this.testString = new StringBuilder();
			this.stringDescriptor = new List<MaskedTextProvider.CharDescriptor>();
			MaskedTextProvider.CaseConversion caseConversion = MaskedTextProvider.CaseConversion.None;
			bool flag = false;
			int num = 0;
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Literal;
			string text = string.Empty;
			int i = 0;
			while (i < this.mask.Length)
			{
				char c = this.mask[i];
				if (!flag)
				{
					if (c <= 'C')
					{
						switch (c)
						{
						case '#':
							goto IL_19E;
						case '$':
							text = this.culture.NumberFormat.CurrencySymbol;
							charType = MaskedTextProvider.CharType.Separator;
							goto IL_1BE;
						case '%':
							goto IL_1B8;
						case '&':
							break;
						default:
							switch (c)
							{
							case ',':
								text = this.culture.NumberFormat.NumberGroupSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_1BE;
							case '-':
								goto IL_1B8;
							case '.':
								text = this.culture.NumberFormat.NumberDecimalSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_1BE;
							case '/':
								text = this.culture.DateTimeFormat.DateSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_1BE;
							case '0':
								break;
							default:
								switch (c)
								{
								case '9':
								case '?':
								case 'C':
									goto IL_19E;
								case ':':
									text = this.culture.DateTimeFormat.TimeSeparator;
									charType = MaskedTextProvider.CharType.Separator;
									goto IL_1BE;
								case ';':
								case '=':
								case '@':
								case 'B':
									goto IL_1B8;
								case '<':
									caseConversion = MaskedTextProvider.CaseConversion.ToLower;
									goto IL_22A;
								case '>':
									caseConversion = MaskedTextProvider.CaseConversion.ToUpper;
									goto IL_22A;
								case 'A':
									break;
								default:
									goto IL_1B8;
								}
								break;
							}
							break;
						}
					}
					else if (c <= '\\')
					{
						if (c != 'L')
						{
							if (c != '\\')
							{
								goto IL_1B8;
							}
							flag = true;
							charType = MaskedTextProvider.CharType.Literal;
							goto IL_22A;
						}
					}
					else
					{
						if (c == 'a')
						{
							goto IL_19E;
						}
						if (c != '|')
						{
							goto IL_1B8;
						}
						caseConversion = MaskedTextProvider.CaseConversion.None;
						goto IL_22A;
					}
					this.requiredEditChars++;
					c = this.promptChar;
					charType = MaskedTextProvider.CharType.EditRequired;
					goto IL_1BE;
					IL_19E:
					this.optionalEditChars++;
					c = this.promptChar;
					charType = MaskedTextProvider.CharType.EditOptional;
					goto IL_1BE;
					IL_1B8:
					charType = MaskedTextProvider.CharType.Literal;
					goto IL_1BE;
				}
				flag = false;
				goto IL_1BE;
				IL_22A:
				i++;
				continue;
				IL_1BE:
				MaskedTextProvider.CharDescriptor charDescriptor = new MaskedTextProvider.CharDescriptor(i, charType);
				if (MaskedTextProvider.IsEditPosition(charDescriptor))
				{
					charDescriptor.CaseConversion = caseConversion;
				}
				if (charType != MaskedTextProvider.CharType.Separator)
				{
					text = c.ToString();
				}
				foreach (char c2 in text)
				{
					this.testString.Append(c2);
					this.stringDescriptor.Add(charDescriptor);
					num++;
				}
				goto IL_22A;
			}
			this.testString.Capacity = this.testString.Length;
		}

		/// <summary>Gets a value indicating whether the prompt character should be treated as a valid input character or not.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can enter <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" /> into the control; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000E4D11 File Offset: 0x000E2F11
		public bool AllowPromptAsInput
		{
			get
			{
				return this.flagState[MaskedTextProvider.ALLOW_PROMPT_AS_INPUT];
			}
		}

		/// <summary>Gets the number of editable character positions that have already been successfully assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions in the input mask that have already been assigned a character value in the formatted string.</returns>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000E4D23 File Offset: 0x000E2F23
		public int AssignedEditPositionCount
		{
			get
			{
				return this.assignedCharCount;
			}
		}

		/// <summary>Gets the number of editable character positions in the input mask that have not yet been assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions that not yet been assigned a character value.</returns>
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000E4D2B File Offset: 0x000E2F2B
		public int AvailableEditPositionCount
		{
			get
			{
				return this.EditPositionCount - this.assignedCharCount;
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.ComponentModel.MaskedTextProvider" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.MaskedTextProvider" /> object this method creates, cast as an object.</returns>
		// Token: 0x0600346A RID: 13418 RVA: 0x000E4D3C File Offset: 0x000E2F3C
		public object Clone()
		{
			Type type = base.GetType();
			MaskedTextProvider maskedTextProvider;
			if (type == MaskedTextProvider.maskTextProviderType)
			{
				maskedTextProvider = new MaskedTextProvider(this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly);
			}
			else
			{
				object[] array = new object[] { this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly };
				maskedTextProvider = SecurityUtils.SecureCreateInstance(type, array) as MaskedTextProvider;
			}
			maskedTextProvider.ResetOnPrompt = false;
			maskedTextProvider.ResetOnSpace = false;
			maskedTextProvider.SkipLiterals = false;
			for (int i = 0; i < this.testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
				{
					maskedTextProvider.Replace(this.testString[i], i);
				}
			}
			maskedTextProvider.ResetOnPrompt = this.ResetOnPrompt;
			maskedTextProvider.ResetOnSpace = this.ResetOnSpace;
			maskedTextProvider.SkipLiterals = this.SkipLiterals;
			maskedTextProvider.IncludeLiterals = this.IncludeLiterals;
			maskedTextProvider.IncludePrompt = this.IncludePrompt;
			return maskedTextProvider;
		}

		/// <summary>Gets the culture that determines the value of the localizable separators and placeholders in the input mask.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> containing the culture information associated with the input mask.</returns>
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x000E4E83 File Offset: 0x000E3083
		public CultureInfo Culture
		{
			get
			{
				return this.culture;
			}
		}

		/// <summary>Gets the default password character used obscure user input.</summary>
		/// <returns>A <see cref="T:System.Char" /> that represents the default password character.</returns>
		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000E4E8B File Offset: 0x000E308B
		public static char DefaultPasswordChar
		{
			get
			{
				return '*';
			}
		}

		/// <summary>Gets the number of editable positions in the formatted string.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable positions in the formatted string.</returns>
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000E4E8F File Offset: 0x000E308F
		public int EditPositionCount
		{
			get
			{
				return this.optionalEditChars + this.requiredEditChars;
			}
		}

		/// <summary>Gets a newly created enumerator for the editable positions in the formatted string.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that supports enumeration over the editable positions in the formatted string.</returns>
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000E4EA0 File Offset: 0x000E30A0
		public IEnumerator EditPositions
		{
			get
			{
				List<int> list = new List<int>();
				int num = 0;
				foreach (MaskedTextProvider.CharDescriptor charDescriptor in this.stringDescriptor)
				{
					if (MaskedTextProvider.IsEditPosition(charDescriptor))
					{
						list.Add(num);
					}
					num++;
				}
				return ((IEnumerable)list).GetEnumerator();
			}
		}

		/// <summary>Gets or sets a value that indicates whether literal characters in the input mask should be included in the formatted string.</summary>
		/// <returns>
		///   <see langword="true" /> if literals are included; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000E4F10 File Offset: 0x000E3110
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000E4F22 File Offset: 0x000E3122
		public bool IncludeLiterals
		{
			get
			{
				return this.flagState[MaskedTextProvider.INCLUDE_LITERALS];
			}
			set
			{
				this.flagState[MaskedTextProvider.INCLUDE_LITERALS] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is used to represent the absence of user input when displaying the formatted string.</summary>
		/// <returns>
		///   <see langword="true" /> if the prompt character is used to represent the positions where no user input was provided; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000E4F35 File Offset: 0x000E3135
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x000E4F47 File Offset: 0x000E3147
		public bool IncludePrompt
		{
			get
			{
				return this.flagState[MaskedTextProvider.INCLUDE_PROMPT];
			}
			set
			{
				this.flagState[MaskedTextProvider.INCLUDE_PROMPT] = value;
			}
		}

		/// <summary>Gets a value indicating whether the mask accepts characters outside of the ASCII character set.</summary>
		/// <returns>
		///   <see langword="true" /> if only ASCII is accepted; <see langword="false" /> if <see cref="T:System.ComponentModel.MaskedTextProvider" /> can accept any arbitrary Unicode character. The default is <see langword="false" />.</returns>
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x000E4F5A File Offset: 0x000E315A
		public bool AsciiOnly
		{
			get
			{
				return this.flagState[MaskedTextProvider.ASCII_ONLY];
			}
		}

		/// <summary>Gets or sets a value that determines whether password protection should be applied to the formatted string.</summary>
		/// <returns>
		///   <see langword="true" /> if the input string is to be treated as a password string; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000E4F6C File Offset: 0x000E316C
		// (set) Token: 0x06003475 RID: 13429 RVA: 0x000E4F77 File Offset: 0x000E3177
		public bool IsPassword
		{
			get
			{
				return this.passwordChar > '\0';
			}
			set
			{
				if (this.IsPassword != value)
				{
					this.passwordChar = (value ? MaskedTextProvider.DefaultPasswordChar : '\0');
				}
			}
		}

		/// <summary>Gets the upper bound of the range of invalid indexes.</summary>
		/// <returns>A value representing the largest invalid index, as determined by the provider implementation. For example, if the lowest valid index is 0, this property will return -1.</returns>
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000E4F93 File Offset: 0x000E3193
		public static int InvalidIndex
		{
			get
			{
				return -1;
			}
		}

		/// <summary>Gets the index in the mask of the rightmost input character that has been assigned to the mask.</summary>
		/// <returns>If at least one input character has been assigned to the mask, an <see cref="T:System.Int32" /> containing the index of rightmost assigned position; otherwise, if no position has been assigned, <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000E4F96 File Offset: 0x000E3196
		public int LastAssignedPosition
		{
			get
			{
				return this.FindAssignedEditPositionFrom(this.testString.Length - 1, false);
			}
		}

		/// <summary>Gets the length of the mask, absent any mask modifier characters.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of positions in the mask, excluding characters that modify mask input.</returns>
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000E4FAC File Offset: 0x000E31AC
		public int Length
		{
			get
			{
				return this.testString.Length;
			}
		}

		/// <summary>Gets the input mask.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the full mask.</returns>
		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003479 RID: 13433 RVA: 0x000E4FB9 File Offset: 0x000E31B9
		public string Mask
		{
			get
			{
				return this.mask;
			}
		}

		/// <summary>Gets a value indicating whether all required inputs have been entered into the formatted string.</summary>
		/// <returns>
		///   <see langword="true" /> if all required input has been entered into the mask; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000E4FC1 File Offset: 0x000E31C1
		public bool MaskCompleted
		{
			get
			{
				return this.requiredCharCount == this.requiredEditChars;
			}
		}

		/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the formatted string.</summary>
		/// <returns>
		///   <see langword="true" /> if all required and optional inputs have been entered; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000E4FD1 File Offset: 0x000E31D1
		public bool MaskFull
		{
			get
			{
				return this.assignedCharCount == this.EditPositionCount;
			}
		}

		/// <summary>Gets or sets the character to be substituted for the actual input characters.</summary>
		/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
		/// <exception cref="T:System.InvalidOperationException">The password character specified when setting this property is the same as the current prompt character, <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000E4FE1 File Offset: 0x000E31E1
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x000E4FEC File Offset: 0x000E31EC
		public char PasswordChar
		{
			get
			{
				return this.passwordChar;
			}
			set
			{
				if (value == this.promptChar)
				{
					throw new InvalidOperationException(SR.GetString("MaskedTextProviderPasswordAndPromptCharError"));
				}
				if (!MaskedTextProvider.IsValidPasswordChar(value) && value != '\0')
				{
					throw new ArgumentException(SR.GetString("MaskedTextProviderInvalidCharError"));
				}
				if (value != this.passwordChar)
				{
					this.passwordChar = value;
				}
			}
		}

		/// <summary>Gets or sets the character used to represent the absence of user input for all available edit positions.</summary>
		/// <returns>The character used to prompt the user for input. The default is an underscore (_).</returns>
		/// <exception cref="T:System.InvalidOperationException">The prompt character specified when setting this property is the same as the current password character, <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000E503D File Offset: 0x000E323D
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000E5048 File Offset: 0x000E3248
		public char PromptChar
		{
			get
			{
				return this.promptChar;
			}
			set
			{
				if (value == this.passwordChar)
				{
					throw new InvalidOperationException(SR.GetString("MaskedTextProviderPasswordAndPromptCharError"));
				}
				if (!MaskedTextProvider.IsPrintableChar(value))
				{
					throw new ArgumentException(SR.GetString("MaskedTextProviderInvalidCharError"));
				}
				if (value != this.promptChar)
				{
					this.promptChar = value;
					for (int i = 0; i < this.testString.Length; i++)
					{
						MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
						if (this.IsEditPosition(i) && !charDescriptor.IsAssigned)
						{
							this.testString[i] = this.promptChar;
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, <see langword="false" /> to indicate that the prompt character is to be processed as a normal input character. The default is <see langword="true" />.</returns>
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000E50DC File Offset: 0x000E32DC
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000E50EE File Offset: 0x000E32EE
		public bool ResetOnPrompt
		{
			get
			{
				return this.flagState[MaskedTextProvider.RESET_ON_PROMPT];
			}
			set
			{
				this.flagState[MaskedTextProvider.RESET_ON_PROMPT] = value;
			}
		}

		/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
		/// <returns>
		///   <see langword="true" /> if the space input character causes the current editable position in the mask to be reset; otherwise, <see langword="false" /> to indicate that it is to be processed as a normal input character. The default is <see langword="true" />.</returns>
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000E5101 File Offset: 0x000E3301
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000E5113 File Offset: 0x000E3313
		public bool ResetOnSpace
		{
			get
			{
				return this.flagState[MaskedTextProvider.SKIP_SPACE];
			}
			set
			{
				this.flagState[MaskedTextProvider.SKIP_SPACE] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether literal character positions in the mask can be overwritten by their same values.</summary>
		/// <returns>
		///   <see langword="true" /> to allow literals to be added back; otherwise, <see langword="false" /> to not allow the user to overwrite literal characters. The default is <see langword="true" />.</returns>
		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000E5126 File Offset: 0x000E3326
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x000E5138 File Offset: 0x000E3338
		public bool SkipLiterals
		{
			get
			{
				return this.flagState[MaskedTextProvider.RESET_ON_LITERALS];
			}
			set
			{
				this.flagState[MaskedTextProvider.RESET_ON_LITERALS] = value;
			}
		}

		/// <summary>Gets the element at the specified position in the formatted string.</summary>
		/// <param name="index">A zero-based index of the element to retrieve.</param>
		/// <returns>The <see cref="T:System.Char" /> at the specified position in the formatted string.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than or equal to the <see cref="P:System.ComponentModel.MaskedTextProvider.Length" /> of the mask.</exception>
		// Token: 0x17000CE4 RID: 3300
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.testString.Length)
				{
					throw new IndexOutOfRangeException(index.ToString(CultureInfo.CurrentCulture));
				}
				return this.testString[index];
			}
		}

		/// <summary>Adds the specified input character to the end of the formatted string.</summary>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string.</param>
		/// <returns>
		///   <see langword="true" /> if the input character was added successfully; otherwise <see langword="false" />.</returns>
		// Token: 0x06003487 RID: 13447 RVA: 0x000E5180 File Offset: 0x000E3380
		public bool Add(char input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the specified input character to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string.</param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the input character was added successfully; otherwise <see langword="false" />.</returns>
		// Token: 0x06003488 RID: 13448 RVA: 0x000E5198 File Offset: 0x000E3398
		public bool Add(char input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == this.testString.Length - 1)
			{
				testPosition = this.testString.Length;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			testPosition = lastAssignedPosition + 1;
			testPosition = this.FindEditPositionFrom(testPosition, true);
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string.</summary>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters from the input string were added successfully; otherwise <see langword="false" /> to indicate that no characters were added.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003489 RID: 13449 RVA: 0x000E5208 File Offset: 0x000E3408
		public bool Add(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string.</param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters from the input string were added successfully; otherwise <see langword="false" /> to indicate that no characters were added.</returns>
		// Token: 0x0600348A RID: 13450 RVA: 0x000E5220 File Offset: 0x000E3420
		public bool Add(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			testPosition = this.LastAssignedPosition + 1;
			if (input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestSetString(input, testPosition, out testPosition, out resultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters.</summary>
		// Token: 0x0600348B RID: 13451 RVA: 0x000E5254 File Offset: 0x000E3454
		public void Clear()
		{
			MaskedTextResultHint maskedTextResultHint;
			this.Clear(out maskedTextResultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters, and then outputs descriptive information.</summary>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600348C RID: 13452 RVA: 0x000E526C File Offset: 0x000E346C
		public void Clear(out MaskedTextResultHint resultHint)
		{
			if (this.assignedCharCount == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return;
			}
			resultHint = MaskedTextResultHint.Success;
			for (int i = 0; i < this.testString.Length; i++)
			{
				this.ResetChar(i);
			}
		}

		/// <summary>Returns the position of the first assigned editable position after the specified position using the specified search direction.</summary>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x0600348D RID: 13453 RVA: 0x000E52A8 File Offset: 0x000E34A8
		public int FindAssignedEditPositionFrom(int position, bool direction)
		{
			if (this.assignedCharCount == 0)
			{
				return -1;
			}
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindAssignedEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first assigned editable position between the specified positions using the specified search direction.</summary>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x0600348E RID: 13454 RVA: 0x000E52E1 File Offset: 0x000E34E1
		public int FindAssignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (this.assignedCharCount == 0)
			{
				return -1;
			}
			return this.FindEditPositionInRange(startPosition, endPosition, direction, 2);
		}

		/// <summary>Returns the position of the first editable position after the specified position using the specified search direction.</summary>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x0600348F RID: 13455 RVA: 0x000E52F8 File Offset: 0x000E34F8
		public int FindEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first editable position between the specified positions using the specified search direction.</summary>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x06003490 RID: 13456 RVA: 0x000E5328 File Offset: 0x000E3528
		public int FindEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.EditOptional | MaskedTextProvider.CharType.EditRequired;
			return this.FindPositionInRange(startPosition, endPosition, direction, charType);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000E5344 File Offset: 0x000E3544
		private int FindEditPositionInRange(int startPosition, int endPosition, bool direction, byte assignedStatus)
		{
			int num;
			for (;;)
			{
				num = this.FindEditPositionInRange(startPosition, endPosition, direction);
				if (num == -1)
				{
					return -1;
				}
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num];
				if (assignedStatus != 1)
				{
					if (assignedStatus != 2)
					{
						break;
					}
					if (charDescriptor.IsAssigned)
					{
						return num;
					}
				}
				else if (!charDescriptor.IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
				if (startPosition > endPosition)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Returns the position of the first non-editable position after the specified position using the specified search direction.</summary>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x06003492 RID: 13458 RVA: 0x000E53A4 File Offset: 0x000E35A4
		public int FindNonEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindNonEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first non-editable position between the specified positions using the specified search direction.</summary>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x06003493 RID: 13459 RVA: 0x000E53D4 File Offset: 0x000E35D4
		public int FindNonEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Separator | MaskedTextProvider.CharType.Literal;
			return this.FindPositionInRange(startPosition, endPosition, direction, charType);
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000E53F0 File Offset: 0x000E35F0
		private int FindPositionInRange(int startPosition, int endPosition, bool direction, MaskedTextProvider.CharType charTypeFlags)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this.testString.Length)
			{
				endPosition = this.testString.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return -1;
			}
			while (startPosition <= endPosition)
			{
				int num;
				if (!direction)
				{
					endPosition = (num = endPosition) - 1;
				}
				else
				{
					startPosition = (num = startPosition) + 1;
				}
				int num2 = num;
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num2];
				if ((charDescriptor.CharType & charTypeFlags) == charDescriptor.CharType)
				{
					return num2;
				}
			}
			return -1;
		}

		/// <summary>Returns the position of the first unassigned editable position after the specified position using the specified search direction.</summary>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x06003495 RID: 13461 RVA: 0x000E5460 File Offset: 0x000E3660
		public int FindUnassignedEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this.testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindEditPositionInRange(num, num2, direction, 1);
		}

		/// <summary>Returns the position of the first unassigned editable position between the specified positions using the specified search direction.</summary>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either <see langword="true" /> to search forward or <see langword="false" /> to search backward.</param>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x06003496 RID: 13462 RVA: 0x000E5490 File Offset: 0x000E3690
		public int FindUnassignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			for (;;)
			{
				int num = this.FindEditPositionInRange(startPosition, endPosition, direction, 0);
				if (num == -1)
				{
					break;
				}
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num];
				if (!charDescriptor.IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
			}
			return -1;
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> denotes success or failure.</summary>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value typically obtained as an output parameter from a previous operation.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value represents a success; otherwise, <see langword="false" /> if it represents failure.</returns>
		// Token: 0x06003497 RID: 13463 RVA: 0x000E54D5 File Offset: 0x000E36D5
		public static bool GetOperationResultFromHint(MaskedTextResultHint hint)
		{
			return hint > MaskedTextResultHint.Unknown;
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted.</param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		/// <returns>
		///   <see langword="true" /> if the insertion was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003498 RID: 13464 RVA: 0x000E54DB File Offset: 0x000E36DB
		public bool InsertAt(char input, int position)
		{
			return position >= 0 && position < this.testString.Length && this.InsertAt(input.ToString(), position);
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string, returning the last insertion position and the status of the operation.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted.</param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the insertion was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003499 RID: 13465 RVA: 0x000E54FF File Offset: 0x000E36FF
		public bool InsertAt(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			return this.InsertAt(input.ToString(), position, out testPosition, out resultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string.</summary>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted.</param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <returns>
		///   <see langword="true" /> if the insertion was successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600349A RID: 13466 RVA: 0x000E5514 File Offset: 0x000E3714
		public bool InsertAt(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.InsertAt(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string, returning the last insertion position and the status of the operation.</summary>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted.</param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the insertion was successful; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600349B RID: 13467 RVA: 0x000E552D File Offset: 0x000E372D
		public bool InsertAt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.InsertAtInt(input, position, out testPosition, out resultHint, false);
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000E5568 File Offset: 0x000E3768
		private bool InsertAtInt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			if (input.Length == 0)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			if (!this.TestString(input, position, out testPosition, out resultHint))
			{
				return false;
			}
			int i = this.FindEditPositionFrom(position, true);
			bool flag = this.FindAssignedEditPositionInRange(i, testPosition, true) != -1;
			int lastAssignedPosition = this.LastAssignedPosition;
			if (flag && testPosition == this.testString.Length - 1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			int num = this.FindEditPositionFrom(testPosition + 1, true);
			if (flag)
			{
				MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
				while (num != -1)
				{
					MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
					if (charDescriptor.IsAssigned && !this.TestChar(this.testString[i], num, out maskedTextResultHint))
					{
						resultHint = maskedTextResultHint;
						testPosition = num;
						return false;
					}
					if (i != lastAssignedPosition)
					{
						i = this.FindEditPositionFrom(i + 1, true);
						num = this.FindEditPositionFrom(num + 1, true);
					}
					else
					{
						if (maskedTextResultHint > resultHint)
						{
							resultHint = maskedTextResultHint;
							goto IL_F3;
						}
						goto IL_F3;
					}
				}
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this.testString.Length;
				return false;
			}
			IL_F3:
			if (testOnly)
			{
				return true;
			}
			if (flag)
			{
				while (i >= position)
				{
					MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[i];
					if (charDescriptor2.IsAssigned)
					{
						this.SetChar(this.testString[i], num);
					}
					else
					{
						this.ResetChar(num);
					}
					num = this.FindEditPositionFrom(num - 1, false);
					i = this.FindEditPositionFrom(i - 1, false);
				}
			}
			this.SetString(input, position);
			return true;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000E56C9 File Offset: 0x000E38C9
		private static bool IsAscii(char c)
		{
			return c >= '!' && c <= '~';
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000E56DA File Offset: 0x000E38DA
		private static bool IsAciiAlphanumeric(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000E5701 File Offset: 0x000E3901
		private static bool IsAlphanumeric(char c)
		{
			return char.IsLetter(c) || char.IsDigit(c);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000E5713 File Offset: 0x000E3913
		private static bool IsAsciiLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		/// <summary>Determines whether the specified position is available for assignment.</summary>
		/// <param name="position">The zero-based position in the mask to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified position in the formatted string is editable and has not been assigned to yet; otherwise <see langword="false" />.</returns>
		// Token: 0x060034A1 RID: 13473 RVA: 0x000E5730 File Offset: 0x000E3930
		public bool IsAvailablePosition(int position)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return MaskedTextProvider.IsEditPosition(charDescriptor) && !charDescriptor.IsAssigned;
		}

		/// <summary>Determines whether the specified position is editable.</summary>
		/// <param name="position">The zero-based position in the mask to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified position in the formatted string is editable; otherwise <see langword="false" />.</returns>
		// Token: 0x060034A2 RID: 13474 RVA: 0x000E5774 File Offset: 0x000E3974
		public bool IsEditPosition(int position)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return MaskedTextProvider.IsEditPosition(charDescriptor);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000E57A8 File Offset: 0x000E39A8
		private static bool IsEditPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired || charDescriptor.CharType == MaskedTextProvider.CharType.EditOptional;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000E57BE File Offset: 0x000E39BE
		private static bool IsLiteralPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.Literal || charDescriptor.CharType == MaskedTextProvider.CharType.Separator;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000E57D4 File Offset: 0x000E39D4
		private static bool IsPrintableChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ';
		}

		/// <summary>Determines whether the specified character is a valid input character.</summary>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified character contains a valid input value; otherwise <see langword="false" />.</returns>
		// Token: 0x060034A6 RID: 13478 RVA: 0x000E57F5 File Offset: 0x000E39F5
		public static bool IsValidInputChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		/// <summary>Determines whether the specified character is a valid mask character.</summary>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified character contains a valid mask value; otherwise <see langword="false" />.</returns>
		// Token: 0x060034A7 RID: 13479 RVA: 0x000E57FD File Offset: 0x000E39FD
		public static bool IsValidMaskChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		/// <summary>Determines whether the specified character is a valid password character.</summary>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified character contains a valid password value; otherwise <see langword="false" />.</returns>
		// Token: 0x060034A8 RID: 13480 RVA: 0x000E5805 File Offset: 0x000E3A05
		public static bool IsValidPasswordChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c) || c == '\0';
		}

		/// <summary>Removes the last assigned character from the formatted string.</summary>
		/// <returns>
		///   <see langword="true" /> if the character was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034A9 RID: 13481 RVA: 0x000E5818 File Offset: 0x000E3A18
		public bool Remove()
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Remove(out num, out maskedTextResultHint);
		}

		/// <summary>Removes the last assigned character from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <param name="testPosition">The zero-based position in the formatted string where the character was actually removed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034AA RID: 13482 RVA: 0x000E5830 File Offset: 0x000E3A30
		public bool Remove(out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == -1)
			{
				testPosition = 0;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			this.ResetChar(lastAssignedPosition);
			testPosition = lastAssignedPosition;
			resultHint = MaskedTextResultHint.Success;
			return true;
		}

		/// <summary>Removes the assigned character at the specified position from the formatted string.</summary>
		/// <param name="position">The zero-based position of the assigned character to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034AB RID: 13483 RVA: 0x000E585E File Offset: 0x000E3A5E
		public bool RemoveAt(int position)
		{
			return this.RemoveAt(position, position);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string.</summary>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034AC RID: 13484 RVA: 0x000E5868 File Offset: 0x000E3A68
		public bool RemoveAt(int startPosition, int endPosition)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.RemoveAt(startPosition, endPosition, out num, out maskedTextResultHint);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string of where the characters were actually removed; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034AD RID: 13485 RVA: 0x000E5881 File Offset: 0x000E3A81
		public bool RemoveAt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.RemoveAtInt(startPosition, endPosition, out testPosition, out resultHint, false);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000E58BC File Offset: 0x000E3ABC
		private bool RemoveAtInt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			int num = this.FindEditPositionInRange(startPosition, endPosition, true);
			resultHint = MaskedTextResultHint.NoEffect;
			if (num == -1 || num > lastAssignedPosition)
			{
				testPosition = startPosition;
				return true;
			}
			testPosition = startPosition;
			bool flag = endPosition < lastAssignedPosition;
			if (this.FindAssignedEditPositionInRange(startPosition, endPosition, true) != -1)
			{
				resultHint = MaskedTextResultHint.Success;
			}
			if (flag)
			{
				int num2 = this.FindEditPositionFrom(endPosition + 1, true);
				int num3 = num2;
				startPosition = num;
				MaskedTextResultHint maskedTextResultHint;
				for (;;)
				{
					char c = this.testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[num2];
					if ((c != this.PromptChar || charDescriptor.IsAssigned) && !this.TestChar(c, num, out maskedTextResultHint))
					{
						break;
					}
					if (num2 == lastAssignedPosition)
					{
						goto IL_B3;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				resultHint = maskedTextResultHint;
				testPosition = num;
				return false;
				IL_B3:
				if (MaskedTextResultHint.SideEffect > resultHint)
				{
					resultHint = MaskedTextResultHint.SideEffect;
				}
				if (testOnly)
				{
					return true;
				}
				num2 = num3;
				num = startPosition;
				for (;;)
				{
					char c2 = this.testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[num2];
					if (c2 == this.PromptChar && !charDescriptor2.IsAssigned)
					{
						this.ResetChar(num);
					}
					else
					{
						this.SetChar(c2, num);
						this.ResetChar(num2);
					}
					if (num2 == lastAssignedPosition)
					{
						break;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				startPosition = num + 1;
			}
			if (startPosition <= endPosition)
			{
				this.ResetString(startPosition, endPosition);
			}
			return true;
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully replaced; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034AF RID: 13487 RVA: 0x000E5A08 File Offset: 0x000E3C08
		public bool Replace(char input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully replaced; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034B0 RID: 13488 RVA: 0x000E5A24 File Offset: 0x000E3C24
		public bool Replace(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			testPosition = position;
			if (!this.TestEscapeChar(input, testPosition))
			{
				testPosition = this.FindEditPositionFrom(testPosition, true);
			}
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = position;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		/// <summary>Replaces a single character between the specified starting and ending positions with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the character was successfully replaced; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034B1 RID: 13489 RVA: 0x000E5A88 File Offset: 0x000E3C88
		public bool Replace(char input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition == endPosition)
			{
				testPosition = startPosition;
				return this.TestSetChar(input, startPosition, out resultHint);
			}
			return this.Replace(input.ToString(), startPosition, endPosition, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters were successfully replaced; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060034B2 RID: 13490 RVA: 0x000E5AE8 File Offset: 0x000E3CE8
		public bool Replace(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters were successfully replaced; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034B3 RID: 13491 RVA: 0x000E5B04 File Offset: 0x000E3D04
		public bool Replace(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this.testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(position, position, out testPosition, out resultHint);
			}
			return this.TestSetString(input, position, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters between the specified starting and ending positions with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters were successfully replaced; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034B4 RID: 13492 RVA: 0x000E5B60 File Offset: 0x000E3D60
		public bool Replace(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (endPosition >= this.testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(startPosition, endPosition, out testPosition, out resultHint);
			}
			if (!this.TestString(input, startPosition, out testPosition, out resultHint))
			{
				return false;
			}
			if (this.assignedCharCount > 0)
			{
				if (testPosition < endPosition)
				{
					int num;
					MaskedTextResultHint maskedTextResultHint;
					if (!this.RemoveAtInt(testPosition + 1, endPosition, out num, out maskedTextResultHint, false))
					{
						testPosition = num;
						resultHint = maskedTextResultHint;
						return false;
					}
					if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
				}
				else if (testPosition > endPosition)
				{
					int lastAssignedPosition = this.LastAssignedPosition;
					int i = testPosition + 1;
					int num2 = endPosition + 1;
					MaskedTextResultHint maskedTextResultHint;
					for (;;)
					{
						num2 = this.FindEditPositionFrom(num2, true);
						i = this.FindEditPositionFrom(i, true);
						if (i == -1)
						{
							goto Block_12;
						}
						if (!this.TestChar(this.testString[num2], i, out maskedTextResultHint))
						{
							goto Block_13;
						}
						if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
						{
							resultHint = MaskedTextResultHint.Success;
						}
						if (num2 == lastAssignedPosition)
						{
							break;
						}
						num2++;
						i++;
					}
					while (i > testPosition)
					{
						this.SetChar(this.testString[num2], i);
						num2 = this.FindEditPositionFrom(num2 - 1, false);
						i = this.FindEditPositionFrom(i - 1, false);
					}
					goto IL_162;
					Block_12:
					testPosition = this.testString.Length;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
					Block_13:
					testPosition = i;
					resultHint = maskedTextResultHint;
					return false;
				}
			}
			IL_162:
			this.SetString(input, startPosition);
			return true;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000E5CD8 File Offset: 0x000E3ED8
		private void ResetChar(int testPosition)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[testPosition];
			if (this.IsEditPosition(testPosition) && charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = false;
				this.testString[testPosition] = this.promptChar;
				this.assignedCharCount--;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this.requiredCharCount--;
				}
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000E5D41 File Offset: 0x000E3F41
		private void ResetString(int startPosition, int endPosition)
		{
			startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
			if (startPosition != -1)
			{
				endPosition = this.FindAssignedEditPositionFrom(endPosition, false);
				while (startPosition <= endPosition)
				{
					startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
					this.ResetChar(startPosition);
					startPosition++;
				}
			}
		}

		/// <summary>Sets the formatted string to the specified input string.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters were successfully set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060034B7 RID: 13495 RVA: 0x000E5D78 File Offset: 0x000E3F78
		public bool Set(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Set(input, out num, out maskedTextResultHint);
		}

		/// <summary>Sets the formatted string to the specified input string, and then outputs the removal position and descriptive information.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually set; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the set operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if all the characters were successfully set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060034B8 RID: 13496 RVA: 0x000E5D90 File Offset: 0x000E3F90
		public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = 0;
			if (input.Length == 0)
			{
				this.Clear(out resultHint);
				return true;
			}
			if (!this.TestSetString(input, testPosition, out testPosition, out resultHint))
			{
				return false;
			}
			int num = this.FindAssignedEditPositionFrom(testPosition + 1, true);
			if (num != -1)
			{
				this.ResetString(num, this.testString.Length - 1);
			}
			return true;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000E5DF8 File Offset: 0x000E3FF8
		private void SetChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			this.SetChar(input, position, charDescriptor);
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000E5E1C File Offset: 0x000E401C
		private void SetChar(char input, int position, MaskedTextProvider.CharDescriptor charDescriptor)
		{
			MaskedTextProvider.CharDescriptor charDescriptor2 = this.stringDescriptor[position];
			if (this.TestEscapeChar(input, position, charDescriptor))
			{
				this.ResetChar(position);
				return;
			}
			if (char.IsLetter(input))
			{
				if (char.IsUpper(input))
				{
					if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToLower)
					{
						input = this.culture.TextInfo.ToLower(input);
					}
				}
				else if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToUpper)
				{
					input = this.culture.TextInfo.ToUpper(input);
				}
			}
			this.testString[position] = input;
			if (!charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = true;
				this.assignedCharCount++;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this.requiredCharCount++;
				}
			}
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000E5ED4 File Offset: 0x000E40D4
		private void SetString(string input, int testPosition)
		{
			foreach (char c in input)
			{
				if (!this.TestEscapeChar(c, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
				}
				this.SetChar(c, testPosition);
				testPosition++;
			}
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000E5F20 File Offset: 0x000E4120
		private bool TestChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (!MaskedTextProvider.IsPrintableChar(input))
			{
				resultHint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			if (MaskedTextProvider.IsLiteralPosition(charDescriptor))
			{
				if (this.SkipLiterals && input == this.testString[position])
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
					return true;
				}
				resultHint = MaskedTextResultHint.NonEditPosition;
				return false;
			}
			else
			{
				if (input == this.promptChar)
				{
					if (this.ResetOnPrompt)
					{
						if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
						{
							resultHint = MaskedTextResultHint.SideEffect;
						}
						else
						{
							resultHint = MaskedTextResultHint.CharacterEscaped;
						}
						return true;
					}
					if (!this.AllowPromptAsInput)
					{
						resultHint = MaskedTextResultHint.PromptCharNotAllowed;
						return false;
					}
				}
				if (input == ' ' && this.ResetOnSpace)
				{
					if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
					else
					{
						resultHint = MaskedTextResultHint.CharacterEscaped;
					}
					return true;
				}
				char c = this.mask[charDescriptor.MaskPosition];
				if (c <= '0')
				{
					if (c != '#')
					{
						if (c != '&')
						{
							if (c == '0')
							{
								if (!char.IsDigit(input))
								{
									resultHint = MaskedTextResultHint.DigitExpected;
									return false;
								}
							}
						}
						else if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
					else if (!char.IsDigit(input) && input != '-' && input != '+' && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c <= 'C')
				{
					if (c != '9')
					{
						switch (c)
						{
						case '?':
							if (!char.IsLetter(input) && input != ' ')
							{
								resultHint = MaskedTextResultHint.LetterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'A':
							if (!MaskedTextProvider.IsAlphanumeric(input))
							{
								resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'C':
							if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly && input != ' ')
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						}
					}
					else if (!char.IsDigit(input) && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c != 'L')
				{
					if (c == 'a')
					{
						if (!MaskedTextProvider.IsAlphanumeric(input) && input != ' ')
						{
							resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
							return false;
						}
						if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
				}
				else
				{
					if (!char.IsLetter(input))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
					{
						resultHint = MaskedTextResultHint.AsciiCharacterExpected;
						return false;
					}
				}
				if (input == this.testString[position] && charDescriptor.IsAssigned)
				{
					resultHint = MaskedTextResultHint.NoEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.Success;
				}
				return true;
			}
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x000E6180 File Offset: 0x000E4380
		private bool TestEscapeChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[position];
			return this.TestEscapeChar(input, position, charDescriptor);
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x000E61A4 File Offset: 0x000E43A4
		private bool TestEscapeChar(char input, int position, MaskedTextProvider.CharDescriptor charDex)
		{
			if (MaskedTextProvider.IsLiteralPosition(charDex))
			{
				return this.SkipLiterals && input == this.testString[position];
			}
			return (this.ResetOnPrompt && input == this.promptChar) || (this.ResetOnSpace && input == ' ');
		}

		// Token: 0x060034BF RID: 13503 RVA: 0x000E61F4 File Offset: 0x000E43F4
		private bool TestSetChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (this.TestChar(input, position, out resultHint))
			{
				if (resultHint == MaskedTextResultHint.Success || resultHint == MaskedTextResultHint.SideEffect)
				{
					this.SetChar(input, position);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x000E6216 File Offset: 0x000E4416
		private bool TestSetString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (this.TestString(input, position, out testPosition, out resultHint))
			{
				this.SetString(input, position);
				return true;
			}
			return false;
		}

		// Token: 0x060034C1 RID: 13505 RVA: 0x000E6230 File Offset: 0x000E4430
		private bool TestString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = position;
			if (input.Length == 0)
			{
				return true;
			}
			MaskedTextResultHint maskedTextResultHint = resultHint;
			foreach (char c in input)
			{
				if (testPosition >= this.testString.Length)
				{
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
				}
				if (!this.TestEscapeChar(c, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
					if (testPosition == -1)
					{
						testPosition = this.testString.Length;
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						return false;
					}
				}
				if (!this.TestChar(c, testPosition, out maskedTextResultHint))
				{
					resultHint = maskedTextResultHint;
					return false;
				}
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				testPosition++;
			}
			testPosition--;
			return true;
		}

		/// <summary>Returns the formatted string in a displayable form.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes prompts and mask literals.</returns>
		// Token: 0x060034C2 RID: 13506 RVA: 0x000E62DC File Offset: 0x000E44DC
		public string ToDisplayString()
		{
			if (!this.IsPassword || this.assignedCharCount == 0)
			{
				return this.testString.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder(this.testString.Length);
			for (int i = 0; i < this.testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				stringBuilder.Append((MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned) ? this.passwordChar : this.testString[i]);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns the formatted string that includes all the assigned character values.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values.</returns>
		// Token: 0x060034C3 RID: 13507 RVA: 0x000E636A File Offset: 0x000E456A
		public override string ToString()
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, 0, this.testString.Length);
		}

		/// <summary>Returns the formatted string, optionally including password characters.</summary>
		/// <param name="ignorePasswordChar">
		///   <see langword="true" /> to return the actual editable characters; otherwise, <see langword="false" /> to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <returns>The formatted <see cref="T:System.String" /> that includes literals, prompts, and optionally password characters.</returns>
		// Token: 0x060034C4 RID: 13508 RVA: 0x000E638B File Offset: 0x000E458B
		public string ToString(bool ignorePasswordChar)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, 0, this.testString.Length);
		}

		/// <summary>Returns a substring of the formatted string.</summary>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins.</param>
		/// <param name="length">The number of characters to return.</param>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		// Token: 0x060034C5 RID: 13509 RVA: 0x000E63AC File Offset: 0x000E45AC
		public string ToString(int startPosition, int length)
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including password characters.</summary>
		/// <param name="ignorePasswordChar">
		///   <see langword="true" /> to return the actual editable characters; otherwise, <see langword="false" /> to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins.</param>
		/// <param name="length">The number of characters to return.</param>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes literals, prompts, and optionally password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		// Token: 0x060034C6 RID: 13510 RVA: 0x000E63C3 File Offset: 0x000E45C3
		public string ToString(bool ignorePasswordChar, int startPosition, int length)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns the formatted string, optionally including prompt and literal characters.</summary>
		/// <param name="includePrompt">
		///   <see langword="true" /> to include prompt characters in the return string; otherwise, <see langword="false" />.</param>
		/// <param name="includeLiterals">
		///   <see langword="true" /> to include literal characters in the return string; otherwise, <see langword="false" />.</param>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values and optionally includes literals and prompts.</returns>
		// Token: 0x060034C7 RID: 13511 RVA: 0x000E63DA File Offset: 0x000E45DA
		public string ToString(bool includePrompt, bool includeLiterals)
		{
			return this.ToString(true, includePrompt, includeLiterals, 0, this.testString.Length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt and literal characters.</summary>
		/// <param name="includePrompt">
		///   <see langword="true" /> to include prompt characters in the return string; otherwise, <see langword="false" />.</param>
		/// <param name="includeLiterals">
		///   <see langword="true" /> to include literal characters in the return string; otherwise, <see langword="false" />.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins.</param>
		/// <param name="length">The number of characters to return.</param>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals and prompts; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		// Token: 0x060034C8 RID: 13512 RVA: 0x000E63F1 File Offset: 0x000E45F1
		public string ToString(bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			return this.ToString(true, includePrompt, includeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt, literal, and password characters.</summary>
		/// <param name="ignorePasswordChar">
		///   <see langword="true" /> to return the actual editable characters; otherwise, <see langword="false" /> to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="includePrompt">
		///   <see langword="true" /> to include prompt characters in the return string; otherwise, <see langword="false" />.</param>
		/// <param name="includeLiterals">
		///   <see langword="true" /> to return literal characters in the return string; otherwise, <see langword="false" />.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins.</param>
		/// <param name="length">The number of characters to return.</param>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals, prompts, and password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		// Token: 0x060034C9 RID: 13513 RVA: 0x000E6400 File Offset: 0x000E4600
		public string ToString(bool ignorePasswordChar, bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			if (length <= 0)
			{
				return string.Empty;
			}
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (startPosition >= this.testString.Length)
			{
				return string.Empty;
			}
			int num = this.testString.Length - startPosition;
			if (length > num)
			{
				length = num;
			}
			if ((!this.IsPassword || ignorePasswordChar) && (includePrompt && includeLiterals))
			{
				return this.testString.ToString(startPosition, length);
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = startPosition + length - 1;
			if (!includePrompt)
			{
				int num3 = (includeLiterals ? this.FindNonEditPositionInRange(startPosition, num2, false) : MaskedTextProvider.InvalidIndex);
				int num4 = this.FindAssignedEditPositionInRange((num3 == MaskedTextProvider.InvalidIndex) ? startPosition : num3, num2, false);
				num2 = ((num4 != MaskedTextProvider.InvalidIndex) ? num4 : num3);
				if (num2 == MaskedTextProvider.InvalidIndex)
				{
					return string.Empty;
				}
			}
			int i = startPosition;
			while (i <= num2)
			{
				char c = this.testString[i];
				MaskedTextProvider.CharDescriptor charDescriptor = this.stringDescriptor[i];
				MaskedTextProvider.CharType charType = charDescriptor.CharType;
				if (charType - MaskedTextProvider.CharType.EditOptional > 1)
				{
					if (charType != MaskedTextProvider.CharType.Separator && charType != MaskedTextProvider.CharType.Literal)
					{
						goto IL_12F;
					}
					if (includeLiterals)
					{
						goto IL_12F;
					}
				}
				else if (charDescriptor.IsAssigned)
				{
					if (!this.IsPassword || ignorePasswordChar)
					{
						goto IL_12F;
					}
					stringBuilder.Append(this.passwordChar);
				}
				else
				{
					if (includePrompt)
					{
						goto IL_12F;
					}
					stringBuilder.Append(' ');
				}
				IL_138:
				i++;
				continue;
				IL_12F:
				stringBuilder.Append(c);
				goto IL_138;
			}
			return stringBuilder.ToString();
		}

		/// <summary>Tests whether the specified character could be set successfully at the specified position.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the specified character is valid for the specified position; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034CA RID: 13514 RVA: 0x000E6559 File Offset: 0x000E4759
		public bool VerifyChar(char input, int position, out MaskedTextResultHint hint)
		{
			hint = MaskedTextResultHint.NoEffect;
			if (position < 0 || position >= this.testString.Length)
			{
				hint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.TestChar(input, position, out hint);
		}

		/// <summary>Tests whether the specified character would be escaped at the specified position.</summary>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		/// <returns>
		///   <see langword="true" /> if the specified character would be escaped at the specified position; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034CB RID: 13515 RVA: 0x000E657F File Offset: 0x000E477F
		public bool VerifyEscapeChar(char input, int position)
		{
			return position >= 0 && position < this.testString.Length && this.TestEscapeChar(input, position);
		}

		/// <summary>Tests whether the specified string could be set successfully.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified string represents valid input; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034CC RID: 13516 RVA: 0x000E65A0 File Offset: 0x000E47A0
		public bool VerifyString(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.VerifyString(input, out num, out maskedTextResultHint);
		}

		/// <summary>Tests whether the specified string could be set successfully, and then outputs position and descriptive information.</summary>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		/// <param name="testPosition">If successful, the zero-based position of the last character actually tested; otherwise, the first position where the test failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the test operation. An output parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the specified string represents valid input; otherwise, <see langword="false" />.</returns>
		// Token: 0x060034CD RID: 13517 RVA: 0x000E65B8 File Offset: 0x000E47B8
		public bool VerifyString(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			testPosition = 0;
			if (input == null || input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestString(input, 0, out testPosition, out resultHint);
		}

		// Token: 0x040029E0 RID: 10720
		private const char spaceChar = ' ';

		// Token: 0x040029E1 RID: 10721
		private const char defaultPromptChar = '_';

		// Token: 0x040029E2 RID: 10722
		private const char nullPasswordChar = '\0';

		// Token: 0x040029E3 RID: 10723
		private const bool defaultAllowPrompt = true;

		// Token: 0x040029E4 RID: 10724
		private const int invalidIndex = -1;

		// Token: 0x040029E5 RID: 10725
		private const byte editAny = 0;

		// Token: 0x040029E6 RID: 10726
		private const byte editUnassigned = 1;

		// Token: 0x040029E7 RID: 10727
		private const byte editAssigned = 2;

		// Token: 0x040029E8 RID: 10728
		private const bool forward = true;

		// Token: 0x040029E9 RID: 10729
		private const bool backward = false;

		// Token: 0x040029EA RID: 10730
		private static int ASCII_ONLY = BitVector32.CreateMask();

		// Token: 0x040029EB RID: 10731
		private static int ALLOW_PROMPT_AS_INPUT = BitVector32.CreateMask(MaskedTextProvider.ASCII_ONLY);

		// Token: 0x040029EC RID: 10732
		private static int INCLUDE_PROMPT = BitVector32.CreateMask(MaskedTextProvider.ALLOW_PROMPT_AS_INPUT);

		// Token: 0x040029ED RID: 10733
		private static int INCLUDE_LITERALS = BitVector32.CreateMask(MaskedTextProvider.INCLUDE_PROMPT);

		// Token: 0x040029EE RID: 10734
		private static int RESET_ON_PROMPT = BitVector32.CreateMask(MaskedTextProvider.INCLUDE_LITERALS);

		// Token: 0x040029EF RID: 10735
		private static int RESET_ON_LITERALS = BitVector32.CreateMask(MaskedTextProvider.RESET_ON_PROMPT);

		// Token: 0x040029F0 RID: 10736
		private static int SKIP_SPACE = BitVector32.CreateMask(MaskedTextProvider.RESET_ON_LITERALS);

		// Token: 0x040029F1 RID: 10737
		private static Type maskTextProviderType = typeof(MaskedTextProvider);

		// Token: 0x040029F2 RID: 10738
		private BitVector32 flagState;

		// Token: 0x040029F3 RID: 10739
		private CultureInfo culture;

		// Token: 0x040029F4 RID: 10740
		private StringBuilder testString;

		// Token: 0x040029F5 RID: 10741
		private int assignedCharCount;

		// Token: 0x040029F6 RID: 10742
		private int requiredCharCount;

		// Token: 0x040029F7 RID: 10743
		private int requiredEditChars;

		// Token: 0x040029F8 RID: 10744
		private int optionalEditChars;

		// Token: 0x040029F9 RID: 10745
		private string mask;

		// Token: 0x040029FA RID: 10746
		private char passwordChar;

		// Token: 0x040029FB RID: 10747
		private char promptChar;

		// Token: 0x040029FC RID: 10748
		private List<MaskedTextProvider.CharDescriptor> stringDescriptor;

		// Token: 0x02000894 RID: 2196
		private enum CaseConversion
		{
			// Token: 0x040037B6 RID: 14262
			None,
			// Token: 0x040037B7 RID: 14263
			ToLower,
			// Token: 0x040037B8 RID: 14264
			ToUpper
		}

		// Token: 0x02000895 RID: 2197
		[Flags]
		private enum CharType
		{
			// Token: 0x040037BA RID: 14266
			EditOptional = 1,
			// Token: 0x040037BB RID: 14267
			EditRequired = 2,
			// Token: 0x040037BC RID: 14268
			Separator = 4,
			// Token: 0x040037BD RID: 14269
			Literal = 8,
			// Token: 0x040037BE RID: 14270
			Modifier = 16
		}

		// Token: 0x02000896 RID: 2198
		private class CharDescriptor
		{
			// Token: 0x06004579 RID: 17785 RVA: 0x00122551 File Offset: 0x00120751
			public CharDescriptor(int maskPos, MaskedTextProvider.CharType charType)
			{
				this.MaskPosition = maskPos;
				this.CharType = charType;
			}

			// Token: 0x0600457A RID: 17786 RVA: 0x00122568 File Offset: 0x00120768
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "MaskPosition[{0}] <CaseConversion.{1}><CharType.{2}><IsAssigned: {3}", new object[] { this.MaskPosition, this.CaseConversion, this.CharType, this.IsAssigned });
			}

			// Token: 0x040037BF RID: 14271
			public int MaskPosition;

			// Token: 0x040037C0 RID: 14272
			public MaskedTextProvider.CaseConversion CaseConversion;

			// Token: 0x040037C1 RID: 14273
			public MaskedTextProvider.CharType CharType;

			// Token: 0x040037C2 RID: 14274
			public bool IsAssigned;
		}
	}
}
