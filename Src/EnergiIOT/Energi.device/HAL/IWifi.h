

#ifndef __IWIFI_H__
#define __IWIFI_H__

#include <avr/io.h>

/**
* @brief Enum - For the port type to use.
*/
typedef enum Port
{
	TCP = 0,
	TCPssl = 1
};

/**
* @brief  Standard supported by the WIFI.
* All the settings for the base wifi.
*/
typedef struct wifiSettings_t
{
	char* ssid;
	char* password;
} wifiSettings_t;

/**
* @brief  Standard supported by the WIFI.
* All the settings for the base server.
*/
typedef struct serverInfo_t
{
	char* serverIp;
	Port port;

} serverInfo_t;

/**
* @brief  An interface to the underlying Wifi Stack.
**/
class IWifi
{
	public:
	virtual ~IWifi() = default;
	
	/**
	* @brief Initializes the Wifi stack.
	* @param settings - all settings needed for the WIFI.
	* @return bool - true if initialization succeeded.
	*/
	virtual bool Initialize(wifiSettings_t* settings) = 0;
	
	/**
	* @brief Check if the hardware exits.
	* @return bool - true if the hardware exits.
	*/
	virtual bool IsPresented() = 0;
	
	/**
	* @brief Open a TCP connection.
	* @param domain - the domain address to connect to.
	* @param port - the port to connect with.
	* @return status - return a status code.
	*/
	virtual uint8_t Start(char* domain, char* port) = 0;
	
	/**
	* @brief Transmit data to er server over TCP.
	* @param domain - All the server information needed to sent.
	* @param data - the data to sent.
	* @param dataLength - How many bytes are there.
	*/
	virtual void SendData(serverInfo_t* serverInfo, char data[], uint8_t dataLength) = 0;
	
	/**
	* @brief Create a ping, for test.
	*/
	virtual void Ping(const char *ip) = 0;
	
	/**
	* @brief Check if there is a connection.
	*/
	virtual uint8_t IsConnected() = 0;
};

#endif
