from flask import Flask, request
import json

import movieAI

app = Flask(__name__)

@app.route('/')
def make_predictions():
    predictions_set = request.json
    ai = movieAI.MovieAI()
    res, predictions = ai.predictRatingForUnseenMovies(predictions_set['ratings'], predictions_set['config'])
    return json.dumps(predictions)