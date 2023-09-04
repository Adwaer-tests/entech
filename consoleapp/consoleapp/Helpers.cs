namespace consoleapp
{
    public static class Helpers
	{
        /// <summary>
        /// Calculate area count
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Calculate(this string input)
        {
            var rows = input.Split(';');
            var colCount = rows[0].Split(',').Length;
            var rowCount = 2;

            var matrix = rows.ParseToMatrix(colCount);
            var count = 0;

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < colCount; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        matrix.ClearCellArea(i, j, colCount);
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Checks if input correct. If not - returns string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string? ValidateStringMatrix(this string? input)
        {
            if (input == null) return "Input is empty";

            string[] rows = input.Split(';');
            if (rows.Length != 2) return "Row count is not 2";

            var rowOne = rows[0]!; var rowTwo = rows[1]!;
            if (rowOne.Length != rowTwo.Length) return "Rows have different lengths";
            if (rowOne.Length < 2) return "Rows should have at least 2 digits";

            int rowCount = rowOne.Length;
            var predSymbolIsComma = true;
            for (var i = 0; i < rowCount; i++)
            {
                if (predSymbolIsComma)
                {
                    if (rowOne[i] != '0' && rowOne[i] != '1')
                    {
                        return $"Enexpected symbol {rowOne[i]} (Expected: 0 or 1)";
                    }

                    if (rowTwo[i] != '0' && rowTwo[i] != '1')
                    {
                        return $"Enexpected symbol {rowTwo[i]}  (Expected: 0 or 1)";
                    }
                    predSymbolIsComma = false;
                }
                else
                {
                    if (rowOne[i] != ',')
                    {
                        return $"Enexpected symbol {rowOne[i]} (Expected: ,)";
                    }

                    if (rowTwo[i] != ',')
                    {
                        return $"Enexpected symbol {rowTwo[i]}  (Expected: ,)";
                    }
                    predSymbolIsComma = true;
                }

            }

            return null;
        }

        /// <summary>
        /// Parse rows to int matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="colCount"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
		public static int[,] ParseToMatrix(this string[] rows, int colCount, int rowCount = 2)
		{
            int[,] matrix = new int[rowCount, colCount];
            for (int i = 0; i < rowCount; i++)
            {
                string[] values = rows[i].Split(',');
                for (int j = 0; j < colCount; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }

            return matrix;
        }

        /// <summary>
        /// Nullifies neighbord cells (it's not gonna work for col count > 2
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="colCount"></param>
        /// <param name="rowCount"></param>
        public static void ClearCellArea(this int[,] matrix, int row, int col, int colCount, int rowCount = 2)
        {
            if(row < 0) return;
            if (row >= rowCount) return;
            if (col < 0) return;
            if (col >= colCount) return;
            if (matrix[row, col] == 0) return;

            matrix[row, col] = 0;

            matrix.ClearCellArea(row - 1, col, colCount, rowCount);
            matrix.ClearCellArea(row + 1, col, colCount, rowCount);
            matrix.ClearCellArea(row, col - 1, colCount, rowCount);
            matrix.ClearCellArea(row, col + 1, colCount, rowCount);
        }
    }
}

