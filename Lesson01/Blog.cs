using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Lesson01;

public  class Blog
{
    static readonly HttpClient client = new HttpClient();

    public class Item
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public void Test2()
    {
        string jsontxt =
 @"{
 ""userId"": 1 
}
";

        //            @"{
        //  ""Date"": ""2019-08-01T00:00:00-07:00"",
        //  ""TemperatureCelsius"": 25,
        //  ""Summary"": ""Hot"",
        //  ""DatesAvailable"": [
        //    ""2019-08-01T00:00:00-07:00"",
        //    ""2019-08-02T00:00:00-07:00""
        //  ],
        //  ""TemperatureRanges"": {
        //                ""Cold"": {
        //                    ""High"": 20,
        //      ""Low"": -10
        //                },
        //    ""Hot"": {
        //                    ""High"": 60,
        //      ""Low"": 20
        //    }
        //            },
        //  ""SummaryWords"": [
        //    ""Cool"",
        //    ""Windy"",
        //    ""Humid""
        //  ]
        //}
        //";

        //@"{  "userId": 1,  "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit" }";

        Item? item = JsonSerializer.Deserialize<Item>(jsontxt);

        Console.WriteLine($" {item.userId} {item.id} {item.title} {item.body} ");

    }
    
    public  async Task Test1()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Console.WriteLine(responseBody);

            Item? item = JsonSerializer.Deserialize<Item>(responseBody);

            Console.WriteLine( $" {item.userId} {item.id} {item.title} {item.body} ");

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }
}
