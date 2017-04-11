namespace TForTea.Services
{
    using Managers;
    using Microsoft.Toolkit.Uwp;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TForTea.Models;
    using Windows.Web.Http;

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
                if (!NetworkHelper.Instance.ConnectionInformation.IsInternetAvailable)
                {
                    return null;
                }

                using (var request = new HttpHelperRequest(new Uri("http://xtea.azurewebsites.net/api/tea"), HttpMethod.Get))
                {
                    using (var response = await HttpHelper.Instance.SendRequestAsync(request))
                    {
                        var textResult = await response.GetTextResultAsync();
                        var teaList = JsonConvert.DeserializeObject<List<Tea>>(textResult);
                        return teaList;
                    }
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