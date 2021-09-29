
#ifndef __SERIALPORT_H__
#define __SERIALPORT_H__

#include <avr/io.h>
#include <avr/interrupt.h>

#include "../Utility/Logging/ErrorLog.h"
#include "../Utility/Types.h"
#include "ISerialport.h"
#include "../Portexpander/CD4052BM96.h"
#include "../Utility/Time//Chronos.h"
#include "../Utility/Buffers/RingBuffer.h"
#include "../Boards/GreenDealDevice/PinMux.h"

#define BAUD    9600
#define BRC     ((F_CPU/16/BAUD) - 1)

class Serialport : public ISerialport
{
	public:
	Serialport(CD4052BM96* portexpander, Chronos* chronos);
	~Serialport() = default;
	
	void Initialize();
	void Write(const char c[], SerialPorts port) final override;
	void Write(const char c[], uint8_t strlen, SerialPorts port) final override;
	void WriteChar(const char c, SerialPorts port) final override;
	void WriteLine(const char c[], SerialPorts port) final override;
	char ReadByte() final override;
	bool ReadByteWithTimeout(unsigned char* byte, unsigned int timeout) final override;
	uint8_t ReadTagWithTimeout(SerialPorts port, char buffer[], const char tag[], unsigned int timeout) final override;
	void EnableInterrupt() final override;
	void DisableInterrupt() final override;

	private:
	void Transmit(unsigned char data);
	
	RingBuffer _ringBuffer;
	CD4052BM96* _portexpander;
	Chronos* _chronos;
};

#endif //__SERIALPORT_H__


