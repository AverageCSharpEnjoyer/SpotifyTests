using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using System.Threading;
using System.Linq;

namespace TestSpotify
{
    [TestClass]
    public class SpotifyTests:SpotifySession
    {
        [TestClass]
        public class ScenarioStandard : SpotifySession
        {

            [TestMethod]
            public void CreatePlaylist()
            {
                session.FindElementByName("Strona główna").Click();
                string playlistName = RandomString(10);
                string playlistDescription = RandomString(10);

                var newPlaylist = session.FindElementByName("Utwórz nową playlistę");
                newPlaylist.Click();

                var pName = session.FindElementByName("Nazwa");
                var pDesc = session.FindElementByName("Opis");

                pName.SendKeys(playlistName);
                pDesc.SendKeys(playlistDescription);

                var createPlaylist = session.FindElementByName("Utwórz");
                createPlaylist.Click();
                createPlaylist.Click();


                var result = session.FindElementByName("Playlist");
                Assert.IsNotNull(result);


            }

            [TestMethod]
            public void SaveToFavourite()
            {
                session.FindElementByName("Strona główna").Click();
                //save from playlista1
                session.FindElementByName("playlista1").Click();
                session.FindElementsByName("Zapisz w Ulubionych utworach")[0].Click();

                session.FindElementByName("Ulubione utwory").Click();
                var result = session.FindElementByName("Crazy według Gnarls Barkley. Naciśnij Shift+Enter, aby odtworzyć.");
                Assert.IsNotNull(result);

            }

          

            private static Random random = new Random();
            public static string RandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            }

            [TestMethod]
            public void DeleteFromFavourite()
            {
                session.FindElementByName("Strona główna").Click();
                session.FindElementByName("Ulubione utwory").Click();

                try
                {
                    session.FindElementsByName("Usuń z Ulubionych utworów")[0].Click();

                } catch (Exception e)
                {
                    throw new Exception("Can't find the element. Playlist must be empty.");
                }
                
                var result = session.FindElementByName("PRZEJDŹ DO KATALOGU PRZEGLĄDANIA");
                Assert.IsNotNull(result);
            }



            [ClassInitialize]
            public static void ClassInitialize(TestContext context)
            {
                // Create session to launch Spotify
                Setup(context);
                
            }

            [ClassCleanup]
            public static void ClassCleanup()
            {
                TearDown();
            }

        }
    }
}
