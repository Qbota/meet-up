from flask import Flask, request
import json

from mealSelector import MealSelector

app = Flask(__name__)

@app.route('/')
def find_meals():
    prefs = request.json
    print(prefs)
    selector = MealSelector()
    data = selector.recommendMeals(prefs['allergenes'],
                                   prefs['cusines'],
                                   prefs['mealsAmmount'])
    return json.dumps(data)
