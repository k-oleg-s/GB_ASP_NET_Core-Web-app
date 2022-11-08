

using Lesson01;

Console.WriteLine("program start");

var b = new Blog();
await b.GetPosts();

Console.WriteLine("program end");