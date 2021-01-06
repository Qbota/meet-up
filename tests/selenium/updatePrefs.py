import pymongo
from time import sleep
from selenium import webdriver
from random import randint

MEETING_TITLE = 'Selenium test meeting'

myclient = pymongo.MongoClient('mongodb://localhost:27017/')
my_db = myclient['meet-up']
my_col = my_db['meet-up.users']

driver = webdriver.Chrome('C:\Program Files (x86)\chromedriver.exe')
driver.get('localhost:8080/login')

login_field = driver.find_element_by_id(101)
password_field = driver.find_element_by_id(102)
login_field.send_keys('prefsTest')
password_field.send_keys('Selenium!123')
login_button = driver.find_element_by_id(200)
login_button.click()
sleep(2)
account_button = driver.find_element_by_id(299)
account_button.click()
sleep(2)

new_rates = {
    1: randint(0, 9),
    19: randint(0, 9),
    39: randint(0, 9),
    1258: randint(0, 9),
    2571: randint(0, 9),
    3578: randint(0, 9),
    4262: randint(0, 9),
    40629: randint(0, 9),
    68157: randint(0, 9),
    92259: randint(0, 9)}

def set_pref(key, value):
    button = driver.find_element_by_id(key)
    button.click()
    rating = driver.find_element_by_class_name('v-rating')
    stars = rating.find_elements_by_class_name('v-icon')
    sleep(3)
    stars[value].click()
    confirm = driver.find_element_by_id(310)
    confirm.click()
    sleep(2)

for key, value in new_rates.items():
    set_pref(key, value)
save_button = driver.find_element_by_id(320)
save_button.click()

sleep(3)

user = my_col.find_one({'Name': 'prefsTest'})
ratings = set()
for rating in user['MoviePreference']['Ratings'].values():
    ratings.add(rating)
assert ratings != set([5.0]), 'Ratings were not changed in database!'

for rating in user['MoviePreference']['Ratings'].keys():
    user['MoviePreference']['Ratings'][rating] = 5.0
my_col.update_one({'Name': 'prefsTest'}, {'$set': {'MoviePreference': user['MoviePreference']}})
print('TEST PASSED')
