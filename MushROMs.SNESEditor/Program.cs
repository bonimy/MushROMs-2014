using System;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using MushROMs.Editors;
using MushROMs.LunarCompress;
using MushROMs.SNESEditor.GFXEditor;
using MushROMs.SNESEditor.PaletteEditor;
using MushROMs.SNESEditor.Properties;

namespace MushROMs.SNESEditor
{
	/// <summary>
	/// Provides universal program data for the application.
	/// </summary>
    public static class Program
    {
		/// <summary>
		/// The fallback <see cref="FPSModes"/> value for the application.
		/// This field is constant.
		/// </summary>
		private const FPSModes FallbackFPSMode = FPSModes.NTSC;
		/// <summary>
		/// The fallback <see cref="FrameReductions"/> value for the application.
		/// This field is constant.
		/// </summary>
		private const FrameReductions FallbackFrameReduction = FrameReductions.None;

		/// <summary>
		/// The <see cref="FPSModes"/> value for the application.
		/// </summary>
		private static FPSModes fpsMode;
		/// <summary>
		/// The <see cref="FrameReductions"/> value for the application.
		/// </summary>
		private static FrameReductions frameReduction;

		/// <summary>
		/// The number of calls per second the <see cref="EventTimer"/> has
		/// </summary>
		private static double frequency;
		/// <summary>
		/// The time, in milliseconds, between calls form the <see cref="EventTimer"/>.
		/// </summary>
		private static double interval;

		/// <summary>
		/// The <see cref="EventTimer"/> used throughout the application.
		/// </summary>
		private static EventTimer animator;

		/// <summary>
		/// Gets the <see cref="EventTimer"/> used throughout the application.
		/// </summary>
		public static EventTimer Animator
		{
			get { return Program.animator; }
		}

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Program.ResetFrameRateSettings();
			Program.animator = new EventTimer(Program.interval);
			Program.animator.Start();

            if (Settings.Default.FirstTime)
            {
                Settings.Default.LastPaletteFiles = new StringCollection();
                Settings.Default.LastGFXFiles = new StringCollection();
                Settings.Default.FirstTime = false;
                Settings.Default.Save();
            }

            if (!File.Exists(Settings.Default.DefaultPalettePath))
            {
                try
                {
                    File.WriteAllBytes(Settings.Default.DefaultPalettePath, Resources.DefaultPalette);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            Application.Run(new PaletteMdiForm());
        }

		/// <summary>
		/// Reset the framerate settings to their fallback values.
		/// </summary>
		public static void ResetFrameRateSettings()
		{
			Program.fpsMode = FallbackFPSMode;
			Program.frameReduction = FallbackFrameReduction;
			Program.frequency = (double)Program.fpsMode / (double)Program.frameReduction;
			Program.interval = 1000.0 / Program.frequency;
		}

#if !DEBUG
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;

                MessageBox.Show("Whoops! Please contact the developers with the following"
                      + " information:\n\n" + ex.Message + ex.StackTrace,
                      "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                Application.Exit();
            }
        }
#endif
    }
}