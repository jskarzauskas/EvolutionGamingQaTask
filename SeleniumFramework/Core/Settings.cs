using System;

namespace SeleniumFramework.Core
{
    public static class Settings
    {
        public static readonly TimeSpan TimeOutInSeconds = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan ImplicitWaitTimeOut = TimeSpan.FromSeconds(30);
    }
}