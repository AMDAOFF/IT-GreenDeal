

#include "TemperatureTask.h"

#include <stdlib.h>
#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>

float _value;

ISR(ADC_vect)
{
	float temp = log(10000.0 * (1024.0 / ADC - 1));
	float tempK = 1/(0.001129148+(0.000234125+(0.0000000876741*temp*temp))*temp);
	_value = tempK - 273.15;
	
	//ADCSRA = | (1<<ADSC)
	ADCSRA &= ~(1<<ADSC); ////clear PC3	
}

TemperatureTask::TemperatureTask(Board& board, publishMessage_t& publishMessage) :
_board { board },
_publishMessage { publishMessage }
{}

void TemperatureTask::Service()
{
	char buffer[7];
	dtostrf(_value,4,2,buffer);	

	char str[14];
	memset(str, 0, 14);
	strcpy(str, "#Temp="); // 3 is a fixed number, we need to control free sockets in software.
	strcat(str, buffer);
	strcat(str, "#");

	_publishMessage.message = str;	
	_board.GetMqttClient().Publish(&_publishMessage);

	ADCSRA |= (1<<ADSC);

	return;
}