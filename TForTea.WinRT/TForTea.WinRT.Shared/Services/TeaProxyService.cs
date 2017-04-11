using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TForTea.Managers;
using TForTea.WinRT.Models;

namespace TForTea.WinRT.Services
{
    public class TeaProxyService
    {
        private LogManager logManager;

        public TeaProxyService(LogManager logManager)
        {
            this.logManager = logManager;
        }

        public async Task<List<Tea>> GetTeaList()
        {
            try
            {
                //if (!NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
                //{
                //    return null;
                //}

                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync("http://xtea.azurewebsites.net/api/tea"))
                using (HttpContent content = response.Content)
                {
                    string result = await content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Tea>>(result);
                }
            }
            catch (Exception ex)
            {
                this.logManager.Logger.Error(ex.StackTrace);
                return null;
            }
        }
    }
}