using System;
using System.Configuration;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumFramework.Core
{
    public static class LocalWebdriver
    {
        public static IWebDriver CreateFirefoxDriver()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = GetPathToFireFoxBinaryFile();

            FirefoxProfile ffp = new FirefoxProfile();
            ffp.SetPreference("intl.accept_languages", "en");
            ffp.SetPreference("intl.accept_languages", "lv");
            ffp.SetPreference("intl.accept_languages", "ru");

            ffp.SetPreference("browser.translation.detectLanguage", "false");
            options.Profile = ffp;
            FirefoxDriver firefoxDriver = new FirefoxDriver(options);
            firefoxDriver.Manage().Window.Maximize();
            return firefoxDriver;
        }

        public static void TurnOnImplicitlyWait(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = Settings.ImplicitWaitTimeOut;
        }

        public static void TurnOffImplicitlyWait(this IWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        private static string  GetPathToFireFoxBinaryFile()
        {
           string ffPathToBinary=ConfigurationSettings.AppSettings.Get("FireFoxBinaryPath");
            if (!File.Exists(ffPathToBinary))
            {
                throw new ConfigurationException("Firefox path to binary is not defined. Please set path to Firefox binary in EvolutionGamingQaTask > Tests project > App.config file > key = FireFoxBinaryPath");
            }

            return ffPathToBinary;
        }
    }
}