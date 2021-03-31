using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using IniParser.Model;
using IxothPodFilterDownloader.Models;

namespace IxothPodFilterDownloader
{
    public static class UpdateAndDownload
    {
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
                if (File.Exists($"{poDInstallLocation}\\filter\\{filter.SectionName}.filter"))
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
                    FilterHttpHeaderDataModel results = GetFilterHttpHeaders(iniData, filter);
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

            using (var webResponse = webRequest.GetResponse())
            {
                output.ETag = webResponse.Headers["ETag"];
                output.ContentLength = webResponse.ContentLength.ToString();
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

        private static FilterContentDataModel GetFilterContent(IniData iniData, string filter)
        {
            FilterContentDataModel output = new FilterContentDataModel { FilterName = filter };

            var filterUrl = iniData[filter].GetKeyData("download_url").Value;

            using (var wc = new WebClient())
            {
                output.Content = wc.DownloadString(new Uri(filterUrl));

                if (wc.ResponseHeaders != null)
                {
                    output.ETag = wc.ResponseHeaders["ETag"] ?? "";
                    output.ContentLength = wc.ResponseHeaders["content-length"] ?? "";
                }
            }

            return output;
        }
    }
}
