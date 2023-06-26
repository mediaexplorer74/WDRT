using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Microsoft.Xaml.Behaviors.Media
{
	// Token: 0x0200002D RID: 45
	public abstract class TransitionEffect : ShaderEffect
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000056D2 File Offset: 0x000038D2
		public new TransitionEffect CloneCurrentValue()
		{
			return (TransitionEffect)base.CloneCurrentValue();
		}

		// Token: 0x06000152 RID: 338
		protected abstract TransitionEffect DeepCopy();

		// Token: 0x06000153 RID: 339 RVA: 0x000056DF File Offset: 0x000038DF
		protected TransitionEffect()
		{
			base.UpdateShaderValue(TransitionEffect.InputProperty);
			base.UpdateShaderValue(TransitionEffect.OldImageProperty);
			base.UpdateShaderValue(TransitionEffect.ProgressProperty);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005708 File Offset: 0x00003908
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000571A File Offset: 0x0000391A
		public Brush Input
		{
			get
			{
				return (Brush)base.GetValue(TransitionEffect.InputProperty);
			}
			set
			{
				base.SetValue(TransitionEffect.InputProperty, value);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005728 File Offset: 0x00003928
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000573A File Offset: 0x0000393A
		public Brush OldImage
		{
			get
			{
				return (Brush)base.GetValue(TransitionEffect.OldImageProperty);
			}
			set
			{
				base.SetValue(TransitionEffect.OldImageProperty, value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005748 File Offset: 0x00003948
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000575A File Offset: 0x0000395A
		public double Progress
		{
			get
			{
				return (double)base.GetValue(TransitionEffect.ProgressProperty);
			}
			set
			{
				base.SetValue(TransitionEffect.ProgressProperty, value);
			}
		}

		// Token: 0x0400006D RID: 109
		public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(TransitionEffect), 0, SamplingMode.NearestNeighbor);

		// Token: 0x0400006E RID: 110
		public static readonly DependencyProperty OldImageProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("OldImage", typeof(TransitionEffect), 1, SamplingMode.NearestNeighbor);

		// Token: 0x0400006F RID: 111
		public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(TransitionEffect), new PropertyMetadata(0.0, ShaderEffect.PixelShaderConstantCallback(0)));
	}
}
