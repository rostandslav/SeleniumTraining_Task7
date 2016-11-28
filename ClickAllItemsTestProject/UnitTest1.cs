using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections;
using System.Collections.Generic;



namespace ClickAllItemsTestProject
{
    [TestClass]
    public class ClickAllItemsTestClass
    {
        private IWebDriver driver;
        private WebDriverWait wait;


        [TestInitialize]
        public void Init()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }


        [TestMethod]
        public void ClickAllMenuItemsTest()
        {
            driver.Url = "http://litecart/admin/";

            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-apps-menu")));

            int mainItemsCount = driver.FindElements(By.CssSelector("#box-apps-menu > li")).Count;

            for (int i = 0; i < mainItemsCount; i++)
            {
                var mainItems = driver.FindElements(By.CssSelector("#box-apps-menu > li"));
                var currentMainItem = mainItems[i];
                currentMainItem.Click();

                Assert.IsTrue(driver.FindElements(By.CssSelector("td#content > h1")).Count > 0);

                int subItemsCount = driver.FindElements(By.CssSelector("#box-apps-menu [class=docs] > li")).Count;

                for (int j = 0; j < subItemsCount; j++)
                {
                    var subItems = driver.FindElements(By.CssSelector("#box-apps-menu [class=docs] > li"));
                    var currentSubItem = subItems[j];
                    currentSubItem.Click();

                    Assert.IsTrue(driver.FindElements(By.CssSelector("td#content > h1")).Count > 0);
                }
            }
        }


        [TestCleanup]
        public void Finish()
        {
            driver.Quit();
            //driver = null;
        }
    }
}
