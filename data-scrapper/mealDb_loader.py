import requests
import pymongo

from meal import Meal

LOADING_MEALS_NUMBER = 100
myclient = pymongo.MongoClient('mongodb://localhost:27017/')
my_db = myclient['test']
my_col = my_db['Meals']


def parse_ingredients(body):
    ingredients = []
    for i in range(1,21):
        ingredient = body['strIngredient{}'.format(i)]
        if ingredient == '':
            break
        else:
            ingredients.append(ingredient)
    return ingredients

if __name__ == '__main__':
    for i in range(LOADING_MEALS_NUMBER):
        response = requests.get('https://www.themealdb.com/api/json/v1/1/random.php')
        body = response.json()['meals'][0]

        meal = Meal(
            name = body['strMeal'],
            cuisine = body['strArea'],
            category = body['strCategory'],
            ingredients = parse_ingredients(body)
        )
        print(meal)
        if not my_col.find_one({'name': meal.name}):
            my_col.insert_one(meal.to_dict())
