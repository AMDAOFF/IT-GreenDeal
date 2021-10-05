
#ifndef __II2C_H__
#define __II2C_H__

/**
* @brief  This interface class specifies the functions that are available for
* an application that requires I2C communication.
*/
class II2C
{
	public:
	virtual ~II2C() = default;
	
	/**
	* @brief Write to an I2C slave and performs a complete write transaction.
	* @param address - address of the slave device to be written to.
	* @param registerAddress -  The register to be sent to the slave.
	*/
	virtual void WriteRegister(uint8_t* address, uint8_t* reg) = 0;
	
	/**
	* @brief Read from an I2C slave and performs a complete read transaction.
	*/
	virtual void ReadRegister() = 0;
};

#endif