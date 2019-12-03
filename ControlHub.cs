using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferControl.CommandConvert;
using TransferControl.Engine;
using TransferControl.Management;

namespace sandalphon
{
    public class ControlHub : IUserInterfaceReport
    {
        public ControlHub()
        {
            SECSInterface SECS = new SECSInterface(this);
            RouteControl control = new RouteControl(SECS);
            
            control.ConnectAll();
        }

        public void NewTask(string Id, TaskFlowManagement.Command TaskName, Dictionary<string, string> param = null)
        {
          
        }

        public void On_Alarm_Happen(TransferControl.Management.AlarmManagement.AlarmInfo Alarm)
        {
      
            Startup.GlobalHub.InvokeOnAllAsync("On_Alarm_Happen", new { Alarm });
        }

        public void On_Command_Error(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
            //string NodeName = Node.Name;
            //AlarmMessage Status = AlarmMapping.Get(Node.Name, Msg.Value);
            //Startup.GlobalHub.InvokeOnAllAsync("On_Node_Connection_Changed", new { NodeName, Status });
        }

        public void On_Command_Excuted(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
    
        }

        public void On_Command_Finished(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
    
        }

        public void On_Command_TimeOut(Node Node, Transaction Txn)
        {

        }

        public void On_Connection_Error(string DIOName, string ErrorMsg)
        {
           
        }

        public void On_Connection_Status_Report(string DIOName, string Status)
        {
        }

        public void On_DIO_Data_Chnaged(string Parameter, string Value, string Type)
        {
      
        }

        public void On_Event_Trigger(Node Node, CommandReturnMessage Msg)
        {
       
        }

        public void On_Job_Location_Changed(Job Job)
        {
            
        }
        public class LogInfo
        {
            public string Type { get; set; }
            public string Message { get; set; }
        }
        public static List<LogInfo> SECS_Log = new List<LogInfo>();
        public static List<LogInfo> CMD_Log = new List<LogInfo>();
        public void On_Message_Log(string Type, string Message)
        {
            Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+ " " + Message;
            if (Type.Equals("SECS"))
            {
                lock (SECS_Log)
                {
                    LogInfo tmp = new LogInfo();
                    tmp.Type = Type;
                    tmp.Message = Message;
                    SECS_Log.Add(tmp);
                    if (SECS_Log.Count > 6000)
                    {
                        SECS_Log.RemoveAt(0);
                    }
                }
            }
            else if (Type.Equals("CMD"))
            {
                lock (CMD_Log)
                {
                    LogInfo tmp = new LogInfo();
                    tmp.Type = Type;
                    tmp.Message = Message;
                    CMD_Log.Add(tmp);
                    if (CMD_Log.Count > 6000)
                    {
                        CMD_Log.RemoveAt(0);
                    }
                }
            }
            Startup.GlobalHub.InvokeOnAllAsync("On_Message_Log", new { Type, Message });

        }

        public void On_Node_Connection_Changed(string NodeName, string Status)
        {
            Startup.GlobalHub.InvokeOnAllAsync("On_Node_Connection_Changed",  new { NodeName, Status } );
        }

        public void On_Node_State_Changed(Node Node, string Status)
        {
           
        }

        public void On_Status_Changed(string Type, string Message)
        {
            
        }

        public void On_TaskJob_Aborted(TaskFlowManagement.CurrentProcessTask Task, string NodeName, string ReportType, string Message)
        {
            
            Startup.GlobalHub.InvokeOnAllAsync("On_TaskJob_Aborted", new { Task });
        }

        public void On_TaskJob_Ack(TaskFlowManagement.CurrentProcessTask Task)
        {


            Startup.GlobalHub.InvokeOnAllAsync("On_TaskJob_Ack", new { Task });
        }

        public void On_TaskJob_Finished(TaskFlowManagement.CurrentProcessTask Task)
        {
            Startup.GlobalHub.InvokeOnAllAsync("On_TaskJob_Finished", new { Task });
        }

        public Node GetNode(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
