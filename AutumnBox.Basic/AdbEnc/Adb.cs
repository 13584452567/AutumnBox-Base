﻿using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Other;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutumnBox.Basic.AdbEnc
{
    /// <summary>
    /// 封装Adb工具
    /// </summary>
    internal class Adb:Cmd,ITools, IAdbCommandExecuter
    {
        public Adb() :base(){
        }
        public new OutputData Execute(string command)
        {
            return base.Execute(Paths.ADB_TOOLS + " " + command);
        }
        public OutputData Execute(string id, string command) {
            return base.Execute(Paths.ADB_TOOLS + $" -s {id} " + command);
        }
        public DevicesHashtable GetDevices()
        {
            List<string> x = Execute(" devices").output;
            DevicesHashtable hs = new DevicesHashtable();
            for (int i = 1; i < x.Count - 2; i++)
            {
                hs.Add(x[i].Split('\t')[0], x[i].Split('\t')[1]);
                Debug.WriteLine($"{x[i].Split('\t')[0]}  -- {x[i].Split('\t')[1]}");
            }
            return hs;
        }
    }
}
