using System;
using System.Threading;

namespace Avans.StatisticalRobot
{
    public class ButtonSensorManager
    {
        private readonly Button _button;
        private readonly MqttClientHandler _mqttClientHandler;
        private Thread _listenThread;
        private bool _isListening;

        public ButtonSensorManager(int buttonSlot, MqttClientHandler mqttClientHandler)
        {
            _button = new Button(buttonSlot);
            _mqttClientHandler = mqttClientHandler;
        }

        public void StartListening()
        {
            if (_isListening) return;

            _isListening = true;

            _listenThread = new Thread(() =>
            {
                while (_isListening)
                {
                    if (_button.GetState() == "Pressed")
                    {
                        _mqttClientHandler.PublishMessage("emergencystop", "robot");
                    }
                    Thread.Sleep(1000); 
                }
            });

            _listenThread.IsBackground = true;
            _listenThread.Start();
        }
    }
}