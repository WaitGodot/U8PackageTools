using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;

namespace U8PackageTools.code
{
    class GameManager
    {
        private static GameManager instance;
        public static GameManager getInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        private static String [,] RequireStr = {
                                            {"appName", "appName"},
                                            {"appID", "appID"},
                                            {"appKey", "appKey"},
                                            {"appPayPrivateKey", "appPayPrivateKey"},
                                            {"appDesc", "appDesc"},
                                            {"orientation", "横屏|竖屏(landscape|portrait),默认横屏"},
                                            {"outputApkName", "apk：{bundleID}:包名,{versionName}:版本名称,{versionCode}:版本号,{time}:时间戳(yyyyMMddmmss),{channelID}:渠道号,{channelName}:渠道名,{appName}:游戏名,{appID}:appID"},
                                        };
        private static String[,] OptionalStr = {
                                            {"cpuSupport", "armeabi|armeabi-v7a|x86|mips"},
                                            {"versionCode", "游戏包的版本号,对应AndroidManifest.xml中的versionCode"},
                                            {"versionName", "游戏包中的版本名称,对应AndroidManifest.xml中的versionName"},
                                            {"minSdkVersion", "最小支持的Android SDK版本，minSdkVersion,targetSdkVersion,maxSdkVersion 三个同时配置，或者同时不配置"},
                                            {"targetSdkVersion", "默认使用的Android SDK版本，minSdkVersion,targetSdkVersion,maxSdkVersion 三个同时配置，或者同时不配置"},
                                            {"maxSdkVersion", "最大支持的Android SDK版本，minSdkVersion,targetSdkVersion,maxSdkVersion 三个同时配置，或者同时不配置"},
                                        };
        private GameManager()
        {
           
        }
        
        public class Game
        {
            public Game()
            {
                 for (int i = 0; i < RequireStr.Length / 2; ++i)
                {
                    Params.Add(RequireStr[i, 0], new Param(RequireStr[i, 0], "", RequireStr[i, 1]));
                }

                for (int i = 0; i < OptionalStr.Length / 2; ++i)
                {
                    Params.Add(OptionalStr[i, 0], new Param(OptionalStr[i, 0], "", OptionalStr[i, 1], false));
                }
            }

            public Dictionary<String, Param> Params = new Dictionary<String, Param>();
            public Dictionary<String, Param> Logs = new Dictionary<String, Param>();
            public String AppName = "";
            public GameChannel GameChannel;
        }

        public ArrayList GameList = new ArrayList();
        //////////////////////////////////////////////////////////////////////////

        public void readGameXML(String xmlfile)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlfile);
            XmlNode root = xml.SelectSingleNode("xml");
            XmlNode games = root.SelectSingleNode("games");
            GameList.Clear();
            foreach (XmlNode game in games.ChildNodes)
            {
                Game g = new Game();
                foreach (XmlNode p in game.ChildNodes)
                {
                    if (p.Attributes["name"] != null)
                    {
                        Param pm = g.Params[p.Attributes["name"].Value as String];
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
                XmlNode log = game.SelectSingleNode("log");
                if (log != null)
                {
                    foreach (XmlNode l in log.ChildNodes)
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
                            g.Logs.Add(key, pm);
                        }
                    }
                }
                g.AppName = g.Params["appName"].value.value;
                if (g.AppName == "")
                {
                    continue;
                }

                g.GameChannel = new GameChannel();
                g.GameChannel.readGameConfig(ProjectConfig.getInstance().getGameList() + g.AppName + "\\config.xml");
                GameList.Add(g);
            }
            xml.Save(xmlfile);
        }

        public void saveGameXML(String xmlfile)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("xml");
            XmlElement games = xml.CreateElement("games");
            xml.AppendChild(root);
            root.AppendChild(games);

            foreach (Game g in GameList)
            {
                XmlElement game = xml.CreateElement("game");
                games.AppendChild(game);
                foreach (KeyValuePair<String, Param> entry in g.Params)
                {
                    if(entry.Value.value.value != "")
                    {
                        XmlElement e = xml.CreateElement("param");
                        e.SetAttribute(entry.Value.name.key, entry.Value.name.value);
                        e.SetAttribute(entry.Value.value.key, entry.Value.value.value);
                        e.SetAttribute(entry.Value.desc.key, entry.Value.desc.value);
                        game.AppendChild(e);
                    }
                }
                if (g.Logs.Count > 0)
                {
                    XmlElement log = xml.CreateElement("log");
                    game.AppendChild(log);
                    foreach (KeyValuePair<String, Param> entry in g.Logs)
                    {
                        if (entry.Value.value.value != "")
                        {
                            XmlElement e = xml.CreateElement("param");
                            e.SetAttribute(entry.Value.name.key, entry.Value.name.value);
                            e.SetAttribute(entry.Value.value.key, entry.Value.value.value);
                            e.SetAttribute(entry.Value.desc.key, entry.Value.desc.value);
                            log.AppendChild(e);
                        }
                    }
                }

                try
                {
                    String dest = g.Params["appName"].value.value;
                    if (g.AppName != dest && dest != "")
                    {
                        String source = g.AppName;
                        g.AppName = dest;
                        Directory.Move(ProjectConfig.getInstance().getGameList() + source, ProjectConfig.getInstance().getGameList() + dest);
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.InsertBefore(xmldecl, root);
            xml.Save(xmlfile);
        }
    }
}
