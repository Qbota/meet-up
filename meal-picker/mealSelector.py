from pymongo import MongoClient
from alergenDict import ALERGEN_DICT
from random import shuffle


DATABASE = "mealsDatabase"
COLLECTION = "mealsCollection"
ALLERGEN_FREE_COLLECTION = "allergenFreeMealsCollection"
FIRST_INGREDIENT = 1
LAST_INGREDIENT = 21


class MealSelector():

    def __init__(self):
        self.myClient = MongoClient('mongodb://localhost:27017/')
        self.myDb = self.myClient["mealsDatabase"]
        self.myCol = self.myDb["mealsCollection"]

    def _displayAllCategories(self):
        categories = []
        print("*** Categories ***")
        for item in self.myCol.find():
            if item['strCategory'] not in categories:
                categories.append(item['strCategory'])
                print(item['strCategory'])
        print()

    def _displayAllAreas(self):
        areas = []
        print("*** Areas ***")
        for item in self.myCol.find():
            if item['strArea'] not in areas:
                areas.append(item['strArea'])
                print(item['strArea'])
        self.areas = areas
        print()

    def _displayAllTags(self):
        tags = []
        print("*** Tags ***")
        for item in self.myCol.find():
            _tags = item['strTags']
            try:
                _tags = _tags.split(",")
                for tag in _tags:
                    if tag not in tags:
                        tags.append(tag)
                        print(tag)
            except (ValueError, AttributeError):
                continue
        print()

    def _displayAllIngredients(self):
        ingredients = []
        print("*** Ingredients ***")
        for item in self.myCol.find():
            for i in range(FIRST_INGREDIENT, LAST_INGREDIENT):
                ingredient = item['strIngredient{}'.format(i)]
                if ingredient not in ingredients and \
                        ingredient is not None and ingredient != "":
                    ingredients.append(ingredient)
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
        for item in self.noAllergenCol.find():
            if any(cusine in item['strArea'] for cusine in cusines):
                mealsList.append(item)
        if len(mealsList) < mealsAmmount:
            print("Could not find that many meals: {}".format(mealsAmmount))
            mealsAmmount = len(mealsList)
        shuffle(mealsList)
        return mealsList[0:mealsAmmount]

    def recommendMeals(self, allergens, cusines, mealsAmmount):
        self._createAllergenFreeTempCollection(allergens)
        meals = self.recommendMeals(cusines, mealsAmmount)
        mealsDict = {str(i): meals[i] for i in range(0, len(meals))}
        return mealsDict

    def parseResuest(self, prefs):
        try:
            prefs.split(',')
        except Exception as e:
            print("Exception {} occured".format(e))
            self._displayAllAreas()
            return ([], self.areas, 5)


if __name__ == "__main__":
    selector = MealSelector()
    selector._displayAllCategories()
    selector._displayAllAreas()
    selector._displayAllTags()
    selector._displayAllIngredients()
    selector._createAllergenFreeTempCollection(['meat', 'dairy', 'eggs'])
    meals = selector._recommendMeals(["british", "chinese", "polish"], 5)
    for meal in meals:
        print(meal['strMeal'])