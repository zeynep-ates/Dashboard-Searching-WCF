using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace DashboardWCF2Lib
{
    public class DashboardJson
    {
        public DashboardJson() { }
        public string errorMessage {  get; set; }
        public string apiCallUrl { get; set; }
        public bool isSuccess { get; set; }
        public string ipAdress { get; set; } = ConfigurationManager.AppSettings["IpAdress"];
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

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ConfigurationManager.AppSettings["ApiKey"]);

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
                                //resultOk();
                            }
                            catch (Exception responseEx)
                            {
                                dashboardOutputModel = null;
                                errorMessage = "Grafana apiden response okunurken serialize hatası. uId:" + dashboardUid;
                                //_logger.Error(responseEx, OpResult.Message);
                            }
                        }
                        else
                        {
                            dashboardOutputModel = null;
                            errorMessage = "Grafana apiden okunurken response hatası. uId:" + dashboardUid + " " + response.ReasonPhrase;
                            //_logger.Error(OpResult.Message);
                        }
                    }
                    catch (Exception postEx)
                    {
                        dashboardOutputModel = null;
                        errorMessage = "Grafana apiden get edilirken hata. uId:" + dashboardUid;
                        //_logger.Error(postEx, OpResult.Message);
                    }
                }
            }
            catch (Exception clientEx)
            {
                dashboardOutputModel = null;
                errorMessage = "Grafana apiye httpclient oluşturulurken. uId:" + dashboardUid;
                //_logger.Error(clientEx, OpResult.Message);
            }

            return dashboardOutputModel;
        }

    }
}
