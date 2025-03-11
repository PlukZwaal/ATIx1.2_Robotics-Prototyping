using System.Text.Json;

public class DataCollector
{
    private readonly SensorManager _sensorManager;
    private readonly MqttClientHandler _mqttClientHandler;

    public DataCollector(SensorManager sensorManager, MqttClientHandler mqttClientHandler)
    {
        _sensorManager = sensorManager;
        _mqttClientHandler = mqttClientHandler;
    }

    public void StartCollecting(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                CollectData();
                await Task.Delay(5000);
            }
        }, cancellationToken);
    }

    private void CollectData()
    {
        var (temperature, humidity) = _sensorManager.GetTemperatureAndHumidity();
        int battery = _sensorManager.GetBatteryMillivolts();
        int lux = _sensorManager.GetLux();
        var (leftSpeed, rightSpeed) = _sensorManager.GetMotorSpeed();

        DatabaseConnection.LogData(temperature, humidity, battery, lux);

        var messageObject = new
        {
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Temperature = temperature,
            Humidity = humidity,
            Battery = battery,
            Lux = lux,
            LeftSpeed = leftSpeed,
            RightSpeed = rightSpeed

        };
        string message = JsonSerializer.Serialize(messageObject);
        _mqttClientHandler.PublishMessage(message, "robot/log");
    }
}
