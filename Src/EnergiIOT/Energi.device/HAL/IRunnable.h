


#ifndef __IRUNNABLE_H__
#define __IRUNNABLE_H__

/**
* @brief Interface for eligible thread objects.
*/
class IRunnable
{
public:
	virtual ~IRunnable() = default;
		
    /**
    * @brief Function which runs continously in the thread.
    */
    virtual void Service() = 0;	

}; 

#endif 
