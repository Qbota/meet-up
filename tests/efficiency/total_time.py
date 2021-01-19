import asyncio
import aiohttp
import datetime
import json
from matplotlib import pyplot as plt
from time import sleep
from random import randint


class Shuffler:
    def __init__(self, id, value):
        self.id = id
        self.value = value


async def fetch(url, credentials):
    async with aiohttp.request('POST', url, json=credentials) as response:
        return await response.read()

async def main(requests):
    url = 'http://localhost:5000/api/meet-up/login'
    credentials = {"login":"test","password":"Test1234!"}

    tasks = []
    start_time = datetime.datetime.now()

    for i in range(requests):
        tasks.append(fetch(url, credentials))
    responses = await asyncio.gather(*tasks)
    end_time = datetime.datetime.now()
    result = end_time-start_time
    for res in responses:
        assert 'accessToken' in res.decode()
    avg = (result.microseconds + 1000000* result.seconds)/len(responses)
    avg = avg/1000
    print('In {}.{} AVG {} Handled {} requests'.format(result.seconds, result.microseconds, avg, requests))
    return avg


if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    x_results = []
    y_results = []
    my_range = range(400, 1301, 25)
    my_list = []
    for i in my_range:
        my_list.append(Shuffler(randint(0, 10000), i))
    my_list.sort(key= lambda x: x.id)
    REQUESTS = [shuffler.value for shuffler in my_list]
    print(REQUESTS)
    for num in REQUESTS:
        sleep(3)
        try:
            time = loop.run_until_complete(main(num))
            x_results.append(num)
            y_results.append(time)
        except:
            sleep(5)
            print('Error for {} requests'.format(num))
    plt.figure()
    plt.xlabel('Number of simultaneous requests')
    plt.ylabel('Average time to response [ms]')
    plt.plot(x_results, y_results, 'o')
    plt.show()
    import pdb;pdb.set_trace()
