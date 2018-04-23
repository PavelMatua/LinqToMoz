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
using System.Linq;
using System.Linq.Expressions;

namespace LINQTOMOZ
{
	internal class MozQueryProvider: IQueryProvider 
	{
		private MozAuth _mozAuth = null;
		private Type _type;
		public MozQueryProvider(MozAuth mozAuth, Type classType)
		{
			this._mozAuth = mozAuth;
			this._type = classType;
		}
		public IQueryable CreateQuery(Expression expression)
		{
			Type elementType = TypeSystem.GetElementType(expression.Type);
			try
			{
				return (IQueryable)Activator.CreateInstance(typeof(QueryableMozData<>).MakeGenericType(elementType), new object[] { this, expression });
			}
			catch (System.Reflection.TargetInvocationException tie)
			{
				throw tie.InnerException;
			}
		}

		public Boolean AuthIsNull()
		{
			return this._mozAuth == null;
		}

		public void setAuth(MozAuth authentication)
		{
			this._mozAuth = authentication;
		}

		// Queryable's collection-returning standard query operators call this method. 
		public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
		{
			

			//MozQueryProvider<TData> dataType = (MozQueryProvider<TData>)this.MemberwiseClone();
			return new QueryableMozData<TResult>(this, expression);
		}

		public object Execute(Expression expression)
		{
			if (_type == typeof(URLMetrics))
			{
				MozQueryContext<URLMetrics> context = new MozQueryContext<URLMetrics>(_mozAuth);
				return context.Execute(expression, false);
			}
			else
			{
				MozQueryContext<LinkMetrics> context = new MozQueryContext<LinkMetrics>(_mozAuth);
				return context.Execute(expression, false);
			}


		}

		// Queryable's "single value" standard query operators call this method.
		// It is also called from QueryableTerraServerData.GetEnumerator(). 
		public TResult Execute<TResult>(Expression expression)
		{
			bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");
			if (_type == typeof(URLMetrics))
			{
				MozQueryContext<URLMetrics> context = new MozQueryContext<URLMetrics>(_mozAuth);
				TResult reslt = (TResult)context.Execute(expression, IsEnumerable);
				return reslt;
			}
			else
			{
				MozQueryContext<LinkMetrics> context = new MozQueryContext<LinkMetrics>(_mozAuth);
				TResult reslt = (TResult)context.Execute(expression, IsEnumerable);
				return reslt;
			}

		}
	}
}