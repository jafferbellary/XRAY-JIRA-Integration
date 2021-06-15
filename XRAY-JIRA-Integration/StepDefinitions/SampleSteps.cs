using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace XRAY_JIRA_Integration.StepDefinitions
{
    [Binding]
    public class SampleSteps
    {
        int number1;
        int number2;
        int result;

        [Given(@"I have entered (.*) and (.*) into the calculator")]
        public void GivenIHaveEnteredAndIntoTheCalculator(int num1, int num2)
        {
            number1 = num1;
            number2 = num2;
        }
        
        [When(@"I do addition of two number")]
        public void WhenIDoAdditionOfTwoNumber()
        {
            result = number1 + number2;
        }
        
        [When(@"I do subtraction of two number")]
        public void WhenIDoSubtractionOfTwoNumber()
        {
            result = number1 - number2;
        }
        
        [When(@"I do multiply of two number")]
        public void WhenIDoMultiplyOfTwoNumber()
        {
            result = number1 * number2;
        }
        
        [When(@"I do divide of two number")]
        public void WhenIDoDivideOfTwoNumber()
        {
            result = number1 / number2;
        }
        
        [When(@"I do mod of two number")]
        public void WhenIDoModOfTwoNumber()
        {
            result = number1 % number2;
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expected)
        {
            Assert.IsTrue(result == expected);
        }
    }
}
