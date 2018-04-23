﻿using System;
using System.Linq.Expressions;

namespace LINQTOMOZ
{
	internal class WhereFinder : ExpressionVisitor
	{
		private MethodCallExpression innermostWhereExpression;

		public MethodCallExpression GetInnermostWhere(Expression expression)
		{
			Visit(expression);
			return innermostWhereExpression;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (expression.Method.Name == "Where")
				innermostWhereExpression = expression;

			Visit(expression.Arguments[0]);

			return expression;
		}
	}
}
