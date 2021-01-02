import pymongo
from time import sleep
from datetime import datetime
from selenium import webdriver

MEETING_TITLE = 'Selenium test meeting'

myclient = pymongo.MongoClient('mongodb://localhost:27017/')
my_db = myclient['meet-up']
my_col = my_db['meet-up.meetings']

driver = webdriver.Chrome('C:\Program Files (x86)\chromedriver.exe')
driver.get('localhost:8080/login')

login_field = driver.find_element_by_id(101)
password_field = driver.find_element_by_id(102)
login_field.send_keys('SeleniumTest')
password_field.send_keys('Selenium!123')
login_button = driver.find_element_by_id(200)
login_button.click()
sleep(2)
meeting_button = driver.find_element_by_id(300)
meeting_button.click()
sleep(2)

results = []
for i in range(100):
    create_meeting = driver.find_element_by_id(301)
    create_meeting.click()
    meeting_title = driver.find_element_by_id(302)
    meeting_title.send_keys(MEETING_TITLE)
    meeting_description = driver.find_element_by_id(303)
    meeting_description.send_keys('Meeting description')
    choose_group = driver.find_element_by_id(304)
    choose_group.click()
    sleep(1)
    group = driver.find_element_by_class_name('v-list-item__content')
    group.click()
    submit_meeting = driver.find_element_by_id(305)

    start_time = datetime.now()
    submit_meeting.click()
    RELOADED = False
    while not RELOADED:
        try:
            meeting = driver.find_element_by_class_name('pl-1')
            RELOADED = True
        except:
            sleep(0.0001)
    assert meeting.text == MEETING_TITLE
    delta_time = datetime.now() - start_time
    results.append(delta_time)
    my_col.delete_one({'Title': MEETING_TITLE})
    sleep(1)
    account_button = driver.find_element_by_id(299)
    account_button.click()
    sleep(1)
    meeting_button.click()
    sleep(1.5)

    meeting_deleted = False
    try:
        driver.find_element_by_class_name('pl-1')
    except:
        meeting_deleted = True
    assert meeting_deleted, 'Meeting is stil in database'

with open('results.csv', 'w') as csvfile:
    for res in results:
        csvfile.write('{},{}\n'.format(res.seconds, res.microseconds))