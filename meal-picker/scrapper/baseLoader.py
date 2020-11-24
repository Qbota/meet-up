import json
from pymongo import MongoClient
import os

os.chdir(os.path.dirname(os.path.abspath(__file__)))
f = open('meals.json', 'r')
j = json.load(f)
data = json.loads(j)
myClient = MongoClient('mongodb://localhost:27017/')
myDb = myClient["mealsDatabase"]
myCol = myDb["mealsCollection"]
myCol.remove()
for entry in data:
    _entry = {}
    for k, v in entry['meals'][0].items():
        _entry[k] = str(v).lower()
    myCol.insert_one(_entry)
