// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum.Specs.Serialization
{
	using System;
	using System.Collections.Generic;
	using TestFramework;

	[Scenario]
	public class When_serializing_an_array_of_ints :
		With_the_fast_text_serializer
	{
		private string _body;
		private int[] _value;

		[When]
		public void An_array_of_int_is_serialized()
		{
			_value = new[] {8, 6, 7, 5, 3, 0, 9};
			_body = Subject.Serialize(_value);
		}

		[Then]
		public void Should_create_the_proper_serialized_body()
		{
			_body.ShouldEqual("[8,6,7,5,3,0,9]");
		}

		[Then]
		public void Should_deserialize_to_the_proper_value()
		{
			var value = Subject.Deserialize<int[]>(_body);

			value.ShouldEqual(_value);
		}
	}

	[Scenario]
	public class When_serializing_a_list_of_ints :
		With_the_fast_text_serializer
	{
		private string _body;
		private IList<int> _value;

		[When]
		public void An_array_of_int_is_serialized()
		{
			_value = new[] {8, 6, 7, 5, 3, 0, 9};
			_body = Subject.Serialize(_value);
		}

		[Then]
		public void Should_create_the_proper_serialized_body()
		{
			_body.ShouldEqual("[8,6,7,5,3,0,9]");
		}

		[Then]
		public void Should_deserialize_to_the_proper_value()
		{
			var value = Subject.Deserialize<IList<int>>(_body);

			value.ShouldEqual(_value);
		}
	}

	[Scenario]
	public class When_serializing_an_array_of_strings :
		With_the_fast_text_serializer
	{
		private string _body;
		private string[] _value;

		[When]
		public void An_array_of_string_is_serialized()
		{
			_value = new[] {"One", "Two", "Three", "Four", "Five"};
			_body = Subject.Serialize(_value);
		}

		[Then]
		public void Should_create_the_proper_serialized_body()
		{
			_body.ShouldEqual("[One,Two,Three,Four,Five]");
		}

		[Then]
		public void Should_deserialize_to_the_proper_value()
		{
			var value = Subject.Deserialize<string[]>(_body);

			value.ShouldEqual(_value);
		}
	}

	[Scenario]
	public class When_serializing_an_array_of_objects :
		With_the_fast_text_serializer
	{
		private string _body;
		private TestClass[] _value;

		[When]
		public void An_array_of_string_is_serialized()
		{
			_value = new[]
				{
					new TestClass {Id = new Guid("46e921bdb16d482f93bcdb9642cfe55d"), Name = "Alpha"},
					new TestClass {Id = new Guid("af8ae4c846a64ec2a060acccf06e0eda"), Name = "Beta"},
					new TestClass {Id = new Guid("832773fa604d490d904e0df5334683cd"), Name = "Gemma"},
				};

			_body = Subject.Serialize(_value);
		}

		[Then]
		public void Should_create_the_proper_serialized_body()
		{
			_body.ShouldEqual("[{Id:46e921bdb16d482f93bcdb9642cfe55d,Name:Alpha},{Id:af8ae4c846a64ec2a060acccf06e0eda,Name:Beta},{Id:832773fa604d490d904e0df5334683cd,Name:Gemma}]");
		}

		[Then]
		public void Should_deserialize_to_the_proper_value()
		{
			var value = Subject.Deserialize<TestClass[]>(_body);

			value.ShouldEqual(_value);
		}

		private class TestClass
		{
			public Guid Id { get; set; }
			public string Name { get; set; }

			public bool Equals(TestClass other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return other.Id.Equals(Id) && Equals(other.Name, Name);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != typeof (TestClass)) return false;
				return Equals((TestClass) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return (Id.GetHashCode()*397) ^ (Name != null ? Name.GetHashCode() : 0);
				}
			}
		}
	}

	[Scenario]
	public class When_serializing_an_array_of_strings_with_special_characters :
		With_the_fast_text_serializer
	{
		private string _body;
		private string[] _value;

		[When]
		public void An_array_of_string_is_serialized()
		{
			_value = new[] {"Jackson, Andrew", "Washington, George", "Jefferson, Thomas", "Lincoln, Abraham", "Ford, Gerald"};
			_body = Subject.Serialize(_value);
		}

		[Then]
		public void Should_create_the_proper_serialized_body()
		{
			_body.ShouldEqual(
				"[\"Jackson, Andrew\",\"Washington, George\",\"Jefferson, Thomas\",\"Lincoln, Abraham\",\"Ford, Gerald\"]");
		}

		[Then]
		public void Should_deserialize_to_the_proper_value()
		{
			var value = Subject.Deserialize<string[]>(_body);

			value.ShouldEqual(_value);
		}
	}
}