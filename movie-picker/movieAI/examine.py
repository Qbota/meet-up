import numpy as np
import matplotlib.pyplot as pyplot

with open("fast.txt", 'r') as f:
    fast = f.read()

with open("slow.txt", 'r') as s:
    slow = s.read()

print(fast)
print(slow)

fast = [int(_) for _ in fast.split(',')]
slow = [int(_) for _ in slow.split(',')]
print("fast average:", np.average(fast), "fast std:", np.std(fast))
print("slow average:", np.average(slow), "slow std:", np.std(slow))

bins = np.linspace(min(fast + slow), max(fast+slow), 20)
pyplot.style.use('seaborn-deep')
pyplot.hist([fast, slow], bins, alpha=0.5, label=['initcwnd = initrwnd = 20', 'initcwnd = initrwnd = 3'])
pyplot.legend(loc='upper right')
pyplot.title("Histogram czasu pobierania informacji dla 100 filmów")
pyplot.xlabel("Czas pobierania [s]")
pyplot.ylabel("Ilość wystąpień")
pyplot.show()