
#Insufficient reason
arr = [[20, 20, 0 , 10],[10, 10, 10, 10], [0,40,0,0], [10, 30, 0, 0]]

dict = {}
for i in range(len(arr)):
     dict[i] = 0.25*sum(arr[i])

maxVal = max(dict, key=dict.get)    
print(f"Insufficient reason principle: Choose act {maxVal+1}")

#to compile: make sure python is installed, navigate to 
#path of file in cmd/shell and type "py Q3d.py"
#to run: (windows) py .\Q3e.py, linux/ubuntu: python3 Q3e.py 
