using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class DriverVersionController : ControllerBase
    {
        private string methodName = nameof(DriverVersionController).Substring(0, nameof(DriverVersionController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<StringResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                string driverVersion = Program.Simulator.DriverVersion;
                Program.TraceLogger.LogMessage(methodName + " Get", driverVersion);
                return new StringResponse(ClientTransactionID, ClientID, methodName, driverVersion);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Get", string.Format("Exception: {0}", ex.ToString()));
                StringResponse response = new StringResponse(ClientTransactionID, ClientID, methodName, "");
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
