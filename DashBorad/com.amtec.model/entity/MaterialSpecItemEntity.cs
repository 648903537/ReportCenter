using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.amtec.action
{
    public class MaterialSpecItemEntity
    {
        public string PartNumber { get; set; }

        public int ErrorNumber { get; set; }

        //误差值
        public string ErrorValue { get; set; }

        public string BaseValue { get; set; }

        public string LowValue { get; set; }

        public string MaxValue { get; set; }

        public string Unit { get; set; }
    }
}
