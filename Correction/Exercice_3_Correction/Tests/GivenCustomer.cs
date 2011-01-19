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
            Given(
                new AddNewMediaCommand(Id("media1"), "SomeTitle1"),
                new RegisterNewCustomerCommand(Id("cust1"), "Uncle Joe"))
            .When(
                new CustomerRentMediaCommand(Id("cust1"), Id("media1")))
            .ThenExpect(
                new CustomerRentedMediaEvent(Id("media1"), Now().AddDays(7)));
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
            Given(
                new AddNewMediaCommand(Id("media1"), "SomeTitle1"),
                new AddNewMediaCommand(Id("media2"), "SomeTitle2"),
                new AddNewMediaCommand(Id("media3"), "SomeTitle3"),
                new AddNewMediaCommand(Id("media4"), "SomeTitle4"),
                new RegisterNewCustomerCommand(Id("cust1"), "Name"),
                new CustomerRentMediaCommand(Id("cust1"), Id("media1")),
                new CustomerRentMediaCommand(Id("cust1"), Id("media2")),
                new CustomerRentMediaCommand(Id("cust1"), Id("media3")))
            .When(
                new CustomerRentMediaCommand(Id("cust1"), Id("media4")))
            .ThenExpect(
                ErrorCode.MaximumOfMediaPermitedExceeded);
        }
    }
}
