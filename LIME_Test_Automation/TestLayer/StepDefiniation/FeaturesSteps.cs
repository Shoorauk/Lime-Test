using LIME_Test_Automation.ApplicationLayer.Pages;
using LIME_Test_Automation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace LIME_Test_Automation.TestLayer.StepDefiniation
{
    [Binding]
    public sealed class FeaturesSteps
    {
        private FeaturesPage _featuresPage;
        public FeaturesSteps(WebDriver driver)
        {
            _featuresPage = new FeaturesPage(driver.Driver);
        }

        [When(@"Click on feature page link")]
        public void WhenClickOnFeaturePageLink()
        {
            _featuresPage.ClickOnFeatureLink();
        }

        [Then(@"""(.*)"" feature title should be displayed")]
        public void ThenFeatureTitleShouldBeDisplayed(string featureTitle)
        {
            _featuresPage.ValidateFeatureTitle(featureTitle);
        }

        [When(@"Click on reload feature button")]
        public void WhenClickOnReloadFeatureButton()
        {
            _featuresPage.ClickOnReleoadFeature();
        }


    }
}
