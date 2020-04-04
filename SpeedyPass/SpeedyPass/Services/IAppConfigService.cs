using SpeedyPass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyPass.Services
{
    interface IAppConfigService
    {
        AppConfigModel AppConfigModel { get; set; }

        bool IsCreated();
        bool IsSetup();
        bool IsPinSetIfRequired();
    }
}
