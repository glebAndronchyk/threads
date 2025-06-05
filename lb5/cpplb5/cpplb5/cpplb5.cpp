#include<iostream>
#include"omp.h"
#include <vector>
#include <tuple>
#include <random>

using namespace std;

long long async_aggregate(vector<vector<int>>& arr, int);
tuple<long long, int> async_get_min_row_sum(vector<vector<int>>& arr, int);

int main() {
	omp_set_nested(1);
	random_device rnd;
	mt19937 gen(rnd());

	int rows = 20000;
	int cols = 20000;
	int minVal = 1;
	int maxVal = 20;
	int sections = 10;

	uniform_int_distribution<> distribution(minVal, maxVal);
	vector<vector<int>> array2D(rows, vector<int>(cols));

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			array2D[i][j] = distribution(gen);
		}
	}

	#pragma omp parallel sections
	{
		#pragma omp section 
		{

			double t1 = omp_get_wtime();
			long long sum = async_aggregate(array2D, 8);
			double t2 = omp_get_wtime();

			cout << "Sum: " << sum << endl;
			cout << "Total time - " << t2 - t1 << " seconds" << endl;
		}
		#pragma omp section 
		{

			double t1 = omp_get_wtime();
			tuple<long long, int> min_data = async_get_min_row_sum(array2D, 8);
			double t2 = omp_get_wtime();

			cout << "Min row sum: " << get<0>(min_data) << endl;
			cout << "Min row sum index: " << get<1>(min_data) << endl;
			cout << "Total time - " << t2 - t1 << " seconds" << endl;
		}
	}
}

tuple<long long, int> async_get_min_row_sum(vector<vector<int>>& arr, int threads_amount) {
	long long min_sum = LLONG_MAX;
	int min_row_index = -1;

	#pragma omp parallel for num_threads(threads_amount)
	for (int i = 0; i < arr.size(); i++) {
		long long row_sum = 0;

		for (int j = 0; j < arr[i].size(); j++) {
			row_sum += arr[i][j];
		}

		#pragma omp critical 
		{
			if (row_sum < min_sum) {
				min_sum = row_sum;
				min_row_index = i;
			}
		}
	}
	return make_tuple(min_sum, min_row_index);
}


long long async_aggregate(vector<vector<int>>& arr, int threads_amount) {
	long long sum = 0;
	
	#pragma omp parallel for reduction(+:sum) num_threads(threads_amount)
	for (int i = 0; i < arr.size(); i++)
	{
		for (int j = 0; j < arr[i].size(); j++)
		{
			int col = arr[i][j];

			sum += col;
		}
	}

	return sum;
}
