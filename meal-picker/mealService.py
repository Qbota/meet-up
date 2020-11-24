from flask import Flask, request
import json

from mealSelector import MealSelector

app = Flask(__name__)

@app.route('/')
def find_meals():
    prefs = request.json
    print(prefs)
    selector = MealSelector()
    allergens, cusines, mealsAmmount = selector.parseResuest(prefs)
    data = selector.recommendMeals(allergens, cusines, mealsAmmount)
    return json.dumps(data)
