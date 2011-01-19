using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commands;
using Domain;
using NUnit.Framework;
using Events;

namespace Tests
{
    [TestFixture]
    public class GivenCustomer : DomainTestCaseBase
    {
        [Test]
        public void WhenRegisteringANewCustomer_ThenNewCustomerRegistered()
        {
            Given()
            .When(
                new RegisterNewCustomerCommand(Id("cust1"), "Uncle Joe"))
            .ThenExpect(
                new NewCustomerRegisteredEvent("Uncle Joe"));
        }

        [Test]
        public void WhenRentingMedia_ThenMediaRentedByCustomer()
        {
            // TODO: remove .Fail() and implement
            Assert.Fail();

            // Hint: to get an assertable 'now' value, use Now()
        }

        [Test]
        public void WhenRentingSameMediaTwice_ThenAlreadyRentedError()
        {
            Given(
                new AddNewMediaCommand(Id("media1"), "SomeTitle1"),
                new RegisterNewCustomerCommand(Id("cust1"), "Name"),
                new CustomerRentMediaCommand(Id("cust1"), Id("media1")))
            .When(
                new CustomerRentMediaCommand(Id("cust1"), Id("media1")))
            .ThenExpect(
                ErrorCode.AlreadyRented);
        }

        [Test]
        public void WhenRentingMoreThan3Movies_ThenLimitExceededError()
        {
            // TODO: remove .Fail() and implement
            Assert.Fail();
        }
    }
}
