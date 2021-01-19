import pymongo
from time import sleep
from datetime import datetime
from selenium import webdriver

MEETING_TITLE = 'Selenium test group'

myclient = pymongo.MongoClient('mongodb://localhost:27017/')
my_db = myclient['meet-up']
my_col = my_db['meet-up.groups']

driver = webdriver.Chrome(r'C:\Users\gardz\AppData\Local\Programs\Python\Python38\Scripts\chromedriver.exe')
driver.get('localhost:8080/login')

login_field = driver.find_element_by_id(101)
password_field = driver.find_element_by_id(102)
login_field.send_keys('SeleniumTest')
password_field.send_keys('Selenium!123')
login_button = driver.find_element_by_id(200)
login_button.click()
sleep(2)
meeting_button = driver.find_element_by_id('groupsButton')
meeting_button.click()
sleep(2)

count = driver.find_elements_by_class_name('pl-3')
create_meeting = driver.find_element_by_id('enterGroupCreation')
create_meeting.click()
meeting_title = driver.find_element_by_id('groupName')
meeting_title.send_keys('thisIsSelenium')
meeting_description = driver.find_element_by_id('groupDescription')
meeting_description.send_keys('Group by selenium')
icon = driver.find_element_by_id('fas fa-bicycle')
icon.click()
choose_members = driver.find_element_by_class_name('v-select__selections')
choose_members.click()
sleep(1)
members = driver.find_elements_by_class_name("v-list-item__title")
members[10].click()
members[12].click()
members[20].click()
members[19].click()
sleep(1)
submit_group = driver.find_element_by_id("createGroup")
start_time = datetime.now()
submit_group.click()
sleep(2)
new_count = driver.find_elements_by_class_name('pl-3')
assert len(new_count) > len(count)
delta_time = datetime.now() - start_time



print('TEST PASSED')