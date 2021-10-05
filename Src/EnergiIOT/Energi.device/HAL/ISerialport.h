
#ifndef __ISERIALPORT_H__
#define __ISERIALPORT_H__

#include "../Utility/Types.h"
#include "../Utility/Buffers/RingBuffer.h"

/**
* @brief Interface abstraction for a serial port.
*/
class ISerialport
{
	public:
	~ISerialport() = default;
	
	/**
	* @brief Write to an serial port.
	* @param char array - The array to be sent.
	* @param port -  Which port to send to.
	*/
	virtual void Write(const char c[], SerialPorts port) = 0;
	
	/**
	* @brief Writes to an serial port and can handle an array without a stop char.
	* @param char array - The array to be sent.
	* @param strlen - How many bytes to sent.
	* @param port -  Which port to send to.
	*/
	virtual void Write(const char c[], uint8_t strlen, SerialPorts port) = 0;
	
	/**
	* @brief Write a single byte to an serial port.
	* @param char - The char to be sent.
	* @param port - Which port to send to.
	*/
	virtual void WriteChar(const char c, SerialPorts port) = 0;
	
	/**
	* @brief Write to an serial port and adds return and new line chars.
	* @param char array - The array to be sent.
	* @param port -  Which port to send to.
	*/
	virtual void WriteLine(const char c[], SerialPorts port) = 0;
	
	/**
	* @brief Read a single byte.
	* @return byte . the received byte.
	*/
	virtual char ReadByte() = 0;
	
	/**
	* @brief Read a single byte with a timeout.
	* @param byte - The recieved byte, will be stored here.
	* @param timeout -  Time to wait for the byte.
	*/
	virtual bool ReadByteWithTimeout(unsigned char* byte, unsigned int timeout) = 0;
	
	/**
	* @brief Read a single byte with a timeout and a tag check.
	* @param port -  Which port to send to.
	* @param char array - The recieved byte array, will be stored here.
	* @param tag -  the responese tag to check.
	* @param timeout -  Time to wait for the byte.
	* @return tag result - the tag number.
	*/
	virtual uint8_t ReadTagWithTimeout(SerialPorts port, char buffer[], const char tag[], unsigned int timeout) = 0;
	
	/**
	* @brief This enables the serial interrupt.
	*/
	virtual void EnableInterrupt() = 0;
	
	/**
	* @brief This disablers the serial interrupt.
	*/
	virtual void DisableInterrupt() = 0;
};

#endif