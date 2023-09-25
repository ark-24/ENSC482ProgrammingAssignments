
#Maximax
arr = [[20, 20, 0 , 10],[10, 10, 10, 10], [0,40,0,0], [10, 30, 0, 0]]

dict = {}
for i in range(len(arr)):
    maxNum = max(arr[i])
    dict[i] = maxNum

maxVal = max(dict, key=dict.get)
print(f"Maximin: Choose act {maxVal+1}")        


#to compile: make sure python is installed, navigate to 
#appropriate path in cmd/shell and type "py Q3b.py"
#to run: (windows) py .\Q3b.py, linux/ubuntu: python3 Q3b.py 
