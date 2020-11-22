from flask import Flask, request
import json

#import MEAL PICKER

app = Flask(__name__)

@app.route('/')
def find_meals():
    prefs = request.json
    #print(prefs)

    #MAKE PREDICTION BASED ON prefs and RETURN THEM
    return json.dumps({'1': 'example meal'})