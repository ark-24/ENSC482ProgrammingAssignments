#minimax regret
#pip install numpy
import numpy as np


arr = np.array([[20, 20, 0, 10],
       [10, 10, 10, 10], 
       [0, 40, 0, 0], 
       [10, 30, 0, 0]])

maxDict = {}

for i in range(len(arr)):
    maxDict[i] = max(arr.transpose()[i])

regretMat  = [[0] * 4 for i in range(4)]

for i in range(len(arr)):
    for j in range(len(arr)):
        regretMat[j][i] = maxDict[i] - arr[j][i]

regretDict = {}
for i in range(len(regretMat)):
    regretDict[i] = max(regretMat[i])



minVal = min(regretDict, key=regretDict.get)
print(f"Minimax Regret: Choose act {minVal+1}")        

#to compile: make sure python and numpy is installed, navigate to 
#appropriate path in cmd/shell and type "py Q3c.py"
#to run: (windows) py .\Q3c.py, linux/ubuntu: python3 Q3c.py 
