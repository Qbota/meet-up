import numpy as np

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
