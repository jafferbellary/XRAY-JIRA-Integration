using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRAY_JIRA_Integration.Utility
{
    public class WriteToPropertyFile
    {
        public static StringBuilder results;
        public static Dictionary<string, string> dict = new Dictionary<string, string>();
        public static void setValueToPropertyFile(String key, String value)
        {
            FileStream file = new FileStream(@"C:\Users\jafferb\Desktop\TestProjects\PropertyFiles" + "\\executionResults,txt", FileMode.OpenOrCreate);

            //dict = new Dictionary<string, string>();
            dict.Add(key, value);
            //results = new StringBuilder();
            //results.Append(dict);

            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(dict);
            sw.Close();
            file.Close();

      //      Properties props = new Properties();
      //      props.load(in);
      //      OutputStream output = new FileOutputStream(
      //              System.getProperty("user.dir") + "\\executionResults.properties");
      //      props.setProperty(key, value);
      //      props.store(output, null);
      //      output.close();
		    //in.close();
        }
    }
}
