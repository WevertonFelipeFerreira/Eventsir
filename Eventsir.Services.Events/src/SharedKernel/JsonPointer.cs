using SharedKernel.Utils;
using System.Linq.Expressions;

namespace SharedKernel
{
    public class JsonPointer
    {
        public static string Point<T>(Expression<Func<T, object>> expression) =>
            $"#/{GetMemberPath(expression.Body).Replace('.', '/')}";

        private static string GetMemberPath(Expression expression)
        {
            if (expression is MemberExpression memberExpression)
            {
                var parentPath = GetMemberPath(memberExpression.Expression);
                return string.IsNullOrEmpty(parentPath) ? memberExpression.Member.Name.ToCamelCase() : $"{parentPath}.{memberExpression.Member.Name.ToCamelCase()}";
            }
            else if (expression is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
            {
                return GetMemberPath(unaryExpression.Operand);
            }
            return string.Empty;
        }
    }
}
