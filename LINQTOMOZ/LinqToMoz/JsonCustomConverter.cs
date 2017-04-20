using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LINQTOMOZ
{
	public class JsonCustomConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return false;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				return serializer.Deserialize(reader, objectType);
			}

			if (reader.ValueType == typeof(Int64))
			{
				long initialValue = (long)reader.Value;
				List<String> result = new List<String>();
				while (initialValue != 0)
				{
					var list = Enum.GetValues(typeof(LinkFlags)).Cast<int>().ToList();
					var closeNumber = list.Where(x => x <= initialValue).OrderBy(item => Math.Abs(initialValue - item)).First();
					initialValue -= closeNumber;
					result.Add(Enum.GetName(typeof(LinkFlags), closeNumber).ToString());
					//result = result + Enum.GetName(typeof(LinkFlags), closeNumber).ToString() + ";";


				}
				return result;
			}

			if (reader.ValueType == typeof(String))
				return reader.Value.ToString().Split(' ').ToList();
			else return serializer.Deserialize(reader, objectType);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
