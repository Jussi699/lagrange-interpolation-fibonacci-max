namespace Coursework
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application. Initializes the application
        /// configuration and starts the main form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}