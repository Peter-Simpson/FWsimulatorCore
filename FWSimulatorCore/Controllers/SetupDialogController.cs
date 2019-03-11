using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class SetupDialogController : ControllerBase
    {
        private string methodName = nameof(SetupDialogController).Substring(0, nameof(SetupDialogController).IndexOf("Controller"));

        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID)
        {
            try
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Calling {0}", methodName));
                Program.Simulator.SetupDialog();
                Program.TraceLogger.LogMessage(methodName , string.Format("Finished {0}", methodName));
                return new MethodResponse(ClientTransactionID, ClientID, methodName);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("Exception calling {0}: {1}", methodName, ex.ToString()));
                MethodResponse response = new MethodResponse(ClientTransactionID, ClientID, methodName);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
