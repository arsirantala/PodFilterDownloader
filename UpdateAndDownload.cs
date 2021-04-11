using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using IniParser.Model;
using IxothPodFilterDownloader.Models;

namespace IxothPodFilterDownloader
{
    public static class UpdateAndDownload
    {
        public enum BoolEnum
        {
            True = 0,
            False = 1,
            None = 2
        }

        /// <summary>
        /// Check if the filter has content length for both to installed version and for server.
        /// </summary>
        /// <param name="filter">Name of the filter</param>
        /// <param name="data">Ini file content</param>
        /// <returns>True if content lengths don't match. None if one of the content lengths didn't exist</returns>
        public static BoolEnum ContentLengthCheck(string filter, IniData data)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return BoolEnum.None;
            }

            if (string.IsNullOrEmpty(data[filter].GetKeyData("server_content_length").Value))
            {
                return BoolEnum.None;
            }
            else
            {
                if (string.IsNullOrEmpty(data[filter].GetKeyData("installed_content_length").Value))
                {
                    return BoolEnum.None;
                }
                else
                {
                    if (data[filter].GetKeyData("server_content_length").Value !=
                        data[filter].GetKeyData("installed_content_length").Value)
                    {
                        return BoolEnum.True;
                    }
                }
            }

            return BoolEnum.False;
        }

        public static BoolEnum Sha256Check(string filter, IniData data)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return BoolEnum.None;
            }

            if (string.IsNullOrEmpty(data[filter].GetKeyData("downloaded_sha256").Value))
            {
                return BoolEnum.None;
            }
            else
            {
                if (string.IsNullOrEmpty(data[filter].GetKeyData("installed_sha256").Value))
                {
                    return BoolEnum.None;
                }
                else
                {
                    if (data[filter].GetKeyData("downloaded_sha256").Value !=
                        data[filter].GetKeyData("installed_sha256").Value)
                    {
                        return BoolEnum.True;
                    }
                }
            }

            return BoolEnum.False;
        }

        public static BoolEnum ETagCheck(string filter, IniData data)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return BoolEnum.None;
            }

            // Filter has ETags
            if (string.IsNullOrEmpty(data[filter].GetKeyData("downloaded_etag").Value))
            {
                return BoolEnum.None;
            }
            else
            {
                if (data[filter].GetKeyData("server_etag").Value !=
                    data[filter].GetKeyData("downloaded_etag").Value)
                {
                    return BoolEnum.True;
                }
            }

            return BoolEnum.False;
        }

        public static bool HasEtags(string filter, IniData data)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return false;
            }

            return !string.IsNullOrEmpty(data[filter].GetKeyData("downloaded_etag").Value) &&
                   !string.IsNullOrEmpty(data[filter].GetKeyData("server_etag").Value);
        }

        public static bool HasSha256Tags(string filter, IniData data)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return false;
            }

            return !string.IsNullOrEmpty(data[filter].GetKeyData("downloaded_sha256").Value) &&
                   !string.IsNullOrEmpty(data[filter].GetKeyData("installed_sha256").Value);
        }

        /// <summary>
        /// Get ETag and content-length for the installed PoD filters in the servers
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="iniData"></param>
        /// <param name="poDInstallLocation"></param>
        /// <returns></returns>
        public static async Task<List<FilterHttpHeaderDataModel>> RunGetFilterHttpHeadersInParallelAsync(
            IProgress<ProgressReportModel> progress, IniData iniData, string poDInstallLocation)
        {
            List<string> sources = new List<string>();

            foreach (var filter in iniData.Sections)
            {
                if (File.Exists($"{poDInstallLocation}\\{frmMain.FilterDirectoryName}\\{filter.SectionName}.filter"))
                {
                    sources.Add(filter.SectionName);
                }
            }

            List<FilterHttpHeaderDataModel> output = new List<FilterHttpHeaderDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            await Task.Run(() =>
            {
                Parallel.ForEach(sources, (filter) =>
                {
                    FilterHttpHeaderDataModel results = new FilterHttpHeaderDataModel();
                    results = GetFilterHttpHeaders(iniData, filter);
                    output.Add(results);

                    report.PercentageComplete = (output.Count * 100) / sources.Count;
                    progress.Report(report);
                });
            });

            return output;
        }

        private static FilterHttpHeaderDataModel GetFilterHttpHeaders(IniData iniData, string filter)
        {
            FilterHttpHeaderDataModel output = new FilterHttpHeaderDataModel {FilterName = filter};

            var filterUrl = iniData[filter].GetKeyData("download_url").Value;
            var webRequest = WebRequest.Create(filterUrl);
            webRequest.Method = "HEAD";

            try
            {
                using (var webResponse = webRequest.GetResponse())
                {
                    output.ETag = webResponse.Headers["ETag"];
                    output.ContentLength = webResponse.ContentLength.ToString();
                    output.HttpStatusCode = ((HttpWebResponse)webResponse).StatusCode;
                }
            }
            catch (Exception exception)
            {
                var test = exception.GetBaseException();
                output.HttpStatusCode = ((HttpWebResponse) ((WebException) test).Response).StatusCode;
            }

            return output;
        }

        public static async Task<List<FilterContentDataModel>> RunGetFilterContentInParallelAsync(
            IProgress<ProgressReportModel> progress, IniData iniData, string poDInstallLocation, List<string> wantedFilters)
        {
            List<FilterContentDataModel> output = new List<FilterContentDataModel>();
            ProgressReportModel report = new ProgressReportModel();

            await Task.Run(() =>
            {
                Parallel.ForEach(wantedFilters, (filter) =>
                {
                    FilterContentDataModel results = GetFilterContent(iniData, filter);
                    output.Add(results);

                    report.PercentageComplete = (output.Count * 100) / wantedFilters.Count;
                    progress.Report(report);
                });
            });

            return output;
        }

        public static string DownloadFileContent(string url)
        {
            string content = string.Empty;

            using (var wc = new WebClient())
            {
                try
                {
                    content = wc.DownloadString(new Uri(url));
                }
                catch (Exception)
                {
                }
            }

            return content;
        }

        private static FilterContentDataModel GetFilterContent(IniData iniData, string filter)
        {
            FilterContentDataModel output = new FilterContentDataModel { FilterName = filter };

            var filterUrl = iniData[filter].GetKeyData("download_url").Value;

            using (var wc = new WebClient())
            {
                try
                {
                    output.Content = wc.DownloadString(new Uri(filterUrl));

                    if (wc.ResponseHeaders != null)
                    {
                        output.ETag = wc.ResponseHeaders["ETag"] ?? "";
                        output.ContentLength = wc.ResponseHeaders["content-length"] ?? "";
                        output.HttpStatusCode = HttpStatusCode.OK; // Can't really get this but if no exception occurs then it should be OK
                    }
                }
                catch (Exception exception)
                {
                    var test = exception.GetBaseException();
                    output.HttpStatusCode = ((HttpWebResponse)((WebException)test).Response).StatusCode;
                }
            }

            return output;
        }
    }
}
