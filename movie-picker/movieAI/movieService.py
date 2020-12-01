from flask import Flask, request
import json

import movieAI

app = Flask(__name__)


@app.route('/')
def make_predictions():
    predictions_set = request.json
    ai = movieAI.MovieAI()
    predictions = ai.generateMovieRecommendation(predictions_set['ratings'],
                                                 predictions_set['config'])
    if predictions is None:
        return 404
    movieInfo = ai.getMovieDetails(predictions)
    return json.dumps(movieInfo)
