
#ifndef __CHRONOS_H__
#define __CHRONOS_H__

#include "IChronos.h"

class Chronos final : public IChronos
{
	public:
	Chronos(unsigned long f_cpu);
	~Chronos() = default;

	unsigned long Time() final override;
	void ResetTimer() final override;
	void Delay(unsigned long timeout) final override;
};

#endif
