﻿// Copyright 2007-2010 The Apache Software Foundation.
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
namespace Magnum.Validation.Conditions
{
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using Impl;


	public class MatchesValidator :
		Validator<string>
	{
		readonly Regex _regex;

		public MatchesValidator(Regex regex)
		{
			_regex = regex;
		}

		public IEnumerable<Violation> Validate(string value)
		{
			bool result = _regex.IsMatch(value ?? "");

			if (!result)
				yield return new ValidatorViolation<string>("did not match expression: " + _regex);
		}
	}
}