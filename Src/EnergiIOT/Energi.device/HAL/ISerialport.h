
#ifndef __ISERIALPORT_H__
#define __ISERIALPORT_H__

#include "../Utility/Types.h"
#include "../Utility/Buffers/RingBuffer.h"

class ISerialport
{
	public:
	~ISerialport() = default;
	
	virtual void Write(const char c[], SerialPorts port) = 0;
	virtual void Write(const char c[], uint8_t strlen, SerialPorts port) = 0;
	virtual void WriteChar(const char c, SerialPorts port) = 0;
	virtual void WriteLine(const char c[], SerialPorts port) = 0;
	virtual char ReadByte() = 0;
	virtual bool ReadByteWithTimeout(unsigned char* byte, unsigned int timeout) = 0;
	virtual uint8_t ReadTagWithTimeout(SerialPorts port, char buffer[], const char tag[], unsigned int timeout) = 0;
	virtual void EnableInterrupt() = 0;
	virtual void DisableInterrupt() = 0;
};

#endif