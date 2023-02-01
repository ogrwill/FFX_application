using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MessageReceiver
{
    class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            AppSettings = config.GetSection("AppSettings").Get<AppSettings>();

        }

        public AppSettings AppSettings { get; private set; }
    }
}
