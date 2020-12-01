from flask import Flask, request, Response
import json

import movieAI

app = Flask(__name__)


@app.route('/predict/')
def make_predictions():
    predictions_set = request.json
    ai = movieAI.MovieAI()
    predictions = ai.generateMovieRecommendation(predictions_set['ratings'],
                                                 predictions_set['genres'])
    if predictions is None:
        return Response(status=204)
    movieInfo = ai.getMovieDetails(predictions)
    print(movieInfo)
    return json.dumps(movieInfo)


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