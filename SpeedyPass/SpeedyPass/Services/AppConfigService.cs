using CustomUnity;
using SpeedyPass.Helpers;
using SpeedyPass.Models;
using System.IO;

namespace SpeedyPass.Services
{
    public class AppConfigService : IAppConfigService
    {
        private const string CONFIG_PATH = "./Data/config.dat";

        public AppConfigModel AppConfigModel 
        { 
            get => this.appConfigModel;
            set
            {
                this.appConfigModel = value;
                this.appConfigModel.OnUpdated += AppConfigModel_OnUpdated;

                this.AppConfigModel_OnUpdated();
            }
        }

        private IModelStorageHelper modelStorageHelper;
        private AppConfigModel appConfigModel;

        public AppConfigService()
        {
            this.modelStorageHelper = CustomUnityContainer.Resolve<IModelStorageHelper>();

            this.LoadModel();
        }

        private void LoadModel()
        {
            this.appConfigModel = null;

            if (this.IsCreated())
            {
                this.appConfigModel = this.modelStorageHelper.LoadModel<AppConfigModel>(AppConfigService.CONFIG_PATH);
            }
            
            if (this.appConfigModel ==  null)
            {
                this.appConfigModel = new AppConfigModel();
            }

            this.appConfigModel.OnUpdated += this.AppConfigModel_OnUpdated;
        }

        private void AppConfigModel_OnUpdated()
        {
            this.modelStorageHelper.SaveModel(this.appConfigModel, AppConfigService.CONFIG_PATH);
        }

        public void Refresh()
        {
            this.LoadModel();
        }

        public bool IsCreated()
        {
            return File.Exists(AppConfigService.CONFIG_PATH);
        }

        public bool IsSetup()
        {
            return this.appConfigModel.Setup;
        }

        public bool IsPinSetIfRequired()
        {
            return (!this.appConfigModel.UsePin || this.appConfigModel.Pin != null);
        }
    }
}
