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

	void printMatrix(float[,] matrix)
	{
		int m = matrix.GetLength(0);
		int n = matrix.GetLength(1);

		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				Debug.Log ("The " + j + "-" + i + " element is " + matrix [j, i]);
			}
		}
	}
}
