﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _Client;

        protected string ServiceAddress { get; }

        protected BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            this.ServiceAddress = ServiceAddress;
            _Client = new HttpClient
            {
                BaseAddress = new Uri(Configuration["ClientAddress"])
            };

            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
