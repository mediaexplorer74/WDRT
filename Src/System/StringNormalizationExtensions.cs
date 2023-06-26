using System;
using System.ComponentModel;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>Provides extension methods to work with string normalization.</summary>
	// Token: 0x02000038 RID: 56
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StringNormalizationExtensions
	{
		/// <summary>Indicates whether the specified string is in Unicode normalization form C.</summary>
		/// <param name="value">A string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is in normalization form C; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> contains invalid Unicode characters.</exception>
		// Token: 0x060002D3 RID: 723 RVA: 0x00010D2B File Offset: 0x0000EF2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsNormalized(this string value)
		{
			return value.IsNormalized(NormalizationForm.FormC);
		}

		/// <summary>Indicates whether a string is in a specified Unicode normalization form.</summary>
		/// <param name="value">A string.</param>
		/// <param name="normalizationForm">A Unicode normalization form.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is in normalization form <paramref name="normalizationForm" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> contains invalid Unicode characters.</exception>
		// Token: 0x060002D4 RID: 724 RVA: 0x00010D34 File Offset: 0x0000EF34
		[SecurityCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool IsNormalized(this string value, NormalizationForm normalizationForm)
		{
			return value.IsNormalized(normalizationForm);
		}

		/// <summary>Normalizes a string to a Unicode normalization form C.</summary>
		/// <param name="value">The string to normalize.</param>
		/// <returns>A new string whose textual value is the same as <paramref name="value" /> but whose binary representation is in Unicode normalization form C.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> contains invalid Unicode characters.</exception>
		// Token: 0x060002D5 RID: 725 RVA: 0x00010D3D File Offset: 0x0000EF3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string Normalize(this string value)
		{
			return value.Normalize(NormalizationForm.FormC);
		}

		/// <summary>Normalizes a string to the specified Unicode normalization form.</summary>
		/// <param name="value">The string to normalize.</param>
		/// <param name="normalizationForm">The Unicode normalization form.</param>
		/// <returns>A new string whose textual value is the same as <paramref name="value" /> but whose binary representation is in the <paramref name="normalizationForm" /> normalization form.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> contains invalid Unicode characters.</exception>
		// Token: 0x060002D6 RID: 726 RVA: 0x00010D46 File Offset: 0x0000EF46
		[SecurityCritical]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static string Normalize(this string value, NormalizationForm normalizationForm)
		{
			return value.Normalize(normalizationForm);
		}
	}
}
