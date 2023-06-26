using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Provides a method for reading values of a particular type from the configuration.</summary>
	// Token: 0x020000BB RID: 187
	public class AppSettingsReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.AppSettingsReader" /> class.</summary>
		// Token: 0x0600063C RID: 1596 RVA: 0x000240C6 File Offset: 0x000222C6
		public AppSettingsReader()
		{
			this.map = ConfigurationManager.AppSettings;
		}

		/// <summary>Gets the value for a specified key from the <see cref="P:System.Configuration.ConfigurationSettings.AppSettings" /> property and returns an object of the specified type containing the value from the configuration.</summary>
		/// <param name="key">The key for which to get the value.</param>
		/// <param name="type">The type of the object to return.</param>
		/// <returns>The value of the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.  
		/// -or-
		///  <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="key" /> does not exist in the <see langword="&lt;appSettings&gt;" /> configuration section.  
		/// -or-
		///  The value in the <see langword="&lt;appSettings&gt;" /> configuration section for <paramref name="key" /> is not of type <paramref name="type" />.</exception>
		// Token: 0x0600063D RID: 1597 RVA: 0x000240DC File Offset: 0x000222DC
		public object GetValue(string key, Type type)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = this.map[key];
			if (text == null)
			{
				throw new InvalidOperationException(SR.GetString("AppSettingsReaderNoKey", new object[] { key }));
			}
			if (!(type == AppSettingsReader.stringType))
			{
				object obj;
				try
				{
					obj = Convert.ChangeType(text, type, CultureInfo.InvariantCulture);
				}
				catch (Exception)
				{
					string text2 = ((text.Length == 0) ? "AppSettingsReaderEmptyString" : text);
					throw new InvalidOperationException(SR.GetString("AppSettingsReaderCantParse", new object[]
					{
						text2,
						key,
						type.ToString()
					}));
				}
				return obj;
			}
			int noneNesting = this.GetNoneNesting(text);
			if (noneNesting == 0)
			{
				return text;
			}
			if (noneNesting == 1)
			{
				return null;
			}
			return text.Substring(1, text.Length - 2);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000241C0 File Offset: 0x000223C0
		private int GetNoneNesting(string val)
		{
			int num = 0;
			int length = val.Length;
			if (length > 1)
			{
				while (val[num] == '(' && val[length - num - 1] == ')')
				{
					num++;
				}
				if (num > 0 && string.Compare(AppSettingsReader.NullString, 0, val, num, length - 2 * num, StringComparison.Ordinal) != 0)
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x04000C68 RID: 3176
		private NameValueCollection map;

		// Token: 0x04000C69 RID: 3177
		private static Type stringType = typeof(string);

		// Token: 0x04000C6A RID: 3178
		private static Type[] paramsArray = new Type[] { AppSettingsReader.stringType };

		// Token: 0x04000C6B RID: 3179
		private static string NullString = "None";
	}
}
