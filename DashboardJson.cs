using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace DashboardWCF2Lib
{
    public class DashboardJson
    {
        public string errorMessage {  get; set; }
        public string apiCallUrl { get; set; }
        public bool isSuccess { get; set; }
        public string ipAdress { get; set; } = "10.1.150.40";
        public async Task<System.Dynamic.ExpandoObject> getDashboardJson(string dashboardUid)
        {
            string grafanaApiUrl = "http://" + ipAdress + ":3000"; 

            System.Dynamic.ExpandoObject dashboardOutputModel = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(grafanaApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "[API key]");

                    try
                    {
                        apiCallUrl = grafanaApiUrl + "/api/dashboards/uid/" + dashboardUid;
                        HttpResponseMessage response = await client.GetAsync(apiCallUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                var outputResponse = response.Content.ReadAsStringAsync().Result;
                                dashboardOutputModel = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(outputResponse);
                                isSuccess = true;
                            }
                            catch (Exception responseEx)
                            {
                                dashboardOutputModel = null;
                                errorMessage = "Grafana apiden response okunurken serialize hatası. uId:" + dashboardUid;
                            }
                        }
                        else
                        {
                            dashboardOutputModel = null;
                            errorMessage = "Grafana apiden okunurken response hatası. uId:" + dashboardUid + " " + response.ReasonPhrase;
                        }
                    }
                    catch (Exception postEx)
                    {
                        dashboardOutputModel = null;
                        errorMessage = "Grafana apiden get edilirken hata. uId:" + dashboardUid;
                    }
                }
            }
            catch (Exception clientEx)
            {
                dashboardOutputModel = null;
                errorMessage = "Grafana apiye httpclient oluşturulurken. uId:" + dashboardUid;
            }

            return dashboardOutputModel;
        }

    }
}
