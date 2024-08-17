using System;
using System.Collections.Generic;

namespace DashboardWCF2Lib
{
    public class SearchingClass
    {
        GetUidList getUidList = new GetUidList();
        DashboardJson dashboardJson = new DashboardJson();
        public List<SearchResult> SearchingID(string userInput)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
            if (int.TryParse(userInput, out int a))
            {
                foreach (var uid in getUidList.UidList)
                {
                    dynamic dashboardOutputModel = dashboardJson.getDashboardJson(uid).Result;

                    foreach (var panel in dashboardOutputModel.dashboard.panels)
                    {
                        foreach (var target in panel.targets)
                        {
                            if (target.rawSql.Replace(" ", "").Contains(String.Concat("id=", userInput)))
                            {
                                SearchResult searchResult = new SearchResult();
                                string panelUrl = "http://" + dashboardJson.ipAdress + ":3000/d/" + uid + "/" + dashboardOutputModel.dashboard.title + "?orgId=1&viewPanel=" + panel.id;
                                searchResult.panelUrl = panelUrl;
                                searchResult.uid = uid;
                                searchResult.isSuccess = true;
                                searchResult.panelTitle = panel.title;
                                searchResults.Add(searchResult);
                            }
                        }
                    }
                }
            }
            else
            {
                SearchResult searchResult = new SearchResult();
                searchResult.isSuccess = false;
                searchResult.errorMessage = "Telemetry ID must be an integer.";
                searchResults.Add(searchResult);
            }


            if (searchResults.Count == 0)
            {
                SearchResult searchResult = new SearchResult();
                searchResult.isSuccess = false;
                searchResult.errorMessage = "This ID does not appear in any panels.";
                searchResults.Add(searchResult);
            }

            return searchResults;

        }

        public List<SearchResult> SearchingName(string userInput)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
            foreach (var uid in getUidList.UidList)
            {
                dynamic dashboardOutputModel = dashboardJson.getDashboardJson(uid).Result;

                foreach (var panel in dashboardOutputModel.dashboard.panels)
                {
                    if (panel.title.Contains(userInput))
                    {
                        SearchResult searchResult = new SearchResult();
                        string panelUrl = "http://" + dashboardJson.ipAdress + ":3000/d/" + uid + "/" + dashboardOutputModel.dashboard.title + "?orgId=1&viewPanel=" + panel.id;
                        searchResult.panelUrl = panelUrl;
                        searchResult.uid = uid;
                        searchResult.isSuccess = true;
                        searchResult.panelTitle = panel.title;
                        searchResults.Add(searchResult);
                    }
                }

            }

            if (searchResults.Count == 0)
            {
                SearchResult searchResult = new SearchResult();
                searchResult.isSuccess = false;
                searchResult.errorMessage = "This name does not appear in any panels.";
                searchResults.Add(searchResult);
            }

            return searchResults;

        }
    }
}
