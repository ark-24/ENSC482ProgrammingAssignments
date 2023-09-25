import numpy as np #pip install numpy
from sympy import symbols, Eq, solve #pip install sympy

# Define the payoff matrix (game matrix)
arr = np.array([[10, 16], [14, 24], [15, 20], [6, 12]])

# Define the symbols for the mixed strategies
sigmaA, sigmaB = symbols('sigmaA sigmaB')
sigmaC, sigmaD = symbols('sigmaC sigmaD')

# Define the expected utility equations for player C
EUc = Eq(arr[0][1] * sigmaA + arr[2][1] * sigmaB, arr[1][1] * sigmaA + arr[3][1] * sigmaB)
EUd = Eq(1 - sigmaA, sigmaB)

# Define the expected utility equations for player A
EUa = Eq(arr[0][0] * sigmaC + arr[1][0] * sigmaD, arr[2][0] * sigmaC + arr[3][0] * sigmaD)
EUb = Eq(1 - sigmaC, sigmaD)

# Solve the system of equations for player C
sol_dict = solve((EUc, EUd), (sigmaA, sigmaB))
# Solve the system of equations for player A
sol_dict2 = solve((EUa, EUb), (sigmaC, sigmaD))

# Print the results 
print(f'sigmaA = {sol_dict[sigmaA]}')
print(f'sigmaB = {sol_dict[sigmaB]}')

# Print the results 
print(f'sigmaC = {sol_dict2[sigmaC]}')
print(f'sigmaD = {sol_dict2[sigmaD]}')

#to run: py .\Q9.py