using Xamarin.Forms;

namespace AddingMore
{
    public class App : Application
    {
        public bool IsReply { get; set; }

        public static App Self { get; private set; }

        public App()
        {
            Self = this;
            MainPage = new NavigationPage(new MessagePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

