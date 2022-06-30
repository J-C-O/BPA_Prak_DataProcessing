using System.Text;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;

//ToDo's: TASK-1, TASK-2 and TASK-3 + optional Bonus

//Telemetry-Data
int uId = 1;
int eventIdx = 0;
string[] eventTypes = {"start", "end"};
string timeStamp;
string messageString;

Random rand = new Random();
int simulatedProcessTime;

// Device-Data
// TASK-1: fill the variables with your data from your created Azure IoT-Hub
string iotHubHostName = "...";
string deviceKey = "...";
string deviceId = "...";

// TASK-2: create a new Object of the type DeviceAuthenticationWithRegistrySymmetricKey
// use the DeviceAuthenticationWithRegistrySymmetricKey Constructor that takes the deviceId and the deviceKey
var deviceAuthentication = new Object();

// TASK-3: create a new Object of the type DeviceClient 
// use for this the Create()-Method that takes the iotHubHostName, deviceAuthentication and Mqtt as TransportType
// for more Info see: https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.devices.client.deviceclient.create?view=azure-dotnet#microsoft-azure-devices-client-deviceclient-create(system-string-microsoft-azure-devices-client-iauthenticationmethod-microsoft-azure-devices-client-transporttype) 
DeviceClient deviceClient = DeviceClient.Create();

// create continuous new telemetry data and send it to the Azure IoT-Hub

while(true) {
    timeStamp = DateTime.UtcNow.ToString("o");

    var telemetryData = new {
        UID = uId,
        EventType = eventTypes[eventIdx],
        TimeStamp = timeStamp
    }

    messageString = JsonConvert.SerializeObject(telemetryData);
    Message message = new Message(Encoding.ASCII.GetBytes(messageString));

    await deviceClient.SendEventAsync(message);
    System.Console.WriteLine($"{DateTime.Now} > Sending message: {messageString}");

    if(eventIdx < 1) {
        eventIdx++;
    } else {
        eventIdx = 0;
    }

    //Bonus: use the Random-Object from above to simulate a processing time between 3 and 11 seconds
    //simulatedProcessTime = ...

    await Task.Delay(3500);
}