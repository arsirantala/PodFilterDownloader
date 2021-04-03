using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace IxothPodFilterDownloader
{
    public static class Utils
    {
        private static readonly SHA256 Sha256 = SHA256.Create();

        /// <summary>
        /// Write an exception to applications event log
        /// </summary>
        /// <param name="exception">Exception to be written to applications event log</param>
        public static void WriteExceptionToEventLog(Exception exception)
        {
            EventLog mEventLog = new EventLog("") { Source = "IxothPodFilterDownloader" };
            mEventLog.WriteEntry($"Computing sha256 failed due the reason of: {exception.Message}",
                EventLogEntryType.FailureAudit);
        }

        /// <summary>
        /// Compute the file's hash
        /// </summary>
        /// <param name="filename">Path to file from which the sha256 is calculated</param>
        /// <returns>Sha256 value</returns>
        public static byte[] GetHashSha256(string filename)
        {
            try
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    return Sha256.ComputeHash(stream);
                }
            }
            catch (Exception ex)
            {
                WriteExceptionToEventLog(ex);
                return new byte[0];
            }
        }

        /// <summary>
        /// Convert byte array to string.
        /// </summary>
        /// <param name="bytes">Byte array to be converted</param>
        /// <returns>Converted byte array as string</returns>
        public static string BytesToString(byte[] bytes)
        {
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        /// <summary>
        /// Get from Windows registry the installation location of Diablo 2
        /// </summary>
        /// <returns>If install location was found return it, otherwise return empty string</returns>
        public static string GetD2InstallLocationFromRegistry()
        {
            string d2LoDInstallPath = string.Empty;
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    "Software\\Blizzard Entertainment\\Diablo II"))
                {
                    Object o = key?.GetValue("InstallPath");
                    if (o != null)
                    {
                        d2LoDInstallPath = o.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteExceptionToEventLog(ex);
            }

            return d2LoDInstallPath;
        }
    }
}
