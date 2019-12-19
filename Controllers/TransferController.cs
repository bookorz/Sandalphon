using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ControlService.Config;
using ControlService.Engine;
using ControlService.Management;

namespace sandalphon.Controllers
{
    [Route("api/[controller]")]
    public class TransferController : Controller
    {
        public class TaskRequest
        {
            public string name { get; set; }
            public string target { get; set; }
            public string value { get; set; }
            public string val2 { get; set; }
            public string position { get; set; }
            public string mode { get; set; }
            public string station { get; set; }
            public string direction { get; set; }
        }
        [HttpPost("[action]")]

        public IActionResult TaskRun([FromForm]TaskRequest req)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("@Target", req.target);
            param.Add("@Value", req.value);
            param.Add("@Val2", req.val2);
            param.Add("@Position", req.position);
            param.Add("@Mode", req.mode);
            param.Add("@Station", req.station);
            param.Add("@Direction", req.direction);
            TaskFlowManagement.Excute(Guid.NewGuid().ToString(), (TaskFlowManagement.Command)Enum.Parse(typeof(TaskFlowManagement.Command), req.name), param);

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
        public IActionResult NodeInfo([FromForm]InfoRequest req)
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
        [HttpGet("[action]")]
        public IActionResult AlarmHistory([FromQuery(Name = "from")] int from = 0, [FromQuery(Name = "to")] int to = 4)
        {
            //System.Threading.Thread.Sleep(500); // Fake latency
            var quantity = to - from;

            // We should also avoid going too far in the list.
            if (quantity <= 0)
            {
                return BadRequest("You cannot have the 'to' parameter higher than 'from' parameter.");
            }

            if (from < 0)
            {
                return BadRequest("You cannot go in the negative with the 'from' parameter");
            }

            var allAlarm = AlarmManagement.GetHistory();
            var result = new
            {
                Total = allAlarm.Count,
                alarmList = allAlarm.Skip(from).Take(quantity).ToArray()
            };

            return Ok(result);
        }
       
        [HttpGet("[action]")]
        public IActionResult IO_List()
        {

            //var nodeList = ioList.Select(o => o.node).Distinct();
            SpinWait.SpinUntil(() => TaskFlowManagement.Excute(Guid.NewGuid().ToString(), TaskFlowManagement.Command.GET_IO).Finished, 5000);

            List<RELIO> tmp = NodeManagement.Get("CTU").DIO.Values.ToList();
            var result = new
            {
                ioList = tmp
            };

            return Ok(result);
        }
    }
}
