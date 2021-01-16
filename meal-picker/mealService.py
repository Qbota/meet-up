import pika
import json
import ast

from mealSelector import MealSelector

selector = MealSelector()
def find_meals(prefs):
    data = selector.recommendMeals(prefs['Allergens'],
                                   prefs['Cusines'],
                                   prefs['MealsAmount'])
    return json.dumps(data)


connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost'))
channel = connection.channel()
channel.queue_declare(queue='requestqueuemeals', durable=True)


def on_request(ch, method, props, body):
    prefs = ast.literal_eval(body.decode('utf-8'))
    print(prefs)
    response = find_meals(prefs)

    ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(correlation_id = \
                                                         props.correlation_id),
                     body=response)
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_qos(prefetch_count=1)
channel.basic_consume(queue='requestqueuemeals', on_message_callback=on_request)

print(" [x] Awaiting RPC requests")
channel.start_consuming()