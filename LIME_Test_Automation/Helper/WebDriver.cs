using LIME_Test_Automation.Utility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIME_Test_Automation.Helper
{
    public class WebDriver
    {
        public IWebDriver Driver { get; set; }

        public BasePage CurrentPage { get; set; }
    }
}
