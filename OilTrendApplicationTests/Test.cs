using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace OilTrendApplicationTests
{

    public class Test
    {
        public static String WRONG_METHOD_ERROR_CODE = "-32601";
        public static String WRONG_PARAMETER_ERROR_CODE = "-32603";

        [Fact]
        public async Task ValidResponseEmpty()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            var query = new Dictionary<string, string>
            {
                ["id"] = "1",
                ["method"] = "oilprice.Trend",
                ["startDateISO8601"] = "2009-01-01",
                ["endDateISO8601"] = "2009-01-01",
            };

            var response = await client.GetAsync(QueryHelpers.AddQueryString(client.BaseAddress.AbsoluteUri, query));
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            String expectedResponseString = "{\"jsonrpc\":\"2.0\",\"id\":\"1\",\"result\":{\"prices\":[]},\"error\":null}";
            Assert.Equal(expectedResponseString, responseString, ignoreCase: false, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public async Task ValidResponseListNotEmpty()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            var query = new Dictionary<string, string>
            {
                ["id"] = "1",
                ["method"] = "oilprice.Trend",
                ["startDateISO8601"] = "2015-01-02",
                ["endDateISO8601"] = "2015-01-02",
            };

            var response = await client.GetAsync(QueryHelpers.AddQueryString(client.BaseAddress.AbsoluteUri, query));
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            String expectedResponseString = "{\"jsonrpc\":\"2.0\",\"id\":\"1\",\"result\":{\"prices\":[{\"dateISO8601\":\"2015-01-02\",\"price\":55.38}]},\"error\":null}";
            Assert.Equal(expectedResponseString, responseString, ignoreCase: false, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
        }


        [Fact]
        public async Task ValidResponseWrongMethod()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            var query = new Dictionary<string, string>
            {
                ["id"] = "1",
                ["method"] = "oilpric.Trend",
                ["startDateISO8601"] = "2015-01-02",
                ["endDateISO8601"] = "2015-01-02",
            };

            var response = await client.GetAsync(QueryHelpers.AddQueryString(client.BaseAddress.AbsoluteUri, query));
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            OilTrendApplication.Model.JsonRpcResponse deserialized = JsonConvert.DeserializeObject<OilTrendApplication.Model.JsonRpcResponse>(responseString);

            Assert.Equal(WRONG_METHOD_ERROR_CODE, deserialized.error.code.ToString(), ignoreCase: false, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public async Task ValidResponseWrongParameter()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            var query = new Dictionary<string, string>
            {
                ["id"] = "1",
                ["method"] = "oilprice.Trend",
                ["start"] = "2015-01-02",
                ["endDateISO8601"] = "2015-01-02",
            };

            var response = await client.GetAsync(QueryHelpers.AddQueryString(client.BaseAddress.AbsoluteUri, query));
            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            OilTrendApplication.Model.JsonRpcResponse deserialized = JsonConvert.DeserializeObject<OilTrendApplication.Model.JsonRpcResponse>(responseString);

            Assert.Equal(WRONG_PARAMETER_ERROR_CODE, deserialized.error.code.ToString(), ignoreCase: false, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);

        }
    }
}