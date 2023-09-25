
#Maximin
arr = [[20, 20, 0 , 10],
       [10, 10, 10, 10],
       [0, 40, 0, 0], 
       [10, 30, 0, 0]]

dict = {}
for i in range(len(arr)):
    dict[i] = min(arr[i])

maxVal =max(dict, key=dict.get)
print(f"Maximin: Choose act {maxVal+1}")        


#to compile: make sure python is installed, navigate to 
#appropriate path in cmd/shell and type "py Q3a.py"
#to run: (windows) py .\Q3a.py, linux/ubuntu: python3 Q3a.py 
