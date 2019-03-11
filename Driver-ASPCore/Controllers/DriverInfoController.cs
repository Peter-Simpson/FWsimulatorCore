using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class DriverInfoController : ControllerBase
    {
        private string methodName = nameof(DriverInfoController).Substring(0, nameof(DriverInfoController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<StringResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                string driverInfo = Program.Simulator.DriverInfo;
                Program.TraceLogger.LogMessage(methodName+" Get", driverInfo);
                return new StringResponse(ClientTransactionID, ClientID, methodName, driverInfo);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName+" Get", string.Format("Exception: {0}", ex.ToString()));
                StringResponse response = new StringResponse(ClientTransactionID, ClientID, methodName, "");
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
