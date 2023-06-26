using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a set of options used by a code generator.</summary>
	// Token: 0x02000671 RID: 1649
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class CodeGeneratorOptions
	{
		/// <summary>Gets or sets the object at the specified index.</summary>
		/// <param name="index">The name associated with the object to retrieve.</param>
		/// <returns>The object associated with the specified name. If no object associated with the specified name exists in the collection, <see langword="null" />.</returns>
		// Token: 0x17000E61 RID: 3681
		public object this[string index]
		{
			get
			{
				return this.options[index];
			}
			set
			{
				this.options[index] = value;
			}
		}

		/// <summary>Gets or sets the string to use for indentations.</summary>
		/// <returns>A string containing the characters to use for indentations.</returns>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000F8C34 File Offset: 0x000F6E34
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000F8C61 File Offset: 0x000F6E61
		public string IndentString
		{
			get
			{
				object obj = this.options["IndentString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "    ";
			}
			set
			{
				this.options["IndentString"] = value;
			}
		}

		/// <summary>Gets or sets the style to use for bracing.</summary>
		/// <returns>A string containing the bracing style to use.</returns>
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000F8C74 File Offset: 0x000F6E74
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000F8CA1 File Offset: 0x000F6EA1
		public string BracingStyle
		{
			get
			{
				object obj = this.options["BracingStyle"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "Block";
			}
			set
			{
				this.options["BracingStyle"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to append an <see langword="else" />, <see langword="catch" />, or <see langword="finally" /> block, including brackets, at the closing line of each previous <see langword="if" /> or <see langword="try" /> block.</summary>
		/// <returns>
		///   <see langword="true" /> if an else should be appended; otherwise, <see langword="false" />. The default value of this property is <see langword="false" />.</returns>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000F8CB4 File Offset: 0x000F6EB4
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000F8CDD File Offset: 0x000F6EDD
		public bool ElseOnClosing
		{
			get
			{
				object obj = this.options["ElseOnClosing"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.options["ElseOnClosing"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to insert blank lines between members.</summary>
		/// <returns>
		///   <see langword="true" /> if blank lines should be inserted; otherwise, <see langword="false" />. By default, the value of this property is <see langword="true" />.</returns>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000F8CF8 File Offset: 0x000F6EF8
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000F8D21 File Offset: 0x000F6F21
		public bool BlankLinesBetweenMembers
		{
			get
			{
				object obj = this.options["BlankLinesBetweenMembers"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.options["BlankLinesBetweenMembers"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to generate members in the order in which they occur in member collections.</summary>
		/// <returns>
		///   <see langword="true" /> to generate the members in the order in which they occur in the member collection; otherwise, <see langword="false" />. The default value of this property is <see langword="false" />.</returns>
		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000F8D3C File Offset: 0x000F6F3C
		// (set) Token: 0x06003C58 RID: 15448 RVA: 0x000F8D65 File Offset: 0x000F6F65
		[ComVisible(false)]
		public bool VerbatimOrder
		{
			get
			{
				object obj = this.options["VerbatimOrder"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.options["VerbatimOrder"] = value;
			}
		}

		// Token: 0x04002C57 RID: 11351
		private IDictionary options = new ListDictionary();
	}
}
