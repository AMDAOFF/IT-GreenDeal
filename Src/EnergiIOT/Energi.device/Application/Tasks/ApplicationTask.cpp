

#include "ApplicationTask.h"

ApplicationTask::ApplicationTask(Board& board, publishMessage_t& publishMessage, connectSettings_t& connectSettings, subscribeTopic_t& subscribeTopic) :
_board { board },
_publishMessage { publishMessage },
_connectionSettings { connectSettings },
_subscribeTopic { subscribeTopic }
{}

// WARNING!
// The implementation of the serial RX interrupt driver is easy to expose to stack overflow!!!!
// This driver will only get the payload, of the MQTT message, everything else will be lost!

#define RX_BUFF_SIZE 10
char _rxBuffer[RX_BUFF_SIZE];
volatile uint8_t current_index = 0;
volatile bool process_data = false;
volatile char temp;
uint8_t hashCount;

ISR(USART_RX_vect)
{
	temp = UDR0;
	
	if(process_data == true)
	{
		return;
	}
	
	if(temp == '#')
	{
		++hashCount;
		
		if(hashCount >= 2)
		{
			hashCount = 0;
			current_index = 0;
			process_data = true;
		}
		return;
	}
	
	if (hashCount == 1)
	{
		_rxBuffer[current_index++] = temp;
	}
}

void ApplicationTask::Service()
{
	if (process_data)
	{
		_board.SetReadyState(true);

		SetConfig();

		_board.GetLedController().ToggleLed(Leds::Send);
		
		_publishMessage.message =  "#Config#";
		_board.GetMqttClient().Publish(&_publishMessage);
		
		process_data = false;
	}
	
	if (_board.GetReadyState() == false)
	{
		_publishMessage.message =  "#Online#";
		_board.GetLedController().SetLedState(Leds::Send, true);
		_board.GetMqttClient().Connect(&_connectionSettings);
		_board.GetChronos().Delay(200);
		_board.GetMqttClient().Subscribe(&_subscribeTopic);
		_board.GetChronos().Delay(200);
		_board.GetMqttClient().Publish(&_publishMessage);
		_board.GetLedController().SetLedState(Leds::Send, false);
	}
	
	return;
}

void ApplicationTask::SetConfig()
{
	// Ventilation.
	if (_rxBuffer[0] == '1')
	{
		_board.GetLedController().SetLedState(Leds::Error, true);
		_board.GetVentilationFan().SetValue(LogicalState::Active);
	}
	else
	{
		_board.GetLedController().SetLedState(Leds::Error, false);
		_board.GetVentilationFan().SetValue(LogicalState::InActive);
	}
	
	// Recycling.
	if (_rxBuffer[1] == '1')
	{
		_board.GetLedController().SetLedState(Leds::Working, true);
		_board.GetRecyclingFan().SetValue(LogicalState::Active);
	}
	else
	{
		_board.GetLedController().SetLedState(Leds::Working, false);
		_board.GetRecyclingFan().SetValue(LogicalState::InActive);
	}
	
	// Ventilation valve.
	if (_rxBuffer[2] == '1')
	{
		_board.GetLedController().SetLedState(Leds::Fail, true);

	}
	else
	{
		_board.GetLedController().SetLedState(Leds::Fail, false);
	}
	
	// Radiator.
	if (_rxBuffer[3] == '1')
	{
		_board.GetLedController().SetLedState(Leds::Busy, true);
		//_board.GetRadiator();
	}
	else
	{
		_board.GetLedController().SetLedState(Leds::Busy, false);
		//_board.GetRadiator();
	}
	
	return;
}