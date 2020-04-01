using SpeedyPass.Models;
using SpeedyPass.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpeedyPass.Controllers
{
    public class MainWindowController
    {
        private const string APP_DYN_CONFIG_PATH = "./Data/config.dat";
        private const string DEFAULT_PSWD_DATA_PATH = "./SpeedyPass.dat";

        private MainWindow view;
        private MainWindowViewModel viewModel;
        private ApplicationConfigModel applicationConfigModel;
        private PersistantModelStorageService persistantModelStorageService;
        private PersistantLoggingService persistantLoggingService;
        private FileChangeWatcherService fileChangeWatcherService;

        public MainWindowController(string overridePasswordDataPath = null)
        {
            this.view = new MainWindow();
            this.viewModel = new MainWindowViewModel()
            {
                VersionString = this.GetBuildNumber(),
            };

            this.persistantLoggingService = new PersistantLoggingService();
            this.persistantModelStorageService = new PersistantModelStorageService();
            this.fileChangeWatcherService = new FileChangeWatcherService();

            //Temporary
            if (overridePasswordDataPath != null)
            {
                this.applicationConfigModel = new ApplicationConfigModel
                {
                    PasswordDataPath =string.Format("{0}{1}", overridePasswordDataPath, "/SpeedyPass.dat"),
                };

                this.persistantModelStorageService.Save(MainWindowController.APP_DYN_CONFIG_PATH, this.applicationConfigModel);
            }

            this.LoadDynamicApplicationConfig();
            this.LoadDynamicPasswordData();

            this.fileChangeWatcherService.Watch(this.applicationConfigModel.PasswordDataPath);
            this.fileChangeWatcherService.OnFileChanged += this.FileChangeWatcherService_OnFileChanged;

            this.view.BindViewModel(this.viewModel);
            this.view.BindController(this);
        }

        private void FileChangeWatcherService_OnFileChanged()
        {
            this.LoadDynamicPasswordData();
        }

        public void LoadDynamicApplicationConfig()
        {
            this.applicationConfigModel = this.persistantModelStorageService.Load<ApplicationConfigModel>(MainWindowController.APP_DYN_CONFIG_PATH, PersistantModelStorageService.StorageTypes.Default);

            if (this.applicationConfigModel == null)
            {
                this.applicationConfigModel = new ApplicationConfigModel
                {
                    PasswordDataPath = MainWindowController.DEFAULT_PSWD_DATA_PATH,
                };

                this.persistantModelStorageService.Save(MainWindowController.APP_DYN_CONFIG_PATH, this.applicationConfigModel);
            }
        }

        private void LoadDynamicPasswordData()
        {
            this.viewModel.PasswordDataModelList =
                this.persistantModelStorageService.Load<List<PasswordDataModel>>(
                    this.applicationConfigModel.PasswordDataPath,
                PersistantModelStorageService.StorageTypes.Encoded);

            if (this.viewModel.PasswordDataModelList == null)
            {
                this.viewModel.PasswordDataModelList = new List<PasswordDataModel>();

                this.persistantModelStorageService.Save(
                    this.applicationConfigModel.PasswordDataPath,
                    this.viewModel.PasswordDataModelList,
                    PersistantModelStorageService.StorageTypes.Encoded);
            }

            this.view.BindViewModel(this.viewModel);
        }

        private string GetBuildNumber()
        {
            try
            {
                return File.GetCreationTime("./SpeedyPass.exe").ToString("ddMMyyyy.HHmmss");
            }
            catch (Exception ex)
            {
                this.persistantLoggingService.Log(ex);
                return "?";
            }
        }

        public void ShowAddPasswordDataDialog()
        {
            ViewAddPassword viewAddPassword = new ViewAddPassword();
            viewAddPassword.ShowDialog();

            PasswordDataModel passwordDataModel = viewAddPassword.Result;

            if (passwordDataModel.Domain != null)
            {
                this.viewModel.PasswordDataModelList.Add(passwordDataModel);

                this.persistantModelStorageService.Save(
                    this.applicationConfigModel.PasswordDataPath,
                    this.viewModel.PasswordDataModelList,
                    PersistantModelStorageService.StorageTypes.Encoded);

                this.view.DataContext = null;
                this.view.DataContext = this.viewModel;
            }
        }

        public void DeletePasswordData(string domain)
        {
            if (domain != null && domain != string.Empty)
            {
                this.viewModel.PasswordDataModelList.RemoveAll(s => s.Domain == domain);

                this.persistantModelStorageService.Save(
                    this.applicationConfigModel.PasswordDataPath,
                    this.viewModel.PasswordDataModelList,
                    PersistantModelStorageService.StorageTypes.Encoded);

                this.view.DataContext = null;
                this.view.DataContext = this.viewModel;
            }
        }
    }
}
