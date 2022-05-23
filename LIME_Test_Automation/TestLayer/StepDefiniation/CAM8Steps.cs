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
    public sealed class CAM8Steps
    {
        private CAM8Page _cam8Page;
        public CAM8Steps(WebDriver driver)
        {

            _cam8Page = new CAM8Page(driver.Driver);
        }

        [Given(@"Navigate to Lime Home Page")]
        public void GivenNavigateToLimeHomePage()
        {
            _cam8Page.NavigateToHomepage(Hookinitialization.configSetting.BaseUrl);
        }

        [When(@"the user clicked on CAM(.*)")]
        public void WhenTheUserClickedOnCAM(int p0)
        {
            _cam8Page.ClickOnCam8();
        }



        [Then(@"""(.*)"" page should be displayed")]
        public void ThenPageShouldBeDisplayed(string pageHeader)
        {
            _cam8Page.ValidateSearchAndManagePageTxt(pageHeader);
        }

        [When(@"the user clicks on Add button")]
        public void WhenTheUserClicksOnAddButton()
        {
            _cam8Page.ClickOnAddBtn();
        }


        [When(@"enter the (.*) and (.*)")]
        public void WhenEnterTheAnd(string material, string description)
        {
            _cam8Page.AddMaterialAndDesc(material, description);
        }

        [When(@"Click on ""(.*)""")]
        public void WhenClickOn(string p0)
        {
            _cam8Page.ClickOnSaveBtn();
        }

        [Then(@"the new Material Type is listed in the Search & Manage Page")]
        public void ThenTheNewMaterialTypeIsListedInTheSearchManagePage()
        {
            Console.WriteLine("test");
        }

        [When(@"Click on home page link")]
        public void WhenClickOnHomePageLink()
        {
            _cam8Page.ClickHomePageLink();
        }

        [Then(@"""(.*)"" title should be displayed")]
        public void ThenTitleShouldBeDisplayed(string homeTitle)
        {
            _cam8Page.ValidateHomeTitle(homeTitle);
        }

        [Then(@"""(.*)"" Material type text should be displayed")]
        public void ThenMaterialTypeTextShouldBeDisplayed(string materialType)
        {
           _cam8Page.ValidateMaterialType(materialType);
        }



    }
}
