import generateMovieDataSet
import os

LOOP = 2
fast = ""
slow = ""
links = generateMovieDataSet.generateLinks()
for i in range(0, LOOP):
    os.system("ip route | while read p; do ip route change $p initcwnd 20 initrwnd 20; done")
    _, seconds = generateMovieDataSet.combineDataset(links)
    fast += str(seconds) + ","
    os.system("ip route | while read p; do ip route change $p initcwnd 3 initrwnd 3; done")
    _, seconds = generateMovieDataSet.combineDataset(links)
    slow += str(seconds) + ","

with open("fast.txt","a+") as f:
    f.write(fast)

with open("slow.txt","a+") as s:
    s.write(slow)
