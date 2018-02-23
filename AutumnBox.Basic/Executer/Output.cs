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
    using AutumnBox.Support.CstmDebug;
    using System;

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
        /// �����һ��OutputData���������
        /// </summary>
        /// <param name="output"></param>
        public void Append(Output output)
        {
            this.All += output.All;
            this.Out += output.Out;
            this.Error += output.Error;
        }
        /// <summary>
        /// ��ȡ�������������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return All.ToString();
        }
        public Output() {
            this.Out = "";
            this.Error = "";
            this.All = "";
        }
        public Output(string all, string _out, string err ="") {
            this.Out = _out;
            this.Error = err;
            this.All = all;
        }
        public void PrintOnLog(bool printOnRelease = false)
        {
            if (printOnRelease)
            {
                Logger.T($"PrintOnLog(): {ToString()}");
            }
            else
            {
                Logger.D($"PrintOnLog(): {ToString()}");
            }
        }
        public void PrintOnConsole()
        {
            Console.WriteLine($"PrintOnConsole(): {ToString()}");
        }
    }
}
