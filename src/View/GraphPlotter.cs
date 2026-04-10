using System.Globalization;

namespace View
{
    /// <summary>
    /// Responsible for drawing function plots on a Panel control. Supports
    /// both System.Drawing.Point and Model.Point inputs and renders interpolated
    /// curves, axes, ticks and the maximum difference marker.
    /// </summary>
    internal class GraphPlotter
    {
        private Panel panel;
        private List<PointF> fPointsScreen = new List<PointF>();
        private List<PointF> gPointsScreen = new List<PointF>();
        private List<PointF> diffPointsScreen = new List<PointF>();
        private PointF maxPointScreen = new PointF(float.NaN, float.NaN);
        private PlotBounds lastBounds;

        /// <summary>
        /// Initializes a new instance of GraphPlotter and subscribes to the panel's
        /// Paint event so the graph will repaint automatically.
        /// </summary>
        /// <param name="panel">Panel control where graphs will be drawn.</param>
        public GraphPlotter(Panel panel)
        {
            this.panel = panel;
            this.panel.Paint += Panel_Paint;
        }

        /// <summary>
        /// Малює графіки f та g на вказаній панелі (тип точок: System.Drawing.Point).
        /// </summary>
        public void DrawGraph(
            List<System.Drawing.Point> fPoints,
            List<System.Drawing.Point> gPoints,
            double maxX,
            double maxValue,
            double searchMin,
            double searchMax,
            Control targetPanel)
        {
            using var g = targetPanel.CreateGraphics();
            DrawGraphInternal(
                fPoints,
                gPoints,
                p => p.X,
                p => p.Y,
                maxX,
                maxValue,
                searchMin,
                searchMax,
                targetPanel.Width,
                targetPanel.Height,
                g);
        }

        /// <summary>
        /// Draws interpolated curves for f and g using points of type Model.Point.
        /// This overload is used by the application when working with its data model.
        /// </summary>
        internal void DrawGraph(
            List<Model.Point> fPoints,
            List<Model.Point> gPoints,
            double maxX,
            double maxValue,
            double searchMin,
            double searchMax,
            Panel panelGraph)
        {
            using var g = panelGraph.CreateGraphics();
            DrawGraphInternal(
                fPoints,
                gPoints,
                p => p.X,
                p => p.Y,
                maxX,
                maxValue,
                searchMin,
                searchMax,
                panelGraph.Width,
                panelGraph.Height,
                g);
        }

        /// <summary>
        /// Узагальнений метод малювання графіків для будь-якого типу точки.
        /// </summary>
        private void DrawGraphInternal<TPoint>(
            List<TPoint> fPoints,
            List<TPoint> gPoints,
            Func<TPoint, double> getX,
            Func<TPoint, double> getY,
            double maxX,
            double maxValue,
            double searchMin,
            double searchMax,
            int panelWidth,
            int panelHeight,
            Graphics g)
        {
            g.Clear(Color.White);

            // Sample interpolated curves using Lagrange polynomials
            const int samples = 400;
            if (fPoints == null || gPoints == null || fPoints.Count == 0 || gPoints.Count == 0)
                return;

            var xs = new double[samples];
            for (int i = 0; i < samples; i++)
                xs[i] = searchMin + i * (searchMax - searchMin) / (samples - 1);

            var fYs = new double[samples];
            var gYs = new double[samples];
            var diffs = new double[samples];

            for (int i = 0; i < samples; i++)
            {
                double x = xs[i];
                fYs[i] = LagrangeInterpolate(fPoints, getX, getY, x);
                gYs[i] = LagrangeInterpolate(gPoints, getX, getY, x);
                diffs[i] = fYs[i] - gYs[i];
            }

            double minY = Math.Min(Math.Min(fYs.Min(), gYs.Min()), diffs.Min());
            double maxY = Math.Max(Math.Max(fYs.Max(), gYs.Max()), Math.Max(diffs.Max(), maxValue));

            // If flat, expand range
            if (Math.Abs(maxY - minY) < double.Epsilon)
            {
                maxY += 1;
                minY -= 1;
            }

            var bounds = new PlotBounds
            {
                MinX = searchMin,
                MaxX = searchMax,
                MinY = minY,
                MaxY = maxY,
                MarginLeft = 60,
                MarginRight = 20,
                MarginTop = 20,
                MarginBottom = 40,
                Width = panelWidth - 60 - 20,
                Height = panelHeight - 20 - 40
            };

            bounds.ScaleX = bounds.Width / (bounds.MaxX - bounds.MinX);
            bounds.ScaleY = bounds.Height / (bounds.MaxY - bounds.MinY);

            fPointsScreen = new List<PointF>(samples);
            gPointsScreen = new List<PointF>(samples);
            diffPointsScreen = new List<PointF>(samples);

            for (int i = 0; i < samples; i++)
            {
                float sx = bounds.MarginLeft + (float)((xs[i] - bounds.MinX) * bounds.ScaleX);
                float fy = bounds.MarginTop + bounds.Height - (float)((fYs[i] - bounds.MinY) * bounds.ScaleY);
                float gy = bounds.MarginTop + bounds.Height - (float)((gYs[i] - bounds.MinY) * bounds.ScaleY);
                float dy = bounds.MarginTop + bounds.Height - (float)((diffs[i] - bounds.MinY) * bounds.ScaleY);

                fPointsScreen.Add(new PointF(sx, fy));
                gPointsScreen.Add(new PointF(sx, gy));
                diffPointsScreen.Add(new PointF(sx, dy));
            }

            // Find maximum of difference
            int maxIdx = 0;
            double maxDiffVal = diffs[0];
            for (int i = 1; i < samples; i++)
            {
                if (diffs[i] > maxDiffVal)
                {
                    maxDiffVal = diffs[i];
                    maxIdx = i;
                }
            }

            maxPointScreen = new PointF(
                bounds.MarginLeft + (float)((xs[maxIdx] - bounds.MinX) * bounds.ScaleX),
                bounds.MarginTop + bounds.Height - (float)((maxDiffVal - bounds.MinY) * bounds.ScaleY));

            // Store bounds for repaint
            lastBounds = bounds;

            DrawAxesAndLabels(g, bounds);
            using var fPen = new Pen(Color.Blue, 2);
            using var gPen = new Pen(Color.Red, 2);
            using var diffPen = new Pen(Color.DarkGreen, 2);

            DrawFunctionCurve(g, fPointsScreen, fPen);
            DrawFunctionCurve(g, gPointsScreen, gPen);
            DrawFunctionCurve(g, diffPointsScreen, diffPen);
            DrawMaxPoint(g, maxPointScreen);
        }

