using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066E RID: 1646
	internal static class HandlerBase
	{
		// Token: 0x06003B90 RID: 15248 RVA: 0x000F6240 File Offset: 0x000F4440
		private static XmlNode GetAndRemoveAttribute(XmlNode node, string attrib, bool fRequired)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attrib);
			if (fRequired && xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_missing_required_attribute", new object[] { attrib, node.Name }), node);
			}
			return xmlNode;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x000F6288 File Offset: 0x000F4488
		private static XmlNode GetAndRemoveStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				val = andRemoveAttribute.Value;
			}
			return andRemoveAttribute;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000F62AA File Offset: 0x000F44AA
		internal static XmlNode GetAndRemoveStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x000F62B5 File Offset: 0x000F44B5
		internal static XmlNode GetAndRemoveRequiredNonEmptyStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveNonEmptyStringAttributeInternal(node, attrib, true, ref val);
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x000F62C0 File Offset: 0x000F44C0
		private static XmlNode GetAndRemoveNonEmptyStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveStringAttributeInternal = HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, fRequired, ref val);
			if (andRemoveStringAttributeInternal != null && val.Length == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Empty_attribute", new object[] { attrib }), andRemoveStringAttributeInternal);
			}
			return andRemoveStringAttributeInternal;
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000F6300 File Offset: 0x000F4500
		private static XmlNode GetAndRemoveIntegerAttributeInternal(XmlNode node, string attrib, bool fRequired, ref int val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				if (andRemoveAttribute.Value.Trim() != andRemoveAttribute.Value)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_invalid_integer_attribute", new object[] { andRemoveAttribute.Name }), andRemoveAttribute);
				}
				try
				{
					val = int.Parse(andRemoveAttribute.Value, CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_invalid_integer_attribute", new object[] { andRemoveAttribute.Name }), ex, andRemoveAttribute);
				}
			}
			return andRemoveAttribute;
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x000F639C File Offset: 0x000F459C
		private static XmlNode GetAndRemoveNonNegativeAttributeInternal(XmlNode node, string attrib, bool fRequired, ref int val)
		{
			XmlNode andRemoveIntegerAttributeInternal = HandlerBase.GetAndRemoveIntegerAttributeInternal(node, attrib, fRequired, ref val);
			if (andRemoveIntegerAttributeInternal != null && val < 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_nonnegative_integer_attribute", new object[] { attrib }), andRemoveIntegerAttributeInternal);
			}
			return andRemoveIntegerAttributeInternal;
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000F63D7 File Offset: 0x000F45D7
		internal static XmlNode GetAndRemoveNonNegativeIntegerAttribute(XmlNode node, string attrib, ref int val)
		{
			return HandlerBase.GetAndRemoveNonNegativeAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000F63E4 File Offset: 0x000F45E4
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[] { node.Attributes[0].Name }), node.Attributes[0]);
			}
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000F6434 File Offset: 0x000F4634
		internal static void CheckForNonElement(XmlNode node)
		{
			if (node.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_elements_only"), node);
			}
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x000F6450 File Offset: 0x000F4650
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.Whitespace)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x000F646E File Offset: 0x000F466E
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_no_child_nodes"), node.FirstChild);
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x000F648E File Offset: 0x000F468E
		internal static void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), node);
		}
	}
}
