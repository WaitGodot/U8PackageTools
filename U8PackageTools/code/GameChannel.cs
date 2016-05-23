using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace U8PackageTools.code
{
    class GameChannel
    {
        public GameChannel()
        {

        }

        private static String[,,] ReadOnlyStr = {
                                            {{"name", "", "sdk名字"},},
                                            {{"sdk", "", "sdk简写"},},
                                            {{"desc", "", "描述"},},
                                        };
        private static String[,,] ReadWriteStr = {
                                            {{"id", "0", "渠道编号"},},
                                            {{"suffix", "", "apk包，可以用全称或者后缀"},},
                                            {{"icon", "",  "角标: 默认为空，右下：rb,左下:lb,左上:lt,右上:rt"},},
                                            {{"splash", "0", "闪屏默认为0，目前不可配置"},},
                                            {{"splash_copy_to_unity", "0", "unity闪屏默认为0，目前不可配置"},},
                                        };
        
        public class Channel
        {
            public Channel()
            {
                for (int i = 0; i < ReadOnlyStr.Length / 3; ++i)
                {
                    Params.Add(ReadOnlyStr[i, 0, 0], new Param(ReadOnlyStr[i, 0, 0], ReadOnlyStr[i, 0, 1], ReadOnlyStr[i, 0, 2], true, true));
                }

                for (int i = 0; i < ReadWriteStr.Length / 3; ++i)
                {
                    Params.Add(ReadWriteStr[i, 0, 0], new Param(ReadWriteStr[i, 0, 0], ReadWriteStr[i, 0, 1], ReadWriteStr[i, 0, 2]));
                }
            }

            public Dictionary<String, Param> Params = new Dictionary<String, Param>();
            public Dictionary<String, Param> SDKParams = new Dictionary<String, Param>();
            public SDKManager.SDK sdk;
        }

        public ArrayList ChannelList = new ArrayList();

        public Channel addChannlWithSDK(SDKManager.SDK sdk)
        {
            Channel c = new Channel();
            foreach (KeyValuePair<String, Param> key in sdk.Params )
            {
                Param p = key.Value;
                if (p.require == true)
                {
                    c.SDKParams.Add(p.name.value, p.Clone());
                }
            }
            c.Params["name"].value.value = sdk.SDKName;
            c.Params["sdk"].value.value = sdk.SDKName;
            c.Params["desc"].value.value = sdk.ShowName;
            c.Params["suffix"].value.value = "." + sdk.SDKName;
            c.sdk = sdk;
            ChannelList.Add(c);

            return c;
        }

        public void readGameConfig(String xmlfile)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfile);
            XmlNode root = xml.SelectSingleNode("xml");
            XmlNode games = root.SelectSingleNode("channels");
            ChannelList.Clear();

            foreach (XmlNode game in games.ChildNodes)
            {
                Channel c = new Channel();
                foreach (XmlNode p in game.ChildNodes)
                {
                    if (p.Name == "param")
                    {
                        if (p.Attributes["name"] != null)
                        {
                            Param pm = c.Params[p.Attributes["name"].Value as String];
                            if (pm != null)
                            {
                                pm.value.value = p.Attributes["value"].Value as String;
                            }
                            else
                            {
                                Console.WriteLine(p.Attributes["name"].Value as String);
                            }
                        }
                    }
                }
                XmlNode sdkparams = game.SelectSingleNode("sdk-params");
                if (sdkparams != null)
                {
                    foreach (XmlNode l in sdkparams.ChildNodes)
                    {
                        if (l.Attributes["name"] != null)
                        {
                            String key = l.Attributes["name"].Value as String;
                            String value = "";
                            String desc = "";
                            if (l.Attributes["value"] != null)
                            {
                                value = l.Attributes["value"].Value as String;
                            }
                            if (l.Attributes["desc"] != null)
                            {
                                desc = l.Attributes["desc"].Value as String;
                            }

                            Param pm = new Param(key, value, desc);
                            c.SDKParams.Add(key, pm);
                        }
                    }
                }
                c.sdk = SDKManager.getInstance().SDKList[c.Params["sdk"].value.value];
                if (c.sdk != null)
                {
                    foreach (KeyValuePair<String, Param> key in c.sdk.Params)
                    {
                        Param p = key.Value;
                        if (p.require == true)
                        {
                            if (! c.SDKParams.ContainsKey(p.name.value))
                            {
                                c.SDKParams.Add(p.name.value, p.Clone());
                            }
                            else
                            {
                                c.SDKParams[p.name.value].desc.value = key.Value.desc.value;
                            }
                        }
                    }
                }
                ChannelList.Add(c);
            }
            xml.Save(xmlfile);
        }

        public void writeGameConfig(String xmlfile)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("xml");
            XmlElement games = xml.CreateElement("channels");
            
            xml.AppendChild(root);
            root.AppendChild(games);

            foreach (Channel g in ChannelList)
            {
                XmlElement c = xml.CreateElement("channel");
                games.AppendChild(c);
                foreach (KeyValuePair<String, Param> entry in g.Params)
                {
                    if (!entry.Value.value.value.Equals(""))
                    {
                        XmlElement e = xml.CreateElement("param");
                        e.SetAttribute(entry.Value.name.key, entry.Value.name.value);
                        e.SetAttribute(entry.Value.value.key, entry.Value.value.value);
                        //e.SetAttribute(entry.Value.desc.key, entry.Value.desc.value);
                        c.AppendChild(e);
                    }
                }
                if (g.SDKParams.Count > 0)
                {
                    XmlElement sdkp = xml.CreateElement("sdk-params");
                    c.AppendChild(sdkp);
                    foreach (KeyValuePair<String, Param> entry in g.SDKParams)
                    {
                        XmlElement e = xml.CreateElement("param");
                        e.SetAttribute(entry.Value.name.key, entry.Value.name.value);
                        e.SetAttribute(entry.Value.value.key, entry.Value.value.value);
                        e.SetAttribute(entry.Value.desc.key, entry.Value.desc.value);
                        sdkp.AppendChild(e);
                    }
                }
                if (g.sdk != null)
                {
                    XmlElement sdkv = xml.CreateElement("sdk-version");
                    c.AppendChild(sdkv);

                    XmlElement versionCode = xml.CreateElement("versionCode");
                    versionCode.InnerText = g.sdk.versionCode;
                    sdkv.AppendChild(versionCode);

                    XmlElement versionName = xml.CreateElement("versionName");
                    versionName.InnerText = g.sdk.versionName;
                    sdkv.AppendChild(versionName);
                }
            }

            XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.InsertBefore(xmldecl, root);
            xml.Save(xmlfile);
        }
    }
}