        private struct PlotBounds
        {
            public double MinX;
            public double MaxX;
            public double MinY;
            public double MaxY;
            public int MarginLeft;
            public int MarginRight;
            public int MarginTop;
            public int MarginBottom;
            public int Width;
            public int Height;
            public double ScaleX;
            public double ScaleY;
        }

        /// <summary>
        /// Обчислює межі, відступи та масштаби для побудови графіка.
        /// </summary>
        private bool TryCalculateBounds<TPoint>(
            List<TPoint> fPoints,
            List<TPoint> gPoints,
            Func<TPoint, double> getY,
            double maxValue,
            double searchMin,
            double searchMax,
            int panelWidth,
            int panelHeight,
            out PlotBounds bounds)
        {
            bounds = new PlotBounds();

            if (fPoints == null || gPoints == null || fPoints.Count == 0 || gPoints.Count == 0)
                return false;

            bounds.MinX = searchMin;
            bounds.MaxX = searchMax;

            double minY = Math.Min(fPoints.Min(p => getY(p)), gPoints.Min(p => getY(p)));
            double maxY = Math.Max(Math.Max(fPoints.Max(p => getY(p)), gPoints.Max(p => getY(p))), maxValue);

            bounds.MarginLeft = 60;
            bounds.MarginRight = 20;
            bounds.MarginTop = 20;
            bounds.MarginBottom = 40;

            bounds.Width = panelWidth - bounds.MarginLeft - bounds.MarginRight;
            bounds.Height = panelHeight - bounds.MarginTop - bounds.MarginBottom;

            if (bounds.Width <= 0 || bounds.Height <= 0 ||
                bounds.MaxX - bounds.MinX == 0 || maxY - minY == 0)
                return false;

            bounds.MinY = minY;
            bounds.MaxY = maxY;
            bounds.ScaleX = bounds.Width / (bounds.MaxX - bounds.MinX);
            bounds.ScaleY = bounds.Height / (bounds.MaxY - bounds.MinY);

            return true;
        }

        /// <summary>
        /// Проєктує список точок у екранні координати.
        /// </summary>
        private List<PointF> ProjectToScreen<TPoint>(
            List<TPoint> points,
            Func<TPoint, double> getX,
            Func<TPoint, double> getY,
            PlotBounds bounds)
        {
            var result = new List<PointF>(points.Count);
            foreach (var p in points)
            {
                float x = bounds.MarginLeft +
                          (float)((getX(p) - bounds.MinX) * bounds.ScaleX);
                float y = bounds.MarginTop + bounds.Height -
                          (float)((getY(p) - bounds.MinY) * bounds.ScaleY);
                result.Add(new PointF(x, y));
            }
            return result;
        }

        /// <summary>
        /// Проєктує точку максимуму у екранні координати.
        /// </summary>
        private PointF ProjectMaxPointToScreen(double maxX, double maxValue, PlotBounds bounds)
        {
            float x = bounds.MarginLeft +
                      (float)((maxX - bounds.MinX) * bounds.ScaleX);
            float y = bounds.MarginTop + bounds.Height -
                      (float)((maxValue - bounds.MinY) * bounds.ScaleY);
            return new PointF(x, y);
        }

