# MusterNgNewsfeedRepo

# ASPNET MVC FILTER
ASP.NET MVC Filter is a custom class where you can write custom logic to execute before or after an action method executes. Filters can be applied to an action method or controller in a declarative or programmatic way. Declarative means by applying a filter attribute to an action method or controller class and programmatic means by implementing a corresponding interface. 

MVC provides different types of filters. Below are some in-built filters in ASPNET MVC
  -Authorization [AllowAnonymous]
  -Result [OutputCache]
  -Exception [HandleError]
  
  most times filters are added to the top of a controller or at the top of method. for example if [AllowAnonymous] attribute is added to the top of controller,all request call to that controller is granted without passing through authentication phase.

# LIBRARY BINDING In XAMARIN
Library binding in xamarin is basically an act of making use of codes/libraries(e.g written in java) that are not native to xamarin, i.e c-sharp in a xamarin project.Library bindings automatically wraps the library with C# wrappers so you can invoke Java or other language's code via C-sharp calls.

# ABOUT THE NEWSFEED PROJECT
- Please change the Project Url in NewsApi.Integration\NewsFeed\App\app.ts to your system project url
- Note: There is a database script called newsfeed.sql, do ensure to run the script and change the connection string options in the       web.config file   in the NewsFeed folder to your system database options.

- There is two other project involved in the project: NewsApi.Integration for the implementation of NewsAPi 
