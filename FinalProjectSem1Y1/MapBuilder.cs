public static class MapBuilder
    {
    public static char[,] ReadTextFile(string fileName)
    {
        List<string> lines = new List<string>();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }
        int numRows = lines.Count;
        int numColumns = lines[0].Length;
        char[,] mapLayout = new char[numRows, numColumns];
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numColumns; j++)
            {
                mapLayout[i, j] = lines[i][j];
            }
        }
        return mapLayout;
    }

    
}