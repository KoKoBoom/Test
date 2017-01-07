using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    /*

    public IQueryable<Person> Query(IQueryable<Person> source, string name, string code, string address)
    {
        var result = source;
        if(string.IsNullOrEmpty(name) == false)
            result = source.Where(p => p.Name.Contains(name));
        if (string.IsNullOrEmpty(code) == false)
            result = source.Where(p => p.Code.Contains(code));
        if (string.IsNullOrEmpty(address) == false)
            result = source.Where(p => p.Code.Contains(address));
        return result;
    }

    ↓↓↓↓↓↓↓↓↓↓↓↓将上面代码改为下面↓↓↓↓↓↓↓↓↓

    public IQueryable<Person> Query(IQueryable<Person> source, string name, string code, string address)
    {
        return source
            .WhereIf(p => p.Name.Contains(name), string.IsNullOrEmpty(name) == false)
            .WhereIf(p => p.Code.Contains(code), string.IsNullOrEmpty(code) == false)
            .WhereIf(p => p.Code.Contains(address), string.IsNullOrEmpty(address) == false);
    }

    */
    public static class WhereIfExtension
    {
        /// <summary>
        /// 如果条件不为空，则添加条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
