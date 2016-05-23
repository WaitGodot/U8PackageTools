using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace U8PackageTools.code
{
    class Param
    {
        public Param(String name, String value = "", String desc = "", bool require = true, bool readOnly = false)
        {
            this.name = new Element("name", name);
            this.value = new Element("value", value);
            this.desc = new Element("desc", desc);
            this.require = require;
            this.readOnly = readOnly;
        }

        public Param Clone()
        {
            return new Param(name.value, value.value, desc.value, require, readOnly);
        }

        public Element name;
        public Element value;
        public Element desc;
        public bool require;
        public bool readOnly;
    }
}
