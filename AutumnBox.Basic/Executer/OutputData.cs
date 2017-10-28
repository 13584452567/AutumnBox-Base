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
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Basic.Util;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public sealed class OutputData
    {
        public IOutSender OutSender
        {
            set
            {
                if (_OutSender != null)
                {
                    throw new EventAddException("Only set one!");
                }
                else
                {
                    _OutSender = value;
                    _OutSender.ErrorDataReceived += (s, e) => { if (e != null) ErrorAdd(e.Data); };
                    _OutSender.OutputDataReceived += (s, e) => { if (e != null) OutAdd(e.Data); };
                }
            }
        }
        private IOutSender _OutSender = null;
        public List<string> LineAll { get; private set; } = new List<string>();
        public List<string> LineOut { get; private set; } = new List<string>();
        public List<string> LineError { get; private set; } = new List<string>();
        public StringBuilder All { get; private set; } = new StringBuilder();
        public StringBuilder Out { get; private set; } = new StringBuilder();
        public StringBuilder Error { get; private set; } = new StringBuilder();
        private bool _IsClosed = false;
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        /// <param name="outData"></param>
        public void OutAdd(string outData)
        {
            if (_IsClosed) return;
            All.AppendLine(outData);
            Out.AppendLine(outData);
            LineAll.Add(outData);
            LineOut.Add(outData);
        }
        /// <summary>
        /// ��Ӵ��������Ϣ
        /// </summary>
        /// <param name="errorData"></param>
        public void ErrorAdd(string errorData)
        {
            if (_IsClosed) return;
            All.AppendLine(errorData);
            LineAll.Add(errorData);
            LineError.Add(errorData);
            this.Error.AppendLine(errorData);
        }
        /// <summary>
        /// �����һ��OutputData���������
        /// </summary>
        /// <param name="output"></param>
        public void Append(OutputData output)
        {
            if (_IsClosed) return;
            LineAll.AddRange(output.LineAll);
            LineOut.AddRange(output.LineOut);
            LineError.AddRange(output.LineError);
            All.Append(output.ToString());
            Out.Append(output.Out.ToString());
            Error.Append(output.Error.ToString());
        }
        /// <summary>
        /// �������
        /// </summary>
        public void Clear()
        {
            Out.Clear();
            Error.Clear();
            All.Clear();
            LineAll.Clear();
            LineOut.Clear();
            LineError.Clear();
        }
        /// <summary>
        /// ֹͣ�������,ִ�к󽫲������������
        /// </summary>
        public void StopAdding()
        {
            _IsClosed = true;
        }
        /// <summary>
        /// ��ȡ�������������
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return All.ToString();
        }
    }
}
