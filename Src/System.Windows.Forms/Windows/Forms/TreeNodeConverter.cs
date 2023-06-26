using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.TreeNode" /> objects to and from various other representations.</summary>
	// Token: 0x02000412 RID: 1042
	public class TreeNodeConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048E9 RID: 18665 RVA: 0x0002792C File Offset: 0x00025B2C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x060048EA RID: 18666 RVA: 0x00132E50 File Offset: 0x00131050
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is TreeNode)
			{
				TreeNode treeNode = (TreeNode)value;
				MemberInfo memberInfo;
				object[] array;
				if (treeNode.ImageIndex == -1 || treeNode.SelectedImageIndex == -1)
				{
					if (treeNode.Nodes.Count == 0)
					{
						memberInfo = typeof(TreeNode).GetConstructor(new Type[] { typeof(string) });
						array = new object[] { treeNode.Text };
					}
					else
					{
						memberInfo = typeof(TreeNode).GetConstructor(new Type[]
						{
							typeof(string),
							typeof(TreeNode[])
						});
						TreeNode[] array2 = new TreeNode[treeNode.Nodes.Count];
						treeNode.Nodes.CopyTo(array2, 0);
						array = new object[] { treeNode.Text, array2 };
					}
				}
				else if (treeNode.Nodes.Count == 0)
				{
					memberInfo = typeof(TreeNode).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(int),
						typeof(int)
					});
					array = new object[] { treeNode.Text, treeNode.ImageIndex, treeNode.SelectedImageIndex };
				}
				else
				{
					memberInfo = typeof(TreeNode).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(int),
						typeof(int),
						typeof(TreeNode[])
					});
					TreeNode[] array3 = new TreeNode[treeNode.Nodes.Count];
					treeNode.Nodes.CopyTo(array3, 0);
					array = new object[] { treeNode.Text, treeNode.ImageIndex, treeNode.SelectedImageIndex, array3 };
				}
				if (memberInfo != null)
				{
					return new InstanceDescriptor(memberInfo, array, false);
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
