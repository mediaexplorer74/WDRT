using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Data.OData
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public abstract class ODataAnnotatable
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003404 File Offset: 0x00001604
		public T GetAnnotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					return this.annotations as T;
				}
				foreach (object obj in array)
				{
					if (obj == null)
					{
						break;
					}
					T t = obj as T;
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000346E File Offset: 0x0000166E
		public void SetAnnotation<T>(T annotation) where T : class
		{
			this.VerifySetAnnotation(annotation);
			if (annotation == null)
			{
				this.RemoveAnnotation<T>();
				return;
			}
			this.AddOrReplaceAnnotation<T>(annotation);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003492 File Offset: 0x00001692
		internal virtual void VerifySetAnnotation(object annotation)
		{
			if (annotation is InstanceAnnotationCollection)
			{
				throw new NotSupportedException(Strings.ODataAnnotatable_InstanceAnnotationsOnlyOnODataError);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000034A8 File Offset: 0x000016A8
		internal T GetOrCreateAnnotation<T>() where T : class, new()
		{
			T t = this.GetAnnotation<T>();
			if (t == null)
			{
				t = new T();
				this.SetAnnotation<T>(t);
			}
			return t;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034D2 File Offset: 0x000016D2
		internal ICollection<ODataInstanceAnnotation> GetInstanceAnnotations()
		{
			return this.instanceAnnotations;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034DA File Offset: 0x000016DA
		internal void SetInstanceAnnotations(ICollection<ODataInstanceAnnotation> value)
		{
			ExceptionUtils.CheckArgumentNotNull<ICollection<ODataInstanceAnnotation>>(value, "value");
			this.instanceAnnotations = value;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034EE File Offset: 0x000016EE
		private static bool IsOfType(object instance, Type type)
		{
			return instance.GetType() == type;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034FC File Offset: 0x000016FC
		private void AddOrReplaceAnnotation<T>(T annotation) where T : class
		{
			if (this.annotations == null)
			{
				this.annotations = annotation;
				return;
			}
			object[] array = this.annotations as object[];
			if (array != null)
			{
				int i;
				for (i = 0; i < array.Length; i++)
				{
					object obj = array[i];
					if (obj == null || ODataAnnotatable.IsOfType(obj, typeof(T)))
					{
						array[i] = annotation;
						break;
					}
				}
				if (i == array.Length)
				{
					Array.Resize<object>(ref array, i * 2);
					this.annotations = array;
					array[i] = annotation;
				}
				return;
			}
			if (ODataAnnotatable.IsOfType(this.annotations, typeof(T)))
			{
				this.annotations = annotation;
				return;
			}
			this.annotations = new object[] { this.annotations, annotation };
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035C4 File Offset: 0x000017C4
		private void RemoveAnnotation<T>() where T : class
		{
			if (this.annotations != null)
			{
				object[] array = this.annotations as object[];
				if (array == null)
				{
					if (ODataAnnotatable.IsOfType(this.annotations, typeof(T)))
					{
						this.annotations = null;
						return;
					}
				}
				else
				{
					int i = 0;
					int num = -1;
					int num2 = array.Length;
					while (i < num2)
					{
						object obj = array[i];
						if (obj == null)
						{
							break;
						}
						if (ODataAnnotatable.IsOfType(obj, typeof(T)))
						{
							num = i;
							break;
						}
						i++;
					}
					if (num >= 0)
					{
						for (int j = num; j < num2 - 1; j++)
						{
							array[j] = array[j + 1];
						}
						array[num2 - 1] = null;
					}
				}
			}
		}

		// Token: 0x0400002F RID: 47
		[NonSerialized]
		private object annotations;

		// Token: 0x04000030 RID: 48
		[NonSerialized]
		private ICollection<ODataInstanceAnnotation> instanceAnnotations = new Collection<ODataInstanceAnnotation>();
	}
}
