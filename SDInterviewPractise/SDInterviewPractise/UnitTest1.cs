using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace SDInterviewPractise
{
    [TestFixture]
    public class UnitTest1
    {
        IWebDriver _driver;

        [SetUp]
        public void TestSetUp()
        {
            _driver = new FirefoxDriver();
            _driver.Url = "https://admin.brandview.com";
            _driver.FindElement(By.Id("login--username")).SendKeys("aditya.arisetty");
            _driver.FindElement(By.Id("login--password")).SendKeys("arisetty29");
            _driver.FindElement(By.Id("login--log-in")).Click();

            _driver.Manage().Window.Maximize();
            
            //Check user s logged in
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2000));
            var empName = _driver.FindElement(By.XPath(".//*[@id='bv--user-name']/var"));
            string getName = empName.Text;
            Assert.AreEqual("Aditya Arisetty", getName);
        
            //Go to User & Customer page
            _driver.FindElement(By.LinkText("Admin")).Click();
            Assert.AreEqual("Brand View - Analyse the retail market, instantly.", _driver.Title);
            _driver.FindElement(By.LinkText("Users and Customers")).Click();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2000));
            //var _text = _driver.FindElement(By.ClassName("pageTitle"));
            //Assert.AreEqual("Customers", _text.Text);
        }

        [Test]
        public void Analyse()
        {
            _driver.FindElement(By.Id("txtSearch")).SendKeys("internal testing");
            _driver.FindElement(By.XPath(".//*[@id='divList']/div[1]/div/a[1]/span/span")).Click();

            _driver.FindElement(By.XPath(".//*[@id='ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_rgCustomers_ctl00__0']/td[2]")).Click();

            Thread.Sleep(3000);
            //Check Edit Internal Testing page is opened
            var _editTitle = _driver.FindElement(By.Id("divEditTitle"));
            Assert.AreEqual("Edit Internal Testing", _editTitle.Text);

            //Go to Manage Users tab
            _driver.FindElement(By.LinkText("Manage Users")).Click();

            //Enter Christy in the search box
            _driver.FindElement(By.XPath(".//*[@id='ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_ggUsersGrid_txtSimpleSearch']")).SendKeys("Christy");
            _driver.FindElement(By.XPath(".//*[@id='ggSearchButton']/span/span")).Click();

            _driver.FindElement(By.PartialLinkText("christy")).Click();

            Thread.Sleep(2000);

            _driver.FindElement(By.CssSelector("html.js.flexbox.canvas.canvastext.webgl.no-touch.geolocation.postmessage.no-websqldatabase.indexeddb.hashchange.history.draganddrop.websockets.rgba.hsla.multiplebgs.backgroundsize.borderimage.borderradius.boxshadow.textshadow.opacity.cssanimations.csscolumns.cssgradients.no-cssreflections.csstransforms.csstransforms3d.csstransitions.fontface.generatedcontent.video.audio.localstorage.sessionstorage.webworkers.applicationcache.svg.inlinesvg.smil.svgclippaths body.body div.l-main--wrapper div.l-main--content form#aspnetForm.aspnetForm div.pageContainer.group div#ctl00_ctl00_ctl00_ContentPlaceHolder1_divContent.pageContent div.fullWidthContentBody div#divEditCustomer div#divBottomRow div#ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_rmpMain.bvPanel div#ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_pvUsers div#divBottomRightContent div#divEditUserUnderManageUsers div#ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_rmpManageUsers.bvPanel div#ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_pv_Users_EditUser div fieldset.validationGroup div.center_edit_item div#divUserButtons.topborder.group div#divAssumeButton a#ctl00_ctl00_ctl00_ContentPlaceHolder1_Content_Content_switchUser.button.b-yellow span.b-caption.b-static_90 span")).Click();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2000));

            _driver.FindElement(By.LinkText("comp [testing share]")).Click();
            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2000));

            //Click Analyze button
            _driver.FindElement(By.XPath("html/body/div[2]/div[2]/div/div[1]/div[5]/div/div[5]/button")).Click();

            Thread.Sleep(2000);

            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30000));
            
            //Click on 100%
            _driver.FindElement(By.XPath("html/body/div[2]/div[2]/div/div[2]/div/div[3]/div[2]/div/div[2]/div[3]/div/span/div/div/div[1]/div/a")).Click();

            Thread.Sleep(2000);

            Assert.AreEqual("true",_driver.PageSource.Contains("Compliance Evidence"));


        }




        [TearDown]
        public void Logout()
        {
            _driver.FindElement(By.XPath(".//*[@id='bv--header-links-wrapper']/nav/ul/li[3]/a")).Click();
            string title = _driver.Title;
            Assert.AreEqual("Brand View - Log in", title);
            _driver.Close();
        }
    }
}
