using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Infra.Util
{
	public class TResultModel<T> : ResultModel
	{

		public TResultModel(int resultNo, string message) : base(resultNo, message)
		{
		}
		public TResultModel(int resultNo) : base(resultNo)
		{

		}

		public T TModel { get; set; }

	}

	public class PageResultModel<T> : ResultModel
	{

        public List<T> TModel { get; set; }

        public int RecordCount { get; set; }

        public int PageCount { get; set; }
    }
}
