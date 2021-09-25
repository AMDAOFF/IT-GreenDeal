
#include "MqttClient.h"
#include <stdio.h>
#include <string.h>


MqttClient::MqttClient(IWifi* wifiClient, IChronos* chronos) :
_wifiClient { wifiClient },
_chronos { chronos },
_connectStatus { false }
{}



void MqttClient::Connect(connectSettings_t* settings)
{	
	// Protocol name and settings byte count.
	uint8_t mqttBasePacketLength = 12;
	
	// Base length + user length.
	uint8_t clientBase = mqttBasePacketLength + 2;
	
	// Client id byte count.
	uint8_t clientIdLength = strlen(settings->userId);
	
	// Base client id length + user length + two bytes for length.
	uint8_t userNameBase = clientBase + clientIdLength + 2;
	
	// User name byte count.
	uint8_t userNameLength = strlen(settings->userName);
	
	// Base client id length + user length + two bytes for length.
	uint8_t passwordBase = userNameBase + userNameLength + 2;
	
	// User name byte count.
	uint8_t passwordLength = strlen(settings->userPassword);
	
	// Total data packet, byte count.
	uint8_t dataLength = passwordBase + passwordLength;
	
	// Protocol Name.
	messageBuffer[0] = 0x10; // Control field.
	messageBuffer[1] = dataLength - 2; // Total remaining Length. (Minus the first two bytes)
	messageBuffer[2] =  0x00; // Protocol name length. (2 bytes)
	messageBuffer[3] =  0x04;
	messageBuffer[4] =  'M'; // Protocol name. (4 bytes)
	messageBuffer[5] =  'Q';
	messageBuffer[6] =  'T';
	messageBuffer[7] =  'T';
	
	messageBuffer[8] =  MQTT_VERSION_3_1_1; // Protocol level. 0x04 = version 3.1.1
	messageBuffer[9] =  0xC2; // Connect flag byte. (user name + password ++ clean session)
	messageBuffer[10] = 0x00; // Keep alive duration in seconds. (2 bytes)
	messageBuffer[11] = 0x0F;
	
	// Client ID
	messageBuffer[12] = 0x00; // User length. (2 bytes)
	messageBuffer[13] = clientIdLength;

	for(uint8_t i = 0; i < clientIdLength ; i++)
	{
		messageBuffer[clientBase + i] = settings->userId[i];
	}
	
	// User name.
	messageBuffer[userNameBase - 2] = 0x00; // User name length. (2 bytes)
	messageBuffer[userNameBase - 1] = userNameLength;
	
	for(uint8_t i = 0; i < userNameLength; i++)
	{
		messageBuffer[userNameBase + i] = settings->userName[i];
	}
	
	// Password.
	messageBuffer[passwordBase - 2] = 0x00; // Password length. (2 bytes)
	messageBuffer[passwordBase - 1] = passwordLength;
	
	for(uint8_t i = 0; i < passwordLength; i++)
	{
		messageBuffer[passwordBase + i] = settings->userPassword[i];
	}
	
	_wifiClient->SendData(settings->serverInfo, messageBuffer, dataLength);
	
	_connectStatus = true;
	
	return;
}

void MqttClient::PingReq(connectSettings_t* settings)
{
	messageBuffer[0] = (12 << 4);
	messageBuffer[1] = 0x00;
	
	_wifiClient->SendData(settings->serverInfo, messageBuffer, 2);
	
	return;
}

void MqttClient::Publish(publishMessage_t* message)
{
	// Control field + remaining length.
	uint8_t mqttBasePacketLength = 2;
	
	// Base packet + topic length.
	uint8_t topicBase = mqttBasePacketLength + 2;
	
	// Topic length.
	uint8_t topicLength = strlen(message->topic);
	
	// Message start.
	uint8_t messageBase = topicBase + topicLength + 2;
	
	// Message length.
	uint8_t messageLength = strlen(message->message);
	
	// Total data packet, byte count.
	//uint8_t dataLength = clientBase + clientIdLength;
	uint8_t dataLength = messageBase + messageLength;
	
	messageBuffer[0] = 0x30; // QoS level 0 and without retaining the message control flag will be 0.
	messageBuffer[1] = dataLength - 2; // Total remaining Length. (Minus the first two bytes)
	
	// User name.
	messageBuffer[topicBase - 2] = 0x00; // Topic length. (2 bytes)
	messageBuffer[topicBase - 1] = topicLength;
	
	for(uint8_t i = 0; i < topicLength; i++)
	{
		messageBuffer[topicBase + i] = message->topic[i];
	}
	
	// Message.
	messageBuffer[messageBase - 2] = 0x00; // Topic length. (2 bytes)
	messageBuffer[messageBase - 1] = messageLength;
	
	for(uint8_t i = 0; i < messageLength; i++)
	{
		messageBuffer[messageBase + i] = message->message[i];
	}
	
	_wifiClient->SendData(message->connectionSettings->serverInfo, messageBuffer, dataLength);
	
	return;
}

void MqttClient::Subscribe(subscribeTopic_t * topic)
{
	// Topic start. base mqtt + packet id + length bytes.
	uint8_t topicBase = 6;
	
	// Topic length.
	uint8_t topicLength = strlen(topic->topic);
	
	// Total data packet, byte count.
	uint8_t dataLength = topicBase + topicLength + 1;
	
	// Base mqtt
	messageBuffer[0] = 0x82;
	messageBuffer[1] = dataLength - 2; // Total remaining Length. (Minus the first byte)
	
	// Packet ID
	messageBuffer[2] = 0x00; // Packet ID. (two bytes)
	messageBuffer[3] = _pakatID++;
	
	// Topic length.
	messageBuffer[topicBase - 2] = 0x00; // Topic length. (2 bytes)
	messageBuffer[topicBase - 1] = topicLength;
	
	for(uint8_t i = 0; i < topicLength; i++)
	{
		messageBuffer[topicBase + i] = topic->topic[i];
	}
	
	messageBuffer[dataLength - 1] = 0x00;
	
	_wifiClient->SendData(topic->connectionSettings->serverInfo, messageBuffer, dataLength);
	
	return;
}

bool MqttClient::IsConnected()
{
	return _connectStatus;
}