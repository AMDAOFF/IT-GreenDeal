
#ifndef __BOARD_H__
#define __BOARD_H__


#include <avr/io.h>
#include <stdlib.h>

#include "PinMux.h"
#include "../Utility/Types.h"

#include "../Utility/Time/Chronos.h"
#include "../Serialport/Serialport.h"
#include "ISerialport.h"
#include "../I2C/I2C.h"
#include "II2C.h"
#include "ILedController.h"
#include "../Portexpander/PCF8574.h"
#include "../DigitalPins/DigitalOutput.h"
#include "IDigitalOutput.h"
#include "../Portexpander/CD4052BM96.h"
#include "../Wifi/EspCommunicator.h"
#include "../Mqtt/MqttClient.h"
#include "IAdc.h"
#include "../Adc/Adc.h"

#include "../Utility/Logging/ErrorLog.h"

class Board
{
	public:
	Board();
	~Board() = default;
	
	void Initialize();
	
	ISerialport& GetSerialport();
	ILedController& GetLedController();
	II2C& GetI2C();
	IDigitalOutput& GetFan1();
	IDigitalOutput& GetFan2();
	IWifi& GetWifi();
	IMqttClient& GetMqttClient();
	IChronos& GetChronos();
	IAdc& GetAdc();
	
	void DelayInMS(unsigned long ms);

	private:
	Board( const Board &c );
	Board& operator=( const Board &c );
	
	Chronos _chronos;
	I2C _i2c;
	PCF8574 _ledController;
	DigitalOutput _fan1;
	DigitalOutput _fan2;
	DigitalOutput _serialPortexpanderA;
	DigitalOutput _serialPortexpanderB;
	DigitalOutput _espResetPin;
	CD4052BM96 _serialPortexpander;
	Serialport _serialport;
	EspCommunicator _esp;
	MqttClient _mqttClient;
	Adc _adc;
};

#endif //__BOARD_H__
