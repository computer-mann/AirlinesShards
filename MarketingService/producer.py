import pika
import sys
import time
import json
import ssl

# RabbitMQ connection parameters
rabbitmq_host = 'localhost'  # Replace with RabbitMQ server's IP or hostname if not local
queue_name = 'GettingStarted'    # The name of the queue to send messages to
#credentials=pika.PlainCredentials('hubtelamq_user','hubtelamq_user122')
#ssl_context=ssl.SSLContext(ssl.PROTOCOL_TLS)
#ssl_options=pika.SSLOptions(ssl_context,'b-baa208ea-689b-4185-b9f8-de81c2ba0942.mq.eu-west-1.amazonaws.com')
envelope = {
    "messageId": "str(uuid.uuid4())",
    "conversationId": "str(uuid.uuid4())",
    "destinationAddress": "rabbitmq://localhost/GettingStarted",
    "messageType": ["urn:message:YourNamespace:YourMessageClass"],
    "message": {
        "name": "John Doe1"
    }
}

def send_message():
    # Connect to RabbitMQ server
    connection = pika.BlockingConnection(pika.ConnectionParameters(host=rabbitmq_host,port=5672))
    channel = connection.channel()

    # Declare the queue (this is idempotent, so it won’t create duplicates)
    channel.queue_declare(queue=queue_name, durable=True)

    # Publish the message to the queue
    
    channel.basic_publish(
        exchange='',
        routing_key=queue_name,
        body=json.dumps(envelope).encode(),
        properties=pika.BasicProperties(
            content_type='application/vnd.masstransit+json',
            delivery_mode=2  # Make message persistent
        )
    )
    print(f" [x] Sent: {envelope}")
    
    # Close the connection
    connection.close()
    
send_message()