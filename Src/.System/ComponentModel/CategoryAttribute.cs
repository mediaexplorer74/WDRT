using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the category in which to group the property or event when displayed in a <see cref="T:System.Windows.Forms.PropertyGrid" /> control set to Categorized mode.</summary>
	// Token: 0x02000522 RID: 1314
	[AttributeUsage(AttributeTargets.All)]
	public class CategoryAttribute : Attribute
	{
		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Action category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the action category.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000DFC32 File Offset: 0x000DDE32
		public static CategoryAttribute Action
		{
			get
			{
				if (CategoryAttribute.action == null)
				{
					CategoryAttribute.action = new CategoryAttribute("Action");
				}
				return CategoryAttribute.action;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Appearance category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the appearance category.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x000DFC55 File Offset: 0x000DDE55
		public static CategoryAttribute Appearance
		{
			get
			{
				if (CategoryAttribute.appearance == null)
				{
					CategoryAttribute.appearance = new CategoryAttribute("Appearance");
				}
				return CategoryAttribute.appearance;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Asynchronous category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the asynchronous category.</returns>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000DFC78 File Offset: 0x000DDE78
		public static CategoryAttribute Asynchronous
		{
			get
			{
				if (CategoryAttribute.asynchronous == null)
				{
					CategoryAttribute.asynchronous = new CategoryAttribute("Asynchronous");
				}
				return CategoryAttribute.asynchronous;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Behavior category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the behavior category.</returns>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000DFC9B File Offset: 0x000DDE9B
		public static CategoryAttribute Behavior
		{
			get
			{
				if (CategoryAttribute.behavior == null)
				{
					CategoryAttribute.behavior = new CategoryAttribute("Behavior");
				}
				return CategoryAttribute.behavior;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Data category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the data category.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000DFCBE File Offset: 0x000DDEBE
		public static CategoryAttribute Data
		{
			get
			{
				if (CategoryAttribute.data == null)
				{
					CategoryAttribute.data = new CategoryAttribute("Data");
				}
				return CategoryAttribute.data;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Default category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the default category.</returns>
		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000DFCE1 File Offset: 0x000DDEE1
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.defAttr == null)
				{
					CategoryAttribute.defAttr = new CategoryAttribute();
				}
				return CategoryAttribute.defAttr;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Design category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the design category.</returns>
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000DFCFF File Offset: 0x000DDEFF
		public static CategoryAttribute Design
		{
			get
			{
				if (CategoryAttribute.design == null)
				{
					CategoryAttribute.design = new CategoryAttribute("Design");
				}
				return CategoryAttribute.design;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the DragDrop category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the drag-and-drop category.</returns>
		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000DFD22 File Offset: 0x000DDF22
		public static CategoryAttribute DragDrop
		{
			get
			{
				if (CategoryAttribute.dragDrop == null)
				{
					CategoryAttribute.dragDrop = new CategoryAttribute("DragDrop");
				}
				return CategoryAttribute.dragDrop;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Focus category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the focus category.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000DFD45 File Offset: 0x000DDF45
		public static CategoryAttribute Focus
		{
			get
			{
				if (CategoryAttribute.focus == null)
				{
					CategoryAttribute.focus = new CategoryAttribute("Focus");
				}
				return CategoryAttribute.focus;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Format category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the format category.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000DFD68 File Offset: 0x000DDF68
		public static CategoryAttribute Format
		{
			get
			{
				if (CategoryAttribute.format == null)
				{
					CategoryAttribute.format = new CategoryAttribute("Format");
				}
				return CategoryAttribute.format;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Key category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the key category.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000DFD8B File Offset: 0x000DDF8B
		public static CategoryAttribute Key
		{
			get
			{
				if (CategoryAttribute.key == null)
				{
					CategoryAttribute.key = new CategoryAttribute("Key");
				}
				return CategoryAttribute.key;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Layout category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the layout category.</returns>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000DFDAE File Offset: 0x000DDFAE
		public static CategoryAttribute Layout
		{
			get
			{
				if (CategoryAttribute.layout == null)
				{
					CategoryAttribute.layout = new CategoryAttribute("Layout");
				}
				return CategoryAttribute.layout;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Mouse category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the mouse category.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000DFDD1 File Offset: 0x000DDFD1
		public static CategoryAttribute Mouse
		{
			get
			{
				if (CategoryAttribute.mouse == null)
				{
					CategoryAttribute.mouse = new CategoryAttribute("Mouse");
				}
				return CategoryAttribute.mouse;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the WindowStyle category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the window style category.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000DFDF4 File Offset: 0x000DDFF4
		public static CategoryAttribute WindowStyle
		{
			get
			{
				if (CategoryAttribute.windowStyle == null)
				{
					CategoryAttribute.windowStyle = new CategoryAttribute("WindowStyle");
				}
				return CategoryAttribute.windowStyle;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the category name Default.</summary>
		// Token: 0x060031D8 RID: 12760 RVA: 0x000DFE17 File Offset: 0x000DE017
		public CategoryAttribute()
			: this("Default")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the specified category name.</summary>
		/// <param name="category">The name of the category.</param>
		// Token: 0x060031D9 RID: 12761 RVA: 0x000DFE24 File Offset: 0x000DE024
		public CategoryAttribute(string category)
		{
			this.categoryValue = category;
			this.localized = false;
		}

		/// <summary>Gets the name of the category for the property or event that this attribute is applied to.</summary>
		/// <returns>The name of the category for the property or event that this attribute is applied to.</returns>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000DFE3C File Offset: 0x000DE03C
		public string Category
		{
			get
			{
				if (!this.localized)
				{
					this.localized = true;
					string localizedString = this.GetLocalizedString(this.categoryValue);
					if (localizedString != null)
					{
						this.categoryValue = localizedString;
					}
				}
				return this.categoryValue;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.CategoryAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031DB RID: 12763 RVA: 0x000DFE75 File Offset: 0x000DE075
		public override bool Equals(object obj)
		{
			return obj == this || (obj is CategoryAttribute && this.Category.Equals(((CategoryAttribute)obj).Category));
		}

		/// <summary>Returns the hash code for this attribute.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060031DC RID: 12764 RVA: 0x000DFE9D File Offset: 0x000DE09D
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Looks up the localized name of the specified category.</summary>
		/// <param name="value">The identifer for the category to look up.</param>
		/// <returns>The localized name of the category, or <see langword="null" /> if a localized name does not exist.</returns>
		// Token: 0x060031DD RID: 12765 RVA: 0x000DFEAA File Offset: 0x000DE0AA
		protected virtual string GetLocalizedString(string value)
		{
			return (string)SR.GetObject("PropertyCategory" + value);
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031DE RID: 12766 RVA: 0x000DFEC1 File Offset: 0x000DE0C1
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(CategoryAttribute.Default.Category);
		}

		// Token: 0x0400292B RID: 10539
		private static volatile CategoryAttribute appearance;

		// Token: 0x0400292C RID: 10540
		private static volatile CategoryAttribute asynchronous;

		// Token: 0x0400292D RID: 10541
		private static volatile CategoryAttribute behavior;

		// Token: 0x0400292E RID: 10542
		private static volatile CategoryAttribute data;

		// Token: 0x0400292F RID: 10543
		private static volatile CategoryAttribute design;

		// Token: 0x04002930 RID: 10544
		private static volatile CategoryAttribute action;

		// Token: 0x04002931 RID: 10545
		private static volatile CategoryAttribute format;

		// Token: 0x04002932 RID: 10546
		private static volatile CategoryAttribute layout;

		// Token: 0x04002933 RID: 10547
		private static volatile CategoryAttribute mouse;

		// Token: 0x04002934 RID: 10548
		private static volatile CategoryAttribute key;

		// Token: 0x04002935 RID: 10549
		private static volatile CategoryAttribute focus;

		// Token: 0x04002936 RID: 10550
		private static volatile CategoryAttribute windowStyle;

		// Token: 0x04002937 RID: 10551
		private static volatile CategoryAttribute dragDrop;

		// Token: 0x04002938 RID: 10552
		private static volatile CategoryAttribute defAttr;

		// Token: 0x04002939 RID: 10553
		private bool localized;

		// Token: 0x0400293A RID: 10554
		private string categoryValue;
	}
}
