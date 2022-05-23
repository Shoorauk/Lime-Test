using LIME_Test_Automation.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace LIME_Test_Automation.ApplicationLayer.Pages
{
    public class FeaturesPage : BasePage
    {
        public FeaturesPage(IWebDriver driver) :base(driver)
        {

        }

        //Locators

        By FeaturesLink = By.XPath("//a[normalize-space()='Features']");
        By FeaturesTitle = By.XPath("//h3[normalize-space()='Features']");
        By ReloadFeature = By.XPath("//span[@class='k-button-text']");

        public void ClickOnFeatureLink()
        {
            ClickOnButton(FeaturesLink);
        }

        public void ValidateFeatureTitle(string featureTitle)
        {
            Assert.AreEqual(getText(FeaturesTitle), featureTitle);
        }

        public void ClickOnReleoadFeature()
        {
            ClickOnButton(ReloadFeature);
        }

    }
}
