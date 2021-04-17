using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.UiAutomator;
using System;

namespace AppiumTester
{
    [TestClass]
    public class CounterFragmentUnitTest
    {
        private AndroidDriver<AppiumWebElement> driver;
        private readonly AndroidUiSelector txtCounterSelector = new AndroidUiSelector().ResourceIdMatches(".*:id/txtCounter"); // resource id regex
        private readonly AndroidUiSelector btnIncreaseSelector = new AndroidUiSelector().ResourceIdMatches(".*:id/btnIncrease");
        private readonly AndroidUiSelector btnDecreaseSelector = new AndroidUiSelector().ResourceIdMatches(".*:id/btnDecrease");
        private readonly AndroidUiSelector btnResetSelector = new AndroidUiSelector().ResourceIdMatches(".*:id/btnReset");

        [TestInitialize]
        public void TestInit()
        {
            AppiumOptions capabilities = new AppiumOptions();

            capabilities.AddAdditionalCapability("deviceName", "device29");
            capabilities.AddAdditionalCapability("platformName", "Android");
            capabilities.AddAdditionalCapability("app", @"C:\Sapir\app-debug.apk");

            driver = new AndroidDriver<AppiumWebElement>(
                                       new Uri("http://127.0.0.1:4723/wd/hub"),
                                           capabilities);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void TestDefaultCounterText()
        {
            //sample - https://github.com/appium/appium-dotnet-driver/wiki/Android-Sample
            //like Xpath - https://github.com/appium/appium-dotnet-driver/blob/master/src/Appium.Net/Appium/Android/UiAutomator/AndroidUiSelector.cs

            AppiumWebElement textCounterElem = driver.FindElementByAndroidUIAutomator(txtCounterSelector);

            Assert.AreEqual("0", textCounterElem.Text);
        }

        [TestMethod]
        public void TestIncrease()
        {
            AppiumWebElement textCounterElem = driver.FindElementByAndroidUIAutomator(txtCounterSelector);
            AppiumWebElement btnIncreaseElem = driver.FindElementByAndroidUIAutomator(btnIncreaseSelector);

            Assert.AreEqual("0", textCounterElem.Text);
            btnIncreaseElem.Click();
            Assert.AreEqual("1", textCounterElem.Text);
        }

        [TestMethod]
        public void TestDecrease()
        {
            AppiumWebElement textCounterElem = driver.FindElementByAndroidUIAutomator(txtCounterSelector);
            AppiumWebElement btnDecreaseElem = driver.FindElementByAndroidUIAutomator(btnDecreaseSelector);

            Assert.AreEqual("0", textCounterElem.Text);
            btnDecreaseElem.Click();
            Assert.AreEqual("-1", textCounterElem.Text);
        }

        [TestMethod]
        public void TestReset()
        {
            AppiumWebElement textCounterElem = driver.FindElementByAndroidUIAutomator(txtCounterSelector);
            AppiumWebElement btnIncreaseElem = driver.FindElementByAndroidUIAutomator(btnIncreaseSelector);
            AppiumWebElement btnResetElem = driver.FindElementByAndroidUIAutomator(btnResetSelector);

            // Click increase 4 times
            Assert.AreEqual("0", textCounterElem.Text);
            btnIncreaseElem.Click();
            btnIncreaseElem.Click();
            btnIncreaseElem.Click();
            btnIncreaseElem.Click();
            Assert.AreEqual("4", textCounterElem.Text);

            // Click reset
            btnResetElem.Click();
            Assert.AreEqual("0", textCounterElem.Text);
        }

        /* NOTES

           apk or zip only, the default activity will be launched ('app' capability)
           apk + activity ('app' + 'appActivity' capabilities)
           apk + activity + intent ('app' + 'appActivity' + 'appIntent' capabilities)

            */
    }
}
