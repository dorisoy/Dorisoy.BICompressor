using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dorisoy.BICompressor
{
    internal class AppConfig
    {
        //静态构造,不能实例化
        static AppConfig() { }


        public static int GetOnceOfQuantity()
        {
            int onceOfQuantity = 0;
            int.TryParse(GetAppSettingsKeyValue("OnceOfQuantity"),out onceOfQuantity);
            return onceOfQuantity;
        }


        public static string GetFileOutPutPath()
        {
            return GetAppSettingsKeyValue("OutPutPath");
        }


        public static int GetFileLimitSize()
        {
            int fileLimitSize = 1024;
            int.TryParse(GetAppSettingsKeyValue("FileLimitSize"), out fileLimitSize);
            return fileLimitSize;
        }



        public static string[] GetFileExtensionNames()
        {
            if (null==FileExtensionNames)
            {
                string Extensions = GetAppSettingsKeyValue("FileExtensionNames");
                FileExtensionNames= Extensions.Split(',');
            }
            return FileExtensionNames;
        }
        private static string[] FileExtensionNames;

        /// <summary>
        /// 获取AppSettings配置节中的Key值
        /// </summary>
        /// <param name="keyName">Key's name</param>
        /// <returns>Key's value</returns>
        public static string GetAppSettingsKeyValue(string keyName)
        {
            return ConfigurationManager.AppSettings.Get(keyName);
        }
        /// <summary>
        /// 获取ConnectionStrings配置节中的值
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionStringsElementValue()
        {
            ConnectionStringSettings settings = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"];
            return settings.ConnectionString;
        }
        /// <summary>
        /// 保存节点中ConnectionStrings的子节点配置项的值
        /// </summary>
        /// <param name="ConnectionStringsName"></param>
        /// <param name="elementValue"></param>
        public static void ConnectionStringsSave(string ConnectionStringsName, string elementValue)
        {
            System.Configuration.Configuration config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["connectionString"].ConnectionString = elementValue;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
        /// <summary>
        /// 判断appSettings中是否有此项
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static bool AppSettingsKeyExists(string strKey, Configuration config)
        {
            foreach (string str in config.AppSettings.Settings.AllKeys)
            {
                if (str == strKey)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 保存appSettings中某key的value值
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="newValue"></param>
        public static void AppSettingsSave(string strKey, string newValue)
        {
            System.Configuration.Configuration config =
             ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (AppSettingsKeyExists(strKey, config))
            {
                config.AppSettings.Settings[strKey].Value = newValue;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

    }
}
