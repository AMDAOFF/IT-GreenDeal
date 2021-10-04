
#ifndef F_CPU
#define F_CPU 16000000UL
#endif

#include <avr/io.h>
#include <util/delay.h>
#include <avr/pgmspace.h>

#include "../Boards/GreenDealDevice/Board.h"
#include "../Boards/GreenDealDevice/PinMux.h"
#include "../Utility/Types.h"

#include "ApplicationSettings.h"

#include "Tasks/MqttPingTask.h"
#include "Tasks/ApplicationTask.h"
#include "Tasks/TemperatureTask.h"
#include "Tasks/WatchdogTask.h"

// Logging!!! This is only for the poc!!
ErrorLog Logger;

int main(void)
{
	Board _board;
	_board.Initialize();
	MqttPingTask mqttPingTask(_board.GetMqttClient(), connectSettings);
	ApplicationTask applicationTask(_board, publishMessage, connectSettings, subscribeTopic);
	TemperatureTask temperatureTask(_board, publishMessage);
	WatchdogTask watchdogTask(_board.GetWatchdog());
	
	_board.GetWifi().Initialize(&wifiSettings);
	
	// Start watchdog.
	_board.GetWatchdog().Enable(WdtTime::S8);
	
	unsigned long task1 = _board.GetChronos().Time();
	unsigned long task2 = _board.GetChronos().Time();
	unsigned long task3 = _board.GetChronos().Time();
	unsigned long task4 = _board.GetChronos().Time();
	
	while (1)
	{
		// Mqtt Ping.
		if(_board.GetChronos().Time() - task1 > 5000)
		{
			if (_board.GetReadyState())
			{
				mqttPingTask.Service();
			}

			_board.GetLedController().ToggleLed(Leds::Ready);
			task1 = _board.GetChronos().Time();
		}
		
		// Application service.
		if(_board.GetChronos().Time() - task3 > 500)
		{
			applicationTask.Service();
			task3 = _board.GetChronos().Time();
		}
		
		// Publish. now temperature task.
		if(_board.GetChronos().Time() - task4 > 900)
		{
			if (_board.GetReadyState())
			{
				temperatureTask.Service();
			}

			task4 = _board.GetChronos().Time();
		}
		
		// Watchdog.
		if(_board.GetChronos().Time() - task2 > 4000)
		{
			watchdogTask.Service();
			_board.GetLedController().ToggleLed(Leds::Busy);
			task2 = _board.GetChronos().Time();
		}
	}
	// ERROR...
	_board.GetLedController().SetLedState(Leds::Error, true);
}
