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
using System.Threading.Tasks;
using System.Web;

namespace LINQTOMOZ
{
	internal class MozQueryContext<TMetricsType> 
	{
		AuthCalucalation authUrl = null;
		private Type _type;
		internal MozQueryContext(MozAuth authUrl)
		{
			this.authUrl = (AuthCalucalation)authUrl;
	
		}
		// Executes the expression tree that is passed to it. 
		internal object Execute(Expression expression, bool IsEnumerable)
		{
			// The expression must represent a query over the data source. 
			if (!IsQueryOverDataSource(expression))
				throw new InvalidProgramException("No query over the data source was specified.");

			// Find the call to Where() and get the lambda expression predicate.
			WhereFinder whereFinder = new WhereFinder();
			MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
			LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

			// Send the lambda expression through the partial evaluator.
			lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

			TakeFinder takeFinder = new TakeFinder();
			MethodCallExpression takeExpression = takeFinder.GetInnermostTake(expression);
			int limit = 10;
			if(takeExpression != null)
				limit = (int)((ConstantExpression)(takeExpression.Arguments[1])).Value;


			List<TMetricsType> returnedMetrics = GetWebServiceResponse(lambdaExpression.Body, limit);



			TMetricsType[] metricsResult = returnedMetrics.ToArray();
		
			IQueryable<TMetricsType> queryableResult = metricsResult.AsQueryable<TMetricsType>();


			ExpressionTreeModifier<TMetricsType> treeCopier = new ExpressionTreeModifier<TMetricsType>(queryableResult);
			Expression newExpressionTree = treeCopier.Visit(expression);

			// This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods. 
			if (IsEnumerable)
			{
				return queryableResult.Provider.CreateQuery(newExpressionTree);
			}else
			{
				return queryableResult.Provider.Execute(newExpressionTree);
			}
				
		}

		private List<TMetricsType> GetWebServiceResponse(Expression body, int limit)
		{
			if (typeof(TMetricsType) == typeof(LinkMetrics))
			{
				LinkMetricsFinder LinkFinder = new LinkMetricsFinder(body);

				URLMetricsCols metricsCols = LinkFinder.CollMetrics;
				List<String> urls = LinkFinder.URL;

				if (metricsCols == 0)
					throw new InvalidQueryException("You must specify at least one metrics in your query.");

				if (urls == null)
					throw new InvalidQueryException("You must specify at url in your query.");

				String sourceDomainUrl = LinkFinder.SourceDomainURL;

				// Check sourceDomain. It must be specified for link metrics
				//if (String.IsNullOrEmpty(sourceDomainUrl))
				//	throw new InvalidQueryException("You must specify source domain for link metrics");

				URLMetricsCols targetMetricsCols = LinkFinder.TergetMetrics;

				LinkMetricsCols linkMetricsCols = LinkFinder.LinkCols;

				String scope = LinkFinder.Scope;

				if (String.IsNullOrEmpty(scope))
				{
					scope = ScopeType.PageToPage;
				}

				String sort = LinkFinder.Sort;

				if (string.IsNullOrEmpty(sort))
					sort = Sort.PageAuthority;



				List<LinkMetrics> searchingList = new List<LinkMetrics>();

				foreach (var url in urls)
				{
					LinkMetrics linkMetrics = new LinkMetrics();
					linkMetrics.LinkCols = linkMetricsCols;
					linkMetrics.SearchingURL = url;
					linkMetrics.Scope = scope;
					linkMetrics.Sort = sort;
					linkMetrics.SourceCols = metricsCols;
					linkMetrics.SourceDomainURL = sourceDomainUrl;
					linkMetrics.TargetCols = targetMetricsCols;
					searchingList.Add(linkMetrics);
				}

				ParallelsLinkMetricsWorker parallelWorker = new ParallelsLinkMetricsWorker(searchingList, authUrl, limit, 0);

				List<LinkMetrics> returnedMetrics = parallelWorker.GetMetricsResult();

				return returnedMetrics.Cast<TMetricsType>().ToList();
			}
			else
			{
				URLMetricsFinder URLfinder = new URLMetricsFinder(body);
				URLMetricsCols metricsCols = URLfinder.CollMetrics;
				List<String> urls = URLfinder.URL;
				if (metricsCols == 0)
					throw new InvalidQueryException("You must specify at least one metrics in your query.");

				if (urls == null)
					throw new InvalidQueryException("You must specify at url in your query.");


				List<URLMetrics> urlMetricsWorker = new List<URLMetrics>();

				foreach (var url in urls)
				{

					URLMetrics urlMetricWorker = new URLMetrics();
					urlMetricWorker.SearchingURL = url;
					urlMetricWorker.SourceCols = metricsCols;
					urlMetricsWorker.Add(urlMetricWorker);
				}

				ParallelsURLMetricsWorker serverRequester = new ParallelsURLMetricsWorker(urlMetricsWorker, authUrl);

				List<URLMetrics> returnedMetrics = serverRequester.GetMetricsResult();

				return returnedMetrics.Cast<TMetricsType>().ToList();

			}
		}

		private static bool IsQueryOverDataSource(Expression expression)
		{
			// If expression represents an unqueried IQueryable data source instance, 
			// expression is of type ConstantExpression, not MethodCallExpression. 
			return (expression is MethodCallExpression);
		}
	}
}
