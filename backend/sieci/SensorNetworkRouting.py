import numpy as np
from scipy.optimize import minimize
import math
#inputs
V  = 18 #nodes without source
E = 6 #links
vs = 0# source
vq = 18 #destination
W  = list(range(V))# set o labeled nodes
L = np.zeros(V)
M = np.zeros(V)
Previous = np.zeros(V) #-1 <-- undefined
Neighbour = np.zeros(V)

def initializing():
    L[vq] = 0
    M[vq] = 1
    for vi in range(V-1):
        L[vi] = 12222222222222222
        Previous[vi] = -1
        M[vi] = 1
    return 0
def main():
    while not W.__contains__(vs) and Neighbour(W) not None:
