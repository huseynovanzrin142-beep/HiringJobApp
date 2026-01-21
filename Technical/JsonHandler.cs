namespace FinalProject;

using Newtonsoft.Json;
using Serilog;

public class JsonHandler
{
    public string? FilePath { get; set; }

    public void GetJsonSerializerList<T>(List<T> anyObject)
    {
        SerializeInternal(anyObject);
    }

    public void GetJsonSerializerElement<T>(T anyObject)
    {
        SerializeInternal(anyObject!);
    }

    private void SerializeInternal(object anyObject)
    {
        try
        {
            using (var sw = new StreamWriter(FilePath!))
            {
                using (var jw = new JsonTextWriter(sw))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(jw, anyObject);
                }
            }
            Log.Information("Data successfully saved to {FilePath}", FilePath);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error occurred while writing to file: {FilePath}", FilePath);
        }
    }

    public List<T>? GetJsonDeserializerList<T>()
    {
        return DeserializeInternal<List<T>>();
    }

    public T? GetJsonDeserializerElement<T>()
    {
        return DeserializeInternal<T>();
    }

    public T? DeserializeInternal<T>()
    {
        try
        {
            if (!File.Exists(FilePath) || new FileInfo(FilePath!).Length == 0)
            {
                Log.Warning("File not found or empty: {FilePath}. Returning default value.", FilePath);
                return default;
            }

            using (var sr = new StreamReader(FilePath!))
            {
                using (var jr = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<T>(jr);
                }
            }
        }
        catch (JsonException ex)
        {
            Log.Error(ex, "JSON Format error in file: {FilePath}", FilePath);
            return default;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "General error while reading file: {FilePath}", FilePath);
            return default;
        }
    }
}
