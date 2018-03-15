using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixOp : MonoBehaviour {

//	void Start()
//	{
//		float[,] test = {{ 1f,2f,3f} , 
//						{4f,5f,6f}};
//
//		printMatrix (test);
//		test =  MatrixMul(test,Transpose (test));
//		printMatrix (test);
//	}


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
		Debug.Log("This matrix is " + m + "x" + n);
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

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++) 
			{
				if (i == j)
				{
					inMat[i , j] = matrix[n - i - 1, m - j - 1];	
				}
				else
				{
					inMat[i , j] = -1 * matrix[i , j];	
				}
			}
		}

		for (int i = 0; i < n; i++)
		{
			for (int j = 0; j < m; j++) 
			{
				inMat[i , j] = det * matrix[i , j];
			}
		}

		return inMat;
	}

	static float Determinant (float[,] matrix)
	{
		float det = 0f;
		int n = matrix.GetLength(0);
		int m = matrix.GetLength(1);

		if (n < 2 || m < 2)
		{
			Debug.LogError("Matrix is too small returning first element");
			return matrix[0,0];
		}
		else if (n == 2 && m == 2)
		{
			//Use standart determinant here
		}
		else
		{
			//Call recursion here
		}

		return det;

	}

	//Test needed
	static float[,] SubMatrix(float[,] matrix, int xBegin, int xEnd, int yBegin, int yEnd)
	{
		float[,] subMat = new float[xEnd - xBegin + 1, yEnd - yBegin + 1];

		for (int i = xBegin; i < xBegin + xEnd; i++)
		{
			for (int j = yBegin; j < yBegin + yEnd; j++) 
			{
				subMat[i - xBegin, j -yBegin] = matrix[i,j];
			}
		}


		return subMat;
	}
}
