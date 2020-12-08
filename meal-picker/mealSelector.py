from pymongo import MongoClient
from alergenDict import ALERGEN_DICT
from random import shuffle
from random import random

DATABASE = "mealsDatabase"
COLLECTION = "mealsCollection"
ALLERGEN_FREE_COLLECTION = "allergenFreeMealsCollection"
FIRST_INGREDIENT = 1
LAST_INGREDIENT = 21
DEF_MEAL_AMMOUNT = 10


class MealSelector():

    def __init__(self):
        self.myClient = MongoClient('mongodb://localhost:27017/')
        self.myDb = self.myClient["mealsDatabase"]
        self.myCol = self.myDb["mealsCollection"]

    def _displayAllCategories(self):
        categories = set()
        print("*** Categories ***")
        for item in self.myCol.find():
            categories.add(item['strCategory'])
            print(item['strCategory'])
        print()

    def _displayAllAreas(self):
        areas = set()
        print("*** Areas ***")
        for item in self.myCol.find():
            areas.add(item['strArea'])
            print(item['strArea'])
        self.areas = areas
        print()

    def _displayAllTags(self):
        tags = set()
        print("*** Tags ***")
        for item in self.myCol.find():
            _tags = item['strTags']
            try:
                _tags = _tags.split(",")
                for tag in _tags:
                    tags.add(tag)
                    print(tag)
            except (ValueError, AttributeError):
                continue
        print()

    def _displayAllIngredients(self):
        ingredients = set()
        print("*** Ingredients ***")
        for item in self.myCol.find():
            for i in range(FIRST_INGREDIENT, LAST_INGREDIENT):
                ingredient = item['strIngredient{}'.format(i)]
                if ingredient:
                    ingredients.add(ingredient)
                    print(ingredient)
        self.ingredients = ingredient
        print()

    def _createAllergenFreeTempCollection(self, allergens):
        colList = self.myDb.list_collection_names()
        if ALLERGEN_FREE_COLLECTION in colList:
            self.myDb[ALLERGEN_FREE_COLLECTION].drop()
        self.noAllergenCol = self.myDb[ALLERGEN_FREE_COLLECTION]
        for item in self.myCol.find():
            reject = False
            for key, value in ALERGEN_DICT.items():
                if key in allergens:
                    for i in range(FIRST_INGREDIENT, LAST_INGREDIENT):
                        ingredient = item['strIngredient{}'.format(i)]
                        if ingredient in ALERGEN_DICT[key]:
                            reject = True
            if not reject:
                self.noAllergenCol.insert_one(item)

    def _recommendMeals(self, cusines, mealsAmmount):
        mealsList = []
        if mealsAmmount is None:
            mealsAmmount = DEF_MEAL_AMMOUNT
        for item in self.noAllergenCol.find():
            if any(cusine.lower() in item['strArea'].lower()
                    for cusine in cusines.keys()):
                mealsList.append(item)
        if len(mealsList) < mealsAmmount:
            print("Could not find that many meals: {}".format(mealsAmmount))
            while len(mealsList) < mealsAmmount:
                for item in self.noAllergenCol.find():
                    if random() < 0.1:
                        mealsList.append(item)
                        break
        shuffle(mealsList)
        for i in range(0, len(mealsList)):
            mealsList[i].pop('_id', None)

        return mealsList[0:mealsAmmount]

    def recommendMeals(self, allergens, cusines, mealsAmmount):
        self._createAllergenFreeTempCollection(allergens)
        meals = self._recommendMeals(cusines, mealsAmmount)
        meals.sort(key=lambda meal: cusines[meal['strArea']])
        return meals


if __name__ == "__main__":
    selector = MealSelector()
    selector._displayAllCategories()
    selector._displayAllAreas()
    selector._displayAllTags()
    selector._displayAllIngredients()
    selector._createAllergenFreeTempCollection(['meat', 'dairy', 'eggs'])
    meals = selector._recommendMeals({"british":1, "chinese":2, "polish":1}, 5)
    for meal in meals:
        print(meal['strMeal'])