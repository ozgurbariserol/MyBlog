using Microsoft.AspNetCore.Mvc.Filters;
using MyBlog.Data.UnitOfWorks;
using MyBlog.Entity.Entities;

namespace MyBlog.Web.Filters.ArticleVisitors
{
    public class ArticleVisitorFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork unitOfWork;
        public bool Disable { get; set; }

        public ArticleVisitorFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          
            List<Visitor> visitors =  unitOfWork.GetRepository<Visitor>().GetAllAsync().Result;

            string getId = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string getUserAgent = context.HttpContext.Request.Headers["User-Agent"];

            Visitor visitor = new()
            {
                IpAddress = getId,
                UserAgent = getUserAgent,
            };

            if(visitors.Any(x=>x.IpAddress == visitor.IpAddress)) {
                return next();
            }
            else
            {
                unitOfWork.GetRepository<Visitor>().AddAsync(visitor);
                unitOfWork.SaveAsync();
            }
            return next();
        }
    }
}
