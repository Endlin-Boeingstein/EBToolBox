using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResSC
{
    //建立预置类
    public class Preset
    {
        public int version = 1;
        public int content_version = 1;
        public long slot_count = 0;
    }
}