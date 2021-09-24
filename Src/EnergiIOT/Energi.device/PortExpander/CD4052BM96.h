
#ifndef __CD4052BM96_H__
#define __CD4052BM96_H__

#include <avr/io.h>

#include "../Utility/Types.h"
#include "../DigitalPins/DigitalOutput.h"

class CD4052BM96
{
	public:
	CD4052BM96(DigitalOutput* doA, DigitalOutput* doB, SerialPorts defaultSerial);
	~CD4052BM96() = default;
	
	void ChangePort(SerialPorts port);
	void UseDefaultPort();
	void SetDefaultPort(SerialPorts port);
	SerialPorts GetCurrentPort();

	private:
	CD4052BM96( const CD4052BM96 &c );
	CD4052BM96& operator=( const CD4052BM96 &c );
	
	void SetPort(SerialPorts port);
	
	DigitalOutput* _doA;
	DigitalOutput* _doB;
	
	SerialPorts _currentPort;
	SerialPorts _defaultSerial;
	
}; //CD4052BM96

#endif


//
//#ifndef __CD4052BM96_H__
//#define __CD4052BM96_H__
//
//#include "avr/io.h"
//
//#include "../Utility/Types.h"
//#include "../DigitalPins/DigitalOutput.h"
//
//class CD4052BM96
//{
//public:
//CD4052BM96(volatile uint8_t* statePortA, uint8_t statePinA, volatile uint8_t* statePortB, uint8_t statePinB, SerialPorts defaultSerial);
//~CD4052BM96() = default;
//
//void ChangePort(SerialPorts port);
//void UseDefaultPort();
//void SetDefaultPort(SerialPorts port);
//
//private:
//CD4052BM96( const CD4052BM96 &c );
//CD4052BM96& operator=( const CD4052BM96 &c );
//
//
//SerialPorts _defaultSerial;
//
//DigitalOutput _doA;
//DigitalOutput _doB;
//
//}; //CD4052BM96
//
//#endif
