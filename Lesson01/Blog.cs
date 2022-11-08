using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Lesson01;

public class Blog
{
    static readonly HttpClient client = new HttpClient();
    static List<string> list = new List<string>();
    public class Item
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public async Task GetPosts()
    {
        string httppath = "https://jsonplaceholder.typicode.com/posts/";
        var tasks = new List<Task<HttpResponseMessage>>();

        try
        {
            HttpResponseMessage response;
            for (int i = 4; i < 14; i++)
            {
                tasks.Add(client.GetAsync(httppath + i.ToString()));
            }

            await Task.WhenAll(tasks);

            foreach(Task<HttpResponseMessage> task in tasks)
            {
                response = task.Result;
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                list.Add(responseBody);
            }

             WriteToFileAsync();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    private async Task WriteToFileAsync()
    {
        string currentDir;
        string file = "result.txt";
        currentDir = Directory.GetCurrentDirectory();
        
        StringBuilder stringBuilder = new StringBuilder();

        foreach(string js_itm in list)
        {
            Item? item = JsonSerializer.Deserialize<Item>(js_itm);
            stringBuilder.AppendLine(item.userId.ToString());
            stringBuilder.AppendLine(item.id.ToString());
            stringBuilder.AppendLine(item.title);
            stringBuilder.AppendLine(item.body);
            stringBuilder.AppendLine("");
        }

        using (StreamWriter writer = new StreamWriter(Path.Combine(currentDir, file), false))
        {
            await writer.WriteLineAsync(stringBuilder.ToString());
        } 
    }
}
