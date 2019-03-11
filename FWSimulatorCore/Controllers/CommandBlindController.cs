using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class CommandBlindController : ControllerBase
    {
        private string methodName = nameof(CommandBlindController).Substring(0, nameof(CommandBlindController).IndexOf("Controller"));

        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID, [FromForm] string Command, [FromForm] bool Raw)
        {
            try
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0}, Parameters: {1}", Command, Raw));
                Program.Simulator.CommandBlind(Command, Raw);
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0} completed OK", Command));
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
