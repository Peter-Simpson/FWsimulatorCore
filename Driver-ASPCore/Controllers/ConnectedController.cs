using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class ConnectedController : ControllerBase
    {
        private string methodName = nameof(ConnectedController).Substring(0, nameof(ConnectedController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<BoolResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                bool connected = Program.Simulator.Connected;
                Program.TraceLogger.LogMessage(methodName + " Get", connected.ToString());
                return new BoolResponse(ClientTransactionID, ClientID, methodName, connected);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Get", string.Format("Exception: {0}", ex.ToString()));
                BoolResponse response = new BoolResponse(ClientTransactionID, ClientID, methodName, false);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }

        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID, bool Connected)
        {
            try
            {
                Program.Simulator.Connected = Connected;
                Program.TraceLogger.LogMessage(methodName + " Set", string.Format("Connected set {0} OK", Connected));
                return new MethodResponse(ClientTransactionID, ClientID, methodName);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Set", string.Format("Exception when setting {0} {1} {2}", methodName, Connected, ex.ToString()));
                MethodResponse response = new MethodResponse(ClientTransactionID, ClientID, methodName);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
