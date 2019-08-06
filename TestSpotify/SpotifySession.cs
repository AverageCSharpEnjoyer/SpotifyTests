using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace TestSpotify
{
    public class SpotifySession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string SpotifyAppId = "C:\\Users\\zieli\\AppData\\Local\\Microsoft\\WindowsApps\\Spotify.exe";

        protected static WindowsDriver<WindowsElement> session;

        public static void Setup(TestContext context)
        {
            if (session == null)
            {
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", SpotifyAppId);
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(session);

                session.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(4.0));
            }
        }

        public static void TearDown()
        {
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}
