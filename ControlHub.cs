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

        public void On_Alarm_Happen(AlarmInfo Alarm)
        {
          
        }

        public void On_Command_Error(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
            string NodeName = Node.Name;
            AlarmMessage Status = AlarmMapping.Get(Node.Name, Msg.Value);
            Startup.GlobalHub.InvokeOnAllAsync("On_Node_Connection_Changed", new { NodeName, Status });
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

        public void On_Message_Log(string Type, string Message)
        {
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
            NodeName = Task.TaskName.ToString();
            AlarmMessage Status = AlarmMapping.Get("SYSTEM", Message);
            Startup.GlobalHub.InvokeOnAllAsync("On_Node_Connection_Changed", new { NodeName, Status });
        }

        public void On_TaskJob_Ack(TaskFlowManagement.CurrentProcessTask Task)
        {
            string NodeName = Task.TaskName.ToString();
            string Status = "On_TaskJob_Ack";

            Startup.GlobalHub.InvokeOnAllAsync("On_Node_Connection_Changed", new { NodeName, Status });
        }

        public void On_TaskJob_Finished(TaskFlowManagement.CurrentProcessTask Task)
        {
            string NodeName = Task.TaskName.ToString();
            string Status = "On_TaskJob_Finished";
            Startup.GlobalHub.InvokeOnAllAsync("On_Node_Connection_Changed", new { NodeName, Status });
        }

       
    }
}
