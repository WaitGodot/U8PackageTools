using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U8PackageTools.code
{
    class ProjectConfig
    {
        private ProjectConfig()
        {
            U8Workspace = "";
        }
        private static ProjectConfig instance;
        public static ProjectConfig getInstance()
        {
            if (instance == null)
            {
                instance = new ProjectConfig();
            }
            return instance;
        }

        public void setU8Workspace(String workspace)
        {
            U8Workspace = workspace;
        }

        public String getU8Workspace()
        {
            return U8Workspace;
        }
        public String getGameListXML()
        {
            return U8Workspace + "\\games\\games.xml";
        }
        public String getGameList()
        {
            return U8Workspace + "\\games\\";
        }
        public String getSDKDirectory()
        {
            return U8Workspace + "\\config\\sdk";
        }

        //////////////////////////////////////////////////////////////////////////
        private String U8Workspace;
    }
}
