﻿using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace 远程通信控制系统
{
    /// <summary>
    /// 参数配置工具类
    /// </summary>
    static class ConfigUtil
    {
        static private Config config;
        static private Configuration configuration;
        // TODO 将Config类映射为配置文件中的一个节点，使用工具类实现参数的读写，并同步至配置文件中

        static ConfigUtil()
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            // TODO 暂时写死，之后需要动态配置
            file.ExeConfigFilename = "远程通信控制系统.exe.config";
            configuration = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
        }

        static public Config getConfig()
        {
            if (config == null)
            {
                config = new Config();
                initConfig(config);
            }
            return config;
        }

        static private void initConfig(Config config)
        {
            PropertyInfo[] props = config.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (containsKey(prop.Name))
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(config, readConfigInt(prop.Name));
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        prop.SetValue(config, readConfigBool(prop.Name));
                    }
                    else
                    {
                        prop.SetValue(config, readConfigStr(prop.Name));
                    }
                }
                else
                {
                    addConfig(prop.Name, prop.GetValue(config));
                }

                // TODO 根据config类中的属性删除配置文件中多余的节点
            }
            configuration.Save();
        }

        static public void update(Config config)
        {
            PropertyInfo[] props = config.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (containsKey(prop.Name))
                {
                    writeConfig(prop.Name, prop.GetValue(config));
                }
                else
                {
                    addConfig(prop.Name, prop.GetValue(config));
                }
            }
            configuration.Save();
        }

        static private String readConfigStr(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        static private bool readConfigBool(string key)
        {
            try
            {
                return Boolean.Parse(ConfigurationManager.AppSettings[key]);
            }
            catch (Exception e)
            {
                LogUtil.GetLog().Write(e);
                return false;
            }

        }

        static private int readConfigInt(string key)
        {
            try
            {
                return int.Parse(ConfigurationManager.AppSettings[key]);
            }
            catch (Exception e)
            {
                LogUtil.GetLog().Write(e);
                return 0;
            }

        }

        #region Configuration操作相关方法

        /// <summary>
        /// 判断配置文件中是否有Config中的属性
        /// </summary>
        /// <param name="key"></param>
        static private bool containsKey(string key)
        {
            return configuration.AppSettings.Settings.AllKeys.Contains(key);
        }

        static private void addConfig(string key, object value)
        {
            value = value == null ? "" : value;
            configuration.AppSettings.Settings.Add(key, value.ToString());
        }

        static private void writeConfig(string key, object value)
        {
            value = value == null ? "" : value;
            LogUtil.GetLog().Write(key + ":" + value);
            configuration.AppSettings.Settings[key].Value = value.ToString();
        }

        #endregion
    }
}
