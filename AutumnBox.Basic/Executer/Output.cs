/* =============================================================================*\
*
* Filename: OutputData.cs
* Description: 
*
* Version: 1.0
* Created: 9/15/2017 16:01:48(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Executer
{
    using AutumnBox.Basic.Util;
    using AutumnBox.Support.Log;
    using System;
    /// <summary>
    /// �����װ��
    /// </summary>
    public class Output : IPrintable
    {
        /// <summary>
        /// ���е����
        /// </summary>
        public string[] LineAll
        {
            get
            {
                return All.Split(Environment.NewLine.ToCharArray());
            }
        }

        /// <summary>
        /// ���еı�׼���
        /// </summary>
        public string[] LineOut
        {
            get
            {
                return Out.Split(Environment.NewLine.ToCharArray());
            }
        }

        /// <summary>
        /// ���еı�׼����
        /// </summary>
        public string[] LineError
        {
            get
            {
                return Error.Split(Environment.NewLine.ToCharArray());
            }
        }

        /// <summary>
        /// ���е����
        /// </summary>
        public string All { get; protected set; }

        /// <summary>
        /// ���еı�׼���
        /// </summary>
        public string Out { get; protected set; }
        /// <summary>
        /// ���еı�׼����
        /// </summary>
        public string Error { get; protected set; }

        /// <summary>
        /// �ж�������Ƿ����ĳ���ַ���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="ignoreCase">�Ƿ���Դ�Сд</param>
        /// <returns></returns>
        public bool Contains(string str, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                return All.ToLower().Contains(str.ToLower());
            }
            else
            {
                return All.Contains(str);
            }
        }

        /// <summary>
        /// ��ȡ�������������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return All.ToString();
        }

        /// <summary>
        /// ����һ���յ�Output����
        /// </summary>
        public Output()
        {
            this.Out = "";
            this.Error = "";
            this.All = "";
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="all">��������</param>
        /// <param name="stdOutput">��׼���</param>
        /// <param name="stdError">��׼����</param>
        public Output(string all, string stdOutput, string stdError = "")
        {
            this.Out = stdOutput;
            this.Error = stdError;
            this.All = all;
        }

        /// <summary>
        /// �Կɶ����tag������,����log
        /// </summary>
        /// <param name="tagOrSender"></param>
        /// <param name="printOnRelease"></param>
        public void PrintOnLog(object tagOrSender, bool printOnRelease = false)
        {
            if (printOnRelease)
            {
                Logger.Info(tagOrSender, $"{this.GetType().Name}.PrintOnLog():{Environment.NewLine}{ToString()}");
            }
            else
            {
                Logger.Debug(tagOrSender, $"{this.GetType().Name}.PrintOnLog():{Environment.NewLine}{ToString()}");
            }
        }

        /// <summary>
        /// �Կɶ����tag������,��ӡ�ڿ���̨
        /// </summary>
        /// <param name="tagOrSender"></param>
        public void PrintOnConsole(object tagOrSender)
        {
            Console.WriteLine($"{tagOrSender} {this.GetType().Name}.PrintOnConsole():{Environment.NewLine}{ToString()}");
        }
    }
}
