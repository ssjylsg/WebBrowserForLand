using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserForLand
{
    public class LandConfig
    {
        /// <summary>
        /// 单例模式singleton
        /// </summary>
        static LandConfig instance = null;
        public LandConfigInfo landConfigInfo = new LandConfigInfo();
        //默认保存路径
        public static string DefaultConfigXml = Application.StartupPath + Path.DirectorySeparatorChar + "LandConfig.xml";

        /// <summary>
        /// 获取配置信息（单例模式）
        /// </summary>
        /// <returns></returns>
        public static LandConfig GetInstance()
        {
            if (instance == null)
                instance = new LandConfig();
            return instance;
        }
        private LandConfig()
        {
            XmlStorageHelper xmler = new XmlStorageHelper();
            if (!File.Exists(DefaultConfigXml))
                return;
            xmler.LoadFromFile(landConfigInfo, DefaultConfigXml);
        }
        public void saveConfig()
        {
            XmlStorageHelper xmler = new XmlStorageHelper();
            xmler.SaveToFile(landConfigInfo, DefaultConfigXml);
        }
    }
    public class LandConfigInfo
    {
        public string MoneyTextID = "";
        public string MoneyPlusButtonID = "";
        public string MoneySubmitButtonID = "";
    }
}
