using System;
using System.Globalization;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200008D RID: 141
	internal class HandlerBase
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00021A31 File Offset: 0x0001FC31
		private HandlerBase()
		{
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00021A3C File Offset: 0x0001FC3C
		private static XmlNode GetAndRemoveAttribute(XmlNode node, string attrib, bool fRequired)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attrib);
			if (fRequired && xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_missing_required_attribute", new object[] { attrib, node.Name }), node);
			}
			return xmlNode;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00021A84 File Offset: 0x0001FC84
		private static XmlNode GetAndRemoveStringAttributeInternal(XmlNode node, string attrib, bool fRequired, ref string val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				val = andRemoveAttribute.Value;
			}
			return andRemoveAttribute;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00021AA6 File Offset: 0x0001FCA6
		internal static XmlNode GetAndRemoveStringAttribute(XmlNode node, string attrib, ref string val)
		{
			return HandlerBase.GetAndRemoveStringAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00021AB4 File Offset: 0x0001FCB4
		private static XmlNode GetAndRemoveBooleanAttributeInternal(XmlNode node, string attrib, bool fRequired, ref bool val)
		{
			XmlNode andRemoveAttribute = HandlerBase.GetAndRemoveAttribute(node, attrib, fRequired);
			if (andRemoveAttribute != null)
			{
				try
				{
					val = bool.Parse(andRemoveAttribute.Value);
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(SR.GetString(SR.GetString("Config_invalid_boolean_attribute", new object[] { andRemoveAttribute.Name })), ex, andRemoveAttribute);
				}
			}
			return andRemoveAttribute;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00021B14 File Offset: 0x0001FD14
		internal static XmlNode GetAndRemoveBooleanAttribute(XmlNode node, string attrib, ref bool val)
		{
			return HandlerBase.GetAndRemoveBooleanAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00021B20 File Offset: 0x0001FD20
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

		// Token: 0x06000559 RID: 1369 RVA: 0x00021BBC File Offset: 0x0001FDBC
		internal static XmlNode GetAndRemoveIntegerAttribute(XmlNode node, string attrib, ref int val)
		{
			return HandlerBase.GetAndRemoveIntegerAttributeInternal(node, attrib, false, ref val);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00021BC7 File Offset: 0x0001FDC7
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[] { node.Attributes[0].Name }), node);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00021C04 File Offset: 0x0001FE04
		internal static string RemoveAttribute(XmlNode node, string name)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode != null)
			{
				return xmlNode.Value;
			}
			return null;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00021C29 File Offset: 0x0001FE29
		internal static string RemoveRequiredAttribute(XmlNode node, string name)
		{
			return HandlerBase.RemoveRequiredAttribute(node, name, false);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00021C34 File Offset: 0x0001FE34
		internal static string RemoveRequiredAttribute(XmlNode node, string name, bool allowEmpty)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_missing", new object[] { name }), node);
			}
			if (string.IsNullOrEmpty(xmlNode.Value) && !allowEmpty)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_required_attribute_empty", new object[] { name }), node);
			}
			return xmlNode.Value;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00021C9D File Offset: 0x0001FE9D
		internal static void CheckForNonElement(XmlNode node)
		{
			if (node.NodeType != XmlNodeType.Element)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_elements_only"), node);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00021CB9 File Offset: 0x0001FEB9
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (node.NodeType == XmlNodeType.Comment || node.NodeType == XmlNodeType.Whitespace)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00021CD7 File Offset: 0x0001FED7
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_base_no_child_nodes"), node.FirstChild);
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00021CF7 File Offset: 0x0001FEF7
		internal static void ThrowUnrecognizedElement(XmlNode node)
		{
			throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), node);
		}
	}
}
