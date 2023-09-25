
#Optimism-pessimism
arr = [[20, 20, 0 , 10],[10, 10, 10, 10], [0,40,0,0], [10, 30, 0, 0]]

dict = {}
for i in range(len(arr)):
     dict[i] = 0.75*max(arr[i]) +0.25*min(arr[i])

   
maxVal = max(dict, key=dict.get)    
print(f"Optimism-pessimism rule: Choose act {maxVal+1}")

#to compile: make sure python is installed, navigate to 
#path of file in cmd/shell and type "py Q3d.py"
#to run: (windows) py .\Q3d.py, linux/ubuntu: python3 Q3d.py 
