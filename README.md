

# Alpha-Avg

Hi! This is Alpha Team speaking. The following is a quick review of what we did for our C# Average Calculator and what you can expect while checking our project.
This project is result of using lots of caffeine, sleepless nights and a lot of efforts from:

[Ashkan Khademian](https://github.com/ashkan-khd),
[Sepehr Kianian](https://github.com/sepehrkianian09)


## Files
The project contains of 12 cs classes and There Is A **Docs** Folder Where Contains 2 [`JSON`](https://www.w3schools.com/js/js_json_intro.asp) Files.
> **WARNING**: Please DO NOT change the contents of ''Docs'' folder if you are not familiar with json structure. otherwise can occur exceptions or logical errors.

## Project Structure
- The project only uses the c# language and [`.Net`](https://dotnet.microsoft.com/) console application core.
- Due to parsing data inside json files it uses [`NewTonSoft`](https://www.newtonsoft.com/json) framework.
- The project is structured on [`MVC`](https://www.geeksforgeeks.org/mvc-design-pattern/) design pattern.
- For constructing the data structure you may see Language Integrated Query / [`LINQ`](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/) component a lot.
> **Tip**: Check StudentsMapper.cs or AlphaController.cs files.
- Project uses c# method encapsulation and lambda expressions so you may see bunch of of lambdas an delegate methods inside Controller and View packages
> **Tip**: Check IInitialize.cs or IExecute.cs files.