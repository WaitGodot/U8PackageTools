using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U8PackageTools.code
{
    class Element
    {
        public Element(String key = "", String value = "")
        {
            this.key = key;
            this.value = value;
        }

        public String key;
        public String value;
    }
}
