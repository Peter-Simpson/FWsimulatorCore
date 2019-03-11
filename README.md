# An ASCOM ASP.NET Core FilterWheel Simulator
This is an ASCOM FilterWheel simulator written in ASP.NET Core targetting framework 2.2 and developed in Visual Studio on Windows. The application is published to create a Linux-Arm binary that will run on a Raspberry Pi through copy and paste deployment.

# Building the application
  * Install Visual Studion 2017 if not already installed
  * Install the ASCOM Developer Components if not already installed - https://github.com/ASCOMInitiative/ASCOMPlatform/releases
  * Clone this repository
  * Start Visual Studio on the project solution FWSimulatorCore.sln
  * Select the Debug/AnyCPU configuration
  * Correct the Assembly References for ASCOM component references if required. The paths to these may differ on your system, depending on exactly where you cloned the repository. If necessary, for each reference:
    * Note the component name and remove the reference
	* Click on Dependencies / Add Reference 
	* Browse to C:\Program Files (x86)\ASCOM\Platform 6 Developer Components\Components\Platform 6    [Ignore (x86) if you are on a 32bit machine]
	* Select the DLL that matches the component name you noted
  * By default the simulator will start on port 60001 accessible through all network interfaces. If you need different start-up parameters, configure them in the hosting.json file.
  * Build the solution, which should compile without issue

# Publishing to Linux-Arm
  * Start a Visual Studio command prompt
  * Navigate to the root of the repository
  * Run the build.cmd command file, which should generate output similar to this:
  
  Restoring packages for J:\FWSimulatorCore\FWSimulatorCore\FWSimulatorCore.csproj...
  Generating MSBuild file J:\FWSimulatorCore\FWSimulatorCore\obj\FWSimulatorCore.csproj.nuget.g.props.
  Restore completed in 856.01 ms for J:\FWSimulatorCore\FWSimulatorCore\FWSimulatorCore.csproj.
  FWSimulatorCore -> J:\FWSimulatorCore\FWSimulatorCore\bin\Debug\netcoreapp2.2\linux-arm\FWSimulatorCore.dll
  FWSimulatorCore -> J:\FWSimulatorCore\FWSimulatorCore\bin\Debug\netcoreapp2.2\linux-arm\publish\
  
# Deploying and Running
  * Copy the entire contents of the FWSimulatorCore\FWSimulatorCore\bin\Debug\netcoreapp2.2\linux-arm\publish folder to a folder in the target device
  * Mark the FWSimulatorCore file as an executable by changing its flags with "chmod 755"
  * Start the simulator with the ./FWSimulatorCore command, it should produce messages something like this:
  
  pi@pi3b:~/fwsim $ ./FWSimulatorCore
  info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0] User profile is available. Using '/home/pi/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
  Hosting environment: Production
  Content root path: /home/pi/fwsim
  Now listening on: http://[::]:60001
  Application started. Press Ctrl+C to shut down.

# Testing
  * Install ASCOM Remote if not already installed - https://github.com/ASCOMInitiative/ASCOMRemote/releases
  * Install Conform if not already installed - https://github.com/ASCOMInitiative/Conform/releases
  * Start Conform
  * Click "Options" and select "Check FilterWheel"
  * Click "Options/Select Driver" and select "ASCOM Remote Client 1"
  * Click "Options/Driver Setup", enter the IP address and port number of the FilterWheel simulator on your Raspberry Pi and click "OK"
  * Click "Check Conformance"
  * You should get a clean run apart from an issue in the Description property, which I have introduced intentionally to demonstrate that errors are passed back correctly. If you wish, you can get a clean run by editing the Description method in FWSimulator.cs.
