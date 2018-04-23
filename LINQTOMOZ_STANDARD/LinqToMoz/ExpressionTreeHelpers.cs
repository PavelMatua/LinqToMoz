using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LINQTOMOZ
{

	internal static class ExpressionTreeHelpers
	{
		internal static bool IsMemberEqualsValueExpression(Expression exp, Type declaringType, string memberName)
		{
			if (exp.NodeType != ExpressionType.Equal)
				return false;

			BinaryExpression be = (BinaryExpression)exp;

			// Assert. 
			if (ExpressionTreeHelpers.IsSpecificMemberExpression(be.Left, declaringType, memberName) &&
				ExpressionTreeHelpers.IsSpecificMemberExpression(be.Right, declaringType, memberName))
				throw new Exception("Cannot have 'member' == 'member' in an expression!");

			return (ExpressionTreeHelpers.IsSpecificMemberExpression(be.Left, declaringType, memberName) ||
				ExpressionTreeHelpers.IsSpecificMemberExpression(be.Right, declaringType, memberName));
		}

		internal static bool IsSpecificMemberExpression(Expression exp, Type declaringType, string memberName)
		{
			var expression = GetMemberExpression(exp);
			return((expression != null) &&
				(expression.Member.DeclaringType == declaringType) &&
					((expression.Member.Name == memberName)));

		}

		internal static MemberExpression GetMemberExpression(Expression expression)
		{
			MemberExpression memberExpression = null;

			if (expression is MemberExpression)
			{
				memberExpression = (MemberExpression)expression;

			}
			else if (expression is UnaryExpression)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				memberExpression = (MemberExpression)unaryExpression.Operand;


			}

			return memberExpression;

		}

		internal static T GetURLMetricColsFromEqualsExpression<T>(BinaryExpression be, Type memberDeclaringType, string memberName)
		{
			
			if (be.NodeType != ExpressionType.Equal)
				throw new Exception("There is a bug in this program.");

			if (InsideExpressionType(be.Left.NodeType))
			{
				MemberExpression me = GetMemberExpression(be.Left);


				if (me != null && me.Member.DeclaringType == memberDeclaringType && me.Member.Name == memberName)
				{
					return GetURLMetricsColsFromExpression<T>(be.Right);
				}
				else
					throw new Exception("MemberExpression wasn't found. Probably you are using unsupported expression type.");

			}
			else if (InsideExpressionType(be.Right.NodeType))
			{
				MemberExpression me = GetMemberExpression(be.Right);

				if (me != null && me.Member.DeclaringType == memberDeclaringType && me.Member.Name == memberName)
				{
					return GetURLMetricsColsFromExpression<T>(be.Left);
				}
				else
					throw new Exception("MemberExpression wasn't found. Probably you are using unsupported expression type.");

			}

			// We should have returned by now. 
			throw new Exception("There is a bug in this program.");
		}

		static Boolean InsideExpressionType(ExpressionType expression)
		{
			List<ExpressionType> acceptableExpressions = new List<ExpressionType>() { ExpressionType.MemberAccess, ExpressionType.Convert };
			return acceptableExpressions.Contains(expression);
		}

		internal static T GetURLMetricsColsFromExpression<T>(Expression expression)
		{
			if (expression.NodeType == ExpressionType.Constant)
				return (T)(((ConstantExpression)expression).Value);
			else
				throw new InvalidQueryException(
					String.Format("The expression type {0} is not supported to obtain a value.", expression.NodeType));
		}


	}
}

