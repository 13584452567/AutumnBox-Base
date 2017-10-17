/* =============================================================================*\
*
* Filename: SystemHelper.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 05:04:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Helper
{
    /// <summary>
    /// ϵͳ������,��ϵͳ��صľ�̬����
    /// </summary>
    public static class SystemHelper
    {
        /// <summary>
        /// ɱ��һ������
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess(string processName)
        {
            var list = Process.GetProcessesByName(processName);
            foreach (Process p in list)
            {
                p.Kill();
            }
        }
        /// <summary>
        /// �ж��Ƿ���win10ϵͳ
        /// </summary>
        public static bool IsWin10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
        /// <summary>
        /// ��ȡָ�����ָ������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static object[] GetAttributeFromObj<T>(object from) where T : Attribute
        {
            object[] objAttrs = from.GetType().GetCustomAttributes(typeof(T), true);
            return objAttrs;
        }
        public static void AppExit(int exitCode = 0)
        {
            Logger.T("SystemHelper", "Exiting.....");
            App.DevicesListener.Stop();
            CommandExecuter.Kill();
            Environment.Exit(exitCode);
        }
        #region �ڴ���� http://www.cnblogs.com/xcsn/p/4678322.html
        static bool continueAutoGC = true;
        /// <summary>
        /// ��ʼ�Զ�GC
        /// </summary>
        public static void StartAutoGC()
        {
            continueAutoGC = true;
            new Thread(AutoGC) { Name = "Auto GC" }.Start();
        }
        /// <summary>
        /// �����Զ�GC
        /// </summary>
        public static void StopAutoGC()
        {
            continueAutoGC = false;
        }
        /// <summary>
        /// ����ѭ�����ڴ���շ���
        /// </summary>
        private static void AutoGC()
        {
            while (continueAutoGC)
            {
                ClearMemory();
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// �ͷ��ڴ�
        /// </summary>
        private static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
    }
}
