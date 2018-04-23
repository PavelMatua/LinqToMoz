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
using System.Linq.Expressions;

namespace LINQTOMOZ
{
	internal class LinkMetricsFinder : MetricsFinder<LinkMetrics>
	{
		
		private URLMetricsCols targetMetricsCols = 0;
		private LinkMetricsCols linkCols = 0;
		private String sourceDomainUrl;
		private String scope;
		private String sort;
		private List<String> filters;




		public URLMetricsCols TergetMetrics
		{
			get
			{
				if (targetMetricsCols == 0)
				{
					this.Visit(base._expression);
				}
				return this.targetMetricsCols;
			}

		}

		public LinkMetricsCols LinkCols
		{
			get
			{
				if (linkCols == 0)
				{
					this.Visit(base._expression);
				}
				return this.linkCols;
			}

		}

		public String SourceDomainURL
		{
			get
			{
				if (sourceDomainUrl == null)
				{
					this.Visit(base._expression);
				}
				return this.sourceDomainUrl;
			}
		}

		public String Scope
		{
			get
			{
				if (scope == null)
				{
					this.Visit(base._expression);
				}
				return this.scope;
			}
		}

		public String Sort
		{
			get
			{
				if (sort == null)
				{
					this.Visit(base._expression);
				}
				return this.sort;
			}
		}





		public LinkMetricsFinder(Expression expression) : base(expression)
		{
			this.filters = new List<string>();
			this.searchingURL = new List<string>();
		}


		protected override Expression VisitBinary(BinaryExpression be)
		{

			if (be.NodeType == ExpressionType.AndAlso)
			{
				return base.VisitBinary(be);

			}
			else if (be.NodeType == ExpressionType.Equal)
			{
				if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "SourceCols"))
				{
					colsMetrics = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<URLMetricsCols>(be, typeof(LinkMetrics), "SourceCols"));
					return be;
				}
				else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "SearchingURL"))
				{
					String uniqueSearchingURL = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<String>(be, typeof(LinkMetrics), "SearchingURL"));
					searchingURL.Add(uniqueSearchingURL);
					return be;
				}
				else if(ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "TargetCols"))
				{
					targetMetricsCols = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<URLMetricsCols>(be, typeof(LinkMetrics), "TargetCols"));
					return be;
				}
				else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "LinkCols"))
				{
					linkCols = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<LinkMetricsCols>(be, typeof(LinkMetrics), "LinkCols"));
					return be;
				}
				else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "Scope"))
				{
					scope = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<String>(be, typeof(LinkMetrics), "Scope"));
					return be;
				}
				else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "SourceDomainURL"))
				{
					sourceDomainUrl = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<String>(be, typeof(LinkMetrics), "SourceDomainURL"));
					return be;
				}
				else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(LinkMetrics), "Sort"))
				{
					sort = (ExpressionTreeHelpers.GetURLMetricColsFromEqualsExpression<String>(be, typeof(LinkMetrics), "Sort"));
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
