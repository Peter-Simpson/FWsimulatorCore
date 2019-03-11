using System;
using Microsoft.AspNetCore.Mvc;

namespace ASCOMCore.Controllers
{
    [Route("api/v1/FilterWheel/0/[controller]")]
    [ApiController]
    public class FocusOffsetsController : ControllerBase
    {
        private string methodName = nameof(FocusOffsetsController).Substring(0, nameof(FocusOffsetsController).IndexOf("Controller"));

        [HttpGet()]
        public ActionResult<IntArray1DResponse> Get(int ClientID, int ClientTransactionID)
        {
            try
            {
                int[] focusOffsets = Program.Simulator.FocusOffsets;
                string focusOffsetsList = string.Join(" ", focusOffsets);
                Program.TraceLogger.LogMessage(methodName + " Get", focusOffsetsList);
                return new IntArray1DResponse(ClientTransactionID, ClientID, methodName, focusOffsets);
            }
            catch (Exception ex)
            {
                Program.TraceLogger.LogMessage(methodName + " Get", string.Format("Exception: {0}", ex.ToString()));
                IntArray1DResponse response = new IntArray1DResponse(ClientTransactionID, ClientID, methodName, new int[1]);
                response.ErrorMessage = ex.Message;
                response.ErrorNumber = ex.HResult - Program.ASCOM_ERROR_NUMBER_OFFSET;
                return response;
            }
        }
    }
}
