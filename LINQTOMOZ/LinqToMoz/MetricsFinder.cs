/* Copyright 2017 Pavel MATUA

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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LINQTOMOZ
{
	internal abstract class MetricsFinder<TMetricsType> : ExpressionVisitor
	{
		protected Expression _expression;
		protected URLMetricsCols colsMetrics = 0;
        protected List<String> searchingURL;
		protected List<String> filters;
		public MetricsFinder(Expression expression)
		{
			this._expression = expression;
		}

		public URLMetricsCols CollMetrics
		{
			get{
				if (colsMetrics == 0)
				{
					searchingURL = new List<string>();
					this.Visit(this._expression);
				}
				return this.colsMetrics;
			}

		}

		public List<String> URL
		{
			get
			{
				if (searchingURL == null)
				{
					searchingURL = new List<string>();
					this.Visit(this._expression);
				}

				return searchingURL.Select(x => HttpUtility.UrlEncode(x)).ToList();
			}

		}

		public List<String> Filters
		{
			get
			{
				if (filters == null)
				{
					this.Visit(this._expression);
				}

				return filters;
			}

		}

		protected override Expression VisitMethodCall(MethodCallExpression m)
		{
			if (m.Method.Name == "Contains")
			{
				Expression valuesURLExpression = null;
				Boolean searchURL = false;

				Expression valuesFiltersExpression = null;
				Boolean filtersFlag = false;

				if (m.Method.DeclaringType == typeof(Enumerable))
				{
					if (ExpressionTreeHelpers.IsSpecificMemberExpression(m.Arguments[1], typeof(TMetricsType), "SearchingURL"))
					{
						searchURL = true;
						valuesURLExpression = m.Arguments[0];
					}
				}
				else if (m.Method.DeclaringType == typeof(List<string>))
				{
					if (ExpressionTreeHelpers.IsSpecificMemberExpression(m.Arguments[0], typeof(TMetricsType), "SearchingURL"))
					{
						searchURL = true;
						valuesURLExpression = m.Object;
					}
				}

				if (m.Method.DeclaringType == typeof(Enumerable))
				{
					if (ExpressionTreeHelpers.IsSpecificMemberExpression(m.Arguments[1], typeof(TMetricsType), "Filters"))
					{
						filtersFlag = true;
						valuesFiltersExpression = m.Arguments[0];
					}
				}
				else if (m.Method.DeclaringType == typeof(List<string>))
				{
					if (ExpressionTreeHelpers.IsSpecificMemberExpression(m.Arguments[0], typeof(TMetricsType), "Filters"))
					{
						filtersFlag = true;
						valuesFiltersExpression = m.Object;
					}
				}

				if (searchURL && (valuesURLExpression == null || valuesURLExpression.NodeType != ExpressionType.Constant))
					throw new Exception("Could not find the url values.");

				if (filtersFlag && (valuesFiltersExpression == null || valuesFiltersExpression.NodeType != ExpressionType.Constant))
					throw new Exception("Could not find the url values.");

				ConstantExpression ceSearchURL = (ConstantExpression)valuesURLExpression;

				IEnumerable<string> usrsString = (IEnumerable<string>)ceSearchURL.Value;
				searchingURL = new List<string>();
				foreach (string url in usrsString)
					searchingURL.Add(url);

				if (filtersFlag)
				{
					ConstantExpression ceFilters = (ConstantExpression)valuesFiltersExpression;
					IEnumerable<string> filtersString = (IEnumerable<string>)ceFilters.Value;
					filters = new List<string>();
					foreach (string filter in filtersString)
						filters.Add(filter);
				}
				
				return m;
			}

			return base.VisitMethodCall(m);
		}

		protected override Expression VisitBinary(BinaryExpression be)
		{
			
			if (be.NodeType == ExpressionType.AndAlso)
			{
				return base.VisitBinary(be);

			}
			else if (be.NodeType == ExpressionType.Equal)
			{
				if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(TMetricsType), "SourceCols"))
				{
					colsMetrics = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<URLMetricsCols>(be, typeof(TMetricsType), "SourceCols")); 
					return be;
				}
				else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(TMetricsType), "SearchingURL"))
				{
					String uniqueSearchingURL = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<String>(be, typeof(TMetricsType), "SearchingURL"));
					searchingURL.Add(uniqueSearchingURL);
					return be;
				}
				else
					return base.VisitBinary(be);
			}
			else
				return base.VisitBinary(be);
			
				
		}


	}
}
