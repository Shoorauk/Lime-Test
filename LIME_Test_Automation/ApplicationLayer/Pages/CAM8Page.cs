using LIME_Test_Automation.Utility;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LIME_Test_Automation.ApplicationLayer.Pages
{
   public class CAM8Page :BasePage
    {
        public CAM8Page(IWebDriver driver):base(driver)
        {

        }

        public string randomValue= GenerateRandomValue();

        //locators
        By CAM8 = By.XPath("//a[normalize-space()='CAM-8']");
        By SearchAndManageTxt = By.XPath("//h1[normalize-space()='Search & Manage']");
        By AddBtn = By.XPath("//span[normalize-space()='Add']");
        By EnterMaterialtype = By.XPath("//input[@id='label']");
        By EnterDesc = By.XPath("//input[@id='description']");
        By ClickSaveBtn = By.XPath("//span[normalize-space()='Save']");
        By ClickHomePage = By.XPath("//a[normalize-space()='Home']");
        By HomeTitle = By.XPath("//h1[normalize-space()='Caminho.LIME']");
        By MaterialType = By.XPath("//h3[normalize-space()='Material Types']");



        public void NavigateToHomepage(string url)
        {
            GoToUrl(url);
        }

        public void ClickOnCam8()
        {
            ClickOnButton(CAM8);
        }

        public void ValidateSearchAndManagePageTxt(string pageHeader)
        {
            Assert.AreEqual(getText(SearchAndManageTxt), pageHeader);
        }

        public void ClickOnAddBtn()
        {
            ClickOnButton(AddBtn);
        }


        private static Random random = new Random();
        public static string GenerateRandomValue()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(characters, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void AddMaterialAndDesc(string material, string description)
        {
            string materialType = material + randomValue;
            SendText(EnterMaterialtype, materialType);
            SendText(EnterDesc, description);
            Thread.Sleep(100);
            
        }

        public void ClickOnSaveBtn()
        {
            ClickOnButton(ClickSaveBtn);
            //Thread.Sleep(50);
            //RefreshPage();
        }

        public void ClickHomePageLink()
        {
            ClickOnButton(ClickHomePage);
        }

        public void ValidateHomeTitle(string homeTitle)
        {
            Assert.AreEqual(getText(HomeTitle), homeTitle);
        }

        public void ValidateMaterialType(string materialType)
        {
            Assert.AreEqual(getText(MaterialType), materialType);
        }


    }
}
