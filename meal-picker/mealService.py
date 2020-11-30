from flask import Flask, request
import json

from mealSelector import MealSelector

app = Flask(__name__)

@app.route('/')
def find_meals():
    prefs = request.json
    selector = MealSelector()
    allergens, cusines, mealsAmmount = selector.parseResuest(prefs)
    data = selector.recommendMeals(prefs['allergenes'], prefs['cusines'], prefs['mealsAmmount'] if 'mealsAmmount' in prefs else None)
    return json.dumps(data)
