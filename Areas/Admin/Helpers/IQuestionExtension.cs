namespace PustokBook.Areas.Admin.Helpers
{
    public static class IQuestionExtension
    {
        public static IQueryable<T> Paginaton<T>(this IQueryable<T> query, int page, int take)
            where T : class => query.Skip((page - 1) * take).Take(take);


    }
}
