using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class DisposeController : ControllerBase
    {
        private string methodName = nameof(DisposeController).Substring(0, nameof(DisposeController).IndexOf("Controller"));

        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID)
        {
            try
            {
                Program.TraceLogger.LogMessage(methodName, string.Format("About to dispose..."));
                Program.Simulator.Dispose();
                Program.TraceLogger.LogMessage(methodName, string.Format("Disposed OK"));
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
