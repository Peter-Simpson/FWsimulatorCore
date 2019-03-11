using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class CommandStringController : ControllerBase
    {
        private string methodName = nameof(CommandStringController).Substring(0, nameof(CommandStringController).IndexOf("Controller"));

        [HttpPut()]
        public ActionResult<StringResponse> Put(int ClientID, int ClientTransactionID, [FromForm] string Command, [FromForm] bool Raw)
        {
            try
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0}, Parameters: {1}", Command, Raw));
                string response = Program.Simulator.CommandString(Command, Raw);
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0} completed OK", Command));
                return new StringResponse(ClientTransactionID, ClientID, methodName, response);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Exception: {0}", ex.ToString()));
                StringResponse response = new StringResponse(ClientTransactionID, ClientID, methodName, "");
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
