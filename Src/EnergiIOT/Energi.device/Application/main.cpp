
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

// Logging!!! This is only for the poc!!
ErrorLog Logger;

int main(void)
{
	Board board;
	board.Initialize();
	MqttPingTask mqttPingTask(board.GetMqttClient(), connectSettings);
	ApplicationTask applicationTask(board);
	TemperatureTask temperatureTask(board, publishMessage);
	
	board.GetWifi().Initialize("JK", "472yO58;");	
	//board.GetWifi().Initialize("Stofa67337\0", "gyros54fyh36\0");
	
	//// Wifi
	board.GetMqttClient().Connect(&connectSettings);
	board.GetMqttClient().Subscribe(&subscribeTopic);
	board.GetMqttClient().Publish(&publishMessage);
	
	unsigned long task1 = board.GetChronos().Time();
	unsigned long task2 = board.GetChronos().Time();
	unsigned long task3 = board.GetChronos().Time();
	unsigned long task4 = board.GetChronos().Time();
	
	//board.GetRadiator().SetValue(LogicalState::Active);
	
	while (1)
	{
		// Mqtt Ping.
		if(board.GetChronos().Time() - task1 > 2500)
		{
			//board.GetMqttClient().PingReq(&connectSettings);
			
			mqttPingTask.Service();
			board.GetLedController().ToggleLed(Leds::Ready);
			task1 = board.GetChronos().Time();
			
		}
		// Publish. now temperature task.
		if(board.GetChronos().Time() - task4 > 700)
		{			
			temperatureTask.Service();
			task4 = board.GetChronos().Time();
		}
		// Sub service.
		if(board.GetChronos().Time() - task3 > 20)
		{
			
			applicationTask.Service();

			task3 = board.GetChronos().Time();
		}
				
		
	}
	// ERROR...
	board.GetLedController().SetLedState(Leds::Error, true);
}
