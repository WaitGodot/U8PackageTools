using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Xml;

namespace U8PackageTools.code
{
    class SDKManager
    {
        private static SDKManager instance;

        public static SDKManager getInstance()
        {
            if (instance == null)
            {
                instance = new SDKManager();
            }
            return instance;
        }

        private SDKManager()
        {

        }

        public class SDK
        {
            public SDK()
            {
                SDKName = "";
                ShowName = "";
                versionCode = "";
                versionName = "";
                Params = new Dictionary<String, Param>();
            }
            public String SDKName;
            public String ShowName;
            public String versionCode;
            public String versionName;
            public Dictionary<String, Param> Params;
        }

        public Dictionary<String, SDK> SDKList = new Dictionary<String, SDK>();

        public void readSDKDirectory()
        {
            String [] dirs = Directory.GetDirectories(ProjectConfig.getInstance().getSDKDirectory());
            SDKList.Clear();
            foreach (String path in dirs)
            {
                String xmlfile = path + "\\config.xml";
                if (File.Exists(xmlfile))
                {
                    SDK sdk = new SDK();
                    sdk.SDKName = path.Substring(path.LastIndexOf("\\") + 1);
                    SDKList.Add(sdk.SDKName, sdk);

                    XmlDocument xml = new XmlDocument();
                    xml.Load(xmlfile);
                    XmlNode root = xml.SelectSingleNode("config");
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        if (node.Name == "params")
                        {
                            foreach (XmlNode p in node.ChildNodes)
                            {
                                if (p.Attributes["name"] != null)
                                {
                                    Param pm = new Param(p.Attributes["name"].Value as String,
                                                            "",
                                                            p.Attributes["showName"].Value as String + "(" + p.Attributes["desc"].Value as String  + ")",
                                                            (p.Attributes["required"].Value as String) == "1");
                                    sdk.Params.Add(pm.name.value, pm);
                                }
                            }
                        }

                        if (node.Name == "version")
                        {
                            XmlNode n = node.SelectSingleNode("name");
                            sdk.ShowName = n.InnerText;
                            n = node.SelectSingleNode("versionCode");
                            sdk.versionCode = n.InnerText;
                            n = node.SelectSingleNode("versionName");
                            sdk.versionName = n.InnerText;
                        }
                    }
                    xml.Save(xmlfile);
                }
            }
        }
    }
}
