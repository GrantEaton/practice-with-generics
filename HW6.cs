using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6 {
	class Generics {
		public struct Point : IComparable {
			public int x, y;
			public Point(int X, int Y) {
				x = X;
				y = Y;
			}
			int IComparable.CompareTo(Object other) {
				Point pt = (Point)other;
				if (x != pt.x) {
					return x - pt.x;
				} else {
					return y - pt.y;
				}
			}
			public override int GetHashCode() {
				return x << 8 | y;
			}
			public override bool Equals(object obj) {
				Point other = (Point)obj;
				return x == other.x && y == other.y;
			}
			public override string ToString() {
				return String.Format("(%d, %d)", x, y);
			}
		}
		public static void Display<T>(IEnumerable<T> items) {
			foreach (T item in items) {
				Console.Write(item + " ");
			}
			Console.WriteLine();
		}
		public static bool NonGenericContains(IEnumerable<int> items, int value) {
			bool found = false;
			foreach (int v in items) {
				if (v == value) {
					found = true;
					break;
				}
			}
			return found;
		}
		public static bool NonGenericContains(IEnumerable<string> items, string value) {
			bool found = false;
			foreach (string v in items) {
				if (v.Equals(value)) {
					found = true;
					break;
				}
			}
			return found;
		}
		public static bool NonGenericContains(IEnumerable<Point> items, Point value) {
			bool found = false;
			foreach (Point v in items) {
				if (v.Equals(value)) {
					found = true;
					break;
				}
			}
			return found;
		}

		//Contains method takes in two arguments, a list of elements and a value element. 
		//The list must contain the same type of elements as the value
		//If the element is a class, that class must have a .CompareTo method for this function to work.
		// The compareTo method must return 0 if two elements are equal.
		public static bool Contains<T>(IEnumerable<T> items, T value) {

			bool found = false;
			var valComp = value as IComparable;
			foreach (T v in items) {
				var vComp = v as IComparable;

				//Console.WriteLine(valComp.CompareTo(vComp));
				if (valComp.CompareTo(vComp)==0) {
					found = true;
					break;
				}
			}
			return found;
		}
		//IsSorted method takes in one argument, a list of elements. 
		//If the element is a class, that class must have a .CompareTo method for this function to work.
		// The compareTo method must return a number > 0 for  x>y in the statement: x.compareTo(y).
		// This implementation assumes that elements that are equal and next to one another are in sorted order
		public static bool IsSorted<T>(IEnumerable<T> items) {

			var last = items.First();
			var lastComp = last as IComparable;
			foreach (T v in items) {
				
				var vComp = v as IComparable;
				//console.writeline("comparing "+lastcomp+ "to " +vcomp);
				//console.writeline(lastcomp.compareto(vcomp));

				//console.writeline(valcomp.compareto(vcomp));
				if (lastComp.CompareTo(vComp)>0) {
					return false;
				}
				lastComp = vComp;
			}
			return true;
		}
		
		//Requirements: CountIf takes two args: a list of items and a function that returns a 0 or a 1
		//the function must accept exactly one argument, which is the same type as the elements in the list
		public static int CountIf<T>(IEnumerable<T> items, Func<T,bool> f){
			int count = 0;

			foreach (T v in items) {
				if(f(v)){
					count++;
				}
			}
			return count;

		}

		//Requirements: Filer takes two args: a list of items and a function that returns a 0 or a 1
		//the function must accept exactly one argument, which is the same type as the elements in the list
		//The function will return a generic List of the same type as the elements in the list
		public static List<T> Filter<T>(IEnumerable<T> items, Func<T,bool> f){
			List<T> list = new List<T>();

			foreach (T v in items) {
				if(f(v)){
					list.Add(v);
				}
			}
			return list;
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

		static void Main(string[] args) {
			String[] strings1 = new String[] { "a", "b", "ac" };
			String[] strings2 = new String[] { "abc", "34b", "ABDac" };
			HashSet<string> strings3 = new HashSet<string>();
			strings3.Add("ABC");
			strings3.Add("abc");
			strings3.Add("wxyz");
			strings3.Add("wxyz");
									

			int[] ints1 = new int[] { 1, 2, 3, 4, -5 };
			int[] ints2 = new int[] { 1, 2, 3, 4, -5 };

			Point[] pts1 = new Point[] { new Point(2, 3), new Point(4, 3), new Point(4, 9) };

			Console.WriteLine("------------NonGenericContains------------");
			Display(pts1);
			Console.WriteLine(NonGenericContains(strings1, "ac"));
			Console.WriteLine(NonGenericContains(ints1, 4));
			Console.WriteLine(NonGenericContains(ints1, 42));
			Console.WriteLine(NonGenericContains(pts1, new Point(2, 3)));
			Console.WriteLine(NonGenericContains(pts1, new Point(0, 0)));

			//Your code additions above should allow the following code to run properly

			// Contains should be a generic function that works for enumerable collections of int,
			// double, float, short, objects, etc. The second parameter should be a value whose
			// type is the same as the objects in the collection. The function should return
			// true iff the value is in the collection.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------Contains------------");
			Console.WriteLine(Contains(strings1, "ac"));
			Console.WriteLine(Contains(ints1, 4));
			Console.WriteLine(Contains(ints1, 42));
			Console.WriteLine(Contains(pts1, new Point(2, 3)));
			Console.WriteLine(Contains(pts1, new Point(0, 0)));

			// IsSorted should be a generic function that works for arrays of int, double, float, short,
			// objects, etc. It should return true if each array element is >= to its predecessor.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------IsSorted------------");
			Console.WriteLine(IsSorted(strings1));
			Console.WriteLine(IsSorted(ints1));
			Console.WriteLine(IsSorted(pts1));
						
			// CountIf should be a generic function that works for enumerable collections of int,
			// double, float, short, objects, etc. The second parameter should be a predicate (i.e.,
			// a function that return true/false) that accepts a single parameter whose type is 
			// the same as the items in the collection. This function returns the number of items
			// in the collection that satisfy the predicate.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------CountIf------------");
			Console.WriteLine(CountIf(strings1, x => x[0] == 'a'));
			Console.WriteLine(CountIf(ints1, x => x % 2 == 0));
			Console.WriteLine(CountIf(pts1, pt => pt.x == 0 && pt.y == 0));

			// Filter should be a generic function that works for enumerable collections of int,
			// double, float, short, objects, etc. The second parameter should be a predicate (i.e.,
			// a function that return true/false) that accepts a single parameter whose type is 
			// the same as the items in the collection. This function returns a List containing
			// the items that satsify the predicate.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------Filter------------");
			Display(Filter(ints1, x => x % 2 == 0));		// get even values
			Display(Filter(ints2, x => x % 2 == 0));		// get even values
			Display(Filter(strings2, x => x.Length > 2));	// get strings with more than 2 chars
			Display(Filter(strings1, x => x.Length > 2));

			
			/***************************
			// A common operation is to combine two vectors having the same length. For example,
			// the "dot product" between two vectors is defined be: 1. multiply the corresponding
			// pairs of array elements 2. add up all the products produced in step 1.
			// For example: The dot product of [1,2,3] and [3,2,5] is 1*3 + 2*2 + 3*5 = 22
			//
			// This type of operation can be generalized by viewing it as having two replaceable functions.
			// One function is used to combine the corresponding array elements (i.e., *).
			// The other function is then used to combine those individual results.
			//
			// SumProduct should be a generic function that works for arrays of int, double, float, short,
			// objects, etc. It should return a value having the same type as the array elements.
			// The first two arguments of the function are the two vectors. The third parameter is the 
			// operation used to combine the individual results. The fourth parameters is applied to the
			// corresponding array elements.
			//
			// This function should throw an exception if the arrays are not suitable for this operation.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------SumProduct------------");
			Console.WriteLine(SumProduct(ints1, ints2, (x, y) => x + y, (x, y) => x * y));		// Typical dot product
			Console.WriteLine(SumProduct(ints1, ints2, (x, y) => x + y, Math.Max));
			Console.WriteLine(SumProduct(strings1, strings2, (x, y) => x + y, (x, y) => x.CompareTo(y) <= 0 ? x : y));
			******************************/

			// TransformIf should be a generic function that works for an array of int,
			// double, float, short, objects, etc. The second parameter should be function
			// to be applied to certain, individual, elements of the array. For those array
			// elements that satisfy the predicate, that array position should be replaced
			// with the result of the function applied to that element.
			// In a comment, identify any requirements that a client would need to know.
			Console.WriteLine("------------TransformIf------------");
			Display(ints2);
			TransformIf(ints2, x => 0, x => x < 0);			// replace negative elements with 0
			Display(ints2);
			Display(strings2);
			TransformIf(strings2, x => x.Substring(0, 2), x => x.Length > 2);	// Truncate long strings
			Display(strings2);
		}
	}
}
