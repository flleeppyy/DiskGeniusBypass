using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Security.Principal;
using static DiskGeniusBypass.LicenseFileHandler;

namespace DiskGeniusBypass
{
    class DiskGeniusBypass
    {
        private static string DiskGeniusPath = Directory.GetCurrentDirectory();
        private static string[] LicenseFiles =
        {
            "msimg32.dll",
            "OfflineReg.exe",
            "Options.ini"
        };

        private static Process DGProcess;

        private static bool DGexited = false;

        private static string DiskGeniusExecutable = "DiskGenius_.exe";

        // Memory or MoveFiles
        private static StorageMethod StoreType = StorageMethod.Memory;

        public static void Main(string[] args)
        {
            // Check if we're an Administrator
            if (!IsAdministrator())
            {
                MessageBox.Show("The program is not running as administrator. Please run as administrator", "Failed to start", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            formatLicensePaths();

            Bypass();
        }

        public static void formatLicensePaths()
        {
            for (int i = 0; i < LicenseFiles.Length; i++)
            {
                LicenseFiles[i] = Path.Combine(DiskGeniusPath,LicenseFiles[i]);
            }
        }
        public static void Bypass()
        {
            string path = Path.Combine(DiskGeniusPath, DiskGeniusExecutable);
            if (File.Exists(path))
            {

                // Check if there's a previous process and kill it/dispose of it.
                if (DGProcess != null)
                {
                    //(Process DGProcess.HasExited) ? DGProcess.Kill() : DGProcess.Dispose();
                    if (!DGProcess.HasExited) 
                        DGProcess.Kill();
                    DGProcess.Dispose();
                    DGexited = false;
                }

                // Create the process
                DGProcess = new Process
                {
                    StartInfo = new ProcessStartInfo(path),
                    EnableRaisingEvents = true
                };
                DGProcess.Exited += new EventHandler(DiskGenius_Exited);

                // Store the files
                LicenseFileHandler fileHandler = new LicenseFileHandler(LicenseFileHandler.StorageMethod.Memory, LicenseFiles);
                fileHandler.Store();

                // Start it
                DGProcess.Start();

                Thread.Sleep(3000);
                if (DGexited)
                {
                    MessageBox.Show("The bypass most likely failed since DiskGenius exited early.", "Bypass failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                fileHandler.Restore();
            }
            else
            {
                MessageBox.Show("DiskGenius_.exe could not be found. Did you forget to rename it? Are you in the right folder?", "Warning");
            }
        }

        // Handle Exited event and display process information.
        private static void DiskGenius_Exited(object sender, EventArgs e)
        {
            
        }

        // http://stackoverflow.com/questions/11660184/ddg#11660205
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        // This method stores the license files in 
        
    }
}
