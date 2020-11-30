import requests
import json

recipes = []
RANGE = 10000
i = 0
MAX_COUNT = 500
counter = 0

while i < RANGE:
    try:
        response = requests.get("https://www.themealdb.com/api/json/v1/1/random.php")
        temp = json.loads(response.text)
        if temp not in recipes:
            recipes.append(temp)
            i += 1
            counter = 0
            print("{}/{}".format(i, RANGE))
        else:
            counter += 1
            if counter > MAX_COUNT:
                break
    except Exception as e:
        print(e)
        break

with open('meals.json', 'w+') as f:
    json.dump(json.dumps(recipes), f, indent=2)
