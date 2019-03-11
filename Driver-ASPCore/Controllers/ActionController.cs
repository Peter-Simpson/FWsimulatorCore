using System;
using Microsoft.AspNetCore.Mvc;
using ASCOM.Utilities;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private string methodName = nameof(ActionController).Substring(0, nameof(ActionController).IndexOf("Controller"));
        Util util = new Util();
        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID, [FromForm]string Action, [FromForm]string Parameters)
        {
            try
            {
                string platformversion = util.PlatformVersion;
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0}, Parameters: {1} - Platform version: {2}", Action, Parameters, platformversion));
                Program.Simulator.Action(Action, Parameters);
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0} completed OK", Action));
                return new MethodResponse(ClientTransactionID, ClientID, methodName);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Exception: {0}", ex.ToString()));
                MethodResponse response = new MethodResponse(ClientTransactionID, ClientID, methodName);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
