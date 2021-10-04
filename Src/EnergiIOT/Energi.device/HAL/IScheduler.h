
#ifndef __ISCHEDULER_H__
#define __ISCHEDULER_H__


class IScheduler
{
public:
	virtual ~IScheduler(){}
	virtual void Method1() = 0;
	virtual void Method2() = 0;

}; 

#endif 
