import json
import ast
import pika

import movieAI


ai = movieAI.MovieAI()
def make_predictions(predictions_set):
    predictions = ai.generateMovieRecommendation(predictions_set['Ratings'],
                                                 predictions_set['Genres'])
    if predictions is None:
        return json.dumps({})
    movieInfo = ai.getMovieDetails(predictions)
    return json.dumps(movieInfo)

connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost'))
channel = connection.channel()
channel.queue_declare(queue='requestqueuemovies', durable=True)


def on_request(ch, method, props, body):
    predictions_set = ast.literal_eval(body.decode('utf-8'))

    response = make_predictions(predictions_set)
    print('Sending response: {}'.format(response))
    ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(correlation_id = \
                                                         props.correlation_id),
                     body=response)
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_qos(prefetch_count=1)
channel.basic_consume(queue='requestqueuemovies', on_message_callback=on_request)

print(" [x] Awaiting RPC requests")
channel.start_consuming()

'''
def test():
    prediction_set = {"genres": {"Comedy": 2, "Adventure": 1},
                      "ratings": [{"3052": 4.5, "365": 5}]}
    ai = movieAI.MovieAI()
    predictions = ai.generateMovieRecommendation(prediction_set['ratings'],
                                                 prediction_set['genres'])
    if predictions is None:
        return Response(status=204)
    movieInfo = ai.getMovieDetails(predictions)
    print(movieInfo)


if __name__ == "__main__":
    test()
'''