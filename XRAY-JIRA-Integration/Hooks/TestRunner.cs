using Gherkin.Ast;
//using Io.Cucumber.Messages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using XRAY_JIRA_Integration.Utility;

namespace XRAY_JIRA_Integration.Hooks
{
    [Binding]
    public class TestRunner
    {
        static String autherizationToken;
        //private static string _featureName;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            autherizationToken = APIRequestResponse.postAuthenticationRequest().Replace('"', ' ').Trim();
            Console.WriteLine("AUTH TOKEN::: " + autherizationToken);
        }

        //[BeforeFeature]
        //public static void BeforeFeature(FeatureContext featureContext)
        //{
        //    _featureName = featureContext.FeatureInfo.Title;
        //}

        [AfterScenario]
        public static void AfterScenario(ScenarioContext scenario)
        {
            String featureName = FeatureContext.Current.FeatureInfo.Title;
            var scenarioTitle = scenario.ScenarioInfo.Title;
            var scenarioTags = scenario.ScenarioInfo.Tags;
            var scenariostatus = TestContext.CurrentContext.Result.Outcome.ToString();
            String[] array = featureName.Split('/');
            //String[] array2 = array[array.Length - 1].Split("\\.");
            String[] array2 = array[array.Length - 1].Split(new string[] { "\\." }, StringSplitOptions.RemoveEmptyEntries);
            featureName = array2[0];
            //String scenarioId = scenario.getSourceTagNames().toString().replace('@', ' ').replace('[', ' ').replace(']', ' ').trim();
            //String scenarioId = scenarioTags.ToString().Replace('@', ' ').Replace('[', ' ').Replace(']', ' ').Trim();
            //String scenarioStatus = scenario.getStatus().toString().toUpperCase();
            String scenarioId = scenarioTags[0].ToString();
            String scenarioStatus = TestContext.CurrentContext.Result.Outcome.ToString().ToUpper();
            Console.WriteLine("FEATURE NAME::: " + featureName);
            Console.WriteLine("SCENARIO ID::: " + scenarioId);
            Console.WriteLine("SCENARIO ID::: " + scenarioStatus);
            WriteToPropertyFile.setValueToPropertyFile(scenarioId, scenarioStatus);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            String descStatus = "";

            //FileInputStream in = new FileInputStream(
            //        System.getProperty("user.dir") + "\\executionResults.properties");
            //Properties props = new Properties();
            //props.load(in);
            var results = WriteToPropertyFile.dict;
            //Iterator<Object> itr = props.keySet().iterator();
            //while (results.Count!=0)
            foreach(var result in results)
            {
                String key = result.Key;
                String value = result.Value;
                descStatus = descStatus + '{' + "\"testKey\" : " + "\"" + key + "\",\"status\":\"" + value + "\"},";
            }
            Console.WriteLine("TESTCASES ARRAY::: " + descStatus);
            //FileStream out = new FileOutputStream(
            //        System.getProperty("user.dir") + "\\executionResults.properties");
            //props.clear();
            //props.store(out, null);
            descStatus = descStatus.Substring(0, descStatus.Length - 1);
            DateTime date = new DateTime();
            string time = date.TimeOfDay.ToString();
            string ts = DateTime.Now.ToString();
            String vsRequest = null;
            if (ConfigurationManager.AppSettings["update_existing_test_execution"].Equals("true"))
            {
                vsRequest = "{ \"testExecutionKey\": \"" + ConfigurationManager.AppSettings["existing_test_execution_id"] + "\",\"info\" : {\"summary\" : \"Test Execution Cucumber :" + ts + "\",\"description\" :\""
                        + "TestingPurpose" + "\"}, \"tests\" : [" + descStatus + "]}";
            }
            else
            {
                vsRequest = "{\"info\" : {\"summary\" : \"Test Execution Cucumber :" + ts + "\",\"description\" :\""
                        + "TestingPurpose" + "\"}, \"tests\" : [" + descStatus + "]}";
            }
            Console.WriteLine("HTTP BODY::: " + vsRequest);
            APIRequestResponse.postBodyRequest(vsRequest, autherizationToken);
        }
    }
}
