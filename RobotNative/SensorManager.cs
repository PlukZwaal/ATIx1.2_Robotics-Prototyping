using Avans.StatisticalRobot;

public class SensorManager
{
    private readonly Ultrasonic _ultrasonicSensor;
    private readonly DHT12 _dht12Sensor;
    private readonly LightSensor _lightSensor;
    private readonly MotorController _motorController;

    public SensorManager(Ultrasonic ultrasonicSensor, DHT12 dht12Sensor, LightSensor lightSensor, MotorController motorController, ButtonSensorManager buttonSensorManager)
    {
        _ultrasonicSensor = ultrasonicSensor;
        _dht12Sensor = dht12Sensor;
        _lightSensor = lightSensor;
        _motorController = motorController;
    }

    //Hieronder staan alle methodes om data te verkrijgern van de sensoren.s
    public int GetDistance() => _ultrasonicSensor.GetUltrasoneDistance();

    public (int Temperature, int Humidity) GetTemperatureAndHumidity()
    {
        var data = _dht12Sensor.GetSensorReadings();
        return (data.Temperature, data.Humidity);
    }

    public int GetBatteryMillivolts() => Robot.ReadBatteryMillivolts();

    public int GetLux() => _lightSensor.GetLux();

    public (int leftSpeed, int rightSpeed) GetMotorSpeed() => _motorController.GetMotorSpeed();
}