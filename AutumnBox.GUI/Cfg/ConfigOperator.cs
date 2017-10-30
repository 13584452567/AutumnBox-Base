/* =============================================================================*\
*
* Filename: ConfigJson.cs
* Description: 
*
* Version: 1.0
* Created: 9/30/2017 18:32:35(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Shared.CstmDebug;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace AutumnBox.GUI.Cfg
{
    [LogProperty(Show = false)]
    internal class ConfigOperator : IConfigOperator
    {
        public ConfigDataLayout Data { get; private set; } = new ConfigDataLayout();
        private static readonly string ConfigFileName = "autumnbox.json";
        /// <summary>
        /// ������
        /// </summary>
        public ConfigOperator()
        {
            Logger.D(this, "Start Check");
            if (HaveError() || HaveLost())
            {
                Logger.D(this, "Some error checked, init file");
                SaveToDisk();
            }
            Logger.D(this, "Finished Check");
            ReloadFromDisk();
        }
        /// <summary>
        /// ��Ӳ����������
        /// </summary>
        public void ReloadFromDisk()
        {
            if (HaveError()) SaveToDisk();
            if (!File.Exists(ConfigFileName)) { SaveToDisk(); return; }
            using (StreamReader sr = new StreamReader(ConfigFileName))
            {
                Data = (ConfigDataLayout)(JsonConvert.DeserializeObject(sr.ReadToEnd(), Data.GetType()));
            }
            Logger.D(this, "Is first launch? " + Data.IsFirstLaunch.ToString());
        }
        /// <summary>
        /// �����ݴ洢��Ӳ��
        /// </summary>
        public void SaveToDisk()
        {
            if (!File.Exists(ConfigFileName))
            {
                using (FileStream fs = new FileStream(ConfigFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)) { }
            }
            using (StreamWriter sw = new StreamWriter(ConfigFileName, false))
            {
                string text = JsonConvert.SerializeObject(Data);
                Logger.D(this, text);
                sw.Write(text);
                sw.Flush();
            }
        }

        /// <summary>
        /// ���Ӳ���ϵ������Ƿ�������
        /// </summary>
        /// <returns>�Ƿ�������</returns>
        private bool HaveError()
        {
            Logger.D(this, "enter error check");
            try
            {
                JObject jObj = JObject.Parse(File.ReadAllText(ConfigFileName)); return false;
            }
            catch (JsonReaderException) { return true; }
            catch (FileNotFoundException) { return true; }
        }
        /// <summary>
        /// ��������ļ��е����Ƿ��ж�ʧ
        /// </summary>
        /// <returns>���Ƿ��ж�ʧ</returns>
        private bool HaveLost()
        {
            Logger.D(this, "enter lost check");
            JObject j = JObject.Parse(File.ReadAllText(ConfigFileName));
            Logger.D(this, "read finish");
            foreach (var prop in Data.GetType().GetProperties())
            {
                if (!(prop.IsDefined(typeof(JsonPropertyAttribute)))) continue;
                var attr = (JsonPropertyAttribute)prop.GetCustomAttribute(typeof(JsonPropertyAttribute));
                if (j[attr.PropertyName] == null) { Logger.D(this, "have lost"); return true; };
            }
            Logger.D(this, "no lost");
            return false;
        }
    }
}
