using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BaseApi.Ref
{
    /// <summary>
    /// 动态构造Lambda表达式
    /// </summary>
    /// <typeparam name="T">查询目标实体</typeparam>
    public class ConstructLambda<T> where T : class, new()
    {
        private Type TType;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConstructLambda()
        {
            TType = typeof(T);
        }
        /// <summary>
        /// 构造与表达式
        /// </summary>
        /// <param name="dictionary">构造源</param>
        /// <returns>lambda表达式</returns>
        public Expression<Func<T, bool>> GetAndLambdaExpression(Dictionary<string, string> dictionary)
        {
            Expression expression_return = Expression.Constant(true);
            ParameterExpression expression_param = Expression.Parameter(TType, "p");
            foreach (string key in dictionary.Keys)
            {
                Expression temp = Expression.Equal(Expression.Call(Expression.Property(expression_param, TType.GetProperty(key)), TType.GetMethod("ToString")),
                    Expression.Constant(dictionary[key]));
                expression_return = Expression.And(expression_return, temp);
            }
            return (Expression<Func<T, bool>>)Expression.Lambda<Func<T, bool>>(expression_return, new ParameterExpression[] { expression_param });
        }

        /// <summary>
        /// 构造或表达式
        /// </summary>
        /// <param name="dictionary">构造源</param>
        /// <returns>Lambda表达式</returns>
        public Expression<Func<T, bool>> GetOrLambdaExpression(Dictionary<string, string> dictionary)
        {
            Expression expression_return = Expression.Constant(false);
            ParameterExpression expression_param = Expression.Parameter(TType, "p");
            foreach (string key in dictionary.Keys)
            {
                Expression temp = Expression.Equal(Expression.Call(Expression.Property(expression_param, TType.GetProperty(key)), TType.GetMethod("ToString")),
                    Expression.Constant(dictionary[key]));
                expression_return = Expression.Or(expression_return, temp);
            }
            return (Expression<Func<T, bool>>)Expression.Lambda<Func<T, bool>>(expression_return, new ParameterExpression[] { expression_param });
        }
    }
}
/*
    public class Person
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }

        public Person()
        { }
    }

测试数据
public Dictionary<string, string> dictionary = new Dictionary<string, string>()
{
    {"Name","JT"},{"Sex","男"},{"Age","20"},{"Birthday","02/02/2008"}
};

1，无条件查找：    
new ConstructLambda<Person>().GetAndLambdaExpression(new Dictionary<string, string>()).ToString()返回结果：
p => True

  new ConstructLambda<Person>().GetOrLambdaExpression(new Dictionary<string, string>()).ToString()返回结果：
p => False
 2，多条件查找：
  new ConstructLambda<Person>().GetAndLambdaExpression(dictionary).ToString()返回结果：
p => ((((True And(p.Name.ToString() = "JT")) And(p.Sex.ToString() = "男")) And(p.Age.ToString() = "20")) And(p.Birthday.ToString() = "02/02/2008"))

  new ConstructLambda<Person>().GetOrLambdaExpression(dictionary).ToString()返回结果：
p => ((((False Or(p.Name.ToString() = "JT")) Or(p.Sex.ToString() = "男")) Or(p.Age.ToString() = "20")) Or(p.Birthday.ToString() = "02/02/2008"))
*/
