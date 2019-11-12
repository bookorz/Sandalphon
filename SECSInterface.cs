using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sandalphon.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using TransferControl.CommandConvert;
using TransferControl.Config;
using TransferControl.Engine;
using TransferControl.Management;

namespace sandalphon
{
    public class SECSInterface : IUserInterfaceReport, ICommMessage
    {
        IUserInterfaceReport _Report;
        private static readonly ILog logger = LogManager.GetLogger(typeof(SECSInterface));
        SocketServer Comm = null;
        Socket _handler = null;
        public SECSInterface(IUserInterfaceReport Report)
        {
            Comm = new SocketServer(this);
            _Report = Report;
        }

        public void On_Alarm_Happen(AlarmInfo Alarm)
        {
            string Event = "On_Alarm_Happend";
            Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Alarm }, new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii }) + Convert.ToChar(3));
            _Report.On_Alarm_Happen(Alarm);
        }

        public void On_Command_Error(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
            _Report.On_Command_Error(Node, Txn, Msg);
        }

        public void On_Command_Excuted(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
            string Event = "On_Command_Excuted";
            if (Comm != null)
            {
                Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Node, Txn, Msg }) + Convert.ToChar(3));
            }
            _Report.On_Command_Excuted(Node, Txn, Msg);
        }

        public void On_Command_Finished(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
            string Event = "On_Command_Finished";
            if (Comm != null)
            {
                Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Node, Txn, Msg }) + Convert.ToChar(3));
            }
            _Report.On_Command_Finished(Node, Txn, Msg);
        }

        public void On_Command_TimeOut(Node Node, Transaction Txn)
        {
            _Report.On_Command_TimeOut(Node, Txn);
        }

       

        public void On_Connection_Status_Report(string DIOName, string Status)
        {
            _Report.On_Connection_Status_Report(DIOName, Status);
        }

        public void On_DIO_Data_Chnaged(string Parameter, string Value, string Type)
        {
            _Report.On_DIO_Data_Chnaged(Parameter, Value, Type);
        }

        public void On_Event_Trigger(Node Node, CommandReturnMessage Msg)
        {
            string Event = "On_Event_Trigger";
            if (Comm != null)
            {
                Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Node, Msg }) + Convert.ToChar(3));
            }
            _Report.On_Event_Trigger(Node, Msg);
        }

        public void On_Job_Location_Changed(Job Job)
        {
            _Report.On_Job_Location_Changed(Job);
        }

        public void On_Message_Log(string Type, string Message)
        {
            _Report.On_Message_Log(Type, Message);
        }

        public void On_Node_Connection_Changed(string NodeName, string Status)
        {
            _Report.On_Node_Connection_Changed(NodeName, Status);
        }

        public void On_Node_State_Changed(Node Node, string Status)
        {
            _Report.On_Node_State_Changed(Node, Status);
        }

        public void On_Status_Changed(string Type, string Message)
        {
            _Report.On_Status_Changed(Type, Message);
        }

        public void On_TaskJob_Aborted(TaskFlowManagement.CurrentProcessTask Task, string NodeName, string ReportType, string Message)
        {
            string Event = "On_TaskJob_Aborted";
            if (Comm != null)
            {
                Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Task, NodeName, ReportType, Message }) + Convert.ToChar(3));
                AlarmMessage AlarmMessage = AlarmMapping.Get("SYSTEM", Message);
            }
            _Report.On_TaskJob_Aborted(Task, NodeName, ReportType, Message);
        }

        public void On_TaskJob_Ack(TaskFlowManagement.CurrentProcessTask Task)
        {
            string Event = "On_TaskJob_Ack";
            if (Comm != null)
            {
                Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Task })+ Convert.ToChar(3));
            }
            _Report.On_TaskJob_Ack(Task);
        }

        public void On_TaskJob_Finished(TaskFlowManagement.CurrentProcessTask Task)
        {
            string Event = "On_TaskJob_Finished";
            if (Comm != null)
            {
                Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, Task }) + Convert.ToChar(3));
            }
            _Report.On_TaskJob_Finished(Task);
        }
       

        public void On_Connection_Connected(Socket handler)
        {
            _handler = handler;
            string Event = "SystemConfig";
            SystemConfig SystemConfig = SystemConfig.Get();
            Comm.Send(_handler, JsonConvert.SerializeObject(new { Event, SystemConfig }) + Convert.ToChar(3));
        }

        public void On_Connection_Connecting()
        {
        }

        public void On_Connection_Disconnected()
        {
            //_handler = null;
        }

        public void On_Connection_Error(string DIOName, string ErrorMsg)
        {
            
        }

        public void On_Connection_Error(string Msg)
        {

        }

        public void On_Connection_Message(Socket handler, string content)
        {
            try
            {
                JObject restoredObject = JsonConvert.DeserializeObject<JObject>(content);
                //JObject可使用LINQ方式存取
                //var q = from p in restoredObject.Properties()
                //        where p.Name == "Name"
                //        select p;

                switch (restoredObject.Property("Event").Value.ToString())
                {
                    case "NewTask":
                        string Id = restoredObject.Property("Id").Value.ToString();
                        TaskFlowManagement.Command taskName = JsonConvert.DeserializeObject<TaskFlowManagement.Command>(restoredObject.Property("TaskName").Value.ToString());
                        Dictionary<string, string> param = JsonConvert.DeserializeObject<Dictionary<string, string>>(restoredObject.Property("param").Value.ToString());
                        TaskFlowManagement.Excute(Id, taskName, param);
                        break;
                    case "MessageLog":
                        string Type = restoredObject.Property("Type").Value.ToString();
                        string Message = restoredObject.Property("Message").Value.ToString();
                        logger.Info(Type+":"+Message);
                        _Report.On_Message_Log(Type, Message);
                        break;
                }
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                logger.Debug(content);
            }

        }

        public void NewTask(string Id, TaskFlowManagement.Command TaskName, Dictionary<string, string> param = null)
        {
          
        }
    }
}
