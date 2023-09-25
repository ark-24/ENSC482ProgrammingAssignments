import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from scipy.stats import norm
#pip install pandas, numpy, matplotlib
#python -m pip install scipy

cereal = pd.read_csv('cereal.csv')

#get calories and initialize histogram
calories = cereal["calories"]
plt.figure(figsize=(8, 4))
plt.hist(calories,bins=10, density=True, alpha=0.6, color='b')

#calculate variables for calories
muCal, stdCal = norm.fit(calories) 
xminCal, xmaxCal = plt.xlim()
xCal = np.linspace(xminCal, xmaxCal, 100)
pCal = norm.pdf(xCal, muCal, stdCal)
# x = np.random.normal(mu, math.sqrt(), 250)
# calories.plot(kind='hist', density=True)

#plot
plt.plot(xCal, pCal, 'k', linewidth=2)
title = "Cereal Calories"
plt.title(title)

#for sodium (extra)
# sodium = cereal["sodium"]
# plt.figure(figsize=(8, 4))
# plt.hist(sodium,bins=10, density=True, alpha=0.6, color='b')

# muNa, stdNa = norm.fit(sodium) 
# xminNa, xmaxNa = plt.xlim()
# xNa = np.linspace(xminNa, xmaxNa, 100)
# pNa = norm.pdf(xNa, muNa, stdNa)
# # x = np.random.normal(mu, math.sqrt(), 250)
# # calories.plot(kind='hist', density=True)

# plt.plot(xNa, pNa, 'k', linewidth=2)
# titleNa = "Cereal Sodium"
# plt.title(titleNa)

#get rating and initialize histogram
rating = cereal["rating"]
plt.figure(figsize=(8, 4))
plt.hist(rating,bins=10, density=True, alpha=0.6, color='b')

#calculate variables for rating
muRat, stdRat = norm.fit(rating) 
xminRat, xmaxRat = plt.xlim()
xRat = np.linspace(xminRat, xmaxRat, 100)
pRat = norm.pdf(xRat, muRat, stdRat)
# x = np.random.normal(mu, math.sqrt(), 250)
# calories.plot(kind='hist', density=True)

#plot
plt.plot(xRat, pRat, 'k', linewidth=2)
titleRat = "Cereal Rating"
plt.title(titleRat)
hist_calories, _ = np.histogram(calories, density=True)
hist_rating, _ = np.histogram(rating, density=True)

#get bhattacharya coeff
b_coeff = np.sum(np.sqrt(hist_calories * hist_rating))
print(f"bhattacharya coeff: {b_coeff}")
plt.show()

#to run: (windows) py .\Q5.py, linux/ubuntu: python3 Q5.py 