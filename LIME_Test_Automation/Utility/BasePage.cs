using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LIME_Test_Automation.Utility
{
    public class BasePage : Base
    {
        private IWebDriver _driver;
        public BasePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
        }
        public override IWebElement getElement(By locator)
        {
            IWebElement element = null;
            try
            {
                waitForelementExist(locator);
                element = _driver.FindElement(locator);
            }
            catch (Exception e)
            {
                Console.WriteLine("Locator is not found " + locator.ToString());
                Console.WriteLine(e.Message);

            }

            return element;
        }



        public override string getText(By locator)
        {


            waitForelementVisible(locator);
            string text = getElement(locator).Text;

            return text;
        }


        public String ReadValueFromTextBox(String text)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            string value = _driver.FindElement(By.XPath("//*[normalize-space(text())='" + text + "']")).Text;
            //*[normalize-space(text())='" + fieldName + "']

            return value;
        }


        public void ClearTextField(By locator)
        {
            waitForelementVisible(locator);
            _driver.FindElement(locator).Clear();
        }

        public override string getPageTitle()
        {
            return _driver.Title;
        }

        public override void GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();
        }

        public void OpenUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }



        public override void waitForelementExist(By locator)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);

            }
        }

        public override void waitForelementVisible(By locator)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);

            }
        }

        public void JavaScriptexecutorForClick(By locator)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", _driver.FindElement(locator));

        }

        public void SubmitBtn(By locator)
        {
            waitForelementVisible(locator);
            IWebElement element = _driver.FindElement(locator);
            element.Submit();
        }


        public void JavaScriptexecutorForSend(By locator, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].value=" + value + "", _driver.FindElement(locator));

        }
        public void ClickOnButton(By locator)
        {
            
            try
            {
                waitForelementVisible(locator);
                IWebElement element = _driver.FindElement(locator);
                Thread.Sleep(30);
                element.Click();
            }
            catch (Exception e)
            {

                try
                {

                    JavaScriptexecutorForClick(locator);
                }
                catch (Exception)
                {

                    try
                    {
                        ActionsClick(locator);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public void SendText(By locator, string value)
        {
            try
            {
                waitForelementVisible(locator);
                Thread.Sleep(5);
                IWebElement element = _driver.FindElement(locator);
                element.SendKeys(value);

            }
            catch (Exception e)
            {
                try
                {

                    JavaScriptexecutorForSend(locator, value);
                }
                catch (Exception)
                {

                    try
                    {
                        ActionsSendkey(locator, value);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

        }


        public void sendKeynew()
        {
            _driver.FindElement(By.ClassName("gg")).SendKeys("test");
        }

        public void ActionsSendkey(By locator, string value)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            IWebElement element = _driver.FindElement(locator);
            Actions actions = new Actions(_driver);
            actions.SendKeys(element, value).Build().Perform();
        }

        public void ActionsClick(By locator)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            IWebElement element = _driver.FindElement(locator);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element).Click().Perform();
        }


        public void SelectDropDownbyText(By locator, string text)
        {
            IWebElement element = _driver.FindElement(locator);
            SelectElement select = new SelectElement(element);
            select.SelectByText(text);

        }




        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public bool SelectFromSelectBox(WebElement wb, string VisibleText_or_Index_or_Value, string optionValue)
        {
            if (!optionValue.Equals(""))
            {
                SelectElement sel = new SelectElement(wb);
                wb.Click();
                Thread.Sleep(2000);
                if (VisibleText_or_Index_or_Value.Equals("VISIBLETEXT"))
                {
                    sel.SelectByText(optionValue);
                }
                else if (VisibleText_or_Index_or_Value.Equals("INDEX"))
                {
                    sel.SelectByIndex(int.Parse(optionValue));
                }
                else if (VisibleText_or_Index_or_Value.Equals("VALUE"))
                {
                    sel.SelectByValue(optionValue);
                }
            }

            return true;
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }
    }
}
