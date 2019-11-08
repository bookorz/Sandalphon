using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransferControl.Management;

namespace sandalphon.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        public class TaskRequest
        {
            public string Name { get; set; }
            public string Target { get; set; }
        }
        [HttpPost("[action]")]
        public IActionResult TaskRun([FromBody]TaskRequest req)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@Target", req.Target);
            TaskFlowManagement.Excute(Guid.NewGuid().ToString(), (TaskFlowManagement.Command)Enum.Parse(typeof(TaskFlowManagement.Command), req.Name), param);

            var result = new
            {
                status = "OK",
                msg = "",
                ErrMsg = ""
            };

            return Ok(result);
        }
        public class InfoRequest
        {
            public string Name { get; set; }
        }
        [HttpPost("[action]")]
        public IActionResult NodeInfo([FromBody]InfoRequest req)
        {

            var result = new
            {
                status = "OK",
                msg = NodeManagement.Get(req.Name),
                ErrMsg = ""
            };

            return Ok(result);
        }
        [HttpGet("[action]")]
        public IActionResult NodeInfoList()
        {

            var result = new
            {
                status = "OK",
                msg = NodeManagement.GetList(),
                ErrMsg = ""
            };

            return Ok(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetLogData()
        {
            lock (ControlHub.SECS_Log)
            {
                lock (ControlHub.CMD_Log)
                {
                    var result = new
                    {
                        status = "OK",
                        secslog = ControlHub.SECS_Log,
                        cmdlog = ControlHub.CMD_Log,
                        ErrMsg = ""
                    };

                    return Ok(result);
                }
            }
        }
    }
}
