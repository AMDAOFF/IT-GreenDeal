

#include "ApplicationTask.h"

ApplicationTask::ApplicationTask(Board& board, publishMessage_t& publishMessage, connectSettings_t& connectSettings, subscribeTopic_t& subscribeTopic) :
_board { board },
_publishMessage { publishMessage },
_connectionSettings { connectSettings },
_subscribeTopic { subscribeTopic }
{
	//_gotConfig = false;	
}

// WARNING!
// The implementation of the serial interrupt! This will only get the payload, of the MQTT message, everything else will be lost!

#define RX_BUFF_SIZE 10
char _rxBuffer[RX_BUFF_SIZE];
volatile uint8_t current_index = 0;
volatile bool process_data = false;
volatile char temp;
uint8_t hashCount;

ISR(USART_RX_vect)
{	
	temp = UDR0;
	
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
		//_gotConfig = true;
		_board.SetReadyState(true);
		if (_rxBuffer[0] == '1')
		{
			_board.GetLedController().SetLedState(Leds::Error, true);
			_board.GetVentilationFan().SetValue(LogicalState::Active); // Ventilation
		}
		else
		{
			_board.GetLedController().SetLedState(Leds::Error, false);			
		}
		
		if (_rxBuffer[1] == '1')
		{
			_board.GetLedController().SetLedState(Leds::Working, true);
			_board.GetRecyclingFan().SetValue(LogicalState::Active); // Recycling			
		}
		else
		{
			_board.GetLedController().SetLedState(Leds::Working, false);
			
		}
		
		if (_rxBuffer[2] == '1')
		{
			_board.GetLedController().SetLedState(Leds::Fail, true);
			// Ventilation valve.
		}
		else
		{
			_board.GetLedController().SetLedState(Leds::Fail, false);
		}
		
		if (_rxBuffer[3] == '1')
		{
			_board.GetLedController().SetLedState(Leds::Busy, true);
			// Radiator			
		}
		else
		{
			_board.GetLedController().SetLedState(Leds::Busy, false);
		}

		_board.GetLedController().ToggleLed(Leds::Send);
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