using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Text;
using System.Xml;

namespace System.Windows.Forms.Layout
{
	/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
	// Token: 0x020004C5 RID: 1221
	public class TableLayoutSettingsTypeConverter : TypeConverter
	{
		/// <summary>Determines whether this converter can convert an object in the given source type to the native type of this converter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005014 RID: 20500 RVA: 0x000C223C File Offset: 0x000C043C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type by using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005015 RID: 20501 RVA: 0x000C225A File Offset: 0x000C045A
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to the type of this converter by using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x06005016 RID: 20502 RVA: 0x0014CC0C File Offset: 0x0014AE0C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(value as string);
				TableLayoutSettings tableLayoutSettings = new TableLayoutSettings();
				this.ParseControls(tableLayoutSettings, xmlDocument.GetElementsByTagName("Control"));
				this.ParseStyles(tableLayoutSettings, xmlDocument.GetElementsByTagName("Columns"), true);
				this.ParseStyles(tableLayoutSettings, xmlDocument.GetElementsByTagName("Rows"), false);
				return tableLayoutSettings;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the given value object to the specified type by using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		// Token: 0x06005017 RID: 20503 RVA: 0x0014CC7C File Offset: 0x0014AE7C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is TableLayoutSettings && destinationType == typeof(string))
			{
				TableLayoutSettings tableLayoutSettings = value as TableLayoutSettings;
				StringBuilder stringBuilder = new StringBuilder();
				XmlWriter xmlWriter = XmlWriter.Create(stringBuilder);
				xmlWriter.WriteStartElement("TableLayoutSettings");
				xmlWriter.WriteStartElement("Controls");
				foreach (TableLayoutSettings.ControlInformation controlInformation in tableLayoutSettings.GetControlsInformation())
				{
					xmlWriter.WriteStartElement("Control");
					xmlWriter.WriteAttributeString("Name", controlInformation.Name.ToString());
					XmlWriter xmlWriter2 = xmlWriter;
					string text = "Row";
					int num = controlInformation.Row;
					xmlWriter2.WriteAttributeString(text, num.ToString(CultureInfo.CurrentCulture));
					XmlWriter xmlWriter3 = xmlWriter;
					string text2 = "RowSpan";
					num = controlInformation.RowSpan;
					xmlWriter3.WriteAttributeString(text2, num.ToString(CultureInfo.CurrentCulture));
					XmlWriter xmlWriter4 = xmlWriter;
					string text3 = "Column";
					num = controlInformation.Column;
					xmlWriter4.WriteAttributeString(text3, num.ToString(CultureInfo.CurrentCulture));
					XmlWriter xmlWriter5 = xmlWriter;
					string text4 = "ColumnSpan";
					num = controlInformation.ColumnSpan;
					xmlWriter5.WriteAttributeString(text4, num.ToString(CultureInfo.CurrentCulture));
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("Columns");
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (object obj in ((IEnumerable)tableLayoutSettings.ColumnStyles))
				{
					ColumnStyle columnStyle = (ColumnStyle)obj;
					stringBuilder2.AppendFormat("{0},{1},", columnStyle.SizeType, columnStyle.Width);
				}
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				}
				xmlWriter.WriteAttributeString("Styles", stringBuilder2.ToString());
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("Rows");
				StringBuilder stringBuilder3 = new StringBuilder();
				foreach (object obj2 in ((IEnumerable)tableLayoutSettings.RowStyles))
				{
					RowStyle rowStyle = (RowStyle)obj2;
					stringBuilder3.AppendFormat("{0},{1},", rowStyle.SizeType, rowStyle.Height);
				}
				if (stringBuilder3.Length > 0)
				{
					stringBuilder3.Remove(stringBuilder3.Length - 1, 1);
				}
				xmlWriter.WriteAttributeString("Styles", stringBuilder3.ToString());
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Close();
				return stringBuilder.ToString();
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x0014CF60 File Offset: 0x0014B160
		private string GetAttributeValue(XmlNode node, string attribute)
		{
			XmlAttribute xmlAttribute = node.Attributes[attribute];
			if (xmlAttribute != null)
			{
				return xmlAttribute.Value;
			}
			return null;
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x0014CF88 File Offset: 0x0014B188
		private int GetAttributeValue(XmlNode node, string attribute, int valueIfNotFound)
		{
			string attributeValue = this.GetAttributeValue(node, attribute);
			int num;
			if (!string.IsNullOrEmpty(attributeValue) && int.TryParse(attributeValue, out num))
			{
				return num;
			}
			return valueIfNotFound;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x0014CFB4 File Offset: 0x0014B1B4
		private void ParseControls(TableLayoutSettings settings, XmlNodeList controlXmlFragments)
		{
			foreach (object obj in controlXmlFragments)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string attributeValue = this.GetAttributeValue(xmlNode, "Name");
				if (!string.IsNullOrEmpty(attributeValue))
				{
					int attributeValue2 = this.GetAttributeValue(xmlNode, "Row", -1);
					int attributeValue3 = this.GetAttributeValue(xmlNode, "RowSpan", 1);
					int attributeValue4 = this.GetAttributeValue(xmlNode, "Column", -1);
					int attributeValue5 = this.GetAttributeValue(xmlNode, "ColumnSpan", 1);
					settings.SetRow(attributeValue, attributeValue2);
					settings.SetColumn(attributeValue, attributeValue4);
					settings.SetRowSpan(attributeValue, attributeValue3);
					settings.SetColumnSpan(attributeValue, attributeValue5);
				}
			}
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x0014D07C File Offset: 0x0014B27C
		private void ParseStyles(TableLayoutSettings settings, XmlNodeList controlXmlFragments, bool columns)
		{
			foreach (object obj in controlXmlFragments)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string attributeValue = this.GetAttributeValue(xmlNode, "Styles");
				Type typeFromHandle = typeof(SizeType);
				if (!string.IsNullOrEmpty(attributeValue))
				{
					int num;
					for (int i = 0; i < attributeValue.Length; i = num)
					{
						num = i;
						while (char.IsLetter(attributeValue[num]))
						{
							num++;
						}
						SizeType sizeType = (SizeType)Enum.Parse(typeFromHandle, attributeValue.Substring(i, num - i), true);
						while (!char.IsDigit(attributeValue[num]))
						{
							num++;
						}
						StringBuilder stringBuilder = new StringBuilder();
						while (num < attributeValue.Length && char.IsDigit(attributeValue[num]))
						{
							stringBuilder.Append(attributeValue[num]);
							num++;
						}
						stringBuilder.Append('.');
						while (num < attributeValue.Length && !char.IsLetter(attributeValue[num]))
						{
							if (char.IsDigit(attributeValue[num]))
							{
								stringBuilder.Append(attributeValue[num]);
							}
							num++;
						}
						string text = stringBuilder.ToString();
						float num2;
						if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out num2))
						{
							num2 = 0f;
						}
						if (columns)
						{
							settings.ColumnStyles.Add(new ColumnStyle(sizeType, num2));
						}
						else
						{
							settings.RowStyles.Add(new RowStyle(sizeType, num2));
						}
					}
				}
			}
		}
	}
}
