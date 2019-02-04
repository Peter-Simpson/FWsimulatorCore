using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private string methodName = nameof(ActionController).Substring(0, nameof(ActionController).IndexOf("Controller"));

        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID, [FromForm]string Action, [FromForm]string Parameters)
        {
            try
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0}, Parameters: {1}", Action, Parameters));
                Program.Simulator.Action(Action, Parameters);
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0} completed OK", Action));
                return new MethodResponse(ClientTransactionID, ClientID, methodName);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Exception: {0}", ex.ToString()));
                MethodResponse response = new MethodResponse(ClientTransactionID, ClientID, methodName);
                response.DriverException = ex;
                return response;
            }
        }
    }
}