        /// <summary>
        /// Обчислює інтерполяцію значення в точці x за формулою Лагранжа.
        /// Додається, щоб виправити помилку CS0103: відсутній метод LagrangeInterpolate.
        /// Метод загальний і працює з типом точки через делегати getX/getY.
        /// </summary>
        private static double LagrangeInterpolate<TPoint>(
            List<TPoint> points,
            Func<TPoint, double> getX,
            Func<TPoint, double> getY,
            double x)
        {
            if (points == null || points.Count == 0)
                return 0.0;

            int n = points.Count;
            double result = 0.0;

            for (int i = 0; i < n; i++)
            {
                double xi = getX(points[i]);
                double yi = getY(points[i]);

                // If query x exactly equals a node x, return its y to avoid numerical issues
                if (Math.Abs(x - xi) < 1e-14)
                    return yi;

                double term = yi;
                for (int j = 0; j < n; j++)
                {
                    if (j == i) continue;
                    double xj = getX(points[j]);
                    double denom = xi - xj;
                    if (Math.Abs(denom) < 1e-14)
                    {
                        // Duplicate node — cannot form valid basis; skip this term
                        term = 0.0;
                        break;
                    }
                    term *= (x - xj) / denom;
                }

                result += term;
            }

            return result;
        }


        private void DrawAxesAndLabels(Graphics g, PlotBounds b)
        {
            using (var axisPen = new Pen(Color.Black, 1))
            {
                g.DrawLine(axisPen,
                    b.MarginLeft, b.MarginTop + b.Height,
                    b.MarginLeft + b.Width, b.MarginTop + b.Height); 

                g.DrawLine(axisPen,
                    b.MarginLeft, b.MarginTop,
                    b.MarginLeft, b.MarginTop + b.Height); 
            }

            int xTicks = 6;
            int yTicks = 6;

            using var fnt = SystemFonts.DefaultFont;
            var sfX = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near
            };

            for (int i = 0; i <= xTicks; i++)
            {
                double xVal = b.MinX + i * (b.MaxX - b.MinX) / xTicks;
                float xScr = b.MarginLeft + (float)((xVal - b.MinX) * b.ScaleX);
                g.DrawLine(Pens.Black,
                    xScr, b.MarginTop + b.Height,
                    xScr, b.MarginTop + b.Height + 6);

                string label = xVal.ToString("G4", CultureInfo.CurrentCulture);
                var labelPoint = new PointF(xScr, b.MarginTop + b.Height + 6);
                g.DrawString(label, fnt, Brushes.Black, labelPoint, sfX);
            }

            var sfY = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center
            };

            for (int i = 0; i <= yTicks; i++)
            {
                double yVal = b.MinY + i * (b.MaxY - b.MinY) / yTicks;
                float yScr = b.MarginTop + b.Height -
                             (float)((yVal - b.MinY) * b.ScaleY);

                g.DrawLine(Pens.Black,
                    b.MarginLeft - 6, yScr,
                    b.MarginLeft, yScr);

                string label = yVal.ToString("G4", CultureInfo.CurrentCulture);
                var labelPoint = new PointF(b.MarginLeft - 8, yScr);
                g.DrawString(label, fnt, Brushes.Black, labelPoint, sfY);
            }

            g.DrawString("X", fnt, Brushes.Black,
                new PointF(b.MarginLeft + b.Width, b.MarginTop + b.Height + 12));
            g.DrawString("Y", fnt, Brushes.Black,
                new PointF(8, b.MarginTop));
        }

        private void DrawFunctionCurve(Graphics g, List<PointF> screenPoints, Pen pen)
        {
            if (screenPoints.Count > 1)
            {
                g.DrawLines(pen, screenPoints.ToArray());
            }
        }

        private void DrawMaxPoint(Graphics g, PointF maxPoint)
        {
            if (float.IsNaN(maxPoint.X) || float.IsNaN(maxPoint.Y))
                return;

            float size = 10;
            g.FillEllipse(Brushes.Green,
                maxPoint.X - size / 2,
                maxPoint.Y - size / 2,
                size, size);
        }

        /// <summary>
        /// Обробник події Paint: перемальовує збережені лінії та точку максимуму.
        /// </summary>
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            if (fPointsScreen.Count > 1)
                g.DrawLines(Pens.Blue, fPointsScreen.ToArray());

            if (gPointsScreen.Count > 1)
                g.DrawLines(Pens.Red, gPointsScreen.ToArray());

            DrawMaxPoint(g, maxPointScreen);

            g.DrawLine(Pens.Black, 25, panel.Height - 25, panel.Width - 5, panel.Height - 25);
            g.DrawLine(Pens.Black, 25, panel.Height - 25, 25, 25);
        }
    }
}
