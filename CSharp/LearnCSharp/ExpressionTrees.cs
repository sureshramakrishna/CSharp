using System;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees
{
    public class Modifier : ExpressionVisitor
    {
        public Expression<T> Modify<T>(Expression<T> expression)
        {
            var newBody = Visit(expression.Body);
            return Expression.Lambda<T>(newBody, expression.Parameters);
        }
        protected override Expression VisitBinary(BinaryExpression b)
        {
            if (b.NodeType == ExpressionType.Add)
            {
                Expression left = this.Visit(b.Left); //Visits the given expression
                Expression right = this.Visit(b.Right); //Visits the given expression
                return Expression.MakeBinary(ExpressionType.Subtract, left, right, b.IsLiftedToNull, b.Method);
            }
            return base.VisitBinary(b); //Visits all the children
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var num = Expression.Parameter(typeof(int)); //Creates a parameter node in expression tree
            var one = Expression.Constant(1, typeof(int)); // Creates a constant node
            var increment = Expression.Add(num, one); //Uses parameter node and constant node to create an expression tree which does addition
            var expression = Expression.Lambda<Func<int, int>>(increment, num); //Converts expression tree to Expression
            var func = expression.Compile(); //compiles expression to Func
            func.Invoke(10); // Or func(10); //execites expression/function.

            Modifier modifier = new Modifier();
            expression = modifier.Modify(expression);
            func = expression.Compile();
            var result = func(10); //result will be 9
        }
    }
}
