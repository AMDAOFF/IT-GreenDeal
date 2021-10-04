
#ifndef __SCHEDULER_H__
#define __SCHEDULER_H__

class Scheduler
{
public:
	Scheduler();
	~Scheduler() = default;
private:
	Scheduler( const Scheduler &c );
	Scheduler& operator=( const Scheduler &c );

}; 

#endif
