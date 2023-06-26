using System;
using System.Xml;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData
{
	// Token: 0x02000132 RID: 306
	public sealed class ODataInstanceAnnotation : ODataAnnotatable
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x0001A7A2 File Offset: 0x000189A2
		public ODataInstanceAnnotation(string name, ODataValue value)
		{
			ODataInstanceAnnotation.ValidateName(name);
			ODataInstanceAnnotation.ValidateValue(value);
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0001A7C4 File Offset: 0x000189C4
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0001A7CC File Offset: 0x000189CC
		public string Name { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0001A7D5 File Offset: 0x000189D5
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0001A7DD File Offset: 0x000189DD
		public ODataValue Value { get; private set; }

		// Token: 0x06000808 RID: 2056 RVA: 0x0001A7E8 File Offset: 0x000189E8
		internal static void ValidateName(string name)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(name, "name");
			if (name.IndexOf('.') < 0 || name[0] == '.' || name[name.Length - 1] == '.')
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_NeedPeriodInName(name));
			}
			if (ODataAnnotationNames.IsODataAnnotationName(name))
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_ReservedNamesNotAllowed(name, "odata."));
			}
			try
			{
				XmlConvert.VerifyNCName(name);
			}
			catch (XmlException ex)
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_BadTermName(name), ex);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001A874 File Offset: 0x00018A74
		internal static void ValidateValue(ODataValue value)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataValue>(value, "value");
			if (value is ODataStreamReferenceValue)
			{
				throw new ArgumentException(Strings.ODataInstanceAnnotation_ValueCannotBeODataStreamReferenceValue, "value");
			}
		}
	}
}
