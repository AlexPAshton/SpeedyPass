using System.Windows;

namespace SpeedyPass
{
    public static class ExceptionMessageBox
    {
        private const string EXCEPTIONMSG_TITLE = "{0} Exception Thrown";
        private const string EXCEPTIONMSG_MESSAGE = "A fatal error has occured, the application will shutdown.\n\n{0}";

        public static void Show(string appName, string message)
        {
            MessageBox.Show(string.Format(ExceptionMessageBox.EXCEPTIONMSG_MESSAGE, message), string.Format(ExceptionMessageBox.EXCEPTIONMSG_TITLE, appName));
            Application.Current.Shutdown();
        }
    }
}
