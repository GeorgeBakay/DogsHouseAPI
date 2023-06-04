

namespace DogsHouseAPI.Middleware
{
    public class RateLimitMiddleware : IMiddleware
    {
       
        static int _maxRequests = 1000;
        static TimeSpan _interval = TimeSpan.FromSeconds(1);
        static Queue<DateTime> _requestTimes = new Queue<DateTime>();

       
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {   
            if (_requestTimes.Count >= _maxRequests)
            {
                DateTime oldestRequestTime = _requestTimes.Peek();
                if (DateTime.Now - oldestRequestTime < _interval)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    Console.WriteLine("Too many requests. Please try again later.");
                    return;
                }
                else
                {
                    _requestTimes.Dequeue();
                }
            }        
            _requestTimes.Enqueue(DateTime.Now);
            await next.Invoke(context);
        }
    }
}
