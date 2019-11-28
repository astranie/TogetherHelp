

using Microsoft.AspNetCore.Mvc.Filters;


namespace UI.Filter
{
    public class ErrorLogAttribute : ExceptionFilterAttribute//应该是和EntityFramework是不一样的
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}
