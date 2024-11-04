using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public class Cartype
    {
        public CarNum carNum;

        private readonly ILogger _logger;
        public Cartype(ILogger<Cartype> logger)
        {
            this._logger = logger;
            _logger.Log(LogLevel.Debug, "Hello");
        }

        public string typename { get ; set; }
    }
}
