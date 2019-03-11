using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class CommandBoolController : ControllerBase
    {
        private string methodName = nameof(CommandBoolController).Substring(0, nameof(CommandBoolController).IndexOf("Controller"));

        [HttpPut()]
        public ActionResult<BoolResponse> Put(int ClientID, int ClientTransactionID, [FromForm] string Command, [FromForm] bool Raw)
        {
            try
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0}, Parameters: {1}", Command, Raw));
                bool response = Program.Simulator.CommandBool(Command, Raw);
                Program.TraceLogger.LogMessage(methodName, string.Format("Command: {0} completed OK", Command));
                return new BoolResponse(ClientTransactionID, ClientID, methodName, response);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Exception: {0}", ex.ToString()));
                BoolResponse response = new BoolResponse(ClientTransactionID, ClientID, methodName, false);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
