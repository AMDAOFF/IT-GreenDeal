
#ifndef __II2C_H__
#define __II2C_H__


class II2C
{
	public:
	virtual ~II2C() = default;
	virtual void WriteRegister(uint8_t* address, uint8_t* reg) = 0;
	virtual void ReadRegister() = 0;
};

#endif