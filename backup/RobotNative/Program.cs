using Avans.StatisticalRobot;
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var ultrasonicSensor = new Ultrasonic(18);
        var dht12Sensor = new DHT12(24);
        var lightSensor = new LightSensor(4, 100);
        var motorController = new MotorController();
        var mqttClientHandler = new MqttClientHandler();
        var buttonSensorManager = new ButtonSensorManager(6, mqttClientHandler); // Knop toegevoegd op slot 26

        var sensorManager = new SensorManager(ultrasonicSensor, dht12Sensor, lightSensor, motorController, buttonSensorManager);
        var robotController = new RobotController(sensorManager, motorController);
        var dataCollector = new DataCollector(sensorManager, mqttClientHandler);

        // Start de knopluisteraar
        buttonSensorManager.StartListening();

        // Start het verzamelen van data
        var cancellationTokenSource = new CancellationTokenSource();
        dataCollector.StartCollecting(cancellationTokenSource.Token);

        await mqttClientHandler.StartListeningAsync(robotController);

        // Zorg ervoor dat het programma blijft draaien
        Console.WriteLine("Program is running...");
        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}