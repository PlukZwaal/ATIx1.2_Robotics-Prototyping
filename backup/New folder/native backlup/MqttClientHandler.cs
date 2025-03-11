using SimpleMqtt;
using System;
using System.Threading.Tasks;
using Avans.StatisticalRobot;


public class MqttClientHandler
{
    private readonly SimpleMqttClient _client;

    public MqttClientHandler()
    {
        _client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("robotnative");
    }

    public async Task StartListeningAsync(RobotController robotController)
    {
        await _client.SubscribeToTopic("robot");

        _client.OnMessageReceived += (sender, eventArgs) =>
        {
            string message = eventArgs.Message;

            switch (message)
            {
                case "ping":
                    _client.PublishMessage("pong", "robot");
                    break;

                case "emergencystop":
                    robotController.StopRobot();
                    Environment.Exit(0);
                    break;

                case "stoprobot":
                    robotController.StopRobot();
                    break;

                case "startrobotautowalk":
                    robotController.StartRobot();
                    break;

                case "forward":
                    robotController.ForwardRobot();
                    break;

                case "backward":
                    robotController.BackwardRobot();
                    break;

                case "left":
                    robotController.LeftRobot();
                    break;

                case "right":
                    robotController.RightRobot();
                    break;

                default:
                    break;
            }
        };

        while (true)
        {
            await Task.Delay(500);
        }
    }
}
