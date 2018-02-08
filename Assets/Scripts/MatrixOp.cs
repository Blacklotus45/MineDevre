using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixOp : MonoBehaviour {

	/*void Start()
	{
		float[,] test = {{ 1f,2f,3f} , 
						{4f,5f,6f}};

		printMatrix (test);
		test = Transpose (test);
		printMatrix (test);
	}*/


	public static float[,] Transpose(float[,] matrix)
	{
		int m = matrix.GetLength(0);
		int n = matrix.GetLength(1);

		Debug.Log ("M is " + m + "\nN is " + n);

		float[,] TransposedMatrix = new float[n,m];

		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				TransposedMatrix[i,j] = matrix[j,i];
			}
		}

		return TransposedMatrix;
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
