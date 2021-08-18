using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using CP380_B1_BlockList.Models;
using Microsoft.AspNetCore.Mvc;

namespace CP380_B3_BlockBlazor.Data
{
    public class BlockService
    {
        // TODO: Add variables for the dependency-injected resources
        //       - httpClient
        //       - configuration
        //
        static HttpClient _clientFactory;
        private IConfiguration _config;

        //
        // TODO: Add a constructor with IConfiguration and IHttpClientFactory arguments
        //
        public BlockService() { }
        public BlockService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory.CreateClient();
            _config = configuration.GetSection("BlockService");
        }

        //
        // TODO: Add an async method that returns an IEnumerable<Block> (list of Blocks)
        //       from the web service
        //
        public async Task<IEnumerable<Block>> ListBlocks()
        {
            var response = await _clientFactory.GetAsync(_config["url"]);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IEnumerable<Block>>(responseStream);
            }
            return Array.Empty<Block>();
        }
    }
}
