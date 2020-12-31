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
    start = datetime.datetime.now()
    async with aiohttp.request('POST', url, json=credentials) as response:
        result = await response.read()
        delta = datetime.datetime.now() - start
        assert 'accessToken' in result.decode()
        return delta.microseconds/1000


async def main(requests):
    url = 'http://localhost:5000/api/meet-up/login'
    credentials = {"login":"test","password":"Test1234!"}

    tasks = []
    start_time = datetime.datetime.now()

    for i in range(requests):
        tasks.append(fetch(url, credentials))
    responses = await asyncio.gather(*tasks)
    return sum(responses)/len(responses)


if __name__ == '__main__':
    loop = asyncio.get_event_loop()
    x_results, y_results = [], []
    requests_range = range(400, 1301, 25)

    shufflers_list = []
    for i in requests_range:
        shufflers_list.append(Shuffler(randint(0, 10000), i))
    shufflers_list.sort(key= lambda x: x.id)

    requests_order = [shuffler.value for shuffler in shufflers_list]
    print('Requests order:', requests_order)
    for num in requests_order:
        try:
            time = loop.run_until_complete(main(num))
            x_results.append(num)
            y_results.append(time)
            print('Handled {} requests AVG {}'.format(num, time))
        except Exception as err:
            sleep(5)
            print('Error for {} requests\n'.format(num), err)
        else:
            sleep(3)

    plt.figure()
    plt.xlabel('Number of simultaneous requests')
    plt.ylabel('Average time to response [ms]')
    plt.plot(x_results, y_results, 'o')
    plt.show()
    import pdb;pdb.set_trace()
