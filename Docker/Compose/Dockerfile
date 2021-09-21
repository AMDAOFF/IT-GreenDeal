# Ducker file for RabbitMQ server

FROM rabbitmq:3.9.2-management

RUN rabbitmq-plugins enable rabbitmq_management
RUN rabbitmq-plugins enable rabbitmq_mqtt

COPY rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
#COPY rabbitmq-env.conf /etc/rabbitmq/rabbitmq-env.conf

#RUN chown -R rabbitmq:rabbitmq /var/lib/rabbitmq /etc/rabbitmq &&\
#	chmod 777 /var/lib/rabbitmq /etc/rabbitmq

EXPOSE 15672
EXPOSE 5672
EXPOSE 1883
