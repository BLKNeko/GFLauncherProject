using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFUpdateProject
{
    public class CustomProgressBar : ProgressBar
    {
        // Propriedades para definir as cores do gradiente
        public Color StartColor { get; set; } = Color.DodgerBlue;
        public Color EndColor { get; set; } = Color.MediumBlue;
        public LinearGradientMode GradientMode { get; set; } = LinearGradientMode.Horizontal;

        public CustomProgressBar()
        {
            // Permite o controle ser desenhado manualmente
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            // Ajusta a largura do retângulo com base no valor atual da barra
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;

            // Desenha a borda da barra de progresso
            if (ProgressBarRenderer.IsSupported)
            {
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            }

            // Cria um gradiente linear entre StartColor e EndColor
            using (LinearGradientBrush brush = new LinearGradientBrush(rec, StartColor, EndColor, GradientMode))
            {
                // Preenche a barra de progresso com o gradiente
                e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height - 4);
            }
        }
    }
}
