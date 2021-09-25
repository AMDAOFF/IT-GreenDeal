

#include "Board.h"

/////////////////////////////////////////////////////////////////////////////
// For some unknown reason, Pure Virtual uses new from c++ standard lib,   //
// even though we do not use new in the code.                              //
// Microchip studio does not link with c++ standard lib, so we have to     //
// implement new and delete ourselves.                                     //
// It needs to be explored deeper! It is an avr ecosystem thing,           //
// therefore we implemented it at board level                              //
/////////////////////////////////////////////////////////////////////////////

void* operator new(size_t n)
{
	void* const p = malloc(n);
	// handle p == 0
	return p;
}

void* operator new[](unsigned int n)
{
	void* const p = malloc(n);
	// handle p == 0
	return p;
}

void operator delete(void * p, size_t s) // or delete(void *, std::size_t)
{
	free(p);
}

extern "C" void __cxa_pure_virtual() { while (1); }

Board::Board() :
_chronos(F_CPU),
_i2c(),
_ledController(&_i2c, PCF8574_ADDRESS),
_fan1(&PORTB, PORTB1),
_fan2(&PORTB, PORTB2),
_espResetPin(&PORTD, 0x80),
_serialPortexpanderA(&PORTC, SerialPortexpanderA),
_serialPortexpanderB(&PORTC, SerialPortexpanderB),
_serialPortexpander(&_serialPortexpanderA, &_serialPortexpanderB, SerialPorts::Serial1),
_serialport(&_serialPortexpander, &_chronos),
_esp(&_serialport, SerialPorts::Serial2, &_espResetPin, &_chronos),
_mqttClient(&_esp, &_chronos)
{}

void Board::Initialize()
{
	_serialport.Initialize();
	Logger.Initialize(&_serialport, SerialPorts::Serial4);
	_i2c.Initialize();
	_ledController.ClearAll();
	
	_ledController.SetLedState(Leds::Working, true);
	_chronos.Delay(1000);
	_ledController.SetLedState(Leds::Working, false);
}

ISerialport& Board::GetSerialport()
{
	return _serialport;
}

ILedController& Board::GetLedController()
{
	return _ledController;
}

II2C& Board::GetI2C()
{
	return _i2c;
}

IDigitalOutput& Board::GetFan1()
{
	return _fan1;
}

IDigitalOutput& Board::GetFan2()
{
	return _fan2;
}

IWifi& Board::GetWifi()
{
	return _esp;
}

IMqttClient& Board::GetMqttClient()
{
	return _mqttClient;
}

IChronos& Board::GetChronos()
{
	return _chronos;
}

void Board::DelayInMS(unsigned long ms)
{
	_chronos.Delay(ms);
	return;
}