using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Store email addresses that are associated with an email message.</summary>
	// Token: 0x0200026B RID: 619
	public class MailAddressCollection : Collection<MailAddress>
	{
		/// <summary>Add a list of email addresses to the collection.</summary>
		/// <param name="addresses">The email addresses to add to the <see cref="T:System.Net.Mail.MailAddressCollection" />. Multiple email addresses must be separated with a comma character (",").</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="addresses" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="addresses" /> parameter is an empty string.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="addresses" /> parameter contains an email address that is invalid or not supported.</exception>
		// Token: 0x0600173A RID: 5946 RVA: 0x00076A1C File Offset: 0x00074C1C
		public void Add(string addresses)
		{
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			if (addresses == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[] { "addresses" }), "addresses");
			}
			this.ParseValue(addresses);
		}

		/// <summary>Replaces the element at the specified index.</summary>
		/// <param name="index">The index of the email address element to be replaced.</param>
		/// <param name="item">An email address that will replace the element in the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
		// Token: 0x0600173B RID: 5947 RVA: 0x00076A6E File Offset: 0x00074C6E
		protected override void SetItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.SetItem(index, item);
		}

		/// <summary>Inserts an email address into the <see cref="T:System.Net.Mail.MailAddressCollection" />, at the specified location.</summary>
		/// <param name="index">The location at which to insert the email address that is specified by <paramref name="item" />.</param>
		/// <param name="item">The email address to be inserted into the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="item" /> parameter is null.</exception>
		// Token: 0x0600173C RID: 5948 RVA: 0x00076A86 File Offset: 0x00074C86
		protected override void InsertItem(int index, MailAddress item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InsertItem(index, item);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00076AA0 File Offset: 0x00074CA0
		internal void ParseValue(string addresses)
		{
			IList<MailAddress> list = MailAddressParser.ParseMultipleAddresses(addresses);
			for (int i = 0; i < list.Count; i++)
			{
				base.Add(list[i]);
			}
		}

		/// <summary>Returns a string representation of the email addresses in this <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the email addresses in this collection.</returns>
		// Token: 0x0600173E RID: 5950 RVA: 0x00076AD4 File Offset: 0x00074CD4
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MailAddress mailAddress in this)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(mailAddress.ToString());
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00076B44 File Offset: 0x00074D44
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			foreach (MailAddress mailAddress in this)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = mailAddress.Encode(charsConsumed, allowUnicode);
				}
				else
				{
					text = text + ", " + mailAddress.Encode(1, allowUnicode);
				}
			}
			return text;
		}
	}
}
