using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixOp : MonoBehaviour {

	/*void Start()
	{
		float[,] test = {{3f, 7f, 2f, 0f} , 
						{2f, 0f, -2f, 1f} ,
						{0f, 5f, 1f, -1f} ,
						{3f, 2f, 1f, -1f}};

//		float[,] test = {{3f, 3f, 2f} , 
//						{2f, 5f, -2f} ,
//						{-1f, 1f, 1f}};

		printMatrix (test);
		test =  Inverse(test);
		printMatrix (test);
	}*/


	public static float[,] Transpose(float[,] matrix)
	{
		int m = matrix.GetLength(0);
		int n = matrix.GetLength(1);

		float[,] TransposedMatrix = new float[n,m];

		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				TransposedMatrix[i,j] = matrix[j,i];
			}
		}

		return TransposedMatrix;
	}

	public static float[,] MatrixMul(float[,] matrix1, float[,] matrix2)
	{
		int m1 = matrix1.GetLength(0);
		int n1 = matrix1.GetLength(1);
		int m2 = matrix2.GetLength(0);
		int n2 = matrix2.GetLength(1);
		float[,] CombinedMatrix = new float[m1,n2];


		if (n1 != m2) {
			Debug.LogError ("Mismatch matrix sizes");
			Debug.Break ();
		}

		for (int i = 0; i < m1; i++) {
			for (int j = 0; j < n2; j++) {
				float sum = 0f;
				for (int k = 0; k < n1; k++) {
					sum += matrix1 [i, k] * matrix2 [k, j];
				}
				CombinedMatrix[i,j] = sum;
			}
		}

		return CombinedMatrix;
	}

	public static void printMatrix(float[,] matrix)
	{
		int m = matrix.GetLength(0);
		int n = matrix.GetLength(1);
		string line;
		string MainMatrix = "";

		for (int j = 0; j < m; j++) {
			line = "| ";
			for (int i = 0; i < n; i++) {
				line = line + " " + matrix [j, i];
			}
			MainMatrix = MainMatrix + line + " |\n";
		}
		Debug.Log (MainMatrix);
	}

	public static float[,] Inverse(float[,] matrix)
	{
		int n = matrix.GetLength(0);
		int m = matrix.GetLength(1);
		float[,] inMat = new float[n,m];

		float det = Determinant(matrix);
		Debug.Log("Determinant is " + det);
		if (det == 0f)
		{
			Debug.Log("Determinant is zero");
			return null;
		}
		det = 1f/det;

		inMat = AdjointMatrix(matrix);

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++) 
			{
				inMat[i,j] *= det;
			}
		}

		return inMat;
	}

	//use diagonal crosses
	/*static float Determinant (float[,] matrix)
	{
		float det = 0f;
		int n = matrix.GetLength(0);
		int m = matrix.GetLength(1);
//		Debug.Log("Inside of determinant ");
//		printMatrix(matrix);

		if (n == 2 && m == 2)
		{
			det = matrix[0,0] * matrix[1,1] - (matrix[0,1] * matrix[1,0]);
//			Debug.Log("output of determinant " + det);
			return det;
		}

		//Diagonal sum
		float subSum = 1f;
		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++) 
			{
				int normIndex = (i +j) % n;
				subSum *= matrix[normIndex, j];
			}
			det += subSum;
			subSum = 1f;
		}

		//reverse diagonal
		subSum = -1f;
		for (int i = n-1; i > -1; i--)
		{
			for (int j = 0; j < m; j++) 
			{
				int normIndex = mod(i - j, n);
				subSum *= matrix[normIndex, j];

			}
			det += subSum;
			subSum = -1f;
		}

//		Debug.Log("output of determinant " + det);
		return det;

	}*/

	static float Determinant (float[,] matrix)
	{
		int n = matrix.GetLength(0);
		int m = matrix.GetLength(1);
		float det = 0f;

		if (n == 1 && m == 1)	return matrix[0,0];

		for (int i = 0; i < n; i++)
		{
			if (i % 2 == 0)
			{
				det += matrix[i,0] * Determinant(SubMatrix(matrix, i,0));
			}
			else
			{
				det -= matrix[i,0] * Determinant(SubMatrix(matrix, i,0));
			}

		}

		return det;
	}

	//Test needed, Excludes are index
	static float[,] SubMatrix(float[,] matrix, int xExclude, int yExclude)
	{
		int n = matrix.GetLength(0);
		int m = matrix.GetLength(1);
		float[,] subMat = new float[n -1, m -1];

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++) 
			{
				int a = i;
				int b = j;
				if (a == xExclude || b == yExclude) continue;
				if (a > xExclude) a -= 1;
				if (b > yExclude) b -= 1;
					
				subMat[a, b] = matrix[i, j];
			}
		}

//		printMatrix(subMat);
		return subMat;
	}

	static float[,] AdjointMatrix (float[,] matrix)
	{
		int n = matrix.GetLength(0);
		int m = matrix.GetLength(1);
		float[,] adjMat = new float[n,m];

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++) 
			{
				adjMat[i,j] = Determinant(SubMatrix(matrix, i, j));

				//checkerboard pattern added
				if ((i + j) % 2 == 1) adjMat[i,j] *= -1;
			}
		}

//		printMatrix(adjMat);
		adjMat = Transpose(adjMat);
//		printMatrix(adjMat);
		return adjMat;
	}

	static int mod(int x, int m) {
    	return (x%m + m)%m;
	}

}
