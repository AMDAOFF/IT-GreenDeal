
#ifndef __I2CHANDLER_H__
#define __I2CHANDLER_H__

#include "II2C.h"
#include <stdint.h>

#define I2C_READ 0x01
#define I2C_WRITE 0x00

class I2C final : public II2C
{
	public:
	I2C() = default;

	void Initialize(void);
	void WriteRegister(uint8_t* address, uint8_t* reg) final override;
	void ReadRegister() final override;

	private:
	I2C( const I2C &c );
	I2C& operator=( const I2C &c );

	uint8_t Start(uint8_t address);
	uint8_t Write(uint8_t data);
	uint8_t Read_ack(void);
	uint8_t Read_nack(void);
	uint8_t Transmit(uint8_t address, uint8_t* data, uint16_t length);
	uint8_t Receive(uint8_t address, uint8_t* data, uint16_t length);
	uint8_t WriteReg(uint8_t devaddr, uint8_t regaddr, uint8_t* data, uint16_t length);
	uint8_t ReadReg(uint8_t devaddr, uint8_t regaddr, uint8_t* data, uint16_t length);
	void Stop(void);
};

#endif //__I2CHANDLER_H__
