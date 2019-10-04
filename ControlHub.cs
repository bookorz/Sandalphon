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
            RouteControl control = new RouteControl(this);
            control.ConnectAll();
        }
        public void On_Alarm_Happen(string DIOName, string ErrorCode)
        {
          
        }

        public void On_Command_Error(Node Node, Transaction Txn, CommandReturnMessage Msg)
        {
 
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

        public void On_Data_Chnaged(string Parameter, string Value, string Type)
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
            
        }

        public void On_TaskJob_Ack(TaskFlowManagement.CurrentProcessTask Task)
        {
           
        }

        public void On_TaskJob_Finished(TaskFlowManagement.CurrentProcessTask Task)
        {
            
        }
    }
}
