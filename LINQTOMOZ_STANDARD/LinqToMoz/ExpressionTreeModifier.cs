using System;
using System.Linq;
using System.Linq.Expressions;

namespace LINQTOMOZ
{
	internal class ExpressionTreeModifier<T> : ExpressionVisitor
	{
		private IQueryable<T> queryableMetrics;

		internal ExpressionTreeModifier(IQueryable<T> metrics)
		{
			this.queryableMetrics = metrics;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			// Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
			if (c.Type == typeof(QueryableMozData<T>))
				return Expression.Constant(this.queryableMetrics);
			else
				return c;
		}
	}
}
