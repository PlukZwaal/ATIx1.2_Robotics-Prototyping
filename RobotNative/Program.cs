using Avans.StatisticalRobot;

class Program
{
    static async Task Main(string[] args)
    {
        //Initialiseer sensoren, motorcontroller en communicatiecomponenten
        var ultrasonicSensor = new Ultrasonic(18);
        var dht12Sensor = new DHT12(24);
        var lightSensor = new LightSensor(4, 100);
        var motorController = new MotorController();
        var mqttClientHandler = new MqttClientHandler();
        var buttonSensorManager = new ButtonSensorManager(6, mqttClientHandler);
        var sensorManager = new SensorManager(ultrasonicSensor, dht12Sensor, lightSensor, motorController, buttonSensorManager);
        var robotController = new RobotController(sensorManager, motorController, new LCD16x2(0x3E));
        var dataCollector = new DataCollector(sensorManager, mqttClientHandler);
        // var textScreen = new LCD16x2(0x3E);
        // textScreen.SetText("Murron is mijn G");
        

        // Instantieer en zet de ventilator aan op Pin 6
        // var groveMiniFan = new GroveMiniFan(6);
        // groveMiniFan.SetOff();



        //Hier startren wij de listener van de noodstopknop.
        buttonSensorManager.StartListening();
        var cancellationTokenSource = new CancellationTokenSource();
        dataCollector.StartCollecting(cancellationTokenSource.Token);

        //Hier starten wij de listener van de mqtt server.
        await mqttClientHandler.StartListeningAsync(robotController);

        //Deze houd het programma actief zodat hij nooit zal stoppen.
        while (true)
        {
            Thread.Sleep(1000);
        }
    }
}