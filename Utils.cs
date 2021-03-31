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
        private static readonly SHA256 _sha256 = SHA256.Create();

        /// <summary>
        /// Compute the file's hash
        /// </summary>
        /// <param name="filename">Path to file from which the sha256 is calculated</param>
        /// <returns>Sha256 value</returns>
        public static byte[] GetHashSha256(string filename)
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                return _sha256.ComputeHash(stream);
            }
        }

        public static string BytesToString(byte[] bytes)
        {
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }

        public static string GetD2InstallLocationFromRegistry()
        {
            string d2LoDInstallPath = "";
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
                Debug.WriteLine(ex.Message);
            }

            return d2LoDInstallPath;
        }
    }
}
