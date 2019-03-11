using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private string methodName = nameof(NamesController).Substring(0, nameof(NamesController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<StringArrayResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                string[] names = Program.Simulator.Names;
                string namesList = string.Join(" ", names);
                Program.TraceLogger.LogMessage(methodName + " Get", namesList);
                return new StringArrayResponse(ClientTransactionID, ClientID, methodName, names);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Get", string.Format("Exception: {0}", ex.ToString()));
                string[] names = new string[1];
                StringArrayResponse response = new StringArrayResponse(ClientTransactionID, ClientID, methodName, names);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
