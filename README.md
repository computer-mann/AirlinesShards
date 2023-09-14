# Airlines

**Improving my knowledge on optimization techniques **


###Techniques Used:
1. Http Response Caching(Middleware)
   >add cache-control:{value} directive to the request header
   
2. Get the original path when I redirect to an error page
  > var originalPath = HttpContext.Features.Get<IStatusCodeReExecuteFeature>().OriginalPath;

3. Refactored the project structure to use Clean Architecture.

4. Use the HTTP method attributes **ONLY** on my actions when building API's