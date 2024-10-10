using System;

namespace MatrixCalculator
{
  class Matrix
  {
    private double[,] data;
    public int Rows { get; }
    public int Columns { get; }

    public Matrix(int rows, int columns)
    {
      Rows = rows;
      Columns = columns;
      data = new double[rows, columns];
    }

    public void FillManually()
    {
      Console.WriteLine($"Заполнение матрицы размером {Rows}x{Columns}:");
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Columns; j++)
        {
          Console.WriteLine($"Введите элемент [{i + 1},{j + 1}]: ");
          data[i, j] = double.Parse(Console.ReadLine());
        }
      }
    }

    public void FillRandomly(double a, double b)
    {
      Random rand = new Random();
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Columns; j++)
        {
          data[i, j] = a + rand.NextDouble() * (b - a);
        }
      }
    }

    public void Print(string name = "Матрица")
    {
      Console.WriteLine($"{name}:");
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Columns; j++) Console.Write($"{data[i, j]:F2}\t");
        Console.WriteLine();
      }
      Console.WriteLine();
    }

    public static Matrix Add(Matrix a, Matrix b)
    {
      if (a.Rows != b.Rows || a.Columns != b.Columns) throw new InvalidOperationException("Размерности матриц не совпадают для операции сложения.");

      Matrix result = new Matrix(a.Rows, a.Columns);
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < a.Columns; j++)
        {
          result.data[i, j] = a.data[i, j] + b.data[i, j];
        }
      }
      return result;
    }

    public static Matrix Multiply(Matrix a, Matrix b)
    {
      if (a.Columns != b.Rows) throw new InvalidOperationException("Матрицы несовместимы для умножения.");

      Matrix result = new Matrix(a.Rows, b.Columns);
      for (int i = 0; i < a.Rows; i++)
      {
        for (int j = 0; j < b.Columns; j++)
        {
          for (int k = 0; k < a.Columns; k++) result.data[i, j] += a.data[i, k] * b.data[k, j];
        }
      }
      return result;
    }

    public Matrix Transpose()
    {
      Matrix result = new Matrix(Columns, Rows);
      for (int i = 0; i < Rows; i++)
      {
        for (int j = 0; j < Columns; j++) result.data[j, i] = data[i, j];
      }
      return result;
    }

    public double Determinant()
    {
      if (Rows != Columns) throw new InvalidOperationException("Детерминант можно вычислить только для квадратной матрицы.");
      return CalculateDeterminant(data);
    }

    private double CalculateDeterminant(double[,] matrix)
    {
      int n = matrix.GetLength(0);
      if (n == 1) return matrix[0, 0];
      if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

      double det = 0;
      for (int p = 0; p < n; p++)
      {
        double[,] subMatrix = CreateSubMatrix(matrix, 0, p);
        det += matrix[0, p] * Math.Pow(-1, p) + CalculateDeterminant(subMatrix);
      }
      return det;
    }

    private double[,] CreateSubMatrix(double[,] matrix, int excludeRow, int excludeColumn)
    {
      int n = matrix.GetLength(0);
      double[,] result = new double[n - 1, n - 1];
      int rowOffset = 0;

      for (int i = 0; i < n; i++)
      {
        if (i == excludeRow)
        {
          rowOffset = -1;
          continue;
        }
        int colOffset = 0;
        for (int j = 0; j < n; j++)
        {
          if (j == excludeColumn)
          {
            colOffset = -1;
            continue;
          }
          result[i + rowOffset, j + colOffset] = matrix[i, j];
        }
      }
      return result;
    }

    public Matrix Inverse()
    {
      double det = Determinant();
      if (det == 0) throw new InvalidOperationException("Обратная матрица не существует, так как детерминант равен нулю.");

      int n = Rows;
      Matrix adjugate = new Matrix(n, n);

      for (int i = 0; i < n; i++)
      {
        for (int j = 0; j < n; j++)
        {
          double[,] subMatrix = CreateSubMatrix(data, i, j);
          adjugate.data[j, i] = Math.Pow(-1, i + j) * CalculateDeterminant(subMatrix);
        }
      }
      return adjugate.MultiplyByScalar(1 / det);
    }

    public Matrix MultiplyByScalar(double scalar)
    {
      Matrix result = new Matrix(Rows, Columns);
      for (int i = 0; i < Rows; i++)
        for (int j = 0; j < Columns; j++)
          result.data[i, j] = data[i, j] * scalar;
      return result;
    }

    public double[] Solve(double[] b)
    {
      if (Rows != Columns)
        throw new InvalidOperationException("Система может быть решена только для квадратной матрицы.");

      int n = Rows;
      double[,] augmentedMatrix = new double[n, n + 1];
      for (int i = 0; i < n; i++)
      {
        for (int j = 0; j < n; j++)
          augmentedMatrix[i, j] = data[i, j];
        augmentedMatrix[i, n] = b[i];
      }
      for (int i = 0; i < n; i++)
      {
        if (augmentedMatrix[i, i] == 0)
          throw new InvalidOperationException("Система не имеет однозначного решения.");

        for (int j = i + 1; j < n; j++)
        {
          double factor = augmentedMatrix[j, i] / augmentedMatrix[i, i];
          for (int k = i; k <= n; k++)
            augmentedMatrix[j, k] -= factor * augmentedMatrix[i, k];
        }
      }
      double[] x = new double[n];
      for (int i = n - 1; i >= 0; i--)
      {
        x[i] = augmentedMatrix[i, n];
        for (int j = i + 1; j < n; j++)
          x[i] -= augmentedMatrix[i, j] * x[j];
        x[i] /= augmentedMatrix[i, i];
      }
      return x;
    }
  }

  class Program {
    static void Main(string[] args){
      try {
        Console.Write("Введите количество строк n: ");
        int n = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов m: ");
        int m = int.Parse(Console.ReadLine());

        Matrix matrixA = new Matrix(n, m);
        Matrix matrixB = new Matrix(n, m);

        Console.WriteLine("Выберите способ заполнения матриц:");
        Console.WriteLine("1 - Ввести вручную");
        Console.WriteLine("2 - Заполнить случайными числами");
        Console.Write("Ваш выбор: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1) {
          matrixA.FillManually();
          matrixB.FillManually();
        } else if (choice == 2) {
          Console.Write("Введите начало диапазона a: ");
          double a = double.Parse(Console.ReadLine());
          Console.Write("Введите конец диапазона b: ");
          double b = double.Parse(Console.ReadLine());
          matrixA.FillRandomly(a, b);
          matrixB.FillRandomly(a, b);
        } else {
          Console.WriteLine("Некорректный выбор");
          return;
        } 

        matrixA.Print("Матрица A");
        matrixB.Print("Матрица B");

        try
        {
          Matrix sum = Matrix.Add(matrixA, matrixB);
          sum.Print("Сумма матриц A и B");
        }
        catch (InvalidOperationException ex)
        {
          Console.WriteLine($"Ошибка при сложении матриц: {ex.Message}");
        }

        try
        {
          Matrix product = Matrix.Multiply(matrixA, matrixB);
          product.Print("Произведение матриц A и B");
        }
        catch (InvalidOperationException ex)
        {
          Console.WriteLine($"Ошибка при умножении матриц: {ex.Message}");
        }

        try
        {
          double detA = matrixA.Determinant();
          Console.WriteLine($"Детерминант матрицы A: {detA:F2}");
        }
        catch (InvalidOperationException ex)
        {
          Console.WriteLine($"Ошибка при вычислении детерминанта матрицы A: {ex.Message}");
        }

        try
        {
          Matrix inverseA = matrixA.Inverse();
          inverseA.Print("Обратная матрица к A");
        }
        catch (InvalidOperationException ex)
        {
          Console.WriteLine($"Ошибка при вычислении обратной матрицы A: {ex.Message}");
        }

        Matrix transposeA = matrixA.Transpose();
        transposeA.Print("Транспонированная матрица A");

        try
        {
          Console.WriteLine("Введите свободные члены системы уравнений:");
          double[] b = new double[n];
          for (int i = 0; i < n; i++)
          {
            Console.Write($"b[{i + 1}]: ");
            b[i] = double.Parse(Console.ReadLine());
          }
          double[] solution = matrixA.Solve(b);
          Console.WriteLine("Решение системы уравнений:");
          for (int i = 0; i < solution.Length; i++)
            Console.WriteLine($"x[{i + 1}] = {solution[i]:F2}");
        }
        catch (InvalidOperationException ex)
        {
          Console.WriteLine($"Ошибка при решении системы уравнений: {ex.Message}");
        }
      }
      catch (Exception ex){
        Console.WriteLine($"Общая ошибка: {ex.Message}");
      }

      try {
        Matrix m1 = new Matrix(2, 3);
        Matrix m2 = new Matrix(3, 2);
        Matrix sum = Matrix.Add(m1, m2);
      } catch (InvalidOperationException ex){
        Console.WriteLine($"Негативный сценарий сложения матриц: {ex.Message}");
      }

      try
      {
        Matrix m1 = new Matrix(2, 3);
        Matrix m2 = new Matrix(4, 2);
        Matrix product = Matrix.Multiply(m1, m2);
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"Негативный сценарий умножения матриц: {ex.Message}");
      }

      try
      {
        Matrix nonSquare = new Matrix(2, 3);
        double det = nonSquare.Determinant();
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"Негативный сценарий вычисления детерминанта: {ex.Message}");
      }

      try
      {
        Matrix singular = new Matrix(2, 2);
        singular.FillManually();
        Matrix inverse = singular.Inverse();
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"Негативный сценарий вычисления обратной матрицы: {ex.Message}");
      }
      try
      {
        Matrix m = new Matrix(2, 2);
        m.FillManually();
        double[] b = new double[2];
        Console.WriteLine("Введите свободные члены для системы без однозначного решения:");
        for (int i = 0; i < 2; i++)
        {
          Console.Write($"b[{i + 1}]: ");
          b[i] = double.Parse(Console.ReadLine());
        }
        double[] solution = m.Solve(b);
      }
      catch (InvalidOperationException ex)
      {
        Console.WriteLine($"Негативный сценарий решения системы уравнений: {ex.Message}");
      }
    }
  }
}