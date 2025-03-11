using Avans.StatisticalRobot;

public class SensorManager
{
    private readonly Ultrasonic _ultrasonicSensor;
    private readonly DHT12 _dht12Sensor;

    public SensorManager(Ultrasonic ultrasonicSensor, DHT12 dht12Sensor)
    {
        _ultrasonicSensor = ultrasonicSensor;
        _dht12Sensor = dht12Sensor;
    }

    public int GetDistance() => _ultrasonicSensor.GetUltrasoneDistance();

    public (int Temperature, int Humidity) GetTemperatureAndHumidity()
    {
        var data = _dht12Sensor.GetSensorReadings();
        return (data.Temperature, data.Humidity);
    }
}