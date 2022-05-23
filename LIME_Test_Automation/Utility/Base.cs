using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIME_Test_Automation.Utility
{

    public abstract class Base
    {
        private IWebDriver _driver;
        public WebDriverWait wait;

        public Base(IWebDriver driver)
        {
            _driver = driver;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public abstract string getPageTitle();

        public abstract string getText(By locator);

        public abstract IWebElement getElement(By locator);

        public abstract void waitForelementExist(By locator);
        public abstract void waitForelementVisible(By locator);

        public abstract void GoToUrl(string url);

        //protected TPage GetInstance<TPage>() where TPage : BasePage, new()
        //{
        //    return (TPage)Activator.CreateInstance(typeof(TPage));
        //}

        //public TPage As<TPage>() where TPage : BasePage
        //{
        //    return (TPage)this;
        //}


    }
}
