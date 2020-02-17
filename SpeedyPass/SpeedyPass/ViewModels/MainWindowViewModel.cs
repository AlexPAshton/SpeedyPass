using System.Collections.Generic;

namespace SpeedyPass
{
    public class MainWindowViewModel
    {
        private List<PasswordDataModel> passwordDataModelList = new List<PasswordDataModel>();
        private string versionString = "15/02/2020";

        public List<PasswordDataModel> PasswordDataModelList { get => passwordDataModelList; set => passwordDataModelList = value; }
        public string VersionString { get => versionString; set => versionString = value; }
    }
}
