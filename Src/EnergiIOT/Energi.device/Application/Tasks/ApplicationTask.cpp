

#include "ApplicationTask.h"

ApplicationTask::ApplicationTask(Board& board) :
_board { board }
{}

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
		if (_rxBuffer[0] == '1')
		{
			_board.GetLedController().SetLedState(Leds::Error, true);
		}
		else
		{
			_board.GetLedController().SetLedState(Leds::Error, false);
		}
		
		if (_rxBuffer[1] == '1')
		{
			_board.GetLedController().SetLedState(Leds::Working, true);
		}
		else
		{
			_board.GetLedController().SetLedState(Leds::Working, false);
		}

		_board.GetLedController().ToggleLed(Leds::Send);
		process_data = false;
	}
	
	return;
}