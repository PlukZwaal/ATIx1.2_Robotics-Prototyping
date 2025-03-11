class Program
{
    static async Task Main(string[] args)
    {
        var mqttHandler = new MqttClientHandler();
        var robotController = new RobotController();

        await mqttHandler.StartListeningAsync(robotController);
    }
}
