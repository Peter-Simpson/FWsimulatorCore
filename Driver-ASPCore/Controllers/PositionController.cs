using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private string methodName = nameof(PositionController).Substring(0, nameof(PositionController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<ShortResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                short position = Program.Simulator.Position;
                Program.TraceLogger.LogMessage(methodName + " Get", position.ToString());
                return new ShortResponse(ClientTransactionID, ClientID, methodName, position);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Get", string.Format("Exception: {0}", ex.ToString()));
                ShortResponse response = new ShortResponse(ClientTransactionID, ClientID, methodName, 0);
                response.DriverException = ex;
                return response;
            }
        }

        [HttpPut()]
        public ActionResult<MethodResponse> Put(int ClientID, int ClientTransactionID, [FromForm] short Position)
        {
            try
            {
                Program.Simulator.Position = Position;
                    Program.TraceLogger.LogMessage(methodName + " Set", string.Format("{0} set to {1} OK", methodName, Position.ToString()));
                return new MethodResponse(ClientTransactionID, ClientID, methodName);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Set", string.Format("Exception when setting {0} to {1}: {2}", methodName, Position, ex.ToString()));
                MethodResponse response = new MethodResponse(ClientTransactionID, ClientID, methodName);
                response.DriverException = ex;
                return response;
            }
        }



    }
}
