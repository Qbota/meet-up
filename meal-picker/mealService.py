from flask import Flask, request
import json

from mealSelector import MealSelector

app = Flask(__name__)

@app.route('/recomendation')
def find_meals():
    prefs = request.json
    print(prefs)
    selector = MealSelector()
    data = selector.recommendMeals(prefs['Allergens'],
                                   prefs['Cusines'],
                                   prefs['MealsAmount'])
    return json.dumps(data)
