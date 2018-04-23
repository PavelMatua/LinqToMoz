﻿/* Copyright 2017 Pavel MATUA

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Linq.Expressions;

namespace LINQTOMOZ
{
	public class TakeFinder : ExpressionVisitor
	{
		private MethodCallExpression innermostWhereExpression;

		public MethodCallExpression GetInnermostTake(Expression expression)
		{
			Visit(expression);
			return innermostWhereExpression;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (expression.Method.Name == "Take")
				innermostWhereExpression = expression;

			Visit(expression.Arguments[0]);

			return expression;
		}
	}
}
