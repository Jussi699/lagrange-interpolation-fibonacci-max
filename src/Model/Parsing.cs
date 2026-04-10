using System.Globalization;

namespace Coursework
{
    /// <summary>
    /// Utility class that provides parsing helper for DataGridView cell values.
    /// Attempts to parse string input into numeric types (double/float/decimal)
    /// using invariant culture rules.
    /// </summary>
    class Parsings
    {
        /// <summary>
        /// Обробник події парсингу клітинки DataGridView.
        /// </summary>
        /// <param name="sender">Джерело події (зазвичай DataGridView).</param>
        /// <param name="e">Аргументи події парсингу клітинки.</param>
        public void dataGridView_CellParsing(object? sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.Value is not string raw) return;
            var s = raw.Trim().Replace(',', '.');
            if (e.DesiredType == typeof(double) &&
                double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
            {
                e.Value = d;
                e.ParsingApplied = true;
            }
            else if (e.DesiredType == typeof(float) &&
                     float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var f))
            {
                e.Value = f;
                e.ParsingApplied = true;
            }
            else if (e.DesiredType == typeof(decimal) &&
                     decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var m))
            {
                e.Value = m;
                e.ParsingApplied = true;
            }
        }

    }
}