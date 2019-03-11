using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private string methodName = nameof(NameController).Substring(0, nameof(NameController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<StringResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                string name = Program.Simulator.Name;
                Program.TraceLogger.LogMessage(methodName + " Get", name);
                return new StringResponse(ClientTransactionID, ClientID, methodName, name);
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
