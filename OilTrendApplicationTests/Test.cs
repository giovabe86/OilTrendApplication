using Microsoft.AspNetCore.WebUtilities;
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

        [Fact]
        public async Task ValidResponseEmpty()
        {
            int port = 5000;
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
            int port = 5000;
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


    }
}
