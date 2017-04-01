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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LINQTOMOZ
{
	public class QueryableMozData<TData> : IOrderedQueryable<TData> 
	{

		#region Constructors
		/// <summary> 
		/// This constructor is called by the client to create the data source. 
		/// </summary> 
		public QueryableMozData(MozAuth mozAuth)
		{
			_mozAuth = mozAuth;
			Provider = new MozQueryProvider(mozAuth,typeof(TData)); // Change for own provider
			Expression = Expression.Constant(this);
		}

		internal QueryableMozData(MozQueryProvider provider, Expression expression)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}

			if (provider.AuthIsNull())
			{
				provider.setAuth(_mozAuth);
			}

			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			if (!typeof(IQueryable<TData>).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentOutOfRangeException("expression");
			}

			Provider = provider;
			Expression = expression;
		}


		#endregion

		#region Properties

		public IQueryProvider Provider { get; private set; }
		public Expression Expression { get; private set; }
		private MozAuth _mozAuth { get; set; }

		public Type ElementType
		{
			get { return typeof(TData); }
		}

		#endregion


		#region Enumerators
		public IEnumerator<TData> GetEnumerator()
		{
			IEnumerator<TData> enumer2 = (Provider.Execute<IEnumerable<TData>>(Expression)).GetEnumerator();
			return enumer2;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerator enumer = (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
			return enumer;
		}
		#endregion
	}
}
