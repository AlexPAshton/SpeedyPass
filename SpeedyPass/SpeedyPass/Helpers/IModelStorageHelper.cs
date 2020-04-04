using SpeedyPass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedyPass.Helpers
{
    public interface IModelStorageHelper
    {
        T LoadModel<T>(string path);
        void SaveModel<T>(T model, string path);
    }
}
