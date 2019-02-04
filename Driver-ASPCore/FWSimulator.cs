using System.Collections;
using System.Timers;

namespace ASCOMCore
{
    public class FWSimulator : ASCOM.DeviceInterface.IFilterWheelV2
    {
        private const int NUMBER_OF_FILTERS = 6; // Number of available filter slots in the filterwheel

        private static readonly object positionLockObject = new object(); // Lock object used to manage position changes
        private short targetPosition = 0; // New filter wheel position set through the Position Set property
        private short position = 0; // Current position of the filter wheel (can by -1 when the wheel is in motion) - Returned by Position Get property
        private Timer movementTimer; // Timer to manage filter wheel movements and ensure that the correct -1 value is given while the wheel is moving
        private const int WHEEL_MOVEMENT_RATE = 500; // Time (milliseconds) to move from one filter wheel position to the next.

        #region Initialiser

        public FWSimulator()
        {
            Connected = false; // Start in the not connected state
            position = 0; // Start at the first position (0)
            movementTimer = new Timer(); // Create a new timer but disable it
            movementTimer.Enabled = false;
            movementTimer.Interval = WHEEL_MOVEMENT_RATE; // Set the filter wheel movement rate (milliseconds per position)
            movementTimer.Elapsed += MovementTimer_Elapsed; // Add an event handler to the timer tick event
        }

        #endregion

        #region IFilerWheelV2 interface implementation

        /// <summary>
        /// Undertake one of the driver supported actions
        /// </summary>
        /// <param name="ActionName"></param>
        /// <param name="ActionParameters"></param>
        /// <returns></returns>
        public string Action(string ActionName, string ActionParameters)
        {
            throw new ASCOM.MethodNotImplementedException("Action");
        }

        /// <summary>
        /// Send a command to the driver expecting no response
        /// </summary>
        /// <param name="Command">Command to send</param>
        /// <param name="Raw">Indicate whether the driver is to wrap the command in any required communications protocol</param>
        public void CommandBlind(string Command, bool Raw = false)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        /// <summary>
        /// Send a command to the driver expecting a boolean response
        /// </summary>
        /// <param name="Command">Command to send</param>
        /// <param name="Raw">Indicate whether the driver is to wrap the command in any required communications protocol</param>
        /// <returns>Boolean  result</returns>
        public bool CommandBool(string Command, bool Raw = false)
        {
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        /// <summary>
        /// Send a command to the driver expecting a string response
        /// </summary>
        /// <param name="Command">Command to send</param>
        /// <param name="Raw">Indicate whether the driver is to wrap the command in any required communications protocol</param>
        /// <returns>String  result</returns>
        public string CommandString(string Command, bool Raw = false)
        {
            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        /// <summary>
        /// Get or set the driver's Connected state
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// Dispose of any resources used by the driver.
        /// </summary>
        public void Dispose()
        {
            // No action;
        }

        /// <summary>
        /// Return the device description
        /// </summary>
        public string Description => "Filter wheel simulator built using .NET Core, accessible through the ASCOM REST API.";

        /// <summary>
        /// Return the driver information
        /// </summary>
        public string DriverInfo => "ASCOM .NET Core filter wheel simulator driver v 1.0.0.0";

        /// <summary>
        /// Return the driver version number
        /// </summary>
        public string DriverVersion => "1.0.0.0";

        /// <summary>
        /// Return the filter wheel focus offsets
        /// </summary>
        public int[] FocusOffsets
        {
            get
            {
                int[] offsets = new int[NUMBER_OF_FILTERS];
                for (int i = 0; i < NUMBER_OF_FILTERS; i++)
                {
                    offsets[i] = i;
                }

                return offsets;
            }
        }

        /// <summary>
        /// Return the ASCOM interface version number
        /// </summary>
        public short InterfaceVersion => 2;

        /// <summary>
        /// Return the device name
        /// </summary>
        public string Name => "ASCOM .NET Core filter wheel simulator";

        /// <summary>
        /// Return filter wheel names
        /// </summary>
        public string[] Names
        {
            get
            {
                string[] filterNames = new string[NUMBER_OF_FILTERS];
                for (int i = 0; i < NUMBER_OF_FILTERS; i++)
                {
                    filterNames[i] = "Simulated filter " + i.ToString();
                }
                return filterNames;
            }
        }

        /// <summary>
        /// Get and set the filter wheel position
        /// </summary>
        public short Position
        {
            get
            {
                lock (positionLockObject) // Get exclusive access to the position control variables
                {
                    if (position == targetPosition) return position; // If we are not moving return the current position
                    else return -1; // If we are moving return -1 per the ASCOM specification
                }
            }
            set
            {
                // Make sure the requested position is valid
                if ((value < 0) || (value >= NUMBER_OF_FILTERS)) throw new ASCOM.InvalidValueException("Position", value.ToString(), "0 to " + NUMBER_OF_FILTERS.ToString());

                // Set the target position and initiate movement if we are not currently at the requested position 
                lock (positionLockObject) // Make sure we have exclusive access to the position control variables
                {
                    targetPosition = value; // Set the new target position
                    if (position != targetPosition) // Test whether we are already at the target position and, if not, initiate movement to the new position
                    {
                        movementTimer.Enabled = true; // Enable the movement timer if it's not already enabled
                    }
                    else // Disable the movement timer if we are now at the required position
                    {
                        movementTimer.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Run the configuration dialogue - not supported in this version.
        /// </summary>
        public void SetupDialog()
        {
            throw new ASCOM.MethodNotImplementedException("SetupDialog");
        }

        /// <summary>
        /// List of non-standard actions supported by the driver - none in this example.
        /// </summary>
        public ArrayList SupportedActions => new ArrayList();

        #endregion

        #region Event handlers
        /// <summary>
        /// Event handler for the position movement timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovementTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (positionLockObject) // Get exclusive access to the position control variables.
            {
                if (position != targetPosition) // Test whether we are at the required position
                {
                    // Not in position so increment the position by one
                    position += 1;
                    if (position >= NUMBER_OF_FILTERS) position = 0; // Handle the rotator going past the last filter back to the first
                }

                // Now retest whether we are at the target position and, if so, disable the timer
                if (position == targetPosition)
                {
                    ((Timer)sender).Enabled = false;
                }
            }
        }
        #endregion
    }
}
