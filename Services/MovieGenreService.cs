public class MovieGenreService
{
    public static Dictionary<string, int> GetGenres()
    {
        string path = "D:\\Master\\An 1 Sem 2\\Programarea dispozitivelor mobile\\Cinemate\\Cinemate\\Resources\\Raw\\genres.txt";
        var genres = new Dictionary<string, int>();
        try
        {
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    genres.Add(parts[0].Trim(), int.Parse(parts[1].Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading genres from file: {ex.Message}");
        }
        return genres;
    }
}