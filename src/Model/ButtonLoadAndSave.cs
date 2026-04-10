using Model;
using System.Xml;

namespace Coursework
{
    /// <summary>
    /// Helper for loading and saving point data via file dialogs.
    /// Provides methods wired to UI buttons to load from and save to XML.
    /// </summary>
    internal class ButtonLoadAndSave
    {
        /// <summary>
        /// Writes a timestamped message to the console for diagnostics.
        /// </summary>
        /// <param name="message">Message to log.</param>
        private void Log(string message) => Console.WriteLine($"{DateTime.Now:HH:mm:ss} {message}");

        /// <summary>
        /// Shows an OpenFileDialog, loads points from the selected XML file and replaces
        /// the target function's point collection. Updates the provided DataGridView.
        /// </summary>
        /// <param name="selector">Function selector that returns the target FunctionDataBindingList.</param>
        /// <param name="grid">DataGridView to update the data source for.</param>
        /// <param name="nameForMessage">Short name used in messages and logs (e.g. "F" or "G").</param>
        public void btnLoad(Func<FunctionDataBindingList> selector, DataGridView grid, string nameForMessage)
        {
            try
            {
                using (var ofd = new OpenFileDialog() { Filter = "XML files|*.xml" })
                {
                    if (ofd.ShowDialog() != DialogResult.OK)
                        return;

                    var loaded = XmlDataManager.LoadPoints(ofd.FileName)?.ToList();

                    if (loaded == null || loaded.Count == 0)
                    {
                        MessageBox.Show($"The file does not contain points for {nameForMessage}.");
                        return;
                    }

                    var func = selector();

                    func.Points.Clear();
                    foreach (var p in loaded)
                        func.Points.Add(p);

                    grid.DataSource = null;
                    grid.DataSource = func.Points;

                    Log($"------{nameForMessage}------");
                    int counter = 0;
                    foreach (var p in func.Points)
                    {
                        Console.WriteLine($"{DateTime.Now:HH:mm:ss} {counter}: X={p.X}, Y={p.Y}");
                        counter++;
                    }
                    Log($"------{nameForMessage}------");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} File not found: {ex.Message}");
                MessageBox.Show("File not found.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} Access denied: {ex.Message}");
                MessageBox.Show("Access denied.");
            }
            catch (XmlException ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} XML error: {ex.Message}");
                MessageBox.Show("Invalid format XML.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} Something went wrong: {ex.Message}");
                MessageBox.Show($"Error loading points: {ex.Message}");
            }
        }

        /// <summary>
        /// Shows a SaveFileDialog and saves the selected function's points to XML.
        /// </summary>
        /// <param name="selector">Function selector that returns the target FunctionDataBindingList.</param>
        /// <param name="nameForMessage">Short name used in messages and logs (e.g. "F" or "G").</param>
        public void btnSave(Func<FunctionDataBindingList> selector, string nameForMessage)
        {
            var func = selector();
            using (var sfd = new SaveFileDialog() { Filter = "XML files|*.xml" })
            {
                try
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        XmlDataManager.SavePoints(sfd.FileName, func.Points.ToList());
                        // Only report success when the user confirmed Save and no exception was thrown
                        Log($"Points {nameForMessage} were successfully saved to {sfd.FileName}");
                        MessageBox.Show($"Points {nameForMessage} were successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // User cancelled the SaveFileDialog — do not report success
                        Log($"Save cancelled by user for {nameForMessage}.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss} {ex}");
                    MessageBox.Show("Invalid file name or path!");
                }
                catch (XmlException ex)
                {
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss} {ex}");
                    MessageBox.Show("Error while generating XML!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss} Unexpected Error in {nameForMessage} | " + ex);
                    MessageBox.Show($"Unexpected error.\n\nDetails: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }   
    }

}

