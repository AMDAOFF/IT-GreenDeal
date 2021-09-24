
#ifndef __CHRONOS_H__
#define __CHRONOS_H__

#include "IChronos.h"

class Chronos : public IChronos
{
	public:
	Chronos(unsigned long f_cpu);
	~Chronos() = default;

	unsigned long Time() final override;
	void ResetTimer() final override;
	void Delay(unsigned int timeout) final override;

	private:
	Chronos( const Chronos &c );
	Chronos& operator=( const Chronos &c );
};

#endif //__CHRONOS_H__
