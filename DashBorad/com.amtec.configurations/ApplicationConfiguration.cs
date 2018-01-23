using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.IO;
using com.amtec.action;

namespace com.amtec.configurations
{
    public class ApplicationConfiguration
    {

        public String StationNumber { get; set; }

        public String Client { get; set; }

        public String RegistrationType { get; set; }

        public String SerialPort { get; set; }

        public String BaudRate { get; set; }

        public String Parity { get; set; }

        public String StopBits { get; set; }

        public String DataBits { get; set; }

        public String NewLineSymbol { get; set; }

        public String High { get; set; }

        public String Low { get; set; }

        public String EndCommand { get; set; }

        public String IPAddress { get; set; }

        public String Port { get; set; }

        public String DBType { get; set; }

        public String LINE_DESCRIPTION { get; set; }

        public int RefreshTimeSpan { get; set; }

        public string AUTH_TEAM { get; set; }

        public string LockMat { get; set; }
        public string lblCaptionsize { get; set; }
        public int PageSizeCount { get; set; }

        public String PageNextTimer { get; set; }

        public String lblGridHeadersize { get; set; }

        public String lblGridsize { get; set; }

        public String ErrorFolder { get; set; }

        public String SuccessFolder { get; set; }

        public String GenerateFolder { get; set; }

        public String ListenFolder { get; set; }

        public String TemplateFolder { get; set; }

        public String MaxRow { get; set; }

        public Dictionary<string, string> STATUS_MAP = new Dictionary<string, string>();

        public ApplicationConfiguration()
        {
            string filePath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string _appDir = Path.GetDirectoryName(filePath);
            XDocument config = XDocument.Load(_appDir + @"\ApplicationConfig.xml");

            StationNumber = GetParameterValues(config, "REFERENCE_STATION_NO");
            Client = GetParameterValues(config, "Client");
            RegistrationType = GetParameterValues(config, "RegistrationType");
            AUTH_TEAM = GetParameterValues(config, "AUTH_TEAM");
            IPAddress = GetParameterValues(config, "IPAddress");
            Port = GetParameterValues(config, "Port");
            RefreshTimeSpan = GetIntValue(GetParameterValues(config, "REFRESH_RATE"));
            lblCaptionsize = GetParameterValues(config, "lblCaptionsize");
            PageSizeCount = GetIntValue(GetParameterValues(config, "PageSizeCount"));
            string strMapValues = GetParameterValues(config, "STATUS_MAP");
            string[] mapValues = strMapValues.Split(new char[] { ',' });
            foreach (var item in mapValues)
            {
                string[] strValues = item.Replace("{", "").Replace("}", "").Split(new char[] { ';' });
                if (strValues.Length == 2)
                {
                    if (!STATUS_MAP.ContainsKey(strValues[0]))
                        STATUS_MAP[strValues[0]] = strValues[1];
                }
            }
            LINE_DESCRIPTION = GetParameterValues(config, "LINE_DESCRIPTION");
            PageNextTimer = GetParameterValues(config, "PageNextTimer");
            lblGridHeadersize = GetParameterValues(config, "lblGridHeadersize");
            lblGridsize = GetParameterValues(config, "lblGridsize");

            //富士机文档转换
            ErrorFolder = GetParameterValues(config, "TransError");
            SuccessFolder = GetParameterValues(config, "TransOK");
            ListenFolder = GetParameterValues(config, "FileFolder");
            TemplateFolder = GetParameterValues(config, "TemplateFolder");
            GenerateFolder = GetParameterValues(config, "GenerateFolder");
            MaxRow = GetParameterValues(config, "MaxRow");
        }

        private string GetParameterValues(XDocument config, string parameterName)
        {
            string value = null;
            if (config.Descendants(parameterName).FirstOrDefault() == null)
            {
                value = "";
                LogHelper.Error("The parameter " + parameterName + " can't find in the configuration file");
            }
            else
            {
                value = config.Descendants(parameterName).FirstOrDefault().Value;
            }
            return value;
        }

        private int GetIntValue(string text)
        {
            int value = 0;
            if (string.IsNullOrEmpty(text))
            {

            }
            else
            {
                value = Convert.ToInt32(text);
            }
            return value;
        }
    }
}
