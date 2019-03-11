using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class InterFaceVersionController : ControllerBase
    {
        private string methodName = nameof(InterFaceVersionController).Substring(0, nameof(InterFaceVersionController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<ShortResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                short interfaceVersion = Program.Simulator.InterfaceVersion;
                Program.TraceLogger.LogMessage(methodName + " Get", interfaceVersion.ToString());
                return new ShortResponse(ClientTransactionID, ClientID, methodName, interfaceVersion);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Get", string.Format("Exception: {0}", ex.ToString()));
                ShortResponse response = new ShortResponse(ClientTransactionID, ClientID, methodName, 0);
                response.ErrorMessage= ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
