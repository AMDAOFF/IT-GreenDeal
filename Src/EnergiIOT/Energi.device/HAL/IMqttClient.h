
#ifndef __IMQTTCLIENT_H__
#define __IMQTTCLIENT_H__

#include "IWifi.h"
#include <avr/io.h>
#include <stdbool.h>

/**
* @brief Enum - For the QoS level.
*/
typedef enum QoS
{
	QoS0 = 0,
	QoS1 = 1,
	QoS2 = 2
};

/**
* @brief  Standard supported by the MQTT.
* All the connection information need by the application.
*/
typedef struct connectSettings_t
{
	char* userId;
	char* userName;
	char* userPassword;
	QoS qoS;
	serverInfo_t* serverInfo;
} connectSettings_t;

/**
* @brief  Standard supported by the MQTT.
*All the publish information need by the application.
*/
typedef struct publishMessage_t
{
	char* message;
	char* topic;
	connectSettings_t* connectionSettings;
} publishMessage_t;

/**
* @brief  Standard supported by the MQTT.
* All the subscribe information need by the application.
*/
typedef struct subscribeTopic_t {
	char* topic;
	connectSettings_t* connectionSettings;
} subscribeTopic_t;

/**
* @brief  This interface class specifies the functions that are available for
* the MQTT client.
*/

class IMqttClient
{
	public:
	virtual ~IMqttClient() = default;
	
	/**
	* @brief Start the MQTT client and connects to a broker.
	* @param settings - all settings needed for the client to connect.
	*/
	virtual void Connect(connectSettings_t* settings) = 0;
	
	/**
	* @brief Sends a ping req to the server to keep the communication open.
	* @param settings - all settings needed for the client to communicate with the server.
	*/
	virtual void PingReq(connectSettings_t* settings) = 0;
	
	/**
	* @brief Sends a publish message to the broker.
	* @param message - all info needed to send a message.
	*/
	virtual void Publish(publishMessage_t* message) = 0;
	
	/**
	* @brief Subscribe to a topic.
	* @param topic - all info needed to subscrube to a topic,
	*/
	virtual void Subscribe(subscribeTopic_t * topic) = 0;
	
	/**
	* @brief Check if there is a connection.
	* @param bool - return the connection state.
	*/
	virtual bool IsConnected() = 0;
};

#endif
