# This function should behave in a manner similar
# to the C# version.
#Requirements: This function must take in 3 args:
#1. a data structure where the syntax:
	# "structure[index]" modifies the value at that index  of elements of type x
#2. a function that modifies an element of type x
#3. a function that returns if an element of type x needs modifying
#This function modifies the original array
		
def transformIf(items, F, P):
	count = 0
	for item in items:
		if P(item):
			items[count] = F(item)
		count+=1
L1 = [-3, -2, -1, 0, 1, 2, 3]
transformIf(L1, lambda x : x * x, lambda x : x < 0)
print(L1)

L2 = ["hello", "world", "abc"]
transformIf(L2, lambda x : x[0:3], lambda x : len(x) > 3)
print(L2)



"""
		}
		//Requirements: This function must take in 3 args:
		//1. an array of elements of type x
		//2. a funciton that modifies an element of type x
		//3. a function that returns if an element of type x needs modifying
		//This function modifies the original array
		public static void  TransformIf<T>(T[] items, Func<T,T> y, Func<T,bool> f){
			int count = 0;
						
			foreach (T v in items) {
				if(f(v)){
					items[count] = y(v);
				}
			count++;
			}
		}
"""

