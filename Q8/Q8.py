from collections import defaultdict

# Input the payoff matrix of the 2x2 game
arr = [[2, 1], [0, 0], [0, 0], [1, 2]]

# Dictionaries to store the player 1 and player 2 best responses
dict1 = defaultdict(int)
dict2 = defaultdict(int)

# Lookup table for strategies
lookup = {0: "AA", 1: "AB", 2: "BA", 3: "BB"}

# Check for the best responses of player 1
for i in range(len(arr)-2):
    if arr[i][0] > arr[len(arr)-2+i][0]:
        dict1[arr[i][0]] = i
    else:
        dict1[arr[len(arr)-2+i][0]] = len(arr)-2+i

# Check for the best responses of player 2
for i in range(len(arr)-2):
    if arr[i][1] > arr[len(arr)-2+i][1]:
        dict2[arr[i][1]] = i
    else:
        dict2[arr[len(arr)-2+i][1]] = len(arr)-2+i

# Check if there is a pure Nash Equilibrium
# Compare the best responses for each player
if list(dict1.values())[0] == list(dict2.values())[0]:
    print(f"Pure Nash Equilibrium: {lookup[dict1[0]]}")
else:
    print("No pure Nash Equilibrium for this strategy profile.")

# Check for the second strategy profile
if list(dict1.values())[1] == list(dict2.values())[1]:
    print(f"Pure Nash Equilibrium: {lookup[dict1[1]]}")
else:
    print("No pure Nash Equilibrium for the second strategy profile.")

#to run: py .\Q8.py